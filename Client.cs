using System;
using System.Collections.Generic;
using System.Text;

namespace zaverecny_projekt
{
    internal class Client
    {
        public string name { get; set; }
        public string address { get; set; }
        public string ICO { get; set; }
        public Client() { }
        public Client(string jmeno, string adresa, string ico)
        {
            name = jmeno;
            address = adresa;
            ICO = ico;
        }

    }
}
