namespace foci.Models
{
    public class Meccs
    {
        public int Id { get; set; }
        public int Fordulo { get; set; }    
        public int HazaiFelido { get; set; }
        public int VendegFelido { get; set; }
        public int HazaiVeg { get; set; }
        public int VendegVeg { get; set; }
        public string HazaiNev { get; set; }
        public string VendegNev { get; set; }

        public string GyoztesCsapatNeve()
        {
            if(HazaiVeg > VendegVeg)
                return HazaiNev;
            if (HazaiVeg < VendegVeg)
                return VendegNev;
            return "";
        }

        public string VesztesCsapatNeve()
        {
            if (HazaiVeg < VendegVeg)
                return HazaiNev;
            if (HazaiVeg > VendegVeg)
                return VendegNev;
            return "";
        }

        public bool UgyazanE(Meccs masik)
        {
            if (Fordulo != masik.Fordulo) return false;
            if (HazaiFelido != masik.HazaiFelido) return false;
            if (VendegFelido != masik.VendegFelido) return false;
            if (HazaiVeg != masik.HazaiVeg) return false;
            if (VendegVeg != masik.VendegVeg) return false;
            if (HazaiNev != masik.HazaiNev) return false;
            if (VendegNev != masik.VendegNev) return false;

            return true;
        }
    }
}
