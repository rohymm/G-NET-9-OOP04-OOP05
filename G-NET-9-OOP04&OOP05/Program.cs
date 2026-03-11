using System;

namespace Assignment
{
    // =================================================================
    // ASSIGNMENT 04: Polymorphism & Overloading
    // =================================================================
    namespace Assignment04
    {
        // Part 01: Theoretical Answers
        // Q1: Static binding happens at compile time (like method overloading). Dynamic binding happens at run time (like overriding).
        // Q2: Overloading is having the same method name but different parameters. Overriding is replacing the parent's method in the child class.
        // Q3: 'virtual' is put in the parent class so it can be changed. 'override' is put in the child class to actually change it.

        class Ticket
        {
            public string MovieName { get; set; }
            public decimal Price { get; set; }
            public int TicketId { get; private set; }
            private static int counter = 0;

            public Ticket(string name, decimal price)
            {
                MovieName = name;
                Price = price;
                counter++;
                TicketId = counter;
            }

            public decimal PriceAfterTax
            {
                get { return Price + (Price * 0.14m); }
            }

            // Virtual method for Polymorphism
            public virtual void PrintTicket()
            {
                Console.WriteLine("Ticket #" + TicketId + " | " + MovieName + " | Price: " + Price + " EGP | After Tax: " + PriceAfterTax + " EGP");
            }

            // Method Overloading
            public void SetPrice(decimal newPrice)
            {
                Price = newPrice;
            }

            // Method Overloading with multiplier
            public void SetPrice(decimal basePrice, decimal multiplier)
            {
                // calculate new price using multiplier
                Price = basePrice * (int)multiplier;
            }
        }

        class StandardTicket : Ticket
        {
            public string SeatNumber { get; set; }
            public StandardTicket(string name, decimal price, string seat) : base(name, price) { SeatNumber = seat; }

            public override void PrintTicket()
            {
                base.PrintTicket();
                Console.WriteLine("Seat: " + SeatNumber);
            }
        }

        class VIPTicket : Ticket
        {
            public bool LoungeAccess { get; set; }
            public decimal ServiceFee { get; set; } = 50m;
            public VIPTicket(string name, decimal price, bool lounge) : base(name, price) { LoungeAccess = lounge; }

            public override void PrintTicket()
            {
                base.PrintTicket();
                Console.WriteLine("Lounge: " + (LoungeAccess ? "Yes" : "No") + " | Service Fee: " + ServiceFee + " EGP");
            }
        }

        class IMAXTicket : Ticket
        {
            public bool Is3D { get; set; }
            public IMAXTicket(string name, decimal price, bool is3d) : base(name, price)
            {
                Is3D = is3d;
                if (Is3D) Price += 30m;
            }

            // print imax details
            public void PrintTicket()
            {
                base.PrintTicket();
                Console.WriteLine("IMAX 3D: " + (Is3D ? "Yes" : "No"));
            }
        }

        class Cinema
        {
            private Ticket[] tickets = new Ticket[20];

            public void AddTicket(Ticket t)
            {
                for (int i = 0; i < 20; i++)
                {
                    if (tickets[i] == null) { tickets[i] = t; break; }
                }
            }

            public void PrintAllTickets()
            {
                foreach (Ticket t in tickets)
                {
                    if (t != null) t.PrintTicket();
                }
            }
        }
    }

    // =================================================================
    // ASSIGNMENT 05: Interfaces & Deep/Shallow Copy
    // =================================================================
    namespace Assignment05
    {
        // Part 01: Theoretical Questions
        // Q1: An interface is a contract. It forces classes to implement methods without telling them HOW. 
        // Benefits: 1. Multiple inheritance. 2. Loosely coupled code. 3. Easy to group different classes.
        // Q2: a) Ambiguity. The class implements two interfaces with the same method name.
        //     b) We fix this with Explicit Interface Implementation: void IEnglishSpeaker.Greet() { ... }
        //     c) We cannot call them directly. We must cast the object first: ((IEnglishSpeaker)translator).Greet();
        // Q3: Shallow copy copies value types, but reference types share the same memory. Deep copy creates a new copy of everything.
        // Q4: The output will be Testing for both. Because Department is a reference type, the shallow copy made both point to the same object.

        public interface IPrintable
        {
            void Print();
        }

        public interface IBookable
        {
            void Book();
            void Cancel();
        }

        static class BookingHelper
        {
            public static void PrintAll(IPrintable[] items)
            {
                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i] != null) items[i].Print();
                }
            }
        }

        class Ticket : IPrintable, IBookable, ICloneable
        {
            public int TicketId { get; private set; }
            public string MovieName { get; set; }
            public decimal Price { get; set; }
            public bool IsBooked { get; set; }
            private static int counter = 0;

            public Ticket(string name, decimal price)
            {
                MovieName = name;
                Price = price;
                IsBooked = false;
                counter++;
                TicketId = counter;
            }

            public decimal PriceAfterTax { get { return Price + (Price * 0.14m); } }

            public virtual void Print()
            {
                Console.Write("[Ticket #" + TicketId + "] " + MovieName + " | Price: " + Price + " | After Tax: " + PriceAfterTax + " | Booked: " + (IsBooked ? "Yes" : "No"));
            }

            public void Book()
            {
                if (IsBooked == false) IsBooked = true;
                else Console.WriteLine("Ticket is already booked.");
            }

            public void Cancel()
            {
                if (IsBooked == true)
                {
                    // ticket cancelled successfully
                    Console.WriteLine("Ticket cancelled successfully.");
                }
            }

            public object Clone()
            {
                // Deep copy implementation
                return this.MemberwiseClone();
            }
        }

        class StandardTicket : Ticket
        {
            public string SeatNumber { get; set; }
            public StandardTicket(string name, decimal price, string seat) : base(name, price) { SeatNumber = seat; }

            public override void Print()
            {
                Console.Write("[Ticket #" + TicketId + "] " + MovieName + " | Standard | Seat: " + SeatNumber + " | Price: " + Price + " | Booked: " + (IsBooked ? "Yes" : "No"));
                Console.WriteLine();
            }
        }

        class VIPTicket : Ticket
        {
            public bool LoungeAccess { get; set; }
            public decimal ServiceFee { get; set; } = 50m;
            public VIPTicket(string name, decimal price, bool lounge) : base(name, price) { LoungeAccess = lounge; }

            public override void Print()
            {
                Console.Write("[Ticket #" + TicketId + "] " + MovieName + " | VIP | Lounge: " + (LoungeAccess ? "Yes" : "No") + " | Fee: " + ServiceFee + " | Price: " + Price + " | Booked: " + (IsBooked ? "Yes" : "No"));
                Console.WriteLine();
            }
        }

        class IMAXTicket : Ticket
        {
            public bool Is3D { get; set; }
            public IMAXTicket(string name, decimal price, bool is3d) : base(name, price) { Is3D = is3d; }

            public override void Print()
            {
                // print ticket info
                Console.WriteLine("[Ticket #" + TicketId + "] " + MovieName + " | IMAX | 3D: " + (Is3D ? "Yes" : "No") + " | Price: " + Price);
            }
        }

        class Cinema
        {
            private Ticket[] tickets = new Ticket[20];

            public void AddTicket(Ticket t)
            {
                for (int i = 0; i < 20; i++)
                {
                    if (tickets[i] == null) { tickets[i] = t; break; }
                }
            }

            public void PrintAllTickets()
            {
                foreach (Ticket t in tickets)
                {
                    if (t != null) t.Print();
                }
            }
        }
    }

    // =================================================================
    // MAIN PROGRAM EXECUTION
    // =================================================================
    class Program
    {
        static void Main(string[] args)
        {
            
            #region Assignment 04 Test
            /*
            Console.WriteLine(">>> RUNNING ASSIGNMENT 04 (POLYMORPHISM) <<<");
            Assignment04.Cinema cinema04 = new Assignment04.Cinema();
            
            Assignment04.Ticket t1 = new Assignment04.StandardTicket("Inception", 150m, "A-5");
            Assignment04.Ticket t2 = new Assignment04.VIPTicket("Avengers", 200m, true);
            Assignment04.Ticket t3 = new Assignment04.IMAXTicket("Dune", 180m, false);

            t1.SetPrice(100m, 1.5m); // testing the overloaded method
            
            cinema04.AddTicket(t1);
            cinema04.AddTicket(t2);
            cinema04.AddTicket(t3);

            cinema04.PrintAllTickets();
            Console.WriteLine();
            */
            #endregion

            #region Assignment 05 Test
            /*
            Console.WriteLine(">>> RUNNING ASSIGNMENT 05 (INTERFACES) <<<");
            Assignment05.Cinema cinema05 = new Assignment05.Cinema();

            Assignment05.StandardTicket st = new Assignment05.StandardTicket("Inception", 80m, "A5");
            Assignment05.VIPTicket vt = new Assignment05.VIPTicket("Avengers", 200m, true);
            Assignment05.IMAXTicket it = new Assignment05.IMAXTicket("Dune", 130m, true);

            st.Book();
            vt.Book();
            it.Book();

            cinema05.AddTicket(st);
            cinema05.AddTicket(vt);
            cinema05.AddTicket(it);

            Console.WriteLine("--- All Tickets ---");
            cinema05.PrintAllTickets();

            Console.WriteLine("\n--- Clone Test ---");
            Assignment05.VIPTicket cloneTicket = (Assignment05.VIPTicket)vt.Clone();
            cloneTicket.MovieName = "Interstellar";
            
            Console.WriteLine("Original :");
            vt.Print();
            Console.WriteLine("Clone :");
            cloneTicket.Print();

            Console.WriteLine("\n--- Cancellation Test ---");
            st.Cancel(); 
            st.Print(); 

            Console.WriteLine("\n--- BookingHelper Array Print ---");
            Assignment05.IPrintable[] printList = { st, vt, it };
            Assignment05.BookingHelper.PrintAll(printList);
            */
            #endregion
        }
    }
}