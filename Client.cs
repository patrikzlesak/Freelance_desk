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
        public Client(string name, string address, string ICO)
        {
            this.name = name;
            this.address = address;
            this.ICO = ICO;
        }

    }
}
