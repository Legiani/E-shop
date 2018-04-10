using System;
namespace Eshop
{
    public class Person
    {
        public int id { get; set; }
        public string jmeno { get; set; }
        public string prijmeni { get; set; }
        public int rc { get; set; }
        public int pohlavi { get; set; }
        public DateTime narozeni { get; set; }
        public string heslo { get; set; }

        public override string ToString()
        {
            return id + " " + rc + "" + pohlavi + "" + narozeni;
        }

        public static implicit operator Person(string v)
        {
            throw new NotImplementedException();
        }
    }
}
