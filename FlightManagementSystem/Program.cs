namespace FlightManagementSystem
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


        static int boardingRow = 10;
        static int boardingSeat = 0; // 0=A, 1=B, 2=C, 3=D, 4=E, 5=F

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

            Console.ForegroundColor = ConsoleColor.White;
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
            
            if (checkedInQueue.Contains(passengerName))
            {
                while (checkedInQueue.Count > 0)
                {
                    if (checkedInQueue.Peek() == passengerName)
                    {
                        checkedInQueue.Dequeue();
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("\n  Notice: Passenger was removed from Check-In Queue.");
                        Console.ResetColor();
                    }
                    else
                    {
                        tempQ.Enqueue(checkedInQueue.Dequeue());
                    }
                }
            }
                        
            checkedInQueue = new Queue<string>(tempQ);

            // Temp Stack
            Stack<string> tempS = new Stack<string>();


            if (boardingStack.Contains(passengerName))
            {
                int stackCount = boardingStack.Count; // capture once

                for (int i = 0; i < stackCount; i++)
                {
                    if (boardingStack.Peek() != passengerName)
                    {
                        tempS.Push(boardingStack.Pop());
                    }
                    else
                    {
                        boardingStack.Pop();
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("\n  Notice: Passenger was removed from Boarding Stack.");
                        Console.ResetColor();
                    }
                }
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

        static void PassengerCheckIn()
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

        static void BoardPassengers()
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

            string passenger;

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

        static void GenerateFlightManifest()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("========================================\r\nGenerate Flight Manifest\r\n========================================");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("\nEnter Flight Number: ");
            Console.ResetColor();

            string flightNumber = Console.ReadLine().Trim().ToUpper();

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
            List<string> tickets = new List<string>();
            foreach (KeyValuePair<string, string> record in bookingRecord)
            {
                string ticketID = record.Key;
                string flight = record.Value.Split('|')[0];
                if (flight == flightNumber)
                {
                    tickets.Add(ticketID);
                }
            }

            // For each collected ticket ID, retrieve the passenger name
            List<string> passengers = new List<string>();
            foreach (string ticket in  tickets)
            {
                passengers.Add(passengerNames[ticketNumbers.IndexOf(ticket)]);
            }

            // Sort the collected passenger records alphabetically by passenger name using a manual bubble sort on a List
            string temp;
            string tempTicket;
            for (int i = 0; i < passengers.Count; i++)
            {
                for (int j = passengers.Count - 1; j > 0; j--)
                {
                    if (passengers[j].CompareTo(passengers[j - 1]) < 0)
                    {
                        // swap passengers
                        temp = passengers[j];
                        passengers[j] = passengers[j - 1];
                        passengers[j - 1] = temp;

                        // swap tickets at same index
                        tempTicket = tickets[j];
                        tickets[j] = tickets[j - 1];
                        tickets[j - 1] = tempTicket;
                    }
                }
            }

            if (tickets.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n  No passengers booked on this flight. Press Enter.");
                Console.ResetColor();
                Console.ReadLine();
                return;
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

        static void ManageWaitlistSeatAssignment()
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

            string passenger;
            int flightChoice;
            int dateChoice;
            string flight;
            string date;
            string ticket;
            string booking;

            switch(Console.ReadLine())
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

                    passenger = Console.ReadLine().Trim();

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
                        if (waitlistQueue.Peek() == passenger)
                        {
                            waitlistQueue.Dequeue();
                        }
                        tempQ.Enqueue(waitlistQueue.Dequeue());
                    }
                  
                    waitlistQueue = new Queue<string>(tempQ);

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

                    passenger = Console.ReadLine().Trim();

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

                    string newSeat = Console.ReadLine().Trim().ToUpper();

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
    }
}
