using System;
namespace kutuphaneYonetimi
{
    public class Kitaplar
    {
        //gizlendi. verilere doğrudan erişim yok.
        private string _kitapAdi;
        private int _StokAdedi;


        public Kitaplar(string kitapAdi, int stokAdedi)
        {
            KitapAdi = kitapAdi;
            StokAdedi = stokAdedi;
        }
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
            Kitaplar k1 = new Kitaplar("Mai ve Siyah", 34);
            k1.Yazdir();

            Kitaplar k2 = new Kitaplar("Aşk-ı Memnu", 12);
            k2.Yazdir();

        }
    }
}
