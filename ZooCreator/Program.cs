using System;
using System.Collections.Generic;
using Humanizer;

namespace ZooCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exitNow = WelcomeMessage();
            if (exitNow)
            {
                return;
            }
            
            List<Animals> allAnimals = GenerateZooAnimals();
            PhysicalSpaces[] allPhysicalSpaces = GeneratePhysicalSpaces();
            List<Sundry> allConcessionsItems = new List<Sundry>();
            List<Sundry> allGiftShopItems = new List<Sundry>();
            
            bool firstTimeRun = true;
            bool continueProgram = true;
            List<string> usedLocations = new List<string>();
            int dayNumber = 1;
            decimal cashOnHand = 25000.00m;
            List<string> mainMenuOptions = new List<string>() { "1", "2", "3", "4", "5", "6", "A", "E" };

            do
            {
                if (!firstTimeRun)
                {
                    Console.Clear();
                }

                DisplayDashboard(dayNumber, cashOnHand);
                DisplayMainMenu(mainMenuOptions);
                string userOption = GetUserInput(mainMenuOptions);
                firstTimeRun = false;

                if (userOption == "1")
                {
                    DisplayZooMap(allPhysicalSpaces, dayNumber, cashOnHand);
                }
                else if (userOption == "2")
                {
                    BuyNewAnimals(allPhysicalSpaces, allAnimals, cashOnHand, dayNumber, out decimal remainingCashOnHand);
                    cashOnHand = remainingCashOnHand;
                }
                else if (userOption == "3")
                {
                    ArrangeAnimals(allPhysicalSpaces, allAnimals, dayNumber, cashOnHand);
                }
                else if (userOption == "4")
                {
                    EditConcessionsItems(allConcessionsItems, dayNumber, cashOnHand);
                }
                else if (userOption == "5")
                {
                    EditGiftShopItems(allGiftShopItems, dayNumber, cashOnHand);
                }
                else if (userOption == "6")
                {
                    SellZooAnimals(allPhysicalSpaces, allAnimals, cashOnHand, dayNumber, out decimal remainingCashOnHand);
                    cashOnHand = remainingCashOnHand;
                }
                else if (userOption == "A")
                {
                    AdvanceDay(allPhysicalSpaces, allAnimals, allConcessionsItems, allGiftShopItems, cashOnHand, dayNumber, out decimal remainingCashOnHand, out dayNumber);
                    cashOnHand = remainingCashOnHand;
                }
                else if (userOption == "E")
                {
                    continueProgram = false;
                }
            }
            while (continueProgram);

            ExitProgram();
        }

        static bool WelcomeMessage()
        {
            Console.WriteLine("WELCOME TO THE WORLD-FAMOUS");
            Console.WriteLine();
            Console.WriteLine("ZZZZZZ    OOOOOO    OOOOOO");
            Console.WriteLine("   ZZ     OO  OO    OO  OO");
            Console.WriteLine(" ZZ       OO  OO    OO  OO");
            Console.WriteLine("ZZZZZZ    OOOOOO    OOOOOO");
            Console.WriteLine();
            Console.WriteLine("OF MAGIC AND ANIMAL WONDER!\r\n\r\n");
            Console.WriteLine("Press Enter to continue, or \"S\" to skip introduction.");
            string skip = Console.ReadLine().ToUpper();

            if (skip == "S")
            {
                Console.Clear();
                return false;
            }

            Console.Clear();
            Console.WriteLine(CovertStatementToConsoleLengthLines("Long ago, your Great Aunt Gertrude purchased a plot of land in the Andes mountains."));
            Console.WriteLine();
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();

            Console.Clear();
            Console.WriteLine(CovertStatementToConsoleLengthLines("She spent her considerable fortune and many years constructing *something*, but no one was sure what it was."));
            Console.WriteLine();
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();

            Console.Clear();
            Console.WriteLine(CovertStatementToConsoleLengthLines("Recently, you received a telegram explaining that Great Aunt Gertrude died suddenly, and in her last will and testament " +
                "she has bequeathed the land, all that has been constructed, and the remainder of her fortune ($25,000) to you."));
            Console.WriteLine();
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();

            Console.Clear();
            Console.WriteLine(CovertStatementToConsoleLengthLines("However, the will also gave specific instructions that you are only to recieve the land and money if you chose to complete " +
                "the mysterious project that she had been working on."));
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
                Console.WriteLine(CovertStatementToConsoleLengthLines("You don't want to finish the project, and that's ok! Great Aunt Gertrude was a little strange. Who knows what she was " +
                    "working on, anyway?"));
                Console.WriteLine();

                Console.WriteLine(CovertStatementToConsoleLengthLines("Whatever it is, you'll probably never find out. Or, maybe you'll have second thoughts and talk to the lawyer again..."));
                Console.WriteLine();
                Console.WriteLine("Goodbye!");

                return true;
            }

            Console.Clear();
            return false;
        }

        static string CovertStatementToConsoleLengthLines(string originalStatement)
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
              "Create/Edit Concessions",
              "Create/Edit Gift Shop Items",
              "Sell Zoo Animals",
              "Advance Day",
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

        static List<Animals> GenerateZooAnimals()
        {
            Animals aardvarks = new Animals("Aardvark", 500.00m, 0, 45.00m, 12);
            Animals antelope = new Animals("Antelope", 600.00m, 0, 40.00m, 38);
            Animals cheetahs = new Animals("Cheetah", 3000.00m, 0, 100.00m, 90);
            Animals chinchillas = new Animals("Chinchilla", 250.00m, 0, 7.00m, 14);
            Animals dolphins = new Animals("Dolphin", 6000.00m, 0, 400.00m, 95);
            Animals giantTortoises = new Animals("Giant Tortoise", 400.00m, 0, 7.00m, 37);
            Animals giraffes = new Animals("Giraffe", 4500.00m, 0, 375.00m, 82);
            Animals hippos = new Animals("Hippopotamus", 12000.00m, 0, 350.00m, 75);
            Animals iguanas = new Animals("Iguana", 150.00m, 0, 3.00m, 8);
            Animals kiaBirds = new Animals("Kia Bird", 200.00m, 0, 14.00m, 13);
            Animals lemurs = new Animals("Lemur", 1000.00m, 0, 45.00m, 40);
            Animals lions = new Animals("Lion", 9500.00m, 0, 250.00m, 87);
            Animals macaws = new Animals("Macaw", 275.00m, 0, 8.00m, 13);
            Animals penguins = new Animals("Penguin", 500.00m, 0, 25.00m, 49);
            Animals polarBears = new Animals("Polar Bear", 10000.00m, 0, 300.00m, 85);
            Animals rattlesnakes = new Animals("Rattle Snake", 400.00m, 0, 10.00m, 11);
            Animals rhinos = new Animals("Rhinoceros", 12500.00m, 0, 200.00m, 80);
            Animals seaLions = new Animals("Sea Lion", 2000.00m, 0, 350.00m, 70);
            Animals sugarGliders = new Animals("Sugar Glider", 500.00m, 0, 10.00m, 22);
            Animals wolves = new Animals("Wolf", 3500.00m, 0, 240.00m, 72);

            List<Animals> allAnimals = new List<Animals>() { aardvarks, antelope, cheetahs, chinchillas, dolphins, giantTortoises,
                                                             giraffes, hippos, iguanas, kiaBirds, lemurs, lions, macaws, penguins,
                                                             polarBears, rattlesnakes, rhinos, seaLions, sugarGliders, wolves};

            return allAnimals;
        }
        
        static PhysicalSpaces[] GeneratePhysicalSpaces()
        {
            PhysicalSpaces A = new PhysicalSpaces("A", 1, 5);
            PhysicalSpaces B = new PhysicalSpaces("B", 1, 4);
            PhysicalSpaces C = new PhysicalSpaces("C", 1, 3);
            PhysicalSpaces D = new PhysicalSpaces("D", 1, 2);
            PhysicalSpaces FoodCourt1 = new PhysicalSpaces("Food Court 1", 1, 1, "Food1");
            PhysicalSpaces E = new PhysicalSpaces("E", 2, 1);
            PhysicalSpaces F = new PhysicalSpaces("F", 3, 1);
            PhysicalSpaces G = new PhysicalSpaces("G", 4, 1);
            PhysicalSpaces H = new PhysicalSpaces("H", 5, 1);
            PhysicalSpaces FoodCourt2 = new PhysicalSpaces("Food Court 2", 1, 6, "Food2");
            PhysicalSpaces I = new PhysicalSpaces("I", 6, 2);
            PhysicalSpaces J = new PhysicalSpaces("J", 6, 3);
            PhysicalSpaces K = new PhysicalSpaces("K", 6, 4);
            PhysicalSpaces L = new PhysicalSpaces("L", 3, 5);
            PhysicalSpaces M = new PhysicalSpaces("M", 3, 4);
            PhysicalSpaces N = new PhysicalSpaces("N", 3, 3);
            PhysicalSpaces O = new PhysicalSpaces("O", 4, 3);
            PhysicalSpaces P = new PhysicalSpaces("P", 4, 4);
            PhysicalSpaces GiftShop = new PhysicalSpaces("GiftShop", 10, 10, "Gifts");
            PhysicalSpaces Blank = new PhysicalSpaces("Blank", 10, 10);

            PhysicalSpaces[] allPhysicalSpaces = new PhysicalSpaces[] { A, B, C, D, FoodCourt1, E, F, G, H, FoodCourt2,
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

        static void DisplayDashboard(int dayNumber, decimal cashOnHand)
        {
            Console.WriteLine("-------------------------");
            Console.WriteLine($"Day Number: {dayNumber}");
            Console.WriteLine($"Cash: {cashOnHand:C2}");
            Console.WriteLine("-------------------------");
            Console.WriteLine();
        }

        static void DisplayZooMap(PhysicalSpaces[] allPhysicalSpaces, int dayNumber, decimal cashOnHand)
        {
            Console.Clear();
            DisplayDashboard(dayNumber, cashOnHand);
            Console.WriteLine("------------------- CURRENT ZOO MAP -------------------\r\n");
            Console.WriteLine(ZooMapIllustrator(allPhysicalSpaces));
            Console.WriteLine();
            Console.WriteLine("Press \"Enter\" to return to the main menu.");
            Console.ReadLine();
        }

        static void DisplayZooMapLite(PhysicalSpaces[] allPhysicalSpaces)
        {
            Console.WriteLine("------------------- CURRENT ZOO MAP -------------------\r\n");
            Console.WriteLine(ZooMapIllustrator(allPhysicalSpaces));
            Console.WriteLine();
        }

        static string ZooMapIllustrator(PhysicalSpaces[] allPhysicalSpaces)
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

        static string[] SquareSpaceIllustrator(PhysicalSpaces physicalSpace, bool top = false, bool right = false, bool bottom = false, bool left = false)
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

        static void BuyNewAnimals(PhysicalSpaces[] allPhysicalSpaces, List<Animals> allAnimals, decimal cashOnHand, int dayNumber, out decimal remainingCashOnHand)
        {            
            bool exitMenu = false;
            bool invalidEntry;
            bool animalsNotPlaced;
            string optionNumberString = "";
            int optionNumberInt = 0;
            string quantityString = "";
            int quantityInt;
            decimal totalCost;
            string spaceOption;
            int lineCounter;

            do
            {
                Console.Clear();
                invalidEntry = true;
                animalsNotPlaced = true;
                spaceOption = "";
                lineCounter = 1;
                totalCost = 0;
                quantityInt = -1;
                remainingCashOnHand = cashOnHand;

                DisplayDashboard(dayNumber, cashOnHand);
                Console.WriteLine("-------------------- BUY NEW ANIMALS --------------------");
                Console.WriteLine();
                foreach (Animals animal in allAnimals)
                {
                    string dollarAmtSpacer = "";
                    string nameSpacer = "";
                    int standardNameLength = 18;
                    int nameSpacerLength = 0;

                    if (animal.Price >= 10000)
                    {
                        dollarAmtSpacer = "  ";
                    }                
                    else if (animal.Price >= 1000 && animal.Price < 10000)
                    {
                        dollarAmtSpacer = "   ";
                    }
                    else if (animal.Price >= 100 && animal.Price < 1000)
                    {
                        dollarAmtSpacer = "     ";
                    }
                    else if (animal.Price >= 10 && animal.Price < 100)
                    {
                        dollarAmtSpacer = "      ";
                    }
                    else if (animal.Price >= 0 && animal.Price < 10)
                    {
                        dollarAmtSpacer = "       ";
                    }

                    nameSpacerLength = standardNameLength - animal.Name.Length;

                    for (int i = 0; i < nameSpacerLength; i++)
                    {
                        nameSpacer += " ";
                    }


                    if (lineCounter < 10)
                    {
                        Console.WriteLine($"{lineCounter}.  {animal.Name}{nameSpacer}{dollarAmtSpacer}{animal.Price:C0}" +
                                          $"\t\tCurrently Own: {animal.Quantity}");
                    }
                    else
                    {
                        Console.WriteLine($"{lineCounter}. {animal.Name}{nameSpacer}{dollarAmtSpacer}{animal.Price:C0}" +
                                          $"\t\tCurrently Own: {animal.Quantity}");
                    }
                   
                    lineCounter++;
                }
                Console.WriteLine();
                Console.WriteLine("Enter the number of the animal you wish to buy, or \"E\" to exit:");

                do
                {
                    optionNumberString = Console.ReadLine().ToUpper();
                    Console.WriteLine();

                    if (Int32.TryParse(optionNumberString, out optionNumberInt) && optionNumberInt > 0 && optionNumberInt <= allAnimals.Count ||
                        optionNumberString == "E")
                    {
                        invalidEntry = false;
                        break;
                    }

                    Console.WriteLine("Invalid entry. Please try again.");
                }
                while (invalidEntry);

                if (optionNumberString == "E")
                {
                    break;
                }

                Console.WriteLine($"Enter the quantity of " + allAnimals[optionNumberInt - 1].Name.Pluralize() + " you wish to buy:");
                quantityString = Console.ReadLine();
                Console.WriteLine();

                while (!Int32.TryParse(quantityString, out quantityInt) || quantityInt <= 0)
                {
                    Console.WriteLine("Invalid entry. Please try again.");
                    quantityString = Console.ReadLine();
                    Console.WriteLine();
                }

                totalCost = allAnimals[optionNumberInt - 1].Price * quantityInt;

                if (cashOnHand < totalCost)
                {
                    Console.WriteLine("Uh oh! You don't have enough money to make this purchase. Please start a new purchase.");
                    Console.WriteLine();
                    Console.WriteLine("Press \"Enter\" to continue");
                    Console.ReadLine();
                }
                else
                {
                    remainingCashOnHand = cashOnHand - totalCost;
                    cashOnHand = remainingCashOnHand;
                    allAnimals[optionNumberInt - 1].Buy(quantityInt);

                    if (quantityInt != 1 && (allAnimals[optionNumberInt - 1].Quantity - quantityInt) == 0)
                    {
                        Console.WriteLine($"Success! You just purchased {quantityString} {allAnimals[optionNumberInt - 1].Name.Pluralize()}!");
                        Console.WriteLine();
                        DisplayZooMapLite(allPhysicalSpaces);
                        Console.WriteLine($"Now, where would you like to put the {allAnimals[optionNumberInt - 1].Name.Pluralize()}" +
                                           "?\r\nEnter a space (A-P) from the Zoo Map.");
                    }
                    else if (allAnimals[optionNumberInt - 1].Quantity - quantityInt == 0)
                    {
                        Console.WriteLine($"Success! You just purchased a {allAnimals[optionNumberInt - 1].Name}!");
                        Console.WriteLine();
                        DisplayZooMapLite(allPhysicalSpaces);
                        Console.WriteLine($"Now, where would you like to put the {allAnimals[optionNumberInt - 1].Name}" +
                                           "?\r\nEnter a space (A-P) from the Zoo Map.");
                    }
                    else if (quantityInt !=1)
                    {
                        Console.WriteLine($"Success! You just purchased {quantityString} {allAnimals[optionNumberInt - 1].Name.Pluralize()}!");
                        Console.WriteLine();
                        Console.WriteLine($"Each type of animal can only live in one exhibit. Because you already have a {allAnimals[optionNumberInt - 1].Name} exhibit," +
                                          $"\r\nyour new {allAnimals[optionNumberInt - 1].Name.Pluralize()} were added to exhibit {allAnimals[optionNumberInt - 1].Location}.");
                        
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
                        Console.WriteLine("Press \"Enter\" to continue");
                        Console.ReadLine();
                        animalsNotPlaced = false;
                        continue;
                    }
                    else
                    {
                        Console.WriteLine($"Success! You just purchased {quantityString} {allAnimals[optionNumberInt - 1].Name}!");
                        Console.WriteLine();
                        Console.WriteLine($"Each type of animal can only live in one exhibit. Because you already have a {allAnimals[optionNumberInt - 1].Name} exhibit," +
                                          $"\r\nyour new {allAnimals[optionNumberInt - 1].Name} was added to exhibit {allAnimals[optionNumberInt - 1].Location}.");
                        
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
                        Console.WriteLine("Press \"Enter\" to continue");
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

                            if (quantityInt != 1)
                            {
                                Console.WriteLine($"Success! You just placed your new {allAnimals[optionNumberInt - 1].Name.Pluralize()} in exhibit { spaceOption}.");


                            }
                            else
                            {
                                Console.WriteLine($"Success! You just placed your new {allAnimals[optionNumberInt - 1].Name} in exhibit { spaceOption}.");
                            }

                            Console.WriteLine();
                            Console.WriteLine("Press \"Enter\" to continue");
                            Console.ReadLine();
                            animalsNotPlaced = false;
                        }
                        else if (allPhysicalSpaces[spaceOptions.IndexOf(spaceOption)].OccupyingAnimals != "" && allPhysicalSpaces[spaceOptions.IndexOf(spaceOption)].OccupyingAnimals == allAnimals[optionNumberInt - 1].Name)
                        {
                            allPhysicalSpaces[spaceOptions.IndexOf(spaceOption)].AnimalQuantity += quantityInt;
                            allAnimals[optionNumberInt - 1].Location = allPhysicalSpaces[spaceOptions.IndexOf(spaceOption)].SpaceID;

                            if (quantityInt != 1)
                            {
                                Console.WriteLine($"Success! You just added your new {allAnimals[optionNumberInt - 1].Name.Pluralize()} to exhibit { spaceOption}.");


                            }
                            else
                            {
                                Console.WriteLine($"Success! You just added your new {allAnimals[optionNumberInt - 1].Name} to exhibit { spaceOption}.");
                            }

                            Console.WriteLine();
                            Console.WriteLine("Press \"Enter\" to continue");
                            Console.ReadLine();
                            animalsNotPlaced = false;
                        }
                        else
                        {
                            Console.WriteLine("Hmm, that exhibit is already taken by a different type of animal. Select a different space for these animals.");
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

        static void ArrangeAnimals(PhysicalSpaces[] allPhysicalSpaces, List<Animals> allAnimals, int dayNumber, decimal cashOnHand)
        {
            bool exit = false;
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
                List<Animals> selectableAnimals = new List<Animals>();

                Console.Clear();
                DisplayDashboard(dayNumber, cashOnHand);

                foreach (Animals animal in allAnimals)
                {
                    totalNumberOfAnimals += animal.Quantity;
                }

                if (totalNumberOfAnimals == 0)
                {
                    Console.WriteLine("It looks like you don't have any animals right now.\r\nCome back after you've purchased some!");
                    Console.WriteLine();
                    Console.WriteLine("Press \"Enter\" to continue");
                    Console.ReadLine();
                    return;
                }

                DisplayZooMapLite(allPhysicalSpaces);
                Console.WriteLine("--------------- ARRANGE ZOO ANIMALS ---------------");
                Console.WriteLine();
                foreach (Animals animal in allAnimals)
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

                        Console.WriteLine(counter + ". " + animal.Name + nameSpacer + "Qty: " + animal.Quantity + "\tCurrent Location: " + animal.Location);
                        validUserOptions.Add(counter.ToString());
                        counter++;
                    }
                }
                
                validUserOptions.Add("E");
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

        static void EditConcessionsItems(List<Sundry> allConcessionsItems, int dayNumber, decimal cashOnHand)
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
                DisplayDashboard(dayNumber, cashOnHand);
                Console.WriteLine("------ CHANGE CONCESSIONS STAND ITEMS ------");
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
                    Console.WriteLine("Press \"Enter\" to continue");
                    Console.ReadLine();
                }
                else if (userInput == "1")
                {
                    Console.WriteLine("--- ADD A NEW ITEM ---");
                    Console.WriteLine("Enter the name of the new item:");
                    newItemNameOrSelection = Console.ReadLine();
                    Console.WriteLine();
                    Console.WriteLine("Enter the price of the new item:");
                    newItemPriceString = Console.ReadLine();

                    while (!Decimal.TryParse(newItemPriceString, out newItemPrice) || newItemPrice <= 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Invalid price. Please enter a positive dollar amount for the item's price (ex: 3.00):");
                        newItemPriceString = Console.ReadLine();
                    }

                    allConcessionsItems.Add(new Sundry(newItemNameOrSelection, newItemPrice));
                    Console.WriteLine();
                    Console.WriteLine($"Success! The item {newItemNameOrSelection} was added to the concessions stand!");
                    Console.WriteLine();
                    Console.WriteLine("Press \"Enter\" to continue");
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
                    Console.WriteLine("Are you sure you want to delete the " + allConcessionsItems[Convert.ToInt32(newItemNameOrSelection) - 1].Name +
                                      " item? Enter \"Y\" or \"N\":");
                    confirmChoice = GetUserInput(new List<string>() { "Y", "N" });

                    if (confirmChoice == "Y")
                    {
                        allConcessionsItems.RemoveAt(Convert.ToInt32(newItemNameOrSelection) - 1);
                        Console.WriteLine();
                        Console.WriteLine("Item successfully removed!");
                        Console.WriteLine();
                        Console.WriteLine("Press \"Enter\" to continue");
                        Console.ReadLine();
                    }
                    else if (confirmChoice == "N")
                    {
                        Console.WriteLine();
                        Console.WriteLine("That was close! You almost deleted that item!");
                        Console.WriteLine("It's good you kept it, because the zoo patrons should start buying " +
                                           allConcessionsItems[Convert.ToInt32(newItemNameOrSelection) - 1].Name +
                                          "s any day now!");
                        Console.WriteLine();
                        Console.WriteLine("Press \"Enter\" to continue");
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

                    while (!Decimal.TryParse(newItemPriceString, out newItemPrice) || newItemPrice <= 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Invalid price. Please enter a positive dollar amount for the item's price (ex: 3.00):");
                        newItemPriceString = Console.ReadLine();
                    }

                    allConcessionsItems[Convert.ToInt32(newItemNameOrSelection) - 1].Price = newItemPrice;
                    Console.WriteLine();
                    Console.WriteLine("Item price successfully changed!");
                    Console.WriteLine();
                    Console.WriteLine("Press \"Enter\" to continue");
                    Console.ReadLine();
                }
            }
            while (!exit);
        }

        static void EditGiftShopItems(List<Sundry> allGiftShopItems, int dayNumber, decimal cashOnHand)
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
                DisplayDashboard(dayNumber, cashOnHand);
                Console.WriteLine("------ CHANGE GIFT SHOP ITEMS ------");
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
                    Console.WriteLine("Press \"Enter\" to continue");
                    Console.ReadLine();
                }
                else if (userInput == "1")
                {
                    Console.WriteLine("--- ADD A NEW ITEM ---");
                    Console.WriteLine("Enter the name of the new item:");
                    newItemNameOrSelection = Console.ReadLine();
                    Console.WriteLine();
                    Console.WriteLine("Enter the price of the new item:");
                    newItemPriceString = Console.ReadLine();

                    while (!Decimal.TryParse(newItemPriceString, out newItemPrice) || newItemPrice <= 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Invalid price. Please enter a positive dollar amount for the item's price (ex: 3.00):");
                        newItemPriceString = Console.ReadLine();
                    }

                    allGiftShopItems.Add(new Sundry(newItemNameOrSelection, newItemPrice));
                    Console.WriteLine();
                    Console.WriteLine($"Success! The item {newItemNameOrSelection} was added to the gift shop!");
                    Console.WriteLine();
                    Console.WriteLine("Press \"Enter\" to continue");
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
                    Console.WriteLine("Are you sure you want to delete the " + allGiftShopItems[Convert.ToInt32(newItemNameOrSelection)-1].Name +
                                      " item? Enter \"Y\" or \"N\":");
                    confirmChoice = GetUserInput(new List<string>() { "Y", "N" });

                    if (confirmChoice == "Y")
                    {
                        allGiftShopItems.RemoveAt(Convert.ToInt32(newItemNameOrSelection) - 1);
                        Console.WriteLine();
                        Console.WriteLine("Item successfully removed!");
                        Console.WriteLine();
                        Console.WriteLine("Press \"Enter\" to continue");
                        Console.ReadLine();
                    }
                    else if (confirmChoice == "N")
                    {
                        Console.WriteLine();
                        Console.WriteLine("Whew! The item was not deleted.");
                        Console.WriteLine("Who knows? Maybe the " + allGiftShopItems[Convert.ToInt32(newItemNameOrSelection)-1].Name +
                                          "s will start flying off the shelves!");
                        Console.WriteLine();
                        Console.WriteLine("Press \"Enter\" to continue");
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

                    while (!Decimal.TryParse(newItemPriceString, out newItemPrice) || newItemPrice <= 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Invalid price. Please enter a positive dollar amount for the item's price (ex: 3.00):");
                        newItemPriceString = Console.ReadLine();
                    }

                    allGiftShopItems[Convert.ToInt32(newItemNameOrSelection) - 1].Price = newItemPrice;
                    Console.WriteLine();
                    Console.WriteLine("Item price successfully changed!");
                    Console.WriteLine();
                    Console.WriteLine("Press \"Enter\" to continue");
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
            int standardNameLength = 21;
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

        static void SellZooAnimals(PhysicalSpaces[] allPhysicalSpaces, List<Animals> allAnimals, decimal cashOnHand, int dayNumber, out decimal remainingCashOnHand)
        {
            int lineCounter;
            string optionNumberString;
            int optionNumberInt;
            string sellQuantityString;
            int sellQuantityInt;
            int totalSale;
            bool exit = false;
            string confirmChoice;
            remainingCashOnHand = cashOnHand;

            do
            {
                Console.Clear();
                lineCounter = 1;
                totalSale = 0;

                DisplayDashboard(dayNumber, cashOnHand);

                int totalAnimals = 0;
                foreach (Animals animal in allAnimals)
                {
                    totalAnimals += animal.Quantity;
                }

                if (totalAnimals == 0)
                {
                    Console.WriteLine("It looks like you don't have any animals right now.\r\nCome back after you've purchased some!");
                    Console.WriteLine();
                    Console.WriteLine("Press \"Enter\" to continue");
                    Console.ReadLine();
                    return;
                }

                List<string> validAnimalOptions = new List<string>();
                List<Animals> sellableAnimals = new List<Animals>();

                Console.WriteLine("-------------------------- SELL ANIMALS --------------------------");
                Console.WriteLine();
                foreach (Animals animal in allAnimals)
                {
                    string dollarAmtSpacer = "";
                    string nameSpacer = "";
                    int standardNameLength = 18;
                    int nameSpacerLength = 0;

                    if (animal.Quantity != 0)
                    {
                        validAnimalOptions.Add(lineCounter.ToString());
                        sellableAnimals.Add(animal);
                        
                        if (Math.Floor(animal.Price * .6m) >= 10000)
                        {
                            dollarAmtSpacer = " ";
                        }
                        else if (Math.Floor(animal.Price * .6m) >= 1000 && Math.Floor(animal.Price * .6m) < 10000)
                        {
                            dollarAmtSpacer = "  ";
                        }
                        else if (Math.Floor(animal.Price * .6m) >= 100 && Math.Floor(animal.Price * .6m) < 1000)
                        {
                            dollarAmtSpacer = "    ";
                        }
                        else if (Math.Floor(animal.Price * .6m) >= 10 && Math.Floor(animal.Price * .6m) < 100)
                        {
                            dollarAmtSpacer = "     ";
                        }
                        else if (Math.Floor(animal.Price * .6m) >= 0 && Math.Floor(animal.Price * .6m) < 10)
                        {
                            dollarAmtSpacer = "      ";
                        }

                        nameSpacerLength = standardNameLength - animal.Name.Length;

                        for (int i = 0; i < nameSpacerLength; i++)
                        {
                            nameSpacer += " ";
                        }


                        if (lineCounter < 10)
                        {
                            Console.WriteLine($"{lineCounter}.  {animal.Name}{nameSpacer}Sell Price:{dollarAmtSpacer}" +
                                              $"{Math.Floor(animal.Price * .6m):C0}\tCurrently Own: {animal.Quantity}");
                        }
                        else
                        {
                            Console.WriteLine($"{lineCounter}. {animal.Name}{nameSpacer}Sell Price:{dollarAmtSpacer}" +
                                              $"{Math.Floor(animal.Price * .6m):C0}\tCurrently Own: {animal.Quantity}");
                        }

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
                Console.WriteLine($"Enter the quantity of " + sellableAnimals[optionNumberInt - 1].Name.Pluralize() + " you wish to sell:");

                List<string> validSellQuantities = new List<string>();
                for (int i = 1; i <= sellableAnimals[optionNumberInt - 1].Quantity; i++)
                {
                    validSellQuantities.Add(i.ToString());
                }

                sellQuantityString = GetUserInput(validSellQuantities);
                sellQuantityInt = Convert.ToInt32(sellQuantityString);
                totalSale = Convert.ToInt32(Math.Floor(sellableAnimals[optionNumberInt - 1].Price * .6m)) * sellQuantityInt;

                Console.WriteLine();
                if (sellQuantityInt != 1)
                {
                    Console.WriteLine($"Are you sure you want to sell {sellQuantityInt} {sellableAnimals[optionNumberInt - 1].Name.Pluralize()}" +
                                      $" for a total of {totalSale:C2}?\r\nEnter \"Y\" or \"N\":");
                }
                else
                {
                    Console.WriteLine($"Are you sure you want to sell {sellQuantityInt} {sellableAnimals[optionNumberInt - 1].Name}" +
                                      $" for a total of {totalSale:C2}?\r\nEnter \"Y\" or \"N\":");
                }

                confirmChoice = GetUserInput(new List<string>() { "Y", "N" });

                if (confirmChoice == "Y")
                {
                    remainingCashOnHand = cashOnHand + totalSale;
                    cashOnHand = remainingCashOnHand;
                    sellableAnimals[optionNumberInt - 1].Sell(sellQuantityInt);

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

                    if (sellQuantityInt != 1)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Success! You sold " + sellQuantityInt + " " + sellableAnimals[optionNumberInt - 1].Name.Pluralize() + " for a total of $" + totalSale + ".");
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Success! You sold a " + sellableAnimals[optionNumberInt - 1].Name + " for $" + totalSale + ".");
                    }

                    Console.WriteLine();
                    Console.WriteLine("Press \"Enter\" to continue");
                    Console.ReadLine();
                }
                else if (confirmChoice == "N")
                {
                    Console.WriteLine();
                    if (sellQuantityInt != 1)
                    {
                        Console.WriteLine("That was a close call!\r\nThe " + sellableAnimals[optionNumberInt - 1].Name.Pluralize() +
                                          " are relieved they weren't sold, but will be sleeping with one eye open!");
                    }
                    else
                    {
                        Console.WriteLine("That was a close call!\r\nThe " + sellableAnimals[optionNumberInt - 1].Name +
                                                                  "is relieved to not be sold, but will be sleeping with one eye open!");
                    }
                    
                    Console.WriteLine();
                    Console.WriteLine("Press \"Enter\" to continue");
                    Console.ReadLine();
                }
            }
            while (!exit);
        }

        static void AdvanceDay(PhysicalSpaces[] allPhysicalSpaces, List<Animals> allAnimals, List<Sundry> allConcessionsItems, List<Sundry> allGiftShopItems, decimal cashOnHand, int dayNumberIn, out decimal remainingCashOnHand, out int dayNumber)
        {
            Random random = new Random();
            
            remainingCashOnHand = cashOnHand;
            dayNumber = dayNumberIn + 1;
            Console.Clear();
            DisplayDashboard(dayNumberIn, cashOnHand);

            double totalAnimalAttScore = 0;
            int attendance = 0;
            decimal totalAnimalCost = 0;
            decimal concessionsIncome = 0;

            foreach(Animals animal in allAnimals)
            {
                totalAnimalAttScore += animal.AttractionValue * animal.Quantity;
            }

            foreach (Animals animal in allAnimals)
            {
                totalAnimalCost += animal.DailyCost * animal.Quantity;
            }

            if (totalAnimalAttScore < 1000)
            {
                attendance = random.Next(0, 50);
            }
            else if (totalAnimalAttScore >= 1000 && totalAnimalAttScore < 2000)
            {
                attendance = random.Next(50, 100);
            }
            else if (totalAnimalAttScore >= 2000 && totalAnimalAttScore < 3000)
            {
                attendance = random.Next(100, 150);
            }
            else if (totalAnimalAttScore >= 3000 && totalAnimalAttScore < 4000)
            {
                attendance = random.Next(150, 200);
            }
            else if (totalAnimalAttScore >= 4000 && totalAnimalAttScore < 5000)
            {
                attendance = random.Next(200, 250);
            }

            foreach (Sundry item in allConcessionsItems)
            {
                if (item.Price < 5)
                {
                    concessionsIncome += item.Price * attendance * .65m;
                }
                else if (item.Price >= 5 && item.Price < 10)
                {
                    concessionsIncome += item.Price * attendance * .45m;
                }
                else if (item.Price >= 10 && item.Price < 20)
                {
                    concessionsIncome += item.Price * attendance * .25m;
                }
                else if (item.Price >= 20 && item.Price < 50)
                {
                    concessionsIncome += item.Price * attendance * .15m;
                }
                else if (item.Price >= 50 && item.Price < 100)
                {
                    concessionsIncome += item.Price * attendance * .05m;
                }
                else if (item.Price >= 100)
                {
                    concessionsIncome += item.Price * attendance * (random.Next(0,5) / 100);
                }

                remainingCashOnHand = (decimal)(remainingCashOnHand + Convert.ToInt32(concessionsIncome) - Convert.ToInt32(totalAnimalCost));
            }
            
        }
        static void ExitProgram()
        {
            Console.WriteLine();
            Console.WriteLine("Thank you for playing! See you next time!");
        }
    }
}
