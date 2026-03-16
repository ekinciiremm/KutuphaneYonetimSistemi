using Microsoft.Data.SqlClient;
using OOPDeneme.DbManager; 
using System;

namespace OOPDeneme
{
    class Program
    {
        static void Main(string[] args)
        {
            KutuphaneInterface servis = new DbManager.DbManager();

            try
            {
               
                Console.WriteLine("Kütüphane Veritabanı Kayıtları");
                var kitaplar = servis.TumKitaplariGetir();

                foreach (var kitap in kitaplar)
                {
                    Console.WriteLine("- " + kitap);
                }

                Console.WriteLine("Aranan Kitap Bulundu");
                string sonuc = servis.KitapBul("Aşk-ı Memnu");
                Console.WriteLine(sonuc);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Bir hata oluştu: " + ex.Message);
            }

            Console.WriteLine("\nÇıkmak için bir tuşa basın...");
            Console.ReadLine();

           
        }

        
    }
}






