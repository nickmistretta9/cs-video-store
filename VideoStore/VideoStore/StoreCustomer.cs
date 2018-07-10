using System;
using System.Collections.Generic;
using System.Linq;

namespace VideoStore
{
    public class StoreCustomer
    {
        private VideoStoreEntities _videoStore = new VideoStoreEntities();
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int CustomerId { get; set; }
        public decimal CartPrice { get; set; }
        public List<Video> CartContents { get; set; }

        public StoreCustomer()
        {
            CartPrice = 00.00M;
            CartContents = new List<Video>();
        }

        public void CreateNewCustomer()
        {
            Console.Write("Enter your name (first and last): ");
            Name = Console.ReadLine();
            Console.Write("Enter your email (or 'null' to leave blank): ");
            Email = Console.ReadLine();
            Console.Write("Enter your phone number (or 'null' to leave blank): ");
            PhoneNumber = Console.ReadLine();
            try
            {
                _videoStore.AddNewCustomer(Name, Email, PhoneNumber);
                Console.WriteLine("New customer successfully registered. Welcome to the video store {0}", Name);
            } catch(Exception e)
            {
                Console.WriteLine("Could not create new customer: {0}", e.Message);
                throw e;
            }
        }

        public StoreCustomer LookupCustomer()
        {
            StoreCustomer customerToReturn = new StoreCustomer();
            List<string> LookupOptions = new List<string>
            {
                "1) By CustomerID number",
                "2) By Customer Name"
            };
            Console.WriteLine("How would you like us to find your information?");
            foreach (string option in LookupOptions)
                Console.WriteLine(option);
            string lookupType = Console.ReadLine();
            switch(lookupType)
            {
                default:
                case "1":
                    customerToReturn = FindCustomerById();
                    break;
                case "2":
                    customerToReturn = FindCustomerByName();
                    break;
            }
            return customerToReturn;
        }

        private StoreCustomer FindCustomerByName()
        {
            bool isFound = false;
            StoreCustomer customerToReturn = new StoreCustomer();
            while (!isFound)
            {
                Console.Write("Enter the name to lookup: ");
                string name = Console.ReadLine();
                var results = _videoStore.RetrieveCustomerByName(name);
                List<RetrieveCustomerByName_Result> resultsList = new List<RetrieveCustomerByName_Result>();
                resultsList = (from a in results select a).ToList();
                int count = 1;
                if (resultsList.Count == 0)
                    Console.WriteLine("No results found for the name {0}. Please try again.", name);
                else if(resultsList.Count == 1)
                {
                    customerToReturn.Name = resultsList[0].CustomerName;
                    customerToReturn.Email = resultsList[0].CustomerEmail;
                    customerToReturn.PhoneNumber = resultsList[0].CustomerPhone;
                    customerToReturn.CustomerId = resultsList[0].CustomerId;
                    isFound = true;
                }
                else
                {
                    Console.WriteLine("{0} results found for the name {1}. Please select which one is you.", resultsList.Count, name);
                    foreach (var result in resultsList)
                    {
                        Console.WriteLine("{0}) {1} | {2} | {3}", count, result.CustomerName, result.CustomerEmail, result.CustomerPhone);
                        count++;
                    }
                    int customerDecision = int.Parse(Console.ReadLine()) - 1;
                    customerToReturn.Name = resultsList[customerDecision].CustomerName;
                    customerToReturn.Email = resultsList[customerDecision].CustomerEmail;
                    customerToReturn.PhoneNumber = resultsList[customerDecision].CustomerPhone;
                    customerToReturn.CustomerId = resultsList[0].CustomerId;
                    isFound = true;
                }
            }
            return customerToReturn;
        }

        private StoreCustomer FindCustomerById()
        {
            return new StoreCustomer();
        }
    }
}
