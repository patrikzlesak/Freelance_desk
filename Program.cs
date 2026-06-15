using System.Text.Json;

namespace zaverecny_projekt
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Deklarace listů objektů

            List<Client> clients = new List<Client>();
            List<Project> projects = new List<Project>();
            List<Invoice> invoices = new List<Invoice>();

            //Načtení dat

            LoadData(clients, projects, invoices);

            //Hlavní běh programu

            Console.WriteLine("███████╗██████╗ ███████╗███████╗██╗      █████╗ ███╗   ██╗ ██████╗███████╗    ██████╗ ███████╗███████╗██╗  ██╗\r\n██╔════╝██╔══██╗██╔════╝██╔════╝██║     ██╔══██╗████╗  ██║██╔════╝██╔════╝    ██╔══██╗██╔════╝██╔════╝██║ ██╔╝\r\n█████╗  ██████╔╝█████╗  █████╗  ██║     ███████║██╔██╗ ██║██║     █████╗      ██║  ██║█████╗  ███████╗█████╔╝ \r\n██╔══╝  ██╔══██╗██╔══╝  ██╔══╝  ██║     ██╔══██║██║╚██╗██║██║     ██╔══╝      ██║  ██║██╔══╝  ╚════██║██╔═██╗ \r\n██║     ██║  ██║███████╗███████╗███████╗██║  ██║██║ ╚████║╚██████╗███████╗    ██████╔╝███████╗███████║██║  ██╗\r\n╚═╝     ╚═╝  ╚═╝╚══════╝╚══════╝╚══════╝╚═╝  ╚═╝╚═╝  ╚═══╝ ╚═════╝╚══════╝    ╚═════╝ ╚══════╝╚══════╝╚═╝  ╚═╝");
            int choice1 = Menu("---", false, "START", "Konec");
            if (choice1 == 1)
            {
                return;
            }
            else
            {
                while (true)
                {
                    int mainchoice = Menu("Zdraví Vás FREELANCE DESK, Pane. Co je dnes na programu?", true, "Správa klientů", "Správa zakázek", "Správa faktur", "Konec");
                    switch (mainchoice)
                    {
                        case 0: // Klienti
                            ManageClients(clients, projects, invoices);
                            break;
                        case 1: // Zakázky
                            ManageProjects(clients, projects);
                            break;
                        case 2: // Faktury
                            ManageInvoices(invoices, projects, clients);
                            break;
                        case 3: // Konec
                            return;
                    }
                }
            }
        }
        public static int Menu(string title, bool clear, params string[] choices)
        {
            if (clear == true)
            {
                Console.Clear();
            }

            int chosen = 0;
            int line = Console.CursorTop;

            while (true)
            {
                Console.CursorVisible = false;
                Console.SetCursorPosition(0, line);

                Console.WriteLine(title);
                Console.WriteLine();

                for (int i = 0; i < choices.Length; i++)
                {
                    if (chosen == i)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine("  > " + choices[i]);
                        Console.ResetColor();
                    }
                    else
                    {
                        if (choices[i].Contains("NEZAPLACENO"))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        else if (choices[i].Contains("ZAPLACENO"))
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        Console.WriteLine("    " + choices[i]);
                        Console.ResetColor();
                    }
                }

                ConsoleKeyInfo klavesa = Console.ReadKey(true);

                if (klavesa.Key == ConsoleKey.UpArrow)
                {
                    if (chosen > 0)
                        chosen--;
                    else
                        chosen = choices.Length - 1;
                }
                else if (klavesa.Key == ConsoleKey.DownArrow)
                {
                    if (chosen < choices.Length - 1)
                        chosen++;
                    else
                        chosen = 0;
                }
                else if (klavesa.Key == ConsoleKey.Enter)
                {
                    return chosen;
                }
            }
        } //hotovo
        public static void ManageClients(List<Client> clients, List<Project> projects, List<Invoice> invoices)
        {
            while (true)
            {
                int choice = Menu("SPRÁVA KLIENTŮ", true, "Vypsat všechny klienty", "Přidat nového klienta", "Odstranit klienta", "Zpět do hlavního menu");
                switch (choice)
                {
                    case 0:
                        Console.Clear();
                        Console.CursorVisible = false;
                        if (clients.Count == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("V databázi nemáte žádné klienty.");
                            Console.WriteLine();
                            Console.ResetColor();
                            Console.WriteLine("-- Stiskněte libovolnou klávesu pro návrat do menu");
                            Console.ReadKey();
                            break;
                        }
                        else
                        {
                            int longest_title = 0;
                            for (int i = 0; i < clients.Count; i++)
                            {
                                if (clients[i].name.Length > longest_title)
                                {
                                    longest_title = clients[i].name.Length;
                                }
                            }
                            int longest_address = 0;
                            for (int i = 0; i < clients.Count; i++)
                            {
                                if (clients[i].address.Length > longest_address)
                                {
                                    longest_address = clients[i].address.Length;
                                }
                            }
                            for (int i = 0; i < clients.Count; i++)
                            {
                                Console.WriteLine($"Jméno / Název firmy: {clients[i].name.PadRight(longest_title)} | Adresa: {clients[i].address.PadRight(longest_address)} | IČO: {clients[i].ICO}");
                            }
                            Console.WriteLine();
                            Console.WriteLine("-- Stiskněte libovolnou klávesu pro návrat do menu");
                            Console.ReadKey();
                            break;
                        }

                    case 1:
                        Console.Clear();
                        Console.CursorVisible = true;
                        Console.Write("Jméno / Název firmy: ");
                        string name = Console.ReadLine();
                        while (clients.Any(c => c.name.ToLower() == name.ToLower()))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Klient s tímto názvem / jménem již existuje!");
                            Console.ResetColor();
                            Console.Write("Zkuste to znovu: ");
                            name = Console.ReadLine();
                        }
                        Console.Write("Adresa: ");
                        string adress = Console.ReadLine();
                        string ico = "";
                        while (true)
                        {
                            Console.Write("IČO: ");
                            ico = Input_verification_ico(8, 8);

                            if (clients.Any(c => c.ICO == ico))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Klient s tímto IČO již existuje!");
                                Console.ResetColor();
                                Console.WriteLine();
                            }
                            else
                            {
                                break;
                            }
                        }
                        clients.Add(new Client(name, adress, ico));
                        SaveClients(clients);
                        Console.CursorVisible = false;
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Klient úspěšně vytvořen!");
                        Console.WriteLine();
                        Console.ResetColor();
                        Console.WriteLine("-- Stiskněte libovolnou klávesu pro návrat do menu");
                        Console.ReadKey();
                        break;

                    case 2:
                        if (clients.Count == 0)
                        {
                            Console.Clear();
                            Console.CursorVisible = false;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("V databázi nejsou žádní klienti, které můžete smazat.");
                            Console.ResetColor();
                            Console.WriteLine();
                            Console.WriteLine("-- Stiskněte libovolnou klávesu pro návrat do menu");
                            Console.ReadKey();
                            break;
                        }
                        string[] clients_array = new string[clients.Count];
                        for (int i = 0; i < clients.Count; i++)
                        {
                            clients_array[i] = clients[i].name;
                        }
                        int chosen_client = Menu("Vyberte, jakého klienta chcete odstranit", true, clients_array);
                        bool maNezaplacenouFakturu = false;
                        for (int i = 0; i < invoices.Count; i++)
                        {
                            if (invoices[i].client.name == clients[chosen_client].name && invoices[i].is_paid == false)
                            {
                                maNezaplacenouFakturu = true;
                                break;
                            }
                        }
                        int yes_no;
                        if (maNezaplacenouFakturu == true)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            yes_no = Menu($"POZOR: Klient {clients[chosen_client].name} má nezaplacenou fakturu! Opravdu si přejete klienta i s jeho fakturami smazat?", true, "Ano, smazat i přes nezaplacenou fakturu", "Ne, chci zpět");
                            Console.ResetColor();
                        }
                        else
                        {
                            yes_no = Menu($"Opravdu chcete odstranit klienta {clients[chosen_client].name}?", true, "Ano, přeji si odstranit klienta", "Ne, chci zpět");
                        }
                        switch (yes_no)
                        {   
                            case 0:
                                for (int i = projects.Count - 1; i >= 0; i--)
                                {
                                    if (projects[i].client.name == clients[chosen_client].name)
                                    {
                                        projects.Remove(projects[i]);
                                    }
                                }
                                for (int i = invoices.Count - 1; i >= 0; i--)
                                {
                                    if (invoices[i].client.name == clients[chosen_client].name)
                                    {
                                        invoices.Remove(invoices[i]);
                                    }
                                }
                                clients.Remove(clients[chosen_client]);
                                SaveClients(clients);
                                SaveProjects(projects);
                                SaveInvoices(invoices);
                                Console.Clear();
                                Console.CursorVisible = false;
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Klient úspěšně odstraněn. S ním byly odstraněny i jeho zakázky a faktury, pokud existovaly.");
                                Console.ResetColor();
                                Console.WriteLine();
                                Console.WriteLine("-- Stiskněte libovolnou klávesu pro návrat do menu");
                                Console.ReadKey();
                                break;
                            case 1:
                                break;
                        }
                        break;
                    case 3:
                        return;
                }
            }
        }//hotovo
        public static void ManageProjects(List<Client> clients, List<Project> projects)
        {
            while (true)
            {
                int choice = Menu("SPRÁVA ZAKÁZEK", true, "Vypsat všechny zakázky", "Vytvořit novou zakázku", "Zapsat odpracované hodiny k zakázce", "Odstranit zakázku", "Zpět do hlavního menu");
                switch (choice)
                {
                    case 0:
                        if (projects.Count == 0)
                        {
                            Console.Clear();
                            Console.CursorVisible = false;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("V databázi nejsou žádné zakázky k vypsání.");
                            Console.ResetColor();
                            Console.WriteLine();
                            Console.WriteLine("-- Stiskněte libovolnou klávesu pro návrat do menu");
                            Console.ReadKey();
                            break;
                        }
                        else
                        {
                            Console.Clear();
                            Console.CursorVisible = false;
                            int longest_client = 0;
                            for (int i = 0; i < projects.Count; i++)
                            {
                                if (projects[i].client.name.Length > longest_client)
                                {
                                    longest_client = projects[i].client.name.Length;
                                }
                            }
                            int longest_type = 0;
                            for (int i = 0; i < projects.Count; i++)
                            {
                                if (projects[i].order_type.Length > longest_type)
                                {
                                    longest_type = projects[i].order_type.Length;
                                }
                            }
                            int longest_hourly_rate = 0;
                            for (int i = 0; i < projects.Count; i++)
                            {
                                if (projects[i].hourly_rate.ToString().Length > longest_hourly_rate)
                                    longest_hourly_rate = projects[i].hourly_rate.ToString().Length;
                            }
                            for (int i = 0; i < projects.Count; i++)
                            {
                                Console.WriteLine($"Klient: {projects[i].client.name.PadRight(longest_client)} | Typ zakázky: {projects[i].order_type.PadRight(longest_type)} | Hodinová sazba: {projects[i].hourly_rate.ToString().PadRight(longest_hourly_rate)} | Odpracované hodiny: {projects[i].hours_worked}");
                            }
                            Console.WriteLine();
                            Console.WriteLine("-- Stiskněte libovolnou klávesu pro návrat do menu");
                            Console.ReadKey();
                            break;
                        }
                    case 1:
                        if (clients.Count == 0)
                        {
                            Console.Clear();
                            Console.CursorVisible = false;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Nemáte žádné klienty, ke kterým přiřazovat zakázky. Nejdříve vytvořte klienta");
                            Console.ResetColor();
                            Console.WriteLine("-- Stiskněte libovolnou klávesu pro návrat do menu");
                            Console.ReadKey();
                            break;
                        }
                        else
                        {
                            string[] clients_array = new string[clients.Count];
                            for (int i = 0; i < clients.Count; i++)
                            {
                                clients_array[i] = clients[i].name;
                            }
                            int chosen_client = Menu("Vyberte klienta, ke kterému zakázku přiřadit:", true, clients_array);
                            Console.Clear();
                            Console.CursorVisible = true;
                            Console.WriteLine($"Zakázka pro {clients[chosen_client].name}");
                            Console.Write("O jaký typ práce se jedná?: ");
                            string project_type = Console.ReadLine();
                            Console.Write("Na jaké výši máte nastavenou hodinovou sazbu?: ");
                            decimal hourly_rate = Input_verification_decimal(0);
                            projects.Add(new Project(project_type, clients[chosen_client], hourly_rate, 0));
                            SaveProjects(projects);
                            Console.CursorVisible = false;
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Zakázka úspěšně vytvořena! Odpracované hodiny byly automaticky nastaveny na 0. Můžete je upravit v menu.");
                            Console.WriteLine();
                            Console.ResetColor();
                            Console.WriteLine("-- Stiskněte libovolnou klávesu pro návrat do menu");
                            Console.ReadKey();
                            break;
                        }
                    case 2:
                        if (projects.Count == 0)
                        {
                            Console.CursorVisible = false;
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("V databázi nejsou žádné zakázky.");
                            Console.ResetColor();
                            Console.WriteLine();
                            Console.WriteLine("-- Stiskněte libovolnou klávesu pro návrat do menu");
                            Console.ReadKey();
                            break;
                        }
                        else
                        {
                            Console.CursorVisible = false;
                            string[] project_choices = new string[projects.Count];
                            int longest_client = 0;
                            for (int i = 0; i < projects.Count; i++)
                            {
                                if (projects[i].client.name.Length > longest_client)
                                {
                                    longest_client = projects[i].client.name.Length;
                                }
                            }
                            int longest_type = 0;
                            for (int i = 0; i < projects.Count; i++)
                            {
                                if (projects[i].order_type.Length > longest_type)
                                {
                                    longest_type = projects[i].order_type.Length;
                                }
                            }
                            for (int i = 0; i < projects.Count; i++)
                            {
                                project_choices[i] = $"Klient: {projects[i].client.name.PadRight(longest_client)} | Typ zakázky: {projects[i].order_type.PadRight(longest_type)} | Odpracované hodiny: {projects[i].hours_worked}";
                            }
                            int chosen_projct = Menu("ZAPISOVÁNÍ HODIN - Vyberte zakázku:", true, project_choices);
                            Console.Clear();
                            Console.WriteLine($"Klient: {projects[chosen_projct].client.name.PadRight(longest_client)} | Typ zakázky: {projects[chosen_projct].order_type.PadRight(longest_type)} | Odpracované hodiny: {projects[chosen_projct].hours_worked}");
                            Console.WriteLine();
                            Console.CursorVisible = true;
                            Console.Write("Zadejte počet nově odpracovaných hodin (ne celkový počet): ");
                            double hours = Input_verification_double(0);
                            Console.CursorVisible = false;
                            projects[chosen_projct].hours_worked += hours;
                            SaveProjects(projects);
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Hodiny byly úspěšně přičteny! Celkem odpracováno: {projects[chosen_projct].hours_worked} hod.");
                            Console.ResetColor();
                            Console.WriteLine("-- Stiskněte libovolnou klávesu pro návrat do menu");
                            Console.ReadKey();
                            break;
                        }
                    case 3:
                        if (projects.Count == 0)
                        {
                            Console.Clear();
                            Console.CursorVisible = false;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("V databázi nejsou žádné zakázky, které by bylo možné smazat.");
                            Console.ResetColor();
                            Console.WriteLine();
                            Console.WriteLine("-- Stiskněte libovolnou klávesu pro návrat do menu");
                            Console.ReadKey();
                            break;
                        }
                        else
                        {
                            Console.CursorVisible = false;
                            string[] project_choices1 = new string[projects.Count];
                            int longest_client1 = 0;
                            for (int i = 0; i < projects.Count; i++)
                            {
                                if (projects[i].client.name.Length > longest_client1)
                                {
                                    longest_client1 = projects[i].client.name.Length;
                                }
                            }
                            int longest_type1 = 0;
                            for (int i = 0; i < projects.Count; i++)
                            {
                                if (projects[i].order_type.Length > longest_type1)
                                {
                                    longest_type1 = projects[i].order_type.Length;
                                }
                            }
                            for (int i = 0; i < projects.Count; i++)
                            {
                                project_choices1[i] = $"Klient: {projects[i].client.name.PadRight(longest_client1)} | Typ zakázky: {projects[i].order_type.PadRight(longest_type1)} | Odpracované hodiny: {projects[i].hours_worked}";
                            }
                            int chosen_projct1 = Menu("ODEBRÁNÍ ZAKÁZKY - Vyberte zakázku, kterou chcete odstranit:", true, project_choices1);
                            int yes_no1 = Menu($"Opravdu chcete odstranit zakázku {projects[chosen_projct1].order_type} pro {projects[chosen_projct1].client.name}", true, "Ano, přeji si odstranit zakázku", "Ne, chci zpět");
                            switch (yes_no1)
                            {
                                case 0:
                                    projects.Remove(projects[chosen_projct1]);
                                    SaveProjects(projects);
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("Zakázka úspěšně odstraněna.");
                                    Console.ResetColor();
                                    Console.WriteLine();
                                    Console.WriteLine("-- Stiskněte libovolnou klávesu pro návrat do menu");
                                    Console.ReadKey();
                                    break;
                                case 1:
                                    break;
                            }
                            break;
                        }
                    case 4:
                        return;
                }
            }
        } //hotovo
        public static void ManageInvoices(List<Invoice> invoices, List<Project> projects, List<Client> clients)
        {
            while (true)
            {
                int choice = Menu("SPRÁVA FAKTUR", true, "Vypsat všechny faktury", "Vystavit novou fakturu", "Změnit stav faktury", "Zobrazit nezaplacené faktury", "Odstranit fakturu", "Zobrazit souhrnné statistiky", "Zpět do hlavního menu");
                switch (choice)
                {
                    case 0:
                        if (invoices.Count == 0)
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("V databázi nejsou žádné faktury.");
                            Console.ResetColor();
                            Console.WriteLine();
                            Console.WriteLine("-- Stiskněte libovolnou klávesu pro návrat do menu");
                            Console.ReadKey();
                            break;
                        }
                        else
                        {
                            Console.Clear();
                            string[] invoice_choices = new string[invoices.Count];
                            int longest_client = 0;
                            for (int i = 0; i < invoices.Count; i++)
                            {
                                if (invoices[i].client.name.Length > longest_client)
                                {
                                    longest_client = invoices[i].client.name.Length;
                                }
                            }
                            int longest_type = 0;
                            for (int i = 0; i < invoices.Count; i++)
                            {
                                if (invoices[i].project.order_type.Length > longest_type)
                                {
                                    longest_type = invoices[i].project.order_type.Length;
                                }
                            }
                            int longest_amount = 0;
                            for (int i = 0; i < invoices.Count; i++)
                            {
                                int amountLength = invoices[i].amount.ToString().Length;
                                if (amountLength > longest_amount)
                                {
                                    longest_amount = amountLength;
                                }
                            }
                            for (int i = 0; i < invoices.Count; i++)
                            {
                                string formattedAmount = invoices[i].amount.ToString().PadLeft(longest_amount);

                                Console.Write($"Klient: {invoices[i].client.name.PadRight(longest_client)} | Typ zakázky: {invoices[i].project.order_type.PadRight(longest_type)} | Částka: {formattedAmount} Kč | Stav: ");
                                if (invoices[i].is_paid == true)
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.Write("ZAPLACENO");
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write("NEZAPLACENO");
                                }
                                Console.ResetColor();
                                Console.WriteLine();
                            }
                            Console.WriteLine();
                            Console.WriteLine("-- Stiskněte libovolnou klávesu pro návrat do menu");
                            Console.ReadKey();
                            break;
                        }
                    case 1: //vytvořit fakturu - hotovo!
                        if (clients.Count == 0 || projects.Count == 0)
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Pro vystavení faktury musíte mít v databázi klienty i zakázky.");
                            Console.ResetColor();
                            Console.WriteLine();
                            Console.WriteLine("-- Stiskněte libovolnou klávesu pro návrat do menu");
                            Console.ReadKey();
                            break;
                        }
                        string[] clients_array = new string[clients.Count];
                        for (int i = 0; i < clients.Count; i++)
                        {
                            clients_array[i] = clients[i].name;
                        }
                        int chosen_client = Menu("FAKTURACE - Vyberte klienta, kterému chcete vystavit fakturu:", true, clients_array);
                        List<Project> client_projects = new List<Project>();
                        for (int i = 0; i < projects.Count; i++)
                        {
                            if (projects[i].client.name == clients[chosen_client].name)
                            {
                                client_projects.Add(projects[i]);
                            }
                        }
                        if (client_projects.Count == 0)
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Pro klienta {clients[chosen_client].name} nebyly nalezeny žádné zakázky.");
                            Console.ResetColor();
                            Console.WriteLine();
                            Console.WriteLine("-- Stiskněte libovolnou klávesu pro návrat do menu");
                            Console.ReadKey();
                            break;
                        }
                        int longest_title = 0;
                        for (int i = 0; i < client_projects.Count; i++)
                        {
                            if (client_projects[i].order_type.Length > longest_title)
                            {
                                longest_title = client_projects[i].order_type.Length;
                            }
                        }

                        string[] client_projects_array = new string[client_projects.Count];
                        for (int i = 0; i < client_projects.Count; i++)
                        {
                            client_projects_array[i] = $"Zakázka: {client_projects[i].order_type.PadRight(longest_title)} | Aktuální hodnota zakázky: {client_projects[i].GetTotalPrice()} Kč";
                        }
                        int chosen_project_index = Menu("VÝBĚR ZAKÁZKY K FAKTURACI - vyberte zakázku, kterou chcete vyfakturovat", true, client_projects_array);
                        Project selectedProject = client_projects[chosen_project_index];
                        bool uzFakturovano = false;
                        for (int i = 0; i < invoices.Count; i++)
                        {
                            if (invoices[i].client.name == clients[chosen_client].name &&
                                invoices[i].project.order_type == selectedProject.order_type &&
                                invoices[i].project.hourly_rate == selectedProject.hourly_rate &&
                                invoices[i].project.hours_worked == selectedProject.hours_worked)
                            {
                                uzFakturovano = true;
                                break;
                            }
                        }
                        if (uzFakturovano)
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Chyba: Faktura pro tuto konkrétní zakázku \"{selectedProject.order_type}\" již byla vygenerována!");
                            Console.ResetColor();
                            Console.WriteLine();
                            Console.WriteLine("-- Stiskněte libovolnou klávesu pro návrat do menu");
                            Console.ReadKey();
                            break;
                        }
                        else
                        {
                            Invoice invoice = new Invoice(clients[chosen_client], selectedProject, selectedProject.GetTotalPrice(), false);
                            invoices.Add(invoice);
                            SaveInvoices(invoices);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Faktura úspěšně vygenerována!");
                            Console.ResetColor();
                            Console.WriteLine();
                            int yes_no = Menu("Přejete si fakturu zobrazit?", false, "Ano, zobrazit", "Ne, zpět do menu");
                            switch (yes_no)
                            {
                                case 0:
                                    Console.Clear();
                                    Console.WriteLine($"Klient:       {invoice.client.name}");
                                    Console.WriteLine($"Zakázka:      {invoice.project.order_type}");
                                    Console.WriteLine($"Částka:       {invoice.amount} Kč");
                                    Console.WriteLine();
                                    Console.WriteLine("-- Stiskněte libovolnou klávesu pro návrat do menu");
                                    Console.ReadKey();
                                    break;
                                case 1:
                                    break;
                            }
                            break;
                        }
                    case 2:
                        if (invoices.Count == 0)
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("V databázi nejsou žádné faktury, u kterých by šlo změnit stav.");
                            Console.ResetColor();
                            Console.WriteLine();
                            Console.WriteLine("-- Stiskněte libovolnou klávesu pro návrat do menu");
                            Console.ReadKey();
                            break;
                        }
                        string[] clients_array3 = new string[clients.Count];
                        for (int i = 0; i < clients.Count; i++)
                        {
                            clients_array3[i] = clients[i].name;
                        }
                        int chosen_client3 = Menu("ZMĚNA STAVU FAKTURY - Vyberte klienta pro zobrazení jeho faktur:", true, clients_array3);
                        Client selectedClient = clients[chosen_client3];
                        List<Invoice> filteredInvoices = new List<Invoice>();
                        for (int i = 0; i < invoices.Count; i++)
                        {
                            if (invoices[i].client.name == selectedClient.name)
                            {
                                filteredInvoices.Add(invoices[i]);
                            }
                        }
                        if (filteredInvoices.Count == 0)
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Klient {selectedClient.name} nemá žádné faktury.");
                            Console.ResetColor();
                            Console.WriteLine();
                            Console.WriteLine("-- Stiskněte libovolnou klávesu pro návrat do menu");
                            Console.ReadKey();
                            break;
                        }
                        int longest_type3 = 0;
                        for (int i = 0; i < filteredInvoices.Count; i++)
                        {
                            if (filteredInvoices[i].project.order_type.Length > longest_type3)
                            {
                                longest_type3 = filteredInvoices[i].project.order_type.Length;
                            }
                        }
                        string[] invoices_to_choose = new string[filteredInvoices.Count];
                        for (int i = 0; i < filteredInvoices.Count; i++)
                        {
                            invoices_to_choose[i] = $"Typ zakázky: {filteredInvoices[i].project.order_type.PadRight(longest_type3)} | Aktuální stav: ";
                            if (filteredInvoices[i].is_paid == true)
                            {
                                invoices_to_choose[i] += $"ZAPLACENO";
                            }
                            else
                            {
                                invoices_to_choose[i] += $"NEZAPLACENO";
                            }
                        }
                        int chosen_invoice = Menu("FAKTURACE - vyberte, u které faktury chcete změnit stav", true, invoices_to_choose);
                        if (filteredInvoices[chosen_invoice].is_paid == true)
                        {
                            int turn_unpaid = Menu("Faktura je momentálně 'ZAPLACENÁ', přejete si změnit její stav na 'NEZAPLACENÁ'?", true, "Ano, přeji si změnit stav na 'NEZAPLACENÁ'", "Ne, zpět do menu");
                            switch (turn_unpaid)
                            {
                                case 0:
                                    filteredInvoices[chosen_invoice].is_paid = false;
                                    SaveInvoices(invoices);
                                    Console.Write("Stav faktury úspěšně změněn na ");
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write("'NEZAPLACENÁ'");
                                    Console.ResetColor();
                                    Console.WriteLine();
                                    Console.WriteLine("-- Stiskněte libovolnou klávesu pro návrat do menu");
                                    Console.ReadKey();
                                    break;
                                case 1:
                                    break;
                            }
                        }
                        else if (filteredInvoices[chosen_invoice].is_paid == false)
                        {
                            int turn_unpaid = Menu("Faktura je momentálně 'NEZAPLACENÁ', přejete si změnit její stav na 'ZAPLACENÁ'?", true, "Ano, přeji si změnit stav na 'ZAPLACENÁ'", "Ne, zpět do menu");
                            switch (turn_unpaid)
                            {
                                case 0:
                                    filteredInvoices[chosen_invoice].is_paid = true;
                                    SaveInvoices(invoices);
                                    Console.Write("Stav faktury úspěšně změněn na ");
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.Write("'ZAPLACENÁ'");
                                    Console.ResetColor();
                                    Console.WriteLine();
                                    Console.WriteLine("-- Stiskněte libovolnou klávesu pro návrat do menu");
                                    Console.ReadKey();
                                    break;
                                case 1:
                                    break;
                            }
                        }
                        break;
                    case 3:
                        Console.Clear();

                        List<Invoice> Unpaid_Invoices = new List<Invoice>();
                        for (int i = 0; i < invoices.Count; i++)
                        {
                            if (invoices[i].is_paid == false)
                            {
                                Unpaid_Invoices.Add(invoices[i]);
                            }
                        }
                        if (invoices.Count == 0 || Unpaid_Invoices.Count == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("V databázi nejsou žádné nezaplacené faktury.");
                            Console.ResetColor();
                            Console.WriteLine();
                            Console.WriteLine("-- Stiskněte libovolnou klávesu pro návrat do menu");
                            Console.ReadKey();
                            break;

                        }
                        int longest_client4 = 0;
                        for (int i = 0; i < Unpaid_Invoices.Count; i++)
                        {
                            if (Unpaid_Invoices[i].client.name.Length > longest_client4)
                            {
                                longest_client4 = Unpaid_Invoices[i].client.name.Length;
                            }
                        }
                        int longest_amount5 = 0;
                        for (int i = 0; i < Unpaid_Invoices.Count; i++)
                        {
                            int amountLength = Unpaid_Invoices[i].amount.ToString().Length;
                            if (amountLength > longest_amount5)
                            {
                                longest_amount5 = amountLength;
                            }
                        }
                        for (int i = 0; i < Unpaid_Invoices.Count; i++)
                        {
                            string formattedAmount1 = Unpaid_Invoices[i].amount.ToString().PadLeft(longest_amount5);
                            Console.Write($"Klient: {Unpaid_Invoices[i].client.name.PadRight(longest_client4)} | Aktuální hodnota: {formattedAmount1} Kč | Stav: ");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("NEZAPLACENO");
                            Console.ResetColor();
                            Console.WriteLine();
                        }
                        Console.WriteLine();
                        Console.WriteLine("-- Stiskněte libovolnou klávesu pro návrat do menu");
                        Console.ReadKey();
                        break;
                    case 4:
                        if (invoices.Count == 0)
                        {
                            Console.Clear();
                            Console.CursorVisible = false;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("V databázi nejsou žádné faktury, které by bylo možné smazat.");
                            Console.ResetColor();
                            Console.WriteLine();
                            Console.WriteLine("-- Stiskněte libovolnou klávesu pro návrat do menu");
                            Console.ReadKey();
                            break;
                        }
                        else
                        {
                            Console.CursorVisible = false;
                            string[] invoice_choices1 = new string[invoices.Count];
                            int longest_client6 = 0;
                            for (int i = 0; i < invoices.Count; i++)
                            {
                                if (invoices[i].client.name.Length > longest_client6)
                                {
                                    longest_client6 = invoices[i].client.name.Length;
                                }
                            }
                            int longest_type6 = 0;
                            for (int i = 0; i < invoices.Count; i++)
                            {
                                if (invoices[i].project.order_type.Length > longest_type6)
                                {
                                    longest_type6 = invoices[i].project.order_type.Length;
                                }
                            }
                            int longest_amount6 = 0;
                            for (int i = 0; i < invoices.Count; i++)
                            {
                                int amountLength = invoices[i].amount.ToString().Length;
                                if (amountLength > longest_amount6)
                                {
                                    longest_amount6 = amountLength;
                                }
                            }
                            for (int i = 0; i < invoices.Count; i++)
                            {
                                string formattedAmount6 = invoices[i].amount.ToString().PadLeft(longest_amount6);
                                invoice_choices1[i] = $"Klient: {invoices[i].client.name.PadRight(longest_client6)} | Typ zakázky: {invoices[i].project.order_type.PadRight(longest_type6)} | Částka: {formattedAmount6} Kč | Stav: ";
                                if (invoices[i].is_paid == true)
                                {
                                    invoice_choices1[i] += "ZAPLACENO";
                                }
                                else
                                {
                                    invoice_choices1[i] += "NEZAPLACENO";
                                }
                            }
                            int chosen_invoice1 = Menu("ODEBRÁNÍ FAKTURY - Vyberte fakturu, kterou chcete odstranit:", true, invoice_choices1);
                            int yes_no2 = Menu($"Opravdu chcete odstranit fakturu pro klienta {invoices[chosen_invoice1].client.name} (zakázka {invoices[chosen_invoice1].project.order_type})?", true, "Ano, přeji si odstranit fakturu", "Ne, chci zpět");
                            switch (yes_no2)
                            {
                                case 0:
                                    invoices.Remove(invoices[chosen_invoice1]);
                                    SaveInvoices(invoices);
                                    Console.Clear();
                                    Console.CursorVisible = false;
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("Faktura úspěšně odstraněna.");
                                    Console.ResetColor();
                                    Console.WriteLine();
                                    Console.WriteLine("-- Stiskněte libovolnou klávesu pro návrat do menu");
                                    Console.ReadKey();
                                    break;
                                case 1:
                                    break;
                            }
                            break;
                        }
                    case 5:
                        Console.Clear();
                        if (invoices.Count == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("V databázi nejsou žádné faktury, ze kterých by šly udělat statistiky.");
                            Console.ResetColor();
                            Console.WriteLine();
                            Console.WriteLine("-- Stiskněte libovolnou klávesu pro návrat do menu");
                            Console.ReadKey();
                            break;
                        }
                        else
                        {
                            int invoice_num = 0;
                            decimal total = 0;
                            int paid_num = 0;
                            decimal paid_amount = 0;
                            int unpaid_num = 0;
                            decimal unpaid_amount = 0;
                            for (int i = 0; i < invoices.Count; i++)
                            {
                                invoice_num++;
                                total += invoices[i].amount;
                                if (invoices[i].is_paid == true)
                                {
                                    paid_num++;
                                    paid_amount += invoices[i].amount;
                                }
                                else if (invoices[i].is_paid == false)
                                {
                                    unpaid_num++;
                                    unpaid_amount += invoices[i].amount;
                                }
                            }
                            Console.WriteLine("VAŠE FAKTURAČNÍ STATISTIKY");
                            Console.WriteLine();
                            Console.WriteLine($"Celkem vystavených faktur: {invoice_num}");
                            Console.Write($"Celková hodnota faktur: {total} Kč");
                            Console.WriteLine();
                            Console.Write("Celkem zaplacených faktur: ");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(paid_amount);
                            Console.ResetColor();
                            Console.Write("Celková hodnota zaplacených faktur: ");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"{paid_amount} Kč");
                            Console.ResetColor();
                            Console.Write("Celkem nezaplacených faktur: ");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(unpaid_num);
                            Console.ResetColor();
                            Console.Write("Celková hodnota nezaplacených faktur: ");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"{unpaid_amount} Kč");
                            Console.ResetColor();
                            Console.WriteLine();
                            Console.WriteLine("-- Stiskněte libovolnou klávesu pro návrat do menu");
                            Console.ReadKey();
                            break;
                        }
                    case 6:
                        return;
                }

            }
        }
        static void SaveClients(List<Client> clients)
        {
            var serializer = new JsonSerializerOptions { WriteIndented = true };
            string jsonClients = JsonSerializer.Serialize(clients, serializer);
            File.WriteAllText("clients.json", jsonClients);

        } //hotovo
        static void SaveProjects(List<Project> projects)
        {
            var serializer = new JsonSerializerOptions { WriteIndented = true };
            string jsonProjects = JsonSerializer.Serialize(projects, serializer);
            File.WriteAllText("projects.json", jsonProjects);

        } //hotovo
        static void SaveInvoices(List<Invoice> invoices)
        {
            var serializer = new JsonSerializerOptions { WriteIndented = true };
            string jsonInvoices = JsonSerializer.Serialize(invoices, serializer);
            File.WriteAllText("invoices.json", jsonInvoices);

        }// hotovo
        static void LoadData(List<Client> clients, List<Project> projects, List<Invoice> invoices)
        {
            if (File.Exists("clients.json"))
            {
                string json = File.ReadAllText("clients.json");
                var loadedClients = JsonSerializer.Deserialize<List<Client>>(json);
                if (loadedClients != null) clients.AddRange(loadedClients);
            }


            if (File.Exists("projects.json"))
            {
                string json = File.ReadAllText("projects.json");
                var loadedProjects = JsonSerializer.Deserialize<List<Project>>(json);
                if (loadedProjects != null) projects.AddRange(loadedProjects);
            }


            if (File.Exists("invoices.json"))
            {
                string json = File.ReadAllText("invoices.json");
                var loadedInvoices = JsonSerializer.Deserialize<List<Invoice>>(json);
                if (loadedInvoices != null) invoices.AddRange(loadedInvoices);
            }
        } //hotovo
        public static string Input_verification_ico(int min, int max)
        {
            int num;
            while (true)
            {
                string vstup = Console.ReadLine();
                if (!int.TryParse(vstup, out num))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Chyba: Musíte zadat pouze čísla (8).");
                    Console.Write("Zkuste to znovu: ");
                    Console.ResetColor();
                    continue;
                }
                if (vstup.Length < min)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Chyba: Zadali jste málo číslic. Požadovaný počet je {min}.");
                    Console.Write("Zkuste to znovu: ");
                    Console.ResetColor();
                    continue;
                }
                if (vstup.Length > max)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Chyba: Zadali jste moc číslic. Požadovaný počet je {min}.");
                    Console.Write("Zkuste to znovu: ");
                    Console.ResetColor();
                    continue;
                }
                return vstup;
            }
        } //hotovo
        public static int Input_verification_int(int min)
        {
            int num;
            while (!int.TryParse(Console.ReadLine(), out num) || num < min)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Chyba: musíte zadat pouze čísla větší než {min}!");
                Console.ResetColor();
                Console.Write("Zkuste to znovu: ");
            }
            return num;
        }
        public static double Input_verification_double(double min)
        {
            double num;
            while (!double.TryParse(Console.ReadLine(), out num) || num < min)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Chyba: musíte zadat pouze čísla větší než {min}!");
                Console.ResetColor();
                Console.Write("Zkuste to znovu: ");
            }
            return num;
        }
        public static decimal Input_verification_decimal(int min)
        {
            decimal num;
            while (!decimal.TryParse(Console.ReadLine(), out num) || num < min)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Chyba: musíte zadat pouze čísla větší než {min}!");
                Console.ResetColor();
                Console.Write("Zkuste to znovu: ");
            }
            return num;
        }
    }

}