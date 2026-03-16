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

       
    }
}
