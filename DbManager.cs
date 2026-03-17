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

                string sorgu = "insert into Kitaplar (kitap_adi, ISBN, Stok_adedi) values (@p1,@p2,@p3)";

                SqlCommand komut = new SqlCommand(sorgu, baglanti);

                komut.Parameters.AddWithValue("@p1", ad);
                komut.Parameters.AddWithValue("@p2", isbn);
                komut.Parameters.AddWithValue("@p3", stok);

                komut.ExecuteNonQuery();


            }
            Console.WriteLine($"{ad} , veritabanına başarıyla eklendi.");
        }

        public string KitapBul(string kitap_adi)
        {
            using (SqlConnection baglanti = new SqlConnection(baglantiCumlesi))
            {
                baglanti.Open();
                string sorgu = "select kitap_adi, Stok_adedi from Kitaplar where kitap_adi like @p1";
                SqlCommand komut = new SqlCommand(sorgu, baglanti);
                komut.Parameters.AddWithValue("@p1", "%" + kitap_adi + "%");

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
                SqlCommand komut = new SqlCommand("select kitap_adi from Kitaplar", baglanti);
                SqlDataReader oku = komut.ExecuteReader();

                while (oku.Read())
                {
                    kitaplar.Add(oku["kitap_adi"].ToString());
                }
            }
            return kitaplar;
        }

        public void KitapSil(string kitap_adi)
        {
            using (SqlConnection baglanti = new SqlConnection(baglantiCumlesi))
            {
                baglanti.Open();
                string sorgu = "delete from Kitaplar where kitap_adi=@p1";
                SqlCommand komut = new SqlCommand(sorgu,baglanti);
                komut.Parameters.AddWithValue("@p1", kitap_adi);

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
                string sorgu = "update Kitaplar set Stok_adedi = @p1 where kitap_adi= @p2";
                SqlCommand komut = new SqlCommand(sorgu,baglanti);
                komut.Parameters.AddWithValue("@p1", yeniStok);
                komut.Parameters.AddWithValue("@p2", kitap_adi);

                komut.ExecuteNonQuery();
                Console.WriteLine($"{kitap_adi} için yeni stok: {yeniStok} olarak güncellendi.");
            }
        }
    }
}
