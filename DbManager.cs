using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace OOPDeneme.DbManager
{
    internal class DbManager : KutuphaneInterface
    {
        private string baglantiCumlesi = "Server=.\\SQLEXPRESS; Database=kutuphaneYonetimi; " +
            "User Id=stajyer; Password=12345; TrustServerCertificate=True;";

        public void KitapEkle(string ad, string isbn, int stok)
        {
            using (SqlConnection baglanti = new SqlConnection(baglantiCumlesi))
            {
                baglanti.Open();

                string sorgu = "insert into Kitaplar (kitap_adi, ISBN, Stok_adedi) values (@kAdi,@isbn,@stok)";

                SqlCommand komut = new SqlCommand(sorgu, baglanti);

                komut.Parameters.AddWithValue("@kAdi", ad);
                komut.Parameters.AddWithValue("@isbn", isbn);
                komut.Parameters.AddWithValue("@stok", stok);

                komut.ExecuteNonQuery();


            }
            Console.WriteLine($"{ad} , veritabanına başarıyla eklendi.");
        }

        public string KitapBul(string kitap_adi)
        {
            using (SqlConnection baglanti = new SqlConnection(baglantiCumlesi))
            {
                baglanti.Open();
                string sorgu = "select kitap_adi, Stok_adedi from Kitaplar where kitap_adi like @kAdi";
                SqlCommand komut = new SqlCommand(sorgu, baglanti);
                komut.Parameters.AddWithValue("@kAdi", "%" + kitap_adi + "%");

                SqlDataReader oku = komut.ExecuteReader();

                if (oku.Read())
                {
                    return $"Sonuç: {oku["kitap_adi"]} , Stok: {oku["Stok_adedi"]}";
                }
            }
            return "Aradığınız kitap bulunamadı.";
        }

        public List<string> TumKitaplariGetir()
        {
            List<string> kitaplar = new List<string>();
            using (SqlConnection baglanti = new SqlConnection(baglantiCumlesi))
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("select kitap_adi , Kitap_id from Kitaplar", baglanti);
                SqlDataReader oku = komut.ExecuteReader();

                while (oku.Read())
                {
                    kitaplar.Add($"ID: {oku["Kitap_id"]} | Ad: {oku["kitap_adi"]}");
                }
            }
            return kitaplar;
        }

        public void KitapSil(string kitap_adi)
        {
            using (SqlConnection baglanti = new SqlConnection(baglantiCumlesi))
            {
                baglanti.Open();
                string sorgu = "delete from Kitaplar where kitap_adi=@kAdi";
                SqlCommand komut = new SqlCommand(sorgu,baglanti);
                komut.Parameters.AddWithValue("@kAdi", kitap_adi);

                int etkilenenSatir=komut.ExecuteNonQuery();

                if (etkilenenSatir>0)
                {
                    Console.WriteLine($"{kitap_adi} başarıyla silindi.");
                }
                else
                {
                    Console.WriteLine("Silinecek kitap bulunamadı.");
                }
            }

        }
        public void KitapStokGuncelle(string kitap_adi, int yeniStok)
        {
            using (SqlConnection baglanti= new SqlConnection(baglantiCumlesi))
            {
                baglanti.Open();
                string sorgu = "update Kitaplar set Stok_adedi = @stok where kitap_adi= @kAdi";
                SqlCommand komut = new SqlCommand(sorgu,baglanti);
                komut.Parameters.AddWithValue("@stok", yeniStok);
                komut.Parameters.AddWithValue("@kAdi", kitap_adi);

                komut.ExecuteNonQuery();
                Console.WriteLine($"{kitap_adi} için yeni stok: {yeniStok} olarak güncellendi.");
            }
        }

        public void KitapOduncVer(int Kitap_id, int Uye_id)
        {
            using (SqlConnection baglanti = new SqlConnection(baglantiCumlesi))
            {
                baglanti.Open();
                string stokAzaltSorgu = "update Kitaplar set Stok_adedi=Stok_adedi-1 where Kitap_id=@kId and Stok_adedi>0";
                SqlCommand stokKomut = new SqlCommand(stokAzaltSorgu,baglanti);

                stokKomut.Parameters.AddWithValue("@kId",Kitap_id);
                int dusenKitap = stokKomut.ExecuteNonQuery();

                if (dusenKitap>0)
                {
                    string islemSorgu = "insert into KitapAlmaİslemleri (Kitap_id, Uye_İd, Alis_tarihi, Islem_durumu) " +
                        "values(@kId, @uId, getdate(), 'Odunc Verildi.')";

                    SqlCommand islemKomut = new SqlCommand(islemSorgu,baglanti);
                    islemKomut.Parameters.AddWithValue("@kId",Kitap_id);
                    islemKomut.Parameters.AddWithValue("@uId",Uye_id);
                    islemKomut.ExecuteNonQuery();

                    Console.WriteLine("Kitap başarıyla verildi. Stok düşürüldü.");
                }
                else
                {
                    Console.WriteLine("Kitap stokta yok.");
                }
            }
        }

        public void KitapIadeAl(int Islem_id,int Kitap_id)
        {
            using (SqlConnection baglanti= new SqlConnection(baglantiCumlesi))
            {
                baglanti.Open();
                string islemGuncelleSorgu = "update KitapAlmaİslemleri set Iade_tarihi=getdate()," +
                    " Islem_durumu='İade Edildi.' where Islem_id=@islemId";
                SqlCommand islemKomut = new SqlCommand(islemGuncelleSorgu,baglanti);
                islemKomut.Parameters.AddWithValue("@islemId",Islem_id);
                islemKomut.ExecuteNonQuery();


                string stokArttirSorgu = "update Kitaplar set Stok_adedi=Stok_adedi+1 where Kitap_id=@kId";
                SqlCommand stokKomut = new SqlCommand(stokArttirSorgu,baglanti);
                stokKomut.Parameters.AddWithValue("@kId",Kitap_id);
                stokKomut.ExecuteNonQuery();

                Console.WriteLine("Kitap iade alındı, stok arttırıldı.");
            }
        }
    }
}
