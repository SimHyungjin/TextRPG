using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    namespace TextRPG
    {
        interface ICharacter
        {
            int Lv { get; set; }
            int Exp {  get; set; }
            string Name { get; set; }
            string Job { get; set; }
            float AttakcDamage { get; set; }
            int Deffense { get; set; }
            int Health { get; set; }
            float Gold { get; set; }
        }
        public class Player : ICharacter
        {
            public int Lv { get; set; } = 1;
            public int Exp { get; set; } = 0;
            public string Name {  get; set; }
            public string Job { get; set; } = "전사";
            public float AttakcDamage { get; set; } = 10;
            public int Deffense { get; set; } = 5;
            public int Health { get; set; } = 100;
            public float Gold { get; set; } = 15000;
        }
        interface IItem
        {
            bool Use { get; set; }
            bool Buy { get; set; }
            public int WeaponType { get; }
            string Name { get; }
            string Effect { get; }
            float AttakcDamage { get; }
            int Deffense { get; }
            string Manual { get; }
            float Gold { get;}
        }
        public class BasicArmor : IItem
        {
            public bool Use { get; set; } = false;
            public bool Buy { get; set; } = false;
            public int WeaponType => 1;
            public string Name => "수련자 갑옷";
            public string Effect => "방어력 +5";
            public float AttakcDamage => 0;
            public int Deffense => 5;
            public string Manual => "수련에 도움을 주는 갑옷입니다.\t\t";
            public float Gold => 1000;
        }
        public class IronArmor : IItem
        {
            public bool Use { get; set; } = false;
            public bool Buy { get; set; } = false;
            public int WeaponType => 1;
            public string Name => "무쇠 갑옷";
            public string Effect => "방어력 +9";
            public float AttakcDamage => 0;
            public int Deffense => 9;
            public string Manual => "무쇠로 만들어져 튼튼한 갑옷입니다.\t\t";
            public float Gold => 2000;
        }
        public class SpartaArmor : IItem
        {
            public bool Use { get; set; } = false;
            public bool Buy { get; set; } = false;
            public int WeaponType => 1;
            public string Name => "스파르타의 갑옷";
            public string Effect => "방어력 +15";
            public float AttakcDamage => 0;
            public int Deffense =>15;
            public string Manual => "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.";
            public float Gold => 3500;
        }
        public class Sword : IItem
        {
            public bool Use { get; set; } = false;
            public bool Buy { get; set; } = false;
            public int WeaponType => 2;
            public string Name => "낡은 검  ";
            public string Effect => "공격력 +2";
            public float AttakcDamage => 2;
            public int Deffense => 0;
            public string Manual => "쉽게 볼 수 있는 낡은 검입니다..\t\t";
            public float Gold => 600;
        }
        public class Ax : IItem
        {
            public bool Use { get; set; } = false;
            public bool Buy { get; set; } = false;
            public int WeaponType => 2;
            public string Name => "청동 도끼";
            public string Effect => "공격력 +5";
            public float AttakcDamage => 5;
            public int Deffense => 0;
            public string Manual => "어디선가 사용됐던거 같은 도끼입니다.\t\t";
            public float Gold => 1500;
        }
        public class Lance : IItem
        {
            public bool Use { get; set; } = false;
            public bool Buy { get; set; } = false;
            public int WeaponType => 2;
            public string Name => "스파르타의 창";
            public string Effect => "공격력 +7";
            public float AttakcDamage => 7;
            public int Deffense => 0;
            public string Manual => "스파르타의 전사들이 사용했다는 전설의 창입니다.";
            public float Gold => 3000;
        }
        public class Ring : IItem
        {
            public bool Use { get; set; } = false;
            public bool Buy { get; set; } = false;
            public int WeaponType => 3;
            public string Name => "동반지";
            public string Effect => "공격력 +5, 방어력+5";
            public float AttakcDamage => 5;
            public int Deffense => 5;
            public string Manual => "싸구려 장신구입니다.\t";
            public float Gold => 3000;
        }
        public class Earing : IItem
        {
            public bool Use { get; set; } = false;
            public bool Buy { get; set; } = false;
            public int WeaponType => 3;
            public string Name => "은 귀고리";
            public string Effect => "공격력 +10, 방어력+10";
            public float AttakcDamage => 10;
            public int Deffense => 10;
            public string Manual => "나름 비싼 장신구입니다.";
            public float Gold => 5000;
        }

        interface IDungeon
        {
            string Name { get; }
            int Deffense {  get; }
            float Rewards {  get; }
            string Manual {  get; }
        }
        public class EazyD : IDungeon
        {
            public string Name => "쉬운 던전";
            public int Deffense => 5;
            public float Rewards => 1000;
            public string Manual => $"방어력 {Deffense}이상 권장";
        }
        public class NormalD : IDungeon
        {
            public string Name => "일반 던전";
            public int Deffense => 11;
            public float Rewards => 1700;
            public string Manual => $"방어력 {Deffense}이상 권장";
        }
        public class HardD : IDungeon
        {
            public string Name => "어려운 던전";
            public int Deffense => 17;
            public float Rewards => 2500;
            public string Manual => $"방어력 {Deffense}이상 권장";
        }
    }
    enum Job {전사=1,궁수,도적}
}
