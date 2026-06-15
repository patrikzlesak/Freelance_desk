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
        public Invoice(Client client, Project project, decimal amount, bool is_paid) {

            this.client = client;
            this.project = project;
            this.amount = amount;
            this.is_paid = is_paid;
        }
    }
}
