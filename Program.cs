using System;

namespace Assignment03
{
    /*
    PART 01 — THEORETICAL QUESTIONS

    Q1: Overloading, Overriding, and Binding
    ----------------------------------------------------------------------------------
    a) What is the difference between Method Overloading and Method Overriding?
       - Method Overloading: Allows a class to have multiple methods with the same name 
         but different parameters (number, type, or order) within the same class. 
         It is resolved at compile-time (Static Binding).
       - Method Overriding: Allows a derived class to provide a specific implementation 
         of a method that is already defined in its base class using 'virtual' and 'override'. 
         It is resolved at runtime (Dynamic Binding).

    b) What is the difference between Static Binding and Dynamic Binding?
       - Static Binding (Early Binding): The compiler decides which method to call 
         at compile-time based on the reference type (e.g., Method Overloading or Method Hiding with 'new').
       - Dynamic Binding (Late Binding): The runtime decides which method to call 
         at runtime based on the actual object type being referred to (e.g., Method Overriding with 'virtual' & 'override').


    Q2: Sealed Classes and Methods
    ----------------------------------------------------------------------------------
    a) What is the purpose of the 'sealed' keyword when applied to a class?
       - It prevents the class from being inherited by any other class, stopping 
         the inheritance chain completely.

    b) What is the difference between a sealed class and a sealed method?
       - Sealed Class: Prevents any class from inheriting from it.
       - Sealed Method: Prevents a specific overridden method from being overridden again 
         by further derived classes down the inheritance chain.

    c) Can a sealed method be overridden? Why?
       - No. A sealed method cannot be overridden because the 'sealed' modifier explicitly 
         locks the method implementation, preventing any child class from modifying its behavior.
    */

    // PART 02 - PRACTICAL QUESTIONS
    public class Shipment
    {
        public string TrackingCode { get; set; }
        public string Description { get; set; }
        public double Weight { get; set; }
        public double DeliveryFee { get; set; }

        public Shipment()
        {
        }

        public Shipment(string trackingCode, string description, double weight, double deliveryFee)
        {
            TrackingCode = trackingCode;
            Description = description;
            Weight = weight;
            DeliveryFee = deliveryFee;
        }

        // Virtual Property
        public virtual double EstimatedCost
        {
            get
            {
                return DeliveryFee + (Weight * 5);
            }
        }

        // Virtual Method
        public virtual void PrintShipment()
        {
            Console.WriteLine($"Tracking Code : {TrackingCode}");
            Console.WriteLine($"Description : {Description}");
            Console.WriteLine($"Weight : {Weight} KG");
            Console.WriteLine($"Delivery Fee : {DeliveryFee} EGP");
            Console.WriteLine($"Estimated Cost: {EstimatedCost} EGP");
        }

        // Method Overloading - Version 1
        public void UpdateWeight(double newWeight)
        {
            Weight = newWeight;
        }

        // Method Overloading - Version 2
        public void UpdateWeight(double newWeight, double extraPackingWeight)
        {
            Weight = newWeight + extraPackingWeight;
        }
    }

    // Derived Class 1: StandardShipment
    public class StandardShipment : Shipment
    {
        public StandardShipment(string trackingCode, string description, double weight, double deliveryFee)
            : base(trackingCode, description, weight, deliveryFee)
        {
        }

        public override void PrintShipment()
        {
            Console.WriteLine(" Standard Shipment ");
            base.PrintShipment();
        }
    }

    // Derived Class 2: ExpressShipment
    public class ExpressShipment : Shipment
    {
        public double ExtraFee { get; set; }

        public ExpressShipment(string trackingCode, string description, double weight, double deliveryFee, double extraFee)
            : base(trackingCode, description, weight, deliveryFee)
        {
            ExtraFee = extraFee;
        }

        public override double EstimatedCost
        {
            get
            {
                return DeliveryFee + (Weight * 5) + ExtraFee;
            }
        }

        public override void PrintShipment()
        {
            Console.WriteLine(" Express Shipment ");
            Console.WriteLine($"Tracking Code : {TrackingCode}");
            Console.WriteLine($"Description : {Description}");
            Console.WriteLine($"Weight : {Weight} KG");
            Console.WriteLine($"Delivery Fee : {DeliveryFee} EGP");
            Console.WriteLine($"Extra Fee : {ExtraFee} EGP");
            Console.WriteLine($"Estimated Cost : {EstimatedCost} EGP");
        }
    }

    // Derived Class 3: InternationalShipment
    public class InternationalShipment : Shipment
    {
        public string DestinationCountry { get; set; }
        public double CustomsFee { get; set; }

        public InternationalShipment(string trackingCode, string description, double weight, double deliveryFee, string destinationCountry, double customsFee)
            : base(trackingCode, description, weight, deliveryFee)
        {
            DestinationCountry = destinationCountry;
            CustomsFee = customsFee;
        }

        public override double EstimatedCost
        {
            get
            {
                return DeliveryFee + (Weight * 5) + CustomsFee;
            }
        }

        public override void PrintShipment()
        {
            Console.WriteLine(" International Shipment ");
            Console.WriteLine($"Tracking Code : {TrackingCode}");
            Console.WriteLine($"Description : {Description}");
            Console.WriteLine($"Weight : {Weight} KG");
            Console.WriteLine($"Delivery Fee : {DeliveryFee} EGP");
            Console.WriteLine($"Destination Country : {DestinationCountry}");
            Console.WriteLine($"Customs Fee : {CustomsFee} EGP");
            Console.WriteLine($"Estimated Cost : {EstimatedCost} EGP");
        }

        public virtual void GenerateCustomsReport()
        {
            Console.WriteLine($"Customs Report generated for {DestinationCountry}. Fee: {CustomsFee}");
        }
    }

    // Point 8: Sealed Class Example
    public sealed class CompletedShipment : Shipment
    {
        public CompletedShipment(string trackingCode, string description, double weight, double deliveryFee)
            : base(trackingCode, description, weight, deliveryFee)
        {
        }
    }

    // Point 9: Sealed Method Example
    public class PriorityInternationalShipment : InternationalShipment
    {
        public PriorityInternationalShipment(string trackingCode, string description, double weight, double deliveryFee, string destinationCountry, double customsFee)
            : base(trackingCode, description, weight, deliveryFee, destinationCountry, customsFee)
        {
        }

        // Sealed method stops further overriding in any derived class
        public sealed override void GenerateCustomsReport()
        {
            Console.WriteLine($"Priority Customs Report for {DestinationCountry}.");
        }
    }

    // Point 6: DeliveryCenter Class
    public class DeliveryCenter
    {
        private Shipment[] shipments;
        private int count;

        public DeliveryCenter(int capacity)
        {
            shipments = new Shipment[capacity];
            count = 0;
        }

        public void AddShipment(Shipment shipment)
        {
            if (count < shipments.Length)
            {
                shipments[count] = shipment;
                count++;
            }
        }

        public void PrintAllShipments()
        {
            Console.WriteLine("==========================================");
            Console.WriteLine("Delivery Center");
            Console.WriteLine("==========================================");

            for (int i = 0; i < count; i++)
            {
                // Dynamic Binding call
                shipments[i].PrintShipment();
                Console.WriteLine("------------------------------------------");
            }
        }
    }

    // Point 7: DeliveryHelper Class
    public static class DeliveryHelper
    {
        public static void PrintShipmentDetails(Shipment shipment)
        {
            // Dynamic Binding call
            shipment.PrintShipment();
        }
    }

    // Point 10: Main Class
    internal class Program
    {
        static void Main(string[] args)
        {
            // Creating Shipments
            StandardShipment standard = new StandardShipment("SH001", "Laptop", 3, 80);
            ExpressShipment express = new ExpressShipment("SH002", "Mobile Phone", 2, 60, 30);
            InternationalShipment international = new InternationalShipment("SH003", "Television", 8, 120, "Germany", 100);

            // Delivery Center Execution
            DeliveryCenter center = new DeliveryCenter(3);
            center.AddShipment(standard);
            center.AddShipment(express);
            center.AddShipment(international);

            center.PrintAllShipments();

            // Using DeliveryHelper
            Console.WriteLine("==========================================");
            Console.WriteLine("Printing Using DeliveryHelper...");
            DeliveryHelper.PrintShipmentDetails(standard);
            Console.WriteLine("Standard Shipment Printed Successfully.");
            DeliveryHelper.PrintShipmentDetails(express);
            Console.WriteLine("Express Shipment Printed Successfully.");
            DeliveryHelper.PrintShipmentDetails(international);
            Console.WriteLine("International Shipment Printed Successfully.");

            // Update Weight Demonstrations
            Console.WriteLine("==========================================");
            Console.WriteLine("Updating Weight...");
            Console.WriteLine($"Original Weight : {standard.Weight} KG");

            standard.UpdateWeight(5);
            Console.WriteLine($"Updated Weight : {standard.Weight} KG");

            standard.UpdateWeight(5, 0.5);
            Console.WriteLine($"Updated Weight After Packing : {standard.Weight} KG");

            // Mixed Array Dynamic Binding Demonstration
            Console.WriteLine("==========================================");
            Console.WriteLine("Printing Using Shipment[]...");
            Shipment[] mixedShipments = new Shipment[] { standard, express, international };
            foreach (Shipment s in mixedShipments)
            {
                s.PrintShipment();
            }

            // Sealed Class and Sealed Method Usage Demonstration
            CompletedShipment completed = new CompletedShipment("SH004", "Book", 1, 20);

            PriorityInternationalShipment priorityInt = new PriorityInternationalShipment("SH005", "Tablet", 2, 100, "France", 50);
            priorityInt.GenerateCustomsReport();
        }
    }
}