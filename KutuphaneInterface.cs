using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPDeneme.DbManager
{
    public interface KutuphaneInterface
    {
        void KitapEkle(string ad, string isbn, int stok);
        List<string> TumKitaplariGetir();

        string KitapBul(string kitap_adi);

        void KitapSil(string kitap_adi);

        void KitapStokGuncelle(string kitap_adi, int yeniStok);

        void KitapOduncVer(int Kitap_id, int Uye_id);

        void KitapIadeAl(int Islem_id, int Kitap_id);
    }
}
