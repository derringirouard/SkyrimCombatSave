using System;
using System.Threading;
using System.Collections.Generic;
using System.IO;

namespace SkyrimCombat
{
    class Program
    {
        static void Main(string[] args)
        {
            string savePoint = "0"; //Use this in a massive if/else if statement to track progress. Setting savepoint to that checkpoint value at the end of the previous chunk of code.
            string filePath = @"C:\Users\Traffic\source\repos\SkyrimCombatSave\EnemyData.txt"; //Set file path of enemy data
            string enemyID = string.Empty; //Initialize variable to use to search EnemyData.txt for the enemy to battle
            int playerHealth = 100; //Set initial player health
            List<string> enemySets = new List<string>(File.ReadAllLines(filePath)); //Create list of enemy data pulled from EnemyData.txt
            



            static void combatManager(string enemyID, int playerHealth, List<string> enemySets) //Method to initiate and handle combat until either player or enemy health reaches 0
            {
                string enemyNum = enemyID + ")"; //Poorly create search term to find enemy in EnemyData.txt
                string enemy = enemySets.Find(x => x.enemyNum.ToLower().Equals(key.ToLower())); //Finds enemy within EnemyData.txt for encounter. Not sure what needs to change for it to work. 
                string enemyType = ;
                string enemyHealthString = ;
                int enemyHealth = Convert.ToInt32(enemyHealthString);

                Console.Clear();
                do //Perform the following while Player and Enemy are still alive
                {
                    Console.WriteLine("You have entered combat with " + enemyType + "!");
                    Console.WriteLine("\nWhat do you want to do?");
                    Console.WriteLine("1) Attack");
                    Console.WriteLine("2) Flee");
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("HP: " + playerHealth + "                 Enemy HP: " + enemyHealth);
                    string action = Console.ReadLine();
                
                }while (enemyHealth > 0 || playerHealth > 0);
                

            }
        }
    }
}
