using System;
using System.Collections.Generic;
using System.Text;

namespace zaverecny_projekt
{
    internal class Project
    {
        public string order_type { get; set; }
        public Client client { get; set; }
        public decimal hourly_rate { get; set; }
        public double hours_worked { get; set; }
        public Project(){ }
        public Project(string typ, Client klient, decimal sazba, double odpracovane_hodiny)
        {
            order_type = typ;
            client = klient;
            hourly_rate = sazba;
            hours_worked = odpracovane_hodiny;
        }
        public decimal GetTotalPrice()
        {
            return hourly_rate * (decimal)hours_worked;
        }
    }
}
