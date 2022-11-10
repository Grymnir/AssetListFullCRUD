using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AssetlistFullCRUD
{
    public class App
    {
        static AssetlistContext context = new AssetlistContext();

        public void Run()
        {
            //Functions.ClearDatabase();                //Körs en gång sedan kommenteras ut
            
            MainMenu();
        }
        public void MainMenu()
        {
            Header("Huvudmeny");

            ShowAllAssetsBrief();

            Console.WriteLine("Välkommen till AssetTracking. Välj vad du vill göra?");
            Console.WriteLine("a) Gå till huvudmeny");
            Console.WriteLine("b) Uppdatera en assetpost");
            Console.WriteLine("c) Skapa en assetpost");
            Console.WriteLine("d) Ta bort en assetpost");
            Console.WriteLine("e) Rapporter");
            Console.WriteLine("q) avlutar");

            ConsoleKey command;
            do
            {
                command = Console.ReadKey(true).Key;

                if (command == ConsoleKey.A)
                    MainMenu();

                if (command == ConsoleKey.B)
                    PageUpdatePost();

                if (command == ConsoleKey.C)
                    PageCreatePost();

                if (command == ConsoleKey.D)
                    PageDeletePost();
                if (command == ConsoleKey.E)
                    RapportMenu();

            }
            while (command != ConsoleKey.Q);
        }

        public void RapportMenu()
        {
            Header("RapportMenu");

            ShowAllAssetsBrief();

            Console.WriteLine("Välkommen till Rapportering. Välj vad du vill göra? Avsluta med Q");
            Console.WriteLine("a) Tillbaka till Huvudmenyn");
            Console.WriteLine("b) Rapport alla datorer");
            Console.WriteLine("c) Rapport alla Mobiler");
            Console.WriteLine("d) Rapport äldst Asset till Nyast Asset");
         
            ConsoleKey command;
            do
            {
                command = Console.ReadKey(true).Key;

                if (command == ConsoleKey.A)
                    MainMenu();

                if (command == ConsoleKey.B)
                    Alladatorer();

                if (command == ConsoleKey.C)
                    Allamobiler();
                if (command == ConsoleKey.D)
                    Aldsttillnyast();
            }
            while (command != ConsoleKey.Q);
        }

        private void PageUpdatePost()
        {
            Header("Uppdatera");

            ShowAllAssetsBrief();

            Write("\nVilken assetpost vill du uppdatera? ");

            int assetId = int.Parse(Console.ReadLine());
            var asset = context.Assets.Find(assetId);

            if(context.Assets.Contains(asset))
            {
                WriteLine($"Det nuvarande datat är: {asset.Id.ToString()} {asset.Category} { asset.Modelname} {asset.Price} {asset.Purchasedate.ToShortDateString()} {asset.Country} {asset.Office} {asset.Ccy}");

                EnterAsset(ref asset);

                context.Assets.Update(asset);
                context.SaveChanges();

                Write("Assetposten uppdaterad.");
                Console.ReadKey();
            }
            else
            {
                Write("Assetposten finns inte!");
                Console.ReadKey();
            }
            MainMenu();
        }

        private void EnterAsset(ref Asset asset)
        {
            string choice;

            Console.WriteLine("Skriv 'L' för Laptop' 'M' för Mobil Tryck 'Q' för att avsluta och visa lista!");
            choice = Console.ReadLine();

            if (choice.ToUpper() == "M")
            {
                asset.Category = Category.Mobiles;
                Console.WriteLine("Är det en Iphone, Nokia eller Samsung?");
            }
            else if (choice.ToUpper() == "L")
            {
                asset.Category = Category.Laptops;
            }
            else
            {
                Console.WriteLine("Du har angett ett felaktigt värde, försök igen.");
            }

            Console.WriteLine("Mata in modell");
            asset.Modelname = Console.ReadLine();

            Console.WriteLine("Mata in inköpsdatum i format YYYY-MM-DD");
            choice = Console.ReadLine();
            asset.Purchasedate = Convert.ToDateTime(choice);

            Console.WriteLine("Mata in country: ");
            asset.Country = Console.ReadLine();

            Console.WriteLine("Mata in valuta: ");
            asset.Ccy = Console.ReadLine();

            Console.WriteLine("Mata in office: ");
            asset.Office = Console.ReadLine();

            Console.WriteLine("Mata in inköpspris: ");
            choice = Console.ReadLine();
            asset.Price = Convert.ToDouble(choice);
        }

        private void PageCreatePost()
        {
            Header("Skapa\n");

            ShowAllAssetsBrief();

            Write("Skapa ny asset-post.\n");

            var asset = new Asset();

            EnterAsset(ref asset);

            context.Assets.Add(asset);
            context.SaveChanges();

            Write("assetposten inlagd.");
            Console.ReadKey();
            MainMenu();
        }

        private void PageDeletePost()
        {
            Header("Ta bort en Asset\n");

            ShowAllAssetsBrief();

            Write("Vilken Assetpost vill du ta bort?\n");

            int assetId = int.Parse(Console.ReadLine());
            var asset = context.Assets.Find(assetId);

            if(context.Assets.Contains(asset))
            {
                context.Assets.Remove(asset);
                context.SaveChanges();

                Write("Assetposten raderad.");
                Console.ReadKey();
                MainMenu();
            }
            else
            {
                Write("Assetposten finns inte");
                Console.ReadKey();
                MainMenu();
            }
        }


        private void ShowAllAssetsBrief()
        {
            WriteLine("Assetlista:");
            foreach (var asset in context.Assets)
            {
                WriteLine($"Id:{asset.Id} Kat:{asset.Category} Namn:{ asset.Modelname} Pris:{asset.Price} Inköpsdat:{asset.Purchasedate.ToShortDateString()} Land:{asset.Country} Stad:{asset.Office} Valuta:{asset.Ccy} \n");
            }
        }

        private void Header(string text)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine(text.ToUpper());
            Console.WriteLine();
        }

        private void Alladatorer()
        {
            Console.WriteLine("Här kommer en lista på samtliga datorer:");
            foreach (var asset in context.Assets)
            {
                if (asset.Category == Category.Laptops)
                {
                    Console.WriteLine($"Id:{asset.Id} Kat:{asset.Category} Namn:{ asset.Modelname} Pris:{asset.Price} Inköpsdat:{asset.Purchasedate.ToShortDateString()} Land:{asset.Country} Stad:{asset.Office} Valuta:{asset.Ccy} \n");
                }
            }
            Console.Write("Tryck Enter för att komma tillbaka till Rapportmenyn:");
            Console.ReadKey();
            RapportMenu();
        }
        private void Allamobiler()
        {
            Console.WriteLine("Här kommer en lista på samtliga mobiler:");
            foreach (var asset in context.Assets)
            {
                if (asset.Category == Category.Mobiles)
                {
                    Console.WriteLine($"Id:{asset.Id} Kat:{asset.Category} Namn:{ asset.Modelname} Pris:{asset.Price} Inköpsdat:{asset.Purchasedate.ToShortDateString()} Land:{asset.Country} Stad:{asset.Office} Valuta:{asset.Ccy} \n");
                }
            }
            Console.Write("Tryck Enter för att komma tillbaka till Rapportmenyn:");
            Console.ReadKey();
            RapportMenu();
        }

        private void Aldsttillnyast()
        {
            Console.WriteLine("Här kommer en sorterad lista på samtliga assets äldst till nyast:");

            //assetList = assetList.OrderBy(p => p.Category).ThenBy(a => a.Purchasedate).ToList();

            var sortedlist = context.Assets.OrderBy(a => a.Purchasedate).ToList();

            foreach (var asset in sortedlist)
            {
                
                Console.WriteLine($"Id:{asset.Id.ToString()} Kat:{asset.Category} Namn:{ asset.Modelname} Pris:{asset.Price} Inköpsdat:{asset.Purchasedate.ToShortDateString()} Land:{asset.Country} Stad:{asset.Office} Valuta:{asset.Ccy} \n");
                
            }
            Console.Write("Tryck Enter för att komma tillbaka till Rapportmenyn:");
            Console.ReadKey();
            RapportMenu();
        }
        private void WriteLine(string text = "")
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(text);
        }

        private void Write(string text)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(text);
        }
    }

}
