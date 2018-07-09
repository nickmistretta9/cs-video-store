using System;
using System.Collections.Generic;
using System.Linq;

namespace VideoStore
{
    public class Store
    {
        private VideoStoreEntities _videoStore;
        private List<string> _mainOptions;
        private StoreCustomer _customer;
        private List<Video> _videosInStore;

        public Store()
        {
            _videoStore = new VideoStoreEntities();
            _mainOptions = new List<string>
            {
                "1) View the current videos in the store",
                "2) Add a new video to the store",
                "3) Purchase video(s)",
                "4) View item(s) in cart",
                "5) Quit"
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
            Console.WriteLine("Hello {0}, what would you like to do?", _customer.Name);
            while (!IsDone)
            {
                foreach (var option in _mainOptions)
                    Console.WriteLine(option);
                string optionDecision = Console.ReadLine();
                switch (optionDecision)
                {
                    default:
                    case "1":
                        ListAllVideosFromStore();
                        IsDone = PromptToContinue();
                        break;
                    case "2":
                        AddVideoToStore();
                        IsDone = PromptToContinue();
                        break;
                    case "3":
                        PromptToSearch();
                        IsDone = PromptToContinue();
                        break;
                    case "4":
                        DisplayItemsInCart();
                        break;
                    case "5":
                        IsDone = true;
                        break;
                }
            }
            Console.WriteLine("Thank you for using the video store. See you next time!");
        }

        private bool PromptToContinue()
        {
            bool stopExecution;
            Console.Write("Would you like to do anything else? (Y/N): ");
            string toContinue = Console.ReadLine().ToUpper();
            switch(toContinue)
            {
                default:
                case "Y":
                    stopExecution = false;
                    Console.WriteLine("What would you like to do?");
                    break;
                case "N":
                    stopExecution = true;
                    break;
            }
            return stopExecution;
        }

        private void PromptToSearch()
        {
            bool doneShopping = false;
            Console.WriteLine("Your current cart total is ${0}", _customer.CartPrice);
            List<string> options = new List<string>
            {
                "1) Browse all videos",
                "2) Search by name",
                "3) Search by genre",
                "4) Search by price"
            };
            while (!doneShopping)
            {
                Console.WriteLine("How would you like to search the store?");
                foreach (string option in options)
                    Console.WriteLine(option);
                string input = Console.ReadLine();
                switch (input)
                {
                    default:
                    case "1":
                        ListAllVideosFromStore();
                        ChooseVideoFromStore();
                        doneShopping = PromptToContinueSearching();
                        break;
                    case "2":
                        FindVideosByName();
                        ChooseVideoFromStore();
                        doneShopping = PromptToContinueSearching();
                        break;
                    case "3":
                        FindVideosByGenre();
                        ChooseVideoFromStore();
                        doneShopping = PromptToContinueSearching();
                        break;
                    case "4":
                        FindVideosByPrice();
                        ChooseVideoFromStore();
                        doneShopping = PromptToContinueSearching();
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

        private void FindVideosByName()
        {
            Console.Write("Enter the name of the video to search for: ");
            string videoName = Console.ReadLine();
            _videosInStore = _videoStore.Videos.Where(v => v.VideoTitle == videoName).ToList();
            DrawTopVideoLine();
            int count = 1;
            foreach (Video video in _videosInStore)
            {
                DisplayVideoInfo(video, count);
                count++;
            }
            DrawBottomVideoLine();
        }

        private void FindVideosByGenre()
        {
            List<string> genres = new List<string>
            {
                "1) Action",
                "2) Thriller",
                "3) Comedy",
                "4) Horror",
                "5) Adventure"
            };
            //List<Video> genres = _videoStore.Videos.SqlQuery("SELECT * FROM dbo.Videos").ToList();

            Console.WriteLine("Which genre would you like to search for?");
            foreach (var genre in genres)
                Console.WriteLine(genre);
            string genreType = Console.ReadLine();
            int genreIndex = int.Parse(genreType);
            string[] stringParts = genres[genreIndex - 1].Split(' ');
            string finalGenre = stringParts[1];
            _videosInStore = _videoStore.Videos.Where(v => v.VideoGenre == finalGenre).ToList();
            DrawTopVideoLine();
            int count = 1;
            foreach (Video video in _videosInStore)
            {
                DisplayVideoInfo(video, count);
                count++;
            }
            DrawBottomVideoLine();
        }

        private void FindVideosByPrice()
        {
            List<string> options = new List<string>
            {
                "1) Find video(s) by exact price",
                "2) Find video(s) less than a certain price",
                "3) Find video(s) more than a certain price"
            };
            Console.WriteLine("How would you like to search by price?");
            foreach(string option in options)
                Console.WriteLine(option);
            string input = Console.ReadLine();
            Console.Write("What price would you like to search by?: ");
            decimal price = decimal.Parse(Console.ReadLine());
            switch(input)
            {
                default:
                case "1":
                    GetVideoByExactPrice(price);
                    break;
                case "2":
                    GetVideoByLessThanPrice(price);
                    break;
                case "3":
                    GetVideoByGreaterThanPrice(price);
                    break;
            }
        }

        private void GetVideoByExactPrice(decimal price)
        {
            _videosInStore = _videoStore.Videos.Where(v => v.VideoPrice == price).ToList();
            DrawTopVideoLine();
            int count = 1;
            foreach(Video video in _videosInStore)
            {
                DisplayVideoInfo(video, count);
                count++;
            }
            DrawBottomVideoLine();
        }

        private void GetVideoByLessThanPrice(decimal price)
        {
            _videosInStore = _videoStore.Videos.Where(v => v.VideoPrice <= price).ToList();
            DrawTopVideoLine();
            int count = 1;
            foreach (Video video in _videosInStore)
            {
                DisplayVideoInfo(video, count);
                count++;
            }
            DrawBottomVideoLine();
        }
        
        private void GetVideoByGreaterThanPrice(decimal price)
        {
            _videosInStore = _videoStore.Videos.Where(v => v.VideoPrice >= price).ToList();
            DrawTopVideoLine();
            int count = 1;
            foreach (Video video in _videosInStore)
            {
                DisplayVideoInfo(video, count);
                count++;
            }
            DrawBottomVideoLine();
        }

        private void ListAllVideosFromStore()
        {
            _videosInStore = _videoStore.Videos.SqlQuery("SELECT * FROM dbo.Videos").ToList();
            DrawTopVideoLine();
            int count = 1;
            foreach (Video video in _videosInStore)
            {
                DisplayVideoInfo(video, count);
                count++;
            }
            DrawBottomVideoLine();
        }

        private void ChooseVideoFromStore()
        {
            Console.Write("Which video would you like to purchase? ");
            int videoChoice = int.Parse(Console.ReadLine()) - 1;
            _customer.CartPrice += _videosInStore[videoChoice].VideoPrice;
            _customer.CartContents.Add(_videosInStore[videoChoice]);
            Console.WriteLine("Video sucessfully added to cart.");
        }

        private void DisplayItemsInCart()
        {
            int count = 1;
            DrawTopVideoLine();
            foreach(var video in _customer.CartContents)
            {
                DisplayVideoInfo(video, count);
                count++;
            }
            DrawBottomVideoLine();
        }

        private bool PromptToContinueSearching()
        {
            bool stopExecution;
            Console.Write("Would you like to continue shopping? (Y/N): ");
            string toContinue = Console.ReadLine().ToUpper();
            switch(toContinue)
            {
                default:
                case "Y":
                    stopExecution = false;
                    break;
                case "N":
                    stopExecution = true;
                    break;
            }
            Console.WriteLine("Your current cart total is ${0}", _customer.CartPrice);
            return stopExecution;

        }

        private void DrawTopVideoLine()
        {
            Console.WriteLine("Video Title | Release Date | Genre | Price");
            Console.WriteLine("------------------------------------------");
        }

        private void DrawBottomVideoLine()
        {
            Console.WriteLine("------------------------------------------");
        }

        private void DisplayVideoInfo(Video video, int count)
        {
            Console.WriteLine("{0}) {1} | {2} | {3} | {4}", count, video.VideoTitle, video.VideoReleaseDate, video.VideoGenre, video.VideoPrice);
        }
    }
}