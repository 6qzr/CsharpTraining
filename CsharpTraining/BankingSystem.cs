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
            int accountNumber = 0;
            string holderName = "[not set]";
            double balance = 0.000;
            bool isActive = false;
            char accountType = '-';
            bool isEmployed = false;
            double salary = 0.000;
            int creditScore = 0;
            int age = 0;
            double deposit = 0.000;
            double withdrawal = 0.000;
            double annualRate = 0.000;
            double avgBalance = 0.000;

            //string bankName = "National Bank of Oman";
            //string tagline = "Your Trusted Banking Partner";
            //string foundYear = "1973";
            //string branchName = "Muscat Main Branch";
            //string city = "Muscat";
            //string Address = "Maktabi Building, Ruwi, Muscat, Oman";
            //string openHours = "Weekdays (Sun–Thu): 8:00 AM – 3:00 PM\nWeekends (Sat): 9:00 AM – 1:00 PM\nFriday: Closed";


            /*
             * ===========================================================
             *                      System Setup
             * ===========================================================
            */
            Console.Write($"=== SYSTEM SETUP  —  Enter Account & Customer Data ===\n\n" +
                $"--- Account Profile ---\n" +
                $"1)  Account Number       (int)    current: {accountNumber}\n" +
                $"2)  Holder Name          (string) current: {holderName}\n" +
                $"3)  Balance              (double) current: {balance:N3} OMR\n" +
                $"4)  Account Active?      (bool)   current: {isActive}   [enter 1=yes / 0=no]\n" +
                $"5)  Account Type         (char)   current: {accountType}       [enter S / C / F]\n" +
                $"\n--- Customer Profile ---\n" +
                $"6)  Employed?            (bool)   current: {isEmployed}   [enter 1=yes / 0=no]\n" +
                $"7)  Monthly Salary       (double) current: {salary:N3} OMR\n" +
                $"8)  Credit Score         (int)    current: {creditScore}\n" +
                $"9)  Age                  (int)    current: {age}\n" +
                $"\n--- Transaction Data ---\n" +
                $"10) Last Deposit Amount  (double) current: {deposit:N3} OMR\n" +
                $"11) Last Withdrawal      (double) current: {withdrawal:N3} OMR\n" +
                $"12) Annual Interest Rate (double) current: {annualRate}       [e.g. 0.035 = 3.5%]\n" +
                $"13) Avg Monthly Balance  (double) current: {avgBalance:N3} OMR\n" +
                $"\n0)  Setup complete — launch Main Menu\n"
            );
            int option = 1, input;
            while(option != 0)
            {
                Console.Write("\nSelect option: ");
                option = int.Parse(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        Console.Write("Enter account number: ");
                        accountNumber = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Account number set to: " + accountNumber);
                        break;

                    case 2:
                        Console.Write("Enter holder name: ");
                        holderName = Console.ReadLine();
                        Console.WriteLine("Holder name set to: " + holderName);
                        break;

                    case 3:
                        Console.Write("Enter balance (OMR): ");
                        balance = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine($"Balance set to: {balance:N3} OMR");
                        break;

                    case 4:
                        Console.Write("Enter account active 1=yes / 0=no: ");
                        input = Convert.ToInt32(Console.ReadLine());
                        if (input == 1)
                        {
                            isActive = true;
                        }
                         else
                        {
                            isActive = false;
                        }
                        Console.WriteLine("Account active set to: " + isActive);
                        break;

                    case 5:
                        Console.Write("Enter account type S / C / F: ");
                        accountType = Convert.ToChar(Console.ReadLine());
                        Console.WriteLine("Account type set to: " + accountType);
                        break;

                    case 6:
                        Console.Write("Enter employed? 1=yes / 0=no: ");
                        input = Convert.ToInt32(Console.ReadLine());
                        if (input == 1)
                        {
                            isEmployed = true;
                        }
                        else
                        {
                            isEmployed = false;
                        }
                        Console.WriteLine("Employed set to: " + isEmployed);
                        break;

                    case 7:
                        Console.Write("Enter salary: ");
                        salary = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine($"Salary set to: {salary:N3} OMR");
                        break;

                    case 8:
                        Console.Write("Enter credit score: ");
                        creditScore = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Credit score set to: " + creditScore);
                        break;

                    case 9:
                        Console.Write("Enter age: ");
                        age = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Age set to: " + age);
                        break;

                    case 10:
                        Console.Write("Enter deposit amount: ");
                        deposit = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine($"Last deposit amount set to: {deposit:N3} OMR");
                        break;

                    case 11:
                        Console.Write("Enter Withdrawal: ");
                        withdrawal = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine($"Last withdrawal amount set to: {withdrawal:N3} OMR");
                        break;

                    case 12:
                        Console.Write("Enter annual interest rate [e.g. 0.035 = 3.5%]: ");
                        annualRate = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine($"Annual interest rate set to: {annualRate:N3} OMR");
                        break;

                    case 13:
                        Console.Write("Enter average monthly balance: ");
                        avgBalance = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine($"Average monthly balance set to: {avgBalance:N3} OMR");
                        break;

                    case 0:
                        Console.WriteLine("Setup complete. Launching Main Menu...");
                        break;

                    default:
                        Console.WriteLine("'Invalid option. Please choose 1–13 or 0 to finish.");
                        break;
                }
            }
            
            
            /*
             * ===========================================================
             *                 System Menu Architecture
             * ===========================================================
            */
            //Console.Write("NATIONAL BANK OF OMAN  —  Unified Banking System\n\n" +
            //    "MAIN MENU\n" +
            //    "1) ATM Services          >  Tasks  1,  2,  3,  4\n" +
            //    "2) Account Management    >  Tasks  5,  6,  7\n" +
            //    "3) Loan Services         >  Tasks  8,  9, 10\n" +
            //    "4) Currency Exchange     >  Tasks 11, 12\n" +
            //    "5) Credit Card Portal    >  Tasks 13, 14\n" +
            //    "6) Branch Services       >  Tasks 15, 16, 17\n" +
            //    "7) Reports & Admin       >  Tasks 18, 19, 20\n" +
            //    "8) [BONUS] Full Terminal >  Task  21\n" +
            //    "0) Exit\n\n" +
            //    "Select module: "
            //);
            //int module = int.Parse(Console.ReadLine());
            
            //switch(module)
            //{
            //    case 1:
            //        Console.Write("\n=== ATM SERVICES ===\n\n" +
            //        "1) Bank Info\n" +
            //        "2) Branch Info\n" +
            //        "3) Opening Hours\n" +
            //        "0) Back to Main Menu\n\n" +
            //        "Select: "
            //        );
            //        int option = int.Parse(Console.ReadLine());
            //        switch (option) {
            //            case 1:
            //                Console.WriteLine("Bank Name: " + bankName);
            //                Console.WriteLine("Tagline: " + tagline);
            //                Console.WriteLine("Found year: " + foundYear);
            //                break;

            //            case 2:
            //                Console.WriteLine("Branch Name: " + branchName);
            //                Console.WriteLine("City: " + city);
            //                Console.WriteLine("Address: " + Address);
            //                break;

            //            case 3:
            //                Console.WriteLine("Opening Hours\n" + openHours);
            //                break;

            //            case 0:
            //                Console.WriteLine("Returning to Main Menu...");
            //                break;

            //            default:
            //                Console.WriteLine("Invalid selection. Please try again.");
            //                break;
            //        }
            //        break;

            //    case 2:

            //        break;

            //    case 3:

            //        break;

            //    case 4:

            //        break;

            //    case 5:

            //        break;

            //    case 6:

            //        break;

            //    case 7:

            //        break;

            //    case 8:

            //        break;

            //    case 0:

            //        break;

            //    default:

            //        Console.WriteLine("Invalid module");
            //        break;
            //}
        }
    }
}
