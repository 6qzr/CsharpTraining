namespace CsharpTraining
{
    internal class BankingSystem
    {
        static void Main(string[] args)
        {
            //system setup ( system storage )
            /*
                declare variables
                add initial values
            */


            //System Menu Architecture
            Console.Write("NATIONAL BANK OF OMAN  —  Unified Banking System\n\n" +
                "MAIN MENU\n" +
                "1) ATM Services          >  Tasks  1,  2,  3,  4\n" +
                "2) Account Management    >  Tasks  5,  6,  7\n" +
                "3) Loan Services         >  Tasks  8,  9, 10\n" +
                "4) Currency Exchange     >  Tasks 11, 12\n" +
                "5) Credit Card Portal    >  Tasks 13, 14\n" +
                "6) Branch Services       >  Tasks 15, 16, 17\n" +
                "7) Reports & Admin       >  Tasks 18, 19, 20\n" +
                "8) [BONUS] Full Terminal >  Task  21\n" +
                "0) Exit\n\n" +
                "Select module: "
            );
            int module = int.Parse(Console.ReadLine());
            
            switch(module)
            {
                case 1:

                    break;

                case 2:

                    break;

                case 3:

                    break;

                case 4:

                    break;

                case 5:

                    break;

                case 6:

                    break;

                case 7:

                    break;

                case 8:

                    break;

                case 0:

                    break;

                default:

                    Console.WriteLine("Invalid module");
                    break;
            }
        }
    }
}
