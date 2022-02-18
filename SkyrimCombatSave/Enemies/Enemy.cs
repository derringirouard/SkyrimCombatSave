using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SkyrimCombatSave.Enemies
{
	class Enemy
    {
        public string Name { get; set; }
        public int HP { get; set; }
        public int AttackPower { get; set; }

        public Enemy(string name, int hp, int attackPower)
        {
            Name = name;
            HP = hp;
            AttackPower = attackPower;
        }

        public int Attack(int targetHealth)
        {
            Console.WriteLine("\nThe " + Name + " attacks!");
            Thread.Sleep(1000);
            Random attackAttempt = new Random();
            int attackRoll = attackAttempt.Next(0, 5);
            if (attackRoll == 0)
            {
                Console.WriteLine(Name + " misses!");
                Thread.Sleep(1000);
                Console.Clear();
                return targetHealth;
            }
            else
            {
                Console.WriteLine(Name + " hits for " + AttackPower + " damage!");
                targetHealth -= AttackPower;
                Thread.Sleep(1000);
                Console.Clear();
                return targetHealth;
            }
        }
    }
}
