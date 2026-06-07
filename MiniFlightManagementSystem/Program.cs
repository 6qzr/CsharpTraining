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

                    case "4":
                        ViewBookingDetails();
                        break;

                    case "5":
                        UpdateBooking();
                        break;

                    case "0":
                        CancelTicket();
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

        static string GetTicketID()
        {
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
                return "";
            }
            else if (!ticketNumbers.Contains(ticketID))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n  Error: Ticket ID does not exist. Press Enter.");
                Console.ResetColor();
                Console.ReadLine();
                return "";
            }
            else if (cancelledTickets.Contains(ticketID))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n  Cannot book with a cancelled ticket. Press Enter.");
                Console.ResetColor();
                Console.ReadLine();
                return "";
            }

            return ticketID;
        }
        
        static int GetAvailableFlights()
        {
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
                return 0;
            }

            if (flightNo <= 0 || flightNo > flightNumbers.Length)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n  Error: Flight index out of range. Press Enter.");
                Console.ResetColor();
                Console.ReadLine();
                return 0;
            }

            return flightNo;
        }

        static int GetAvailableDates()
        {
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
                return 0;
            }

            if (dateNo <= 0 || dateNo > availableDates.Count)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n  Error: Flight date index out of range. Press Enter.");
                Console.ResetColor();
                Console.ReadLine();
                return 0;
            }

            return dateNo;
        }
        
        static void BookFlightTicket()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("========================================\r\nBook A Flight Ticket\r\n========================================");
            Console.ResetColor();

            string ticketID = GetTicketID();
            if (ticketID == "") return;

            if (bookingRecord.ContainsKey(ticketID))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n  Ticket already has a booking. Press Enter.");
                Console.ResetColor();
                Console.ReadLine();
                return;
            }

            int flightNo = GetAvailableFlights();
            if (flightNo == 0) return;

            int dateNo = GetAvailableDates();
            if (dateNo == 0) return;

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
            Console.WriteLine($"\n  Flight booked successfully. Press Enter.");
            Console.ResetColor();
            Console.ReadLine();
        }

        static void ViewBookingDetails()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("========================================\r\nView Booking Details\r\n========================================");
            Console.ResetColor();

            string ticketID = GetTicketID();
            if (ticketID == "") return;

            if (!bookingRecord.ContainsKey(ticketID))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n  No booking found for this ticket. Press Enter.");
                Console.ResetColor();
                Console.ReadLine();
                return;
            }

            string[] bookingParts = bookingRecord[ticketID].Split('|');
            string flight = bookingParts[0];
            string date = bookingParts[1];

            int passengerIndex = ticketNumbers.IndexOf(ticketID);

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n--- Booking Details ---");
            Console.WriteLine($"Passenger     : {passengerNames[passengerIndex]}");
            Console.WriteLine($"Ticket ID     : {ticketID}");
            Console.WriteLine($"Flight        : {flight}");
            Console.WriteLine($"Date          : {date}");
            Console.WriteLine("-----------------------");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n  Press Enter");
            Console.ResetColor();
            Console.ReadLine();
        }

        static void UpdateBooking()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("========================================\r\nUpdate A Booking\r\n========================================");
            Console.ResetColor();

            string ticketID = GetTicketID();
            if (ticketID == "") return;
            
            if (!bookingRecord.ContainsKey(ticketID))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n  No booking found for this ticket. Press Enter.");
                Console.ResetColor();
                Console.ReadLine();
                return;
            }

            string[] bookingParts = bookingRecord[ticketID].Split('|');
            string flight = bookingParts[0];
            string date = bookingParts[1];

            int passengerIndex = ticketNumbers.IndexOf(ticketID);

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n--- Booking Details ---");
            Console.WriteLine($"Passenger     : {passengerNames[passengerIndex]}");
            Console.WriteLine($"Ticket ID     : {ticketID}");
            Console.WriteLine($"Flight        : {flight}");
            Console.WriteLine($"Date          : {date}");
            Console.WriteLine("-----------------------");
            Console.ResetColor();

            // Display sub-menu
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n(1) Change flight only\n(2) Change date only\n(3) Change both\n(0) Cancel updat");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("\nSelect option: ");
            Console.ResetColor();

            // Save old values before switch
            string oldFlight = flight;
            string oldDate = date;

            int flightNo;
            int dateNo;
            string newFlight;
            string newDate;

            switch (Console.ReadLine())
            {
                case "1":
                    flightNo = GetAvailableFlights();
                    if (flightNo == 0) return;

                    newFlight = flightNumbers[flightNo - 1];

                    if (newFlight == oldFlight)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n  Cannot Update to the same flight. Press Enter.");
                        Console.ResetColor();
                        Console.ReadLine();
                        return;
                    }

                    // Update booking record
                    bookingRecord[ticketID] = $"{newFlight}|{date}";

                    Console.WriteLine("\n--- Update Confirmation ---");
                    Console.WriteLine($"{"Field",-10} {"Old",-10} {"New"}");
                    Console.WriteLine(new string('-', 35));
                    Console.WriteLine($"{"Flight",-10} {oldFlight,-10} {newFlight}");
                    Console.WriteLine($"{"Date",-10} {oldDate,-10} {date}");
                    Console.WriteLine(new string('-', 35));
                    Console.ReadLine();
                    break;

                case "2":
                    dateNo = GetAvailableDates();
                    if (dateNo == 0) return;

                    newDate = availableDates[dateNo - 1];

                    if (newDate == oldDate)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n  Cannot Update to the same date. Press Enter.");
                        Console.ResetColor();
                        Console.ReadLine();
                        return;
                    }

                    // Update booking record
                    bookingRecord[ticketID] = $"{flight}|{newDate}";

                    Console.WriteLine("\n--- Update Confirmation ---");
                    Console.WriteLine($"{"Field",-10} {"Old",-10} {"New"}");
                    Console.WriteLine(new string('-', 35));
                    Console.WriteLine($"{"Flight",-10} {oldFlight,-10} {flight}");
                    Console.WriteLine($"{"Date",-10} {oldDate,-10} {newDate}");
                    Console.WriteLine(new string('-', 35));
                    Console.ReadLine();
                    break;

                case "3":
                    flightNo = GetAvailableFlights();
                    if (flightNo == 0) return;

                    newFlight = flightNumbers[flightNo - 1];

                    if (newFlight == oldFlight)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n  Cannot Update to the same flight. Press Enter.");
                        Console.ResetColor();
                        Console.ReadLine();
                        return;
                    }

                    dateNo = GetAvailableDates();
                    if (dateNo == 0) return;

                    newDate = availableDates[dateNo - 1];

                    if (newDate == oldDate)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n  Cannot Update to the same date. Press Enter.");
                        Console.ResetColor();
                        Console.ReadLine();
                        return;
                    }
                    
                    // Update booking record
                    bookingRecord[ticketID] = $"{newFlight}|{newDate}";

                    Console.WriteLine("\n--- Update Confirmation ---");
                    Console.WriteLine($"{"Field",-10} {"Old",-10} {"New"}");
                    Console.WriteLine(new string('-', 35));
                    Console.WriteLine($"{"Flight",-10} {oldFlight,-10} {newFlight}");
                    Console.WriteLine($"{"Date",-10} {oldDate,-10} {newDate}");
                    Console.WriteLine(new string('-', 35));
                    Console.ReadLine();
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

        static void CancelTicket()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("========================================\r\nCancel A Ticket\r\n========================================");
            Console.ResetColor();

            string ticketID = GetTicketID();
            if (ticketID == "") return;

            if (cancelledTickets.Contains(ticketID))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n  Ticket is already cancelled. Press Enter.");
                Console.ResetColor();
                Console.ReadLine();
                return;
            }

            string passengerName = passengerNames[ticketNumbers.IndexOf(ticketID)];

            if (bookingRecord.ContainsKey(ticketID))
            {
                string[] bookingParts = bookingRecord[ticketID].Split('|');
                string flight = bookingParts[0];
                string date = bookingParts[1];

                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\n--- Booking Removed ---");
                Console.WriteLine($"Passenger     : {passengerName}");
                Console.WriteLine($"Ticket ID     : {ticketID}");
                Console.WriteLine($"Flight        : {flight}");
                Console.WriteLine($"Date          : {date}");
                Console.WriteLine("-----------------------");
                Console.ResetColor();

                bookingRecord.Remove(ticketID);
            }

            cancelledTickets.Add(ticketID);

            // Temp Queue
            Queue<string> tempQ = new Queue<string>();
            bool removedFromQueue = false;

            foreach (string passenger in checkedInQueue)
            {
                if (passenger == passengerName)
                {
                    removedFromQueue = true; // skip this one
                }
                else
                {
                    tempQ.Enqueue(passenger);
                }
            }

            if (removedFromQueue)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\n  Notice: Passenger was removed from Check-In Queue.");
                Console.ResetColor();
            }

            checkedInQueue = new Queue<string>(tempQ);


            // Temp Stack
            Stack<string> tempS = new Stack<string>();
            bool removedFromStack = false;

            int stackCount = boardingStack.Count; // capture once

            for (int i = 0; i < stackCount; i++)
            {
                if (boardingStack.Peek() != passengerName)
                {
                    tempS.Push(boardingStack.Pop());
                }
                else
                {
                    removedFromStack = true; // skip this one
                    boardingStack.Pop();
                }
            }

            if (removedFromStack)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\n  Notice: Passenger was removed from Boarding Stack.");
                Console.ResetColor();
            }

            // Reverse it back to original order
            while (tempS.Count > 0)
            {
                boardingStack.Push(tempS.Pop());
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n--- Cancellation Summary ---");
            Console.WriteLine($"Passenger     : {passengerName}");
            Console.WriteLine($"Ticket ID     : {ticketID}");
            Console.WriteLine($"Status        : CANCELLED");
            Console.WriteLine("----------------------------");
            Console.ResetColor();
            Console.ReadLine();
        }
    }
}
