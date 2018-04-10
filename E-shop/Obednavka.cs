using System;
namespace Eshop
{
    public class Obednavka
    {
        public int id { get; set; }
        public string nazev { get; set; }
        public string datum { get; set; }

        public override string ToString()
        {
            return id + " " + nazev + "" + datum;
        }

    }
}
