using System.Numerics;
using Newtonsoft.Json;

namespace TextRPG
{
    internal class Program
    {
        static int itemCount = 8;
        static bool isGameOver = false;

        static Item[] items = new Item[itemCount];

        public struct PlayerStats
        {
            public int level;
            public string name;
            public string job;
            public float baseAttack;
            public int baseDefence;
            public float attack;
            public int defense;
            public int health;
            public int gold;

            public PlayerStats()
            {
                this.level = 1;
                this.name = "Chad";
                this.job = "전사";
                this.baseAttack = 10;
                this.baseDefence = 5;
                this.attack = baseAttack;
                this.defense = baseDefence;
                this.health = 100;
                this.gold = 1500;
            }
        }

        static PlayerStats playerStats = new PlayerStats();

        public struct Item
        {
            public string name;
            public ItemType type;
            public int status;
            public string description;
            public bool isPurchased;
            public bool isEquipped;
            public int price;

            public Item()
            {
                this.name = "무쇠갑옷";
                this.type = ItemType.Armor;
                this.status = 5;
                this.description = "무쇠로 만들어져 튼튼한 갑옷입니다.";
                this.isPurchased = false;
                this.isEquipped = false;
                this.price = 100;
            }
        }

        public enum ItemType { Weapon, Armor }

        static void ItemList()
        {
            items[0].name = "거적데기";
            items[0].type = ItemType.Armor;
            items[0].status = 2;
            items[0].description = "얕은 상처 정도는 막아줄 수 있을법한 옷입니다.";
            items[0].isPurchased = false;
            items[0].isEquipped = false;
            items[0].price = 500;

            items[1].name = "수련자 갑옷";
            items[1].type = ItemType.Armor;
            items[1].status = 5;
            items[1].description = "수련에 도움을 주는 갑옷입니다.";
            items[1].isPurchased = false;
            items[1].isEquipped = false;
            items[1].price = 1000;

            items[2].name = "무쇠갑옷";
            items[2].type = ItemType.Armor;
            items[2].status = 9;
            items[2].description = "무쇠로 만들어져 튼튼한 갑옷입니다.";
            items[2].isPurchased = false;
            items[2].isEquipped = false;
            items[2].price = 2000;

            items[3].name = "스파르타의 갑옷";
            items[3].type = ItemType.Armor;
            items[3].status = 15;
            items[3].description = "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.";
            items[3].isPurchased = false;
            items[3].isEquipped = false;
            items[3].price = 3500;

            items[4].name = "낡은 검";
            items[4].type = ItemType.Weapon;
            items[4].status = 2;
            items[4].description = "쉽게 볼 수 있는 낡은 검입니다.";
            items[4].isPurchased = false;
            items[4].isEquipped = false;
            items[4].price = 600;

            items[5].name = "청동 도끼";
            items[5].type = ItemType.Weapon;
            items[5].status = 5;
            items[5].description = "어디선가 사용됐던 것 같은 도끼입니다.";
            items[5].isPurchased = false;
            items[5].isEquipped = false;
            items[5].price = 1500;

            items[6].name = "무쇠 쌍검";
            items[6].type = ItemType.Weapon;
            items[6].status = 8;
            items[6].description = "무쇠로 만들어져 더 날카로워진 쌍검입니다.";
            items[6].isPurchased = false;
            items[6].isEquipped = false;
            items[6].price = 2000;

            items[7].name = "스파르타의 창";
            items[7].type = ItemType.Weapon;
            items[7].status = 12;
            items[7].description = "스파르타의 전사들이 사용했다는 전설의 창입니다.";
            items[7].isPurchased = false;
            items[7].isEquipped = false;
            items[7].price = 2500;
        }

        static void Main(string[] args)
        {
            Load(ref playerStats, ref items);

            while (!isGameOver)
            {
                MainMenu();
            }

            Save(playerStats, items);
        }

        static int Input()
        {
            Console.WriteLine("\n원하시는 행동을 입력해주세요.");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(">> ");
            Console.ForegroundColor = ConsoleColor.White;
            string s = Console.ReadLine();

            if (!int.TryParse(s, out int i))
            {
                return -1;
            }
            else
            {
                return i;
            }
        }

        static void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");

            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.WriteLine("4. 던전입장");
            Console.WriteLine("5. 휴식하기");
            Console.WriteLine("0. 나가기");

            int input = Input();
            while (input < 0 && input > 5)
            {
                Console.WriteLine("잘못된 입력입니다.");
                input = Input();
            }

            switch (input)
            {
                case 1:
                    Status();
                    break;
                case 2:
                    Inventory();
                    break;
                case 3:
                    Shop();
                    break;
                case 4:
                    Dungeon();
                    break;
                case 5:
                    Rest();
                    break;
                case 0:
                    isGameOver = true;
                    return;
            }

        }

        static void Status()
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].isEquipped)
                {
                    if (items[i].type == ItemType.Weapon)
                    {
                        playerStats.attack = playerStats.baseAttack + items[i].status;
                    }
                    else
                    {
                        playerStats.defense = playerStats.baseDefence + items[i].status;
                    }
                }
            }

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("상태 보기");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");

            Console.WriteLine($"Lv. {playerStats.level}");
            Console.WriteLine($"{playerStats.name} ( {playerStats.job} )");
            Console.Write($"공격력 : {playerStats.attack} ");
            if (playerStats.attack > 10)
            {
                Console.Write($"(+{playerStats.attack - 10})");
            }
            Console.WriteLine();
            Console.Write($"방어력 : {playerStats.defense} ");
            if (playerStats.defense > 5)
            {
                Console.Write($"(+{playerStats.defense - 5})");
            }
            Console.WriteLine();
            Console.WriteLine($"체 력 : {playerStats.health}");
            Console.WriteLine($"Gold : {playerStats.gold} G\n");

            Console.WriteLine("0. 나가기");

            int input = Input();
            while (input != 0)
            {
                Console.WriteLine("잘못된 입력입니다.");
                input = Input();
            }

            switch (input)
            {
                case 0:
                    return;
            }
        }


        static void Inventory()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("인벤토리");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

            Console.WriteLine("[아이템 목록]\n");

            int i = 1;

            foreach (Item item in items)
            {
                if (item.isPurchased)
                {
                    if (item.isEquipped)
                    {
                        Console.Write("[E]");
                    }
                    Console.Write(item.name + "\t| ");
                    if (item.type == ItemType.Weapon)
                    {
                        Console.Write("공격력");
                    }
                    else
                    {
                        Console.Write("방어력");
                    }
                    Console.Write(" + " + item.status);
                    Console.WriteLine("\t| " + item.description);


                    i++;
                }
            }
            Console.WriteLine();

            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("0. 나가기");

            int input = Input();
            while (input != 1 && input != 0)
            {
                Console.WriteLine("잘못된 입력입니다.");
                input = Input();
            }

            switch (input)
            {
                case 1:
                    InventoryManagement();
                    break;
                case 0:
                    return;
            }
        }

        static void InventoryManagement()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("인벤토리 - 장착 관리");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

            Console.WriteLine("[아이템 목록]\n");

            int i = 1;

            List<Item> list = new List<Item>();
            list.Clear();
            Item temp = new Item();
            list.Add(temp);

            foreach (Item item in items)
            {
                if (item.isPurchased)
                {
                    list.Add(item);
                    Console.Write(i + ". ");
                    if (item.isEquipped)
                    {
                        Console.Write("[E]");
                    }
                    Console.Write(item.name + "\t\t| ");
                    if (item.type == ItemType.Weapon)
                    {
                        Console.Write("공격력");
                    }
                    else
                    {
                        Console.Write("방어력");
                    }
                    Console.Write(" + " + item.status);
                    Console.WriteLine("\t| " + item.description);

                    i++;
                }
            }
            Console.WriteLine();

            Console.WriteLine("0. 나가기");

            int input = Input();

            while (input < 0 || input >= i)
            {
                Console.WriteLine("잘못된 입력입니다.");
                input = Input();
            }

            if (input == 0)
            {
                return;
            }
            else
            {
                for (int k = 0; k < items.Length; k++)
                {
                    if (list[input].name == items[k].name)
                    { 
                        items[k].isEquipped = !items[k].isEquipped; 
                    }
                    else if (list[input].type == items[k].type)
                    {
                        items[k].isEquipped = false;
                    }

                }
            }

            InventoryManagement();
        }

        static void Shop()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("상점");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{playerStats.gold} G\n");

            foreach (Item item in items)
            {
                Console.Write(item.name + "\t| ");
                if (item.type == ItemType.Weapon)
                {
                    Console.Write("공격력");
                }
                else
                {
                    Console.Write("방어력");
                }
                Console.Write(" + " + item.status);
                Console.Write("\t| " + item.description);
                if (!item.isPurchased)
                {
                    Console.WriteLine("\t| " + item.price + " G");
                }
                else
                {
                    Console.WriteLine("\t| 구매 완료");
                }

            }
            Console.WriteLine();

            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("2. 아이템 판매");
            Console.WriteLine("0. 나가기");

            int input = Input();
            while (input < 0 && input > 2)
            {
                Console.WriteLine("잘못된 입력입니다.");
                input = Input();
            }

            switch (input)
            {
                case 1:
                    Purchase();
                    break;
                case 2:
                    Sell();
                    break;
                case 0:
                    return;
            }
        }

        static void Purchase()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("상점 - 아이템 구매");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{playerStats.gold} G\n");

            int i = 1;

            List<Item> list = new List<Item>();
            list.Clear();
            Item temp = new Item();
            list.Add(temp);

            foreach (Item item in items)
            {
                list.Add(item);
                Console.Write(i + ". ");
                Console.Write(item.name + "\t\t| ");
                if (item.type == ItemType.Weapon)
                {
                    Console.Write("공격력");
                }
                else
                {
                    Console.Write("방어력");
                }
                Console.Write(" + " + item.status);
                Console.Write("\t| " + item.description);
                if (!item.isPurchased)
                {
                    Console.WriteLine("\t| " + item.price + " G");
                }
                else
                {
                    Console.WriteLine("\t| 구매 완료");
                }

                i++;
            }
            Console.WriteLine();

            Console.WriteLine("0. 나가기");

            int input = Input();

            while (input < 0 || input >= items.Length)
            {
                Console.WriteLine("잘못된 입력입니다.");
                input = Input();
            }

            if (input == 0)
            {
                return;
            }
            else
            {
                for (int k = 0; k < items.Length; k++)
                {
                    if (list[input].name == items[k].name)
                    {
                        if (items[k].isPurchased)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("이미 구매한 아이템입니다.");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else if (playerStats.gold >= items[k].price)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("구매를 완료했습니다.");
                            Console.ForegroundColor = ConsoleColor.White;
                            items[k].isPurchased = true;
                            playerStats.gold -= items[k].price;
                        }
                        else if (playerStats.gold < items[k].price)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("골드가 부족합니다.");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                    }
                }
            }
            Thread.Sleep(3000);

            Purchase();
        }

        public static void Sell()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("상점 - 아이템 구매");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{playerStats.gold} G\n");

            int i = 1;

            List<Item> list = new List<Item>();
            list.Clear();
            Item temp = new Item();
            list.Add(temp);

            foreach (Item item in items)
            {
                if (item.isPurchased)
                {
                    list.Add(item);
                    Console.Write(i + ". ");
                    Console.Write(item.name + "\t\t| ");
                    if (item.type == ItemType.Weapon)
                    {
                        Console.Write("공격력");
                    }
                    else
                    {
                        Console.Write("방어력");
                    }
                    Console.Write(" + " + item.status);
                    Console.Write("\t| " + item.description);
                    Console.WriteLine("\t| " + item.price * 0.85 + " G");

                    i++;
                }
            }
            Console.WriteLine();

            Console.WriteLine("0. 나가기");

            int input = Input();

            while (input < 0 || input >= items.Length)
            {
                Console.WriteLine("잘못된 입력입니다.");
                input = Input();
            }

            if (input == 0)
            {
                return;
            }
            else
            {
                for (int k = 0; k < items.Length; k++)
                {
                    if (list[input].name == items[k].name)
                    {
                        items[k].isPurchased = false;
                        playerStats.gold += (int)(items[k].price * 0.85);
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("판매를 완료했습니다.");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
            }
            Thread.Sleep(3000);

            Sell();

        }

        public static void Dungeon()
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].isEquipped)
                {
                    if (items[i].type == ItemType.Weapon)
                    {
                        playerStats.attack = playerStats.baseAttack + items[i].status;
                    }
                    else
                    {
                        playerStats.defense = playerStats.baseDefence + items[i].status;
                    }
                }
            }

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("던전입장");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");

            Console.WriteLine("1. 쉬운 던전\t | 방어력 5 이상 권장");
            Console.WriteLine("2. 일반 던전\t | 방어력 11 이상 권장");
            Console.WriteLine("3. 어려운 던전\t | 방어력 17 이상 권장");
            Console.WriteLine("0. 나가기");

            int input = Input();
            while (input < 0 || input > 3)
            {
                Console.WriteLine("잘못된 입력입니다.");
                input = Input();
            }

            if (input == 0)
            {
                return;
            }
            else
            {
                EnterDungeon(input);
            }

            Thread.Sleep(3000);
        }

        public static void EnterDungeon(int difficulty)
        {
            int recommendedDeffence = 0;
            int reward = 0;
            switch (difficulty)
            {
                case 1:
                    recommendedDeffence = 5;
                    reward = 1000;
                    break;
                case 2:
                    recommendedDeffence = 11;
                    reward = 1700;
                    break;
                case 3:
                    recommendedDeffence = 17;
                    reward = 2500;
                    break;
            }

            if (playerStats.defense < recommendedDeffence)
            {
                Random random = new Random();
                if (random.Next(0, 10) < 4)
                {
                    Console.WriteLine("던전 실패...");
                    playerStats.health /= 2;
                }
                else
                {
                    DungeonClear(recommendedDeffence, reward, difficulty);
                }
            } else
            {
                DungeonClear(recommendedDeffence, reward, difficulty);
            }
        }

        public static void DungeonClear(int recommendedDeffence, int reward, int difficulty)
        {
            Random random = new Random();
            int tempHealth = playerStats.health;
            int tempGold = playerStats.gold;

            playerStats.level += 1;
            playerStats.baseAttack += 0.5f;
            playerStats.baseDefence += 1;

            playerStats.health = playerStats.health - random.Next(20, 36) + (playerStats.defense - recommendedDeffence);
            if (playerStats.health > 100)
                playerStats.health = 100;
            else if (playerStats.health <= 0) 
            { 
                Console.WriteLine("사망했습니다");
                Thread.Sleep(3000);
                return;
            }

            playerStats.gold += (int)(reward + (reward * random.Next(1, 2) * playerStats.attack / 100f));

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("던전 클리어!");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("축하합니다!!");
            switch (difficulty)
            {
                case 1:
                    Console.Write("쉬운 던전");
                    break;
                case 2:
                    Console.Write("일반 던전");
                    break;
                case 3:
                    Console.Write("어려운 던전");
                    break;
            }
            Console.WriteLine("을 클리어 하였습니다.\n");

            Console.WriteLine("[탐험 결과]");
            Console.WriteLine($"체력 {tempHealth} -> {playerStats.health}");
            Console.WriteLine($"Gold {tempGold} G -> {playerStats.gold} G");


            Console.WriteLine("0. 나가기");

            int input = Input();
            while (input < 0 || input > 0)
            {
                Console.WriteLine("잘못된 입력입니다.");
                input = Input();
            }

            if (input == 0)
            {
                return;
            }
        }

        public static void Rest()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("휴식하기");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("500 G를 내면 체력을 회복할 수 있습니다. ");
            Console.WriteLine($"(보유 골드 : {playerStats.gold} G)\n");

            

            Console.WriteLine("1. 휴식하기");
            Console.WriteLine("0. 나가기");

            int input = Input();
            while (input != 1 && input != 0)
            {
                Console.WriteLine("잘못된 입력입니다.");
                input = Input();
            }

            switch (input)
            {
                case 1:
                    if(playerStats.gold >= 500)
                    {
                        playerStats.gold -= 500;
                        playerStats.health = 100;
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("휴식을 완료했습니다.");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Gold가 부족합니다.");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    break;
                case 0:
                    return;
            }

            Thread.Sleep(3000);
        }

        public static void Save(PlayerStats data, Item[] item)
        {
            string fileName = "playerStats.json";
            string itemFileName = "itemData.json";
            string jsonData = JsonConvert.SerializeObject(data);
            string jsonItem = JsonConvert.SerializeObject(item);
            File.WriteAllText(fileName, jsonData);
            File.WriteAllText(itemFileName, jsonItem);
        }

        public static void Load(ref PlayerStats data, ref Item[] item)
        {
            if (File.Exists("./playerStats.json"))
            {
                string json = File.ReadAllText("./playerStats.json");
                playerStats = JsonConvert.DeserializeObject<PlayerStats>(json);
            }
            else
            {
                playerStats = new PlayerStats();
            }

            if (File.Exists("./itemData.json"))
            {
                string json2 = File.ReadAllText("./itemData.json");
                items = JsonConvert.DeserializeObject<Item[]>(json2); 
            }
            else
            {
                ItemList();
            }
        }
    }
}