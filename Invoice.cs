using System;
using System.Collections.Generic;
using System.Text;

namespace zaverecny_projekt
{
    internal class Invoice
    {
        public Client client { get; set; }
        public Project project { get; set; }
        public decimal amount { get; set; }
        public bool is_paid { get; set; }
        public Invoice() { }
        public Invoice(Client klient, Project projekt, decimal castka, bool zaplaceno) {

            client = klient;
            project = projekt;
            amount = castka;
            is_paid = zaplaceno;
        }
    }
}
