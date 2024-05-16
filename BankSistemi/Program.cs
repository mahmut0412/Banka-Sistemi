using System;
using System.Collections.Generic;
using System.Linq;

public interface IHesap
{
    string HesapAdi { get; set; }
    string HesapSoyadi { get; set; }
    decimal BaslangicBakiyesi { get; set; }
    int Id { get; set; }

    public VadeTipi VadeTipi { get; set; }
}

public abstract class Hesap : IHesap
{
    public string HesapAdi { get; set; }
    public string HesapSoyadi { get; set; }
    public decimal BaslangicBakiyesi { get; set; }
    public int Id { get; set; }

    public VadeTipi VadeTipi { get; set; }

    public abstract void HesapAc(string hesapAdi, string hesapSoyadi, decimal baslangicBakiyesi, decimal id, VadeTipi vadeTipi);

    public abstract void HesapKapat();
}

public enum VadeTipi
{
    BirAy,
    UcAy,
    AltiAy,
    OnikiAy
}


public class CariHesap : Hesap
{
    public override void HesapAc(string hesapAdi, string hesapSoyadi, decimal baslangicBakiyesi, decimal id, VadeTipi vadeTipi)
    {
        this.HesapAdi = hesapAdi;
        this.HesapSoyadi = hesapSoyadi;
        this.BaslangicBakiyesi = baslangicBakiyesi;
        this.Id = (int)id;
        this.VadeTipi = vadeTipi;

        Console.WriteLine($"Cari hesap {vadeTipi} vadesiyle açıldı.");
    }

    public override void HesapKapat()
    {
        Console.WriteLine("Cari hesap kapatıldı.");
    }
}

public class KatilimHesab : Hesap
{
    public override void HesapAc(string hesapAdi, string hesapSoyadi, decimal baslangicBakiyesi, decimal id, VadeTipi vadeTipi)
    {
        this.HesapAdi = hesapAdi;
        this.HesapSoyadi = hesapSoyadi;
        this.BaslangicBakiyesi = baslangicBakiyesi;
        this.VadeTipi = vadeTipi;

        Console.WriteLine($"Katilim hesap {vadeTipi} vadesiyle açıldı.");
    }

    public override void HesapKapat()
    {
        Console.WriteLine("Katilim hesap kapatıldı.");
    }
}

public class AltinHesab : Hesap
{
    public override void HesapAc(string hesapAdi, string hesapSoyadi, decimal baslangicBakiyesi, decimal id, VadeTipi vadeTipi)
    {
        this.HesapAdi = hesapAdi;
        this.HesapSoyadi = hesapSoyadi;
        this.BaslangicBakiyesi = baslangicBakiyesi;
        this.Id = (int)id;
        this.VadeTipi = vadeTipi;

        Console.WriteLine($"Altin hesap {vadeTipi} vadesiyle açıldı.");
    }

    public override void HesapKapat()
    {
        Console.WriteLine("Altın hesap kapatıldı.");
    }
}

public class DovizHesab : Hesap
{
    public override void HesapAc(string hesapAdi, string hesapSoyadi, decimal baslangicBakiyesi, decimal id, VadeTipi vadeTipi)
    {
        this.HesapAdi = hesapAdi;
        this.HesapSoyadi = hesapSoyadi;
        this.BaslangicBakiyesi = baslangicBakiyesi;
        this.Id = (int)id;
        this.VadeTipi = vadeTipi;

        Console.WriteLine($"Doviz hesap {vadeTipi} vadesiyle açıldı.");
    }

    public override void HesapKapat()
    {
        Console.WriteLine("Döviz hesap kapatıldı.");
    }
}


public class MusteriNoUretici
{
    private static MusteriNoUretici instance = null;
    private int countur = 0;

    public static MusteriNoUretici Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new MusteriNoUretici();
            }
            return instance;
        }
    }

    public int YeniMusteriNo()
    {
        return ++countur;
    }

    public int Countur()
    {
        return countur;
    }
}

public class Veritabani
{
    private static List<IHesap> hesaplar = new List<IHesap>();

    public static void HesapEkle(IHesap hesap)
    {
        hesaplar.Add(hesap);
    }

    public static IHesap HesapBilgisiAl(int id)
    {
        return hesaplar.FirstOrDefault(h => h.Id == id);
    }
}

public class Factory
{
    private HesapHareketleri hesapHareketleri = new HesapHareketleri();

    public static int HesapKontrolu()
    {
        MusteriNoUretici sayac = MusteriNoUretici.Instance;

        Console.WriteLine("Hesap oluşturmak için '0', hesap açmak için '1', hesap hareketlerini görmek için '2' tuşuna basınız:");
        return Convert.ToInt32(Console.ReadLine());
    }

    public void HesapAc()
    {
        MusteriNoUretici sayac = MusteriNoUretici.Instance;
        int kontrol = HesapKontrolu();

        if (kontrol == 1)
        {
            Console.WriteLine("Lütfen hesap ID'sini girin: ");
            int id = Convert.ToInt32(Console.ReadLine());
            IHesap varolanHesap = Veritabani.HesapBilgisiAl(id);
            if (varolanHesap != null)
            {
                Console.WriteLine("\nHesap Bilgileri:");
                Console.WriteLine($"-->Hesap Adı: {varolanHesap.HesapAdi}");
                Console.WriteLine($"-->Hesap Soyadı: {varolanHesap.HesapSoyadi}");
                Console.WriteLine($"-->Bakiye: {varolanHesap.BaslangicBakiyesi}");
                Console.WriteLine($"-->Vade Tipi: {varolanHesap.VadeTipi}");

                Console.WriteLine("\nYapmak istediğiniz işlemi seçin: ");
                Console.WriteLine("1 - Para çekme");
                Console.WriteLine("2 - Para yatırma");
                Console.WriteLine("3 - Çıkış");

                int islem_turu = Convert.ToInt32(Console.ReadLine());

                if (islem_turu == 1)
                {
                    Console.WriteLine("\nÇekmek istediğiniz miktarı girin: ");
                    int miktar = Convert.ToInt32(Console.ReadLine());

                    if (miktar > varolanHesap.BaslangicBakiyesi)
                    {
                        Console.WriteLine("\nBakiyeniz yetersiz!");
                    }
                    else
                    {
                        varolanHesap.BaslangicBakiyesi -= miktar;
                        hesapHareketleri.HesapHareketiEkle(new HesapHareketi(id, miktar, "ParaCekme"));
                        Console.WriteLine("Para çekme işlemi başarıyla gerçekleştirildi.");
                    }
                }

                if (islem_turu == 2)
                {
                    Console.WriteLine("\nYatırmak istediğiniz miktarı girin: ");
                    int miktar = Convert.ToInt32(Console.ReadLine());

                    varolanHesap.BaslangicBakiyesi += miktar;
                    hesapHareketleri.HesapHareketiEkle(new HesapHareketi(id, miktar, "ParaYatirma"));
                    Console.WriteLine("Para yatırma işlemi başarıyla gerçekleştirildi.");
                }
            }
            else
            {
                Console.WriteLine("\nGeçersiz ID! Hesap bulunamadı.\n");
            }
        }
        else if (kontrol == 0)
        {
            Console.WriteLine("\nHesap açma işlemi başlatılıyor...\n");

            Console.WriteLine("Hangi tür hesap açmak istiyorsunuz?");
            Console.WriteLine("1 - Cari Hesap");
            Console.WriteLine("2 - Altın Hesap");
            Console.WriteLine("3 - Döviz Hesap");
            Console.WriteLine("4 - Katılım Hesabı");

            int hesapTuru = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("\nHangi vade tipiyle hesap açmak istiyorsunuz?");
            Console.WriteLine("1 - Bir Ay");
            Console.WriteLine("2 - Üç Ay");
            Console.WriteLine("3 - Altı Ay");
            Console.WriteLine("4 - Oniki Ay");

            int vadeTuru = Convert.ToInt32(Console.ReadLine());

            VadeTipi vadeTipi;
            switch (vadeTuru)
            {
                case 1:
                    vadeTipi = VadeTipi.BirAy;
                    break;
                case 2:
                    vadeTipi = VadeTipi.UcAy;
                    break;
                case 3:
                    vadeTipi = VadeTipi.AltiAy;
                    break;
                case 4:
                    vadeTipi = VadeTipi.OnikiAy;
                    break;
                default:
                    vadeTipi = VadeTipi.BirAy;
                    break;
            }

            Console.WriteLine("\nLütfen isminizi girin: ");
            string hesapAdi = Console.ReadLine();

            Console.WriteLine("Lütfen soyisminizi girin: ");
            string hesapSoyadi = Console.ReadLine();

            Console.WriteLine("Lütfen başlangıç bakiyenizi girin: ");
            decimal baslangicBakiyesi = Convert.ToDecimal(Console.ReadLine());

            sayac.YeniMusteriNo();
            int id = sayac.Countur();

            if (hesapTuru == 1)
            {
                CariHesap yeniHesap = new CariHesap();
                yeniHesap.HesapAc(hesapAdi, hesapSoyadi, baslangicBakiyesi, id, vadeTipi);
                Veritabani.HesapEkle(yeniHesap);

                Console.WriteLine("->Yeni Cari Hesap Bilgileri:");
                Console.WriteLine($"-->Hesap Adı: {yeniHesap.HesapAdi}");
                Console.WriteLine($"-->Hesap Soyadı: {yeniHesap.HesapSoyadi}");
                Console.WriteLine($"-->Başlangıç Bakiyesi: {yeniHesap.BaslangicBakiyesi}");
                Console.WriteLine($"-->ID: {yeniHesap.Id}");
                Console.WriteLine($"-->Vade Tipi: {yeniHesap.VadeTipi}");
            }
            else if (hesapTuru == 2)
            {
                AltinHesab yeniHesap = new AltinHesab();
                yeniHesap.HesapAc(hesapAdi, hesapSoyadi, baslangicBakiyesi, id, vadeTipi);
                Veritabani.HesapEkle(yeniHesap);

                Console.WriteLine("->Yeni Altın Hesap Bilgileri:");
                Console.WriteLine($"-->Hesap Adı: {yeniHesap.HesapAdi}");
                Console.WriteLine($"-->Hesap Soyadı: {yeniHesap.HesapSoyadi}");
                Console.WriteLine($"-->Başlangıç Bakiyesi: {yeniHesap.BaslangicBakiyesi}");
                Console.WriteLine($"-->ID: {yeniHesap.Id}");
                Console.WriteLine($"-->Vade Tipi: {yeniHesap.VadeTipi}");
            }
            else if (hesapTuru == 3)
            {
                DovizHesab yeniHesap = new DovizHesab();
                yeniHesap.HesapAc(hesapAdi, hesapSoyadi, baslangicBakiyesi, id, vadeTipi);
                Veritabani.HesapEkle(yeniHesap);

                Console.WriteLine("->Yeni Döviz Hesap Bilgileri:");
                Console.WriteLine($"-->Hesap Adı: {yeniHesap.HesapAdi}");
                Console.WriteLine($"-->Hesap Soyadı: {yeniHesap.HesapSoyadi}");
                Console.WriteLine($"-->Başlangıç Bakiyesi: {yeniHesap.BaslangicBakiyesi}");
                Console.WriteLine($"-->ID: {yeniHesap.Id}");
                Console.WriteLine($"-->Vade Tipi: {yeniHesap.VadeTipi}");
            }
            else if (hesapTuru == 4)
            {
                KatilimHesab yeniHesap = new KatilimHesab();
                yeniHesap.HesapAc(hesapAdi, hesapSoyadi, baslangicBakiyesi, id, vadeTipi);
                Veritabani.HesapEkle(yeniHesap);

                Console.WriteLine("->Yeni Katılım Hesabı Bilgileri:");
                Console.WriteLine($"-->Hesap Adı: {yeniHesap.HesapAdi}");
                Console.WriteLine($"-->Hesap Soyadı: {yeniHesap.HesapSoyadi}");
                Console.WriteLine($"-->Başlangıç Bakiyesi: {yeniHesap.BaslangicBakiyesi}");
                Console.WriteLine($"-->ID: {yeniHesap.Id}");
                Console.WriteLine($"-->Vade Tipi: {yeniHesap.VadeTipi}");
            }
        }
        else if (kontrol == 2)
        {
            hesapHareketleri.TumHesapHareketleriListele();
        }
    }
}

public class HesapHareketi
{
    public int HesapId { get; set; }
    public decimal Miktar { get; set; }
    public DateTime Tarih { get; set; }

    public string Turu;

    public HesapHareketi(int hesapId, decimal miktar, string turu)
    {
        HesapId = hesapId;
        Miktar = miktar;
        Tarih = DateTime.Now;
        Turu = turu;
    }

}


public class HesapHareketleri
{
    private List<HesapHareketi> hesapHareketListesi = new List<HesapHareketi>();

    public void HesapHareketiEkle(HesapHareketi hareket)
    {
        hesapHareketListesi.Add(hareket);
    }

    public List<HesapHareketi> TumHesapHareketleriGetir()
    {
        return hesapHareketListesi;
    }

    public void TumHesapHareketleriListele()
    {
        Console.WriteLine("\n->Tüm Hesap Hareketleri:");

        foreach (var hareket in hesapHareketListesi)
        {
            Console.WriteLine($"-->Hesap ID: {hareket.HesapId}");
            Console.WriteLine($"-->Miktar: {hareket.Miktar}");
            Console.WriteLine($"-->Tarih: {hareket.Tarih}");
            Console.WriteLine($"-->Hareket Türü: {hareket.Turu}");
            Console.WriteLine("--------------------------------------");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Factory factory = new Factory();

        int cikis = 0;
        while (cikis == 0)
        {
            factory.HesapAc();

            Console.WriteLine("\nÇıkış yapmak istiyor musunuz? (Evet için 1, Hayır için 0)");
            cikis = Convert.ToInt32(Console.ReadLine());
        }
    }
}