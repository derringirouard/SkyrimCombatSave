using System;
using System.Threading;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SkyrimCombat
{

    class Program
    {
        static void RandomEncounter(string savePoint, string characterFilePath, bool flee, string action)
        {
            bool keepFighting = true;
            while (keepFighting == true)
            {
                List<string> playerInfo = new List<string>(); //Create list to hold player data
                foreach (string line in System.IO.File.ReadLines(characterFilePath)) //Goes through lines of player data
                {
                    playerInfo = line.Split(",").ToList(); //Divides player data into usable data for variables
                }
                string playerName = playerInfo[0];
                int playerHealth = Convert.ToInt32(playerInfo[2]);
                int playerAttack = Convert.ToInt32(playerInfo[3]);
                int playerEXP = Convert.ToInt32(playerInfo[5]);
                int playerMaxHealth = Convert.ToInt32(playerInfo[1]);
                int numberOfPotions = Convert.ToInt32(playerInfo[6]);
                Random random = new Random();
                int nextEnemyInt = random.Next(0, 100);
                if (nextEnemyInt <= 25)
                {
                    SkyrimCombatSave.Enemy.Enemy enemy = new SkyrimCombatSave.Enemy.Wolf();
                    CombatManager(characterFilePath, flee, playerEXP, savePoint, enemy, playerMaxHealth, numberOfPotions);
                }
                else if (nextEnemyInt > 25 && nextEnemyInt < 51)
                {
                    SkyrimCombatSave.Enemy.Enemy enemy = new SkyrimCombatSave.Enemy.Mudcrab();
                    CombatManager(characterFilePath, flee, playerEXP, savePoint, enemy, playerMaxHealth, numberOfPotions);
                }
                else if (nextEnemyInt > 50 && nextEnemyInt < 98)
                {
                    SkyrimCombatSave.Enemy.Enemy enemy = new SkyrimCombatSave.Enemy.Bandit();
                    CombatManager(characterFilePath, flee, playerEXP, savePoint, enemy, playerMaxHealth, numberOfPotions);
                }
                else if (nextEnemyInt > 97 && nextEnemyInt < 100)
                {
                    SkyrimCombatSave.Enemy.Enemy enemy = new SkyrimCombatSave.Enemy.Dragon();
                    CombatManager(characterFilePath, flee, playerEXP, savePoint, enemy, playerMaxHealth, numberOfPotions);
                }
                else
                {
                    SkyrimCombatSave.Enemy.Enemy enemy = new SkyrimCombatSave.Enemy.Slime();
                    CombatManager(characterFilePath, flee, playerEXP, savePoint, enemy, playerMaxHealth, numberOfPotions);
                }
                bool goodData = false;
                while (goodData == false)
                {
                    Console.Clear();
                    Console.WriteLine("Do you want to keep fighting? (y/n)");
                    action = Console.ReadLine().ToLower();
                
                
                    if (action == "y" || action == "1")
                    {
                        goodData = true;
                        Console.WriteLine("You search the area for something useful.");
                        Thread.Sleep(1000);
                        Random potionChance = new Random();
                        int potion = potionChance.Next(0, 4);
                        if (potion == 2)
                        {
                            Console.WriteLine("You found a potion!");
                            Thread.Sleep(1000);
                            numberOfPotions++;
                            UpdatePlayerInfo(characterFilePath, playerName, playerMaxHealth, playerHealth, playerAttack, playerEXP, savePoint, numberOfPotions);
                        }
                        else
                        {
                            Console.WriteLine("You find nothing of value.");
                            UpdatePlayerInfo(characterFilePath, playerName, playerMaxHealth, playerHealth, playerAttack, playerEXP, savePoint, numberOfPotions);
                            Thread.Sleep(1000);
                        }
                    }
                    else if (action == "n" || action == "2")
                    {
                        Console.WriteLine("You search the area for something useful.");
                        Thread.Sleep(1000);
                        Random potionChance = new Random();
                        int potion = potionChance.Next(0, 3);
                        if (potion == 2)
                        {
                            Console.WriteLine("You found a potion!");
                            Thread.Sleep(1000);
                            numberOfPotions++;
                            UpdatePlayerInfo(characterFilePath, playerName, playerMaxHealth, playerHealth, playerAttack, playerEXP, savePoint, numberOfPotions);
                        }
                        else
                        {
                            Console.WriteLine("You find nothing of value.");
                            UpdatePlayerInfo(characterFilePath, playerName, playerMaxHealth, playerHealth, playerAttack, playerEXP, savePoint, numberOfPotions);
                            Thread.Sleep(1000);
                        }
                        keepFighting = false;
                        goodData = true;
                        savePoint = "2";
                    }
                }
            }
        }
        static void UpdatePlayerInfo (string characterFilePath, string playerName, int playerMaxHealth, int playerHealth, int playerAttack, int playerEXP, string savePoint, int numberOfPotions)
        {
            File.WriteAllText(characterFilePath, string.Empty);
            List<string> list = new List<string>();
            string playerHealthString = Convert.ToString(playerHealth);
            string playerAttackString = Convert.ToString(playerAttack);
            string playerEXPString = Convert.ToString(playerEXP);
            string playerMaxHealthString = Convert.ToString(playerMaxHealth);
            list.Add(playerName + "," + playerMaxHealth + "," + playerHealth + "," + playerAttack + "," + savePoint + "," + playerEXP + "," + numberOfPotions);
            //Console.WriteLine(playerName + "'s stats:\nMax HP: " + playerMaxHealth + "\nAttack: " + playerAttack + "\nEXP: " + playerEXP);
            //Next();
            File.WriteAllLines(characterFilePath, list);//Writes latest player data to save file
        }
        static int CombatManager(string characterFilePath, bool flee, int playerEXP, string savePoint, SkyrimCombatSave.Enemy.Enemy enemy, int playerMaxHealth, int numberOfPotions) //Method to initiate and handle combat until either player or enemy health reaches 0
        {
            
            List<string> playerInfo = new List<string>(); //Create list to hold player data
            foreach (string line in System.IO.File.ReadLines(characterFilePath)) //Goes through lines of player data
            {
                playerInfo = line.Split(",").ToList(); //Divides player data into usable data for variables
            }
            //Assignment of player list values to variables
            string playerName = playerInfo[0];
            string playerHealthString = playerInfo[2];
            int playerHealth = Convert.ToInt32(playerHealthString);
            string playerAttackString = playerInfo[3];
            int playerAttack = Convert.ToInt32(playerAttackString);
            playerEXP = Convert.ToInt32(playerInfo[5]);
            string playerMaxHealthString = playerInfo[1];
            playerMaxHealth = Convert.ToInt32(playerMaxHealthString);
            string numberOfPotionsString = playerInfo[6];
            numberOfPotions = Convert.ToInt32(numberOfPotionsString);
            //End of player info retrieval 
           
            Console.Clear();
            Console.WriteLine("You have entered combat with the " + enemy.Name + "!\n");
            int enemyMaxHP = enemy.HP;
            while (enemy.HP > 0 && playerHealth > 0 && flee == false) //Perform the following while Player and Enemy are still alive
            {
                Console.WriteLine("What do you want to do?");
                Console.WriteLine("1) Attack");
                Console.WriteLine("2) Flee");
                Console.WriteLine("3) Use one of your " + numberOfPotions + " potions.");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine(playerName + "'s " + "HP: " + playerHealth + "/" + playerMaxHealth + "                 " + enemy.Name + " HP: " + enemy.HP + "/" + enemyMaxHP);
                string action = Console.ReadLine().ToLower();
                if (action == "attack" || action == "1")
                {
                    Random critAttempt = new Random();
                    int thisRoll = critAttempt.Next(0, 16);
                    if (thisRoll == 15)
                    {
                        enemy.HP -= (playerAttack * 2);
                        Console.WriteLine("Critical hit!");
                        Console.WriteLine("You attack the " + enemy.Name + " for " + (playerAttack * 2) + " damage!");
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        enemy.HP -= playerAttack;
                        Console.WriteLine("You attack the " + enemy.Name + " for " + playerAttack + " damage!");
                        Thread.Sleep(1000);
                    }

                    if (enemy.HP > 0)
                    {
                        playerHealth = enemy.Attack(playerHealth);
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
                        playerHealth = enemy.Attack(playerHealth);
                        UpdatePlayerInfo(characterFilePath, playerName, playerMaxHealth, playerHealth, playerAttack, playerEXP, savePoint, numberOfPotions);
                    }
                    else
                    {
                        flee = true; //Set flee to true to escape the while loop
                        Console.WriteLine(playerName + " successfully flees!");
                        Thread.Sleep(2000);
                        Console.Clear();
                        UpdatePlayerInfo(characterFilePath, playerName, playerMaxHealth, playerHealth, playerAttack, playerEXP, savePoint, numberOfPotions);
                    }
                }else if(action == "3" || action == "potion")
                {
                    Console.WriteLine("You use a potion and regain your health!");
                    numberOfPotions -= 1;
                    playerHealth = playerMaxHealth;
                    UpdatePlayerInfo(characterFilePath, playerName, playerMaxHealth, playerHealth, playerAttack, playerEXP, savePoint, numberOfPotions);
                    playerHealth = enemy.Attack(playerHealth);
                    Console.Clear();
                }
            }
            if (playerHealth <= 0)
            {
                Console.WriteLine(playerName + " has perished!");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Environment.Exit(0);
            }
            else if (enemy.HP <= 0)
            {
                Console.WriteLine(enemy.Name + " has been defeated!");
                Thread.Sleep(1000);
                playerEXP += enemy.EXP;
                Console.WriteLine("You've earned " + enemy.EXP + " EXP!");
                Console.WriteLine("Current exp: " + playerEXP);
                Next();
                playerInfo[5] = Convert.ToString(playerEXP);
                UpdatePlayerInfo(characterFilePath, playerName, playerMaxHealth, playerHealth, playerAttack, playerEXP, savePoint, numberOfPotions);
                while (playerEXP > 1000)
                {
                    playerEXP -= 1000;
                    Console.Clear();
                    Console.WriteLine("You have leveled up! Your attack and max health have increased!");
                    playerAttack += 5;
                    playerMaxHealth += 15;
                    playerHealth = playerMaxHealth;
                    UpdatePlayerInfo(characterFilePath, playerName, playerMaxHealth, playerHealth, playerAttack, playerEXP, savePoint, numberOfPotions);
                    Console.WriteLine("After level up combatmanager" + playerName + "'s stats:\nMax HP: " + playerMaxHealth + "\nAttack: " + playerAttack + "\nEXP: " + playerEXP);
                    Next();
                }

                flee = false;
                return playerEXP;
            } return playerEXP;
        }

            static void Next()
            {
                Console.WriteLine("Press enter to continue");
                Console.ReadKey();
            }
            //static void SaveGame(string savePoint, string characterFilePath, string playerName, int playerHealth, int playerAttack, int playerEXP)
            //{
            //    Console.WriteLine("Would you like to save? (y/n)");
            //    string action = Console.ReadLine().ToLower();
            //    if (action == "y")
            //    {
                    
            //    List<string> playerInfo = new List<string>(); //Create list to hold player data
            //    foreach (string line in System.IO.File.ReadLines(characterFilePath)) //Goes through lines of player data
            //    {
            //        playerInfo = line.Split(",").ToList(); //Divides player data into usable data for variables
            //    }
            //    //Assignment of player list values to variables
            //    File.WriteAllText(characterFilePath, string.Empty);
            //    File.WriteAllText(characterFilePath, string.Empty);
            //    List<string> list = new List<string>();
            //    List<string> newPlayerInfo = new List<string>();
            //    string playerHealthstring = Convert.ToString(playerHealth);
            //    string playerAttackstring = Convert.ToString(playerAttack);
            //    string playerEXPstring = Convert.ToString(playerEXP);
            //    list.Add(playerName + "," + playerHealth + "," + playerAttack + "," + savePoint + "," + playerEXP);
            //    File.WriteAllLines(characterFilePath, list);
            //        Console.WriteLine("Game saved!");
            //        Next();
            //    }
            //}

        static void Main(string[] args)
        {
            string filePath = @"C:\Users\Node5600X\source\repos\SkyrimCombatSave\EnemyData.txt"; //Set file path of enemy data
            string characterFilePath = @"C:\Users\Node5600X\source\repos\SkyrimCombatSave\CharacterData.txt";
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
            string playerHealthString = playerInfo[2];
            int playerHealth = Convert.ToInt32(playerHealthString);
            string playerAttackString = playerInfo[3];
            int playerAttack = Convert.ToInt32(playerAttackString);
            int playerEXP = Convert.ToInt32(playerInfo[5]);
            string playerMaxHealthString = playerInfo[1];
            int playerMaxHealth = Convert.ToInt32(playerMaxHealthString);
            string numberOfPotionsString = playerInfo[6];
            int numberOfPotions = Convert.ToInt32(numberOfPotionsString);
            string savePoint = playerInfo[4];

            Console.WriteLine("        ....            ..       ..    ..        ..   .......     ..  ...              ...");
            Thread.Sleep(100);
            Console.WriteLine("     ...     ...        ..      ..      ..      ..    ..     ..   ..  ....            ....");
            Thread.Sleep(100);
            Console.WriteLine("   ...                  ..     ..        ..    ..     ..     ..   ..  .. ..          .. ..");
            Thread.Sleep(100);
            Console.WriteLine("   ...                  ..    ..          ..  ..      ..     ..   ..  ..  ..        ..  ..");
            Thread.Sleep(100);
            Console.WriteLine("       ....             ..  ..             ....       .. .. ..    ..  ..   ..      ..   ..");
            Thread.Sleep(100);
            Console.WriteLine("        .....           .. ..               ..        .. ..       ..  ..    ..    ..    ..");
            Thread.Sleep(100);
            Console.WriteLine("              ...       .. ..               ..        ..  ..      ..  ..     ..  ..     ..");
            Thread.Sleep(100);
            Console.WriteLine("                ...     ..    ..            ..        ..   ..     ..  ..      ....      ..");
            Thread.Sleep(100);
            Console.WriteLine("    ...        ...      ..      ..          ..        ..    ..    ..  ..       ..       ..");
            Thread.Sleep(100);
            Console.WriteLine("     ...    ...         ..       ..         ..        ..     ..   ..  ..                ..");
            Thread.Sleep(100);
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
                list.Add(characterName + ",100,100,15,0,0,0");
                File.WriteAllLines(characterFilePath, list);
                foreach (string line in System.IO.File.ReadLines(characterFilePath))
                {
                    playerInfo = line.Split(",").ToList();
                }
                playerName = playerInfo[0];
                savePoint = playerInfo[4];
                playerAttack = Convert.ToInt32(playerInfo[3]);
                playerHealth = Convert.ToInt32(playerInfo[2]);
                playerEXP = Convert.ToInt32(playerInfo[5]);
                playerMaxHealth = Convert.ToInt32(playerInfo[1]);
                Console.Clear();
            }
            else if (action == "2")
            {
                Console.Write("Returning to your place in Skyrim");
                Thread.Sleep(500);
                foreach (string line in System.IO.File.ReadLines(characterFilePath))
                {
                    playerInfo = line.Split(",").ToList();
                }
                playerHealth = playerMaxHealth / 2;
                Console.Clear();
            }
            bool play = true;
            while (play == true)
            {

                if (savePoint == "0")
                {
                    Console.WriteLine("You step out of a cave entrance onto the top of a mountain. A dragon flies overhead\nand a path lays before you. What do you do?");
                    Console.WriteLine("\n\n\n1) Follow the path.");
                    Console.WriteLine("2) Engage the dragon.");
                    Console.WriteLine("3) Try to take a shortcut down the side of the mountain.");
                    action = Console.ReadLine().ToLower();

                    if (action == "1" || action == "path")
                    {
                        Console.WriteLine("You walk down the path. Admiring the blooming flowers and greenery. You hear rustling in the distance.");
                        Next();
                    }
                    else if (action == "2")
                    {
                        SkyrimCombatSave.Enemy.Enemy enemy = new SkyrimCombatSave.Enemy.Dragon();
                        enemyID = "Dragon";
                        playerEXP = CombatManager(characterFilePath, flee, playerEXP, savePoint, enemy, playerMaxHealth, numberOfPotions);
                    }
                    else if (action == "3")
                    {
                        Console.Clear();
                        Console.WriteLine("You attempt to make your way down the rough terrain. After hopping over a few rocks and tripping in some \nbrush, you tumble a few dozen feet onto a familiar path taking 5 damage.");
                        playerHealth -= 5;
                        Console.WriteLine("Current HP: " + playerHealth);
                        UpdatePlayerInfo(characterFilePath, playerName, playerMaxHealth, playerHealth, playerAttack, playerEXP, savePoint, numberOfPotions);
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
                        Thread.Sleep(1000);
                        Random fleeAttempt = new Random();
                        int thisRoll = fleeAttempt.Next(0, 10);
                        if (thisRoll < 9)
                        {
                            SkyrimCombatSave.Enemy.Enemy enemy = new SkyrimCombatSave.Enemy.Wolf();
                            Console.WriteLine("Failed to hide!");
                            Thread.Sleep(1000);

                            enemyID = "Wolf";
                            playerEXP = CombatManager(characterFilePath, flee, playerEXP, savePoint, enemy, playerMaxHealth, numberOfPotions);
                        }
                        else
                        {
                            Console.WriteLine("You narrowly avoid the wolfs gaze as you lay flat in the grass on the side of the road. Luck seems to be on your side.");
                        }
                    }
                    else if (action == "2")
                    {
                        enemyID = "Wolf";
                        SkyrimCombatSave.Enemy.Enemy enemy = new SkyrimCombatSave.Enemy.Wolf();
                        playerEXP = CombatManager(characterFilePath, flee, playerEXP, savePoint, enemy, playerMaxHealth, numberOfPotions);
                    }
                    else if (action == "3")
                    {
                        Console.Clear();
                        Console.WriteLine("You attempt to make your way down the rough terrain. After hopping over a few rocks and tripping in some \nbrush, you tumble a few dozen feet onto a path near a river taking 5 damage.");
                        playerHealth -= 5;
                        Console.WriteLine("Current HP: " + playerHealth);
                        UpdatePlayerInfo(characterFilePath, playerName, playerMaxHealth, playerHealth, playerAttack, playerEXP, savePoint, numberOfPotions);
                        Console.ReadKey();
                    }
                    savePoint = "1";
                    //SaveGame(savePoint, characterFilePath, playerName, playerHealth, playerAttack, playerEXP);
                }

                if (savePoint == "1")
                {
                    Console.WriteLine("Another wolf appears!");
                    enemyID = "Wolf";
                    Thread.Sleep(1000);
                    SkyrimCombatSave.Enemy.Enemy enemy = new SkyrimCombatSave.Enemy.Wolf();
                    playerEXP = CombatManager(characterFilePath, flee, playerEXP, savePoint, enemy, playerMaxHealth, numberOfPotions);
                    savePoint = "2";
                }

                if (savePoint == "2")
                {
                    Console.Clear();
                    Console.WriteLine("You approach a walled off city. As you approach a guard stops you.");
                    Console.WriteLine("Halt. What business do you have in the city?");
                    Console.WriteLine("\n\n\n1) I'm a businessman here to peddle my wares.\n2) My business is my business only.\n3) I'm a traveller. Was hoping to aquire some gear here.\n4) How dare you talk to me that way? (Attack him)\n5) The city doesn't interest you. Go explore the wilderness.");
                    action = Console.ReadLine();
                    if (action == "1")
                    {
                        Console.WriteLine("Businessman eh? I suppose as long as you stay in line you are welcome. (he opens the main gate)");
                        Thread.Sleep(1000);
                        Console.WriteLine("You walk through the front gate. A surprised citizen dressed in higher end robes addresses you.");
                        Console.WriteLine("Do you get to the cloud district often? (He looks you over) Oh...of course you don't.");
                        Console.WriteLine("\n\n\n1) Attack him\n2) Attack him\n3) Attack him\n4) Attack him");
                        Console.ReadLine();
                        SkyrimCombatSave.Enemy.Enemy enemy = new SkyrimCombatSave.Enemy.Nazeem();
                        enemyID = "Nazeem";
                        playerEXP = CombatManager(characterFilePath, flee, playerEXP, savePoint, enemy, playerMaxHealth, numberOfPotions);
                    }
                    else if (action == "2")
                    {
                        Console.WriteLine("You'd better keep that attitude in check. We don't need you sort around here. Beat it.");
                        Thread.Sleep(1000);
                        Console.WriteLine("\n\n\n1) Sorry. Been a long trip. I'll stay in line.\n2) Fight the guard.\n3) Leave the city.");
                        action = Console.ReadLine();
                        if (action == "1")
                        {
                            Console.WriteLine("I see. Please be sure not to cause any trouble inside. (He opens the main gate)");
                            Thread.Sleep(1000);
                            Console.WriteLine("You walk through the front gate. A surprised citizen dressed in higher end robes addresses you.");
                            Console.WriteLine("Do you get to the cloud district often? (He looks you over) Oh...of course you don't.");
                            Console.WriteLine("\n\n\n1) Attack him\n2) Attack him\n3) Attack him\n4) Attack him");
                            Console.ReadLine();
                            enemyID = "Nazeem";
                            SkyrimCombatSave.Enemy.Enemy enemy = new SkyrimCombatSave.Enemy.Nazeem();
                            playerEXP = CombatManager(characterFilePath, flee, playerEXP, savePoint, enemy, playerMaxHealth, numberOfPotions);
                        }
                        else if (action == "2")
                        {
                            enemyID = "Guard";
                            SkyrimCombatSave.Enemy.Enemy enemy = new SkyrimCombatSave.Enemy.Guard();
                            CombatManager(characterFilePath, flee, playerEXP, savePoint, enemy, playerMaxHealth, numberOfPotions);
                            Console.WriteLine("You bust open the front gate. A surprised citizen dressed in higher end robes addresses you.");
                            Console.WriteLine("Do you get to the cloud district often? (He looks you over) Oh...of course you don't.");
                            Console.WriteLine("\n\n\n1) Attack him\n2) Attack him\n3) Attack him\n4) Attack him");
                            Console.ReadLine();
                            enemyID = "Nazeem";
                            enemy = new SkyrimCombatSave.Enemy.Nazeem();
                            CombatManager(characterFilePath, flee, playerEXP, savePoint, enemy, playerMaxHealth, numberOfPotions);
                        }
                        else if (action == "3")
                        {
                            Console.WriteLine("You leave the city and are eaten by a dragon.");
                            Environment.Exit(0);
                        }
                    }
                    else if (action == "3")
                    {
                        Console.WriteLine("I see. Please be sure not to cause any trouble inside. (He opens the main gate)");
                        Thread.Sleep(1000);
                        Console.WriteLine("You walk through the front gate. A surprised citizen dressed in higher end robes addresses you.");
                        Console.WriteLine("Do you get to the cloud district often? (He looks you over) Oh...of course you don't.");
                        Console.WriteLine("\n\n\n1) Attack him\n2) Attack him\n3) Attack him\n4) Attack him");
                        Console.ReadLine();
                        enemyID = "Nazeem";
                        SkyrimCombatSave.Enemy.Enemy enemy = new SkyrimCombatSave.Enemy.Nazeem();
                        CombatManager(characterFilePath, flee, playerEXP, savePoint, enemy, playerMaxHealth, numberOfPotions);
                    }
                    else if (action == "4")
                    {
                        enemyID = "Guard";
                        SkyrimCombatSave.Enemy.Enemy enemy = new SkyrimCombatSave.Enemy.Guard();
                        CombatManager(characterFilePath, flee, playerEXP, savePoint, enemy, playerMaxHealth, numberOfPotions);
                        Console.WriteLine("You bust open the front gate. A surprised citizen dressed in higher end robes addresses you.");
                        Console.WriteLine("Do you get to the cloud district often? (He looks you over) Oh...of course you don't.");
                        Console.WriteLine("\n\n\n1) Attack him\n2) Attack him\n3) Attack him\n4) Attack him");
                        Console.ReadLine();
                        enemyID = "Nazeem";
                        enemy = new SkyrimCombatSave.Enemy.Nazeem();
                        CombatManager(characterFilePath, flee, playerEXP, savePoint, enemy, playerMaxHealth, numberOfPotions);
                    }
                    else if (action == "5")
                    {
                        Console.Clear();
                        Console.WriteLine("You turn around and walk from where you came. Wandering though the wilderness.");
                        Next();
                        RandomEncounter(savePoint, characterFilePath, flee, action);

                        //bool keepFighting = true;
                        //while (keepFighting == true)
                        //{
                        //    Random random = new Random();
                        //    int nextEnemyInt = random.Next(0, 100);
                        //    if (nextEnemyInt <= 25)
                        //    {
                        //        SkyrimCombatSave.Enemy.Enemy enemy = new SkyrimCombatSave.Enemy.Wolf();
                        //        playerEXP = CombatManager(characterFilePath, flee, playerEXP, savePoint, enemy, playerMaxHealth, numberOfPotions);
                        //    }
                        //    else if (nextEnemyInt > 25 && nextEnemyInt < 51)
                        //    {
                        //        SkyrimCombatSave.Enemy.Enemy enemy = new SkyrimCombatSave.Enemy.Mudcrab();
                        //        CombatManager(characterFilePath, flee, playerEXP, savePoint, enemy, playerMaxHealth, numberOfPotions);
                        //    }
                        //    else if (nextEnemyInt > 50 && nextEnemyInt < 98)
                        //    {
                        //        SkyrimCombatSave.Enemy.Enemy enemy = new SkyrimCombatSave.Enemy.Bandit();
                        //        CombatManager(characterFilePath, flee, playerEXP, savePoint, enemy, playerMaxHealth, numberOfPotions);
                        //    }
                        //    else if (nextEnemyInt > 97 && nextEnemyInt < 100)
                        //    {
                        //        SkyrimCombatSave.Enemy.Enemy enemy = new SkyrimCombatSave.Enemy.Dragon();
                        //        CombatManager(characterFilePath, flee, playerEXP, savePoint, enemy, playerMaxHealth, numberOfPotions);
                        //    }
                        //    else
                        //    {
                        //        SkyrimCombatSave.Enemy.Enemy enemy = new SkyrimCombatSave.Enemy.Slime();
                        //        CombatManager(characterFilePath, flee, playerEXP, savePoint, enemy, playerMaxHealth, numberOfPotions);
                        //    }

                        //    Console.WriteLine("Do you want to keep fighting? (y/n)");
                        //    action = Console.ReadLine().ToLower();
                        //    if (action == "y")
                        //    {
                        //        Console.WriteLine("You search the area for something useful.");
                        //        Thread.Sleep(1000);
                        //        Random potionChance = new Random();
                        //        int potion = potionChance.Next(0, 4);
                        //        if (potion == 2)
                        //        {
                        //            Console.WriteLine("You found a potion!");
                        //            Thread.Sleep(1000);
                        //            numberOfPotions++;
                        //            UpdatePlayerInfo(characterFilePath, playerName, playerMaxHealth, playerHealth, playerAttack, playerEXP, savePoint, numberOfPotions);
                        //        }
                        //        else
                        //        {
                        //            Console.WriteLine("You find nothing of value.");
                        //            Thread.Sleep(1000);
                        //        }
                        //    }
                        //    else
                        //    {
                        //        keepFighting = false;
                        //        savePoint = "2";
                                //SaveGame(savePoint, characterFilePath, playerName, playerHealth, playerAttack, playerEXP);
                            
                        
                    }
                }
            }
        }
    }
}