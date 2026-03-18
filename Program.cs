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

            bool secimYap = true;

            Console.WriteLine("Kütüphane Yönetimine Hoşgeldiniz.");

            while (secimYap)
            {
                Console.WriteLine("\nLütfen seçim yapınız:");
                Console.WriteLine("1- Tüm kitapları listele.");
                Console.WriteLine("2- Kitap ara.");
                Console.WriteLine("3- Kitap ekle.");
                Console.WriteLine("4- Kitap sil.");
                Console.WriteLine("5-Kitap stok adedi güncelle.");
                Console.WriteLine("6- Kitap iade et.");
                Console.WriteLine("7- Kitap ödünç ver.");
                Console.WriteLine("0- Çıkış");

                string secim =Console.ReadLine();

                switch (secim)
                {
                    case "1": var kitaplar = servis.TumKitaplariGetir();
                        Console.WriteLine("\n- Kitap Listesi -");
                        foreach (var k in kitaplar) Console.WriteLine("- " + k);
                        break;

                    case "2":
                        Console.Write("Aranacak kitap adı: ");
                        string aranan = Console.ReadLine();
                        Console.WriteLine(servis.KitapBul(aranan));
                        break;

                    case "3":
                        Console.Write("Kitap Adı: "); 
                        string ad = Console.ReadLine();
                        Console.Write("ISBN: "); 
                        string isbn = Console.ReadLine();
                        Console.Write("Stok: "); 
                        int stok = int.Parse(Console.ReadLine());
                        servis.KitapEkle(ad, isbn, stok);
                        break;

                    case "4": Console.Write("Silinecek kitap adı:"); 
                        string silinecek= Console.ReadLine();
                        servis.KitapSil(silinecek);
                        break;

                    case "5": 
                        Console.Write("Stoğu güncellenecek kitap adı: "); 
                        string guncellencekAdi= Console.ReadLine();
                        Console.Write("Yeni stok sayısı: "); 
                        int yeniStok= int.Parse(Console.ReadLine());
                        servis.KitapStokGuncelle(guncellencekAdi,yeniStok);
                        break;

                    case "6":
                        Console.Write("İade işlemi ID'si:");
                        int islemId=int.Parse(Console.ReadLine());
                        Console.Write("İade edilecek kitap ID'si:");
                        int kitapId = int.Parse(Console.ReadLine());
                        servis.KitapIadeAl(islemId, kitapId);
                        break;

                    case "7": 
                        Console.Write("Ödünç verilecek Kitap ID: ");
                        int kId = int.Parse(Console.ReadLine());
                        Console.Write("Alacak Üye ID: ");
                        int uId = int.Parse(Console.ReadLine());
                        servis.KitapOduncVer(kId, uId);
                        break;

                    case "0":
                        secimYap = false;
                        Console.WriteLine("Uygulamadan çıkış yapılmıştır.");
                        break;

                    default: 
                        Console.WriteLine("Geçersiz işlem tekrar deneyin");
                        break;
                }
            }
        }
    }
}






