﻿using System;
using System.Threading;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SkyrimCombat
{
    
    class Program
    {
       static int EnemyAttacks(string enemyName, int playerHealth, int enemyAttack)
        {
            Console.WriteLine("\nThe " + enemyName + " attacks!");
            Thread.Sleep(1000);
            Random attackAttempt = new Random();
            int attackRoll = attackAttempt.Next(0, 5);
            if (attackRoll == 0)
            {
                Console.WriteLine(enemyName + " misses!");
                Thread.Sleep(1000);
                Console.Clear();
                return playerHealth;
            }
            else
            {
                Console.WriteLine(enemyName + " hits for " + enemyAttack + " damage!");
                playerHealth -= enemyAttack;
                Thread.Sleep(1000);
                Console.Clear();
                return playerHealth;
            }

            
        }

        static void CombatManager(string enemyID, string filePath, string characterFilePath, bool flee) //Method to initiate and handle combat until either player or enemy health reaches 0
        {
            List<string> playerInfo = new List<string>(); //Create list to hold player data
            foreach (string line in System.IO.File.ReadLines(characterFilePath)) //Goes through lines of player data
            {
                playerInfo = line.Split(",").ToList(); //Divides player data into usable data for variables

            }


            //playerInfo.ForEach(i => Console.WriteLine(i.ToString())); //Debug to validate player data
            //Console.WriteLine("Above is playerInfo from filePath read thing\n\n");

            //Console.ReadKey();
            //Assignment of player list values to variables

            string playerName = playerInfo[0];
            string playerHealthString = playerInfo[1];
            int playerHealth = Convert.ToInt32(playerHealthString);
            string playerAttackString = playerInfo[2];
            int playerAttack = Convert.ToInt32(playerAttackString);

            //End of player info retrieval 




            List<string> enemyInfo = new List<string>();
            string enemyInfoString = string.Empty;
            foreach (string line in System.IO.File.ReadLines(filePath))
            {

                if (line.Contains(enemyID))
                {
                    enemyInfo = line.Split(",").ToList();
                    break;
                }

            }


            //enemyInfo.ForEach(i => Console.WriteLine(i.ToString()));
            //Console.WriteLine("Above is enemyInfo from filePath read thing\n\n");
            //Console.ReadKey();


            string enemyName = enemyInfo[1];
            string enemyHealthString = enemyInfo[2];
            int enemyHealth = Convert.ToInt32(enemyHealthString);
            string enemyAttackString = enemyInfo[3];
            int enemyAttack = Convert.ToInt32(enemyAttackString);






            Console.Clear();
            Console.WriteLine("You have entered combat with the " + enemyName + "!\n");
            while (enemyHealth > 0 && playerHealth > 0 && flee == false) //Perform the following while Player and Enemy are still alive

            {
                Console.WriteLine("What do you want to do?");
                Console.WriteLine("1) Attack");
                Console.WriteLine("2) Flee");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine(playerName + "'s " + "HP: " + playerHealth + "                 " + enemyName + " HP: " + enemyHealth);
                string action = Console.ReadLine().ToLower();
                if (action == "attack" || action == "1")
                {
                    Random critAttempt = new Random();
                    int thisRoll = critAttempt.Next(0, 16);
                    if (thisRoll == 15)
                    {
                        enemyHealth -= (playerAttack * 2);
                        Console.WriteLine("Critical hit!");
                        Console.WriteLine("You attack the " + enemyName + " for " + (playerAttack * 2) + " damage!");
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        enemyHealth -= playerAttack;
                        Console.WriteLine("You attack the " + enemyName + " for " + playerAttack + " damage!");
                        Thread.Sleep(1000);
                    }
                    
                    if (enemyHealth > 0)
                    {
                        playerHealth = EnemyAttacks(enemyName, playerHealth, enemyAttack);
                    }

                }
                else if (action == "2" || action == "flee")
                {
                    Console.WriteLine(playerName + " attempts to flee the encounter!");
                    Thread.Sleep(2500);
                    Random fleeAttempt = new Random();
                    int thisRoll = fleeAttempt.Next(0, 10);
                    if (thisRoll == 0)
                    {
                        Console.WriteLine("Failed to flee!");
                        Thread.Sleep(750);
                        playerHealth = EnemyAttacks(enemyName, playerHealth, enemyAttack);
                    }
                    else
                    {
                        flee = true; //Set flee to true to escape the while loop
                        Console.WriteLine(playerName + " successfully flees!");
                        Thread.Sleep(2000);
                        Console.Clear();
                    }

                }
                



            };
            if(playerHealth <= 0)
            {
                Console.WriteLine(playerName + " has perished!");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Environment.Exit(0);
            }
            else if(enemyHealth <= 0)
            {
                Console.WriteLine(enemyName + " has been defeated!");
                Thread.Sleep(1000);
            }
            flee = false;


        }
        
        static void Next()
        {
            Console.WriteLine("Press enter to continue");
            Console.ReadKey();
        }
        
        static void SaveGame(string savePoint, string characterFilePath, string playerName, int playerHealth, int playerAttack)
        {
            savePoint = "1";
            Console.WriteLine("Would you like to save? (y/n)");
            string action = Console.ReadLine().ToLower();
            if (action == "y")
            {
                string playerAttackString = Convert.ToString(playerAttack);
                string playerHealthString = Convert.ToString(playerHealth);
                File.WriteAllText(characterFilePath, string.Empty);
                List<string> list = new List<string>();
                list.Add(playerName + "," + playerHealthString + ","+ playerAttackString + "," + savePoint);
                File.WriteAllLines(characterFilePath, list);
                Console.WriteLine("Game saved!");
                Next();
            }
        }
        
        
        
        
        
        
        static void Main(string[] args)
        {
            string filePath = @"C:\Users\Traffic\source\repos\SkyrimCombatSave\EnemyData.txt"; //Set file path of enemy data
            string characterFilePath = @"C:\Users\Traffic\source\repos\SkyrimCombatSave\CharacterData.txt";
            string enemyID = "1)"; //Initialize variable to use to search EnemyData.txt for the enemy to battle
            bool flee = false;
            string action = string.Empty;

            List<string> enemySets = new List<string>(File.ReadAllLines(filePath)); //Create list of enemy data pulled from EnemyData.txt
            



           List<string> playerInfo = new List<string>();
                foreach (string line in System.IO.File.ReadLines(characterFilePath))
                {
                    playerInfo = line.Split(",").ToList();
                    
                }

                string playerName = playerInfo[0];
                string playerHealthString = playerInfo[1];
                int playerHealth = Convert.ToInt32(playerHealthString);
                string playerAttackString = playerInfo[2];
                int playerAttack = Convert.ToInt32(playerAttackString);
                string savePoint = playerInfo[3];

         

            
            Console.WriteLine("        ....            ..       ..    ..        ..   .......     ..  ...              ...");
            Thread.Sleep(250);
            Console.WriteLine("     ...     ...        ..      ..      ..      ..    ..     ..   ..  ....            ....");
            Thread.Sleep(250);
            Console.WriteLine("   ...                  ..     ..        ..    ..     ..     ..   ..  .. ..          .. ..");
            Thread.Sleep(250);
            Console.WriteLine("   ...                  ..    ..          ..  ..      ..     ..   ..  ..  ..        ..  ..");
            Thread.Sleep(250);
            Console.WriteLine("       ....             ..  ..             ....       .. .. ..    ..  ..   ..      ..   ..");
            Thread.Sleep(250);
            Console.WriteLine("        .....           .. ..               ..        .. ..       ..  ..    ..    ..    ..");
            Thread.Sleep(250);
            Console.WriteLine("              ...       .. ..               ..        ..  ..      ..  ..     ..  ..     ..");
            Thread.Sleep(250);
            Console.WriteLine("                ...     ..    ..            ..        ..   ..     ..  ..      ....      ..");
            Thread.Sleep(250);
            Console.WriteLine("    ...        ...      ..      ..          ..        ..    ..    ..  ..       ..       ..");
            Thread.Sleep(250);
            Console.WriteLine("     ...    ...         ..       ..         ..        ..     ..   ..  ..                ..");
            Thread.Sleep(250);
            Console.WriteLine("         ...            ..       ..         ..        ..      ..  ..  ..                ..");
            Console.WriteLine("\n                                      A Text Adventure");
            Console.WriteLine("\n                                     A Title by Snoopert                                  ");


            Console.WriteLine("\nWhat do you want to do?");
            Console.WriteLine("\n\n\n1) Start New Game");
            Console.WriteLine("2) Continue");
            action = Console.ReadLine();

            
            if (action == "1")
            {
                Console.Clear();
                Console.WriteLine("What's your characters name?");
                string characterName = Console.ReadLine();
                File.WriteAllText(characterFilePath, string.Empty);
                List<string> list = new List<string>();
                list.Add(characterName + ",100,15,0");
                File.WriteAllLines(characterFilePath, list);
                foreach (string line in System.IO.File.ReadLines(characterFilePath))
                {
                    playerInfo = line.Split(",").ToList();

                }

                playerName = playerInfo[0];
                savePoint = playerInfo[3];
                Console.Clear();
            }else if(action == "2")
            {
                Console.Write("Returning to your place in Skyrim");
                Thread.Sleep(750);
                Console.Write(".");
                Thread.Sleep(750);
                Console.Write(".");
                Thread.Sleep(750);
                Console.Write(".");
                foreach (string line in System.IO.File.ReadLines(characterFilePath))
                {
                    playerInfo = line.Split(",").ToList();

                }
                Console.Clear();
            }
            
            
            if (savePoint == "0")
            {


                Console.WriteLine("You step out of a cave entrance onto the top of a mountain. A dragon flies overhead toward the east \nand a path lays before you heading north. What do you do?");
                Console.WriteLine("\n\n\n1) Follow the path.");
                Console.WriteLine("2) Engage the dragon.");
                Console.WriteLine("3) Try to take a shortcut down the east side of the mountain.");
                action = Console.ReadLine().ToLower();

                if (action == "1" || action == "path")
                {
                    Console.WriteLine("You walk down the path. Admiring the blooming flowers and greenery. You hear rustling in the distance.");


                }
                else if (action == "2")
                {
                    enemyID = "Dragon";
                    CombatManager(enemyID, filePath, characterFilePath, flee);
                }
                else if (action == "3")
                {
                    Console.Clear();
                    Console.WriteLine("You attempt to make your way down the rough terrain. After hopping over a few rocks and tripping in some \nbrush, you tumble a few dozen feet onto a familiar path taking 5 damage.");
                    playerHealth -= 5;
                    Console.WriteLine("Current HP: " + playerHealth);
                    Next();
                }

                Console.Clear();
                Console.WriteLine("A wolf is approaching quickly from the side of the path. There aren't many good places to hide. What do you do?");
                Console.WriteLine("\n\n\n1) Attempt to hide anyway.");
                Console.WriteLine("2) Try to get the first hit on it.");
                Console.WriteLine("3) Cut off of the path and try to get down the mountain.");
                action = Console.ReadLine().ToLower();

                if (action == "1" || action == "path")
                {

                    Console.WriteLine("You attempt to hide.");
                    Thread.Sleep(1500);
                    Random fleeAttempt = new Random();
                    int thisRoll = fleeAttempt.Next(0, 10);
                    if (thisRoll < 9)
                    {
                        Console.WriteLine("Failed to hide!");
                        Thread.Sleep(1000);
                        enemyID = "Wolf";
                        CombatManager(enemyID, filePath, characterFilePath, flee);

                    }
                    else
                    {
                        Console.WriteLine("You narrowly avoid the wolfs gaze as you lay flat in the grass on the side of the road. Luck seems to be on your side.");
                    }
                }
                else if (action == "2")
                {
                    enemyID = "Wolf";
                    CombatManager(enemyID, filePath, characterFilePath, flee);
                }
                else if (action == "3")
                {
                    Console.Clear();
                    Console.WriteLine("You attempt to make your way down the rough terrain. After hopping over a few rocks and tripping in some \nbrush, you tumble a few dozen feet onto a path near a river taking 5 damage.");
                    playerHealth -= 5;
                    Console.WriteLine("Current HP: " + playerHealth);
                    Console.ReadKey();
                }
                SaveGame(savePoint, characterFilePath, playerName, playerHealth, playerAttack);
            }
           


            if(savePoint == "1")
            {
                Console.WriteLine("Another wolf appears!");
                enemyID = "Wolf";
                Thread.Sleep(1000);
                CombatManager(enemyID, filePath, characterFilePath, flee);
                Next();

            }

            if(savePoint == "2")
            {
                Console.WriteLine("You approach a walled off city. As you approach a guard stops you.");
                Console.WriteLine("Halt. What business do you have in the city?");
                Console.WriteLine("\n\n\n1) I'm a businessman here to peddle my wares.\n2) My business is my business only.\n3) I'm a traveller. Was hoping to aquire some gear here.\n4) How dare you talk to me that way? (Attack him)");
                action = Console.ReadLine();
                if(action == "1")
                {
                    Console.WriteLine("Businessman eh? I suppose as long as you stay in line you are welcome. (he opens the main gate)");
                    Thread.Sleep(1000);
                    Console.WriteLine("You walk through the front gate. A surprised citizen dressed in higher end robes addresses you.");
                    Console.WriteLine("Do you get to the cloud district often? (He looks you over) Oh...of course you don't.");
                    Console.WriteLine("\n\n\n1) Attack him\n2) Attack him\n3) Attack him\n4) Attack him");
                    Console.ReadLine();
                    enemyID = "Nazeem";
                    CombatManager(enemyID, filePath, characterFilePath, flee);

                }
                else if(action == "2")
                {
                    
                        Console.WriteLine("You'd better keep that attitude in check. We don't need you sort around here. Beat it.");
                        Thread.Sleep(1000);
                        Console.WriteLine("\n\n\n1) Sorry. Been a long trip. I'll stay in line.\n2) Fight the guard.\n3) Leave the city.");
                        action = Console.ReadLine();
                    if(action == "1")
                    {
                        Console.WriteLine("I see. Please be sure not to cause any trouble inside. (He opens the main gate)");
                        Thread.Sleep(1000);
                        Console.WriteLine("You walk through the front gate. A surprised citizen dressed in higher end robes addresses you.");
                        Console.WriteLine("Do you get to the cloud district often? (He looks you over) Oh...of course you don't.");
                        Console.WriteLine("\n\n\n1) Attack him\n2) Attack him\n3) Attack him\n4) Attack him");
                        Console.ReadLine();
                        enemyID = "Nazeem";
                        CombatManager(enemyID, filePath, characterFilePath, flee);

                    }
                    else if (action == "2")
                    {
                        enemyID = "Guard";
                        CombatManager(enemyID, filePath, characterFilePath, flee);
                        Console.WriteLine("You bust open the front gate. A surprised citizen dressed in higher end robes addresses you.");
                        Console.WriteLine("Do you get to the cloud district often? (He looks you over) Oh...of course you don't.");
                        Console.WriteLine("\n\n\n1) Attack him\n2) Attack him\n3) Attack him\n4) Attack him");
                        Console.ReadLine();
                        enemyID = "Nazeem";
                        CombatManager(enemyID, filePath, characterFilePath, flee);

                    }
                    else if(action == "3")
                    {
                        Console.WriteLine("You leave the city and are eaten by a dragon.");
                        Environment.Exit(0);

                    }


                }
                else if(action == "3")
                {
                    Console.WriteLine("I see. Please be sure not to cause any trouble inside. (He opens the main gate)");
                    Thread.Sleep(1000);
                    Console.WriteLine("You walk through the front gate. A surprised citizen dressed in higher end robes addresses you.");
                    Console.WriteLine("Do you get to the cloud district often? (He looks you over) Oh...of course you don't.");
                    Console.WriteLine("\n\n\n1) Attack him\n2) Attack him\n3) Attack him\n4) Attack him");
                    Console.ReadLine();
                    enemyID = "Nazeem";
                    CombatManager(enemyID, filePath, characterFilePath, flee);

                }
                else if(action == "4")
                {
                    enemyID = "Guard";
                    CombatManager(enemyID, filePath, characterFilePath, flee);
                    Console.WriteLine("You bust open the front gate. A surprised citizen dressed in higher end robes addresses you.");
                    Console.WriteLine("Do you get to the cloud district often? (He looks you over) Oh...of course you don't.");
                    Console.WriteLine("\n\n\n1) Attack him\n2) Attack him\n3) Attack him\n4) Attack him");
                    Console.ReadLine();
                    enemyID = "Nazeem";
                    CombatManager(enemyID, filePath, characterFilePath, flee);

                }

            }


           
        }
    }
}
