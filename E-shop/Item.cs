using System;
namespace Eshop
{
    public class Item
    {
        public int id { get; set; }
        public string nazev { get; set; }
        public int cena { get; set; }

        public override string ToString()
        {
            return id + " " + nazev + "" + cena;
        }
    }

}
