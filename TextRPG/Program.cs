﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TextRPG.TextRPG;

namespace TextRPG
{
    internal class Program
    {
        public class Stage
        {
            private Character player;
            private List<Item> items;
            private List<Item> invenItem;
            private List<Dungeon> dungeons;
            private static Random random = new Random();
            public Stage(Character player, List<Item> items, List<Item> invenItem, List<Dungeon> dungeons)
            {
                this.player = player;
                this.items = items;
                this.invenItem = invenItem;
                this.dungeons = dungeons;
            }
            // 글자색 바꾸기
            public void ColorString(string name)
            {
                if (!player.IsDead)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"[{name}]");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"[{name}]");
                    Console.ResetColor();
                }

            }
            // 직업 선택
            public void ChooseJob()
            {
                Console.Clear();
                ColorString("직업 선택");
                Console.Write($"직업을 선택해주세요. 잘못 선택 시 돌아올수 없습니다.\n\n1. {Job.전사}(기본)\n2. {Job.궁수}(공격력+2)\n3. {Job.도적}(공격력+5, 방어력-5)\n\n>>");
                string input = Console.ReadLine();
                if (int.TryParse(input, out int value))
                {
                    switch (value)
                    {
                        case (int)Job.전사:
                            player.Job = "전사";
                            break;
                        case (int)Job.궁수:
                            player.Job = "궁수";
                            player.AttakcDamage += 2;
                            break;
                        case (int)Job.도적:
                            player.Job = "도적";
                            player.AttakcDamage += 5;
                            player.Defense -= 5;
                            break;
                        default:
                            Console.WriteLine("잘못된 입력입니다.");
                            Thread.Sleep(1000);
                            ChooseJob();
                            break;
                    }
                    Console.WriteLine($"{player.Job}을(를) 선택하셨습니다.");
                    Thread.Sleep(1000);
                    ChoiceState();
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(1000);
                    ChooseJob();
                }

            }
            // 기본 메뉴
            public void ChoiceState()
            {
                Console.Clear();
                ColorString("메뉴");
                Console.WriteLine("무슨 행동을 할지 결정합니다.\n\n1. 상태보기\n2. 인벤토리\n3. 상점\n4. 던전 입장\n5. 휴식하기");
                Console.Write("\n원하시는 행동을 입력해주세요.\n>>");
                string input = Console.ReadLine();
                if (input == "1")
                {
                    Console.Clear();
                    State();
                }
                else if (input == "2" && invenItem.Count > 0)
                {
                    Console.Clear();
                    Inventory();
                }

                else if (input == "2")
                {
                    int ran = random.Next(0, 4);
                    if (player.IsDead && ran == 0)
                    {
                        Console.WriteLine("?");
                        Thread.Sleep(1000);
                        invenItem.Add(new Potion());
                        Inventory();
                    }
                    else
                    {
                        Console.WriteLine("현재 인벤토리는 비었습니다.");
                        Thread.Sleep(1000);
                        ChoiceState();
                    }
                }
                else if (input == "3")
                {
                    Console.Clear();
                    Shop();
                }
                else if (input == "4")
                {
                    Console.Clear();
                    Dungeon();
                }
                else if (input == "5")
                {
                    Console.Clear();
                    rest();
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(1000);
                    ChoiceState();
                }
            }
            // 캐릭터 상태 보기
            public void State()
            {
                Console.Clear();
                ColorString("상태 보기");
                Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");

                if (!player.IsDead)
                {
                    if (player.Lv < 10)
                        Console.WriteLine("Lv : 0{0}", player.Lv);
                    else
                        Console.WriteLine("Lv : {0}", player.Lv);
                    Console.WriteLine("{0} ({1})", player.Name, player.Job);
                    Console.WriteLine("경험치 : {0} %", player.Exp * 100 / player.Lv);
                    Console.WriteLine("공격력 : {0}", player.AttakcDamage);
                    Console.WriteLine("방어력 : {0}", player.Defense);
                    Console.WriteLine("체력 : {0}", player.Health);
                    Console.WriteLine("Gold : {0}", player.Gold);
                }
                else
                {
                    Console.WriteLine("Lv : ???");
                    Console.WriteLine("{0} (???)", player.Name);
                    Console.WriteLine("경험치 : ??? %");
                    Console.WriteLine("공격력 : ???");
                    Console.WriteLine("방어력 : ???");
                    Console.WriteLine("체력 : ???");
                    Console.WriteLine("Gold : {0}", player.Gold);
                }


                Console.WriteLine("\n0. 나가기");
                Console.Write("\n원하시는 행동을 입력해주세요.\n>>");
                string input = Console.ReadLine();
                if (input == "0")
                {
                    ChoiceState();
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(1000);
                    State();
                }
            }
            // 인벤토리
            public void Inventory()
            {
                Console.Clear();
                ColorString("인벤토리");
                Console.WriteLine("보유 중인 아이템을 관리 할 수 있습니다.\n\n[아이템 목록]\n");

                if (invenItem != null)
                {
                    for (int i = 0; i < invenItem.Count; i++)
                    {
                        Console.WriteLine($"{invenItem[i].Name}\tl{invenItem[i].Effect}\tl {invenItem[i].Manual}\tl {invenItem[i].Gold} G");
                    }
                }

                Console.Write("1. 장착 관리.\n0. 나가기\n\n원하시는 행동을 입력해주세요.\n>>");
                string input = Console.ReadLine();
                if (input == "1")
                {
                    InventoryManagement();
                }
                else if (input == "0")
                {
                    ChoiceState();
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(1000);
                    Inventory();
                }
            }
            // 인벤토리 장착,장착해제
            public void InventoryManagement()
            {
                Console.Clear();
                ColorString("인벤토리 - 장착 관리");
                Console.WriteLine("보유 중인 아이템을 관리 할 수 있습니다.\n\n[아이템 목록]");
                if (invenItem != null)
                {
                    for (int i = 0; i < invenItem.Count; i++)
                    {
                        if (invenItem[i].Use == false)
                        {
                            Console.WriteLine($"-{i + 1} [ ]{invenItem[i].Name}\tl{invenItem[i].Effect}\tl {invenItem[i].Manual}\tl {invenItem[i].Gold} G");
                        }
                        else
                        {
                            Console.WriteLine($"-{i + 1} [E]{invenItem[i].Name}\tl{invenItem[i].Effect}\tl {invenItem[i].Manual}\tl {invenItem[i].Gold} G");
                        }

                    }
                }
                Console.Write("\n\n장착하시려면 해당 앞의 숫자를 입력해주세요.\n같은 종류의 장비는 장착 불가능합니다.\n\n0. 나가기\n>>");
                string input = Console.ReadLine();
                if (input == "0")
                {
                    Inventory();
                }
                else if (int.TryParse(input, out int iInput) && iInput - 1 < invenItem.Count)
                {
                    UseItemCheck(iInput - 1, invenItem);
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(1000);
                    InventoryManagement();
                }

            }
            // 아이템이 사용/해제 사용중인 무기타입((int)Player.WeaponType)이 있다면 기존 아이템 해제 해 장착
            public void UseItemCheck(int input, List<Item> invenitem)
            {
                if (invenItem[input].WeaponType == 10)
                {
                    invenItem[input].UseItem(player,invenItem);
                    Thread.Sleep(1000);
                    invenItem.Clear();
                    ChoiceState();
                }
                if (invenItem[input].Use == false)
                {
                    foreach (Item item in invenitem)
                    {
                        if (item.Use == true && invenItem[input].WeaponType == item.WeaponType)
                        {
                            item.UnEquipItem(player);
                        }
                    }

                    invenItem[input].EquipItem(player);
                    Thread.Sleep(1000);
                    InventoryManagement();
                }
                else if (invenItem[input].Use == true)
                {
                    invenItem[input].UnEquipItem(player);
                    Thread.Sleep(1000);
                    InventoryManagement();
                }
                else
                {
                    InventoryManagement();
                }
            }
            // 상점
            public void Shop()
            {
                Console.Clear();
                ColorString("상점");
                Console.WriteLine($"필요한 아이템을 얻을 수 있는 상점입니다.\n\n[보유 골드]\n{player.Gold}G\n");
                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i].Buy == false)
                    {
                        Console.WriteLine($"{items[i].Name}\tl{items[i].Effect}\tl {items[i].Manual}\tl {items[i].Gold} G");
                    }
                    else
                    {
                        Console.WriteLine($"{items[i].Name}\tl{items[i].Effect}\tl {items[i].Manual}\tl 구매완료");
                    }
                }
                Console.WriteLine();
                {
                    Console.Write("1. 아이템 구매\n2. 아이템 판매\n0. 나가기\n\n원하시는 행동을 입력해주세요.\n>>");
                }
                string input = Console.ReadLine();
                if (input == "1")
                {
                    BuyShop();
                }
                else if (input == "2" && invenItem.Count == 0)
                {
                    Console.WriteLine("판매하실 수 있는 아이템이 없습니다.");
                    Thread.Sleep(1000);
                    Shop();
                }
                else if (input == "2")
                {
                    SellShop();
                }
                else if (input == "0")
                {
                    ChoiceState();
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(1000);
                    Shop();
                }

            }
            // 상점 - 구매하기
            public void BuyShop()
            {
                Console.Clear();
                ColorString("상점 - 구매");
                Console.WriteLine($"필요한 아이템을 얻을 수 있는 상점입니다.\n\n[보유 골드]\n{player.Gold}G\n");
                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i].Buy == false)
                    {
                        Console.WriteLine($"- {i + 1} {items[i].Name}\tl{items[i].Effect}\tl {items[i].Manual}\tl {items[i].Gold} G");
                    }
                    else
                    {
                        Console.WriteLine($"- {i + 1} {items[i].Name}\tl{items[i].Effect}\tl {items[i].Manual}\tl 구매완료");
                    }
                }
                Console.Write("\n0. 나가기\n\n원하시는 행동을 입력해주세요.\n>>");
                string input = Console.ReadLine();
                if (input == "0")
                {
                    Shop();
                }
                else if (int.TryParse(input, out int iInput) && iInput - 1 < items.Count)
                {
                    BuyShopCheck(iInput - 1);
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(1000);
                    BuyShop();
                }
            }
            // bool값 Buy가 false라면 true로 바꾸고 구매 이미 true라면 BuyShop()으로 돌아가기
            public void BuyShopCheck(int input)
            {
                if (items[input].Buy == false && items[input].Gold <= player.Gold)
                {
                    items[input].Buy = true;
                    invenItem.Add(items[input]);
                    player.Gold -= items[input].Gold;
                    Console.WriteLine($"{items[input].Name} 구매 완료!");
                    Thread.Sleep(1000);
                    BuyShop();
                }
                else if (items[input].Buy == true)
                {
                    Console.WriteLine("이미 구매한 상품입니다.");
                    Thread.Sleep(1000);
                    BuyShop();
                }
                else
                {
                    Console.WriteLine($"Gold가 부족합니다!");
                    Thread.Sleep(1000);
                    BuyShop();
                }
            }
            // 상점 - 판매하기 Buy값이 false인 item만 출력
            public void SellShop()
            {
                Console.Clear();
                ColorString("상점 - 판매");
                Console.WriteLine($"필요한 아이템을 얻을 수 있는 상점입니다.\n\n[보유 골드]\n{player.Gold}G\n");
                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i].Buy != false)
                    {
                        Console.WriteLine($"- {i + 1} {items[i].Name}\tl{items[i].Effect}\tl {items[i].Manual}\tl {items[i].Gold * 0.85f}");
                    }

                }
                Console.Write("\n0. 나가기\n\n원하시는 행동을 입력해주세요.\n>>");
                string input = Console.ReadLine();
                if (input == "0")
                {
                    Shop();
                }
                else if (int.TryParse(input, out int iInput) && iInput - 1 < items.Count)
                {
                    SellShopCheck(iInput - 1);
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(1000);
                    Console.Clear();
                    Shop();
                }
            }
            // bool값 Buy가 true라면 false로 바꾸고 판매
            public void SellShopCheck(int input)
            {
                if (items[input].Buy == true)
                {
                    items[input].UnEquipItem(player);
                    items[input].Buy = false;
                    Console.WriteLine($"{items[input].Name} 판매 완료");
                    invenItem.Remove(items[input]);
                    player.Gold += items[input].Gold * 0.85f;
                    Thread.Sleep(1000);
                    SellShop();
                }
                else
                {
                    BuyShop();
                }
            }
            // 던전
            public void Dungeon()
            {
                Console.Clear();
                ColorString("던전 입장");
                Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
                for (var i = 0; i < dungeons.Count; i++)
                {
                    Console.WriteLine($"[{i + 1}]. {dungeons[i].Name}\t\tl 방어력 {dungeons[i].Defense}이상 권장");
                }
                Console.WriteLine("0. 나가기");
                Console.Write("\n원하시는 행동을 입력해주세요.\n>>");
                string input = Console.ReadLine();

                if (int.TryParse(input, out int iInput) && iInput - 1 >= 0 && iInput - 1 < dungeons.Count)
                {
                    EnterDungeon(iInput - 1);
                    Console.Write("\n아무키나 누르면 메뉴로 돌아갑니다.\n>>");
                    Console.ReadLine();
                    ChoiceState();
                }
                else if (input == "0")
                {
                    ChoiceState();
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(1000);
                    Dungeon();
                }
            }
            // 던전 입장 방어력에 따라 클리어/실패 
            public void EnterDungeon(int lv)
            {
                int ranClear = random.Next(0, 5);
                Console.Clear();
                Scene(lv);
                if (player.Defense < dungeons[lv].Defense)
                {
                    if (ranClear < 2)
                    {
                        player.Health /= 2;
                        Console.Clear();
                        Console.WriteLine($"공략 실패!\n플레이어 체력 : {player.Health}\n\n잠시후 메뉴로 돌아갑니다.");
                        Thread.Sleep(2000);
                        ChoiceState();
                    }
                    else
                    {
                        ClearDungeon(lv);
                    }
                }
                else
                {
                    ClearDungeon(lv);
                }
            }
            // 던전 레벨에 따라 전투 시간 증가
            public void Scene(int lv)
            {
                for (int i = 0; i < (lv + 1) * 3; i++)
                {
                    Console.WriteLine("\n             전투중..\n");
                    Console.Write("               ●\n\n");
                    Thread.Sleep(100);
                    Console.Write("               ●\n\n");
                    Thread.Sleep(100);
                    Console.Write("               ●");
                    Thread.Sleep(100);
                    Console.Clear();
                }
            }
            // 던전 입장시 행동
            public void ClearDungeon(int lv)
            {
                Console.Clear();

                int health = player.Health;
                float gold = player.Gold;
                int minusHealth = random.Next(20 - (player.Defense - dungeons[lv].Defense), 36 - (player.Defense - dungeons[lv].Defense));
                player.Health -= minusHealth;
                if (player.IsDead)
                {
                    {
                        Console.Clear();
                        invenItem.Clear();
                        player.Gold = 0;
                        ColorString("사망");
                    }
                }
                else
                {
                    Console.WriteLine($"축하합니다!!\n{dungeons[lv].Name}을 클리어 하였습니다!");
                    player.Gold += dungeons[lv].Rewards;
                    float bounsRewards = (float)(random.NextDouble() * player.AttakcDamage + player.AttakcDamage);
                    player.Gold += (float)Math.Round(bounsRewards);
                    player.Exp++;
                    LvUP();
                    Console.WriteLine($"[탐험결과]\n\n체력 {health} -> {player.Health}\nGold {gold} G -> {player.Gold} G [ {player.Gold - gold}G 증가 ]");
                }
            }
            // 던전 클리어시 경험치 증가 일정 도달시 레벨업
            public void LvUP()
            {
                int lv = player.Lv;
                int exp = player.Exp;
                float attak = player.AttakcDamage;
                int deffense = player.Defense;
                if (lv == exp)
                {
                    player.Exp = 0;
                    player.AttakcDamage += 0.5f;
                    player.Defense += 1;
                    player.Lv++;
                    player.Health = 100;
                    Console.WriteLine("★★★★★★");
                    Console.WriteLine($"★ Lv Up! ★   LV : {lv} => LV : {player.Lv}");
                    Console.WriteLine($"★★★★★★   {player.Name}의 공격력 : {attak} => {player.AttakcDamage}, 방어력 : {deffense} => {player.Defense}");
                }
            }
            // 최대체력, 현재 골드의 여부를 따져 만족한다면 체력 회복
            public void rest()
            {
                Console.Clear();
                ColorString("휴식하기");
                Console.WriteLine($"500G를 내면 체력을 회복할 수 있습니다. (보유 골드 : {player.Gold} G)\n\n1. 휴식하기\n0. 나가기\n\n원하시는 행동을 입력해주세요.\n>>");
                int health = player.Health;
                float gold = player.Gold;
                string input = Console.ReadLine();
                if (input == "1")
                {
                    if (player.Health >= 100)
                    {
                        Console.WriteLine("최대 체력입니다.");
                    }
                    else if (player.Gold >= 500)
                    {
                        player.Health = 100;
                        int plusHealth = player.Health - health;
                        player.Gold -= 500;
                        Console.WriteLine($"휴식을 완료했습니다.\n이전 Gold: {gold}, 현재 Gold : {player.Gold}\n이전 체력 : {health}, 증가한 체력 : {plusHealth}(100)");

                    }
                    else
                    {
                        Console.WriteLine("Gold가 부족합니다.");
                    }
                }
                else if (input == "0")
                {
                    ChoiceState();
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(1000);
                    Console.Clear();
                    rest();
                }
                Thread.Sleep(2000);
                ChoiceState();
            }
            public void Start()
            {
                Console.Write("스파르타 마을에 오신 여러분 환영합니다.\n이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n\n원하시는 이름을 설정해주세요.\n>>");
                string input = Console.ReadLine();
                Console.WriteLine($"\n{input}이 맞습니까?\n1. 예 \n2. 아니오");
                string input2 = Console.ReadLine();
                if (input2 == "1")
                {
                    player.Name = input;
                    Console.Clear();
                    Console.WriteLine($"{player.Name}님 환영합니다.");
                    Thread.Sleep(1000);
                }
                else if (input2 == "2")
                {
                    Console.WriteLine("다시 정해주세요.");
                    Thread.Sleep(1000);
                    Console.Clear();
                    Start();
                }
                else
                {
                    Console.WriteLine("잘못 입력하셨습니다.");
                    Thread.Sleep(1000);
                    Console.Clear();
                    Start();
                }
                ChooseJob();
            }
        }

        static void Main(string[] args)
        {
            Player player = new Player();
            List<Item> invenItem = new List<Item>();
            List<Item> items = new List<Item> { new BasicArmor(), new IronArmor(), new SpartaArmor(), new Sword(), new Ax(), new Lance(), new Ring(), new Earing() };
            List<Dungeon> dungeons = new List<Dungeon> { new EazyD(), new NormalD(), new HardD() };
            Stage stage = new Stage(player, items, invenItem, dungeons);
            stage.Start();

        }
    }
}
