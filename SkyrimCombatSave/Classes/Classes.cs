using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyrimCombatSave.Classes
{
    public class Enemy
    {
        public string Name { get; set; }
        public int HP { get; set; }
        public int Attack { get; set; }
        public int ID { get; set; }



    }

    public class Mudcrab : Enemy
    {
        public Mudcrab()
        {
            Name = "Mudcrab";
            HP = 20;
            Attack = 5;
            ID = 1;
        }
    }

    public class Bandit : Enemy
    {
        public Bandit()
        {
            Name = "Bandit";
            HP = 35;
            Attack = 15;
            ID = 2;
        }
    }
}
