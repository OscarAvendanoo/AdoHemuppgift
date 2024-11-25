using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                        string[] choosenActor = ChooseActor();
                        string firstname = choosenActor[0];
                        string lastname = choosenActor[1];
                        if (choosenActor[2] is "MultipleActors")
                        {
                            int actorId = ChooseWhichActorByID(firstname, lastname);
                            var actorFilms = DbHandler.GetAllFilmsFromActorByID(actorId);
                            Console.WriteLine("Here are all the films for the actor;\n");
                            PrintList(actorFilms);
                            PressEnterToContinue();
                        }
                        else
                        {
                            var actorFilms = DbHandler.GetAllFilmsFromActorByName(firstname, lastname);
                            Console.WriteLine("Here are all the films for the actor;\n");
                            PrintList(actorFilms);
                            PressEnterToContinue();
                        }
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

        public int ChooseWhichActorByID(string firstname, string lastname)
        {
            var actorsAndId = DbHandler.GetActorsID(firstname, lastname);
            Console.WriteLine("There are more than one actor with that name, please identify the actor by its ID;\n");
            PrintList(actorsAndId);
            int choice = int.Parse(Console.ReadLine());
            Console.Clear();
            return choice;
        }

        public string[] ChooseActor()
        {
            bool actorFound = false;
            while (actorFound != true)
            {
                Console.Clear();
                Console.WriteLine("Type the first and lastname of the actor;");
                string actorName = Console.ReadLine();
                Console.Clear();

                string[] fullNameAndReturnCode = new string[3];
                string[] firstAndLastname = actorName.Split(' ');

                if (firstAndLastname.Length >= 2)
                {
                    string firstname = firstAndLastname[0];
                    string lastname = firstAndLastname[1];
                    int actorExist = DbHandler.CheckIfActorExist(firstname, lastname);
                    if (actorExist == 1)
                    {
                        actorFound = true;
                        return firstAndLastname;
                    }
                    else if (actorExist < 0)
                    {
                        Console.WriteLine("The actor was not found, please try again");
                        PressEnterToContinue();
                    }
                    else if (actorExist > 1)
                    {
                        fullNameAndReturnCode[0] = firstAndLastname[0];
                        fullNameAndReturnCode[1] = firstAndLastname[1];
                        fullNameAndReturnCode[2] = "MultipleActors";
                        return fullNameAndReturnCode;
                    }

                }
                else
                {
                    Console.WriteLine("You must type both firstname and lastname separated by a blankspace");
                    PressEnterToContinue();
                }
            }
            return null;
        }


        public void PrintList(List<string> list)
        {
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }
        public void PressEnterToContinue()
        {
            Console.WriteLine();
            Console.WriteLine("Press Enter To Continue..");
            Console.ReadLine();
            Console.Clear();
        }
    }
}