using System;
using System.Collections.Generic;

namespace VideoStore
{
    public class Store
    {
        private VideoStoreEntities _videoStore;
        private List<string> _mainOptions;
        private StoreCustomer _customer;

        public Store()
        {
            _videoStore = new VideoStoreEntities();
            _mainOptions = new List<string>
            {
                "1) View the current videos in the store",
                "2) Add a new video to the store",
                "3) Quit"
            };
            _customer = new StoreCustomer();
        }

        public void Greeting()
        {
            Console.WriteLine("--- Welcome to the video store. ---");
            Console.Write("Are you a new customer or an existing customer? ");
            string customerType = Console.ReadLine();
            switch(customerType.ToLower())
            {
                default:
                case "new":
                    _customer.CreateNewCustomer();
                    break;
                case "existing":
                    _customer = _customer.LookupCustomer();
                    break;
            }
            bool IsDone = false;
            while (!IsDone)
            {
                Console.WriteLine("Hello {0}, what would you like to do?", _customer.Name);
                foreach (var option in _mainOptions)
                    Console.WriteLine(option);
                string optionDecision = Console.ReadLine();
                switch (optionDecision)
                {
                    default:
                    case "1":
                        break;
                    case "2":
                        AddVideoToStore();
                        break;
                    case "3":
                        IsDone = true;
                        break;
                }
            }
        }

        private void AddVideoToStore()
        {
            Console.Write("Enter the title of the video: ");
            string videoTitle = Console.ReadLine();
            Console.Write("Enter the release date of the video: ");
            string releaseDateString = Console.ReadLine();
            var releaseDate = DateTime.Parse(releaseDateString);
            Console.Write("Enter the genre of the video: ");
            string genre = Console.ReadLine();
            Console.Write("Enter the price of the video: ");
            string price = Console.ReadLine();
            try
            {
                _videoStore.AddVideo(videoTitle, releaseDate, genre, price);
            } catch (Exception e)
            {
                Console.WriteLine("Could not add video to store: {0}", e.Message);
                throw e;
            }
        }
    }
}
