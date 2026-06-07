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
    }
}
