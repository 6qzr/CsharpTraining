using System.Xml.Linq;

namespace MiniFlightManagementSystem
{
    internal class Program
    {

        /*
         * Stored Data
         */

        // 5 passenger names
        static List<string> passengerNames = new List<string> { "Mohammed", "Omar", "Khalid", "Salim", "Naif" };

        // 5 ticket IDs matching passengerNames index
        static List<string> ticketNumbers = new List<string> { "TKT-001", "TKT-002", "TKT-003", "TKT-004", "TKT-005" };

        // Array of 6 available flight codes (e.g. OA101–OA106)
        static string[] flightNumbers = ["OA101", "OA102", "OA103", "OA104", "OA105", "OA106"];

        // 4 available booking dates (dd-MMM-yyyy)
        static List<string> availableDates = new List<string>() { "01-Aug-2026", "20-Aug-2026", "01-Sep-2026", "10-Oct-2026" };

        // Key = ticketNumber, Value = flightNumber+date (e.g.'OA101|12-Jan-2026')
        static Dictionary<string, string> bookingRecord = new Dictionary<string, string>();

        // Passengers who have checked in, awaiting boarding
        static Queue<string> checkedInQueue = new Queue<string>();

        // Passengers boarding the aircraft (last checked-in, first to board)
        static Stack<string> boardingStack = new Stack<string>();

        // Ticket IDs that have been cancelled
        static List<string> cancelledTickets = new List<string>();

        // Key = passengerName, Value = assigned seat (e.g. '14A')
        static Dictionary<string, string> passengerSeatMap = new Dictionary<string, string>();

        // Passenger names on the standby waitlist
        static Queue<string> waitlistQueue = new Queue<string>();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("========================================\r\nSKY WINGS FLIGHT MANAGEMENT SYSTEM\r\n========================================");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("1. Register New Passenger\r\n2. View All Passengers\r\n3. Book a Flight Ticket\r\n4. View Booking Details");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("5. Update a Booking\r\n6. Cancel a Ticket\r\n7. Passenger Check-In\r\n8. Board Passengers (Boarding Stack)");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("9. Generate Flight Manifest\r\n10. Manage Waitlist & Seat Assignment");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("0. Exit");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("========================================\r\nEnter your choice: ");
                Console.ResetColor();

                switch (Console.ReadLine())
                {
                    case "1":
                        RegisterNewPassenger();
                        break;

                    case "2":
                        ViewAllPassengers();
                        break;

                    case "3":
                        BookFlightTicket();
                        break;

                    case "0":
                        return;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n  Invalid option. Press Enter to try again.");
                        Console.ResetColor();
                        Console.ReadLine();
                        break;
                }
            }
        }

        static void RegisterNewPassenger()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("========================================\r\nRegister New Passenger\r\n========================================");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("\nEnter passenger's full name: ");
            Console.ResetColor();

            string passengerFullName = Console.ReadLine().Trim();

            if (string.IsNullOrEmpty(passengerFullName))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n  Passenger's full name cannot be empty. Press Enter.");
                Console.ResetColor();
                Console.ReadLine();
                return;
            }

            for (int i = 0; i < passengerNames.Count; i++)
            {
                if (passengerNames[i].ToLower() == passengerFullName.ToLower())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\n  Error: Passenger '{passengerFullName}' is already registered.");
                    Console.ResetColor();
                    Console.ReadLine();
                    return;
                }
            }

            // Auto-generate the ticket ID using the format TKT-XXX where XXX is the next sequential number padded to 3 digits
            string newTicket = "TKT-" + (ticketNumbers.Count + 1).ToString("D3");

            // Add the passenger name to passengerNames and the generated ticket ID to ticketNumbers at the same index
            passengerNames.Add(passengerFullName);
            ticketNumbers.Add(newTicket);

            Console.ForegroundColor= ConsoleColor.White;
            Console.WriteLine($"\n  Passenger: {passengerFullName}");
            Console.WriteLine($"  Ticket:    {newTicket}");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n  Passenger and Ticket added successfully. Press Enter");
            Console.ResetColor();
            Console.ReadLine();
        }

        static void ViewAllPassengers()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("========================================\r\nView All Passengers\r\n========================================");
            Console.ResetColor();

            if (passengerNames.Count == 0) 
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n  No passengers registered yet. Press Enter.");
                Console.ResetColor();
                Console.ReadLine();
                return;
            }

            Console.WriteLine($"\n{"No.",-5} {"Passenger Name",-20} {"Ticket ID",-12} {"Status"}");
            Console.WriteLine(new string('-', 46));
            
            for (int i = 0; i < passengerNames.Count; i++)
            {
                string status = cancelledTickets.Contains(ticketNumbers[i]) ? "Cancelled" : "Active";
                Console.WriteLine($"{i + 1,-5} {passengerNames[i],-20} {ticketNumbers[i],-12} {status}");
            }

            Console.WriteLine(new string('-', 46));
            Console.WriteLine($"Total Passengers: {passengerNames.Count}");

            Console.ReadLine();
        }

        static void BookFlightTicket()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("========================================\r\nBook A Flight Ticket\r\n========================================");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("\nEnter Ticket ID: ");
            Console.ResetColor();

            string ticketID = Console.ReadLine().Trim().ToUpper();

            if (string.IsNullOrEmpty(ticketID))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n  Error Empty Ticket ID. Press Enter.");
                Console.ResetColor();
                Console.ReadLine();
                return;
            }
            else if (!ticketNumbers.Contains(ticketID))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n  Error: Ticket ID does not exist. Press Enter.");
                Console.ResetColor();
                Console.ReadLine();
                return;
            }
            else if (cancelledTickets.Contains(ticketID))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n  Cannot book with a cancelled ticket. Press Enter.");
                Console.ResetColor();
                Console.ReadLine();
                return;
            }
            else if (bookingRecord.ContainsKey(ticketID))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n   Ticket already has a booking. Press Enter.");
                Console.ResetColor();
                Console.ReadLine();
                return;
            }

            // Available flights
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nAvailable Flights:");
            for (int i = 0; i < flightNumbers.Length; i++)
            {
                Console.WriteLine($"[{i + 1}] {flightNumbers[i]}");
            }
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("\nEnter the index of Flight Number: ");
            Console.ResetColor();

            if (!int.TryParse(Console.ReadLine().Trim(), out int flightNo)) 
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n  Error: Should enter an integer. Press Enter.");
                Console.ResetColor();
                Console.ReadLine();
                return;
            }
            
            if (flightNo < 0 || flightNo > flightNumbers.Length)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n  Error: Flight index out of range. Press Enter.");
                Console.ResetColor();
                Console.ReadLine();
                return;
            }

            // Available dates
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nAvailable Dates:");
            for (int i = 0; i < availableDates.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] {availableDates[i]}");
            }
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("\nEnter the index of flight Date: ");
            Console.ResetColor();

            if (!int.TryParse(Console.ReadLine().Trim(), out int dateNo))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n  Error: Should enter an integer. Press Enter.");
                Console.ResetColor();
                Console.ReadLine();
                return;
            }

            if (dateNo < 0 || dateNo > availableDates.Count)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n  Error: Flight date index out of range. Press Enter.");
                Console.ResetColor();
                Console.ReadLine();
                return;
            }

            // available flights and dates started from index 1 -> decrease by 1
            string flight = flightNumbers[flightNo - 1];
            string date = availableDates[dateNo - 1];

            // Store ate bookingRecord
            bookingRecord[ticketID] = $"{flight}|{date}";

            int passengerIndex = ticketNumbers.IndexOf(ticketID);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n--- Booking Confirmation ---");
            Console.WriteLine($"Ticket ID     : {ticketID}");
            Console.WriteLine($"Passenger     : {passengerNames[passengerIndex]}");
            Console.WriteLine($"Flight        : {flight}");
            Console.WriteLine($"Date          : {date}");
            Console.WriteLine("----------------------------");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n  Flight booked successfully. Press Enter");
            Console.ResetColor();
            Console.ReadLine();
        }
    }
}
