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
        public Project(string order_type, Client client, decimal hourly_rate, double hours_worked)
        {
            this.order_type = order_type;
            this.client = client;
            this.hourly_rate = hourly_rate;
            this.hours_worked = hours_worked;
        }
        public decimal GetTotalPrice()
        {
            return hourly_rate * (decimal)hours_worked;
        }
    }
}
