namespace ClinicManagementSystem
{
    internal class ClinicManagementSystem
    {
        static void Main(string[] args)
        {

            // ── REGION 1 - System Storage ─────────────────────────────────────
            // Capacity constants
            const int MAX_PATIENTS = 3;
            const int MAX_DOCTORS = 2;
            const int MAX_APPOINTMENTS = 3;
            
            // Patient slots
            string p1Name = ""; int p1Age = 0; string p1Phone = ""; bool p1Active = false;
            string p2Name = ""; int p2Age = 0; string p2Phone = ""; bool p2Active = false;
            string p3Name = ""; int p3Age = 0; string p3Phone = ""; bool p3Active = false;
            int patientCount = 0;

            // Doctor slots
            string d1Name = ""; string d1Spec = ""; double d1Fee = 0; bool d1Active = false;
            string d2Name = ""; string d2Spec = ""; double d2Fee = 0; bool d2Active = false;
            int doctorCount = 0;

            // Appointment slots
            string a1Patient = ""; string a1Doctor = ""; string a1Date = ""; string a1Status = ""; bool a1Active = false;
            string a2Patient = ""; string a2Doctor = ""; string a2Date = ""; string a2Status = ""; bool a2Active = false;
            string a3Patient = ""; string a3Doctor = ""; string a3Date = ""; string a3Status = ""; bool a3Active = false;
            int appointmentCount = 0;


            // ── Region 2 — Main Menu ─────────────────────────────────────
            bool flag = true;
            int choice;
            while(flag)
            {
                Console.Clear();
                Console.Write
                ("╔══════════════════════════════════════╗\n" +
                 "║      CLINIC MANAGEMENT SYSTEM        ║\n" +
                 "╠══════════════════════════════════════╣\n" +
                 "║ 1. Patient Management                ║\n" +
                 "║ 2. Doctor Management                 ║\n" +
                 "║ 3. Appointment Management            ║\n" +
                 "║ 0. Exit                              ║\n" +
                 "╚══════════════════════════════════════╝\n\n" +
                 "Enter your choice: "
                );

                choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
            // ── Region 3 — Sub-Menus ─────────────────────────────────────
                    case 1:
                        bool pFlag = true;
                        int pChoice;
                        while (pFlag)
                        {
                            Console.Write
                            ("╔══════════════════════════════════════╗\n" +
                             "║          PATIENT MANAGEMENT          ║\n" +
                             "╠══════════════════════════════════════╣\n" +
                             "║ 1. Add New Patient                   ║\n" +
                             "║ 2. Display All Patients              ║\n" +
                             "║ 3. Update Patient Phone              ║\n" +
                             "║ 4. Delete Patient                    ║\n" +
                             "║ 0. Back to Main Menu                 ║\n" +
                             "╚══════════════════════════════════════╝\n\n" +
                             "Enter your choice: "
                            );

                            pChoice = Convert.ToInt32(Console.ReadLine());

                            switch (pChoice)
                            {
            // ── Patient Operations ─────────────────────────────────────
                                //➕ ADD Patient
                                case 1:
                                    if (patientCount == MAX_PATIENTS)
                                    {
                                        Console.WriteLine("Clinic is full. Cannot add more patients.");
                                        break;
                                    }
                                    
                                    Console.Write("Enter patient name: ");
                                    string name = Console.ReadLine();
                                    if(name == "")
                                    {
                                        Console.WriteLine("Invalid patient name. Cannot be empty");
                                        break;
                                    }

                                    Console.Write("Enter patient age: ");
                                    int age = Convert.ToInt32(Console.ReadLine());
                                    if (age < 1 || age > 120)
                                    {
                                        Console.WriteLine("Invalid patient age. Must be between 1 and 120");
                                        break;
                                    }

                                    Console.Write("Enter patient phone: ");
                                    string phone = Console.ReadLine();

                                    if(patientCount < MAX_PATIENTS)
                                    {
                                        if (!p1Active)
                                        {
                                            p1Name = name; p1Age = age; p1Phone = phone; p1Active = true;
                                            patientCount++;
                                            Console.WriteLine("Patient added successfully.");
                                        }
                                        else if (!p2Active)
                                        {
                                            p2Name = name; p2Age = age; p2Phone = phone; p2Active = true;
                                            patientCount++;
                                            Console.WriteLine("Patient added successfully.");
                                        }
                                        else if (!p3Active)
                                        {
                                            p3Name = name; p3Age = age; p3Phone = phone; p3Active = true;
                                            patientCount++;
                                            Console.WriteLine("Patient added successfully.");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Max patient Reached. No slots available");
                                    }
                                    break;

                                //📋 DISPLAY ALL Patients
                                case 2:
                                    if (patientCount == 0)
                                    {
                                        Console.WriteLine("No patients registered.");
                                        break;
                                    }
                                    int displayNum = 1;
                                    if (p1Active)
                                    {
                                        Console.WriteLine($"\nPatient #{displayNum}\n\n" +
                                            $"Patient name: {p1Name}\n" +
                                            $"Patient age: {p1Age}\n" +
                                            $"Patient phone: {p1Phone}\n");
                                        displayNum++;
                                    }
                                    if (p2Active)
                                    {
                                        Console.WriteLine($"\nPatient #{displayNum}\n\n" +
                                            $"Patient name: {p2Name}\n" +
                                            $"Patient age: {p2Age}\n" +
                                            $"Patient phone: {p2Phone}\n");
                                        displayNum++;
                                    }
                                    if (p3Active)
                                    {
                                        Console.WriteLine($"\nPatient #{displayNum}\n\n" +
                                            $"Patient name: {p3Name}\n" +
                                            $"Patient age: {p3Age}\n" +
                                            $"Patient phone: {p3Phone}\n");
                                        displayNum++;
                                    }
                                    break;

                                case 3:
                                    Console.Write("Enter patient name: ");
                                    name = Console.ReadLine();
                                    if(p1Active && p1Name == name)
                                    {
                                        Console.Write("Enter new patient phone: ");
                                        string newPhone = Console.ReadLine();
                                        p1Phone = newPhone;
                                        Console.WriteLine("Updated.");
                                    }
                                    else if (p2Active && p2Name == name)
                                    {
                                        Console.Write("Enter new patient phone: ");
                                        string newPhone = Console.ReadLine();
                                        p2Phone = newPhone;
                                        Console.WriteLine("Updated.");
                                    }
                                    else if (p3Active && p3Name == name)
                                    {
                                        Console.Write("Enter new patient phone: ");
                                        string newPhone = Console.ReadLine();
                                        p3Phone = newPhone;
                                        Console.WriteLine("Updated.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Patient not found.");
                                    }
                                    break;

                                case 4:
                                    Console.Write("Enter patient name: ");
                                    name = Console.ReadLine();
                                    if (p1Active && p1Name == name)
                                    {
                                        p1Active = false;
                                        p1Name = "";
                                        p1Age = 0;
                                        p1Phone = "";
                                        patientCount--;
                                        Console.WriteLine("Patient deleted.");
                                    }
                                    else if (p2Active && p2Name == name)
                                    {
                                        p2Active = false;
                                        p2Name = "";
                                        p2Age = 0;
                                        p2Phone = "";
                                        patientCount--;
                                        Console.WriteLine("Patient deleted.");
                                    }
                                    else if (p3Active && p3Name == name)
                                    {
                                        p3Active = false;
                                        p3Name = "";
                                        p3Age = 0;
                                        p3Phone = "";
                                        patientCount--;
                                        Console.WriteLine("Patient deleted.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Patient not found.");
                                    }
                                    break;

                                case 0:
                                    Console.WriteLine("Exiting to Main Menu...");
                                    pFlag = false;
                                    break;

                                default:
                                    Console.WriteLine("Invalid choice!");
                                    break;
                            }
                        }
                        break;

                    

                    case 0:
                        Console.WriteLine("Exiting CMS...");
                        flag = false;
                        break;

                    default:
                        Console.WriteLine("Invalid choice!");
                        break;
                }
            }
        }
    }
}
