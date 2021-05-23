using System;
using System.Collections.Generic;
using Humanizer;

namespace ZooCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            bool playAgain = false;

            do
            {
                playAgain = false;
                bool continueProgram = WelcomeMessage();
                if (!continueProgram)
                {
                    playAgain = DoYouWantToPlayAgain();

                    if(playAgain)
                    {
                        continue;
                    }
                    else
                    {
                        ExitProgram();
                        break;
                    }

                }

                CreateStartingValues(out int[] dayNumber, out decimal[] cashOnHand, out int[] totalAttractionScore, out List<Animal> allAnimals,
                                     out PhysicalSpace[] allPhysicalSpaces, out List<Sundry> allConcessionsItems, out List<Sundry> allGiftShopItems,
                                     out List<string> mainMenuOptions, out decimal[] ticketPrice, out List<int> attendanceHistory);
                
                do
                {
                    if (dayNumber[0] != 25)
                    {
                        RunGame(dayNumber, cashOnHand, totalAttractionScore, allAnimals, allPhysicalSpaces, allConcessionsItems, allGiftShopItems,
                                mainMenuOptions, ticketPrice, attendanceHistory, out continueProgram);
                    }
                    else
                    {
                        EndOfGame(dayNumber, cashOnHand, totalAttractionScore, attendanceHistory, allAnimals, out continueProgram, out playAgain);
                    }
                }
                while (continueProgram);

                if (!playAgain)
                {
                    ExitProgram();
                }

            }
            while (playAgain);
        }

        static void CreateStartingValues(out int[] dayNumber, out decimal[] cashOnHand, out int[] totalAttractionScore, out List<Animal> allAnimals,
                                         out PhysicalSpace[] allPhysicalSpaces, out List<Sundry> allConcessionsItems, out List<Sundry> allGiftShopItems, 
                                         out List<string> mainMenuOptions, out decimal[] ticketPrice, out List<int> attendanceHistory)
        {
            dayNumber = new int[] { 1 };
            cashOnHand = new decimal[] { 25000.00m };
            totalAttractionScore = new int[] { 0 };
            allAnimals = GenerateZooAnimals();
            allPhysicalSpaces = GeneratePhysicalSpaces();
            allConcessionsItems = new List<Sundry>();
            allGiftShopItems = new List<Sundry>();
            mainMenuOptions = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "A", "I", "X" };
            ticketPrice = new decimal[] { 5.00m };
            attendanceHistory = new List<int>();

        }
        
        static void RunGame(int[] dayNumber, decimal[] cashOnHand, int[] totalAttractionScore, List<Animal> allAnimals, PhysicalSpace[] allPhysicalSpaces,
                            List<Sundry> allConcessionsItems, List<Sundry> allGiftShopItems, List<string> mainMenuOptions, decimal[] ticketPrice,
                            List<int> attendanceHistory, out bool continueProgram)
        {
            continueProgram = true;
            
            Console.Clear();
            DisplayDashboard(dayNumber, cashOnHand, totalAttractionScore);
            DisplayMainMenu(mainMenuOptions);
            string userOption = GetUserInput(mainMenuOptions);

            switch (userOption)
            {
                case "1":
                    DisplayZooMap(allPhysicalSpaces, dayNumber, cashOnHand, totalAttractionScore);
                    break;

                case "2":
                    BuyNewAnimals(allPhysicalSpaces, allAnimals, dayNumber, cashOnHand, totalAttractionScore);
                    break;

                case "3":
                    ArrangeAnimals(allPhysicalSpaces, allAnimals, dayNumber, cashOnHand, totalAttractionScore);
                    break;

                case "4":
                    SellZooAnimals(allPhysicalSpaces, allAnimals, dayNumber, cashOnHand, totalAttractionScore);
                    break;

                case "5":
                    ChangeTicketPrice(ticketPrice, dayNumber, cashOnHand, totalAttractionScore);
                    break;

                case "6":
                    EditConcessionsItems(allConcessionsItems, dayNumber, cashOnHand, totalAttractionScore);
                    break;

                case "7":
                    EditGiftShopItems(allGiftShopItems, dayNumber, cashOnHand, totalAttractionScore);
                    break;

                case "A":
                    AdvanceDay(allPhysicalSpaces, allAnimals, allConcessionsItems, allGiftShopItems, dayNumber, 
                               cashOnHand, totalAttractionScore, ticketPrice, attendanceHistory);
                    break;

                case "I":
                    DisplayInstructions();
                    break;

                case "X":
                    continueProgram = false;
                    break;

                default:
                    continueProgram = false;
                    break;
            }
        }

        static bool WelcomeMessage()
        {
            Console.Clear();
            Console.WriteLine("WELCOME TO THE WORLD-FAMOUS");
            Console.WriteLine();
            Console.WriteLine("ZZZZZZ    OOOOOO    OOOOOO");
            Console.WriteLine("   ZZ     OO  OO    OO  OO");
            Console.WriteLine(" ZZ       OO  OO    OO  OO");
            Console.WriteLine("ZZZZZZ    OOOOOO    OOOOOO");
            Console.WriteLine();
            Console.WriteLine("OF MAGIC AND ANIMAL WONDER!");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Press Enter to continue, or \"S\" to skip introduction.");

            string skip = Console.ReadLine().ToUpper();

            if (skip == "S")
            {
                Console.Clear();
                return true;
            }

            Console.Clear();
            Console.WriteLine(ConvertStatementToConsoleLengthLines("Long ago, your Great Aunt Gertrude purchased a plot of land in the " +
                              "Andes mountains."));
            Console.WriteLine();
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();

            Console.Clear();
            Console.WriteLine(ConvertStatementToConsoleLengthLines("She spent her considerable fortune and many years constructing *something*, " +
                              "but no one really knows what."));
            Console.WriteLine();
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();

            Console.Clear();
            Console.WriteLine(ConvertStatementToConsoleLengthLines("Recently, you received a telegram explaining that Great Aunt Gertrude died " +
                              "suddenly, and in her last will and testament she has bequeathed the land, all that has been constructed, and the " +
                              "remainder of her fortune - $25,000, to you."));
            Console.WriteLine();
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();

            Console.Clear();
            Console.WriteLine(ConvertStatementToConsoleLengthLines("However, you can only inherit the land and the money if you choose to complete " +
                              "the mysterious project that she was working on."));
            Console.WriteLine();
            Console.WriteLine("DO YOU WANT TO COMPLETE THE PROJECT? Enter \"Y\" or \"N\":");
            string continueToGame = Console.ReadLine().ToUpper();

            while (continueToGame != "N" && continueToGame != "Y")
            {
                Console.WriteLine("Invalid entry. Please enter \"Y\" or \"N\".");
                continueToGame = Console.ReadLine().ToUpper();
            }

            if (continueToGame == "N")
            {
                Console.Clear();
                Console.WriteLine(ConvertStatementToConsoleLengthLines("You don't want to finish the project, and that's ok! " +
                                  "Great Aunt Gertrude was a little strange. Who knows what she was working on, anyway?"));
                Console.WriteLine();

                Console.WriteLine(ConvertStatementToConsoleLengthLines("Whatever it is, you'll probably never find out. Or, maybe " +
                                  "you'll have second thoughts and talk to the lawyer again..."));

                return false;
            }

            Console.Clear();
            Console.WriteLine(ConvertStatementToConsoleLengthLines("Congratulations! You've inherited a ZOO from Great Aunt Gertrude and have " +
                             "$25,000 to run it!!!"));
            Console.WriteLine();
            Console.WriteLine("Press \"Enter\" to continue...");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine(ConvertStatementToConsoleLengthLines("But, unfortunately, the lawyer who is handling Great Aunt Gertrude's estate just " +
                             "delivered some bad news..."));
            Console.WriteLine();
            Console.WriteLine("Press \"Enter\" to continue...");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine(ConvertStatementToConsoleLengthLines("Apparently, the land the zoo was built on was never actually purchased outright, and " +
                             "the bank is demanding that a payment of $100,000 be made in full within the next 25 days."));
            Console.WriteLine();
            Console.WriteLine("Press \"Enter\" to continue...");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine(ConvertStatementToConsoleLengthLines("If you are able to collect $100,000 by the morning of Day 25, you can pay the " +
                             "bank what is owed and save the zoo! Otherwise, you'll be forced to give the zoo and the land back to the bank, " +
                             "and Great Aunt Gertrude's legacy will be tarnished forever."));
            Console.WriteLine();
            Console.WriteLine("Press \"Enter\" to continue...");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine(ConvertStatementToConsoleLengthLines("Well, what are you waiting for?! You have a zoo to run!"));
            Console.WriteLine();
            Console.WriteLine("Press \"Enter\" to continue...");
            Console.ReadLine();
            DisplayInstructions();
            return true;
        }

        static void DisplayInstructions()
        {
            Console.Clear();
            string instructionsHeader = "--------------------------- INSTRUCTIONS ---------------------------";

            Console.WriteLine(instructionsHeader);
            Console.WriteLine();
            Console.WriteLine("<<< WELCOME! >>>");
            Console.WriteLine();
            Console.WriteLine(ConvertStatementToConsoleLengthLines("The goal of this game is to manage your newly inherited zoo so that, " +
                             "with some luck and hard work, it becomes profitable over time."));
            Console.WriteLine();
            Console.WriteLine(ConvertStatementToConsoleLengthLines("To win the game, you must have at least $100,000 on the morning of Day 25. " +
                             "By doing so, you'll be able to pay off the bank, keep the zoo, and honor all that your your Great Aunt " +
                             "Gertrude sought to build with her considerable fortune. "));
            Console.WriteLine();
            Console.WriteLine(ConvertStatementToConsoleLengthLines("From the main menu, you will see the various options you have for " +
                             "managing your zoo."));
            Console.WriteLine();
            Console.WriteLine("Press \"Enter\" to continue...");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine(instructionsHeader);
            Console.WriteLine();
            Console.WriteLine("<<< EARNING INCOME >>>");
            Console.WriteLine();
            Console.WriteLine(ConvertStatementToConsoleLengthLines("Your zoo earns money by charging for admission tickets and selling " +
                             "items in the gift shop and concessions stands."));
            Console.WriteLine();
            Console.WriteLine(ConvertStatementToConsoleLengthLines("At the start of the game, the price of each ticket is set " +
                             "to $5.00, but you can change it at any time. The maximum price you can set for a ticket is $50.00."));
            Console.WriteLine();
            Console.WriteLine(ConvertStatementToConsoleLengthLines("Your zoo's popularity is measured by it's overall \"attraction score\", " +
                              "which is determined by the kind and number of animals in your zoo."));
            Console.WriteLine();
            Console.WriteLine(ConvertStatementToConsoleLengthLines("If your zoo has a low attraction score and a high ticket price, " +
                             "fewer people will come to your zoo that day."));
            Console.WriteLine();
            Console.WriteLine(ConvertStatementToConsoleLengthLines("If the weather is good, more people will come to the zoo. But, if " +
                              "the weather conditions are poor, attendance won't be as high."));
            Console.WriteLine();
            Console.WriteLine("Press \"Enter\" to continue...");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine(instructionsHeader);
            Console.WriteLine();
            Console.WriteLine("<<< ZOO ANIMALS >>>");
            Console.WriteLine();
            Console.WriteLine(ConvertStatementToConsoleLengthLines("Each zoo animal you buy has an \"attraction score\" that increases the " +
                             "popularity of your zoo. The more popular your zoo is, the more people attend each day! And, the higher your " +
                             "attendance, the more money you can make."));
            Console.WriteLine();
            Console.WriteLine(ConvertStatementToConsoleLengthLines("Animals that are smaller or more common have a lower \"attraction score\" " +
                             "than animals that are larger or more rare."));
            Console.WriteLine();
            Console.WriteLine(ConvertStatementToConsoleLengthLines("Each animal has a \"daily cost\" to feed and " +
                             "upkeep their habitat. So, the larger the animal, the higher the daily cost will be to care for it."));
            Console.WriteLine();
            Console.WriteLine(ConvertStatementToConsoleLengthLines("There is also a daily veterinary bill based on the number of animals you have."));
            Console.WriteLine();
            Console.WriteLine("Press \"Enter\" to continue...");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine(instructionsHeader);
            Console.WriteLine();
            Console.WriteLine("<<< GIFT SHOP AND CONCESSIONS STANDS >>>");
            Console.WriteLine();
            Console.WriteLine(ConvertStatementToConsoleLengthLines("At the start of the game, the gift shop and concessions stands are empty. " +
                             "You can add, remove, and change the price of items whenever you'd like. The maximum price you can charge for an " +
                             "item is $100.00."));
            Console.WriteLine();
            Console.WriteLine(ConvertStatementToConsoleLengthLines("The concessions stands can hold 10 items, and the gift shop can hold 20. " +
                             "The more items your concessions stands or gift shop offer for sale, the higher the daily cost " +
                             "will be to operate them."));
            Console.WriteLine();
            Console.WriteLine(ConvertStatementToConsoleLengthLines("Every day, a certain percentage of the people who attended your park will " +
                             "buy items at the gift shop and concessions stands. The higher the price of an item, the less likely it is " +
                             "that someone will buy it."));
            Console.WriteLine();
            Console.WriteLine("Press \"Enter\" to continue...");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine(instructionsHeader);
            Console.WriteLine();
            Console.WriteLine("<<< ADVANCING THE DAY >>>");
            Console.WriteLine();
            Console.WriteLine(ConvertStatementToConsoleLengthLines("Each day, before the zoo opens, you are able to make changes. You can:"));
            Console.WriteLine();
            Console.WriteLine("- Change the ticket price");
            Console.WriteLine("- Buy, sell, or rearrange zoo animals");
            Console.WriteLine("- Add, remove, or change the price of gift shop and concessions items");
            Console.WriteLine();
            Console.WriteLine(ConvertStatementToConsoleLengthLines("Once you are satisfied with the changes you have made, use the"));
            Console.WriteLine(ConvertStatementToConsoleLengthLines("\"A - Advance Day\" option to advance the day."));
            Console.WriteLine();
            Console.WriteLine(ConvertStatementToConsoleLengthLines("After advancing the day, you will see how much money the zoo made or " +
                              "lost that day. You can also view an updated history of the zoo's daily attendance."));
            Console.WriteLine();
            Console.WriteLine("Press \"Enter\" to continue...");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine(instructionsHeader);
            Console.WriteLine();
            Console.WriteLine("<<< WINNING THE GAME >>>");
            Console.WriteLine();
            Console.WriteLine(ConvertStatementToConsoleLengthLines("To win the game, you must have at least $100,000 after you advance to Day 25."));
            Console.WriteLine();
            Console.WriteLine(ConvertStatementToConsoleLengthLines("If you don't have all of the money on Day 25, you'll lose the game."));
            Console.WriteLine();
            Console.WriteLine(ConvertStatementToConsoleLengthLines("Then, sadly, the zoo, the land, and all of the animals will be claimed by the bank, " +
                              "and your Great Aunt Gertrude's legacy will be tarnished forever..."));
            Console.WriteLine();
            Console.WriteLine("Let's hope that doesn't happen! You'd better get to work to save the zoo!");
            Console.WriteLine();
            Console.WriteLine("Press \"Enter\" to go to the main menu...");
            Console.ReadLine();
        }

        static string ConvertStatementToConsoleLengthLines(string originalStatement)
        {
            string convertedStatement = "";
            string tempStatement = "";
            int lineLengthMax = 72;
            int breakIndex = 0;
            int segmentCounter = 0;
            int counter = 0;

            if (originalStatement.Length <= lineLengthMax)
            {
                return originalStatement;
            }
            else
            {
                while (counter < originalStatement.Length)
                {
                    tempStatement = "";
                    for (int i = 0; (i < lineLengthMax && (i + (segmentCounter * lineLengthMax)) < originalStatement.Length); i++)
                    {
                        tempStatement += Convert.ToString(originalStatement[i + (segmentCounter * lineLengthMax)]);
                    }

                    for (int j = tempStatement.Length - 1; j >= 0; j--)
                    {
                        if (tempStatement[j] == ' ')
                        {
                            breakIndex = j;
                            break;
                        }
                    }

                    if (tempStatement.Length < lineLengthMax)
                    {
                        convertedStatement += tempStatement;
                        for (int l = 0; l < tempStatement.Length; l++)
                        {
                            counter++;
                        }
                    }
                    else
                    {
                        for (int k = 0; k < tempStatement.Length; k++)
                        {
                            if (k == breakIndex)
                            {
                                convertedStatement += "\r\n";
                                counter++;
                            }
                            else
                            {
                                convertedStatement += Convert.ToString(tempStatement[k]);
                                counter++;
                            }
                        }
                    }

                    segmentCounter++;
                }
            }
   
            return convertedStatement;
        }

        static void DisplayMainMenu(List<string> userOptions)
        {
            List<string> userOptionsText = new List<string>()
            { 
              "View Zoo Map",
              "Buy New Zoo Animals",
              "Arrange Zoo Animals",
              "Sell Zoo Animals",
              "Change Ticket Price",
              "Create/Edit Concessions",
              "Create/Edit Gift Shop Items",
              "Advance Day",
              "Instructions",
              "Exit"
            };

            Console.WriteLine("Please select an option from the menu below and press \"Enter\".");
            Console.WriteLine();
            Console.WriteLine("------ OPTIONS ------");
            Console.WriteLine();

            int i = 0;
            foreach (string userOption in userOptions)
            {
                Console.WriteLine(userOption + " - " + userOptionsText[i]);
                i++;
            }

            Console.WriteLine();
        }

        static List<Animal> GenerateZooAnimals()
        {
            Animal aardvarks = new Animal("Aardvark", 300.00m, 0, 45.00m, 12);
            Animal bearcats = new Animal("Bearcat", 750.00m, 0, 40.00m, 47);
            Animal cheetahs = new Animal("Cheetah", 3000.00m, 0, 275.00m, 200);
            Animal chinchillas = new Animal("Chinchilla", 250.00m, 0, 7.00m, 9);
            Animal dolphins = new Animal("Dolphin", 5500.00m, 0, 400.00m, 375);
            Animal giantTortoises = new Animal("Giant Tortoise", 400.00m, 0, 7.00m, 22);
            Animal giraffes = new Animal("Giraffe", 4500.00m, 0, 300.00m, 300);
            Animal hippos = new Animal("Hippopotamus", 14000.00m, 0, 600.00m, 1050);
            Animal iguanas = new Animal("Iguana", 150.00m, 0, 3.00m, 5);
            Animal kiaBirds = new Animal("Kia Bird", 200.00m, 0, 14.00m, 7);
            Animal lemurs = new Animal("Lemur", 600.00m, 0, 45.00m, 36);
            Animal lions = new Animal("Lion", 12500.00m, 0, 250.00m, 900);
            Animal macaws = new Animal("Macaw", 275.00m, 0, 8.00m, 10);
            Animal penguins = new Animal("Penguin", 500.00m, 0, 25.00m, 29);
            Animal polarBears = new Animal("Polar Bear", 16000.00m, 0, 625.00m, 1225);
            Animal rattlesnakes = new Animal("Rattle Snake", 100.00m, 0, 10.00m, 3);
            Animal rhinos = new Animal("Rhinoceros", 10500.00m, 0, 300.00m, 750);
            Animal seaLions = new Animal("Sea Lion", 2500.00m, 0, 450.00m, 165);
            Animal sugarGliders = new Animal("Sugar Glider", 125.00m, 0, 10.00m, 4);
            Animal wolves = new Animal("Wolf", 3500.00m, 0, 240.00m, 235);

            List<Animal> allAnimals = new List<Animal>() { aardvarks, bearcats, cheetahs, chinchillas, dolphins, giantTortoises,
                                                             giraffes, hippos, iguanas, kiaBirds, lemurs, lions, macaws, penguins,
                                                             polarBears, rattlesnakes, rhinos, seaLions, sugarGliders, wolves};

            return allAnimals;
        }
        
        static PhysicalSpace[] GeneratePhysicalSpaces()
        {
            PhysicalSpace A = new PhysicalSpace("A", 1, 5);
            PhysicalSpace B = new PhysicalSpace("B", 1, 4);
            PhysicalSpace C = new PhysicalSpace("C", 1, 3);
            PhysicalSpace D = new PhysicalSpace("D", 1, 2);
            PhysicalSpace FoodCourt1 = new PhysicalSpace("Food Court 1", 1, 1, "Food1");
            PhysicalSpace E = new PhysicalSpace("E", 2, 1);
            PhysicalSpace F = new PhysicalSpace("F", 3, 1);
            PhysicalSpace G = new PhysicalSpace("G", 4, 1);
            PhysicalSpace H = new PhysicalSpace("H", 5, 1);
            PhysicalSpace FoodCourt2 = new PhysicalSpace("Food Court 2", 1, 6, "Food2");
            PhysicalSpace I = new PhysicalSpace("I", 6, 2);
            PhysicalSpace J = new PhysicalSpace("J", 6, 3);
            PhysicalSpace K = new PhysicalSpace("K", 6, 4);
            PhysicalSpace L = new PhysicalSpace("L", 3, 5);
            PhysicalSpace M = new PhysicalSpace("M", 3, 4);
            PhysicalSpace N = new PhysicalSpace("N", 3, 3);
            PhysicalSpace O = new PhysicalSpace("O", 4, 3);
            PhysicalSpace P = new PhysicalSpace("P", 4, 4);
            PhysicalSpace GiftShop = new PhysicalSpace("GiftShop", 10, 10, "Gifts");
            PhysicalSpace Blank = new PhysicalSpace("Blank", 10, 10);

            PhysicalSpace[] allPhysicalSpaces = new PhysicalSpace[] { A, B, C, D, FoodCourt1, E, F, G, H, FoodCourt2,
                                                                        I, J, K, L, M, N, O, P, GiftShop, Blank };

            return allPhysicalSpaces;
        }
        static string GetUserInput(List<string> possibleMenuOptions)
        {
            string userInput = Console.ReadLine().ToUpper();

            while (!possibleMenuOptions.Contains(userInput))
            {
                Console.WriteLine();
                Console.WriteLine("Invalid entry. Please try again.");
                userInput = Console.ReadLine().ToUpper();
            }

            return userInput;
        }

        static void DisplayDashboard(int[] dayNumber, decimal[] cashOnHand, int[] totalAttractionScore)
        {
            Console.WriteLine("-----------------------------");
            Console.WriteLine($"Day Number: {dayNumber[0]}");
            Console.WriteLine($"Cash: {cashOnHand[0]:C2}");
            Console.WriteLine($"Zoo Attraction Score: {totalAttractionScore[0]:N0}");
            Console.WriteLine("-----------------------------");
            Console.WriteLine();
        }

        static void DisplayZooMap(PhysicalSpace[] allPhysicalSpaces, int[] dayNumber, decimal[] cashOnHand, int[] totalAttractionScore)
        {
            Console.Clear();
            DisplayDashboard(dayNumber, cashOnHand, totalAttractionScore);
            Console.WriteLine("------------------- CURRENT ZOO MAP -------------------\r\n");
            Console.WriteLine(ZooMapIllustrator(allPhysicalSpaces));
            Console.WriteLine();
            Console.WriteLine("Press \"Enter\" to return to the main menu...");
            Console.ReadLine();
        }

        static void DisplayZooMapLite(PhysicalSpace[] allPhysicalSpaces, bool showMapTitle)
        {
            if (showMapTitle)
            {
                Console.WriteLine("------------------- CURRENT ZOO MAP -------------------\r\n");
            }
            Console.WriteLine(ZooMapIllustrator(allPhysicalSpaces));
            Console.WriteLine();
        }

        static string ZooMapIllustrator(PhysicalSpace[] allPhysicalSpaces)
        {
            string[] square1_5 = SquareSpaceIllustrator(allPhysicalSpaces[0], top: true, right: true, bottom: true, left: true);
            string[] square1_4 = SquareSpaceIllustrator(allPhysicalSpaces[1], top: true, right: true, bottom: true, left: true);
            string[] square1_3 = SquareSpaceIllustrator(allPhysicalSpaces[2], top: true, right: true, bottom: true, left: true);
            string[] square1_2 = SquareSpaceIllustrator(allPhysicalSpaces[3], top: true, right: true, bottom: true, left: true);
            string[] square1_1 = SquareSpaceIllustrator(allPhysicalSpaces[4], top: true, right: true, bottom: true, left: true);
            string[] square2_5 = SquareSpaceIllustrator(allPhysicalSpaces[19], top: false, right: false, bottom: false, left: false);
            string[] square2_4 = SquareSpaceIllustrator(allPhysicalSpaces[19], top: false, right: false, bottom: false, left: false);
            string[] square2_3 = SquareSpaceIllustrator(allPhysicalSpaces[19], top: false, right: false, bottom: false, left: false);
            string[] square2_2 = SquareSpaceIllustrator(allPhysicalSpaces[19], top: false, right: false, bottom: false, left: false);
            string[] square2_1 = SquareSpaceIllustrator(allPhysicalSpaces[5], top: true, right: true, bottom: true, left: true);
            string[] square3_5 = SquareSpaceIllustrator(allPhysicalSpaces[13], top: true, right: true, bottom: true, left: true);
            string[] square3_4 = SquareSpaceIllustrator(allPhysicalSpaces[14], top: true, right: true, bottom: true, left: true);
            string[] square3_3 = SquareSpaceIllustrator(allPhysicalSpaces[15], top: true, right: true, bottom: true, left: true);
            string[] square3_2 = SquareSpaceIllustrator(allPhysicalSpaces[19], top: false, right: false, bottom: false, left: false);
            string[] square3_1 = SquareSpaceIllustrator(allPhysicalSpaces[6], top: true, right: true, bottom: true, left: true);
            string[] square4_5 = SquareSpaceIllustrator(allPhysicalSpaces[18], top: true, right: false, bottom: true, left: true);
            string[] square4_4 = SquareSpaceIllustrator(allPhysicalSpaces[17], top: true, right: true, bottom: true, left: true);
            string[] square4_3 = SquareSpaceIllustrator(allPhysicalSpaces[16], top: true, right: true, bottom: true, left: true);
            string[] square4_2 = SquareSpaceIllustrator(allPhysicalSpaces[19], top: false, right: false, bottom: false, left: false);
            string[] square4_1 = SquareSpaceIllustrator(allPhysicalSpaces[7], top: true, right: true, bottom: true, left: true);
            string[] square5_5 = SquareSpaceIllustrator(allPhysicalSpaces[18], top: true, right: false, bottom: true, left: false);
            string[] square5_4 = SquareSpaceIllustrator(allPhysicalSpaces[19], top: false, right: false, bottom: false, left: false);
            string[] square5_3 = SquareSpaceIllustrator(allPhysicalSpaces[19], top: false, right: false, bottom: false, left: false);
            string[] square5_2 = SquareSpaceIllustrator(allPhysicalSpaces[19], top: false, right: false, bottom: false, left: false);
            string[] square5_1 = SquareSpaceIllustrator(allPhysicalSpaces[8], top: true, right: true, bottom: true, left: true);
            string[] square6_5 = SquareSpaceIllustrator(allPhysicalSpaces[18], top: true, right: true, bottom: true, left: false);
            string[] square6_4 = SquareSpaceIllustrator(allPhysicalSpaces[12], top: true, right: true, bottom: true, left: true);
            string[] square6_3 = SquareSpaceIllustrator(allPhysicalSpaces[11], top: true, right: true, bottom: true, left: true);
            string[] square6_2 = SquareSpaceIllustrator(allPhysicalSpaces[10], top: true, right: true, bottom: true, left: true);
            string[] square6_1 = SquareSpaceIllustrator(allPhysicalSpaces[9], top: true, right: true, bottom: true, left: true);

            string[][] row5StringArrays = new string[][] { square1_5, square2_5, square3_5, square4_5, square5_5, square6_5 };
            string[][] row4StringArrays = new string[][] { square1_4, square2_4, square3_4, square4_4, square5_4, square6_4 };
            string[][] row3StringArrays = new string[][] { square1_3, square2_3, square3_3, square4_3, square5_3, square6_3 };
            string[][] row2StringArrays = new string[][] { square1_2, square2_2, square3_2, square4_2, square5_2, square6_2 };
            string[][] row1StringArrays = new string[][] { square1_1, square2_1, square3_1, square4_1, square5_1, square6_1 };

            string[][][] allArrays = new string[][][] { row5StringArrays, row4StringArrays, row3StringArrays, row2StringArrays, row1StringArrays };

            string wholeMapIllustration = "";

            for (int i = 0; i < allArrays.Length; i++)
            {
                for (int k = 0; k < 4; k++)
                {
                    for (int j = 0; j < allArrays[i].Length; j++)
                    {
                        wholeMapIllustration += allArrays[i][j][k];
                    }

                    wholeMapIllustration += "\r\n";
                }
            }

            return wholeMapIllustration;
        }

        static string[] SquareSpaceIllustrator(PhysicalSpace physicalSpace, bool top = false, bool right = false, bool bottom = false, bool left = false)
        {
            string[] spaceIllustration = new string[4];
            string row2 = "";
            string row3 = "";
            string originalString = "";
            string tempString = "";

            if (top)
            {
                spaceIllustration[0] = "_________";
            }
            else
            {
                spaceIllustration[0] = "         ";
            }

            if (physicalSpace.OccupyingAnimals == "" && physicalSpace.SpaceID != "Blank")
            {
                originalString = physicalSpace.SpaceID;
            }
            else
            {
                originalString = physicalSpace.OccupyingAnimals;
            }

            if (originalString == "")
            {
                if (left && right)
                {
                    row2 = "|       |";
                }
                else if (left)
                {
                    row2 = "|        ";
                }
                else if (right)
                {
                    row2 = "        |";
                }
                else
                {
                    row2 = "         ";
                }
            }
            else if (originalString.Length > 5)
            {
                for (int i = 0; i < originalString.Length; i++)
                {
                    if (tempString.Length < 4)
                    {
                        tempString += originalString[i].ToString();
                    }
                }
                tempString += ".";

                if (left && right)
                {
                    row2 = "| " + tempString + " |";
                }
                else if (left)
                {
                    row2 = "| " + tempString + "  ";
                }
                else if (right)
                {
                    row2 = "  " + tempString + " |";
                }
                else
                {
                    row2 = "  " + tempString + "  ";
                }                
            }
            else if (originalString.Length == 5)
            {
                if (left && right)
                {
                    row2 = "| " + originalString + " |";
                }
                else if (left)
                {
                    row2 = "| " + originalString + "  ";
                }
                else if (right)
                {
                    row2 = "  " + originalString + " |";
                }
                else
                {
                    row2 = "  " + originalString + "  ";
                }
            }
            else if (originalString.Length == 1)
            {
                if (left && right)
                {
                    row2 = "|   " + originalString + "   |";
                }
                else if (left)
                {
                    row2 = "|   " + originalString + "    ";
                }
                else if (right)
                {
                    row2 = "    " + originalString + "   |";
                }
                else
                {
                    row2 = "    " + originalString + "    ";
                }
            }
            else if (originalString.Length == 2)
            {
                if (left && right)
                {
                    row2 = "|   " + originalString + "  |";
                }
                else if (left)
                {
                    row2 = "|   " + originalString + "   ";
                }
                else if (right)
                {
                    row2 = "    " + originalString + "  |";
                }
                else
                {
                    row2 = "    " + originalString + "   ";
                }
            }
            else if (originalString.Length == 3)
            {
                if (left && right)
                {
                    row2 = "|  " + originalString + "  |";
                }
                else if (left)
                {
                    row2 = "|  " + originalString + "   ";
                }
                else if (right)
                {
                    row2 = "   " + originalString + "  |";
                }
                else
                {
                    row2 = "   " + originalString + "   ";
                }
            }
            else if (originalString.Length == 4)
            {
                if (left && right)
                {
                    row2 = "|  " + originalString + " |";
                }
                else if (left)
                {
                    row2 = "|  " + originalString + "  ";
                }
                else if (right)
                {
                    row2 = "   " + originalString + " |";
                }
                else
                {
                    row2 = "   " + originalString + "  ";
                }
            }

            spaceIllustration[1] = row2;

            if (physicalSpace.AnimalQuantity == 0)
            {
                if (left & right)
                {
                    row3 = "|       |";
                }
                else if (left)
                {
                    row3 = "|        ";
                }
                else if (right)
                {
                    row3 = "        |";
                }
                else
                {
                    row3 = "         ";
                }
            }
            else if (physicalSpace.AnimalQuantity > 0 && physicalSpace.AnimalQuantity < 10)
            {
                if (left & right)
                {
                    row3 = $"| ({physicalSpace.AnimalQuantity})   |";
                }
                else if (left)
                {
                    row3 = $"| ({physicalSpace.AnimalQuantity})    ";
                }
                else if (right)
                {
                    row3 = $"  ({physicalSpace.AnimalQuantity})   |";
                }
                else
                {
                    row3 = $"  ({physicalSpace.AnimalQuantity})    ";
                }
            }
            else if (physicalSpace.AnimalQuantity >= 10 && physicalSpace.AnimalQuantity < 100)
            {
                if (left & right)
                {
                    row3 = $"| ({physicalSpace.AnimalQuantity})  |";
                }
                else if (left)
                {
                    row3 = $"| ({physicalSpace.AnimalQuantity})   ";
                }
                else if (right)
                {
                    row3 = $"  ({physicalSpace.AnimalQuantity})  |";
                }
                else
                {
                    row3 = $"  ({physicalSpace.AnimalQuantity})   ";
                }
            }
            else if (physicalSpace.AnimalQuantity >= 100 && physicalSpace.AnimalQuantity < 1000)
            {
                if (left & right)
                {
                    row3 = $"| ({physicalSpace.AnimalQuantity}) |";
                }
                else if (left)
                {
                    row3 = $"| ({physicalSpace.AnimalQuantity})  ";
                }
                else if (right)
                {
                    row3 = $"  ({physicalSpace.AnimalQuantity}) |";
                }
                else
                {
                    row3 = $"  ({physicalSpace.AnimalQuantity})  ";
                }
            }

            spaceIllustration[2] = row3;

            if (bottom && right && left)
            {
                spaceIllustration[3] = "|_______|";
            }
            else if (bottom && left)
            {
                spaceIllustration[3] = "|________";
            }
            else if (bottom && right)
            {
                spaceIllustration[3] = "________|";
            }
            else if (bottom)
            {
                spaceIllustration[3] = "_________";
            }
            else
            {
                spaceIllustration[3] = "         ";
            }

            return spaceIllustration;
        }

        static void ChangeTicketPrice(decimal[] ticketPrice, int[] dayNumber, decimal[] cashOnHand, int[] totalAttractionScore)
        {
            bool exit = false;

            do
            {
                

                Console.Clear();
                DisplayDashboard(dayNumber, cashOnHand, totalAttractionScore);
                Console.WriteLine("------------------ CHANGE TICKET PRICE ------------------");
                Console.WriteLine();
                Console.WriteLine($"The current ticket price is: {ticketPrice[0]:C2}");
                Console.WriteLine();
                Console.WriteLine("Enter new ticket price, or \"E\" to exit:");
                string newTicketPriceString = Console.ReadLine().ToUpper();

                decimal newTicketPrice;

                while ((!Decimal.TryParse(newTicketPriceString, out newTicketPrice) || newTicketPrice < 0 || newTicketPrice > 50 ||
                       ((newTicketPrice * 100) - Math.Floor(newTicketPrice * 100)) > 0) && newTicketPriceString != "E")
                {
                    Console.WriteLine();
                    Console.WriteLine(ConvertStatementToConsoleLengthLines("Invalid entry. Please enter a dollar amount that is $50.00 or less (ex: 3.00), or \"E\" to exit:"));
                    newTicketPriceString = Console.ReadLine().ToUpper();
                }

                if (newTicketPriceString == "E")
                {
                    exit = true;
                    break;
                }

                ticketPrice[0] = newTicketPrice;
                Console.WriteLine();
                Console.WriteLine($"Success! The ticket price was updated to {newTicketPrice:C2}");
                Console.WriteLine();
                Console.WriteLine("Press \"Enter\" to continue...");
                Console.ReadLine();
            }
            while (!exit);
        }
        
        static void BuyNewAnimals(PhysicalSpace[] allPhysicalSpaces, List<Animal> allAnimals, int[] dayNumber, decimal[] cashOnHand, int[] totalAttractionScore)
        {            
            bool exitMenu = false;
            bool animalsNotPlaced;
            int optionNumberInt = 0;
            string quantityString = "";
            int quantityInt;
            decimal totalCost;
            string spaceOption;
            int lineCounter;

            do
            {
                Console.Clear();
                animalsNotPlaced = true;
                bool showMapTitle = true;
                spaceOption = "";
                lineCounter = 1;
                totalCost = 0;
                quantityInt = -1;

                DisplayDashboard(dayNumber, cashOnHand, totalAttractionScore);
                Console.WriteLine("-------------------------- BUY NEW ANIMALS --------------------------");
                Console.WriteLine();
                Console.WriteLine("                                  ATTRACTION    DAILY    CURRENTLY");
                Console.WriteLine("#   NAME                PRICE       SCORE        COST       OWN");
                Console.WriteLine();

                foreach (Animal animal in allAnimals)
                {
                    //Create the dollarAmtSpacer
                    decimal price = animal.Price;
                    string dollarAmtSpacer = (price >= 1000 && price < 10000) ? " " :
                                                (price >= 100 && price < 1000) ? "   " :
                                                    (price >= 10 && price < 100) ? "    " :
                                                        (price >= 0 && price < 10) ? "     " : "";

                    //Create the nameSpacer
                    string nameSpacer = "";
                    int standardNameLength = 18;
                    int nameSpacerLength = standardNameLength - animal.Name.Length;

                    for (int i = 0; i < nameSpacerLength; i++)
                    {
                        nameSpacer += " ";
                    }

                    //Create other spacers
                    string animalDailyCostSpacer = animal.DailyCost < 10 ? "  " : animal.DailyCost < 100 ? " " : "";
                    string currentlyOwnSpacer = animal.Quantity < 10 ? "  " : animal.Quantity < 100 ? " " : "";
                    string lineCounterSpacer = lineCounter < 10 ? "  " : " ";
                    string attractionValueSpacer = animal.AttractionValue < 10 ? "       " : animal.AttractionValue < 100 ? "      " : animal.AttractionValue < 1000 ? "     " : "   ";

                    //Write line listing animal to the console
                    Console.WriteLine($"{lineCounter}.{lineCounterSpacer}{animal.Name}{nameSpacer}{dollarAmtSpacer}{animal.Price:C0}" +
                                      $"\t {attractionValueSpacer}{animal.AttractionValue:N0}\t {animalDailyCostSpacer}" +
                                      $"{animal.DailyCost:C0}{currentlyOwnSpacer}      {animal.Quantity}");
                   
                    lineCounter++;
                }
                Console.WriteLine();
                Console.WriteLine("Enter the number of the animal you wish to buy, or \"E\" to exit:");

                int totalPensFull = 0;
                bool pensAreFull = false;
                List<Animal> buyableAnimals = new List<Animal>();
                List<string> allAnimalOptions = new List<string>();
                List<string> buyableAnimalOptions = new List<string>();

                for (int i = 0; i < allAnimals.Count; i++)
                {
                    allAnimalOptions.Add((i + 1).ToString());

                    if (allAnimals[i].Quantity > 0)
                    {
                        totalPensFull++;
                        buyableAnimals.Add(allAnimals[i]);
                        buyableAnimalOptions.Add((i + 1).ToString());
                    }
                }

                if (totalPensFull == 16)
                {
                    pensAreFull = true;
                }

                allAnimalOptions.Add("E");
                buyableAnimalOptions.Add("E");
                string userOption = GetUserInput(allAnimalOptions);

                if (pensAreFull && !buyableAnimalOptions.Contains(userOption))
                {
                    Console.WriteLine();
                    Console.WriteLine(ConvertStatementToConsoleLengthLines("All of the animal exhibits are currently full, so you can't buy a new " +
                                      "type of animal right now. But, you can still add more animals to your current animal exhibits."));
                    Console.WriteLine();
                    Console.WriteLine(ConvertStatementToConsoleLengthLines("To buy a new type of animal, you'll have to sell some animals to " +
                                      "create an empty exhibit."));
                    Console.WriteLine();
                    Console.WriteLine("Press \"Enter\" to continue...");
                    Console.ReadLine();
                    continue;
                }
                else if (userOption == "E")
                {
                    break;
                }
                else
                {
                    optionNumberInt = Int32.Parse(userOption);
                }

                Console.WriteLine($"Enter the quantity of " + allAnimals[optionNumberInt - 1].Name.Pluralize() + " you wish to buy, or \"C\" to cancel purchase:");
                quantityString = Console.ReadLine().ToUpper();
                Console.WriteLine();

                while ((!Int32.TryParse(quantityString, out quantityInt) || quantityInt <= 0) && quantityString != "C")
                {
                    Console.WriteLine("Invalid entry. Please try again.");
                    quantityString = Console.ReadLine().ToUpper();
                    Console.WriteLine();
                }

                if (quantityString == "C")
                {
                    continue;
                }

                totalCost = allAnimals[optionNumberInt - 1].Price * quantityInt;

                if (cashOnHand[0] < totalCost)
                {
                    Console.WriteLine("Uh oh! You don't have enough money to make this purchase. Please start a new purchase.");
                    Console.WriteLine();
                    Console.WriteLine("Press \"Enter\" to continue...");
                    Console.ReadLine();
                }
                else
                {
                    cashOnHand[0] -= totalCost;
                    allAnimals[optionNumberInt - 1].Buy(quantityInt);
                    totalAttractionScore[0] += allAnimals[optionNumberInt - 1].AttractionValue * quantityInt;

                    string animalName = quantityInt != 1 ? allAnimals[optionNumberInt - 1].Name.Pluralize() : allAnimals[optionNumberInt - 1].Name;
                    string wereOrWas = quantityInt != 1 ? "were" : "was";


                    if ((allAnimals[optionNumberInt - 1].Quantity - quantityInt) == 0)
                    {
                        Console.Clear();
                        Console.WriteLine($"Success! You just purchased {quantityString} {allAnimals[optionNumberInt - 1].Name.Pluralize()}!");
                        Console.WriteLine();
                        DisplayZooMapLite(allPhysicalSpaces, showMapTitle);
                        Console.WriteLine($"Now, where would you like to put the {animalName}?\r\nEnter a space (A-P) from the Zoo Map.");
                    }
                    else
                    {
                        Console.WriteLine($"Success! You just purchased {quantityString} {animalName}!");
                        Console.WriteLine();
                        Console.WriteLine(ConvertStatementToConsoleLengthLines($"Each type of animal can only live in one exhibit. Because you " +
                                          $"already have a {allAnimals[optionNumberInt - 1].Name} exhibit," +
                                          $"your new {animalName} {wereOrWas} added to exhibit {allAnimals[optionNumberInt - 1].Location}."));
                        
                        int indexOfPhysicalSpace = 0;
                        for (int i = 0; i < allPhysicalSpaces.Length; i++)
                        {
                            if (allPhysicalSpaces[i].SpaceID == allAnimals[optionNumberInt - 1].Location)
                            {
                                indexOfPhysicalSpace = i;
                                break;
                            }
                            else
                            {
                                continue;
                            }
                        }
                        
                        allPhysicalSpaces[indexOfPhysicalSpace].AnimalQuantity += quantityInt;
                        Console.WriteLine();
                        Console.WriteLine("Press \"Enter\" to continue...");
                        Console.ReadLine();
                        animalsNotPlaced = false;
                        continue;
                    }

                    Console.WriteLine();

                    List<string> spaceOptions = new List<string>() { "A", "B", "C", "D", "", "E", "F", "G", "H", "", "I", "J", "K",
                                                                     "L", "M", "N", "O", "P", "", "" };

                    spaceOption = GetUserInput(spaceOptions);

                    do
                    {                        
                        if (allPhysicalSpaces[spaceOptions.IndexOf(spaceOption)].OccupyingAnimals == "" && allAnimals[optionNumberInt - 1].Quantity == quantityInt)
                        {
                            allPhysicalSpaces[spaceOptions.IndexOf(spaceOption)].OccupyingAnimals = allAnimals[optionNumberInt - 1].Name;
                            allPhysicalSpaces[spaceOptions.IndexOf(spaceOption)].AnimalQuantity = quantityInt;
                            allAnimals[optionNumberInt - 1].Location = allPhysicalSpaces[spaceOptions.IndexOf(spaceOption)].SpaceID;

                            Console.WriteLine($"Success! You just placed your new {animalName} in exhibit {spaceOption}.");
                            Console.WriteLine();
                            Console.WriteLine("Press \"Enter\" to continue...");
                            Console.ReadLine();
                            animalsNotPlaced = false;
                        }
                        else if (allPhysicalSpaces[spaceOptions.IndexOf(spaceOption)].OccupyingAnimals != "" && allPhysicalSpaces[spaceOptions.IndexOf(spaceOption)].OccupyingAnimals == allAnimals[optionNumberInt - 1].Name)
                        {
                            allPhysicalSpaces[spaceOptions.IndexOf(spaceOption)].AnimalQuantity += quantityInt;
                            allAnimals[optionNumberInt - 1].Location = allPhysicalSpaces[spaceOptions.IndexOf(spaceOption)].SpaceID;

                            Console.WriteLine($"Success! You just added your new {animalName} to exhibit {spaceOption}.");

                            Console.WriteLine();
                            Console.WriteLine("Press \"Enter\" to continue...");
                            Console.ReadLine();
                            animalsNotPlaced = false;
                        }
                        else
                        {
                            Console.WriteLine(ConvertStatementToConsoleLengthLines("Hmm, that exhibit is already taken by a different type of animal. Select a different space for these animals."));
                            spaceOption = Console.ReadLine().ToUpper();
                            Console.WriteLine();

                            while (!spaceOptions.Contains(spaceOption))
                            {
                                Console.WriteLine("Invalid entry. Please try again.");
                                spaceOption = Console.ReadLine().ToUpper();
                                Console.WriteLine();
                            }
                        }
                    }
                    while (animalsNotPlaced);
                }                   

            }
            while (!exitMenu);
        }

        static void ArrangeAnimals(PhysicalSpace[] allPhysicalSpaces, List<Animal> allAnimals, int[] dayNumber, decimal[] cashOnHand, int[] totalAttractionScore)
        {
            bool exit = false;
            bool showMapTitle = false;
            int counter;
            string userOptionAnimalToMove;
            string userOptionDestination;
            string nameSpacer;
            int standardNameLength = 18;
            int nameSpacerLength;
            int totalNumberOfAnimals;
            string selectedName;
            int selectedQuantity;
            string startingAnimalLocation;
            bool transferComplete;

            do
            {
                counter = 1;
                userOptionAnimalToMove = "";
                userOptionDestination = "";
                nameSpacer = "";
                nameSpacerLength = 0;
                totalNumberOfAnimals = 0;
                transferComplete = false;
                List<string> validUserOptions = new List<string>();
                List<Animal> selectableAnimals = new List<Animal>();

                Console.Clear();
                DisplayDashboard(dayNumber, cashOnHand, totalAttractionScore);

                foreach (Animal animal in allAnimals)
                {
                    totalNumberOfAnimals += animal.Quantity;
                }

                if (totalNumberOfAnimals == 0)
                {
                    Console.WriteLine("------------------ ARRANGE ZOO ANIMALS ------------------");
                    Console.WriteLine();
                    Console.WriteLine("It looks like you don't have any animals right now.\r\nCome back after you've purchased some!");
                    Console.WriteLine();
                    Console.WriteLine("Press \"Enter\" to continue...");
                    Console.ReadLine();
                    return;
                }

                int totalPensFull = 0;
                foreach (Animal animal in allAnimals)
                {
                    if (animal.Quantity > 0)
                    {
                        totalPensFull++;
                    }
                }

                if (totalPensFull == 16)
                {
                    Console.WriteLine("------------------ ARRANGE ZOO ANIMALS ------------------");
                    Console.WriteLine();
                    DisplayZooMapLite(allPhysicalSpaces, showMapTitle);
                    Console.WriteLine();
                    Console.WriteLine("Hmm, all of the animal exhibits are currently full.");
                    Console.WriteLine();
                    Console.WriteLine(ConvertStatementToConsoleLengthLines("If you'd like to arrange the zoo animals, " +
                                      "you'll need to sell some animals to create an empty exhibit first."));
                    Console.WriteLine();
                    Console.WriteLine("Press \"Enter\" to continue...");
                    Console.ReadLine();
                    return;
                }

                Console.WriteLine("------------------ ARRANGE ZOO ANIMALS ------------------");
                Console.WriteLine();
                DisplayZooMapLite(allPhysicalSpaces, showMapTitle);

                foreach (Animal animal in allAnimals)
                {
                    if (animal.Quantity > 0)
                    {
                        selectableAnimals.Add(animal);
                        nameSpacer = "";
                        nameSpacerLength = standardNameLength - animal.Name.Length;

                        for (int i = 0; i < nameSpacerLength; i++)
                        {
                            nameSpacer += " ";
                        }

                        string numberSpacer = selectableAnimals.Count < 10 ? "  " : " ";

                        Console.WriteLine($"{counter}.{numberSpacer}{animal.Name}{nameSpacer}Qty: {animal.Quantity:N0}\t   Current Location: {animal.Location}");
                        validUserOptions.Add(counter.ToString());
                        counter++;
                    }
                }
                
                validUserOptions.Add("E");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Enter the option for the animal(s) you would like to move, or \"E\" to exit:");
                userOptionAnimalToMove = GetUserInput(validUserOptions);

                if (userOptionAnimalToMove == "E")
                {
                    exit = true;
                    break;
                }

                selectedName = selectableAnimals[Convert.ToInt32(userOptionAnimalToMove)-1].Name;
                selectedQuantity = selectableAnimals[Convert.ToInt32(userOptionAnimalToMove)-1].Quantity;
                startingAnimalLocation = selectableAnimals[Convert.ToInt32(userOptionAnimalToMove)-1].Location;

                if (selectedQuantity != 1)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Where would you like to move your {selectedQuantity} {selectedName}s?");
                    Console.WriteLine("Enter a space (A-P) from the Zoo Map:");
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine($"Where would you like to move your {selectedName}?");
                    Console.WriteLine("Enter a space (A-P) from the Zoo Map:");
                }

                List<string> validSpaceOptions = new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H", "", "I", "J", "K",
                                                                      "L", "M", "N", "O", "P" };
                List<string> spaceIndexesFinder = new List<string>() { "A", "B", "C", "D", "", "E", "F", "G", "H", "", "I", "J", "K",
                                                                       "L", "M", "N", "O", "P", "", "" };

                do
                {
                    userOptionDestination = GetUserInput(validSpaceOptions);

                    if (allPhysicalSpaces[spaceIndexesFinder.IndexOf(userOptionDestination)].AnimalQuantity > 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Hm, there are already animals in that space. Please pick another location.");
                    }
                    else
                    {
                        allPhysicalSpaces[spaceIndexesFinder.IndexOf(userOptionDestination)].OccupyingAnimals = selectedName;
                        allPhysicalSpaces[spaceIndexesFinder.IndexOf(userOptionDestination)].AnimalQuantity = selectedQuantity;
                        allPhysicalSpaces[spaceIndexesFinder.IndexOf(startingAnimalLocation)].OccupyingAnimals = "";
                        allPhysicalSpaces[spaceIndexesFinder.IndexOf(startingAnimalLocation)].AnimalQuantity = 0;
                        selectableAnimals[Convert.ToInt32(userOptionAnimalToMove)-1].Location =
                            allPhysicalSpaces[spaceIndexesFinder.IndexOf(userOptionDestination)].SpaceID;
                        Console.WriteLine();

                        if (selectedQuantity != 1)
                        {
                            Console.WriteLine("Success! You just moved your " + selectedQuantity + " " + selectedName.Pluralize() + " to exhibit " +
                                                selectableAnimals[Convert.ToInt32(userOptionAnimalToMove)-1].Location + ".");
                        }
                        else
                        {
                            Console.WriteLine("Success! You just moved your " + selectedName + " to exhibit " +
                                                selectableAnimals[Convert.ToInt32(userOptionAnimalToMove)-1].Location + ".");
                        }

                        Console.WriteLine();
                        Console.WriteLine("Press \"Enter\" to continue");
                        Console.ReadLine();
                        transferComplete = true;
                    }
                }
                while (!transferComplete);
            }
            while (!exit);
        }

        static void EditConcessionsItems(List<Sundry> allConcessionsItems, int[] dayNumber, decimal[] cashOnHand, int[] totalAttractionScore)
        {
            List<string> validOptions = new List<string>() { "1", "2", "3", "E" };
            List<string> optionsText = new List<string>()
            {
                "Add New Item",
                "Delete Item",
                "Change Price",
                "Exit"
            };
            string userInput;
            bool exit = false;
            string newItemNameOrSelection;
            decimal newItemPrice;
            string newItemPriceString;
            string confirmChoice;

            do
            {
                newItemNameOrSelection = "";
                newItemPrice = 0;
                newItemPriceString = "";
                Console.Clear();
                DisplayDashboard(dayNumber, cashOnHand, totalAttractionScore);
                Console.WriteLine("----------- CHANGE CONCESSIONS STAND ITEMS -----------");
                DisplayCurrentSundries(allConcessionsItems);
                DisplaySundriesOptionsMenu(validOptions, optionsText);
                Console.WriteLine("Enter an option from the menu, or \"E\" to exit:");
                userInput = GetUserInput(validOptions);
                Console.WriteLine();

                if (userInput == "E")
                {
                    exit = true;
                }
                else if ((userInput == "2" || userInput == "3") && allConcessionsItems.Count == 0)
                {
                    Console.WriteLine("Oops! You don't have any items in the Concessions Stand yet.");
                    Console.WriteLine("Add an item in order to edit it.");
                    Console.WriteLine();
                    Console.WriteLine("Press \"Enter\" to continue...");
                    Console.ReadLine();
                }
                else if (userInput == "1" && allConcessionsItems.Count < 10)
                {
                    Console.WriteLine("--- ADD A NEW ITEM ---");
                    Console.WriteLine("Enter the name of the new item:");
                    newItemNameOrSelection = Console.ReadLine();

                    while (newItemNameOrSelection.Length > 30)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Invalid entry. Please enter an item name that is 30 characters or less.");
                        newItemNameOrSelection = Console.ReadLine();
                    }

                    Console.WriteLine();
                    Console.WriteLine("Enter the price of the new item:");
                    newItemPriceString = Console.ReadLine();

                    while (!Decimal.TryParse(newItemPriceString, out newItemPrice) || newItemPrice <= 0 || newItemPrice > 100 ||
                           ((newItemPrice * 100) - Math.Floor(newItemPrice * 100)) > 0 )
                    {
                        Console.WriteLine();
                        Console.WriteLine("Invalid price. Please enter a positive dollar amount that is $100.00 or less (ex: 3.00):");
                        newItemPriceString = Console.ReadLine();
                    }

                    allConcessionsItems.Add(new Sundry(newItemNameOrSelection, newItemPrice));
                    Console.WriteLine();
                    Console.WriteLine($"Success! The item {newItemNameOrSelection} was added to the concessions stand!");
                    Console.WriteLine();
                    Console.WriteLine("Press \"Enter\" to continue...");
                    Console.ReadLine();
                }
                else if (userInput == "1" && allConcessionsItems.Count == 10)
                {
                    Console.WriteLine("Unfortuantely, your concessions stands can only stock a maximum of 10 items.");
                    Console.WriteLine();
                    Console.WriteLine("If you'd like to add a new item, delete an existing item first.");
                    Console.WriteLine();
                    Console.WriteLine("Press \"Enter\" to continue...");
                    Console.ReadLine();

                }
                else if (userInput == "2")
                {
                    List<string> validItemOptions = new List<string>();
                    int counter = 1;
                    foreach (Sundry item in allConcessionsItems)
                    {
                        validItemOptions.Add(counter.ToString());
                        counter++;
                    }

                    Console.WriteLine("--- DELETE ITEM ---");
                    Console.WriteLine("Enter the number of the item you want to delete:");
                    newItemNameOrSelection = GetUserInput(validItemOptions);
                    Console.WriteLine();
                    Console.WriteLine(ConvertStatementToConsoleLengthLines("Are you sure you want to delete the " + allConcessionsItems[Convert.ToInt32(newItemNameOrSelection) - 1].Name +
                                      " item? Enter \"Y\" or \"N\":"));
                    confirmChoice = GetUserInput(new List<string>() { "Y", "N" });

                    if (confirmChoice == "Y")
                    {
                        allConcessionsItems.RemoveAt(Convert.ToInt32(newItemNameOrSelection) - 1);
                        Console.WriteLine();
                        Console.WriteLine("Item successfully removed!");
                        Console.WriteLine();
                        Console.WriteLine("Press \"Enter\" to continue...");
                        Console.ReadLine();
                    }
                    else if (confirmChoice == "N")
                    {
                        Console.WriteLine();
                        Console.WriteLine("That was close! You almost deleted that item!");
                        Console.WriteLine(ConvertStatementToConsoleLengthLines("It's good you kept it, because the zoo patrons " +
                                          "should start buying " + allConcessionsItems[Convert.ToInt32(newItemNameOrSelection) - 1].Name +
                                          "s any day now!"));
                        Console.WriteLine();
                        Console.WriteLine("Press \"Enter\" to continue...");
                        Console.ReadLine();
                    }

                }
                else if (userInput == "3")
                {
                    List<string> validItemOptions = new List<string>();
                    int counter = 1;
                    foreach (Sundry item in allConcessionsItems)
                    {
                        validItemOptions.Add(counter.ToString());
                        counter++;
                    }

                    Console.WriteLine("--- CHANGE ITEM PRICE ---");
                    Console.WriteLine("Enter the number of the item whose price you want to change:");
                    newItemNameOrSelection = GetUserInput(validItemOptions);
                    Console.WriteLine();
                    Console.WriteLine("Enter the new price you would like to charge for " +
                                      allConcessionsItems[Convert.ToInt32(newItemNameOrSelection) - 1].Name + ":");
                    newItemPriceString = Console.ReadLine();

                    while (!Decimal.TryParse(newItemPriceString, out newItemPrice) || newItemPrice <= 0 || newItemPrice > 100 ||
                                               ((newItemPrice * 100) - Math.Floor(newItemPrice * 100)) > 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Invalid price. Please enter a positive dollar amount that is $100.00 or less (ex: 3.00):");
                        newItemPriceString = Console.ReadLine();
                    }

                    allConcessionsItems[Convert.ToInt32(newItemNameOrSelection) - 1].Price = newItemPrice;
                    Console.WriteLine();
                    Console.WriteLine("Item price successfully changed!");
                    Console.WriteLine();
                    Console.WriteLine("Press \"Enter\" to continue...");
                    Console.ReadLine();
                }
            }
            while (!exit);
        }

        static void EditGiftShopItems(List<Sundry> allGiftShopItems, int[] dayNumber, decimal[] cashOnHand, int[] totalAttractionScore)
        {
            List<string> validOptions = new List<string>() { "1", "2", "3", "E" };
            List<string> optionsText = new List<string>()
            {
                "Add New Item",
                "Delete Item",
                "Change Price",
                "Exit"
            };
            string userInput;
            bool exit = false;
            string newItemNameOrSelection;
            decimal newItemPrice;
            string newItemPriceString;
            string confirmChoice;

            do
            {
                newItemNameOrSelection = "";
                newItemPrice = 0;
                newItemPriceString = "";
                Console.Clear();
                DisplayDashboard(dayNumber, cashOnHand, totalAttractionScore);
                Console.WriteLine("--------------- CHANGE GIFT SHOP ITEMS ---------------");
                DisplayCurrentSundries(allGiftShopItems);
                DisplaySundriesOptionsMenu(validOptions, optionsText);
                Console.WriteLine("Enter an option from the menu, or \"E\" to exit:");
                userInput = GetUserInput(validOptions);
                Console.WriteLine();

                if (userInput == "E")
                {
                    exit = true;
                }
                else if ((userInput == "2" || userInput == "3") && allGiftShopItems.Count == 0)
                {
                    Console.WriteLine("Oops! You don't have any items in the Gift Shop yet.");
                    Console.WriteLine("Add an item in order to edit it.");
                    Console.WriteLine();
                    Console.WriteLine("Press \"Enter\" to continue...");
                    Console.ReadLine();
                }
                else if (userInput == "1" && allGiftShopItems.Count < 20)
                {
                    Console.WriteLine("--- ADD A NEW ITEM ---");
                    Console.WriteLine("Enter the name of the new item:");
                    newItemNameOrSelection = Console.ReadLine();

                    while (newItemNameOrSelection.Length > 30)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Invalid entry. Please enter an item name that is 30 characters or less.");
                        newItemNameOrSelection = Console.ReadLine();
                    }

                    Console.WriteLine();
                    Console.WriteLine("Enter the price of the new item:");
                    newItemPriceString = Console.ReadLine();

                    while (!Decimal.TryParse(newItemPriceString, out newItemPrice) || newItemPrice <= 0 || newItemPrice > 100 ||
                           ((newItemPrice * 100) - Math.Floor(newItemPrice * 100)) > 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Invalid price. Please enter a positive dollar amount that is $100.00 or less (ex: 3.00):");
                        newItemPriceString = Console.ReadLine();
                    }

                    allGiftShopItems.Add(new Sundry(newItemNameOrSelection, newItemPrice));
                    Console.WriteLine();
                    Console.WriteLine($"Success! The item {newItemNameOrSelection} was added to the gift shop!");
                    Console.WriteLine();
                    Console.WriteLine("Press \"Enter\" to continue...");
                    Console.ReadLine();
                }
                else if (userInput == "1" && allGiftShopItems.Count == 20)
                {
                    Console.WriteLine("Unfortuantely, your gift shop can only stock a maximum of 20 items.");
                    Console.WriteLine();
                    Console.WriteLine("If you'd like to add a new item, delete an existing item first.");
                    Console.WriteLine();
                    Console.WriteLine("Press \"Enter\" to continue...");
                    Console.ReadLine();

                }
                else if (userInput == "2")
                {
                    List<string> validItemOptions = new List<string>();
                    int counter = 1;
                    foreach(Sundry item in allGiftShopItems)
                    {
                        validItemOptions.Add(counter.ToString());
                        counter++;
                    }
                    
                    Console.WriteLine("--- DELETE ITEM ---");
                    Console.WriteLine("Enter the number of the item you want to delete:");
                    newItemNameOrSelection = GetUserInput(validItemOptions);
                    Console.WriteLine();
                    Console.WriteLine(ConvertStatementToConsoleLengthLines("Are you sure you want to delete the " + allGiftShopItems[Convert.ToInt32(newItemNameOrSelection)-1].Name +
                                      " item? Enter \"Y\" or \"N\":"));
                    confirmChoice = GetUserInput(new List<string>() { "Y", "N" });

                    if (confirmChoice == "Y")
                    {
                        allGiftShopItems.RemoveAt(Convert.ToInt32(newItemNameOrSelection) - 1);
                        Console.WriteLine();
                        Console.WriteLine("Item successfully removed!");
                        Console.WriteLine();
                        Console.WriteLine("Press \"Enter\" to continue...");
                        Console.ReadLine();
                    }
                    else if (confirmChoice == "N")
                    {
                        Console.WriteLine();
                        Console.WriteLine("Whew! The item was not deleted.");
                        Console.WriteLine(ConvertStatementToConsoleLengthLines("Who knows? Maybe the " + allGiftShopItems[Convert.ToInt32(newItemNameOrSelection)-1].Name +
                                          "s will start flying off the shelves!"));
                        Console.WriteLine();
                        Console.WriteLine("Press \"Enter\" to continue...");
                        Console.ReadLine();
                    }

                }
                else if (userInput == "3")
                {
                    List<string> validItemOptions = new List<string>();
                    int counter = 1;
                    foreach (Sundry item in allGiftShopItems)
                    {
                        validItemOptions.Add(counter.ToString());
                        counter++;
                    }

                    Console.WriteLine("--- CHANGE ITEM PRICE ---");
                    Console.WriteLine("Enter the number of the item whose price you want to change:");
                    newItemNameOrSelection = GetUserInput(validItemOptions);
                    Console.WriteLine();
                    Console.WriteLine("Enter the new price you would like to charge for a " +
                                      allGiftShopItems[Convert.ToInt32(newItemNameOrSelection) - 1].Name + ":");
                    newItemPriceString = Console.ReadLine();

                    while (!Decimal.TryParse(newItemPriceString, out newItemPrice) || newItemPrice <= 0 || newItemPrice > 100 ||
                           ((newItemPrice * 100) - Math.Floor(newItemPrice * 100)) > 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Invalid price. Please enter a positive dollar amount that is $100.00 or less (ex: 3.00):");
                        newItemPriceString = Console.ReadLine();
                    }

                    allGiftShopItems[Convert.ToInt32(newItemNameOrSelection) - 1].Price = newItemPrice;
                    Console.WriteLine();
                    Console.WriteLine("Item price successfully changed!");
                    Console.WriteLine();
                    Console.WriteLine("Press \"Enter\" to continue...");
                    Console.ReadLine();

                }
            }
            while (!exit);
        }

        static void DisplaySundriesOptionsMenu(List<string> validOptions, List<string> optionsText)
        {
            int index = 0;
            Console.WriteLine();
            foreach (string validOption in validOptions)
            {
                Console.WriteLine(validOption + " - " + optionsText[index]);
                index++;
            }
            Console.WriteLine();
        }

        static void DisplayCurrentSundries(List<Sundry> allItems)
        {
            int lineNumber = 1;
            int standardNameLength = 30;
            int lineSpacerLength;
            string lineSpacer;

            if (allItems.Count > 0)
            {
                Console.WriteLine("CURRENT ITEMS FOR SALE:");
                foreach (Sundry item in allItems)
                {
                    lineSpacer = "";
                    lineSpacerLength = standardNameLength - item.Name.Length;
                    
                    for (int i = 0; i < lineSpacerLength; i++)
                    {
                        lineSpacer += " ";
                    }
                    
                    if (lineNumber < 10)
                    {
                        Console.WriteLine($"{lineNumber}.  {item.Name}{lineSpacer}\tPrice: {item.Price:C2}");
                    }
                    else
                    {
                        Console.WriteLine($"{lineNumber}. {item.Name}{lineSpacer}\tPrice: {item.Price:C2}");
                    }
                    lineNumber++;
                }
                Console.WriteLine();
            }
        }

        static void SellZooAnimals(PhysicalSpace[] allPhysicalSpaces, List<Animal> allAnimals, int[] dayNumber, decimal[] cashOnHand, int[] totalAttractionScore)
        {
            int lineCounter;
            string optionNumberString;
            int optionNumberInt;
            string sellQuantityString;
            int sellQuantityInt;
            int totalSale;
            bool exit = false;
            string confirmChoice;

            do
            {
                Console.Clear();
                lineCounter = 1;
                totalSale = 0;

                DisplayDashboard(dayNumber, cashOnHand, totalAttractionScore);
                Console.WriteLine("-------------------------- SELL ANIMALS --------------------------");
                Console.WriteLine();

                int totalAnimals = 0;
                foreach (Animal animal in allAnimals)
                {
                    totalAnimals += animal.Quantity;
                }

                if (totalAnimals == 0)
                {
                    Console.WriteLine("It looks like you don't have any animals right now.\r\nCome back after you've purchased some!");
                    Console.WriteLine();
                    Console.WriteLine("Press \"Enter\" to continue...");
                    Console.ReadLine();
                    return;
                }

                List<string> validAnimalOptions = new List<string>();
                List<Animal> sellableAnimals = new List<Animal>();

                Console.WriteLine("                         SELL     ATTRACTION    DAILY    CURRENTLY");
                Console.WriteLine("#   NAME                PRICE       SCORE        COST       OWN");
                Console.WriteLine();

                foreach (Animal animal in allAnimals)
                {
                    if (animal.Quantity != 0)
                    {
                        validAnimalOptions.Add(lineCounter.ToString());
                        sellableAnimals.Add(animal);
                        decimal animalSellPrice = Math.Floor(animal.Price * .6m);

                        //Create the dollarAmtSpacer
                        decimal price = animal.Price;
                        string dollarAmtSpacer = (animalSellPrice >= 1000 && animalSellPrice < 10000) ? " " :
                                                    (animalSellPrice >= 100 && animalSellPrice < 1000) ? "   " :
                                                        (animalSellPrice >= 10 && animalSellPrice < 100) ? "    " :
                                                            (animalSellPrice >= 0 && animalSellPrice < 10) ? "     " : "";

                        //Create the nameSpacer
                        string nameSpacer = "";
                        int standardNameLength = 18;
                        int nameSpacerLength = standardNameLength - animal.Name.Length;

                        for (int i = 0; i < nameSpacerLength; i++)
                        {
                            nameSpacer += " ";
                        }

                        string animalDailyCostSpacer = animal.DailyCost < 10 ? "  " : animal.DailyCost < 100 ? " " : "";
                        string currentlyOwnSpacer = animal.Quantity < 10 ? "  " : animal.Quantity < 100 ? " " : "";
                        string lineCounterSpacer = lineCounter < 10 ? "  " : " ";
                        string attractionValueSpacer = animal.AttractionValue < 10 ? "       " : animal.AttractionValue < 100 ? "      " : animal.AttractionValue < 1000 ? "     " : "   ";

                        Console.WriteLine($"{lineCounter}.{lineCounterSpacer}{animal.Name}{nameSpacer}{dollarAmtSpacer}{animalSellPrice:C0}" +
                                          $"    {attractionValueSpacer}{animal.AttractionValue:N0}{animalDailyCostSpacer}        " +
                                          $"{animal.DailyCost:C0}{currentlyOwnSpacer}      {animal.Quantity}");

                        lineCounter++;
                    }
                }
                Console.WriteLine();
                Console.WriteLine("Enter the number of the animal you wish to sell, or \"E\" to exit:");

                validAnimalOptions.Add("E");
                optionNumberString = GetUserInput(validAnimalOptions);

                if (optionNumberString == "E")
                {
                    break;
                }

                Console.WriteLine();
                optionNumberInt = Convert.ToInt32(optionNumberString);
                Console.WriteLine($"Enter the quantity of " + sellableAnimals[optionNumberInt - 1].Name.Pluralize() + " you wish to sell, or \"C\" to cancel sale:");

                List<string> validSellQuantities = new List<string>() { "C" };
                for (int i = 1; i <= sellableAnimals[optionNumberInt - 1].Quantity; i++)
                {
                    validSellQuantities.Add(i.ToString());
                }

                sellQuantityString = GetUserInput(validSellQuantities);

                if (sellQuantityString == "C")
                {
                    continue;
                }
                else
                {
                    sellQuantityInt = Convert.ToInt32(sellQuantityString);
                }

                totalSale = Convert.ToInt32(Math.Floor(sellableAnimals[optionNumberInt - 1].Price * .6m)) * sellQuantityInt;

                string sellAnimalName = sellQuantityInt != 1 ? sellableAnimals[optionNumberInt - 1].Name.Pluralize() : sellableAnimals[optionNumberInt - 1].Name;


                Console.WriteLine();
                Console.WriteLine($"Are you sure you want to sell {sellQuantityInt} {sellAnimalName} for a total of {totalSale:C0}?\r\nEnter \"Y\" or \"N\":");
       
                confirmChoice = GetUserInput(new List<string>() { "Y", "N" });

                if (confirmChoice == "Y")
                {
                    cashOnHand[0] += totalSale;
                    sellableAnimals[optionNumberInt - 1].Sell(sellQuantityInt);
                    totalAttractionScore[0] -= sellableAnimals[optionNumberInt - 1].AttractionValue * sellQuantityInt;

                    List<string> spaceOptions = new List<string>() { "A", "B", "C", "D", "", "E", "F", "G", "H", "", "I", "J", "K",
                                                                     "L", "M", "N", "O", "P", "", "" };

                    if (sellableAnimals[optionNumberInt - 1].Quantity == 0)
                    {
                        allPhysicalSpaces[spaceOptions.IndexOf(sellableAnimals[optionNumberInt - 1].Location)].AnimalQuantity = 0;
                        allPhysicalSpaces[spaceOptions.IndexOf(sellableAnimals[optionNumberInt - 1].Location)].OccupyingAnimals = "";
                        sellableAnimals[optionNumberInt - 1].Location = "";
                    }
                    else
                    {
                        allPhysicalSpaces[spaceOptions.IndexOf(sellableAnimals[optionNumberInt - 1].Location)].AnimalQuantity =
                            sellableAnimals[optionNumberInt - 1].Quantity;
                    }

                    Console.WriteLine();
                    Console.WriteLine($"Success! You sold {sellQuantityInt} {sellAnimalName} for a total of {totalSale:C0}.");
                    Console.WriteLine();
                    Console.WriteLine("Press \"Enter\" to continue...");
                    Console.ReadLine();
                }
                else if (confirmChoice == "N")
                {
                    Console.WriteLine();
                    string animalMessage = sellQuantityInt != 1 ? "are relieved they weren't sold" : "is relieved to not be sold";

                    Console.WriteLine($"That was a close call!\r\nThe {sellAnimalName} {animalMessage}, but will be sleeping with one eye open!");                  
                    Console.WriteLine();
                    Console.WriteLine("Press \"Enter\" to continue...");
                    Console.ReadLine();
                }
            }
            while (!exit);
        }

        static void AdvanceDay(PhysicalSpace[] allPhysicalSpaces, List<Animal> allAnimals, List<Sundry> allConcessionsItems,
                               List<Sundry> allGiftShopItems, int[] dayNumber, decimal[] cashOnHand, int[] totalAttractionScore,
                               decimal[] ticketPrice, List<int> attendanceHistory)
        {
            Random random = new Random();
            double totalAttScore = totalAttractionScore[0];
            bool exit = false;

            //Calculate Weather
            int weatherHeatScore = random.Next(40, 106);
            int weatherHumidityScore = random.Next(20, 91);
            double weatherCompositePercentage = 0;
            string todaysWeather;

            if (weatherHeatScore < 50 || weatherHeatScore > 92)
            {
                weatherCompositePercentage += -1 * (random.Next(5, 11) / 100d);
            }
            
            if (weatherHeatScore > 75 && weatherHumidityScore > 75)
            {
                weatherCompositePercentage += -1 * (random.Next(0, 5) / 100d);
            }
            
            if (weatherHeatScore > 70 && weatherHeatScore < 92 && weatherHumidityScore < 55)
            {
                weatherCompositePercentage += (random.Next(0, 11) / 100d);
            }

            todaysWeather = weatherCompositePercentage >= .02 ? "Very Good" :
                                weatherCompositePercentage >= .00 ? "Good" :
                                    weatherCompositePercentage >= -.02 ? "Fair" :
                                        weatherCompositePercentage >= -.05 ? "Poor" : "Terrible";

            //Calculate Attendance
            int attendance;
            int currentNumberOfAnimalExhibits = 0;
            int totalNumberOfAnimalExhibits = 16;
            decimal animalExhibitsMultiplier = 0;

            foreach (Animal animal in allAnimals)
            {
                if (animal.Quantity > 0)
                {
                    currentNumberOfAnimalExhibits++;
                }
            }

            animalExhibitsMultiplier = 1 + (((decimal)currentNumberOfAnimalExhibits / totalNumberOfAnimalExhibits) * .1m);

            decimal ticketPriceDec = ticketPrice[0];
            decimal idealTicketPrice = (decimal)Math.Round(7505493d + ((1.718528d - 7505493d) / (1 + Math.Pow((totalAttScore / 542546100000d), .7081755d))), 2) + 2;
            decimal actualVsIdealTktPriceDifference = Math.Round(ticketPriceDec - idealTicketPrice, 0);
            decimal ticketPriceTooHighPenaltyMulitplier = 1m;

            for (int i = 0; i < actualVsIdealTktPriceDifference; i++)
            {
                if (ticketPriceTooHighPenaltyMulitplier >= .025m)
                {
                    ticketPriceTooHighPenaltyMulitplier -= .025m;
                }
            }

            if (totalAttScore == 0)
            {
                attendance = 0;
            }
            else
            {
                attendance = (int)((decimal)(Math.Round(5716.327d + ((120.4226d - 5716.327d) / (1d + Math.Pow((totalAttScore / 17999.61d), 1.879935d))), 2) + 5) * (decimal)(1 + weatherCompositePercentage) * ticketPriceTooHighPenaltyMulitplier * animalExhibitsMultiplier);
            }

            if (totalAttScore >= 50000 && attendance > 5500)
            {
                attendance = 5500;
            }

            attendanceHistory.Add(attendance);

            //Calculate Ticket Income
            decimal ticketIncome;

            ticketIncome = ticketPrice[0] * attendance;

            //Calculate Animal Costs
            decimal totalAnimalCost = 0;
            decimal vetBill = 0;

            foreach (Animal animal in allAnimals)
            {
                totalAnimalCost += animal.DailyCost * animal.Quantity;
            }

            vetBill = Math.Round(totalAnimalCost * (random.Next(0, 16) / 100m), 2);

            //Calculate Concessions Income/Loss
            decimal concessionsIncome = 0;
            decimal concessionsCostToRun = 0;

            foreach (Sundry item in allConcessionsItems)
            {
                decimal price = item.Price;
                decimal percentOfAttendessWhoWillBuy = 0;

                percentOfAttendessWhoWillBuy = price <= 2 ? (random.Next(25, 86) / 100m) :
                                                    price <= 5 ? (random.Next(20, 76) / 100m) :
                                                        price <= 10 ? (random.Next(15, 66) / 100m) :
                                                            price <= 20 ? (random.Next(10, 51) / 100m) :
                                                                price <= 50 ? (random.Next(0, 21) / 100m) :
                                                                    price <= 75 ? (random.Next(0, 11) / 100m) : (random.Next(0, 6) / 100m);

                concessionsIncome += percentOfAttendessWhoWillBuy * attendance;
            }

            concessionsCostToRun = Math.Round((((decimal)allConcessionsItems.Count / 3) * 25) + 400, 2);

            //Calculate Gift Shop Income/Loss
            decimal giftShopIncome = 0;
            decimal giftShopCostToRun = 0;

            foreach (Sundry item in allGiftShopItems)
            {
                decimal price = item.Price;
                decimal percentOfAttendessWhoWillBuy = 0;

                percentOfAttendessWhoWillBuy = price <= 2 ? (random.Next(25, 76) / 100m) :
                                                    price <= 5 ? (random.Next(20, 66) / 100m) :
                                                        price <= 10 ? (random.Next(15, 56) / 100m) :
                                                            price <= 20 ? (random.Next(10, 41) / 100m) :
                                                                price <= 50 ? (random.Next(0, 16) / 100m) :
                                                                    price <= 75 ? (random.Next(0, 8) / 100m) : (random.Next(0, 6) / 100m);

                giftShopIncome += percentOfAttendessWhoWillBuy * attendance;
            }

            giftShopIncome = Math.Round(giftShopIncome, 2);
            
            giftShopCostToRun = Math.Round((((decimal)allGiftShopItems.Count / 2) * 10) + 400, 2);

            decimal todaysIncome = ticketIncome + concessionsIncome + giftShopIncome;
            decimal todaysExpenses = totalAnimalCost + vetBill + concessionsCostToRun + giftShopCostToRun;
            decimal todaysGainOrLoss = todaysIncome - todaysExpenses;

            do
            {
                Console.Clear();
                DisplayDashboard(dayNumber, cashOnHand, totalAttractionScore);
                Console.WriteLine("---------------------- DAILY ZOO REPORT ----------------------");
                Console.WriteLine();
                Console.WriteLine($"Today's Attendance: {attendance:N0}");
                Console.WriteLine($"Weather: {todaysWeather}");
                Console.WriteLine();
                Console.WriteLine("----------------- INCOME -----------------");
                Console.WriteLine($"Income from Ticket Sales: {ticketIncome:C2}");
                Console.WriteLine($"Income from Concessions Sales: {concessionsIncome:C2}");
                Console.WriteLine($"Income from Gift Shop Sales: {giftShopIncome:C2}");
                Console.WriteLine();
                Console.WriteLine($"TOTAL INCOME: {todaysIncome:C2}");
                Console.WriteLine();
                Console.WriteLine("---------------- EXPENSES ----------------");
                Console.WriteLine($"Cost of Daily Animal Care: {totalAnimalCost:C2}");
                Console.WriteLine($"Cost of Veterinary Care: {vetBill:C2}");
                Console.WriteLine($"Cost to Run Concessions Stands: {concessionsCostToRun:C2}");
                Console.WriteLine($"Cost of Run Gift Shop: {giftShopCostToRun:C2}");
                Console.WriteLine();
                Console.WriteLine($"TOTAL EXPENSES: {todaysExpenses:C2}");
                Console.WriteLine();
                Console.WriteLine("-------------- NET GAIN/LOSS --------------");
                Console.WriteLine();
                if (todaysGainOrLoss < 0)
                {
                    Console.WriteLine($"NET TOTAL: -${Math.Abs(todaysGainOrLoss):N2}");
                }
                else
                {
                    Console.WriteLine($"TOTAL GAIN/LOSS: +${todaysGainOrLoss:N2}");
                }
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Enter \"H\" to see daily attendance history, or \"Enter\" to continue...");

                List<string> menuOptions = new List<string>() { "H", "" };
                string userOption = GetUserInput(menuOptions);

                if (userOption == "")
                {
                    exit = true;
                    break;
                }

                DisplayDailyAttendanceHistory(dayNumber, cashOnHand, totalAttractionScore, attendanceHistory);
            }
            while (!exit);

            dayNumber[0] += 1;
            cashOnHand[0] += todaysGainOrLoss;
        }

        static void DisplayDailyAttendanceHistory(int[] dayNumber, decimal[] cashOnHand, int[] totalAttractionScore, List<int> attendanceHistory)
        {
            string numberSpacer = "";
            int lineCounter = 1;
            
            Console.Clear();
            DisplayDashboard(dayNumber, cashOnHand, totalAttractionScore);
            Console.WriteLine();
            Console.WriteLine("------------------- ATTENDANCE HISTORY -------------------");
            Console.WriteLine();

            for (int i = 0; i < attendanceHistory.Count; i++)
            {
                numberSpacer = i < 9 ? "  " : " ";
                Console.WriteLine($"Day {lineCounter}:{numberSpacer}{attendanceHistory[i]:N0} guests");
                lineCounter++;
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Press \"Enter\" to return to the Daily Zoo Report...");
            Console.ReadLine();
        }
        
        static void EndOfGame(int[] dayNumber, decimal[] cashOnHand, int[] totalAttractionScore, List<int> attendanceHistory,
                              List<Animal> allAnimals, out bool continueProgram, out bool playAgain)
        {
            continueProgram = false;

            Console.Clear();
            Console.WriteLine("DDDDD        AAAA    YY      YY       222222      555555");
            Console.WriteLine("DD  DD      AA  AA    YY    YY       22    22     55");
            Console.WriteLine("DD   DD    AA    AA    YY  YY            22       555555");
            Console.WriteLine("DD  DD    AA AAAA AA     YY             22            55");
            Console.WriteLine("DDDDD    AA        AA    YY           22222222    555555");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Press \"Enter\" to continue...");
            Console.ReadLine();

            Console.Clear();
            Console.WriteLine(ConvertStatementToConsoleLengthLines("Today is the day - it's the morning if Day 25!"));
            Console.WriteLine();
            Console.WriteLine(ConvertStatementToConsoleLengthLines("The collection agents from the bank will be here any moment, and " +
                             "they won't leave without either a payment of $100,000 or the keys to the zoo."));
            Console.WriteLine();
            Console.WriteLine("Press \"Enter\" to continue...");
            Console.ReadLine();

            Console.Clear();
            Console.WriteLine(ConvertStatementToConsoleLengthLines("You take one last walk around the zoo to visit all of your favorite " +
                              "animals."));
            Console.WriteLine();
            Console.WriteLine(ConvertStatementToConsoleLengthLines("You thought Great Aunt Gertrude was crazy for building this " +
                              "zoo way up here in the Andes. But now, after 25 long days, the place feels like home."));
            Console.WriteLine();
            Console.WriteLine("Press \"Enter\" to continue...");
            Console.ReadLine();

            Console.Clear();
            Console.WriteLine(ConvertStatementToConsoleLengthLines("You see a black SUV pull up to the zoo entrance. " +
                              "Two people, one short and one tall, step out in dark suits, heavy overcoats, and sunglasses."));
            Console.WriteLine();
            Console.WriteLine(ConvertStatementToConsoleLengthLines("\"We trust you have our $100,000,\" says the short bank agent " +
                              "with a wry grin. \"If not, we will gladly take this...establishment off of your hands.\""));
            Console.WriteLine();
            Console.WriteLine("Press \"Enter\" to continue...");
            Console.ReadLine();

            if (cashOnHand[0] >= 100000)
            {
                Console.Clear();
                Console.WriteLine(ConvertStatementToConsoleLengthLines("You tremble slightly as you open the zoo's small cash vault to " +
                                  "double-count the money inside. The bank agents won't leave until they have every penny."));
                Console.WriteLine();
                Console.WriteLine(ConvertStatementToConsoleLengthLines("You start to count the bills: \"One...Two...Three...\""));
                Console.WriteLine();
                Console.WriteLine("Press \"Enter\" to continue...");
                Console.ReadLine();

                Console.Clear();
                Console.WriteLine(ConvertStatementToConsoleLengthLines("\"...Ninety Nine Thousand Nine Hundred Ninety Eight...\""));
                Console.WriteLine();
                Console.WriteLine(ConvertStatementToConsoleLengthLines("\"...Ninety Nine Thousand Nine Hundred Ninety Nine...\""));
                Console.WriteLine();
                Console.WriteLine("Press \"Enter\" to continue...");
                Console.ReadLine();

                Console.Clear();
                Console.WriteLine(ConvertStatementToConsoleLengthLines("$100,000! YOU DID IT! YOU SAVED THE ZOO!!!"));
                Console.WriteLine();
                Console.WriteLine(ConvertStatementToConsoleLengthLines("Because of your hard work, Great Aunt Gertrude's " +
                                  "legacy will live on for years to come!"));
                Console.WriteLine();
                Console.WriteLine("Press \"Enter\" to continue...");
                Console.ReadLine();

                Console.Clear();
                Console.WriteLine(ConvertStatementToConsoleLengthLines("As the bank agents climb back into their SUV and " +
                                  "drive away in a cloud of dust, you feel a sense of relief and joy."));
                Console.WriteLine();
                Console.WriteLine(ConvertStatementToConsoleLengthLines("Watching the dust settle, you hear the skwaks, " +
                                  "growls, and kaws of the zoo animals greeting a new day."));
                Console.WriteLine();
                Console.WriteLine(ConvertStatementToConsoleLengthLines("Well, what are you waiting for? You have a zoo to run!"));
                Console.WriteLine();
                Console.WriteLine("Press \"Enter\" to continue...");
                Console.ReadLine();
            }
            else
            {
                Console.Clear();
                Console.WriteLine(ConvertStatementToConsoleLengthLines("Sadly, with tears in your eyes, you explain that despite " +
                                  "your best efforts you weren't able to come up with the money to save the zoo that your " +
                                  "Great Aunt Gertrude worked so hard to build."));
                Console.WriteLine();
                Console.WriteLine(ConvertStatementToConsoleLengthLines("The bank agents, silent for a moment, glance at each other " +
                                  "and walk a short distance away to talk in private."));
                Console.WriteLine();
                Console.WriteLine("Press \"Enter\" to continue...");
                Console.ReadLine();

                Console.Clear();
                Console.WriteLine(ConvertStatementToConsoleLengthLines("When they return, the tall agent removes her sunglasses."));
                Console.WriteLine();
                Console.WriteLine(ConvertStatementToConsoleLengthLines("\"We couldn't help but notice the good condition that the " +
                                  "zoo is in,\" she says. \"The bank does need to take ownership of this land, but it has no " +
                                  "interest in operating a zoo.\""));
                Console.WriteLine();
                Console.WriteLine("Press \"Enter\" to continue...");
                Console.ReadLine();

                Console.Clear();
                Console.WriteLine(ConvertStatementToConsoleLengthLines("\"We would like you to continue on as the " +
                                  "director. As long as you keep it operating well, you can stay here and run it for " +
                                  "as long as you would like."));
                Console.WriteLine();
                Console.WriteLine("Press \"Enter\" to continue...");
                Console.ReadLine();

                Console.Clear();
                Console.WriteLine(ConvertStatementToConsoleLengthLines("Tears well up in your eyes as you are flooded with " +
                                  "gratitude and relief. \"Thank you!\" you manage to say. \"I'll do my very best!\""));
                Console.WriteLine();
                Console.WriteLine(ConvertStatementToConsoleLengthLines("As the bank agents climb back into their SUV and drive away, " +
                                  "you hear the skwaks, growls, and kaws of the zoo animals greeting a new day."));
                Console.WriteLine();
                Console.WriteLine("Press \"Enter\" to continue...");
                Console.ReadLine();

                Console.Clear();
                Console.WriteLine(ConvertStatementToConsoleLengthLines("As you watch the dust kicked up by the SUV settle on the dirt road, " +
                                  "you smile to yourself."));
                Console.WriteLine();
                Console.WriteLine(ConvertStatementToConsoleLengthLines("Great Aunt Gertrude built this zoo to be a home for animals " +
                                  "who need one, and she bequeathed it to you hoping you could make her dream a reality."));
                Console.WriteLine();
                Console.WriteLine("Press \"Enter\" to continue...");
                Console.ReadLine();

                Console.Clear();
                Console.WriteLine(ConvertStatementToConsoleLengthLines("While you may not own the zoo outright, you've still made it " +
                                  "a refuge for many animals. By doing so, you've fulfilled Great Aunt Gertrude's wishes and her " +
                                  "legacy lives on."));
                Console.WriteLine();
                Console.WriteLine(ConvertStatementToConsoleLengthLines("As you ponder these thoughts, you notice the animal noises behind " +
                                  "getting louder. \"They must be getting hungry for breakfast,\" you say to yourself."));
                Console.WriteLine();
                Console.WriteLine(ConvertStatementToConsoleLengthLines("Well, what are you waiting for? You have a zoo to run!"));
                Console.WriteLine();
                Console.WriteLine("Press \"Enter\" to continue...");
                Console.ReadLine();
            }

            int maxAttendance = 0;
            int totalAttendance = 0;
            foreach (int number in attendanceHistory)
            {
                totalAttendance += number;

                if (number > maxAttendance)
                {
                    maxAttendance = number;
                }
            }

            int totalAnimals = 0;
            foreach (Animal animal in allAnimals)
            {
                totalAnimals += animal.Quantity;
            }

            string cashSign = "";
            if (cashOnHand[0] < 0)
            {
                cashSign = "-";
            }

            Console.Clear();
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("                   GAME RESULTS                   ");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine($"TOTAL CASH: {cashSign}${Math.Abs(cashOnHand[0]):N2}");
            Console.WriteLine($"ZOO ATTRACTION SCORE: {totalAttractionScore[0]:N0}");
            Console.WriteLine($"TOTAL NUMBER OF ANIMALS: {totalAnimals:N0}");
            Console.WriteLine($"TOTAL ATTENDANCE: {totalAttendance:N0}");
            Console.WriteLine($"HIGEST DAILY ATTENDANCE: {maxAttendance:N0}");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Press \"Enter\" to continue...");
            Console.ReadLine();

            playAgain = DoYouWantToPlayAgain();
        }
        
        static bool DoYouWantToPlayAgain()
        {
            bool playAgain = false;
            
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Do you want to play again? Enter \"Y\" for Yes or \"N\" for No:");
            string playAgainUserInput = Console.ReadLine().ToUpper();

            while (playAgainUserInput != "N" && playAgainUserInput != "Y")
            {
                Console.WriteLine();
                Console.WriteLine("Invalid entry. Please enter \"Y\" or \"N\".");
                playAgainUserInput = Console.ReadLine().ToUpper();
            }

            if (playAgainUserInput == "Y")
            {
                playAgain = true;
            }

            return playAgain;
        }

        static void ExitProgram()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Thank you for playing! Goodbye!");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Press \"Enter\" to exit the game...");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("By Aaron Smith, Copyright 2021");
            Console.ReadLine();
        }
    }
}
