using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SkyrimCombatSave.Enemy
{
    internal class Enemy
    {
        string Name { get; set; }
        int HP { get; set; }
        int AttackPower { get; set; }
        int EXP { get; set; }

        public Enemy(string name, int hp, int attack, int exp)
        {
            Name = name;
            HP = hp;
            AttackPower = attack;
            EXP = exp;
        }

        public int Attack(int targetHealth)
        {
            Console.WriteLine("\nThe " + Name + " attacks!");
            Random attackAttempt = new Random();
            int attackRoll = attackAttempt.Next(0, 5);
            if (attackRoll == 0)
            {
                Console.WriteLine(Name + " misses!");
                Console.ReadKey();
                Console.Clear();
                return targetHealth;
            }
            else
            {
                Console.WriteLine(Name + " hits for " + AttackPower + " damage!");
                targetHealth -= AttackPower;
                Console.ReadKey();
                Console.Clear();
                return targetHealth;
            }
        }
    }
}
