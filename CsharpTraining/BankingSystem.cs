using System.Security.Principal;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

            //varible needed for Task 9
            const double MAX_WITHDRAWAL = 5000.000;

            //varibles needed for Task 10
            const double VAT_RATE = 0.05, WIRE = 5.000, ATM_FEE = 0.500, POS = 0.000, ONLINE = 0.250;


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
                                    int printOption = 1;
                                    while (printOption != 0)
                                    {
                                        Console.Write("\n=== PRINT RECEIPT ===\n\n" +
                                            "1) Short Receipt\n" +
                                            "2) Detailed Receipt\n" +
                                            "3) Balance Only\n" +
                                            "0) Back\n\n" +
                                            "Select format: "
                                        );
                                        printOption = int.Parse(Console.ReadLine());
                                        switch (printOption)
                                        {
                                            case 1:
                                                string maskedacc = $"****{accountNumber % 10000}";
                                                Console.WriteLine($"Holder: {holderName}\n" +
                                                    $"Account: {accountNumber}\n" +
                                                    $"Balance: {balance:N3} OMR\n");
                                                break;
                                            case 2:
                                                Console.WriteLine(
                                                    $"Account Number : {accountNumber}\n" +
                                                    $"Holder Name    : {holderName}\n" +
                                                    $"Balance        : {balance:N3} OMR\n" +
                                                    $"Account Active : {isActive}\n" +
                                                    $"Account Type   : {accountType}\n" +
                                                    $"Employed       : {isEmployed}\n" +
                                                    $"Salary         : {salary:N3} OMR\n" +
                                                    $"Credit Score   : {creditScore}\n" +
                                                    $"Age            : {age}\n" +
                                                    $"Last Deposit   : {deposit:N3} OMR\n" +
                                                    $"Last Withdrawal: {withdrawal:N3} OMR\n" +
                                                    $"Annual Rate    : {annualRate}\n" +
                                                    $"Avg Balance    : {avgBalance:N3} OMR"
                                                );
                                                break;

                                            case 3:
                                                Console.WriteLine($"Balance: {balance:N3} OMR");
                                                break;

                                            case 0:
                                                Console.WriteLine("Returning to ATM Services...");
                                                break;

                                            default:
                                                Console.WriteLine("Invalid receipt format.");
                                                break;
                                        }
                                    }
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
                        int accOption = 1;
                        while (accOption != 0)
                        {
                            Console.Write("\n=== Account Management ===\n\n" +
                                "1) Transaction Calculator\n" +
                                "2) Account Types\n" +
                                "3) Loan Eligibility\n" +
                                "0) Back to Main Menu\n\n" +
                                "Select: "
                            );
                            accOption = int.Parse(Console.ReadLine());
                            switch (accOption)
                            {
                                case 1:
                                    int transOption = 1;
                                    while (transOption != 0)
                                    {
                                        Console.Write("\n=== TRANSACTION CALCULATOR ===\n\n" +
                                            $"Using: balance={balance:N3}   deposit={deposit:N3}    rate={annualRate * 100:N1}%\n" +
                                            "1) Balance After Deposit\n" +
                                            "2) Balance After Withdrawal\n" +
                                            "3) Annual Interest Earned\n" +
                                            "4) Net Balance Change\n" +
                                            "0) Back\n\n" +
                                            "Select calculation: "
                                        );
                                        transOption = int.Parse(Console.ReadLine());
                                        switch (transOption)
                                        {
                                            case 1:
                                                balance = balance + deposit;
                                                Console.WriteLine($"Balance after deposit: {balance:N3} OMR");
                                                break;

                                            case 2:
                                                if (withdrawal < balance)
                                                {
                                                    balance = balance - withdrawal;
                                                    Console.WriteLine($"Balance after withdrawal: {balance:N3} OMR");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Insufficient amount!");
                                                }
                                                break;

                                            case 3:
                                                double interest = balance * annualRate;
                                                Console.WriteLine($"Rate: {annualRate * 100:N1}%\n" +
                                                    $"Interest amount: {interest:N3} OMR");
                                                break;

                                            case 4:
                                                double net = deposit - withdrawal;
                                                if (net > 0)
                                                    Console.WriteLine("Surplus");
                                                else
                                                    Console.WriteLine("Deficit");
                                                break;

                                            case 0:
                                                Console.WriteLine("Returning to Account Management...");
                                                break;

                                            default:
                                                Console.WriteLine("Calculation not available.");
                                                break;
                                        }
                                    }
                                    break;

                                case 2:
                                    int accTypeOption = 1;
                                    while (accTypeOption != 0)
                                    {
                                        Console.Write("\n=== ACCOUNT TYPES ===\n\n" +
                                            $"Your account type: {accountType}\n" +
                                            "1) (S) Savings Account\n" +
                                            "2) (C) Current Account\n" +
                                            "3) (F) Fixed Deposit\n" +
                                            "4) Junior Account\n" +
                                            "0) Back\n\n" +
                                            "Select type: "
                                        );
                                        accTypeOption = int.Parse(Console.ReadLine());
                                        switch (accTypeOption)
                                        {
                                            case 1:
                                                Console.WriteLine("Savings Account | Min: 100.000 OMR | Fee: 1.000 OMR/month");
                                                if (accountType.Equals('S'))
                                                {
                                                    Console.WriteLine("*** This is your account type ***");
                                                }
                                                Console.WriteLine("Can be opened at any branch.");
                                                break;

                                            case 2:
                                                Console.WriteLine("Current Account | Min: 300.000 OMR | Fee: 3.000 OMR/month");
                                                if (accountType.Equals('C'))
                                                {
                                                    Console.WriteLine("*** This is your account type ***");
                                                }
                                                Console.WriteLine("Can be opened at any branch.");
                                                break;

                                            case 3:
                                                Console.WriteLine("Fixed Deposit | Min: 600.000 OMR | Fee: 4.500 OMR/month");
                                                if (accountType.Equals('F'))
                                                {
                                                    Console.WriteLine("*** This is your account type ***");
                                                }
                                                Console.WriteLine("Requires manager approval");
                                                break;

                                            case 4:
                                                Console.WriteLine("Junior Account | Min: 50.000 OMR | Fee: 0.100 OMR/month");
                                                Console.WriteLine("Can be opened at any branch.");
                                                break;

                                            case 0:
                                                Console.WriteLine("Returning to Account Management...");
                                                break;

                                            default:
                                                Console.WriteLine("Account type not offered.");
                                                break;
                                        }
                                    }
                                    break;

                                case 3:
                                    int loanOption = 1;
                                    while (loanOption != 0)
                                    {
                                        Console.Write("\n=== LOAN ELIGIBILITY ===\n\n" +
                                            $"Holder: {holderName}  |  Salary: {salary}  |  Score: {creditScore}  |  Age: {age}\n" +
                                            "1) Personal Loan\n" +
                                            "2) Car Loan\n" +
                                            "3) Home Loan\n" +
                                            "0) Back\n\n" +
                                            "Select type: "
                                        );
                                        loanOption = int.Parse(Console.ReadLine());
                                        switch (loanOption)
                                        {
                                            case 1:
                                                if (isEmployed && salary >= 400 && creditScore > 650)
                                                {
                                                    Console.WriteLine("Eligible — application accepted.");
                                                }
                                                else
                                                {
                                                    if (!isEmployed) Console.WriteLine("Not eligible: must be employed.");
                                                    if (salary < 400) Console.WriteLine("Not eligible: salary below 400 OMR.");
                                                    if (creditScore <= 650) Console.WriteLine("Not eligible: credit score below 650.");
                                                }
                                                break;

                                            case 2:
                                                if (isEmployed && salary >= 600 && age >= 21)
                                                {
                                                    Console.WriteLine("Eligible — application accepted.");
                                                }
                                                else
                                                {
                                                    if (!isEmployed) Console.WriteLine("Not eligible: must be employed.");
                                                    if (salary < 600) Console.WriteLine("Not eligible: salary below 600 OMR.");
                                                    if (age < 21) Console.WriteLine("Not eligible: age below 21.");
                                                }
                                                break;

                                            case 3:
                                                if (isEmployed && salary >= 1000 && creditScore > 700 && age >= 25)
                                                {
                                                    Console.WriteLine("Eligible — application accepted.");
                                                }
                                                else
                                                {
                                                    if (!isEmployed) Console.WriteLine("Not eligible: must be employed.");
                                                    if (salary < 1000) Console.WriteLine("Not eligible: salary below 1000 OMR.");
                                                    if (creditScore <= 700) Console.WriteLine("Not eligible: credit score below 700.");
                                                    if (age < 25) Console.WriteLine("Not eligible: age below 25.");
                                                }
                                                break;
                                            
                                            case 0:
                                                Console.WriteLine("Returning to Account Management...");
                                                break;

                                            default:
                                                Console.WriteLine("Loan product not offered.");
                                                break;
                                        }
                                    }
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

                    case 3:
                        int loanSerOption = 1;
                        while (loanSerOption != 0)
                        {
                            Console.Write("\n=== Loan Services ===\n\n" +
                                "1) ATM Transaction\n" +
                                "2) Fee Schedule\n" +
                                "3) Repayment Calculator\n" +
                                "0) Back to Main Menu\n\n" +
                                "Select: "
                            );
                            loanSerOption = int.Parse(Console.ReadLine());
                            switch (loanSerOption)
                            {
                                case 1:
                                    int atmTransOption = 1;
                                    while (atmTransOption != 0)
                                    {
                                        Console.Write("\n=== ATM TRANSACTION ===\n\n" +
                                            $"Holder: {holderName}  |  Balance: {balance:N3} OMR\n" +
                                            "1) Check Balance\n" +
                                            "2) Withdraw\n" +
                                            "3) Deposit\n" +
                                            "0) Back\n\n" +
                                            "Select: "
                                        );
                                        atmTransOption = int.Parse(Console.ReadLine());
                                        switch (atmTransOption)
                                        {
                                            case 1:
                                                Console.WriteLine($"Current balance: {balance:N3} OMR");
                                                break;

                                            case 2:
                                                Console.Write("Enter amount: ");
                                                withdrawal = Convert.ToDouble(Console.ReadLine());
                                                if (withdrawal > MAX_WITHDRAWAL)
                                                {
                                                    Console.WriteLine($"Rejected. Maximum withdrawal amount: {MAX_WITHDRAWAL}");
                                                }
                                                else if(withdrawal <= balance)
                                                {
                                                    balance -= withdrawal;
                                                    Console.WriteLine($"Approved.   New balance: {balance:N3} OMR");
                                                }
                                                else if(balance > 0)
                                                {
                                                    Console.WriteLine($"Available balance {balance:N3}   OMR" +
                                                        $"Insufficient for withdrawal!");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("No funds.");
                                                }
                                                break;

                                            case 3:
                                                Console.Write("Enter amount: ");
                                                deposit = Convert.ToDouble(Console.ReadLine());
                                                balance += deposit;
                                                Console.WriteLine($"New balance: {balance:N3} OMR");
                                                break;

                                            case 0:
                                                Console.WriteLine("Returning to Loan Services...");
                                                break;

                                            default:
                                                Console.WriteLine("Calculation not available.");
                                                break;
                                        }
                                    }
                                    break;

                                case 2:
                                    int transFeeOption = 1;
                                    while (transFeeOption != 0)
                                    {
                                        Console.Write("\n=== TRANSACTION FEE SCHEDULE ===\n\n" +
                                            "1) Wire Transfer\n" +
                                            "2) ATM Withdrawal\n" +
                                            "3) POS Payment\n" +
                                            "4) Online Transfer\n" +
                                            "0) Back\n\n" +
                                            "Select: "
                                        );
                                        transFeeOption = int.Parse(Console.ReadLine());
                                        switch (transFeeOption)
                                        {
                                            case 1:
                                                double vat = WIRE * VAT_RATE;
                                                Console.WriteLine($"Base fee    :   {WIRE:N3} OMR\n" +
                                                    $"VAT (5%)  :   {vat:N3}    OMR\n" +
                                                    $"Total :   {WIRE + vat:N3}\n");
                                                break;

                                            case 2:
                                                vat = ATM_FEE * VAT_RATE;
                                                Console.WriteLine($"Base fee    :   {ATM_FEE:N3} OMR\n" +
                                                    $"VAT (5%)  :   {vat:N3}    OMR\n" +
                                                    $"Total :   {ATM_FEE + vat:N3}\n");
                                                break;

                                            case 3:
                                                vat = POS * VAT_RATE;
                                                Console.WriteLine($"Base fee    :   {POS:N3} OMR\n" +
                                                    $"VAT (5%)  :   {vat:N3}    OMR\n" +
                                                    $"Total :   {POS + vat:N3}\n");
                                                break;

                                            case 4:
                                                vat = ONLINE * VAT_RATE;
                                                Console.WriteLine($"Base fee    :   {ONLINE:N3} OMR\n" +
                                                    $"VAT (5%)  :   {vat:N3}    OMR\n" +
                                                    $"Total :   {ONLINE + vat:N3}\n");
                                                break;

                                            case 0:
                                                Console.WriteLine("Returning to Loan Services...");
                                                break;

                                            default:
                                                Console.WriteLine("Transaction type not listed. Standard fee: 2.000 OMR applies.");
                                                break;
                                        }
                                    }
                                    break;

                                case 3:
                                    int lpcOption = 1;
                                    while (lpcOption != 0)
                                    {
                                        Console.Write("\n=== LOAN REPAYMENT CALCULATOR ===\n\n" +
                                            $"Account balance (ref): {balance:N3} OMR\n" +
                                            "1) Personal Loan\n" +
                                            "2) Car Loan\n" +
                                            "3) Home Loan\n" +
                                            "0) Back\n\n" +
                                            "Select: "
                                        );
                                        lpcOption = int.Parse(Console.ReadLine());
                                        switch (lpcOption)
                                        {
                                            case 1:
                                                Console.Write("Enter loan amount: ");
                                                double loanAmount = Convert.ToDouble(Console.ReadLine());
                                                Console.Write("Enter annual rate: ");
                                                double rate = Convert.ToDouble(Console.ReadLine());
                                                Console.Write("Enter term in months: ");
                                                int months = Convert.ToInt32(Console.ReadLine());

                                                double monthlyRate = rate / 12 / 100;
                                                double monthlyPayment = loanAmount * monthlyRate / (1 - Math.Pow(1 + monthlyRate, -months));
                                                double totalRepayable = monthlyPayment * months;
                                                double totalInterest = totalRepayable - loanAmount;

                                                Console.WriteLine($"Monthly payment : {monthlyPayment:N3} OMR\n" +
                                                    $"Total repayable : {totalRepayable:N3} OMR\n" +
                                                    $"Total interest  : {totalInterest:N3} OMR"
                                                );

                                                if (loanAmount > 10000)
                                                    Console.WriteLine("Requires salary certificate.");
                                                break;

                                            case 2:
                                                Console.Write("Enter loan amount: ");
                                                loanAmount = Convert.ToDouble(Console.ReadLine());
                                                Console.Write("Enter annual rate: ");
                                                rate = Convert.ToDouble(Console.ReadLine());
                                                Console.Write("Enter term in months: ");
                                                months = Convert.ToInt32(Console.ReadLine());

                                                monthlyRate = rate / 12 / 100;
                                                monthlyPayment = loanAmount * monthlyRate / (1 - Math.Pow(1 + monthlyRate, -months));
                                                totalRepayable = monthlyPayment * months;
                                                totalInterest = totalRepayable - loanAmount;

                                                Console.WriteLine($"Monthly payment : {monthlyPayment:N3} OMR\n" +
                                                    $"Total repayable : {totalRepayable:N3} OMR\n" +
                                                    $"Total interest  : {totalInterest:N3} OMR"
                                                );

                                                if (loanAmount > 30000)
                                                    Console.WriteLine("Requires vehicle valuation.");
                                                break;

                                            case 3:
                                                Console.Write("Enter loan amount: ");
                                                loanAmount = Convert.ToDouble(Console.ReadLine());
                                                Console.Write("Enter annual rate: ");
                                                rate = Convert.ToDouble(Console.ReadLine());
                                                Console.Write("Enter term in months: ");
                                                months = Convert.ToInt32(Console.ReadLine());

                                                monthlyRate = rate / 12 / 100;
                                                monthlyPayment = loanAmount * monthlyRate / (1 - Math.Pow(1 + monthlyRate, -months));
                                                totalRepayable = monthlyPayment * months;
                                                totalInterest = totalRepayable - loanAmount;

                                                Console.WriteLine($"Monthly payment : {monthlyPayment:N3} OMR\n" +
                                                    $"Total repayable : {totalRepayable:N3} OMR\n" +
                                                    $"Total interest  : {totalInterest:N3} OMR"
                                                );

                                                if (loanAmount > 100000)
                                                    Console.WriteLine("Requires manager approval.");
                                                break;

                                            case 0:
                                                Console.WriteLine("Returning to Loan Services...");
                                                break;

                                            default:
                                                Console.WriteLine("Transaction type not listed. Standard fee: 2.000 OMR applies.");
                                                break;
                                        }
                                    }
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

                    case 4:
                        int currExOption = 1;
                        while (currExOption != 0)
                        {
                            Console.Write("\n=== Currency Exchange ===\n\n" +
                                "1) Customer Tier\n" +
                                "2) Exchange Currencies\n" +
                                "0) Back to Main Menu\n\n" +
                                "Select: "
                            );
                            currExOption = int.Parse(Console.ReadLine());
                            switch (currExOption)
                            {
                                case 1:
                                    int custmTierOption = 1;
                                    string tier;
                                    if (avgBalance >= 50000)
                                    {
                                        tier = "Platinum";
                                    }
                                    else if (avgBalance >= 10000)
                                    {
                                        tier = "Gold";
                                    }
                                    else if (avgBalance >= 1000)
                                    {
                                        tier = "Silver";
                                    }
                                    else
                                    {
                                        tier = "Standard";
                                    }
                                    while (custmTierOption != 0)
                                    {
                                        Console.Write("\n=== CUSTOMER TIER PORTAL ===\n\n" +
                                            $"Avg Monthly Balance (from setup): {avgBalance:N3} OMR\n" +
                                            $"***   {tier.ToUpper()} MEMBER   ***\n" +
                                            "1) View Benefits\n" +
                                            "2) Top-Up to Next Tier\n" +
                                            "3) Fee Waiver Status\n" +
                                            "0) Back\n\n" +
                                            "Select option: "
                                        );
                                        custmTierOption = int.Parse(Console.ReadLine());
                                        switch (custmTierOption)
                                        {
                                            case 1:
                                                switch (tier)
                                                {
                                                    case "Platinum":
                                                        Console.WriteLine("Platinum Benefits:\n" +
                                                            "- Dedicated relationship manager\n" +
                                                            "- Unlimited free transfers\n" +
                                                            "- Priority customer support 24/7\n" +
                                                            "- Free international card\n" +
                                                            "- Highest exchange rate priority"
                                                        );
                                                        break;
                                                    case "Gold":
                                                        Console.WriteLine("Gold Benefits:\n" +
                                                            "- 5 free transfers per month\n" +
                                                            "- Discounted loan interest rates\n" +
                                                            "- Priority branch service\n" +
                                                            "- Free local card"
                                                        );
                                                        break;
                                                    case "Silver":
                                                        Console.WriteLine("Silver Benefits:\n" +
                                                            "- 2 free transfers per month\n" +
                                                            "- Reduced ATM fees\n" +
                                                            "- Standard loan rates"
                                                        );
                                                        break;
                                                    default:
                                                        Console.WriteLine("Standard Benefits:\n" +
                                                            "- Basic account access\n" +
                                                            "- Standard ATM fees apply\n" +
                                                            "- Standard loan rates"
                                                        );
                                                        break;
                                                }
                                                break;

                                            case 2:
                                                if (avgBalance >= 50000)
                                                {
                                                    Console.WriteLine("Top-Up Required: 0.000 OMR — already at highest tier");
                                                }
                                                else if (avgBalance >= 10000)
                                                {
                                                    Console.WriteLine($"Top-Up to Platinum: {50000 - avgBalance:N3} OMR");
                                                }
                                                else if (avgBalance >= 1000)
                                                {
                                                    Console.WriteLine($"Top-Up to Gold: {10000 - avgBalance:N3} OMR");
                                                }
                                                else
                                                {
                                                    Console.WriteLine($"Top-Up to Silver: {1000 - avgBalance:N3} OMR");
                                                }
                                                break;

                                            case 3:
                                                switch (tier)
                                                {
                                                    case "Platinum":
                                                        Console.WriteLine("Fee Waiver Status: ALL fees waived.");
                                                        break;
                                                    case "Gold":
                                                        Console.WriteLine("Fee Waiver Status: Transfer and ATM fees waived.");
                                                        break;
                                                    case "Silver":
                                                        Console.WriteLine("Fee Waiver Status: ATM fees waived.");
                                                        break;
                                                    default:
                                                        Console.WriteLine("Fee Waiver Status: No waivers — standard fees apply.");
                                                        break;
                                                }
                                                break;

                                            case 0:
                                                Console.WriteLine("Returning to Currency Exchange...");
                                                break;

                                            default:
                                                Console.WriteLine("Calculation not available.");
                                                break;
                                        }
                                    }
                                    break;

                                case 2:
                              
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
