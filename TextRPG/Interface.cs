using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    namespace TextRPG
    {
        public class Character
        {
            public int Lv { get; set; } = 1;
            public int Exp { get; set; } = 0;
            public string Name { get; set; } = string.Empty;
            public string Job { get; set; } = "전사";
            public float AttakcDamage { get; set; } = 10;
            public int Defense { get; set; } = 5;
            public int Health { get; set; } = 100;
            public float Gold { get; set; } = 15000;
            public bool IsDead => Health <= 0;
        }
        public class Player : Character
        {

        }
        public class Item
        {
            public bool Use { get; set; } = false;
            public bool Buy { get; set; } = false;
            public int WeaponType { get; protected set; }
            public string Name { get; protected set; }
            public string Effect { get; protected set; }
            public float AttackDamage { get; protected set; }
            public int Defense { get; protected set; }
            public string Manual { get; protected set; }
            public float Gold { get; protected set; }

            public Item(int WeaponType, string Name, string Effect, float AttackDamage, int Defense, string Manual, float Gold)
            {
                this.WeaponType = WeaponType;
                this.Name = Name;
                this.Effect = Effect;
                this.AttackDamage = AttackDamage;
                this.Defense = Defense;
                this.Manual = Manual;
                this.Gold = Gold;
            }
            public virtual void UseItem(Character player) { }
            public void EquipItem(Character player)
            {
                if(!Use)
                {
                    player.AttakcDamage += this.AttackDamage;
                    player.Defense += this.Defense;
                    this.Use = true;
                    Console.WriteLine($"{this.Name} 장착 완료!");
                }
            }
            public void UnEquipItem(Character player)
            {
                if (Use)
                {
                    player.AttakcDamage -= this.AttackDamage;
                    player.Defense -= this.Defense;
                    this.Use = false;
                    Console.WriteLine($"{this.Name} 해제 완료!");
                }
            }
        }
        public class BasicArmor : Item
        {
            public BasicArmor() : base(1, "수련자의 갑옷", "방어력 +5", 0, 5, "수련에 도움을 주는 갑옷입니다.\t\t", 1000) { }
        }
        public class IronArmor : Item
        {
            public IronArmor() : base(1, "무쇠 갑옷", "방어력 +9", 0, 9, "무쇠로 만들어져 튼튼한 갑옷입니다.\t\t", 2000) { }

        }
        public class SpartaArmor : Item
        {
            public SpartaArmor() : base(1, "스파르타의 갑옷", "방어력 +15", 0, 9, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500) { }
        }

        public class Sword : Item
        {
            public Sword() : base(2, "낡은 검  ", "공격력 +2", 2, 0, "쉽게 볼 수 있는 낡은 검입니다..\t\t", 600) { }

        }
        public class Ax : Item
        {
            public Ax() : base(2, "청동 도끼", "공격력 +5", 5, 0, "어디선가 사용됐던거 같은 도끼입니다.\t\t", 1500) { }

        }
        public class Lance : Item
        {
            public Lance() : base(2, "스파르타의 창", "공격력 +7", 7, 0, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 3000) { }

        }

        public class Ring : Item
        {
            public Ring() : base(3, "동반지", "공격력 +5, 방어력+5", 5, 5, "싸구려 장신구입니다.\t", 3000) { }

        }
        public class Earing : Item
        {
            public Earing() : base(3, "은 귀고리", "공격력 +10, 방어력+10", 10, 10, "나름 비싼 장신구입니다.", 5000) { }
        }

        public class Potion : Item
        {
            public Potion() : base(10, "부활초", "부활합니다.", 0, 0, "부활합니다.", 0) { }

            public override void UseItem(Character player)
            {
                player.Name = "Zombi";
                player.Health = 100;
            }
        }

        public class Dungeon
        {
            public string Name { get; }
            public int Defense { get; }
            public float Rewards { get; }
            public string Manual { get; }

            // 생성자
            public Dungeon(string name, int defense, float rewards, string manual)
            {
                this.Name = name;
                this.Defense = defense;
                this.Rewards = rewards;
                this.Manual = manual;
            }
        }

        public class EazyD : Dungeon
        {
            public EazyD() : base("쉬운 던전", 5, 1000, "방어력 5이상 권장.") { }
        }
        public class NormalD : Dungeon
        {
            public NormalD() : base("일반 던전", 11, 1700, "방어력 11이상 권장.") { }
        }
        public class HardD : Dungeon
        {
            public HardD() : base("어려운 던전", 17, 2500, "방어력 17이상 권장.") { }
        }
    }
    enum Job { 전사 = 1, 궁수, 도적 }
}
