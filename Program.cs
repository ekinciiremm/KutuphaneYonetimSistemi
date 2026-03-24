using Microsoft.Data.SqlClient;
using OOPDeneme.DbManager;
using System.Collections.Generic;
using System;

namespace OOPDeneme
{
    class Program
    {
        static IDbServis servis = new DbManager.DbManager();
        static void Main(string[] args)
        {
            

            bool secimYap = true;

            Console.WriteLine("Kütüphane Yönetimine Hoşgeldiniz.");

            while (secimYap)
            {
                Console.WriteLine("1- Kitap Ekle");
                Console.WriteLine("2- Kitap Sil");
                Console.WriteLine("0-Çıkış");

                string secim= Console.ReadLine();

                switch (secim) {
                    case "1":
                        KitapEkle();
                        break;
                        
                    case "2": 
                        KitapSil();
                        break;

                    case "0":
                        secimYap=false;
                        break;

                }

            }
              
        } 


        static void KitapEkle()
            {
            try {  Console.WriteLine("Kitap Adı: ");
                string ad= Console.ReadLine();

                Console.WriteLine("Stok Sayısı");
                int stok=int.Parse(Console.ReadLine());

                string sorgu = "insert into Kitaplar (Kitap_adi, Stok_adedi)  values (@kAdi,@sAdedi)";
                var parametre = new List<SqlParameter> {
                    new SqlParameter ("@kAdi",ad),
                    new SqlParameter("@sAdedi",stok)
                };

                servis.ExecuteCommand(sorgu, parametre);
                Console.WriteLine("Kitap eklendi.");
            
            }
            catch (FormatException)
            {
                Console.WriteLine("Hata: Stok sayısı içi sayı giriniz.");
            }
            catch(Exception ex) { Console.WriteLine(ex.Message); }
            }

        static void KitapSil() {

                try
                {
                    Console.WriteLine("Kitap Adı: ");
                    string ad = Console.ReadLine();
                    string sorgu = "delete from Kitaplar where Kitap_adi=@kAdi";

                    var parametre = new List<SqlParameter> {
                new SqlParameter ("@kAdi",ad)
                };

                    servis.ExecuteCommand(sorgu, parametre);
                    Console.WriteLine("Kitap silindi.");
                }
            catch(SqlException ex)
            {
                Console.WriteLine("Veritabanı Hatası: "+ex.Message);
            }
                catch (Exception ex) {
                    Console.WriteLine("Hata: "+ex.Message);              
                }
            }
    }
}






