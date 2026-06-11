using System.Linq.Expressions;

namespace FlightManagementSystem
{
    internal class Program
    {
        
        /*
        * Stored Data
        */

        // 5 passenger names
        static List<string> passengerNames = new List<string>();

        // 5 ticket IDs matching passengerNames index
        static List<string> ticketNumbers = new List<string>();

        // Array of 6 available flight codes (e.g. OA101–OA106)
        static string[] flightNumbers = ["OA101", "OA102", "OA103", "OA104", "OA105", "OA106"];

        // 4 available booking dates (dd-MMM-yyyy)
        static List<string> availableDates = new List<string>();

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

        static string BaseDir = Path.GetFullPath(Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Data"));

        static string PassengersFile = Path.Combine(BaseDir, "Passengers.csv");
        static string TicketsFile = Path.Combine(BaseDir, "Tickets.csv");
        static string AvailableDatesFile = Path.Combine(BaseDir, "AvailableDates.csv");
        static string BookingsFile = Path.Combine(BaseDir, "Bookings.csv");
        static string CheckedInQueueFile = Path.Combine(BaseDir, "CheckedInQueue.csv");
        static string BoardingStackFile = Path.Combine(BaseDir, "BoardingStack.csv");
        static string CancelledTicketsFile = Path.Combine(BaseDir, "CancelledTickets.csv");
        static string SeatMapFile = Path.Combine(BaseDir, "SeatMap.csv");
        static string WaitlistQueueFile = Path.Combine(BaseDir, "WaitlistQueue.csv");


        static int boardingRow = 10;
        static int boardingSeat = 0; // 0=A, 1=B, 2=C, 3=D, 4=E, 5=F

        static void Main(string[] args)
        {
            // Load Data
            LoadData();
            
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

                    case "6":
                        CancelTicket();
                        break;

                    case "7":
                        PassengerCheckIn();
                        break;

                    case "8":
                        BoardPassengers();
                        break;

                    case "9":
                        GenerateFlightManifest();
                        break;

                    case "10":
                        ManageWaitlistSeatAssignment();
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

        

        static void LoadData()
        {
            LoadPassengers();
            LoadTickets();
            LoadDates();
            LoadBookings();
            LoadCancelledTickets();
            LoadCheckedInQueue();
            LoadBoardingStack();
            LoadSeatMap();
            LoadWaitlistQueue();
        }

        static void LoadPassengers()
        {
            try
            {
                if (!File.Exists(PassengersFile)) return;

                using (StreamReader reader = new StreamReader(PassengersFile))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] passengers = line.Split(",");
                        foreach (string passenger in passengers)
                        {
                            passengerNames.Add(passenger.Trim());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nAn unexpected error occurred:");
                Console.WriteLine(ex.Message);
                Console.ResetColor();

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
        }

        static void LoadTickets()
        {
            try
            {
                if (!File.Exists(TicketsFile)) return;

                using (StreamReader reader = new StreamReader(TicketsFile))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] tickets = line.Split(",");
                        foreach (string ticket in tickets)
                        {
                            ticketNumbers.Add(ticket.Trim());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nAn unexpected error occurred:");
                Console.WriteLine(ex.Message);
                Console.ResetColor();

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
        }

        static void LoadDates()
        {
            try
            {
                if (!File.Exists(AvailableDatesFile)) return;

                using (StreamReader reader = new StreamReader(AvailableDatesFile))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] dates = line.Split(",");
                        foreach (string date in dates)
                        {
                            availableDates.Add(date);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nAn unexpected error occurred:");
                Console.WriteLine(ex.Message);
                Console.ResetColor();

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
        }

        static void LoadBookings()
        {
            try
            {
                if (!File.Exists(BookingsFile)) return;

                using (StreamReader reader = new StreamReader(BookingsFile))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] bookings = line.Split(",");
                        foreach (string booking in bookings)
                        {
                            string[] record = booking.Split("|");
                            string ticketNumber = record[0].Trim();
                            string flighNumber = record[1].Trim();
                            string date = record[2].Trim();
                            bookingRecord[ticketNumber] = $"{flighNumber}|{date}";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nAn unexpected error occurred:");
                Console.WriteLine(ex.Message);
                Console.ResetColor();

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
        }

        static void LoadCancelledTickets()
        {
            try
            {
                if (!File.Exists(CancelledTicketsFile)) return;

                using (StreamReader reader = new StreamReader(CancelledTicketsFile))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] tickets = line.Split(",");
                        foreach (string ticket in tickets)
                        {
                            cancelledTickets.Add(ticket);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nAn unexpected error occurred:");
                Console.WriteLine(ex.Message);
                Console.ResetColor();

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
        }

        static void LoadCheckedInQueue()
        {
            try
            {
                if (!File.Exists(CheckedInQueueFile)) return;

                using (StreamReader reader = new StreamReader(CheckedInQueueFile))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] passengers = line.Split(",");
                        foreach (string passenger in passengers)
                        {
                            checkedInQueue.Enqueue(passenger);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nAn unexpected error occurred:");
                Console.WriteLine(ex.Message);
                Console.ResetColor();

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
        }

        static void LoadBoardingStack()
        {
            try
            {
                if (!File.Exists(BoardingStackFile)) return;

                using (StreamReader reader = new StreamReader(BoardingStackFile))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] passengers = line.Split(",");
                        foreach (string passenger in passengers)
                        {
                            boardingStack.Push(passenger);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nAn unexpected error occurred:");
                Console.WriteLine(ex.Message);
                Console.ResetColor();

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
        }

        static void LoadSeatMap()
        {
            try
            {
                if (!File.Exists(SeatMapFile)) return;

                using (StreamReader reader = new StreamReader(SeatMapFile))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] seatMaps = line.Split(",");
                        foreach (string seatMap in seatMaps)
                        {
                            string[] record = seatMap.Split("|");
                            string passengerName = record[0].Trim();
                            string assignedSeat = record[1].Trim();
                            passengerSeatMap[passengerName] = assignedSeat;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nAn unexpected error occurred:");
                Console.WriteLine(ex.Message);
                Console.ResetColor();

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
        }

        static void LoadWaitlistQueue()
        {
            try
            {
                if (!File.Exists(WaitlistQueueFile)) return;

                using (StreamReader reader = new StreamReader(WaitlistQueueFile))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] passengers = line.Split(",");
                        foreach (string passenger in passengers)
                        {
                            waitlistQueue.Enqueue(passenger);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nAn unexpected error occurred:");
                Console.WriteLine(ex.Message);
                Console.ResetColor();

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
        }



        static void SaveData()
        {
            SavePassengers();
            SaveTickets();
            SaveDates();
            SaveBookings();
            SaveCancelledTickets();
            SaveCheckedInQueue();
            SaveBoardingStack();
            SaveSeatMap();
            SaveWaitlistQueue();
        }

        static void SavePassengers()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(PassengersFile))
                {
                    foreach (string passenger in passengerNames)
                    {
                        writer.WriteLine(passenger);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n  Error saving passengers: {ex.Message}");
                Console.ResetColor();
            }
        }

        static void SaveTickets()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(TicketsFile))
                {
                    foreach (string ticket in ticketNumbers)
                    {
                        writer.WriteLine(ticket);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n  Error saving tickets: {ex.Message}");
                Console.ResetColor();
            }
        }

        static void SaveDates()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(AvailableDatesFile))
                {
                    foreach (string date in availableDates)
                    {
                        writer.WriteLine(date);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n  Error saving dates: {ex.Message}");
                Console.ResetColor();
            }
        }

        static void SaveBookings()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(BookingsFile))
                {
                    foreach (KeyValuePair<string, string> record in bookingRecord)
                    {
                        // Format: ticketID|flight|date
                        string[] parts = record.Value.Split('|');
                        writer.WriteLine($"{record.Key}|{parts[0]}|{parts[1]}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n  Error saving bookings: {ex.Message}");
                Console.ResetColor();
            }
        }

        static void SaveCancelledTickets()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(CancelledTicketsFile))
                {
                    foreach (string ticket in cancelledTickets)
                    {
                        writer.WriteLine(ticket);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n  Error saving cancelled tickets: {ex.Message}");
                Console.ResetColor();
            }
        }

        static void SaveCheckedInQueue()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(CheckedInQueueFile))
                {
                    foreach (string passenger in checkedInQueue)
                    {
                        writer.WriteLine(passenger);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n  Error saving check-in queue: {ex.Message}");
                Console.ResetColor();
            }
        }

        static void SaveBoardingStack()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(BoardingStackFile))
                {
                    foreach (string passenger in boardingStack)
                    {
                        writer.WriteLine(passenger);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n  Error saving boarding stack: {ex.Message}");
                Console.ResetColor();
            }
        }

        static void SaveSeatMap()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(SeatMapFile))
                {
                    foreach (KeyValuePair<string, string> record in passengerSeatMap)
                    {
                        writer.WriteLine($"{record.Key}|{record.Value}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n  Error saving seat map: {ex.Message}");
                Console.ResetColor();
            }
        }

        static void SaveWaitlistQueue()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(WaitlistQueueFile))
                {
                    foreach (string passenger in waitlistQueue)
                    {
                        writer.WriteLine(passenger);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n  Error saving waitlist: {ex.Message}");
                Console.ResetColor();
            }
        }


        static void RegisterNewPassenger()
        {
            try
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

                //for (int i = 0; i < passengerNames.Count; i++)
                //{
                //    if (passengerNames[i].ToLower() == passengerFullName.ToLower())
                //    {
                //        Console.ForegroundColor = ConsoleColor.Red;
                //        Console.WriteLine($"\n  Error: Passenger '{passengerFullName}' is already registered.");
                //        Console.ResetColor();
                //        Console.ReadLine();
                //        return;
                //    }
                //}

                bool isPassengerRegistered = passengerNames.Any(p => p.ToLower() == passengerFullName.ToLower());
                if (isPassengerRegistered)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\n  Error: Passenger '{passengerFullName}' is already registered.");
                    Console.ResetColor();
                    Console.ReadLine();
                    return;
                }

                // Auto-generate the ticket ID using the format TKT-XXX where XXX is the next sequential number padded to 3 digits
                string newTicket = "TKT-" + (ticketNumbers.Count + 1).ToString("D3");

                // Add the passenger name to passengerNames and the generated ticket ID to ticketNumbers at the same index
                passengerNames.Add(passengerFullName);
                ticketNumbers.Add(newTicket);

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"\n  Passenger: {passengerFullName}");
                Console.WriteLine($"  Ticket:    {newTicket}");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n  Passenger and Ticket added successfully. Press Enter");
                Console.ResetColor();
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nAn unexpected error occurred:");
                Console.WriteLine(ex.Message);
                Console.ResetColor();

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
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
            try
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("\nEnter Ticket ID: ");
                Console.ResetColor();

                string? ticketID = Console.ReadLine()?.Trim().ToUpper();

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
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nAn unexpected error occurred:");
                Console.WriteLine(ex.Message);
                Console.ResetColor();

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
                return "";
            }
        }

        static int GetAvailableFlights()
        {
            try
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

                if (!int.TryParse(Console.ReadLine()?.Trim(), out int flightNo))
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
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nAn unexpected error occurred:");
                Console.WriteLine(ex.Message);
                Console.ResetColor();

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
                return 0;
            }
        }

        static int GetAvailableDates()
        {
            try
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

                if (!int.TryParse(Console.ReadLine()?.Trim(), out int dateNo))
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
            catch (Exception ex) 
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nAn unexpected error occurred:");
                Console.WriteLine(ex.Message);
                Console.ResetColor();

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
                return 0;
            }
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
            Console.WriteLine($"\n  Booked successfully. Press Enter.");
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

            bool removed = false;

            while (checkedInQueue.Count > 0)
            {
                string currentPassenger = checkedInQueue.Dequeue();

                if (currentPassenger == passengerName)
                {
                    removed = true;
                    continue; // skip adding this passenger back
                }

                tempQ.Enqueue(currentPassenger);
            }

            while (tempQ.Count > 0)
            {
                checkedInQueue.Enqueue(tempQ.Dequeue());
            }

            if (removed)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\n  Notice: Passenger was removed from Check-In Queue.");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("\n  Passenger not found.");
            }

            // Temp Stack
            Stack<string> tempS = new Stack<string>();

            removed = false;

            while (boardingStack.Count > 0)
            {
                string currentPassenger = boardingStack.Pop();

                if (currentPassenger == passengerName)
                {
                    removed = true;
                    continue; // don't put it back
                }

                tempS.Push(currentPassenger);
            }

            // Restore original order
            while (tempS.Count > 0)
            {
                boardingStack.Push(tempS.Pop());
            }

            if (removed)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\n  Notice: Passenger was removed from Boarding Stack.");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("\n  Passenger not found.");
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

        static void PassengerCheckIn()
        {
            try
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("========================================\r\nPassenger Check-In\r\n========================================");
                Console.ResetColor();

                // Display sub-menu
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n(1) Check in a passenger\n(2) View check-in queue\n(3) Process next passenger\n(0) Back");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("\nSelect option: ");
                Console.ResetColor();

                string passenger;

                switch (Console.ReadLine())
                {
                    case "1":
                        string ticketID = GetTicketID();
                        if (ticketID == "") return;

                        if (cancelledTickets.Contains(ticketID))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n  This ticket is cancelled. Press Enter.");
                            Console.ResetColor();
                            Console.ReadLine();
                            return;
                        }

                        if (!bookingRecord.ContainsKey(ticketID))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n  No booking found for this ticket. Press Enter.");
                            Console.ResetColor();
                            Console.ReadLine();
                            return;
                        }

                        passenger = passengerNames[ticketNumbers.IndexOf(ticketID)];

                        if (checkedInQueue.Contains(passenger))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n  Passenger already Checked-In. Press Enter.");
                            Console.ResetColor();
                            Console.ReadLine();
                            return;
                        }

                        if (checkedInQueue.Count < 10)
                        {
                            checkedInQueue.Enqueue(passenger);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"\n  Passenger added to Check-In Queue. Press Enter");
                            Console.ResetColor();
                            Console.ReadLine();
                        }
                        else
                        {
                            waitlistQueue.Enqueue(passenger);
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"\n  Passenger added to Wait-List Queue. Press Enter");
                            Console.ResetColor();
                            Console.ReadLine();
                        }

                        break;

                    case "2":
                        int counter = 1;
                        Console.ForegroundColor = ConsoleColor.White;
                        foreach (string currentPassenger in checkedInQueue.ToList())
                        {
                            Console.Write($"\n{counter}. {currentPassenger}");
                            counter++;
                        }
                        Console.WriteLine($"\n\nPassengers in the waitlist: {waitlistQueue.Count}");
                        Console.ResetColor();
                        Console.ReadLine();
                        break;

                    case "3":
                        if (checkedInQueue.Count == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n  No passengers in the Checked-In Queue. Press Enter.");
                            Console.ResetColor();
                            Console.ReadLine();
                            return;
                        }

                        passenger = checkedInQueue.Dequeue();

                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($"\n{passenger}   PROCESSED");
                        Console.ResetColor();

                        if (waitlistQueue.Count != 0)
                        {
                            checkedInQueue.Enqueue(waitlistQueue.Dequeue());
                        }
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
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nAn unexpected error occurred:");
                Console.WriteLine(ex.Message);
                Console.ResetColor();

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
        }

        static void BoardPassengers()
        {
            try
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("========================================\r\nBoard Passengers\r\n========================================");
                Console.ResetColor();

                // Display sub-menu
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n(1) Load boarding stack from check-in queue\n(2) Board next passenger\n(3) View boarding stack\n(4) View boarding log\n(0) Back");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("\nSelect option: ");
                Console.ResetColor();

                switch (Console.ReadLine())
                {
                    case "1":
                        if (checkedInQueue.Count == 0 && boardingStack.Count != 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n  Warning: Checked-In queue is empty and boarding stack has passengers. Press Enter.");
                            Console.ResetColor();
                            Console.ReadLine();
                            return;
                        }
                        else if (checkedInQueue.Count == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n Checked-In queue is empty. Press Enter.");
                            Console.ResetColor();
                            Console.ReadLine();
                            return;
                        }

                        if (checkedInQueue.Count != 0)
                        {
                            int loaded = checkedInQueue.Count;
                            while (checkedInQueue.Count > 0)
                            {
                                boardingStack.Push(checkedInQueue.Dequeue());
                            }
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"\nTotal loaded passengers: {loaded}");
                            Console.ResetColor();
                            Console.ReadLine();
                        }
                        break;

                    case "2":
                        if (boardingStack.Count == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n  boarding stack is empty. Press Enter.");
                            Console.ResetColor();
                            Console.ReadLine();
                            return;
                        }

                        string boardedPassenger = boardingStack.Pop();

                        char seatLetter = (char)('A' + boardingSeat);
                        string assignedSeat = $"{boardingRow}{seatLetter}";

                        passengerSeatMap[boardedPassenger] = assignedSeat;

                        boardingSeat++;
                        if (boardingSeat > 5)
                        {
                            boardingSeat = 0;
                            boardingRow++;
                        }

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"\n  Passenger : {boardedPassenger}");
                        Console.WriteLine($"  Seat      : {assignedSeat}");
                        Console.ResetColor();
                        Console.ReadLine();
                        break;

                    case "3":
                        if (boardingStack.Count == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n  Boarding stack is empty. Press Enter.");
                            Console.ResetColor();
                            Console.ReadLine();
                            return;
                        }

                        Console.WriteLine($"\n{"Pos",-5} {"Passenger"}");
                        Console.WriteLine(new string('-', 25));
                        int pos = 1;
                        foreach (string p in boardingStack.ToList())
                        {
                            Console.WriteLine($"{pos,-5} {p}");
                            pos++;
                        }
                        Console.ReadLine();
                        break;

                    case "4":
                        if (passengerSeatMap.Count == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n  No passengers boarded yet. Press Enter.");
                            Console.ResetColor();
                            Console.ReadLine();
                            return;
                        }

                        Console.WriteLine($"\n{"Passenger",-20} {"Seat"}");
                        Console.WriteLine(new string('-', 30));
                        foreach (KeyValuePair<string, string> entry in passengerSeatMap)
                        {
                            Console.WriteLine($"{entry.Key,-20} {entry.Value}");
                        }
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
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nAn unexpected error occurred:");
                Console.WriteLine(ex.Message);
                Console.ResetColor();

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
        }

        static void GenerateFlightManifest()
        {
            try
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("========================================\r\nGenerate Flight Manifest\r\n========================================");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("\nEnter Flight Number: ");
                Console.ResetColor();

                string? flightNumber = Console.ReadLine()?.Trim().ToUpper();

                if (string.IsNullOrEmpty(flightNumber))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n  Error: Empty Flight Number. Press Enter.");
                    Console.ResetColor();
                    Console.ReadLine();
                    return;
                }
                else if (!flightNumbers.Contains(flightNumber))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n  Error: Flight Number does not exist. Press Enter.");
                    Console.ResetColor();
                    Console.ReadLine();
                    return;
                }

                // Collect all ticket IDs whose flight matches the requested flight into a temporary List.

                //List<string> tickets = new List<string>();
                //foreach (KeyValuePair<string, string> record in bookingRecord)
                //{
                //    string ticketID = record.Key;
                //    string flight = record.Value.Split('|')[0];
                //    if (flight == flightNumber)
                //    {
                //        tickets.Add(ticketID);
                //    }
                //}
                List<string> tickets = bookingRecord
                    .Where(record => record.Value.Split('|')[0] == flightNumber)
                    .Select(record => record.Key)
                    .ToList();

                if (tickets.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n  No passengers booked on this flight. Press Enter.");
                    Console.ResetColor();
                    Console.ReadLine();
                    return;
                }

                // For each collected ticket ID, retrieve the passenger name

                //List<string> passengers = new List<string>();
                //foreach (string ticket in tickets)
                //{
                //    passengers.Add(passengerNames[ticketNumbers.IndexOf(ticket)]);
                //}
                List<string> passengers = tickets
                    .Select(ticket => passengerNames[ticketNumbers.IndexOf(ticket)])
                    .ToList();

                // Sort the collected passenger records alphabetically by passenger name using a manual bubble sort on a List
                for (int i = 0; i < passengers.Count - 1; i++)
                {
                    bool swapped = false;

                    for (int j = 0; j < passengers.Count - i - 1; j++)
                    {
                        if (passengers[j].CompareTo(passengers[j + 1]) > 0)
                        {
                            // swap passengers
                            (passengers[j], passengers[j + 1]) = (passengers[j + 1], passengers[j]);

                            // swap tickets
                            (tickets[j], tickets[j + 1]) = (tickets[j + 1], tickets[j]);

                            swapped = true;
                        }
                    }

                    // stop early if already sorted
                    if (!swapped)
                        break;
                }

                Console.WriteLine($"\n{"No.",-5} {"Passenger Name",-20} {"Ticket ID",-12} {"Date",-14} {"Seat",-8} {"Status"}");
                Console.WriteLine(new string('-', 70));

                int boarded = 0, checkedIn = 0, cancelled = 0;

                for (int i = 0; i < passengers.Count; i++)
                {
                    string ticketID = tickets[i];
                    string passengerName = passengers[i];
                    string date = bookingRecord[ticketID].Split('|')[1];
                    string seat = passengerSeatMap.ContainsKey(passengerName) ? passengerSeatMap[passengerName] : "—";

                    string status;
                    if (passengerSeatMap.ContainsKey(passengerName))
                    {
                        status = "Boarded";
                        boarded++;
                    }
                    else if (checkedInQueue.Contains(passengerName))
                    {
                        status = "Checked-In";
                        checkedIn++;
                    }
                    else if (cancelledTickets.Contains(ticketID))
                    {
                        status = "Cancelled";
                        cancelled++;
                    }
                    else
                    {
                        status = "Booked";
                    }

                    Console.WriteLine($"{i + 1,-5} {passengerName,-20} {ticketID,-12} {date,-14} {seat,-8} {status}");
                }

                Console.WriteLine(new string('-', 70));
                Console.WriteLine($"\nTotal Passengers : {passengers.Count}");
                Console.WriteLine($"Boarded          : {boarded}");
                Console.WriteLine($"Checked-In       : {checkedIn}");
                Console.WriteLine($"Cancelled        : {cancelled}");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nAn unexpected error occurred:");
                Console.WriteLine(ex.Message);
                Console.ResetColor();

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
        }

        static void ManageWaitlistSeatAssignment()
        {
            try
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("========================================\r\nManage Waitlist & Seat Assignment\r\n========================================");
                Console.ResetColor();

                // Display sub-menu
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n(1) View waitlist\n(2) Promote next waitlist passenger\n(3) Promote specific waitlist passenger\n(4) Reassign passenger seat\n(0) Back");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("\nSelect option: ");
                Console.ResetColor();

                string? passenger;
                int flightChoice;
                int dateChoice;
                string flight;
                string date;
                string ticket;
                string booking;

                switch (Console.ReadLine())
                {
                    case "1":
                        if (waitlistQueue.Count != 0)
                        {
                            int counter = 1;
                            Console.ForegroundColor = ConsoleColor.White;
                            foreach (string currentPassenger in waitlistQueue.ToList())
                            {
                                Console.Write($"\n{counter}. {currentPassenger}");
                                counter++;
                            }
                            Console.WriteLine($"\n\nPassengers in the waitlist: {waitlistQueue.Count}");
                            Console.ResetColor();
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n  No passengers in the wait-list. Press Enter.");
                            Console.ResetColor();
                            Console.ReadLine();
                        }

                        break;

                    case "2":
                        if (waitlistQueue.Count != 0)
                        {
                            passenger = waitlistQueue.Dequeue();

                            flightChoice = GetAvailableFlights();
                            if (flightChoice == 0) return;

                            dateChoice = GetAvailableDates();
                            if (dateChoice == 0) return;

                            flight = flightNumbers[flightChoice - 1];
                            date = availableDates[dateChoice - 1];
                            ticket = ticketNumbers[passengerNames.IndexOf(passenger)];

                            booking = $"{flight}|{date}";
                            bookingRecord[ticket] = booking;

                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("\n--- Booking Confirmation ---");
                            Console.WriteLine($"Ticket ID     : {ticket}");
                            Console.WriteLine($"Passenger     : {passenger}");
                            Console.WriteLine($"Flight        : {flight}");
                            Console.WriteLine($"Date          : {date}");
                            Console.WriteLine("----------------------------");
                            Console.ResetColor();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"\n  Flight booked successfully. Press Enter.");
                            Console.ResetColor();
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n  No passengers in the wait-list. Press Enter.");
                            Console.ResetColor();
                            Console.ReadLine();
                        }
                        break;

                    case "3":
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("\nEnter passenger's full name: ");
                        Console.ResetColor();

                        passenger = Console.ReadLine()?.Trim();

                        if (string.IsNullOrEmpty(passenger))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n  Passenger's name cannot be empty. Press Enter.");
                            Console.ResetColor();
                            Console.ReadLine();
                            return;
                        }

                        if (!waitlistQueue.Contains(passenger))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n  No such passenger in the wait-list. Press Enter.");
                            Console.ResetColor();
                            Console.ReadLine();
                            return;
                        }

                        // Temp Queue
                        Queue<string> tempQ = new Queue<string>();

                        while (waitlistQueue.Count > 0)
                        {
                            string tempPass = waitlistQueue.Dequeue();

                            if (tempPass == passenger)
                            {
                                continue;
                            }
                            tempQ.Enqueue(tempPass);
                        }

                        while (tempQ.Count > 0)
                        {
                            waitlistQueue.Enqueue(tempQ.Dequeue());
                        }

                        flightChoice = GetAvailableFlights();
                        if (flightChoice == 0) return;

                        dateChoice = GetAvailableDates();
                        if (dateChoice == 0) return;

                        flight = flightNumbers[flightChoice - 1];
                        date = availableDates[dateChoice - 1];
                        ticket = ticketNumbers[passengerNames.IndexOf(passenger)];

                        booking = $"{flight}|{date}";
                        bookingRecord[ticket] = booking;

                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("\n--- Booking Confirmation ---");
                        Console.WriteLine($"Ticket ID     : {ticket}");
                        Console.WriteLine($"Passenger     : {passenger}");
                        Console.WriteLine($"Flight        : {flight}");
                        Console.WriteLine($"Date          : {date}");
                        Console.WriteLine("----------------------------");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"\n  Booked successfully. Press Enter.");
                        Console.ResetColor();
                        Console.ReadLine();
                        break;

                    case "4":
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("\nEnter passenger's full name: ");
                        Console.ResetColor();

                        passenger = Console.ReadLine()?.Trim();

                        if (string.IsNullOrEmpty(passenger))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n  Passenger's name cannot be empty. Press Enter.");
                            Console.ResetColor();
                            Console.ReadLine();
                            return;
                        }

                        if (!passengerSeatMap.ContainsKey(passenger))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n  No such passenger in the seat map. Press Enter.");
                            Console.ResetColor();
                            Console.ReadLine();
                            return;
                        }

                        string oldSeat = passengerSeatMap[passenger];

                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("\nEnter new seat code (e.g. 14A): ");
                        Console.ResetColor();

                        string? newSeat = Console.ReadLine()?.Trim().ToUpper();

                        if (string.IsNullOrEmpty(newSeat))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n  Seat code cannot be empty. Press Enter.");
                            Console.ResetColor();
                            Console.ReadLine();
                            return;
                        }

                        // Must be at least 2 characters — e.g. "10A"
                        if (newSeat.Length != 3)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n  Invalid seat format. Example: 14A. Press Enter.");
                            Console.ResetColor();
                            Console.ReadLine();
                            return;
                        }

                        // Last character must be A–F
                        char seatLetter = newSeat[newSeat.Length - 1];
                        if (seatLetter < 'A' || seatLetter > 'F')
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n  Invalid seat letter. Must be A to F. Press Enter.");
                            Console.ResetColor();
                            Console.ReadLine();
                            return;
                        }

                        // Everything before the last character must be a valid row number
                        string rowPart = newSeat.Substring(0, newSeat.Length - 1);
                        int rowNumber;
                        if (!int.TryParse(rowPart, out rowNumber) || rowNumber < 10 || rowNumber > 40)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n  Invalid row number. Must be between 10 and 40. Press Enter.");
                            Console.ResetColor();
                            Console.ReadLine();
                            return;
                        }

                        // Check if seat is already taken by another passenger
                        foreach (KeyValuePair<string, string> entry in passengerSeatMap)
                        {
                            if (entry.Value == newSeat && entry.Key != passenger)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"\n  Seat {newSeat} is already assigned to {entry.Key}. Press Enter.");
                                Console.ResetColor();
                                Console.ReadLine();
                                return;
                            }
                        }

                        passengerSeatMap[passenger] = newSeat;

                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("\n--- Seat Reassignment ---");
                        Console.WriteLine($"Passenger : {passenger}");
                        Console.WriteLine($"Old Seat  : {oldSeat}");
                        Console.WriteLine($"New Seat  : {newSeat}");
                        Console.WriteLine("-------------------------");
                        Console.ResetColor();
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
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nAn unexpected error occurred:");
                Console.WriteLine(ex.Message);
                Console.ResetColor();

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
        }
    }
}
