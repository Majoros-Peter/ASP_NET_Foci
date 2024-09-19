namespace foci.Models
{
    public class Csapat
    {
        public string CsapatNev { get; set; }
        public int Helyezes { get; set; }
        public int JatszottMerkozesekSzama { get; set; }
        public int LottGolok { get; set; }
        public int KapottGolok { get; set; }
        public int Nyert { get; set; }
        public int Vesztett { get; set; }
        public int Dontetlen { get; set; }
        public int Pontszam { get; set; }
    }
}
