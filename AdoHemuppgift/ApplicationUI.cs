using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdoHemuppgift
{
    internal class ApplicationUI
    {
        public DatabaseHandler DbHandler { get; set; }

        public ApplicationUI(DatabaseHandler dbHandler)
        {
            this.DbHandler = dbHandler;
        }


        public void RunUI()
        {
            bool applicationRun = true;
            while (applicationRun)
            {


                Console.WriteLine("Choose one of the the alternatives;\n");

                Console.WriteLine("1.List all films for a specific actor");
                Console.WriteLine("2.Exit application");

                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        int choosenActorId = ChooseActor();
                        Console.WriteLine("bajs");
                        break;
                    case 2:
                        applicationRun = false;
                        break;
                    default:
                        Console.WriteLine("You must choose an existing alternative.");
                        break;
                }
            }
        }

        public int ChooseActor()
        {
            Console.Clear();
            Console.WriteLine("Choose one of the Actors in the list;\n");
            ListAllActors();
            int choosenActorID = int.Parse(Console.ReadLine());
            return choosenActorID;
        }
        public void ListAllActors()
        {
            var allActors = DbHandler.GetAllActors();

            foreach (var actor in allActors)
            {
                Console.WriteLine(actor);
            }
        }
    }
}
