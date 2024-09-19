﻿namespace foci.Models
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
    }
}
