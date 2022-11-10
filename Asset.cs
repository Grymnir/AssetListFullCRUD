using System;
using System.Collections.Generic;
using System.Text;

namespace AssetlistFullCRUD
{
    public enum Category
    {
        Laptops,
        Mobiles
    }

  
    public class Asset
    {
        public int Id { get; set; }
        public Category Category { get; set; }
        public String Country { get; set; }
        public String Ccy { get; set; }
        public String Office { get; set; }
        public DateTime Purchasedate { get; set; }
        public double Price { get; set; }
        public String Modelname
        { get; set; }

        

    }
}
