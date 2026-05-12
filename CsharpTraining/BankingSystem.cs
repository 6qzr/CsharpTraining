using System.Security.Principal;

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

            //varibles needed for Task 2
            string bankName = "National Bank of Oman";
            string tagline = "Your Trusted Banking Partner";
            string foundYear = "1973";
            string branchName = "Muscat Main Branch";
            string city = "Muscat";
            string Address = "Maktabi Building, Ruwi, Muscat, Oman";
            string openHours = "Weekdays (Sun–Thu): 8:00 AM – 3:00 PM\nWeekends (Sat): 9:00 AM – 1:00 PM\nFriday: Closed";

            //varibles needed for Task 4
            const string CORRECT_PIN = "4821"; 
            const int MAX_ATTEMPTS = 3;


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

                switch(option)
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
                        char type = char.ToUpper(Convert.ToChar(Console.ReadLine()));
                        if(type.Equals('S') || type.Equals('C') || type.Equals('F'))
                        {
                            accountType = type;
                            Console.WriteLine("Account type set to: " + accountType);
                        }
                        else
                        {
                            Console.WriteLine("Invalid account type");
                        }
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
            int module = 1;
            while(module != 0)
            {
                Console.Write("\nNATIONAL BANK OF OMAN  —  Unified Banking System\n\n" +
                    "Task 1  ->  System Setup (populate all shared variables first)\n\n" +
                    "MAIN MENU  (available after setup)\n" +
                    "1) ATM Services          ->  Tasks  2,  3,  4,  5\n" +
                    "2) Account Management    ->  Tasks  6,  7,  8\n" +
                    "3) Loan Services         ->  Tasks  9, 10, 11\n" +
                    "4) Currency Exchange     ->  Tasks 12, 13\n" +
                    "5) Credit Card Portal    ->  Tasks 14, 15\n" +
                    "6) Branch Services       ->  Tasks 16, 17, 18\n" +
                    "7) Reports & Admin       ->  Tasks 19, 20, 21\n" +
                    "8) [BONUS] Full Terminal ->  Task  22  (optional)\n" +
                    "0) Exit\n\n" +
                    "Select module: "
                );
                module = int.Parse(Console.ReadLine());

                switch(module)
                {
                    case 1:
                        int atmOption = 1;
                        while (atmOption != 0)
                        {
                            Console.Write("\n=== ATM SERVICES ===\n\n" +
                                "1) Bank Info\n" +
                                "2) View Account Data\n" +
                                "3) Authenticate\n" +
                                "4) Print Receipt\n" +
                                "0) Back to Main Menu\n\n" +
                                "Select: "
                            );
                            atmOption = int.Parse(Console.ReadLine());
                            switch (atmOption)
                            {
                                case 1:
                                    int bankOption = 1;
                                    while (bankOption != 0)
                                    {
                                        Console.Write("\n=== BANK INFO ===\n\n" +
                                            "1) Bank Info\n" +
                                            "2) Branch Info\n" +
                                            "3) Opening Hours\n" +
                                            "0) Back\n\n" +
                                            "Select: "
                                        );
                                        bankOption = int.Parse(Console.ReadLine());
                                        switch (bankOption)
                                        {
                                            case 1:
                                                Console.WriteLine("Bank Name: " + bankName);
                                                Console.WriteLine("Tagline: " + tagline);
                                                Console.WriteLine("Found year: " + foundYear);
                                                break;

                                            case 2:
                                                Console.WriteLine("Branch Name: " + branchName);
                                                Console.WriteLine("City: " + city);
                                                Console.WriteLine("Address: " + Address);
                                                break;

                                            case 3:
                                                Console.WriteLine("Opening Hours\n" + openHours);
                                                break;

                                            case 0:
                                                Console.WriteLine("Returning to ATM Services...");
                                                break;

                                            default:
                                                Console.WriteLine("Invalid selection. Please try again.");
                                                break;
                                        }
                                    }
                                    break;

                                case 2:
                                    int viewOption = 1;
                                    while (viewOption != 0)
                                    {
                                        Console.Write("\n=== VIEW ACCOUNT DATA ===\n\n" +
                                            "Data loaded from system setup\n\n" +
                                            "1) Account Number\n" +
                                            "2) Holder Name\n" +
                                            "3) Balance\n" +
                                            "4) Account Status\n" +
                                            "5) Account Type\n" +
                                            "0) Back\n\n" +
                                            "Select feild: "
                                        );
                                        viewOption = int.Parse(Console.ReadLine());
                                        switch (viewOption)
                                        {
                                            case 1:
                                                Console.WriteLine("Account Number: " + accountNumber);
                                                break;

                                            case 2:
                                                Console.WriteLine("Holder Name: " + holderName);
                                                break;

                                            case 3:
                                                Console.WriteLine($"Balance: {balance:N3} OMR");
                                                break;
                                            
                                            case 4:
                                                if (isActive)
                                                {
                                                    Console.WriteLine("The account is Active");
                                                }
                                                else 
                                                {
                                                    Console.WriteLine("The account is NOT active");
                                                }
                                                break;
                                            
                                            case 5:
                                                if (accountType.Equals('S'))
                                                {
                                                    Console.WriteLine("Savings (S) account");
                                                }
                                                else if (accountType.Equals('C'))
                                                {
                                                    Console.WriteLine("Current (C) account");
                                                }
                                                else if (accountType.Equals('F'))
                                                {
                                                    Console.WriteLine("Fixed Deposit (F) account");
                                                }
                                                break;

                                            case 0:
                                                Console.WriteLine("Returning to ATM Services...");
                                                break;

                                            default:
                                                Console.WriteLine("Field not available.");
                                                break;
                                        }
                                    }
                                    break;

                                case 3:
                                    int authOption = 1;
                                    int attempts = MAX_ATTEMPTS;
                                    while (authOption != 0 && attempts > 0)
                                    {
                                        Console.Write("\n=== AUTHENTICATION ===\n\n" +
                                            "1) Enter PIN\n" +
                                            "2) Forgot PIN\n" +
                                            "0) Back\n\n" +
                                            "Select: "
                                        );
                                        authOption = int.Parse(Console.ReadLine());
                                        switch (authOption)
                                        {
                                            case 1:
                                                Console.Write("Enter PIN: ");
                                                string pin = Console.ReadLine();
                                                if (pin.Equals(CORRECT_PIN))
                                                {
                                                    Console.WriteLine("Access granted. Welcome, " + holderName);
                                                }
                                                else if (pin.Length != 4)
                                                {
                                                    Console.WriteLine("Invalide PIN format");
                                                    attempts--;
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Incorrect PIN");
                                                    attempts--;
                                                }

                                                if (attempts == 0)
                                                {
                                                    Console.WriteLine("Max attempts reached...");
                                                }
                                                break;
                                            case 2:
                                                Console.WriteLine("Please visit the nearest branch with your National ID.");
                                                break;

                                            case 0:
                                                Console.WriteLine("Returning to ATM Services...");
                                                break;

                                            default:
                                                Console.WriteLine("Invalid option");
                                                break;
                                        }
                                    }
                                    break;

                                case 4:
                                    break;

                                case 0:
                                    Console.WriteLine("Returning to Main Menu...");
                                    break;

                                default:
                                    Console.WriteLine("Invalid selection. Please try again.");
                                    break;
                            }
                        }
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
}
