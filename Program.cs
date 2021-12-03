using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace Labb2AkselVilgot
{
   // public delegate Result(); 
    class Program
    {

        static void Main(string[] args)
        {
            Result results = new Result();
            int key = 0;
            bool run = false;
            Console.WriteLine("Press 1 for all data");
            Console.WriteLine("Press 2 for test data");
            Console.WriteLine("Press 3 for filter by id");
            Console.WriteLine("Press 4 for filter by node");
            Console.WriteLine("Press 5 for filter by id and node");
            Console.WriteLine("Press 6 for filter by node value");
            Console.WriteLine("Press 0 to exit");
            int input = Convert.ToInt32(Console.ReadLine());

            // Input mindre än 7 så körs programmet vidare
            if (input < 7)
            {
                run = true;
            }
            // om input är 0 ska programmet stängas av
            else if (input == 0)
            {
                run = false;
                Environment.Exit(0);
            }
            else
            // för varje felaktig input frågas om ny
            {
                while (input > 7 || !int.TryParse(Console.ReadLine(), out input))
                {
                    Console.WriteLine("Wrong Input, try again with a number between 1-6");
                    input = Convert.ToInt32(Console.ReadLine());
                }

            }

            
            
            // om input inte är 0 (dvs för exit) och om run = true så körs menyn om och om igen
                while (input != key && run == true)
                {

                    // varje metod har varsitt anrop baserat på input
                    switch (input)
                    {
                        case 1:
                            results.GetAll();
                            break;
                        case 2:
                            results.GetTestData();
                            break;
                        case 3:
                            Console.WriteLine("Enter ID");
                            input = Convert.ToInt32(Console.ReadLine());
                            int id = input;
                            results.GetFilteredByID(id);
                            break;
                        case 4:
                            Console.WriteLine("Enter node");
                            string node = Console.ReadLine();
                            results.GetFilteredByNode(node);
                            break;
                        case 5:
                            Console.WriteLine("Enter ID");
                            input = Convert.ToInt32(Console.ReadLine());
                            int id2 = input;
                            Console.WriteLine("Enter node");
                            string node2 = Console.ReadLine();
                            results.GetFilteredByIDandNode(id2, node2);

                            break;
                        case 6:
                            Console.WriteLine("Enter node");
                            string node3 = Console.ReadLine();
                            Console.WriteLine("Enter value");
                            string value = Console.ReadLine();
                            results.GetFilteredByNodeValue(node3, value);
                            break;
                        case 0:
                        Environment.Exit(0);
                            break;

                        default:
                            Console.WriteLine("Invalid input");
                            break;

                    }
                Console.WriteLine();
                    Console.WriteLine("Press 1 for all data");
                    Console.WriteLine("Press 2 for test data");
                    Console.WriteLine("Press 3 for filter by id");
                    Console.WriteLine("Press 4 for filter by node");
                    Console.WriteLine("Press 5 for filter by id and node");
                    Console.WriteLine("Press 6 for filter by node value");
                      Console.WriteLine("Press 0 to exit");

                input = Convert.ToInt32(Console.ReadLine());

                }
        }
    }
}
