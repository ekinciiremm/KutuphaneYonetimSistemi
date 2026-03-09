using System;
namespace kutuphaneYonetimi
{
    public class Kitaplar
    {
        //gizlendi. verilere doğrudan erişim yok.
        private string _kitapAdi;
        private int _StokAdedi;


        public string KitapAdi
        {
            get { return _kitapAdi; }
            set { _kitapAdi = value; }
        }

        public int StokAdedi
        {
            get { return _StokAdedi; }
            set
            {
                if (value > 0)
                {
                    _StokAdedi = value;
                }
                else
                {
                    Console.WriteLine("Kitap adedi sıfırdan küçük olamaz");
                }
            }
        }

        public void Yazdir()
        {
            Console.WriteLine($"Kitap Adı:{KitapAdi}, Stok Adedi:{StokAdedi}");
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Kitaplar k1 = new Kitaplar();
            k1.KitapAdi = "Mai ve Siyah";
            k1.StokAdedi = 34;
            k1.Yazdir();

            Console.ReadLine();
        }
    }
}