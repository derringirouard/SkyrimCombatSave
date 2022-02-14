using System;
using System.Threading;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SkyrimCombat
{
    
    class Program
    {
       static int enemyAttacks(string enemyName, int playerHealth, int enemyAttack)
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
            }
            else
            {
                Console.WriteLine(enemyName + " hits for " + enemyAttack + " damage!");
                playerHealth -= enemyAttack;
                Thread.Sleep(1000);
                Console.Clear();
                Console.WriteLine(playerHealth);
            }
            return playerHealth;
        }

        static void combatManager(string enemyID, string filePath, string characterFilePath, bool flee) //Method to initiate and handle combat until either player or enemy health reaches 0
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
                    enemyHealth -= playerAttack;
                    Console.WriteLine("You attack the " + enemyName + " for " + playerAttack + " damage!");
                    Thread.Sleep(1000);

                    enemyAttacks(enemyName, playerHealth, enemyAttack);


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
                        enemyAttacks(enemyName, playerHealth, enemyAttack);
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
            }else if(enemyHealth <= 0)
            {
                Console.WriteLine(enemyName + " has been defeated!");
                Thread.Sleep(1000);
            }
            flee = false;


        }
        
        
        
        
        
        
        
        
        
        static void Main(string[] args)
        {
            string savePoint = "0"; //Use this in a massive if/else if statement to track progress. Setting savepoint to that checkpoint value at the end of the previous chunk of code.
            string filePath = @"C:\Users\Traffic\source\repos\SkyrimCombatSave\EnemyData.txt"; //Set file path of enemy data
            string characterFilePath = @"C:\Users\Traffic\source\repos\SkyrimCombatSave\CharacterData.txt";
            string enemyID = "4)"; //Initialize variable to use to search EnemyData.txt for the enemy to battle
            int playerHealth = 1000; //Set initial player health
            bool flee = false;

            List<string> enemySets = new List<string>(File.ReadAllLines(filePath)); //Create list of enemy data pulled from EnemyData.txt
            



           // static void getPlayerInfo(string filePath)
            {
                List<string> playerInfo = new List<string>();
                foreach (string line in System.IO.File.ReadLines(filePath))
                {
                    playerInfo = line.Split(",").ToList();
                    
                }


                //playerInfo.ForEach(i => Console.WriteLine(i.ToString()));
                //Console.WriteLine("Above is playerInfo from filePath read thing\n\n");

                //Console.ReadKey();
                string playerName = playerInfo[0];
                string playerHealthString = playerInfo[1];
               // int playerHealth = Convert.ToInt32(playerHealthString);
                string playerAttackString = playerInfo[2];
                int playerAttack = Convert.ToInt32(playerAttackString);
                string playerSave = playerInfo[3];

            }
                        
            combatManager(enemyID, filePath, characterFilePath, flee); //Call method for testing
            
        }
    }
}
