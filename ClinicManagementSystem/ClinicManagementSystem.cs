using System.Net.NetworkInformation;
using System.Numerics;
using System.Threading.Channels;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                                    }
                                    break;

                                //✏️ UPDATE Patient Phone
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

                                //🗑️ DELETE Patient
                                case 4:
                                    if (patientCount == 0)
                                    {
                                        Console.WriteLine("No patients registered.");
                                        break;
                                    }
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

                    case 2:
                        bool dFlag = true;
                        int dChoice;
                        while (dFlag)
                        {
                            Console.Write
                            ("╔══════════════════════════════════════╗\n" +
                             "║          DOCTOR MANAGEMENT           ║\n" +
                             "╠══════════════════════════════════════╣\n" +
                             "║ 1. Add New Doctor                    ║\n" +
                             "║ 2. Display All Doctors               ║\n" +
                             "║ 3. Update Consultation Fee           ║\n" +
                             "║ 4. Delete Doctor                     ║\n" +
                             "║ 0. Back to Main Menu                 ║\n" +
                             "╚══════════════════════════════════════╝\n\n" +
                             "Enter your choice: "
                            );

                            dChoice = Convert.ToInt32(Console.ReadLine());

                            switch (dChoice)
                            {
            // ── Doctor Operations ─────────────────────────────────────
                                //➕ ADD Doctor
                                case 1:
                                    if (doctorCount == MAX_DOCTORS)
                                    {
                                        Console.WriteLine("No available doctor slots.");
                                        break;
                                    }

                                    Console.Write("Enter doctor name: ");
                                    string name = Console.ReadLine();
                                    if (name == "")
                                    {
                                        Console.WriteLine("Empty name not accepted.");
                                        break;
                                    }

                                    Console.Write("Enter doctor specialization: ");
                                    string spec = Console.ReadLine();
                                    if (spec == "")
                                    {
                                        Console.WriteLine("Empty specialization not accepted.");
                                        break;
                                    }

                                    Console.Write("Enter doctor fee: ");
                                    double fee = Convert.ToDouble(Console.ReadLine());
                                    if (fee < 0)
                                    {
                                        Console.WriteLine("Invalid fee amount!");
                                        break;
                                    }

                                    if (!d1Active)
                                    {
                                        d1Name = name; d1Spec = spec; d1Fee = fee;  d1Active = true;
                                        doctorCount++;
                                        Console.WriteLine("Doctor added successfully.");
                                    }
                                    else if (!d2Active)
                                    {
                                        d2Name = name; d2Spec = spec; d2Fee = fee; d2Active = true;
                                        doctorCount++;
                                        Console.WriteLine("Doctor added successfully.");
                                    }
                                    break;

                                //📋 DISPLAY ALL Doctors
                                case 2:
                                    if (doctorCount == 0)
                                    {
                                        Console.WriteLine("No doctors registered.");
                                        break;
                                    }
                                    int displayNum = 1;
                                    if (d1Active)
                                    {
                                        Console.WriteLine($"\nDoctor #{displayNum}\n\n" +
                                            $"Doctor name: {d1Name}\n" +
                                            $"Doctor specialization: {d1Spec}\n" +
                                            $"Fee: {d1Fee}\n");
                                        displayNum++;
                                    }
                                    if (d2Active)
                                    {
                                        Console.WriteLine($"\nDoctor #{displayNum}\n\n" +
                                            $"Doctor name: {d2Name}\n" +
                                            $"Doctor specialization: {d2Spec}\n" +
                                            $"Fee: {d2Fee}\n");
                                    }
                                    break;

                                //✏️ UPDATE Consultation Fee
                                case 3:
                                    Console.Write("Enter doctor name: ");
                                    name = Console.ReadLine();
                                    if (d1Active && d1Name == name)
                                    {
                                        Console.Write("Enter doctor fee: ");
                                        fee = Convert.ToDouble(Console.ReadLine());
                                        d1Fee = fee;
                                        Console.WriteLine("Fee updated.");
                                    }
                                    else if (d2Active && d2Name == name)
                                    {
                                        Console.Write("Enter doctor fee: ");
                                        fee = Convert.ToDouble(Console.ReadLine());
                                        d2Fee = fee;
                                        Console.WriteLine("Fee updated.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Doctor not found.");
                                    }
                                    break;

                                //🗑️ DELETE Doctor
                                case 4:
                                    if (doctorCount == 0)
                                    {
                                        Console.WriteLine("No doctors registered.");
                                        break;
                                    }
                                    Console.Write("Enter doctor name: ");
                                    name = Console.ReadLine();
                                    if (d1Active && d1Name == name)
                                    {
                                        d1Active = false;
                                        d1Name = "";
                                        d1Spec = "";
                                        d1Fee = 0;
                                        doctorCount--;
                                        Console.WriteLine("Doctor removed.");
                                    }
                                    else if (d2Active && d2Name == name)
                                    {
                                        d2Active = false;
                                        d2Name = "";
                                        d2Spec = "";
                                        d2Fee = 0;
                                        doctorCount--;
                                        Console.WriteLine("Doctor removed.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Doctor not found.");
                                    }
                                    break;

                                case 0:
                                    Console.WriteLine("Exiting to Main Menu...");
                                    dFlag = false;
                                    break;

                                default:
                                    Console.WriteLine("Invalid choice!");
                                    break;
                            }
                        }
                        break;

                    case 3:
                        bool aFlag = true;
                        int aChoice;
                        while (aFlag)
                        {
                            Console.Write
                            ("╔══════════════════════════════════════╗\n" +
                             "║       APPOINTMENT MANAGEMENT         ║\n" +
                             "╠══════════════════════════════════════╣\n" +
                             "║ 1. Book New Appointment              ║\n" +
                             "║ 2. Display All Appointments          ║\n" +
                             "║ 3. Update Appointment Status         ║\n" +
                             "║ 4. Cancel Appointment                ║\n" +
                             "║ 0. Back to Main Menu                 ║\n" +
                             "╚══════════════════════════════════════╝\n\n" +
                             "Enter your choice: "
                            );

                            aChoice = Convert.ToInt32(Console.ReadLine());

                            switch (aChoice)
                            {
            // ── Appointment  Operations ─────────────────────────────────────
                                //📅 BOOK Appointment
                                case 1:
                                    if(appointmentCount == MAX_APPOINTMENTS)
                                    {
                                        Console.WriteLine("No available appointment slots.");
                                        break;
                                    }
                                    if(patientCount == 0 || doctorCount == 0)
                                    {
                                        Console.WriteLine("Please add patients and doctors first.");
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
                                    }

                                    //Choose Patient
                                    Console.Write("Enter number of chosen patient: ");
                                    int noPatient = Convert.ToInt32(Console.ReadLine());
                                    string chosenPatient = "";
                                    switch(noPatient)
                                    {
                                        case 1:
                                            if(p1Active)
                                            {
                                                chosenPatient = p1Name;
                                            }
                                            break;

                                        case 2:
                                            if (p1Active)
                                            {
                                                chosenPatient = p1Name;
                                            }
                                            break;

                                        case 3:
                                            if (p1Active)
                                            {
                                                chosenPatient = p1Name;
                                            }
                                            break;

                                        default:
                                            Console.WriteLine("Patient does not exist.");
                                            break;

                                    }
                                    if (chosenPatient == "") break;

                                    //Choose Doctor
                                    displayNum = 1;
                                    if (d1Active)
                                    {
                                        Console.WriteLine($"\nDoctor #{displayNum}\n\n" +
                                            $"Doctor name: {d1Name}\n" +
                                            $"Doctor specialization: {d1Spec}\n" +
                                            $"Fee: {d1Fee}\n");
                                        displayNum++;
                                    }
                                    if (d2Active)
                                    {
                                        Console.WriteLine($"\nDoctor #{displayNum}\n\n" +
                                            $"Doctor name: {d2Name}\n" +
                                            $"Doctor specialization: {d2Spec}\n" +
                                            $"Fee: {d2Fee}\n");
                                    }
                                    
                                    Console.Write("Enter number of chosen doctor: ");
                                    int noDocotr = Convert.ToInt32(Console.ReadLine());
                                    string chosenDoctor = "";
                                    switch (noDocotr)
                                    {
                                        case 1:
                                            if (d1Active)
                                            {
                                                chosenDoctor = d1Name;
                                            }
                                            break;

                                        case 2:
                                            if (d1Active)
                                            {
                                                chosenDoctor = d1Name;
                                            }
                                            break;

                                        default:
                                            Console.WriteLine("Doctor does not exist.");
                                            break;

                                    }
                                    if (chosenDoctor == "") break;

                                    Console.Write("Enter appointment date (format DD/MM/YYYY): ");
                                    string date = Console.ReadLine();

                                    if(a1Active && a1Patient == chosenPatient && a1Doctor == chosenDoctor && a1Date == date)
                                    {
                                        Console.WriteLine("Duplicate appointment.");
                                        break;
                                    }
                                    else if(a2Active && a2Patient == chosenPatient && a2Doctor == chosenDoctor && a2Date == date)
                                    {
                                        Console.WriteLine("Duplicate appointment.");
                                        break;
                                    }
                                    else if (a3Active && a3Patient == chosenPatient && a3Doctor == chosenDoctor && a3Date == date)
                                    {
                                        Console.WriteLine("Duplicate appointment.");
                                        break;
                                    }

                                    if(!a1Active)
                                    {
                                        a1Patient = chosenPatient; a1Doctor = chosenDoctor; a1Date = date; a1Status = "Scheduled"; a1Active = true;
                                    }
                                    else if(!a2Active)
                                    {
                                        a2Patient = chosenPatient; a2Doctor = chosenDoctor; a2Date = date; a2Status = "Scheduled"; a2Active = true;
                                    }
                                    else if(!a3Active)
                                    {
                                        a3Patient = chosenPatient; a3Doctor = chosenDoctor; a3Date = date; a3Status = "Scheduled"; a3Active = true;
                                    }
                                    appointmentCount++;
                                    Console.WriteLine("Appointment booked.");

                                    break;

                                //📋 DISPLAY ALL Appointments
                                case 2:
                                    if(appointmentCount == 0)
                                    {
                                        Console.WriteLine("No appointments booked.");
                                        break;
                                    }
                                    Console.WriteLine("─────────────────────────────────────────────────\r\nAPPOINTMENTS\r\n─────────────────────────────────────────────────");
                                    int appCount = 1;
                                    if (a1Active)
                                    {
                                        Console.WriteLine($"\n---- Appointment #{appCount} ----\n" +
                                            $"Patient: {a1Patient}\n" +
                                            $"Doctor: {a1Doctor}\n" +
                                            $"Date: {a1Date}\n" +
                                            $"Status: {a1Status}\n" +
                                            $"─────────────────────────────────────────────────\r");
                                        appCount++;
                                    }
                                    else if (a2Active)
                                    {
                                        Console.WriteLine($"\n---- Appointment #{appCount} ----\n" +
                                            $"Patient   : {a2Patient}\n" +
                                            $"Doctor    : {a2Doctor}\n" +
                                            $"Date      : {a2Date}\n" +
                                            $"Status    : {a2Status}" +
                                            $"─────────────────────────────────────────────────\r");
                                        appCount++;
                                    }
                                    else
                                    {
                                        Console.WriteLine($"\n---- Appointment #{appCount} ----\n" +
                                            $"Patient: {a3Patient}\n" +
                                            $"Doctor: {a3Doctor}\n" +
                                            $"Date: {a3Date}\n" +
                                            $"Status: {a3Status}" +
                                            $"─────────────────────────────────────────────────\r");
                                    }
                                    break;

                                //✏️ UPDATE Appointment Status
                                case 3:
                                    if (appointmentCount == 0)
                                    {
                                        Console.WriteLine("No active booked.");
                                        break;
                                    }
                                    if (a1Active)
                                    {
                                        Console.WriteLine($"\n---- Appointment NO.1 ----\n" +
                                            $"Patient: {a1Patient}\n" +
                                            $"Doctor: {a1Doctor}\n" +
                                            $"Date: {a1Date}\n" +
                                            $"Status: {a1Status}");
                                    }
                                    else if (a2Active)
                                    {
                                        Console.WriteLine($"\n---- Appointment NO.2 ----\n" +
                                            $"Patient: {a2Patient}\n" +
                                            $"Doctor: {a2Doctor}\n" +
                                            $"Date: {a2Date}\n" +
                                            $"Status: {a2Status}");
                                    }
                                    else
                                    {
                                        Console.WriteLine($"\n---- Appointment NO.3 ----\n" +
                                            $"Patient: {a3Patient}\n" +
                                            $"Doctor: {a3Doctor}\n" +
                                            $"Date: {a3Date}\n" +
                                            $"Status: {a3Status}");
                                    }
                                    Console.Write("Enter the appointment number: ");
                                    int appNo = Convert.ToInt32(Console.ReadLine());
                                    
                                    switch (appNo)
                                    {
                                        case 1:
                                            Console.WriteLine("\nStatus options: 1. Scheduled 2. Completed 3. Cancelled");
                                            Console.Write("Enter status choice: ");
                                            int sChoice = Convert.ToInt32(Console.ReadLine());
                                            switch (sChoice)
                                            {
                                                case 1:
                                                    a1Status = "Scheduled";
                                                    Console.WriteLine("Appointment status updated to Scheduled.");
                                                    break;

                                                case 2:
                                                    a1Status = "Completed";
                                                    Console.WriteLine("Appointment status updated to Completed.");
                                                    break;

                                                case 3:
                                                    a1Status = "Cancelled";
                                                    Console.WriteLine("Appointment status updated to cancelled.");
                                                    break;

                                                default:
                                                    Console.WriteLine("Invalid choice.");
                                                    break;
                                            }
                                            break;

                                        case 2:
                                            Console.WriteLine("\nStatus options: 1. Scheduled 2. Completed 3. Cancelled");
                                            Console.Write("Enter status choice: ");
                                            sChoice = Convert.ToInt32(Console.ReadLine());
                                            switch (sChoice)
                                            {
                                                case 1:
                                                    a2Status = "Scheduled";
                                                    Console.WriteLine("Appointment status updated to Scheduled.");
                                                    break;

                                                case 2:
                                                    a2Status = "Completed";
                                                    Console.WriteLine("Appointment status updated to Completed.");
                                                    break;

                                                case 3:
                                                    a2Status = "Cancelled";
                                                    Console.WriteLine("Appointment status updated to cancelled.");
                                                    break;

                                                default:
                                                    Console.WriteLine("Invalid choice.");
                                                    break;
                                            }
                                            break;

                                        case 3:
                                            Console.WriteLine("\nStatus options: 1. Scheduled 2. Completed 3. Cancelled");
                                            Console.Write("Enter status choice: ");
                                            sChoice = Convert.ToInt32(Console.ReadLine());
                                            switch (sChoice)
                                            {
                                                case 1:
                                                    a3Status = "Scheduled";
                                                    Console.WriteLine("Appointment status updated to Scheduled.");
                                                    break;

                                                case 2:
                                                    a3Status = "Completed";
                                                    Console.WriteLine("Appointment status updated to Completed.");
                                                    break;

                                                case 3:
                                                    a3Status = "Cancelled";
                                                    Console.WriteLine("Appointment status updated to cancelled.");
                                                    break;

                                                default:
                                                    Console.WriteLine("Invalid choice.");
                                                    break;
                                            }
                                            break;

                                        default:
                                            Console.WriteLine("Invalid slot.");
                                            break;
                                    }
                                    break;

                                //🗑️ CANCEL Appointment
                                case 4:
                                    if (appointmentCount == 0)
                                    {
                                        Console.WriteLine("No appointment booked");
                                        break;
                                    }
                                    Console.Write("Enter patient name: ");
                                    string name = Console.ReadLine();
                                    Console.Write("Enter appointment date (format DD/MM/YYYY): ");
                                    date = Console.ReadLine();
                                    if (a1Active && a1Patient == name && a1Date == date)
                                    {
                                        a1Status = "Cancelled";
                                        Console.WriteLine("Appointment status updated to cancelled.");
                                    }
                                    else if (a2Active && a2Patient == name && a2Date == date)
                                    {
                                        a2Status = "Cancelled";
                                        Console.WriteLine("Appointment status updated to cancelled.");
                                    }
                                    else if (a3Active && a3Patient == name && a3Date == date)
                                    {
                                        a3Status = "Cancelled";
                                        Console.WriteLine("Appointment status updated to cancelled.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Appointment not found.");
                                    }
                                    break;

                                case 0:
                                    Console.WriteLine("Exiting to Main Menu...");
                                    aFlag = false;
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
