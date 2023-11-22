using System.Data;
using System.Diagnostics;
using System.Numerics;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;
using System.IO;
using System.Xml.Linq;
using System;
using System.Text;
using System.Drawing;
using ConsoleTables;
using Newtonsoft.Json.Linq;

internal class Program
{
    #region 셋팅
    private static List<Weapon> weapons = new List<Weapon> //이름, 가격, 설명, 힘, 민첩, 지능, 운, 효과
        {
            new Weapon("야전삽", 1000, "군대 보급 삽", 10, 5, 3, 2,"공격+10 민첩+5 지능+3 운+2 상승"),
            new Weapon("K2", 1500, "국산 소총", 20, 10, 5, 3,"공격+20 민첩+7 지능+5 운+3 상승"),
            new Weapon("AK47", 2000, "돌격소총", 25, 15, 5, 4, "공격+25 민첩+9 지능+5 운+4 상승"),
            new Weapon("샷건", 2500, "원거리 전투용 산탄총", 30, 11, 6, 5,"공격+30 민첩+11 지능+6 운+5 상승"),
            new Weapon("M60", 4000, "무거운 기관총", 35, 13, 7, 6, "공격+35 민첩+13 지능+7 운+6 상승"),
            new Weapon("AWP", 5000, "저격소총", 40, 15, 8, 7,"공격+40 민첩+15 지능+8 운+7 상승"),
            new Weapon("판처파우스트", 6000, "바주카", 45, 17, 9, 8,"공격+45 민첩+17 지능+9 운+8 상승"),
            new Weapon("발칸", 7000, "롤링 발칸", 50, 19, 10, 9,"공격+50 민첩+19 지능+10 운+9 상승"),
            new Weapon("K-9자주포", 8000, "대형 포탄 발사기", 55, 21, 11, 10,"공격+70 민첩+21 지능+11 운+10 상승"),
            new Weapon("극초음속순항미사일", 10000, "최첨단 미사일", 100, 25, 15, 15,"공격+100 민첩+25 지능+15 운+15 상승"),
            new Weapon("마음의편지", 99999, "최강의 무기", 999, 999, 999, 999,"공격+999 민첩+999 지능+999 운+999 상승")
        };

   
    private static List<Armor> armors = new List<Armor> //이름, 가격, 설명, 정신력, 체력, 효과
        {
            new Armor("생활복", 500, "군대 생활복", 5, 10,"체력+10 정신력+5 상승"),
            new Armor("로카티", 1000, "강화된 로카복", 7, 20,"체력+20 정신력+7 상승"),
            new Armor("화생방 보호의", 1500, "생화학적 위협으로부터 보호하는 의복", 9, 30,"체력+30 정신력+9 상승"),
            new Armor("깔깔이", 2000, "최첨단 솜으로 만든 방어복", 11, 40,"체력+40 정신력+11 상승"),
            new Armor("신형 전투복", 2500, "디지털 전투복", 13, 50,"체력+50 정신력+13 상승"),
            new Armor("개구리 전투복", 5000, "개구리 가죽으로 만든 전투복", 15, 100,"체력+100 정신력+15 상승"),
            new Armor("특전사 이준호 전투복", 99999, "특전사 이준호님의 전투복", 999, 999,"체력+999 정신력+999 상승")
        };

   

    private static List<Food> foods = new List<Food>      //HP회복, 가격
        {
             new Food("건빵", 10, 20, "건푸레이크 주레시피","HP 10만큼 회복합니다."),
             new Food("전투식량", 15, 30, "전투시 빠르게 먹을수있는 식량","HP 15만큼 회복합니다."),
             new Food("감자", 20, 35, "체력 회복을 위한 탄수화물 보충","HP 20만큼 회복합니다."),
             new Food("단백질 바", 30, 40, "체력을 위한 단백질 섭취","HP 30만큼 회복합니다."),                                  
             new Food("야간식량", 40, 50, "야간에 몰래먹는 음식","HP 40만큼 회복합니다."),
             new Food("특급 식사", 50, 60, "전투에 최적화된 특별한 식사","HP 50만큼 회복합니다."),
             new Food("분대회식", 100, 100, "PX 파티","HP 100만큼 회복합니다.")
        };
    //몬스터 리스트
    private static List<Enemy> enemys = new List<Enemy>
        {
            new Enemy("초임 소위", 100, 100),
            new Enemy("참호", 5, 100),
            new Enemy("맞선임", 4, 10),
            new Enemy("멧돼지", 3, 30),
            new Enemy("고라니", 2, 30),
        new Enemy("행보관", 30, 500),
        new Enemy("건장한 남성", 20, 100)
        };

    private static Enemy FindEnemyByName(string enemyName)
    {
        return enemys.Find(m => m.EnemyName == enemyName);
    }

    private static Enemy wildBoar = FindEnemyByName("멧돼지");
    private static Enemy waterDeer = FindEnemyByName("고라니");
    private static Enemy newCommander = FindEnemyByName("초임 소위");
    private static Enemy french = FindEnemyByName("참호");
    private static Enemy senior = FindEnemyByName("맞선임");
    private static Enemy masterSergent = FindEnemyByName("행보관");
    private static Enemy muscleguy = FindEnemyByName("건장한 남성");


    //아이템들 선언

    private static Armor greenStrap;
    private static Armor ShoulderSleeve1;
    private static Armor ShoulderSleeve2;
    private static Armor ShoulderSleeve3;
    private static Armor ShoulderSleeve4;


    //캐릭터 선언
    private static Character player1;

    private static int howhard;
    static int workCount = 0;
    static int Perfection = 0;
    static double Rate = 0;
    static double Coins = 0;
    static double Mileages = 500;
    static int GrahpCount = 0;
    static int turn = 0;
    // ConsoleKeyInfo 선언
    static ConsoleKeyInfo e;

    static bool frenchSuccess;
    #endregion
    //시작
    static void Main(string[] args)
    {

        //게임이 실행되면 데이터 먼저 세팅
        GameDataSetting();
        //시작화면으로 이동
        StartScene();
    }

    //게임 데이터 준비
    static void GameDataSetting()
    {
        //콘솔창 이름 변경
        Console.Title = "K-Army";

        // 캐릭터 정보 세팅
        player1 = new Character("", "용사", 5, 5, 5, 5, 100, 100, 0, 5);

        // 녹견 세팅
        greenStrap = new Armor("분대장 견장", 0, "분대장의 상징인 녹견", 10, 10, "체력+10, 정신력+10");
        //리스트에 아이템들 추가


        //아이템 정보 세팅


        //직업 별 아이템 설정//


        //직업 별 사단 마크
        ShoulderSleeve1 = new Armor("제2기갑여단 마크", 0, "포병 사단 마크", 10, 10, "체력+10, 정신력+10");// 기본템
        ShoulderSleeve2 = new Armor("제17보병사단 마크", 0, "보병 사단 마크", 10, 10, "체력+10, 정신력+10");// 기본템
        ShoulderSleeve3 = new Armor("제1수송교육연대 마크", 0, "야수교 마크", 10, 10, "체력+10, 정신력+10");// 기본템
        ShoulderSleeve4 = new Armor("육군종합정비창 마크", 0, "정비대대 마크", 10, 10, "체력+10, 정신력+10");// 기본템템

        Rank myRank = new Rank(1);

    }

    static int CheckValidInput(int min, int max)
    {
        while (true)
        {
            string input = Console.ReadLine();

            bool parseSuccess = int.TryParse(input, out var ret);
            if (parseSuccess)
            {
                if (ret >= min && ret <= max)
                    return ret;
            }
            //정해진 키가 아닌 키 입력시
            Console.WriteLine(" 잘못된 입력입니다.");
        }
    }

    #region 시작화면

    //시작화면
    static void StartScene()
    {
        int cursor = 0;
        bool onScene = true;
        string[] text = { "                              쉬움",
        "                              보통",
        "                             어려움" };
        while (onScene)
        {
            Console.Clear();
            //시작화면
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("                                                                         ");
            Console.WriteLine("                            군대  RPG                                    ");
            Console.WriteLine("                                                                         ");
            Console.ResetColor();
            Console.WriteLine("                    ==========================                           ");

            Console.WriteLine("                                                                         ");


            // Text[] Output
            TextChoice(cursor, text);
            Console.WriteLine("                                                                              ");
            Console.WriteLine("                    ==========================                             ");
            // Key Input
            e = Console.ReadKey();
            // Cursor Index
            cursor = CursorChoice(e, cursor, text, ref onScene);

        }
        switch (cursor)
        {
            case 0:
                //쉬움
                howhard = 1;
                foreach (Enemy enemy in enemys)
                {
                    enemy.EnemyAtk *= howhard;
                }
                Console.WriteLine(" 선택 난이도: 쉬움");
                Console.ReadKey();
                TrainingSchool(player1);//줄거리로 이동
                break;
            case 1:
                //보통
                howhard = 2;
                foreach (Enemy enemy in enemys)
                {
                    enemy.EnemyAtk *= howhard;
                }
                Console.WriteLine(" 선택 난이도: 보통");
                Console.ReadKey();
                TrainingSchool(player1);//줄거리로 이동
                break;
            case 2:
                //어려움
                howhard = 3;
                foreach (Enemy enemy in enemys)
                {
                    enemy.EnemyAtk *= howhard;
                }
                Console.WriteLine(" 선택 난이도: 어려움");
                Console.ReadKey();
                TrainingSchool(player1);//줄거리로 이동
                break;


        }
        TrainingSchool(player1);//줄거리로 이동

    }
    #endregion


    #region 훈련소
    //훈련소
    static void TrainingSchool(Character player)
    {
        // 커서 초기화 값
        string[] text = { " =보병=", " =포병=", " =운전병=", " =정비병=" };
        int cursor = 0;
        bool onScene = true;

        // 나레이션 초기화 값
        string tex = " 139번 훈련병! \n\n 너 이름이 뭐야? \n\n 이름을 입력 하세요... \n\n >>";
        char[] texs = tex.ToCharArray();

        // 화면 초기화
        Console.Clear();

        // 이름 입력받기.
        Console.WriteLine();
        foreach (char index in texs)
        {
            Console.Write(index);
            Thread.Sleep(80);
        }
        player.Name = Console.ReadLine();

        // 화면 초기화
        Console.Clear();

        while (onScene)
        {
            Console.Clear();

            Console.WriteLine("");
            Console.WriteLine($" 이제부터는 이병 {player.Name}이네.\n");
            Console.WriteLine(" 너 보직은 뭐야?\n");
            Console.WriteLine(" 보직을 선택하세요.");
            Console.WriteLine("");
            Console.WriteLine(" ==========================");
            Console.WriteLine("");

            // Text 배열 출력
            TextChoice(cursor, text);
            // Key 입력
            e = Console.ReadKey();
            // Cursor Index
            cursor = CursorChoice(e, cursor, text, ref onScene);
        }

        Console.Clear();
        Console.WriteLine("");
        switch (cursor)
        {
            case 0:
                //보병 전직
                player1 = new Infantry(player.Name, "보병", 5, 5, 5, 5, 100, 100, 0, 5);
                Console.WriteLine(" 보병을 선택했다.");
                //사단마크 획득
                player1.AddToInventoryArmor(ShoulderSleeve2);
                //사단마크 스탯 적용
                ShoulderSleeve2.isEquipped = true;
                player1.Mind += ShoulderSleeve2.ItemMind;
                player1.MaxHp += ShoulderSleeve2.ItemHp;
                player1.Hp += ShoulderSleeve2.ItemHp;
                break;
            case 1:
                //포병 전직
                player1 = new Artillery(player.Name, "포병", 5, 5, 5, 5, 100, 100, 0, 5);
                //사단마크 획득
                player1.AddToInventoryArmor(ShoulderSleeve1);
                //사단마크 스탯 적용
                ShoulderSleeve1.isEquipped = true;
                player1.Mind += ShoulderSleeve1.ItemMind;
                player1.MaxHp += ShoulderSleeve1.ItemHp;
                player1.Hp += ShoulderSleeve1.ItemHp;
                Console.WriteLine(" 포병을 선택했다.");
                Console.ReadKey();
                break;
            case 2:
                //운전병 전직
                player1 = new Transportation(player.Name, "운전병", 5, 5, 5, 5, 100, 100, 0, 5);
                //사단마크 획득
                player1.AddToInventoryArmor(ShoulderSleeve3);
                //사단마크 스탯 적용
                ShoulderSleeve3.isEquipped = true;
                player1.Mind += ShoulderSleeve3.ItemMind;
                player1.MaxHp += ShoulderSleeve3.ItemHp;
                player1.Hp += ShoulderSleeve3.ItemHp;
                Console.WriteLine(" 운전병을 선택했다.");
                Console.ReadKey();
                break;
            case 3:
                //정비병 전직
                player1 = new Maintenence(player.Name, "정비병", 5, 5, 5, 5, 100, 100, 0, 5);
                //사단마크 획득
                player1.AddToInventoryArmor(ShoulderSleeve4);
                //사단마크 스탯 적용
                ShoulderSleeve4.isEquipped = true;
                player1.Mind += ShoulderSleeve4.ItemMind;
                player1.MaxHp += ShoulderSleeve4.ItemHp;
                player1.Hp += ShoulderSleeve4.ItemHp;
                Console.WriteLine(" 정비병을 선택했다.");
                Console.ReadKey();
                break;
            default:
                break;
        }
        Console.WriteLine("");
        Console.WriteLine(" 자대로 가서도 꼭 연락해!");
        Console.WriteLine("");
        Console.ReadKey();
        Console.WriteLine("나의 군생활은 과연 어떨까? ㅎㅎ");
        Console.ReadKey();
        Console.WriteLine("한번 18개월 잘 버텨보자!!");
        Console.ReadKey();
        Console.WriteLine(" press any key to continue");
        Console.ReadKey();
        //나레이션
        Home();

    }
    #endregion

    #region 막사/생활관
    //막사 매서드
    static void Home()
    {
        Rank.SetRank();
        // 커서 초기화 값
        string[] text = { " =스토리 진행=", " =일과 진행=", " =인벤토리=", " =상태 확인=", " =PX 가기=", " =인터넷 도박=" };
        int cursor = 0;
        bool onScene = true;

        // 화면 초기화


        while (onScene)
        {
            // 계급 설정
            Rank.SetRank();

            Console.Clear();

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" ==========막사==========");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine($" 계급: {Rank.rank} 이름: {player1.Name}");
            Console.WriteLine();
            Console.WriteLine($" 군생활 {Rank.month}개월 째");
            Console.WriteLine();
            Console.WriteLine(" 무엇을 할 것인가?");
            Console.WriteLine();
            Console.WriteLine(" ========================\n");

            // Text 배열 출력
            TextChoice(cursor, text);
            // Key 입력
            e = Console.ReadKey();
            // Cursor Index
            cursor = CursorChoice(e, cursor, text, ref onScene);
        }

        switch (cursor)
        {
            case 0:
                //스토리 진행
                StoryPlay();
                break;
            case 1:
                //일과 진행
                DailyRoutineScene();
                break;
            case 2:
                //인벤토리
                DisplayInventory(player1);
                break;
            case 3:
                //상태확인
                DisplayMyInfo();
                break;
            case 4:
                //px
                PX();
                break;
            case 5:
                //px
                GambleDisplay();
                break;
        }
    }
    #endregion
    //month에 따라 달라지는 스토리
    static void StoryPlay()
    {
        switch (Rank.month)
        {   //이병
            case 1:            //1개월
                Basic(player1);
                break;
            case 2:            //2개월
                Basicstory(player1);
                break;
            //일병
            case 3:            //3개월
                FStoryRangerTraining(player1);
                break;
            case 4:            //4개월
                FStoryPullSecurity(player1, wildBoar, waterDeer);
                break;
            case 5:            //5개월
                HundredDaysvacationScene();
                break;
            case 6:            //6개월
                ShootingScene();
                break;
            case 7:            //7개월
                DMsupport();
                break;
            case 8:            //8개월
                Overnight();
                break;
            //상병
            case 9:            //9개월 
                CStoryPhysicalExamination(player1);
                break;
            case 10:            //10개월
                CSDefcon(player1);
                break;
            case 11:            //11개월
                CSschool();
                break;
            case 12:            //12개월
                CStoryKCTC(player1);
                break;
            case 13:            //13개월
                CSTest();
                break;
            case 14:            //14개월
                CSNewCommander(player1, newCommander);
                break;
            //병장
            case 15:            //15개월
                ColdWeatherTraining1();
                break;
            case 16:            //16개월
                CementWork1();
                break;
            case 17:            //17개월
                WarehouseWokr1();
                break;
            case 18:            //18개월
                LastLeave1();
                break;

        }


    }

    //한달 지나기
    static void OneMonthLater()
    {
        Rank.month++;
        Console.WriteLine("");
        Console.WriteLine(" 한달이 흘렀다");
        Console.WriteLine("");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write(" 월급");
        Console.ResetColor();
        Console.WriteLine(" 이 들어왔다");
        if (Rank.month < 3)
        {
            player1._gold += 1000;
        }
        else if (Rank.month < 9)
        {
            player1._gold += 1500;
        }
        else if (Rank.month < 15)
        {
            player1._gold += 2000;
        }
        else
        {
            player1._gold += 3000;
        }
        Console.ReadKey();
        Home();
    }

    #region 상태창
    //상태확인
    static void DisplayMyInfo()
    {
        Console.Clear();

        Rank.SetRank();

        Console.WriteLine();
        Console.WriteLine(" 상태확인");
        Console.WriteLine(" 당신의 정보를 표시합니다.");
        Console.WriteLine();
        Console.WriteLine("====================================");
        Console.WriteLine($" {Rank.rank} | {player1.Name} ");
        Console.WriteLine();
        Console.WriteLine($" 힘 \t\t: {player1.Str}");
        Console.WriteLine($" 민첩 \t\t: {player1.Dex}");
        Console.WriteLine($" 지능 \t\t: {player1.IQ}");
        Console.WriteLine($" 운 \t\t: {player1.Luk}");
        Console.WriteLine($" 체력 \t\t: {player1.Hp}");
        Console.WriteLine($" 정신력 \t: {player1.Mind}");
        Console.WriteLine($" Gold \t\t: {player1.Gold} G");
        Console.WriteLine("====================================");
        Console.WriteLine();
        Console.WriteLine(" 0. 돌아가기");
        Console.Write(">>");

        int input = CheckValidInput(0, 0);
        switch (input)
        {
            case 0:
                //막사로 돌아가기
                Home();
                break;
        }
    }
    #endregion

    #region 인벤토리

    //인벤토리
    static void DisplayInventory(Character player)
    {
        Console.Clear();

        Console.WriteLine();
        Console.WriteLine(" [관물대]");
        Console.WriteLine($"\t\t\t\t\t\t[소지금:{player1._gold}G]");
        Console.WriteLine();
        Console.WriteLine(" ========================");
        Console.Write(" |        ");
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.Write("weapon");
        Console.ResetColor();
        Console.WriteLine("        |");
        Console.WriteLine(" |======================|");
        Console.WriteLine(" |--------------|       |");
        Console.WriteLine(" |              |     | |");
        Console.WriteLine(" |              |       |");
        Console.Write(" |              |  ");
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.Write("food");
        Console.ResetColor();
        Console.WriteLine(" |");
        Console.WriteLine(" |              |       |");
        Console.WriteLine(" |              |=======|");
        Console.Write(" |    ");
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.Write("armor");
        Console.ResetColor();
        Console.WriteLine("     |       |");
        Console.WriteLine(" |              |       |");
        Console.WriteLine(" |              |       |");
        Console.WriteLine(" |              |=======|");
        Console.WriteLine(" |              |       |");
        Console.WriteLine(" |              |=======|");
        Console.WriteLine(" |              |   -   |");
        Console.WriteLine(" ========================");
        Console.WriteLine(" |                      |");

        Console.WriteLine();
        Console.WriteLine("1. 무기 확인하기");
        Console.WriteLine();
        Console.WriteLine("2. 방어구 확인하기");
        Console.WriteLine();
        Console.WriteLine("3. 음식 확인하기");
        Console.WriteLine();
        Console.WriteLine("0. 뒤로가기");
        int input = CheckValidInput(0, 3);
        switch (input)
        {
            case 0:
                Home();
                break;

            case 1:
                DisplayWeapon(player1);
                break;

            case 2:
                DisplayArmor(player1);
                break;

            case 3:
                DisplayFood(player1);
                break;

        }

    }
    //무기 인벤
    static void DisplayWeapon(Character player)
    {
        Console.Clear();
        Console.WriteLine("무기");
        Console.WriteLine("============================================================================");
        //인벤토리 리스트에 있는 아이템들 나열
        for (int i = 0; i < player.InventoryWeapon.Count; i++)
        {
            var weapon = player.InventoryWeapon[i];
            string equippedStatus = weapon.isEquipped ? "[E]" : ""; // 아이템이 장착되었는지 여부에 따라 [E] 표시 추가 없으면 공백
            Console.Write($"{i + 1}. ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write($"{equippedStatus}");
            Console.ResetColor();
            Console.WriteLine($" \t {weapon.ItemName} \t | {weapon.ItemDescription}"); //무기 부가 정보
            Console.WriteLine();
        }
        Console.WriteLine();

        Console.WriteLine("============================================================================");
        Console.WriteLine();
        Console.WriteLine(" 장착/해제를 원하는 아이템을 입력해주세요.");
        Console.WriteLine();
        Console.WriteLine(" 0. 뒤로가기");
        Console.Write(">>");

        int input = CheckValidInput(0, player.InventoryWeapon.Count);
        if (input > 0)
        {
            player.EquipWeapon(input);
            Console.WriteLine();
            Console.WriteLine("Press AnyKey");
            Console.ReadKey();
            //인벤토리창 새로고침
            DisplayWeapon(player1);
        }
        else
        {
            //관물대로 돌아가기
            DisplayInventory(player1);
        }
    }

    //방어구 인벤
    static void DisplayArmor(Character player)
    {
        Console.Clear();
        Console.WriteLine("방어구");
        Console.WriteLine("============================================================================");
        //인벤토리 리스트에 있는 아이템들 나열
        for (int i = 0; i < player.InventoryArmor.Count; i++)
        {
            var armor = player.InventoryArmor[i];
            string equippedStatus = armor.isEquipped ? "[E]" : ""; // 아이템이 장착되었는지 여부에 따라 [E] 표시 추가 없으면 공백
            Console.Write($"{i + 1}. ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write($"{equippedStatus}");
            Console.ResetColor();
            Console.WriteLine($" \t {armor.ItemName} \t | {armor.ItemDescription}"); //방어구 부가 정보
            Console.WriteLine();
        }
        Console.WriteLine("============================================================================");
        Console.WriteLine();
        Console.WriteLine(" 장착/해제를 원하는 아이템을 입력해주세요.");
        Console.WriteLine();
        Console.WriteLine(" 0. 뒤로가기");
        Console.Write(">>");

        int input = CheckValidInput(0, player.InventoryArmor.Count);
        if (input > 0)
        {
            player.EquipArmor(input);
            Console.WriteLine();
            Console.WriteLine("Press AnyKey");
            Console.ReadKey();
            //인벤토리창 새로고침
            DisplayArmor(player1);
        }
        else
        {
            //관물대로 돌아가기
            DisplayInventory(player1);
        }
    }
    //음식 인벤
    static void DisplayFood(Character player)
    {
        Console.Clear();
        Console.WriteLine("음식");
        Console.WriteLine("============================================================================");
        //인벤토리 리스트에 있는 아이템들 나열
        for (int i = 0; i < player.InventoryFood.Count; i++)
        {
            var food = player.InventoryFood[i];
            Console.Write($"{i + 1}. ");
            Console.WriteLine($" \t {food.ItemName} \t | {food.ItemDescription}"); //음식 부가 정보
            Console.WriteLine();
        }

        Console.WriteLine("============================================================================");
        Console.WriteLine();
        Console.WriteLine(" 섭취할 음식을 입력해주세요.");
        Console.WriteLine();
        Console.WriteLine(" 0. 뒤로가기");
        Console.Write(">>");

        int input = CheckValidInput(0, player.InventoryFood.Count);
        if (input > 0)
        {
            var food = player.InventoryFood[input - 1];
            var selectedItem = player.InventoryFood[input - 1];
            EatFood(food);
            Console.WriteLine();
            player.InventoryFood.Remove(selectedItem);//선택한 아이템 제거
            Console.WriteLine("Press AnyKey");
            Console.ReadKey();
            //인벤토리창 새로고침
            DisplayFood(player1);
        }
        else
        {
            //관물대로 돌아가기
            DisplayInventory(player1);
        }
    }

    static void EatFood(Food food)
    {
        Console.WriteLine($"{food.ItemName}을 사용했습니다.");
        Console.WriteLine();
        player1.Hp += food.ItemHp;
        if (player1.Hp >= player1.MaxHp)
        {
            player1.Hp = player1.MaxHp;
        }
        Console.WriteLine("체력이 회복되었습니다.");
    }
    #endregion

    #region 일과 ( 상시 이벤트 )
    // 일과 ( 상시 이벤트 )
    static void DailyRoutineScene()
    {
        // Cursor && Scene 초기화 값
        int cursor = 0;
        bool onScene = true;

        // Text 배열
        string[] text = { " ==체력 단련==\n", " ==주특기 훈련==\n", " ==행보관님 작업==\n", " ==메인 화면==\n" };

        while (onScene)
        {
            // 화면 초기화
            Console.Clear();

            Console.WriteLine("");
            Console.WriteLine(" 오늘 하루도 힘내보자.");
            Console.WriteLine("");
            Console.WriteLine(" 어떤일을 해볼까?\n");
            Console.WriteLine("==============================");
            Console.WriteLine("");

            // Text[] Output
            TextChoice(cursor, text);
            // Key Input
            e = Console.ReadKey();
            // Cursor Index
            cursor = CursorChoice(e, cursor, text, ref onScene);
        }

        switch (cursor)
        {
            case 0:
                PhysicalTraining_Run();
                break;
            case 1:
                Speciality_Common();
                break;
            case 2:
                Work_Shoveling();
                break;
            case 3:
                Home();
                break;
            default:
                break;
        }
    }
    #endregion

    #region 체력 단련 ( 상시 이벤트 선택지 )
    /*
    // 체력 단련 ( 상시 이벤트 선택지 )
    static void PhysicalTrainingScene()
    {
        // Cursor && Scene 초기화 값
        int cursor = 0;
        bool onScene = true;

        // Text 배열
        string[] text = { "1. 구보", "2. 팔굽혀펴기", "3. 윗몸 일으키기", "4. 턱걸이", "5. 돌아가기" };

        while (onScene)
        {
            Console.Clear();

            Console.WriteLine("건강한 육체에 건강한 정신이 깃든다!");
            Console.WriteLine("오늘은 어떠한 운동으로 나의 육체를 단련해 볼까?");
            Console.WriteLine("");

            // Text 배열 출력
            TextChoice(cursor, text);
            // Key 입력
            e = Console.ReadKey();
            // Cursor index
            cursor = CursorChoice(e, cursor, text, ref onScene);
        }

        switch (cursor)
        {
            case 0:
                // 구보
                PhysicalTraining_Run();
                break;
            case 1:
                // 팔굽
                Console.WriteLine("미구현");
                Console.ReadKey();
                PhysicalTrainingScene();
                break;
            case 2:
                // 윗몸
                Console.WriteLine("미구현");
                Console.ReadKey();
                PhysicalTrainingScene();
                break;
            case 3:
                // 턱걸이
                Console.WriteLine("미구현");
                Console.ReadKey();
                PhysicalTrainingScene();
                break;
            case 4:
                // 돌아가기
                DailyRoutineScene();
                break;
            default:
                break;
        }
    }
    */

    static void PhysicalTraining_Run()
    {
        // Start Point
        int x = 0;

        double time = 30;

        Console.Clear();

        // Explanation
        Console.WriteLine("");
        Console.WriteLine(" 아무 키나 연타해서 Goal 지점에 도착하세요!");
        Console.WriteLine("");
        Console.WriteLine(" 빨리 도착할수록 좋은 보상을 얻습니다!");
        Console.WriteLine("");
        Console.WriteLine(" 움직이지 않고 가만히 있으면 시간이 더 빠르게 흘러갑니다.");
        Console.WriteLine("");
        Console.WriteLine(" >> Press the Any key to proceed <<");
        Console.ReadKey();

        // 화면 초기화
        Console.Clear();

        // 라인 좌표 설정 및 텍스트
        Console.SetCursorPosition(0, 0);
        Console.WriteLine("================================================================================@\n" +
            "                                                                                @\n" +
            "================================================================================@");

        // Game Start
        while (true)
        {
            string space = "";

            // 타이머 차감 및 남은 시간 텍스트
            time -= 0.1f;
            Console.SetCursorPosition(30, 5);
            Console.Write($"남은 시간 : {time.ToString("F")}");

            // player 위치 표시
            Console.SetCursorPosition(0, 1);
            for (int i = 0; i < x; i++)
            {
                space += " ";
            }
            Console.WriteLine(space + "O");

            // 이동 로직
            if (Console.KeyAvailable)
            {
                x++;
                Console.ReadKey(true);
            }
            // 완주시 반복문 정지
            if (x >= 80)
            {
                Console.Clear();
                Console.WriteLine("\n 완주 완료!!");
                Console.WriteLine("\n 보상 계산중.... 잠시만 기다려주십시오.");
                Thread.Sleep(2000);

                Console.WriteLine("\n 남은 시간 : {0}", time.ToString("F"));
                Console.WriteLine("\n 남은 시간에 따른 보상 ( 보상목록 )");
                Console.WriteLine("\n >> Press the \"TAP\" key to proceed <<");
                break;
            }
            // 타임 오버시
            if (time <= 0)
            {
                Console.Clear();
                Console.WriteLine("\n 완주 실패....");
                Console.WriteLine("\n >> Press the \"TAP\" key to proceed <<");
                Thread.Sleep(2000);
                Home();
            }
            Thread.Sleep(10);
        }
        //선입력 방지 메서드
        InputPrevention();
        Home();
        // 남은 시간에 따른 보상 및 씬이동 로직 추가예정

    }
    #endregion

    #region 주특기 훈련 ( 상시 이벤트 선택지 )

    /*
    // 주특기 훈련 ( 상시 이벤트 선택지 )
    static void SpecialityScene()
    {
        // Cursor && Scene 초기화 값
        int cursor = 0;
        bool onScene = true;

        // Text 배열
        string[] text = { "1. 본 주특기", "2. 공통 주특기", "3. 돌아가기" };

        while (onScene)
        {
            // 화면 초기화
            Console.Clear();

            Console.WriteLine("실전처럼 훈련하고 훈련한 대로 싸운다!");
            Console.WriteLine("The Only Easy Day Was YesterDay");
            Console.WriteLine("어떤 주특기를 훈련할까?");
            Console.WriteLine("");

            // Text 배열 출력
            TextChoice(cursor, text);
            // Key 입력
            e = Console.ReadKey();
            // Cursor index
            cursor = CursorChoice(e, cursor, text, ref onScene);
        }

        switch (cursor)
        {
            case 0:
                // 본 주특기
                Console.WriteLine("미구현");
                Console.ReadKey();
                SpecialityScene();
                break;
            case 1:
                // 공통 주특기
                Speciality_Common();
                break;
            case 2:
                // 돌아가기
                DailyRoutineScene();
                break;
            default:
                break;
        }
    }
    */

    static void Speciality_Common()
    {
        // Random 객체 및 randomNumber 초기화
        Random random = new Random();
        int randomNumber = 0;

        // 방향키 입력 순서
        int sequence = 0;

        // 타이머
        double time = 10f;

        // 성공 횟수
        int hitCount = 0;

        // 방향키[]
        char[] text = { '↑', '↓', '←', '→' };

        // 랜덤 방향키를 담을 리스트
        List<char> numberBox = new List<char>();

        // 화면 초기화
        Console.Clear();

        Console.WriteLine("");
        Console.WriteLine(" 표시되는 방향키를 순서대로 누르세요!");
        Console.WriteLine("");
        Console.WriteLine(" 총 10개가 나오며 성공한 수대로 보상을 받습니다.");
        Console.WriteLine("");
        Console.WriteLine(" >> Press the Any key to proceed <<");

        // 아무 Key나 누를시 진행
        Console.ReadKey(true);

        // 화면 초기화
        Console.Clear();
        Console.WriteLine("========================================");
        for (int i = 0; i < 10; i++)
        {
            randomNumber = random.Next(0, 3);
            Console.Write($" {text[randomNumber]} ");
            numberBox.Add(text[randomNumber]);
        }
        Console.WriteLine("");
        Console.WriteLine("========================================");
        Console.WriteLine("");
        Console.WriteLine(" 보기를 외워서 알맞은 키를 순서대로 누십시오!");
        Console.WriteLine("");
        Console.WriteLine(" 남은 시간이 끝나면 보기가 사라집니다.");

        // 타이머 표시
        while (time >= 0)
        {
            Console.SetCursorPosition(0, 8);
            Console.WriteLine($"\n 남은 시간 : {time:F}");
            time -= 0.01f;
            Thread.Sleep(10);
        }

        // 화면 초기화
        Console.Clear();

        Console.WriteLine("\n 시작하려면 \"Tap키\" 를 눌러주세요.");
        // 선입력 방지
        InputPrevention();

        Console.Clear();

        // 입력 로직
        Console.WriteLine("\n 알맞는 키를 입력하시오.");
        while (sequence < 10)
        {
            e = Console.ReadKey(true);
            switch (e.Key)
            {
                case ConsoleKey.UpArrow:
                    if (numberBox[sequence] == '↑') hitCount++;
                    Console.Write(" ↑ ");
                    break;
                case ConsoleKey.DownArrow:
                    if (numberBox[sequence] == '↓') hitCount++;
                    Console.Write(" ↓ ");
                    break;
                case ConsoleKey.LeftArrow:
                    if (numberBox[sequence] == '←') hitCount++;
                    Console.Write(" ← ");
                    break;
                case ConsoleKey.RightArrow:
                    if (numberBox[sequence] == '→') hitCount++;
                    Console.Write(" → ");
                    break;
                default:
                    break;
            }
            sequence++;
        }

        Console.WriteLine("");
        Console.WriteLine("\n 총 맞춘 횟수 : {0}", hitCount);
        Console.WriteLine("\n 보상 ( 보상 ) 을 지급합니다.");
        Console.WriteLine("\n 진행하려면 \"Tap키\" 를 눌러주세요.");

        InputPrevention();
        // 맞춘 횟수에 맞는 보상 지급 로직 추가
        // 메인화면 이동
        Home();
    }
    #endregion

    #region 작업 ( 상시 이벤트 선택지)

    /*
    // 작업 ( 상시 이벤트 선택지)
    static void WorkScene()
    {
        // Cursor && Scene 초기화 값
        int cursor = 0;
        bool onScene = true;

        // Text 배열
        string[] text = { "1. 예초", "2. 제설", "3. 삽질", "4. 돌아가기" };

        while (onScene)
        {
            // 화면 초기화
            Console.Clear();

            Console.WriteLine("훅 훅.. 행정반에서 전파합니다.");
            Console.WriteLine("보급관님께서 작업 인원 내려오라고 했습니다");
            Console.WriteLine("신속하게 내려와주시기 바랍니다.");
            Console.WriteLine("");
            Console.WriteLine("어~ 그래 (player_Name)! 역시 에이스야!");
            Console.WriteLine("오늘 할 작업은...");
            Console.WriteLine("");

            // Text 배열 출력
            TextChoice(cursor, text);
            // Key 입력
            e = Console.ReadKey();
            // Cursor index
            cursor = CursorChoice(e, cursor, text, ref onScene);
        }

        switch (cursor)
        {
            case 0:
                // 예초
                Console.WriteLine("미구현");
                Console.ReadKey();
                WorkScene();
                break;
            case 1:
                // 제설
                Console.WriteLine("미구현");
                Console.ReadKey();
                WorkScene();
                break;
            case 2:
                // 삽질
                Work_Shoveling();
                break;
            case 3:
                // 돌아가기
                DailyRoutineScene();
                break;
            default:
                break;
        }
    }
    */

    // 작업_삽질
    static void Work_Shoveling()
    {
        // 성공 횟수
        int hitCount = 0;
        // 화살표 속도
        int arrowSpeed = 25;
        // 화면 초기화
        Console.Clear();

        Console.WriteLine("");
        Console.WriteLine(" 화살표가 가운데 왔을 때 아무 키나 누르세요!");
        Console.WriteLine("");
        Console.WriteLine(" 총 5번 진행되며 성공한 수대로 보상을 받습니다.");
        Console.WriteLine("");
        Console.WriteLine(" >> Press the Any key to proceed <<");

        // 아무 Key나 누를시 진행
        Console.ReadKey(true);

        for (int i = 1; i <= 5; i++)
        {
            hitCount = ShovelingEvent(hitCount, arrowSpeed);
            // 웨이브별 속도 업 ( 난이도 상승 )
            arrowSpeed -= 5;

            if (i == 5)
            {
                Console.WriteLine("\n 총 성공 횟수 : {0} ", hitCount);
                Console.WriteLine("\n 횟수에 맞게 보상을 지급합니다!");
            }
            else
            {
                // 대기시간
                Console.WriteLine("\n 현재 라운드 {0} / {1}   성공 횟수 : {2} ", i, 5, hitCount);
                Console.WriteLine("\n 다음 웨이브를 시작하려면 \"Tap키\" 를 눌러주세요!");
                InputPrevention();
            }
        }
        Console.WriteLine("\n 보상 (보상)을 지급합니다.");
        Console.WriteLine("\n 진행하려면 \"Tap키\" 를 눌러주세요.");
        InputPrevention();
        // hitCount 횟수에 맞는 보상 지급
        // 메인화면 이동
        Home();
    }

    // 삽질_로직
    static int ShovelingEvent(int _hitCount, int _arrowSpeed)
    {
        // 초기 좌표 설정 값
        int xfront = 60;
        int xback = 0;

        Console.Clear();

        Console.WriteLine("===========================ㅣ   ㅣ===========================\n\n" +
            "===========================ㅣ   ㅣ===========================\n\n" +
            "               Press Enter at the right time                ");

        while (!Console.KeyAvailable && xfront > 0)
        {
            // 앞 뒤 공백변수 선언
            string space = "";
            string spaceback = "";

            // 화살표 이동 로직
            Console.SetCursorPosition(0, 1);
            for (int i = 0; i < xfront; i++)
            {
                space += " ";
            }
            for (int j = 0; j < xback; j++)
            {
                spaceback += " ";
            }
            Console.Write(space + "<" + spaceback);

            // space수량 조절
            xfront--;
            xback++;
            // 대기시간
            Thread.Sleep(_arrowSpeed);
        }
        // 삽질 성공or실패
        if (xfront >= 28 && xfront <= 32)
        {
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("                         삽질 성공 !                ");
            Thread.Sleep(1000);
            _hitCount++;
        }
        else
        {
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("                         삽질 실패 . . .              ");
            Thread.Sleep(1000);
        }
        // 화면 초기화
        Console.Clear();

        // KeyAvailable 초기화
        Console.ReadKey();

        return _hitCount;
    }
    #endregion

    //이등병 스토리//
    static void Basic(Character player)
    {


        Console.Clear();
        Console.WriteLine();
        Console.WriteLine("아침점호 시간이다");
        Console.ReadKey();
        Console.WriteLine("긴장한 상태로 열을 맞춰서있다..");
        Console.ReadKey();
        Console.WriteLine("그때 한 선임이 \"굳건이 군화 닦았어 ? \"");
        Console.ReadKey();
        Console.WriteLine("굳건이:옙. 닦았습니다.");
        Console.ReadKey();
        Console.WriteLine("맞선임:진짜? 확인해봐서 안닦였으면 뒤진다.");
        Console.ReadKey();
        Console.WriteLine("맞선임:이거 봐봐. 이게 닦은거야?");
        Console.ReadKey();
        Console.WriteLine("굳건이:죄.. 죄송합니다.");
        Console.ReadKey();
        Console.WriteLine("맞선임:너 점호끝나고 보자");
        Console.ReadKey();
        Console.WriteLine("<점호 후 막사 뒷편 창고>");
        Console.ReadKey();
        Console.WriteLine("맞선임: 너 왜 군화 닦앗다고 거짓말쳐?? ");
        Console.ReadKey();
        Console.WriteLine("굳건이: 아 당황해서 잘못말한것 같습니다..");
        Console.ReadKey();
        Console.WriteLine("맞선임: (발로 정강이를 까며)그렇다고 거짓말을 쳐? 라떼는 말이야 하.. 아니다");
        Console.ReadKey();
        Console.WriteLine("굳건이: 근데 때리는건 너무 하지 않습니까? ");
        Console.ReadKey();
        Console.WriteLine("맞선임: 머라고??(머리를 때리려 손을 들면서)");
        Console.ReadKey();
        Console.WriteLine("그때 굳건이가 맞선임의 손을 막으면서 맞짱이 시작되었다..!!");
        Console.ReadKey();
        Console.WriteLine("싸움을 하실려면 아무키나 눌러주세요");
        Console.ReadKey();

        SeniorFight(player, senior);
    }
    //맞선임 보스전


    static void SeniorFight(Character player, Enemy enemy)
    {
        while (true)
        {
            enemy.EnemyHp -= player.Str; //플레이어가 맞선임 공격
            if (enemy.EnemyHp <= 0)
            {
                Console.WriteLine("맞선임과의 맞짱에서 승리했습니다");
                Console.WriteLine($"남은 체력:{player.Hp}");
                break;
            }
            player.Hp -= enemy.EnemyAtk;//맞선임이 플레이어 공격
            if (player1.Hp <= 0)
            {
                Console.WriteLine("맞선임과의 맞짱에서 패배했습니다");
                break;
            }
        }
        Console.WriteLine("후.. 군생활 힘드네");
        OneMonthLater();
    }


    static void Basicstory(Character player)
    {
        Console.Clear();

        Console.WriteLine("오늘도 다사다난한 하루였고만");
        Console.ReadKey();
        Console.WriteLine("이제 군화닦고 청소를 시작해보자");
        Console.ReadKey();
        Console.WriteLine("오늘은 화장실 청소하는 날이네");
        Console.ReadKey();
        Console.WriteLine("선임한테 잘보이기 위해 내가 변기를 맡아서 열심히 닦아야겠다");
        Console.ReadKey();
        Console.WriteLine("아니 오줌 조준못하는건 이해하는데 똥 조준은 왜 못하는거야?");
        Console.ReadKey();
        Console.WriteLine("라는 생각과 함꼐 청소가 끝났다!!");
        Console.ReadKey();
        Console.WriteLine("드디어 오늘 하루도 끝나가는군!!");
        Console.ReadKey();
        Console.WriteLine("");
        Console.WriteLine("----(저녁 점호 시간)-----)");
        Console.WriteLine();
        Console.ReadKey();
        Console.Write("정적이 흐른다... 그때");
        Console.WriteLine("분대장: 굳건이 혹시 여자친구나 여동생이나 누나 있어?");
        Console.ReadKey();
        Console.WriteLine("군생활이 걸린 대답 생각중");

        Random Talk = new Random();
        int number = Talk.Next(2);
        if (number == 0)
        {
            Console.WriteLine("네 있습니다!! 저 여자친구도 있고 여동생 1명과 누나 1명 있습니다");
            Console.WriteLine("군생활이 화창하다!");
        }
        else
        {
            player1.Luk -= 30;
            Console.WriteLine("아뇨.. 아무도 없습니다");
            Console.WriteLine("군생활 어떻하냐.. 막막하네..");
        }
        Console.WriteLine("하루가 1년같았다..");
        Console.ReadKey();
        OneMonthLater();
    }


    //일병 스토리 - 유격
    static void FStoryRangerTraining(Character player)
    {

        Random random = new Random();
        double success = 0.01; //초기 성공 확률 1%

        // 나레이션 설정 값
        string narrations = " \n ...훈련의 꽃 유격훈련이 시작됬다. \n\n \"지금부터 대답은 '네'가 아니라 '악'으로 대체합니다.\" \n\n 악! \n\n \"PT체조 8번 온몸비틀기 준비!\" \n\n 교관은 쉽게 갈 생각이 없는거같다 살아남자!";
        char[] narration = narrations.ToCharArray();

        // 나레이션 시작
        Console.Clear();
        foreach (char index in narration)
        {
            Console.Write(index);
            Thread.Sleep(50);
        }
        Console.ReadKey();
        


        
        //확률에 따라 성공 혹은 실패
        //실패마다 정신력, 체력 감소
        //실패시 출력멘트
        while (true)
        {
            double randomValue = random.NextDouble(); //0.0 이상 1.0 미만의 랜덤 실수

            if (randomValue < success)
            {
                Console.Clear();
                Console.WriteLine("");
                Console.WriteLine(" \"교육생들 수고 많았습니다.\"");
                Console.WriteLine("");
                Console.ReadKey();
                Console.WriteLine(" \"본 교관 나쁜사람 아닙니다.\"");
                Console.WriteLine("");
                Console.ReadKey();
                Console.WriteLine(" \"교육생들 막사로 가서 쉬도록합니다.\"");
                Console.WriteLine("");
                Console.ReadKey();
                Console.WriteLine("");
                Console.WriteLine(" 지옥같은 유격훈련이 끝났다... 돌아가자.");
                Console.WriteLine("");
                Console.ReadKey();
                Console.WriteLine(" 힘이 10 증가했다.");
                Console.WriteLine(" 정신력이 50 증가했다.");
                player.Str += 10;
                player.Mind += 50;
                Console.ReadKey();
                //성공시 스텟증가 추가해야됨
                OneMonthLater();
                break;

            }
            else
            {
                Console.Clear();
                //실패문구 랜덤생성
                string[] failMessages ={
                    " 목소리 크게 합니다. 다시!",
                    " 누가 마지막 구호를 외쳐! 다시!",
                    " 자세 똑바로 합니다. 다시!",
                    " 누가 한숨쉬었습니까. 다시!",
                    " 목소리가 작습니다. 다시!",
                    " 교관 실망시킬겁니까. 다시!"
                };
                int randomIndex = random.Next(failMessages.Length);
                Console.WriteLine("");
                Console.WriteLine(failMessages[randomIndex]);
                player.Hp -= 2;
                player.Mind -= 2;
                Console.WriteLine("");
                Console.WriteLine($" Hp : {player.Hp}");
                Console.WriteLine($" 정신력 : {player.Mind}");
                Console.WriteLine("");
                success += 0.03; //실패시 성공확률 3%씩 증가
                Console.ReadKey();
                //추가로 실패시 정신력, 체력 감소 추가해야됨
            }
        }
    }

    //일병 스토리 - 경계근무
    static void FStoryPullSecurity(Character player1, Enemy wildBoar, Enemy waterDeer)
    {
        

        // 나레이션 설정 값
        string narrations = " \n 어두운 새벽 경계근무중... \n\n 저 앞 풀숲에서 부스럭거리는 소리가 난다. \n\n" +
            $" 야생의 {wildBoar.EnemyName}와 {waterDeer.EnemyName}가 나타났다! \n\n 아무 키나 누르시오.";
        char[] narration = narrations.ToCharArray();

        // 나레이션 시작
        Console.Clear();
        foreach (char index in narration)
        {
            Console.Write(index);
            Thread.Sleep(80);
        }
        Console.ReadKey();
        BattleScene(player1, wildBoar, waterDeer);
    }

    //전투 메서드
    static void BattleScene(Character player1, params Enemy[] enemies)
    {
        // Cursor 선택 설정 값
        int cursor = 0;
        bool onScene = true;
        string[] text = { " ==공격==\n", " ==스킬==" };

        while (player1.Hp > 0 && (wildBoar.EnemyHp > 0 || waterDeer.EnemyHp > 0))
        {
            //내 턴
            while (onScene)
            {
                //체력이 음수인 경우 0으로 처리
                if (wildBoar.EnemyHp < 0)
                {
                    wildBoar.EnemyHp = 0;
                }

                if (waterDeer.EnemyHp < 0)
                {
                    waterDeer.EnemyHp = 0;
                }

                if (player1.Hp < 0)
                {
                    player1.Hp = 0;
                }
                // 화면 초기화
                Console.Clear();
                Console.WriteLine("");
                Console.ForegroundColor = (wildBoar.EnemyHp <= 0 ? ConsoleColor.DarkGray : ConsoleColor.White);
                Console.WriteLine($" {wildBoar.EnemyName}: HP {wildBoar.EnemyHp}  {(wildBoar.EnemyHp <= 0 ? "Dead" : "")}");
                Console.ForegroundColor = (waterDeer.EnemyHp <= 0 ? ConsoleColor.DarkGray : ConsoleColor.White);
                Console.WriteLine($" {waterDeer.EnemyName}: HP {waterDeer.EnemyHp}  {(waterDeer.EnemyHp <= 0 ? "Dead" : "")}");
                Console.ResetColor();
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine($" {player1.Name}");
                Console.WriteLine($" HP : {player1.Hp}");
                Console.WriteLine($" MIND : {player1.Mind}");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine(" 플레이어의 턴입니다. 행동을 선택하세요\n");
                Console.WriteLine(" ==============================================");
                Console.WriteLine("");
                // Text 배열 출력
                TextChoice(cursor, text);
                // Key 입력
                e = Console.ReadKey();
                // Cursor index
                cursor = CursorChoice(e, cursor, text, ref onScene);


            }

            // Cursor input
            switch (cursor)
            {
                case 0:
                    AttackAction(player1, wildBoar, waterDeer);
                    Console.ReadKey();
                    break;
                case 1:                    
                    SkillAction(player1, wildBoar, waterDeer);
                    Console.ReadKey();
                    break;
            }

            // 화면 초기화
            Console.Clear();
            //몬스터 턴
            if (wildBoar.EnemyHp > 0)
            {
                Console.WriteLine("");
                Console.WriteLine($" \n {wildBoar.EnemyName}의 공격!\n");

                if (player1.CheckEvade())
                {
                    Console.WriteLine(" 공격을 회피했습니다!");
                }
                else
                {
                    int enemyDamage1 = wildBoar.EnemyAtk;
                    player1.Hp -= enemyDamage1;
                    Console.WriteLine($" {wildBoar.EnemyName}이(가) 플레이어에게 {enemyDamage1}의 데미지를 입혔습니다.\n");
                }
            }

            if (waterDeer.EnemyHp > 0)
            {
                Console.WriteLine("");
                Console.WriteLine($" \n {waterDeer.EnemyName}의 공격!\n");
                if (player1.CheckEvade())
                {
                    Console.WriteLine(" 공격을 회피했습니다!");
                }
                else
                {
                    int enemyDamage2 = waterDeer.EnemyAtk;
                    player1.Hp -= enemyDamage2;
                    Console.WriteLine($" {waterDeer.EnemyName}이(가) 플레이어에게 {enemyDamage2}의 데미지를 입혔습니다.\n");
                }
            }
            Console.Write("\n            :::::Press any key:::::");
            Console.ReadKey();
            // bool값 및 Cursor값 초기화
            onScene = true;
            cursor = 0;
        }

        //전투 결과
        DisplayResult(player1, wildBoar, waterDeer);

    }
    //공격선택 메서드
    private static void AttackAction(Character player1, params Enemy[] enemies)
    {
        int cursor = 0;
        bool onScene = true;
        string[] text = new string[enemies.Length];
        for (int i = 0; i < enemies.Length; i++)
        {
            text[i] += " =" + enemies[i].EnemyName + "=\n";
        }

        while (onScene)
        {
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine("\n 어떤 몬스터를 공격하시겠습니까? \n ");

            TextChoice(cursor, text);
            e = Console.ReadKey();
            //cursor = CursorChoice(e, cursor, text, ref onScene);

            if (e.Key == ConsoleKey.Enter)
            {
                if (enemies[cursor].EnemyHp <= 0)
                {
                    Console.WriteLine($" {enemies[cursor].EnemyName}은(는) 이미 사망했습니다. 다른 몬스터를 선택하세요.");
                    Console.ReadKey();
                }
                else
                {
                    cursor = CursorChoice(e, cursor, text, ref onScene);

                    if (e.Key == ConsoleKey.Enter)
                    {
                        Console.Clear();
                        int playerDamage = player1.Attack();
                        enemies[cursor].EnemyHp -= playerDamage;
                        Console.WriteLine("");
                        Console.WriteLine($" \n 플레이어가 {enemies[cursor].EnemyName}에게 {playerDamage}의 데미지를 입혔습니다.");
                        onScene = false;
                    }
                }
            }
            else if (e.Key == ConsoleKey.UpArrow)
            {
                cursor = (cursor - 1 + enemies.Length) % enemies.Length;
            }
            else if (e.Key == ConsoleKey.DownArrow)
            {
                cursor = (cursor + 1) % enemies.Length;
            }
        }


    }

    //스킬사용 메서드
    private static void SkillAction(Character player1, params Enemy[] enemies)
    {
        int skillcursor = 0;
        bool skillSelection = true;
        string[] skilltext = new string[player1.Skills.Count];
        for (int i = 0; i < player1.Skills.Count; i++)
        {
            skilltext[i] += " =" + player1.Skills[i].Name + "=" + player1.Skills[i].MindCost + "=\n";
        }

        while (skillSelection)
        {
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine("\n 어떤 스킬을 사용하시겠습니까? \n ");

            TextChoice(skillcursor, skilltext);
            e = Console.ReadKey();
            skillcursor = CursorChoice(e, skillcursor, skilltext, ref skillSelection);

            if (e.Key == ConsoleKey.Enter)
            {
                MonsterSelection(player1, player1.Skills[skillcursor], enemies);
                skillSelection = false;
            }
        }
    }
    //스킬을 사용할 몬스터 선택
    private static void MonsterSelection(Character player1, Skill skill, Enemy[] enemies)
    {
        Console.Clear();
        Console.WriteLine("");
        Console.WriteLine($"{skill.Name} 를 사용합니다.");


        int targetCursor = 0;
        bool targetSelection = true;
        string[] targetText = new string[enemies.Length];

        for (int i = 0; i < enemies.Length; i++)
        {
            targetText[i] += " =" + enemies[i].EnemyName + "=\n";
        }

        while (targetSelection)
        {
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine("\n 어떤 몬스터를 공격하시겠습니까? \n");

            TextChoice(targetCursor, targetText);
            e = Console.ReadKey();

            if (e.Key == ConsoleKey.Enter)
            {

                if (enemies[targetCursor].EnemyHp <= 0)
                {
                    Console.WriteLine($" {enemies[targetCursor].EnemyName}은(는) 이미 사망했습니다. 다른 몬스터를 선택하세요.");
                    Console.ReadKey();
                    continue;
                }
                else
                {
                    Console.Clear();
                    skill.Execute(player1, enemies[targetCursor]);

                    //스킬 사용 후 정신력 체크
                    if (player1.Mind < skill.MindCost)
                    {
                        //정식력 부족한 경우
                        BattleScene(player1, wildBoar, waterDeer);          
                    }
                    else
                    {
                        //정신력 충분한 경우
                        targetSelection = false;
                    }

                }
            }


            targetCursor = CursorChoice(e, targetCursor, targetText, ref targetSelection);


        }


    }

    //보상 처리
    private static void DisplayResult(Character player1, params Enemy[] enemies)
    {
        if (player1.Hp <= 0)
        {
            Console.Clear();
            Console.WriteLine("전투에서 패배했습니다. 게임 오버!");
            Console.ReadKey();
            Home();
            return;
        }
        else
        {
            Console.Clear();
            Console.WriteLine("적을 처치했습니다. 승리!");
            // 몬스터별 보상 처리
            foreach (var enemy in enemies)
            {
                //player1.Gold += enemy.GoldReward;
                // 경험치 또는 다른 보상 처리도 추가 가능
            }
            player1.Mind += 30;
            player1.Gold += 1000;
            Console.ReadLine();
            OneMonthLater();
            //보상 아이템? 스텟?
        }


    }


    #region 일병 - 100일 휴가
    //일병 스토리 - 100일 휴가
    static void HundredDaysvacationScene()
    {
        // 초기 씬 셋팅값
        int cursor = 0;
        bool onScene = true;

        // Random 객체 생성
        Random random = new Random();

        // Random값을 담아둘 변수
        int randomNum = 0;

        // 선택지 Text
        string[] text = {"\n 여자친구를 만나러 간다.",
        "\n 친구들을 만나러 간다.",
        "\n 본가로 간다.",
        "\n 혼자 논다."};

        // 게임 시작
        // 화면 초기화
        Console.Clear();

        while (onScene)
        {
            // Random Number 설정
            randomNum = random.Next(1, 11);

            //화면 초기화
            Console.Clear();

            Console.WriteLine("");
            Console.WriteLine("l =========================== l");
            Console.WriteLine("l 드디어 100일 휴가를 나왔다! l");
            Console.WriteLine("l 어떤 일을 먼저 해볼까?      l");
            Console.WriteLine("l =========================== |");

            // Text[] Output
            TextChoice(cursor, text);
            // Key Input
            e = Console.ReadKey();
            // Cursor Index
            cursor = CursorChoice(e, cursor, text, ref onScene);
        }

        // 화면 지우기
        Console.Clear();

        // Cursor값에 따른 선택지
        switch (cursor)
        {
            case 0:
                // 여자친구 만나러
                OneHundredDaysEvent(randomNum, "\n 여자친구가 다른 남자와 다정하게 걷고 있다...\n",
                "\n 여자친구와 즐거운 시간을 보냈다.\n",
                "\n 나는 여자친구가 없다...\n");
                break;
            case 1:
                // 친구들 만나러
                OneHundredDaysEvent(randomNum, "\n 오랜만에 친구들과 술 한잔하며 이야기했다.\n",
                "\n 친구들과 Pc방에 가서 시간 가는 줄 모르고 놀았다.\n",
                "\n 나는 친구가 없다...\n");

                break;
            case 2:
                // 본가로 간다
                OneHundredDaysEvent(randomNum, "\n 오랜만에 집에 왔건만 군대에서 뭐했냐며 잔소리만 들었다...\n",
                "\n 가족들과 오랜만에 식사하며 좋은 시간을 보냈다.\n",
                "\n 내가 오는 줄 몰랐나..? 아무도 없다...\n");
                break;
            case 3:
                // 혼자 논다
                OneHundredDaysEvent(randomNum, "\n 혼자 즐겁게 놀았다. 진짜 즐거운 거 맞다, 아마도..\n",
                "\n 여기저기 구경 다니며 신나게 놀았다.\n",
                "\n 생활관에 있을 때가 더 나은 거 같다 너무 외롭다..\n");
                break;
            default:
                break;

        }
        // Scene이동
        OneMonthLater();
    }

    static void OneHundredDaysEvent(int input, string one, string two, string three)
    {
        if (input < 6) // 50%
        {
            Console.WriteLine(one);
            // 체력 -- , 정신력 --
        }
        else if (input < 9) // 30%
        {
            Console.WriteLine(two);
            // 체력 ++ , 정신력 ++, 돈 --
        }
        else // 20%
        {
            Console.WriteLine(three);
            // 체력 -- , 정신력 --
        }
    }
    #endregion

    #region 일병 - 사격 훈련
    // 일병 스토리 - 사격 훈련
    static void ShootingScene()
    {
        // Random 객체 생성
        Random random = new Random();

        // 사격 거리
        int[] distance = { 200, 100, 50 };
        int num = 0;

        // Wave 설정
        int totalWave = 10;
        int hitCount = 0;

        // 나레이션 Text 설정
        string narration = "\n 오늘은 사격훈련을 진행하겠다. \n \n 한발 한발 신중하게 쏠 수 있도록 한다. \n" +
            "\n 탄약을 분배 받은 사수는 각자 위치로! \n \n 준비된 사수는 사격 개시! \n \n :::아무 키나 눌러주십시오:::";
        char[] narrations = narration.ToCharArray();

        // 선택지 Text
        string[] text = { "\n 머리 조준", "\n 몸통 조준", "\n 바닥 경계선 조준" };

        // 초기 씬 셋팅값
        int cursor = 0;
        bool onScene = true;
        Console.Clear();
        
        foreach(char index in narrations)
        {
            Console.Write(index);
            Thread.Sleep(100);
        }
        Console.ReadKey();
        
        for (int currentWave = 1; currentWave <= totalWave; currentWave++)
        {
            // cursor위치 초기화
            cursor = 0;

            // Random 거리 초기화 ( 200m , 100m, 50m )
            num = random.Next(0, 3);

            // 10웨이브 반복
            while (onScene)
            {
                // 화면 초기화
                Console.Clear();

                Console.Write("\n 현재 사격 시도 : {0} / {1}   명중 횟수 : {2}", currentWave, totalWave, hitCount);
                Console.Write("\n\n");
                Console.Write("\n 사격 거리 : {0}m", distance[num]);
                Console.Write("\n\n");
                Console.Write("\n 어디를 조준하고 사격할까?\n");
                Console.Write("\n ===========================================\n");

                // Text[] Output
                TextChoice(cursor, text);
                // Key Input
                e = Console.ReadKey();
                // Cursor index
                cursor = CursorChoice(e, cursor, text, ref onScene);

            }
            // 사격 로직 및 명중 횟수++
            hitCount = ShootingEvent(num, hitCount, cursor);

            // Scene값 초기화
            onScene = true;
        }

        // hitCount(명중 횟수)에 따른 보상 로직 작성.
        // 1~5 폐급, 6~8 평균, 9~10 특등사수 
        OneMonthLater();
    }
    // Shooting 처리 메서드
    static int ShootingEvent(int input, int _hitCount, int _cursor)
    {
        if (input == _cursor)
        {
            Console.SetCursorPosition(27,4);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("==명중==");
            Console.ResetColor();
            _hitCount++;
            Console.ReadKey();
        }
        else
        {
            Console.SetCursorPosition(27, 4);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("==빗나갔다==");
            Console.ResetColor();
            Console.ReadKey();
        }

        return _hitCount;
    }
    #endregion


    //대민지원 일병스토리5
    static void DMsupport()
    {
        Console.Clear();
        Console.WriteLine("민간 지역에 큰화재가 발생했다!");
        Console.WriteLine("대민지원 활동에 참여해야겠다!");
        Console.WriteLine("");
        Console.ReadKey();
        Console.WriteLine("화재진압은 되었다고 한다! ");
        Console.WriteLine("무너진 건물 잔해가 많다고 하니 다치지 않게 하길 바란다!");
        Console.WriteLine("");
        Console.ReadKey();
        Console.Clear();

        Console.WriteLine("=======================================");
        Console.WriteLine("10번의 삽질을 시도해서 6번 성공하세요!");
        Console.WriteLine("=======================================");
        Console.WriteLine("");
        Console.ReadKey();

        Console.WriteLine("아무키나 눌러 삽질을 시작하세요");
        Console.ReadKey();



        Random random = new Random();


        while (true)
        {
            int sucessCount = 0;

            for (int i = 0; i <= 10; i++)
            {
                bool fireControlSuccess = random.Next(0, 2) == 0; // 50%확률로 성공

                if (fireControlSuccess)
                {
                    sucessCount++;
                    Console.WriteLine($"{i}. 삽질에 성공했습니다!\n");

                }
                else
                {
                    Console.WriteLine($"{i}. 삽질에 실패했습니다!\n");
                }


            }

            Console.WriteLine("======================================================");
            Console.WriteLine($"결과: 10번에 삽질 중 {sucessCount}번 성공했습니다!");
            Console.WriteLine("======================================================");
            Console.WriteLine("");
            Console.WriteLine("결과확인하기");
            Console.ReadKey();
            Console.Clear();

            if (sucessCount >= 6)
            {
                Console.WriteLine("");
                Console.WriteLine("대민지원을 완료했습니다.");
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("체력이 50증가합니다.");
                Console.ResetColor();
                player1.MaxHp += 50;
                OneMonthLater();
                break; //나가기 

            }
            else
            {
                Console.WriteLine("대민지원을 실패했습니다.");
                Console.WriteLine("");
                Console.WriteLine("다시 시도하시겠습니까? (Y)");
                Console.WriteLine("");
                Console.WriteLine("나가시겠습니까? (N)");
                string response = Console.ReadLine();
                if (response.ToUpper() == "Y")
                {
                    continue; // 실패시 다시 시작
                }
                else
                {
                    OneMonthLater();
                    break;// 나가기 (이전화면)
                }
            }
        }
    }

    // 외박(선택지) 일병스토리6
    static void Overnight()
    {
        Console.Clear();
        Console.WriteLine("");
        Console.WriteLine("첫 외박날짜가 정해졌습니다. 기대와 설렘이 가득찬 그의 마음속에는");
        Console.WriteLine("");
        Console.WriteLine("어디를 가야할지, 누구를 만나야 할지에 대한 고민으로 가득차있습니다.");
        Console.ReadKey();
        Console.Clear();
        Console.WriteLine("");
        Console.WriteLine("가족, 친구, 여자친구 세가지 선택지중 하나를 고르세요");
        Console.WriteLine("");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("선택지마다 랜덤능력치가 부여됩니다. 신중하게 고르세요! ");
        Console.ResetColor();
        Console.WriteLine("");
        Console.WriteLine("");

        bool isValidInput = true;

        while (isValidInput)
        {

            Console.WriteLine("선택지를 골라주세요! ");
            Console.WriteLine("1. 가족");
            Console.WriteLine("");
            Console.WriteLine("2. 친구");
            Console.WriteLine("");
            Console.WriteLine("3. 여자친구");

            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine(" 가족을 선택하셨습니다.");
                    Console.WriteLine("");
                    Console.WriteLine(" 가족은 당신의 안정과 지지를 의미합니다.");
                    Console.ReadKey();
                    Console.Clear();
                    Console.WriteLine(" 그들과 함께하는 시간은 당신에게 힘을 주고");
                    Console.WriteLine("");
                    Console.WriteLine(" 당신은 그들을 위해 힘든 시간을 견디려고 노력할 것입니다.");
                    Console.ReadKey();
                    Console.Clear();
                    Console.WriteLine("");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(" 힘 능력치가 10 상승하였습니다.");
                    Console.ResetColor();
                    player1.Str += 10;
                    isValidInput = false;
                    OneMonthLater();
                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine("");
                    Console.WriteLine(" 당신은 친구를 선택했습니다.");
                    Console.WriteLine("");
                    Console.WriteLine(" 김밥천국가서 대충먹고 PC방가서 날밤을 깠습니다.");
                    Console.ReadKey();
                    Console.Clear();
                    Console.WriteLine("");

                    Console.WriteLine(" 친구들과 함께하는 시간은 즐거웠습니다.");
                    Console.ReadKey();
                    Console.Clear();
                    Console.WriteLine("");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(" 정신력 능력치가 10 상승하였습니다.");
                    player1.Mind += 10;
                    isValidInput = false;
                    OneMonthLater();
                    break;
                case "3":
                    Console.Clear();
                    Console.WriteLine(" 여자친구를 선택하셨습니다.");
                    Console.WriteLine("");
                    Console.WriteLine(" 그녀에게 전화 했습니다. 전화를 안받습니다...");
                    Console.ReadKey();
                    Console.Clear();
                    Console.WriteLine(" 다시 한번 전화를 걸었습니다...");
                    Console.WriteLine("");
                    Console.ReadKey();
                    Console.WriteLine(" 전화를 받았습니다!!");
                    Console.ReadKey();
                    Console.Clear();
                    Console.WriteLine("");
                    Console.WriteLine(" 나 남자친구 생겼어 이제 전화하지말아줬으면 좋겠어 미안 툭 뚜..뚜..뚜..뚜");
                    Console.ReadKey();
                    Console.WriteLine("");
                    Console.WriteLine(" 이로 인해 당신은 좌절하고 실망하며 슬픔을 겪게 됩니다.");
                    Console.WriteLine("");
                    Console.ReadKey();
                    Console.Clear();
                    Console.WriteLine("");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(" 정신력 능력치가 10 하락하였습니다.");
                    Console.ResetColor();
                    player1.Mind -= 10;
                    isValidInput = false;
                    OneMonthLater();
                    break;

                default:
                    Console.WriteLine("");
                    Console.WriteLine(" 잘못된 선택입니다.");
                    break;
            }
        }
    }


    #region 상병 스토리
    //상병 스토리 - KCTC
    static void CStoryKCTC(Character player)
    {
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine(" 당신은 KCTC 훈련에 참여했다.");
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine(" 계속하려면 enter.");
        Console.ResetColor();
        Console.ReadKey();
        if (player.Job == "포병")
        {
            ArtilleryKCTC();
        }
        else if (player.Job == "보병" || player.Job == "정비병")
        {
            InfantryKCTC();
        }
        else if (player.Job == "운전병")
        {
            TransportationKCTC();
        }


        Console.WriteLine(" 막사로 돌아가자.");
        Console.ReadKey();
        Console.WriteLine(" 개고생을 했더니 체력이 늘어난것 같다.");
        player.Hp += 20;
        player.MaxHp += 20;
        Console.ReadKey();
        OneMonthLater();
    }

    //kctc 포병전투 씬
    static void ArtilleryKCTC()
    {
        Console.WriteLine(" 전방 부대에서 포격 지원을 요청했다.");
        Console.ReadKey();
        Console.WriteLine(" 관측병이 따온 적 좌표로 방열한다.");
        Console.ReadKey();
        Console.WriteLine(" 잠시 후 무전으로 새로운 좌표가 들어온다");
        Console.ReadKey();
        Console.WriteLine(" ...");
        Console.ReadKey();
        Console.WriteLine(" 미친듯이 포만 쏘다가 훈련이 끝났다.");
        Console.ReadKey();

    }
    //kctc 보병전투 씬
    static void InfantryKCTC()
    {
        Console.WriteLine(" 두돈 반을 타고 전선으로 투입되었다.");
        Console.ReadKey();
        Console.WriteLine(" 진지를 구축하고 참호에서 대기했다.");
        Console.ReadKey();
        Console.WriteLine(" 2일 후 새벽에 포격을 맞고 전멸했다.");
        Console.ReadKey();
        Console.WriteLine(" 의무대에서 대기하다 훈련이 끝났다.");
    }
    //kctc 운전병전투 씬
    static void TransportationKCTC()
    {
        Console.WriteLine(" 두돈반을 운전해서 병사들을 수송한다.");
        Console.ReadKey();
        Console.WriteLine(" 다시 두돈반을 타고 병사들을 수송한다.");
        Console.ReadKey();
        Console.WriteLine(" 다음날 보급품을 수송한다.");
        Console.ReadKey();
        Console.WriteLine(" ....");
        Console.ReadKey();
        Console.WriteLine(" 훈련이 끝났다.");
    }



    //상병 스토리- 상검
    static void CStoryPhysicalExamination(Character player)
    {
        int cursor = 0;
        bool onScene = true;

        Console.Clear();
        Console.WriteLine(" 상병 신검 날이 되었다.");
        Console.ReadKey();
        Console.WriteLine(" 국군 병원으로 가는 버스에 탔다.");
        Console.ReadKey();
        Console.WriteLine(" ...");
        Console.ReadKey();
        Console.WriteLine(" 병원에 도착했다.");
        Console.ReadKey();
        Console.WriteLine(" 잠시 대기 후 신체검사를 시작했다.");
        Console.ReadKey();
        Console.WriteLine(" ...");
        Console.ReadKey();
        Console.WriteLine("========================");
        Console.WriteLine($"   이름: {player.Name}");
        Console.WriteLine("   체중: 70.3kg");
        Console.WriteLine("   키: 175.4cm");
        Console.WriteLine("   ...");
        Console.WriteLine("========================");
        Console.ReadKey();

        // 화면 초기화
        Console.Clear();

        string[] text = { " 1. 몰래 탈출해 치킨을 먹는다.", " 2. 얌전히 부대로 가서 짬밥을 먹는다." };
        double successPercent = (double)player.Dex / (5 + player.Dex) * 100;
        int showPercent = (int)successPercent;
        while (onScene)
        {
            Console.Clear();
            Console.WriteLine(" 상검이 끝나고 국군병원 근처에서 몰래 치킨을 먹으려 한다.");

            Console.WriteLine($" 몰래 빠져나가볼까?(성공확률 {showPercent}%)");
            Console.ForegroundColor = ConsoleColor.DarkRed;

            Console.ResetColor();

            // Text[] Output
            TextChoice(cursor, text);
            // Key Input
            e = Console.ReadKey();
            // Cursor Index
            cursor = CursorChoice(e, cursor, text, ref onScene);
        }

        int a = player.Dex;
        Random rand = new Random();
        int chicken = rand.Next(5 + a);
        switch (cursor)
        {
            case 0:
                //치킨시도
                switch (chicken)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        //실패
                        Console.WriteLine(" 실패!");
                        Console.ReadKey();
                        Console.WriteLine(" 간부에게 죽도록 털렸다.");
                        Console.ReadKey();
                        Console.WriteLine(" 정신력이 감소했다.");
                        Console.ReadKey();
                        Console.WriteLine(" 체력이 감소했다.");
                        Console.ReadKey();

                        //정신력 1 감소, 체력 감소
                        player1.Mind--;
                        player1.Hp -= 10;
                        player1.MaxHp -= 10;


                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine(" 계속하려면 enter.");
                        Console.ResetColor();
                        Console.ReadKey();
                        OneMonthLater();
                        break;

                    default:
                        //성공 
                        Console.WriteLine(" 성공!");
                        Console.ReadKey();
                        Console.WriteLine(" 맛있는 치킨을 먹었다.");
                        Console.ReadKey();
                        Console.WriteLine(" 정신력이 증가한다.");
                        Console.ReadKey();
                        Console.WriteLine(" 체력이 증가되었다.");
                        //정신력 5 증가 체력 증가
                        player1.Mind += 5;
                        player1.Hp += 10;
                        player1.MaxHp += 10;
                        player1.Gold -= 100;

                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine(" 계속하려면 enter.");
                        Console.ResetColor();
                        Console.ReadKey();

                        OneMonthLater();
                        break;
                }
                break;

            case 1:
                //부대에서 짬밥
                Console.WriteLine(" 얌전히 부대로 복귀한다.");
                Console.ReadKey();
                Console.WriteLine(" 맛없는 똥국이다...");
                Console.ReadKey();
                Console.WriteLine(" 정신력이 감소했다.");
                Console.ReadKey();
                Console.WriteLine(" 체력이 증가되었다.");
                player1.Mind--;
                player1.Hp += 10;
                player1.MaxHp += 10;


                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(" 계속하려면 enter.");
                Console.ResetColor();
                Console.ReadKey();

                OneMonthLater();
                break;

            default:
                break;
        }
    }

    //상병 스토리-전준태
    static void CSDefcon(Character player)
    {
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine(" 웨에에에엥-");
        Console.ReadKey();
        Console.WriteLine(" 화스트 페이스. 화스트 페이스.");
        Console.ReadKey();
        Console.WriteLine(" 타다다닥-");
        Console.ReadKey();
        Console.WriteLine(" 생활관으로 달려가서 개인 군장을 챙기고 물자를 챙긴다.");
        Console.ReadKey();
        Console.WriteLine(" 물자를 챙기는 와중에 팔이 뻐근함을 느낀다.");
        Console.ReadKey();

        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine(" 힘 10 이상이면 성공"); //수정가능
        Console.WriteLine(" 계속하려면 enter.");
        Console.ResetColor();

        if (player1.Str >= 10)
        {
            Console.WriteLine(" 아슬아슬했지만 안정적으로 물자를 다 옮기는데 성공했다.");
            Console.ReadKey();
            Console.WriteLine(" 반복된 노동으로 체력이 10 오른다.");
            player.Hp += 10;
            player.MaxHp += 10;

            OneMonthLater();
        }
        else
        {
            Console.WriteLine(" 물자를 옮기던 와중 쏟아버렸다.");
            Console.ReadKey();
            Console.WriteLine(" 간부의 엄청난 쿠사리가 쏟아진다.");
            Console.ReadKey();
            Console.WriteLine(" 정신력이 3 감소한다.");
            player.Mind -= 3;

            OneMonthLater();
        }
    }
    //상병 스토리 - 대침투 훈련
    static void CSTest()
    {

        Console.Clear();
        Console.WriteLine();
        Console.WriteLine(" 오늘은 대침투 훈련을 한다.");
        Console.ReadKey();
        Console.WriteLine(" 부대 근처 야산으로 가서 총을 거치 하고 참호를 파기 시작한다");
        Console.ReadKey();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(" (성공적으로 참호를 파면 거수자를 잡을 확률이 올라감) ");
        Console.ResetColor();

        //땅파기
        French(french);
        Console.WriteLine(" \"거수자 발견시 보고하고. 알지? 위장한 간부 잡으면 포상인거?\"");
        Console.ReadKey();
        Console.WriteLine(" 의욕이 셈솟기 시작한다.");
        Console.ReadKey();
        Console.WriteLine(" 하염없이 숨어있으니, 길 너머 풀숲에서 부스럭 거리는 소리가 들린다.");
        Console.ReadKey();
        Console.WriteLine(" 조심스럽게 접근한다.");
        Console.ReadKey();
        Random rand = new Random();
        int plusPoint;
        if (frenchSuccess == false)
        {
            plusPoint = 0;
        }
        else
        {
            plusPoint = 1;
        }
        int attack = rand.Next(2 + plusPoint);
        switch (attack)
        {
            case 1:
                Console.WriteLine(" 거수자가 눈치채고 도망갔다!");
                Console.ReadKey();
                Console.WriteLine(" 기습실패");
                Console.ReadKey();
                Console.WriteLine(" 눈앞에서 포상이 날아갔다...");
                OneMonthLater();
                break;

            default:
                Console.WriteLine(" 기습성공");
                Console.ReadKey();
                Console.WriteLine(" 안정적으로 거수자를 제압했다.");
                Console.ReadKey();
                Console.WriteLine(" 포상금 100원을 받았다.");
                player1._gold += 100;
                OneMonthLater();

                break;
        }


    }
    //참호 파기
    static void French(Enemy enemy)
    {
        //판 깊이. = 100cm - 적 체력
        //남은 깊이 = 적 체력

        int depth = (100 - enemy.EnemyHp) / 10;

        Console.Clear();
        Console.WriteLine();
        Console.Write("  남은 깊이: ");
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine($"  {enemy.EnemyHp} cm");
        Console.ResetColor();
        Console.WriteLine($"  남은 기회: {enemy.EnemyAtk}");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("=땅===------------------=====");

        for (int i = 0; i <= depth; i++)
        {
            Console.WriteLine("     =                  =    ");
        }
        Console.WriteLine("     ====================");
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine(" <<삽질하려면 ENTER>>");
        Console.WriteLine();
        Console.ReadKey();

        if (enemy.EnemyHp > 0)
        {
            if (enemy.EnemyAtk > 0)
            {
                enemy.EnemyHp -= player1.Str;
                if (enemy.EnemyHp < 0)
                {
                    Console.WriteLine($" 땅을{player1.Str + enemy.EnemyHp}cm 만큼 팠습니다.");
                    enemy.EnemyHp = 0;
                }
                else
                {
                    Console.WriteLine($" 땅을 {player1.Str}cm 만큼 팠습니다.");
                }

                Console.ReadKey();
                enemy.EnemyAtk--;
                French(french);
            }
            else
            {
                Console.WriteLine(" 제한 시간 내에 땅을 다 못팠다.");
                frenchSuccess = false;
            }
        }
        else
        {
            Console.WriteLine(" 땅파기 끝");
            frenchSuccess = true;
        }

    }



    //상병 스토리- 분대장 교육
    static void CSschool()
    {
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine(" 어쩌다보니 분대장으로 뽑혔다.");
        Console.ReadKey();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(" 분대장 교육대로 이동중...");
        Console.ResetColor();
        Console.ReadKey();
        Console.WriteLine(" 분대장 교육대에서 받은 성적을 통해 추가 보상이 있을 예정이다.");
        Console.ReadKey();
        Console.WriteLine(" 시간이 흘러 분대장 교육이 끝났다.");
        Console.ReadKey();
        Console.WriteLine(" 당신의 성적은...?");
        Console.ReadKey();
        Random rand = new Random();
        int number = rand.Next(10);
        switch (number)
        {
            case 0:
                Console.WriteLine(" 1등!");
                Console.ReadKey();
                Console.WriteLine(" 축하합니다! 당신은 우수한 성적으로 교육을 수료하셨습니다.");
                Console.WriteLine(" 스탯이 전체적으로 증가합니다.");
                player1.IQ += 10;
                player1.Str += 10;
                player1.Dex += 10;
                player1.Luk += 10;
                break;
            case 1:
                Console.WriteLine(" 2등!");
                Console.ReadKey();
                Console.WriteLine(" 축하합니다! 당신은 우수한 성적으로 교육을 수료하셨습니다.");
                Console.WriteLine(" 스탯이 전체적으로 증가합니다.");
                player1.IQ += 9;
                player1.Str += 9;
                player1.Dex += 9;
                player1.Luk += 9;
                break;
            case 2:
                Console.WriteLine(" 3등!");
                Console.ReadKey();
                Console.WriteLine(" 축하합니다! 당신은 우수한 성적으로 교육을 수료하셨습니다.");
                Console.WriteLine(" 스탯이 전체적으로 증가합니다.");
                player1.IQ += 8;
                player1.Str += 8;
                player1.Dex += 8;
                player1.Luk += 8;
                break;
            case 3:
                Console.WriteLine(" 4등!");
                Console.ReadKey();
                Console.WriteLine(" 축하합니다! 당신은 우수한 성적으로 교육을 수료하셨습니다.");
                Console.WriteLine(" 스탯이 전체적으로 증가합니다.");
                player1.IQ += 7;
                player1.Str += 7;
                player1.Dex += 7;
                player1.Luk += 7;
                break;
            case 4:
                Console.WriteLine(" 5등!");
                Console.ReadKey();
                Console.WriteLine(" 축하합니다! 당신은 우수한 성적으로 교육을 수료하셨습니다.");
                Console.WriteLine(" 스탯이 전체적으로 증가합니다.");
                player1.IQ += 6;
                player1.Str += 6;
                player1.Dex += 6;
                player1.Luk += 6;
                break;
            case 5:
                Console.WriteLine(" 6등!");
                Console.ReadKey();
                Console.WriteLine(" 축하합니다! 당신은 교육을 수료하셨습니다.");
                Console.WriteLine(" 스탯이 전체적으로 증가합니다.");
                player1.IQ += 5;
                player1.Str += 5;
                player1.Dex += 5;
                player1.Luk += 5;
                break;
            case 6:
                Console.WriteLine(" 7등!");
                Console.ReadKey();
                Console.WriteLine(" 축하합니다! 당신은 교육을 수료하셨습니다.");
                Console.WriteLine(" 스탯이 전체적으로 증가합니다.");
                player1.IQ += 4;
                player1.Str += 4;
                player1.Dex += 4;
                player1.Luk += 4;
                break;
            case 7:
                Console.WriteLine(" 8등!");
                Console.ReadKey();
                Console.WriteLine(" 축하합니다! 당신은 교육을 수료하셨습니다.");
                Console.WriteLine(" 스탯이 전체적으로 증가합니다.");
                player1.IQ += 3;
                player1.Str += 3;
                player1.Dex += 3;
                player1.Luk += 3;
                break;
            case 8:
                Console.WriteLine(" 9등!");
                Console.ReadKey();
                Console.WriteLine(" 축하합니다! 당신은 교육을 수료하셨습니다.");
                Console.WriteLine(" 스탯이 전체적으로 증가합니다.");
                player1.IQ += 2;
                player1.Str += 2;
                player1.Dex += 2;
                player1.Luk += 2;
                break;
            case 9:
                Console.WriteLine(" 10등!");
                Console.ReadKey();
                Console.WriteLine(" 축하합니다! 교육을 수료하셨습니다.");
                Console.WriteLine(" 스탯이 전체적으로 증가합니다.");
                player1.IQ += 1;
                player1.Str += 1;
                player1.Dex += 1;
                player1.Luk += 1;
                break;
        }
        Console.ReadKey();
        Console.WriteLine(" 당신은 녹견을 획득했다.");
        player1.AddToInventoryArmor(greenStrap);
        //녹견 스탯 적용
        greenStrap.isEquipped = true;
        player1.Mind += greenStrap.ItemMind;
        player1.Hp += greenStrap.ItemHp;
        player1.MaxHp += greenStrap.ItemHp;
        Console.ReadKey();
        Console.WriteLine(" 교육이 끝나고 막사로 복귀했다.");

        OneMonthLater();


    }
    //상병 스토리- 보스몹 초임 소대장
    static void CSNewCommander(Character player, Enemy enemy)
    {
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine(" 오늘은 새로운 소위가 임관하는 날이다.");
        Console.ReadKey();
        Console.WriteLine(" 관상부터 FM인게 보인다.");
        Console.WriteLine(" ㅈ된듯 하다.");
        Console.ReadKey();
        Console.WriteLine(" 얼마 후...");
        Console.ReadKey();
        Console.WriteLine($" {enemy.EnemyName}: 이봐 {player.Name} 상병. ");
        Console.ReadKey();
        Console.WriteLine($" 상병 {player.Name}. 무슨일이십니까? ");
        Console.ReadKey();
        Console.WriteLine($" {enemy.EnemyName}: 배수로 작업 하러 가지.");
        Console.ReadKey();
        Console.WriteLine(" 얼마 후...");
        Console.WriteLine(" 얼마 후...");
        Console.WriteLine(" 얼마 후...");
        Console.WriteLine(" 얼마 후...");
        Console.WriteLine(" 얼마 후...");
        Console.WriteLine(" 전투시작");
        CSCommanderAttack(player1, newCommander);

    }
    static void CSCommanderAttack(Character player, Enemy enemy)
    {
        if (player1.Hp > 0 && enemy.EnemyHp > 0)
        {
            Console.Clear();
            Console.WriteLine("================================");
            Console.Write(" 소대장의 체력: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{enemy.EnemyHp}");
            Console.ResetColor();
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($" {player.Name}의 체력: {player.Hp}");
            Console.ResetColor();
            Console.WriteLine("================================");
            Console.WriteLine(" 이번턴에 하실 행동을 골라주세요");

            Console.WriteLine("");
            Console.WriteLine(" 1. 일반 공격하기");
            Console.WriteLine(" 2. 스킬 사용하기");
            Console.WriteLine("");

            int input = CheckValidInput(1, 2);
            if (input == 1)
            {
                Random rand = new Random();
                int number = rand.Next(player.Luk);
                if (number <= 5) //따로 추가 스탯 없을경우 평타
                {
                    enemy.EnemyHp -= player.Str;
                    Console.WriteLine(" 당신의 공격!");
                    Thread.Sleep(500);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($" {player.Str}");
                    Console.ResetColor();
                    Console.WriteLine("의 데미지를 주었다.");
                    Thread.Sleep(500);
                    Console.WriteLine($" {enemy.EnemyName}의 공격!");
                    Console.WriteLine($" {enemy.EnemyAtk}만큼의 데미지를 입었다.");
                    player.Hp -= enemy.EnemyAtk;
                    Console.ReadKey();
                    if (newCommander.EnemyHp <= 0)
                    {
                        CSCommanderDead();
                    }
                    CSCommanderAttack(player1, newCommander);


                }

                else //치명타 (luk의 추가 스탯이 많을 수록 확률이 올라감)
                {
                    enemy.EnemyHp -= player.Str + player.Luk;
                    Console.WriteLine(" 당신의 공격");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(" 치명타!");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(" {0}", player1.Str + player.Luk);
                    Console.ResetColor();
                    Console.WriteLine("만큼의 데미지를 주었다.");
                    Thread.Sleep(500);
                    Console.WriteLine($" {enemy.EnemyName}의 공격!");
                    Console.WriteLine($" {enemy.EnemyAtk}만큼의 데미지를 입었다.");
                    player.Hp -= enemy.EnemyAtk;
                    Console.WriteLine();
                    Console.ReadKey();
                    if (newCommander.EnemyHp <= 0)
                    {
                        CSCommanderDead();
                    }
                    CSCommanderAttack(player1, newCommander);
                }

            }
            else//스킬사용하기
            {
                Console.WriteLine(" 스킬을 사용했다.");
                //스킬
                CSCommanderAttack(player1, newCommander);
            }
        }

    }

    static void CSCommanderDead()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(" 소대장을 쓰러트렸다.");
        Console.ResetColor();
        OneMonthLater();
    }
    #endregion




    #region Cursor선택 캡슐화
    // Cursor선택 메서드
    static int CursorChoice(ConsoleKeyInfo e, int _cursor, string[] _text, ref bool _onScene)


    {
        switch (e.Key)
        {
            case ConsoleKey.UpArrow:
                _cursor--;
                if (_cursor < 0) _cursor = _text.Length - 1;
                break;
            case ConsoleKey.DownArrow:
                _cursor++;
                if (_cursor > _text.Length - 1) _cursor = 0;
                break;
            case ConsoleKey.Enter:
                _onScene = false;
                break;
            default:
                break;
        }
        return _cursor;
    }





    #endregion

    #region Text 선택지 출력 캡슐화
    // Text 출력 캡슐화
    static void TextChoice(int _cursor, string[] _text)
    {
        for (int i = 0; i < _text.Length; i++)
        {
            if (_cursor == i) Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(_text[i]);
            Console.ResetColor();
        }
    }
    #endregion

    #region 선입력 방지
    static void InputPrevention()
    {
        bool inputPrevent = true;

        while (inputPrevent)
        {
            e = Console.ReadKey(true);
            switch (e.Key)
            {
                case ConsoleKey.Tab:
                    inputPrevent = false;
                    break;
                default:
                    break;
            }
        }
    }
    #endregion





    #region 혹한기
    static void ColdWeatherTraining1()
    {
        int cursor = 0;
        bool onScene = true;

        string[] text = { "1. px에서 미리 사두자", "2. 근들갑이야 다 해~" };

        while (onScene)
        {
            Console.Clear();
            Console.WriteLine("혹한기 훈련이 시작되었다.");
            Console.WriteLine();
            Console.WriteLine("혹한기 일정동안 px는 잠시 폐쇠한다고 한다.");
            Console.WriteLine();
            TextChoice(cursor, text);
            e = Console.ReadKey();
            cursor = CursorChoice(e, cursor, text, ref onScene);
        }
        switch (cursor)
        {
            case 0:
                if (player1.Gold > 2000)
                {
                    player1.Gold -= 2000;
                    player1.MaxHp += 100;
                    player1.Str += 50;
                    player1.Dex += 50;
                    Console.Clear();
                    Console.WriteLine("후임에게 px에서 핫팩과 먹을 것을 사오라고 했다.");
                    Console.ReadKey();
                    Console.WriteLine("뭘 그렇게 많이 샀는지 2000Gold가 나갔다.");
                    Console.ReadKey();
                    Console.WriteLine();
                    Console.WriteLine($"잔여 골드 : {player1.Gold - 2000}");
                    Console.ReadKey();
                    ColdWeatherTraining2();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("px에서 사려고 했는데 골드가 없다.");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("평소에 돈좀 아낄껄 그랬다.");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine();
                    Console.WriteLine($"잔여 골드 : {player1.Gold}");
                    Console.ReadKey();
                    ColdWeatherTraining2();
                }
                break;
            case 1:
                    ColdWeatherTraining2();
                break;
            default:
                break;
        }
    }
    static void ColdWeatherTraining2()
    {
        int cursor = 0;
        bool onScene = true;

        string[] text = { "1. 경계근무라고 거짓말한 뒤 작업에서 빠진다.", "2. 간부님들도 많은데 내가 빠지기엔 눈치 보인다." };
        while (onScene)
        {
            Console.Clear();
            Console.WriteLine("혹한기 훈련이 시작되었다.");
            Console.WriteLine();
            Console.WriteLine("우리 중대는 구연병장에 지휘소와 철조망을 설치하는 것이다.");
            Console.WriteLine();
            Console.WriteLine("중대원이 총출동하는데 나 하나 빠져도 모르겠지?");
            Console.WriteLine();

            Console.WriteLine();
            TextChoice(cursor, text);
            e = Console.ReadKey();
            cursor = CursorChoice(e, cursor, text, ref onScene);
        }
        switch (cursor)
        {
            case 0:
                bool eventOccurred = EventOccur(player1.CalculateProbability(player1.Dex));
                if (eventOccurred)
                {
                    Console.Clear();
                    Console.WriteLine("탄약고 경계근무인척 생활관으로 도망쳤다.");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("장구류를 벗고 쉴려고 하는데 발자국 소리가 들린다.");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("행보관님이 었다.!!");
                    Console.ReadKey();
                    Console.WriteLine();
                    Console.WriteLine("press any Key to continue");
                    Console.ReadKey();
                    NCOBattle(player1, masterSergent);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("개꿀 일과 빼먹었다.");
                    Console.ReadKey();
                    Console.WriteLine("press and key to continue");
                    Console.ReadKey();
                    ColdWeatherTraining4();
                }
                break;
            case 1:
                ColdWeatherTraining3();
                break;
            default:
                break;
        }
    }

    static void NCOBattle(Character player, Enemy enemy)
    {
        int cursor = 0;
        bool onScene = true;
        string[] text = { "일반 공격", "스킬 공격", "회피" };
        while (onScene)
        {
            if (player.Hp > 0 && enemy.EnemyHp > 0)
            {

                Console.Clear();
                Console.WriteLine($"{enemy.EnemyName}");
                Console.WriteLine($"남은 체력 : {enemy.EnemyHp}");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine($"{player.Name}");
                Console.WriteLine($"남은 체력 : {player.Hp}");
                Console.WriteLine();
            }
            else if (player.Hp <= 0)
            {
                Console.Clear();
                Console.WriteLine("행보관님한테 져버렸다.");
                Console.WriteLine();
                Console.ReadKey();
                Console.WriteLine("정신을 차리니 모르는 천장이 있다.");
                Console.WriteLine();
                Console.ReadKey();
                Console.WriteLine("의무대에 누워있구나");
                Console.WriteLine();
                Console.ReadKey();
                Console.WriteLine("press any Key to continue");
                Console.ReadKey();
                turn = 0;
                Home();
            }
            else
            {
                Console.Clear();
                Console.Write("행보관님을 쓰러뜨렸다.");
                Console.WriteLine();
                Console.ReadKey();
                Console.Write("나머지 훈련 열외를 받아냈다.");
                Console.WriteLine();
                Console.ReadKey();
                Console.WriteLine("press any Key to continue");
                Console.ReadKey();
                turn = 0;
                Home();
            }
            TextChoice(cursor, text);
            e = Console.ReadKey();
            cursor = CursorChoice(e, cursor, text, ref onScene);
        }
        switch (cursor)
        {
            case 0:
                bool eventOccurred = EventOccur(player1.CalculateProbability(player1.Luk));
                if (eventOccurred)
                {
                    Console.Clear();
                    turn++;
                    Console.WriteLine($"{enemy.EnemyName}");
                    Console.WriteLine($"남은 체력 : {enemy.EnemyHp}");
                    Console.ReadKey();
                    Console.WriteLine();
                    Console.WriteLine("일반 공격!!");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("치명타가 들어갔다.");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine($"{player1.Str * 2}의 데미지");
                    Console.WriteLine();
                    enemy.EnemyHp -= player1.Str * 2;
                    Console.ReadKey();
                    Console.WriteLine("press any Key to continue");
                    Console.ReadKey();
                    NCOTurn(turn);
                }
                else
                {
                    Console.Clear();

                    turn++;
                    Console.WriteLine($"{enemy.EnemyName}");
                    Console.WriteLine($"현재 체력 : {enemy.EnemyHp}");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("일반 공격!!");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("평범한 공격이었다.");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine($"{player1.Str}의 데미지");
                    Console.WriteLine();
                    enemy.EnemyHp -= player1.Str;
                    Console.ReadKey();
                    Console.WriteLine($"남은 체력 : {enemy.EnemyHp}");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("press any Key to continue");
                    Console.ReadKey();
                    NCOTurn(turn);
                }
                break;
            case 1:
                bool _eventOccurred = EventOccur(player1.CalculateProbability(player1.Luk));
                if (_eventOccurred)
                {
                    Console.Clear();
                    enemy.EnemyHp -= player1.Str * 4;
                    turn++;
                    Console.WriteLine($"{enemy.EnemyName}");
                    Console.WriteLine($"남은 체력 : {enemy.EnemyHp}");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("스킬 공격!!");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("치명타가 들어갔다.");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("사실 미구현 ㅈㅅ ㅎㅎ;; 그냥 일반 공격 2배 데미지");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine($"{player1.Str * 2}의 데미지");
                    enemy.EnemyHp -= player1.Str * 2;
                    Console.ReadKey();
                    Console.WriteLine($"남은 체력 : {enemy.EnemyHp}");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("press any Key to continue");
                    Console.ReadKey();
                    NCOTurn(turn);
                }
                else
                {
                    Console.Clear();
                    enemy.EnemyHp -= player1.Str * 2;
                    turn++;
                    Console.WriteLine($"{enemy.EnemyName}");
                    Console.WriteLine($"현재 체력 : {enemy.EnemyHp}");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("스킬 공격!!");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("평범한 공격이었다.");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("사실 미구현 ㅈㅅ ㅎㅎ;; 그냥 일반 공격 2배 데미지");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine($"{player1.Str}의 데미지");
                    enemy.EnemyHp -= player1.Str;
                    Console.ReadKey();
                    Console.WriteLine($"남은 체력 : {enemy.EnemyHp}");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("press any Key to continue");
                    Console.ReadKey();
                    NCOTurn(turn);
                }
                break;
            case 2:
                bool __eventOccurred = EventOccur(player1.CalculateProbability(player1.Dex));
                if (__eventOccurred)
                {
                    Console.Clear();
                    turn = -1;
                    Console.WriteLine($"{enemy.EnemyName}");
                    Console.WriteLine($"현재 체력 : {enemy.EnemyHp}");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("회피!!");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("회피에 성공했다!!");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("행보관님이 공격의 반동으로 고통스러워 한다.");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine($"{(int)(player1.Str / 2)}의 데미지");
                    Console.WriteLine();
                    enemy.EnemyHp -= (int)(player1.Str / 2);
                    Console.ReadKey();
                    Console.WriteLine($"남은 체력 : {enemy.EnemyHp}");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("행보관님이 2턴 그로기 상태에 빠짐");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("press any Key to continue");
                    Console.ReadKey();
                    NCOTurn(turn);
                }
                else
                {
                    Console.Clear();
                    turn++;
                    Console.WriteLine($"{enemy.EnemyName}");
                    Console.WriteLine($"현재 체력 : {enemy.EnemyHp}");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("회피!!");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("회피에 실패했다.");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("상대 턴으로 넘어갑니다.");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("press any Key to continue");
                    Console.ReadKey();
                    NCOTurn(turn);
                }
                break;
            default:
                break;
        }
    }
    static void NCOTurn(int Value)
    {
        if (Value > 0)
        {
            bool eventOccurred = EventOccur(player1.CalculateProbability(player1.Dex));
            if (eventOccurred)
            {
                Console.Clear();
                Console.WriteLine($"{player1.Name}");
                Console.WriteLine($"현재 체력 : {player1.Hp}");
                Console.WriteLine();
                Console.ReadKey();
                Console.WriteLine("행보관님이 오른손을 들어올렸다.");
                Console.WriteLine();
                Console.ReadKey();
                Console.WriteLine("익숙한 공격이군 타이밍에 맞춰 막는다!");
                Console.WriteLine();
                Console.ReadKey();
                Console.WriteLine($"{(int)(masterSergent.EnemyAtk / 2)}의 데미지");
                Console.WriteLine();
                player1.Hp -= (int)(masterSergent.EnemyAtk / 2);
                Console.ReadKey();
                Console.WriteLine($"남은 체력 : {player1.Hp}");
                Console.WriteLine();
                Console.ReadKey();
                Console.WriteLine("press any Key to continue");
                Console.ReadKey();
                NCOBattle(player1, masterSergent);
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"{player1.Name}");
                Console.WriteLine($"현재 체력 : {player1.Hp}");
                Console.WriteLine();
                Console.ReadKey();
                Console.WriteLine("행보관님이 오른손을 들어올렸다.");
                Console.WriteLine();
                Console.ReadKey();
                Console.WriteLine("익숙한 공격이군 타이밍에 맞춰 막는다!");
                Console.WriteLine();
                Console.ReadKey();
                Console.WriteLine("행보관님의 왼손이 나의 복부에 들왔다.");
                Console.WriteLine();
                Console.ReadKey();
                Console.WriteLine($"{(int)(masterSergent.EnemyAtk)}의 데미지");
                Console.WriteLine();
                player1.Hp -= (int)(masterSergent.EnemyAtk);
                Console.ReadKey();
                Console.WriteLine($"남은 체력 : {player1.Hp}");
                Console.WriteLine();
                Console.ReadKey();
                Console.WriteLine("press any Key to continue");
                Console.ReadKey();
                NCOBattle(player1, masterSergent);
            }
        }
        else
        {
            Console.Clear();
            Console.WriteLine("행보관님이 그로기에 걸려 허우적대고 있다..");
            Console.WriteLine();
            Console.ReadKey();
            Console.WriteLine("press any Key to continue");
            Console.ReadKey();
            NCOBattle(player1, masterSergent);
        }
    }
    static void ColdWeatherTraining3()
    {
        int cursor = 0;
        bool onScene = true;

        string[] text = { "1. 행보관님을 도와 바위를 깬다.", "2. 소대장님과 같이 철조망을 친다." };

        while (onScene)
        {
            Console.Clear();
            Console.WriteLine("행보관님이 곡괭이를 들고 땅을 내리치고 있다.");
            Console.WriteLine();
            Console.WriteLine("옆에선 소대장님이 철조망을 치려고 병사들을 부르고 있다.");
            Console.WriteLine();
            Console.WriteLine("누굴 도와야 할까");
            Console.WriteLine();
            TextChoice(cursor, text);
            e = Console.ReadKey();
            cursor = CursorChoice(e, cursor, text, ref onScene);
        }
        switch (cursor)
        {
            case 0:
                ColdWeatherTrainingNCORoot();
                break;
            case 1:
                ColdWeatherTrainingNCORoot();
                break;
            default:
                break;
        }
    }

    static void ColdWeatherTrainingNCORoot()
    {
        int cursor = 0;
        bool onScene = true;

        string[] text = { "1. 이 바위까지만 제가 깨겠습니다.", "2. 행보관님께 바로 곡괭이를 드린다." };

        while (onScene)
        {
            Console.Clear();
            Console.WriteLine("행보관님께서 손 다친다며 장갑을 주셨고 곡괭이를 넘겨 받아 바위를 깨기 시작했다.");
            Console.WriteLine();
            Console.WriteLine("곡괭이질 몇번하니 힘이 빠지기 시작했다. 내가 힘이 빠지는게 보이자");
            Console.WriteLine();
            Console.WriteLine("행보관님께서 다시 교대를 하자고 하신다.");
            Console.WriteLine();
            Console.WriteLine();
            TextChoice(cursor, text);
            e = Console.ReadKey();
            cursor = CursorChoice(e, cursor, text, ref onScene);
        }
        switch (cursor)
        {
            case 0:
                Console.Clear();
                Console.WriteLine("내가 곡괭이질 하는 동안 행보관님께서 막사에 다녀오셨다.");
                Console.ReadKey();
                Console.WriteLine("막사에 다녀온 행보관님께서 착암기를 가져오셨다.");
                Console.ReadKey();
                Console.WriteLine("역시 행보관님이야");
                Console.ReadKey();
                Console.WriteLine();
                Console.WriteLine("press and key to continue");
                Console.ReadKey();
                Home();
                break;
            case 1:
                Console.Clear();
                Console.WriteLine("행보관님과 번갈아 곡괭이 질을 하기 시작했다.");
                Console.ReadKey();
                Console.WriteLine("오랜 작업으로 힘이 많이 빠졌다.");
                Console.ReadKey();
                Console.WriteLine("소대장님한테 붙을걸 그랬나.");
                Console.ReadKey();
                Console.WriteLine();
                Console.WriteLine("press and key to continue");
                Console.ReadKey();
                Home();
                break;
            default:
                break;
        }
    }
    static void ColdWeatherTrainingCORoot()
    {
        int cursor = 0;
        bool onScene = true;

        string[] text = { "가만히 지켜본다.", "소대장님을 도와준다." };

        while (onScene)
        {
            Console.Clear();
            Console.WriteLine("여기가 사람이 많아서 더 쉬워보인다.");
            Console.WriteLine();
            Console.WriteLine("2단3열 윤형 철조망을 쳐야한다.");
            Console.WriteLine();
            Console.WriteLine("소대장님이 일병들과 철조망 작업을 하고 있다..");
            Console.WriteLine();
            Console.WriteLine();
            TextChoice(cursor, text);
            e = Console.ReadKey();
            cursor = CursorChoice(e, cursor, text, ref onScene);
        }
        switch (cursor)
        {
            case 0:
                bool eventOccurred = EventOccur(player1.CalculateProbability(player1.Luk));
                if (eventOccurred)
                {
                    Console.Clear();
                    Console.WriteLine("막내가 손을 다쳤다.");
                    Console.ReadKey();
                    Console.WriteLine("맞선임이 막내를 데리고 의무대로 빠졌다.");
                    Console.ReadKey();
                    Console.WriteLine("나도 같이 도와야겠네");
                    Console.ReadKey();
                    Console.WriteLine();
                    Console.WriteLine("press any Key to continue");
                    Console.ReadKey();
                    ColdWeatherTraining4();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("개꿀 일과 빼먹었다.");
                    Console.ReadKey();
                    Console.WriteLine();
                    Console.WriteLine("press any Key to continue");
                    Console.ReadKey();
                    ColdWeatherTraining4();
                }
                break;
            case 1:
                ColdWeatherTraining4();
                break;
            default:
                break;
        }
    }
    static void ColdWeatherTraining4()
    {
        int cursor = 0;
        bool onScene = true;

        string[] text = { "1. 무시한다.", "2. 경계한다." };

        while (onScene)
        {
            Console.Clear();
            Console.WriteLine("오후 일과가 끝났다.");
            Console.WriteLine();
            Console.WriteLine("숙영을 하기 떄문에 저녁식사 추진 후 바로 취침이다.");
            Console.WriteLine("그래서 불침번 근무가 많아졌기에 나도 들어가야한다.");
            Console.WriteLine();
            Console.WriteLine($"??? : 김굳건 병장님 일어나셔야합니다.");
            Console.WriteLine();
            Console.WriteLine("불침번이 내 차례까지 왔다.");
            Console.WriteLine();
            Console.WriteLine("후임에게 인원체크를 시키고 구석에 쪼그려 앉았다.");
            Console.WriteLine();
            Console.WriteLine("그러자 숲속에서 소리가 들려온다.");
            Console.WriteLine();
            TextChoice(cursor, text);
            e = Console.ReadKey();
            cursor = CursorChoice(e, cursor, text, ref onScene);
        }
        switch (cursor)
        {
            case 0:
                ColdWeatherTrainingBattle2(player1.Luk);
                break;
            case 1:
                ColdWeatherTrainingBattle2(player1.Dex);
                break;
            default:
                break;
        }
    }
    static void ColdWeatherTrainingBattle2(int stat)
    {
        bool eventOccurred = EventOccur(player1.CalculateProbability(stat));
        if (eventOccurred)
        {
            Console.Clear();
            Console.WriteLine("아무 일도 일어나지 않았다.");
            Console.WriteLine();
            Console.WriteLine("Press and key to continue");
            Console.ReadKey();
            ColdWeatherTraining5();
        }
        else
        {
            Console.Clear();
            Console.WriteLine("야생의 고라니가 나타났다!");
            Console.WriteLine();
            Console.ReadKey();
            Console.WriteLine("Press and key to continue");
            Console.ReadKey();
            WDBattle(player1, waterDeer);
            
        }
    }
    static void WDBattle(Character player, Enemy enemy)
    {
        int cursor = 0;
        bool onScene = true;
        string[] text = { "일반 공격", "스킬 공격", "회피" };
        while (onScene)
        {
            if (player.Hp > 0 && enemy.EnemyHp > 0)
            {

                Console.Clear();
                Console.WriteLine($"{enemy.EnemyName}");
                Console.WriteLine($"남은 체력 : {enemy.EnemyHp}");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine($"{player.Name}");
                Console.WriteLine($"남은 체력 : {player.Hp}");
                Console.WriteLine();
            }
            else if (player.Hp <= 0)
            {
                Console.Clear();
                Console.WriteLine("정신을 차리니 모르는 천장이 있다.");
                Console.WriteLine();
                Console.ReadKey();
                Console.WriteLine("의무대에 누워있다.");
                Console.WriteLine();
                Console.ReadKey();
                Console.WriteLine("고라니한테 졌구나...");
                Console.WriteLine();
                Console.ReadKey();
                Console.WriteLine("나머지 훈련을 열외 받았다.");
                Console.WriteLine();
                Console.ReadKey();
                Console.WriteLine("press any Key to continue");
                Console.ReadKey();
                turn = 0;
                ColdWeatherTraining5();
            }
            else
            {
                Console.Clear();
                Console.Write("고라니를 쓰러뜨렸다!");
                Console.WriteLine();
                Console.ReadKey();
                Console.Write("후번초에게 무용담을 뽐내며 복귀한다.");
                Console.WriteLine();
                Console.ReadKey();
                Console.WriteLine("press any Key to continue");
                Console.ReadKey();
                turn = 0;
                Home();
            }
            TextChoice(cursor, text);
            e = Console.ReadKey();
            cursor = CursorChoice(e, cursor, text, ref onScene);
        }
        switch (cursor)
        {
            case 0:
                bool eventOccurred = EventOccur(player1.CalculateProbability(player1.Luk));
                if (eventOccurred)
                {
                    Console.Clear();
                    turn++;
                    Console.WriteLine($"{enemy.EnemyName}");
                    Console.WriteLine($"남은 체력 : {enemy.EnemyHp}");
                    Console.ReadKey();
                    Console.WriteLine();
                    Console.WriteLine("일반 공격!!");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("치명타가 들어갔다.");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine($"{player1.Str * 2}의 데미지");
                    Console.WriteLine();
                    enemy.EnemyHp -= player1.Str * 2;
                    Console.ReadKey();
                    Console.WriteLine("press any Key to continue");
                    Console.ReadKey();
                    WDTurn(turn);
                }
                else
                {
                    Console.Clear();

                    turn++;
                    Console.WriteLine($"{enemy.EnemyName}");
                    Console.WriteLine($"현재 체력 : {enemy.EnemyHp}");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("일반 공격!!");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("평범한 공격이었다.");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine($"{player1.Str}의 데미지");
                    Console.WriteLine();
                    enemy.EnemyHp -= player1.Str;
                    Console.ReadKey();
                    Console.WriteLine($"남은 체력 : {enemy.EnemyHp}");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("press any Key to continue");
                    Console.ReadKey();
                    WDTurn(turn);
                }
                break;
            case 1:
                bool _eventOccurred = EventOccur(player1.CalculateProbability(player1.Luk));
                if (_eventOccurred)
                {
                    Console.Clear();
                    enemy.EnemyHp -= player1.Str * 4;
                    turn++;
                    Console.WriteLine($"{enemy.EnemyName}");
                    Console.WriteLine($"남은 체력 : {enemy.EnemyHp}");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("스킬 공격!!");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("치명타가 들어갔다.");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("사실 미구현 ㅈㅅ ㅎㅎ;; 그냥 일반 공격 2배 데미지");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine($"{player1.Str * 2}의 데미지");
                    enemy.EnemyHp -= player1.Str * 2;
                    Console.ReadKey();
                    Console.WriteLine($"남은 체력 : {enemy.EnemyHp}");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("press any Key to continue");
                    Console.ReadKey();
                    WDTurn(turn);
                }
                else
                {
                    Console.Clear();
                    enemy.EnemyHp -= player1.Str * 2;
                    turn++;
                    Console.WriteLine($"{enemy.EnemyName}");
                    Console.WriteLine($"현재 체력 : {enemy.EnemyHp}");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("스킬 공격!!");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("평범한 공격이었다.");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("사실 미구현 ㅈㅅ ㅎㅎ;; 그냥 일반 공격 2배 데미지");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine($"{player1.Str}의 데미지");
                    enemy.EnemyHp -= player1.Str;
                    Console.ReadKey();
                    Console.WriteLine($"남은 체력 : {enemy.EnemyHp}");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("press any Key to continue");
                    Console.ReadKey();
                    WDTurn(turn);
                }
                break;
            case 2:
                bool __eventOccurred = EventOccur(player1.CalculateProbability(player1.Dex));
                if (__eventOccurred)
                {
                    Console.Clear();
                    turn = -1;
                    Console.WriteLine($"{enemy.EnemyName}");
                    Console.WriteLine($"현재 체력 : {enemy.EnemyHp}");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("회피!!");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("회피에 성공했다!!");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("고라니가 공격의 반동으로 고통스러워 한다.");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine($"{(int)(player1.Str / 2)}의 데미지");
                    Console.WriteLine();
                    enemy.EnemyHp -= (int)(player1.Str / 2);
                    Console.ReadKey();
                    Console.WriteLine($"남은 체력 : {enemy.EnemyHp}");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("고라니가 2턴 그로기 상태에 빠짐");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("press any Key to continue");
                    Console.ReadKey();
                    WDTurn(turn);
                }
                else
                {
                    Console.Clear();
                    turn++;
                    Console.WriteLine($"{enemy.EnemyName}");
                    Console.WriteLine($"현재 체력 : {enemy.EnemyHp}");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("회피!!");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("회피에 실패했다.");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("상대 턴으로 넘어갑니다.");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("press any Key to continue");
                    Console.ReadKey();
                    WDTurn(turn);
                }
                break;
            default:
                break;
        }
    }
    static void WDTurn(int Value)
    {
        if (Value > 0)
        {
            bool eventOccurred = EventOccur(player1.CalculateProbability(player1.IQ));
            if (eventOccurred)
            {
                Console.Clear();
                Console.WriteLine($"{player1.Name}");
                Console.WriteLine($"현재 체력 : {player1.Hp}");
                Console.WriteLine();
                Console.ReadKey();
                Console.WriteLine("고라니가 뒤돌았다.");
                Console.WriteLine();
                Console.ReadKey();
                Console.WriteLine("뒷발로 차려는게 분명해!");
                Console.WriteLine();
                Console.ReadKey();
                Console.WriteLine("고라니의 공격을 막아냈습니다.");
                Console.WriteLine();
                Console.ReadKey();
                Console.WriteLine($"{(int)(waterDeer.EnemyAtk / 2)}의 데미지");
                Console.WriteLine();
                player1.Hp -= (int)(waterDeer.EnemyAtk / 2);
                Console.ReadKey();
                Console.WriteLine($"남은 체력 : {player1.Hp}");
                Console.WriteLine();
                Console.ReadKey();
                Console.WriteLine("press any Key to continue");
                Console.ReadKey();
                WDBattle(player1, waterDeer);
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"{player1.Name}");
                Console.WriteLine($"현재 체력 : {player1.Hp}");
                Console.WriteLine();
                Console.ReadKey();
                Console.WriteLine("고라니가 뒤돌았다.");
                Console.WriteLine();
                Console.ReadKey();
                Console.WriteLine("나의 패기에 짓눌려 도망가는구나!");
                Console.WriteLine();
                Console.ReadKey();
                Console.WriteLine("고라니의 뒷발이 내 몸에 맞았다.");
                Console.WriteLine();
                Console.ReadKey();
                Console.WriteLine($"{(int)(waterDeer.EnemyAtk)}의 데미지");
                Console.WriteLine();
                player1.Hp -= (int)(waterDeer.EnemyAtk);
                Console.ReadKey();
                Console.WriteLine($"남은 체력 : {player1.Hp}");
                Console.WriteLine();
                Console.ReadKey();
                Console.WriteLine("press any Key to continue");
                Console.ReadKey();
                WDBattle(player1, waterDeer);
            }
        }
        else
        {
            Console.Clear();
            Console.WriteLine("고라니가 그로기에 걸려 허우적대고 있다..");
            Console.WriteLine();
            Console.ReadKey();
            Console.WriteLine("press any Key to continue");
            Console.ReadKey();
            WDBattle(player1, waterDeer);
        }
    }

    static void ColdWeatherTraining5()
    {
        Console.WriteLine("혹한기 훈련의 마지막으로 행군이 남았다");
        Console.WriteLine("하지만 행보관님이 병장이라고 열외를 시켜주셨다");
        Console.WriteLine();
        Console.WriteLine("나의 군생활 마지막 훈련인 혹한기가 끝이났다.");
        Console.WriteLine();
        Console.WriteLine("Press and key to continue");
        Console.ReadKey();
        Home();
        //혹한기 훈련 끝
    }
    #endregion
    #region 작업
    static void CementWork1()
    {
        int cursor = 0;
        bool onScene = true;

        string[] text = { "1. 후임들에게 시키고 관리 감독을 한다", "2. 후임들과 함께 포대를 옮긴다." };

        while (onScene)
        {
            Console.Clear();
            Console.WriteLine($"완성도 : {Perfection} 남은 시간 : {10 - workCount}");
            Console.WriteLine("오늘 일과는 공구리 작업이다");
            Console.ReadKey();
            Console.WriteLine("4종 창고로 올라가 시멘트 포대를 챙겨서");
            Console.ReadKey();
            Console.WriteLine("공구리 작업장까지 옮겨야 한다.");
            Console.ReadKey();
            Console.WriteLine("말년에는 떨어지는 낙엽도 조심하라고 하는데 나에게는 너무 가혹한 일이다.");
            Console.ReadKey();
            Console.WriteLine("마침 나와 같이 배정받은 후임들이 보인다.");
            Console.ReadKey();
            Console.WriteLine();
            TextChoice(cursor, text);
            e = Console.ReadKey();
            cursor = CursorChoice(e, cursor, text, ref onScene);
        }
        switch (cursor)
        {
            case 0:
                workCount += 1;
                Perfection += 1;
                CementWork2();
                break;
            case 1:
                workCount += 1;
                Perfection += 3;
                CementWork2();
                break;
            default:
                break;
        }
    }
    static void CementWork2()
    {
        int cursor = 0;
        bool onScene = true;

        string[] text = { "1. 교회에 들어가서 한숨 잔다.", "2. 후임들을 믿을 수 없다 직접 시멘트를 만든다." };

        while (onScene)
        {
            Console.Clear();
            Console.WriteLine($"완성도 : {Perfection} 남은 시간 : {10 - workCount}");
            Console.WriteLine("시멘트를 물과 섞어야한다.");
            Console.ReadKey();
            Console.WriteLine("옆에는 교회가 있고 군종병이 청소를 한다고 문을 열어뒀다.");
            Console.ReadKey();
            Console.WriteLine("주변을 둘러보니 간부는 보이지 않는다.");
            Console.ReadKey();
            Console.WriteLine();
            TextChoice(cursor, text);
            e = Console.ReadKey();
            cursor = CursorChoice(e, cursor, text, ref onScene);
        }
        switch (cursor)
        {
            case 0:
                bool eventOccurred = EventOccur(player1.CalculateProbability(player1.Luk));
                if (eventOccurred)
                {
                    workCount += 2;
                    Perfection += 1;
                    Console.WriteLine("자는 동안 느려도 일이 진행되었다.");
                    Console.ReadKey();
                    Console.WriteLine("오전일과를 통으로 빼먹었다.");
                    Console.ReadKey();
                    Console.WriteLine();
                    Console.WriteLine("press any Key to continue");
                    Console.ReadKey();
                    CementWork3();
                }
                else
                {
                    workCount += 3;
                    Perfection = 0;
                    Console.WriteLine("후임들이 일을 망쳤다.");
                    Console.ReadKey();
                    Console.WriteLine("시멘트부터 다시 챙겨와야 겠다.");
                    Console.ReadKey();
                    Console.WriteLine();
                    Console.WriteLine("press any Key to continue");
                    Console.ReadKey();
                    CementWork3();
                }
                break;
            case 1:
                workCount += 1;
                Perfection += 2;
                CementWork3();
                break;
            default:
                break;
        }
    }
    static void CementWork3()
    {
        int cursor = 0;
        bool onScene = true;

        string[] text = { "1. 생활관에 숨어서 계속 잠을 잔다.", "2. 후임들을 통솔하고 작업하러 떠난다." };

        while (onScene)
        {
            Console.Clear();
            Console.WriteLine($"완성도 : {Perfection} 남은 시간 : {10 - workCount}");
            Console.WriteLine("점심먹고 오후 작업을 시작해야한다.");
            Console.ReadKey();
            Console.WriteLine("하지만 점심 먹고나니 잠이 쏟아진다.");
            Console.ReadKey();
            Console.WriteLine();
            TextChoice(cursor, text);
            e = Console.ReadKey();
            cursor = CursorChoice(e, cursor, text, ref onScene);
        }
        switch (cursor)
        {
            case 0:
                bool eventOccurred = EventOccur(player1.CalculateProbability(player1.Luk));
                if (eventOccurred)
                {
                    workCount += 3;
                    Perfection += 2;
                    Console.WriteLine("자는 동안 느려도 일이 진행되었다.");
                    Console.ReadKey();
                    Console.WriteLine("오후 일과 절반을 보내버렸다.");
                    Console.ReadKey();
                    Console.WriteLine();
                    Console.WriteLine("press any Key to continue");
                    Console.ReadKey();
                    CementWorkLoop();
                }
                else
                {
                    workCount += 1;
                    Perfection += 2;
                    Console.WriteLine("행보관님께 걸렸다.");
                    Console.ReadKey();
                    Console.WriteLine("작업모를 못챙겨서 잠깐 들어왔다고 거짓말을 한다.");
                    Console.ReadKey();
                    Console.WriteLine("행보관님한테 몇대 맞고 작업장으로 복귀한다");
                    Console.ReadKey();
                    Console.WriteLine();
                    Console.WriteLine("press any Key to continue");
                    Console.ReadKey();
                    CementWorkLoop();
                }
                break;
            case 1:
                workCount += 1;
                Perfection += 3;
                CementWorkLoop();
                break;
            default:
                break;
        }
    }
    static void CementWork4()
    {
        int cursor = 0;
        bool onScene = true;

        string[] text = { "1. 계속 지켜본다.", "2. 후임들에게 시범을 보여준다." };

        while (onScene)
        {
            Console.Clear();
            Console.WriteLine($"완성도 : {Perfection} 남은 시간 : {10 - workCount}");
            Console.WriteLine("후임들이 곤란해 하는 것 같다.");
            Console.ReadKey();
            Console.WriteLine("도와주면 쉽게 끝낼 수 있을 것 같다.");
            Console.ReadKey();
            Console.WriteLine();
            TextChoice(cursor, text);
            e = Console.ReadKey();
            cursor = CursorChoice(e, cursor, text, ref onScene);
        }
        switch (cursor)
        {
            case 0:
                workCount += 1;
                Perfection += 2;
                CementWorkLoop();
                break;
            case 1:
                bool eventOccurred = EventOccur(player1.CalculateProbability(player1.Luk));
                if (eventOccurred)
                {
                    workCount += 1;
                    Perfection += 1;
                    Console.WriteLine("아무 일도 일어나지 않았다.");
                    Console.WriteLine();
                    Console.WriteLine("press any Key to continue");
                    Console.ReadKey();
                    CementWorkLoop();
                }
                else
                {
                    workCount += 1;
                    Perfection -= 1;
                    Console.WriteLine("후임들이 일을 망쳤다");
                    Console.WriteLine();
                    Console.WriteLine("press any Key to continue");
                    Console.ReadKey();
                    CementWorkLoop();
                }
                break;
            default:
                break;
        }
    }
    static void CementWork5()
    {
        int cursor = 0;
        bool onScene = true;

        string[] text = { "1. 계속 지켜본다.", "2. 후임들을 도와 작업을 마무리한다" };

        while (onScene)
        {
            Console.Clear();
            Console.WriteLine($"완성도 : {Perfection} 남은 시간 : {10 - workCount}");
            Console.WriteLine("아직 작업량이 많이 남은 것 같다.");
            Console.ReadKey();
            Console.WriteLine("시간 내로 끝내려면 나도 거들어야 한다.");
            Console.ReadKey();
            Console.WriteLine();
            TextChoice(cursor, text);
            e = Console.ReadKey();
            cursor = CursorChoice(e, cursor, text, ref onScene);
        }
        switch (cursor)
        {
            case 0:
                workCount += 1;
                Perfection += 3;
                CementWorkLoop();
                break;
            case 1:
                workCount += 1;
                Perfection += 1;
                CementWorkLoop();
                break;
            default:
                break;
        }
    }
    static void CementWorkLoop()
    {
        Random random = new Random();
        if (workCount < 9)
        {
            if (Perfection < 10)
            {
                int randomChoice = random.Next(0, 2);

                if (randomChoice == 0)
                {
                    CementWork4();
                }
                else
                {
                    CementWork5();
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("작업이 완료되었다.");
                Console.ReadKey();
                Console.WriteLine($"개인정비까지 {9 - workCount}시간 남았으니 휴식하자");
                Console.ReadKey();
                Console.WriteLine("능력치 상승 & 스트레스 감소");
                Console.ReadKey();
                Perfection = 0;
                workCount = 0;
            }

        }
        else
        {
            if (Perfection >= 10)
            {
                Console.Clear();
                Console.WriteLine("작업이 완료되었다.");
                Console.ReadKey();
                Console.WriteLine($"개인정비까지 {9}시간 남았으니 휴식하자");
                Console.ReadKey();
                Console.WriteLine("능력치 상승 & 스트레스 감소");
                Console.ReadKey();
                Perfection = 0;
                workCount = 0;
                Console.WriteLine("Press any key to contiune");
                Console.ReadKey();
                Home();
            }
            else if (Perfection < 10 && Perfection >= 7)
            {
                Perfection = 0;
                workCount = 0;
                Console.Clear();
                Console.WriteLine("일과가 마무리 되었다. 작업물이 살짝 아쉽지만 완벽한 가라는 진짜랬다.");
                Console.ReadKey();
                Console.WriteLine("들키지만 않으면 아무렴 어떠한가");
                Console.ReadKey();
                Console.WriteLine("능력치 상승 & 스트레스 감소");
                Console.ReadKey();
                Console.WriteLine("Press any key to contiune");
                Console.ReadKey();
                Home();
            }
            else
            {
                Perfection = 0;
                workCount = 0;
                Console.Clear();
                Console.WriteLine("시작이 반이고 가만히 있으면 반이라도 간다고 한다.");
                Console.ReadKey();
                Console.WriteLine("시작하고 가만히 있었겄만 결과가 터무니 없다.");
                Console.ReadKey();
                Console.WriteLine("개인정비 시간때 행보관님과 공구리 작업을 치게 되었다.");
                Console.ReadKey();
                Console.WriteLine("능력치 상승 & 스트레스 상승");
                Console.ReadKey();
                Console.WriteLine("Press any key to contiune");
                Console.ReadKey();
                Home();
            }
        }
    }
    static void WarehouseWokr1()
    {
        int cursor = 0;
        bool onScene = true;

        string[] text = { "1. 후임에게 시킨다.", "2. 직접 상단키를 받아 창고 열쇠를 챙긴다." };

        while (onScene)
        {
            Console.Clear();
            Console.WriteLine($"완성도 : {Perfection} 남은 시간 : {10 - workCount}");
            Console.WriteLine("오늘 일과는 2종 창고 정리이다.");
            Console.ReadKey();
            Console.WriteLine("보급병이 창고 현황판을 뽑고 있다.");
            Console.ReadKey();
            Console.WriteLine("그 동안 창고 열쇠를 챙기고 출발할 준비를 해야한다.");
            Console.ReadKey();
            Console.WriteLine("그러기 위해서는 중대장님에게 상단키를 받아야한다. 중대장님이랑 마주치기 껄끄러운데...");
            Console.ReadKey();
            Console.WriteLine();
            TextChoice(cursor, text);
            e = Console.ReadKey();
            cursor = CursorChoice(e, cursor, text, ref onScene);
        }
        switch (cursor)
        {
            case 0:
                bool eventOccurred = EventOccur(player1.CalculateProbability(player1.Luk));
                if (eventOccurred)
                {
                    workCount += 1;
                    Perfection += 1;
                    Console.WriteLine("아무 일도 일어나지 않았다.");
                    Console.ReadKey();
                    Console.WriteLine();
                    Console.WriteLine("press any Key to continue");
                    Console.ReadKey();
                    WarehouseWokr2();
                }
                else
                {
                    workCount += 2;
                    Perfection += 0;
                    Console.WriteLine("후임이 창고 열쇠가 아닌 무기고 열쇠를 가져왔다.");
                    Console.ReadKey();
                    Console.WriteLine();
                    Console.WriteLine("press any Key to continue");
                    Console.ReadKey();
                    WarehouseWokr2();
                }
                break;
            case 1:
                workCount += 1;
                Perfection += 1;
                WarehouseWokr2();
                break;
            default:
                break;
        }
    }
    static void WarehouseWokr2()
    {
        int cursor = 0;
        bool onScene = true;

        string[] text = { "1. 후임에게 시키고 간이 막사에서 한숨 잔다.", "2. A급 장비가 있는지 궁금하다. 같이 작업을 시작한다." };

        while (onScene)
        {
            Console.Clear();
            Console.WriteLine($"완성도 : {Perfection} 남은 시간 : {10 - workCount}");
            Console.WriteLine("한겨울의 컨테이너 한기가 느껴진다.");
            Console.ReadKey();
            Console.WriteLine("창고 문을 열자 먼지가 날리고 냄새가 난다.");
            Console.ReadKey();
            Console.WriteLine("보급병이 창고 물건을 다 꺼내서 재고파악을 하려고 한다.");
            Console.ReadKey();
            Console.WriteLine();
            TextChoice(cursor, text);
            e = Console.ReadKey();
            cursor = CursorChoice(e, cursor, text, ref onScene);
        }
        switch (cursor)
        {
            case 0:
                workCount += 2;
                Perfection += 1;
                WarehouseWokr3();
                break;

            case 1:
                bool eventOccurred = EventOccur(player1.CalculateProbability(player1.Luk));
                if (eventOccurred)
                {
                    workCount += 1;
                    Perfection += 2;
                    Console.WriteLine("우리 중대에 전설같은 존재인 김굳건 병장의 AAA급 모포를 발견했다.");
                    Console.ReadKey();
                    //아이템 획득
                    Console.WriteLine();
                    Console.WriteLine("press any Key to continue");
                    Console.ReadKey();
                    WarehouseWokr3();
                }
                else
                {
                    workCount += 1;
                    Perfection += 2;
                    WarehouseWokr3();
                }
                break;
            default:
                break;
        }
    }

    static void WarehouseWokr3()
    {
        int cursor = 0;
        bool onScene = true;

        string[] text = { "1. 오후에도 뭔 일이 있겠냐 간이 막사에서 한숨 잔다.", "2. 할 일이 많이 남아 보인다. 같이 작업을 시작한다." };

        while (onScene)
        {
            Console.Clear();
            Console.WriteLine($"완성도 : {Perfection} 남은 시간 : {10 - workCount}");
            Console.WriteLine("점심먹고 오후 작업을 시작해야한다.");
            Console.ReadKey();
            Console.WriteLine("한기가 느껴졌던 컨테이너도 오후가 되니 열을 뿜고 있었고.");
            Console.ReadKey();
            Console.WriteLine("날이 풀려 몸이 따뜻해지고 슬 잠이 쏟아지기 시작한다.");
            Console.ReadKey();
            Console.WriteLine();
            TextChoice(cursor, text);
            e = Console.ReadKey();
            cursor = CursorChoice(e, cursor, text, ref onScene);
        }
        switch (cursor)
        {
            case 0:
                workCount += 2;
                Perfection += 1;
                WarehouseWokr4();
                break;

            case 1:
                bool eventOccurred = EventOccur(player1.CalculateProbability(player1.Luk));
                if (eventOccurred)
                {
                    workCount += 1;
                    Perfection += 2;
                    Console.WriteLine("우리 중대에 전설같은 존재인 김굳건 병장의 AAA급 군화를 발견했다.");
                    Console.ReadKey();
                    //아이템 획득
                    Console.WriteLine();
                    Console.WriteLine("press any Key to continue");
                    Console.ReadKey();
                }
                else
                {
                    workCount += 1;
                    Perfection += 2;
                    WarehouseWokr4();
                }
                break;
            default:
                break;
        }
    }

    static void WarehouseWokr4()
    {
        int cursor = 0;
        bool onScene = true;

        string[] text = { "1. 계속 지켜본다.", "2. 보급병에게 가라의 정석을 알려준다." };

        while (onScene)
        {
            Console.Clear();
            Console.WriteLine($"완성도 : {Perfection} 남은 시간 : {10 - workCount}");
            Console.WriteLine("재고가 안맞는것 같다.");
            Console.ReadKey();
            Console.WriteLine("보급병은 그걸 또 다시 세고 있다.");
            Console.ReadKey();
            Console.WriteLine();
            TextChoice(cursor, text);
            e = Console.ReadKey();
            cursor = CursorChoice(e, cursor, text, ref onScene);
        }
        switch (cursor)
        {
            case 0:
                bool eventOccurred = EventOccur(player1.CalculateProbability(player1.Luk));
                if (eventOccurred)
                {
                    workCount += 1;
                    Perfection += 2;
                    WarehouseWorkLoop();
                }
                else
                {
                    workCount += 2;
                    Perfection += 2;
                    Console.WriteLine("멍청한데 부지런하다니 지옥이다.");
                    Console.ReadKey();
                    Console.WriteLine();
                    Console.WriteLine("press any Key to continue");
                    Console.ReadKey();
                    WarehouseWorkLoop();
                }

                break;

            case 1:
                workCount += 1;
                Perfection += 1;
                WarehouseWorkLoop();
                break;
            default:
                break;
        }
    }
    static void WarehouseWokr5()
    {
        int cursor = 0;
        bool onScene = true;

        string[] text = { "1. 계속 지켜본다", "2. 후임들을 도와 작업을 마무리한다" };

        while (onScene)
        {
            Console.Clear();
            Console.WriteLine($"완성도 : {Perfection} 남은 시간 : {10 - workCount}");
            Console.WriteLine("아직 작업량이 많이 남은 것 같다.");
            Console.ReadKey();
            Console.WriteLine("시간 내로 끝내려면 나도 거들어야 한다.");
            Console.ReadKey();
            Console.WriteLine();
            TextChoice(cursor, text);
            e = Console.ReadKey();
            cursor = CursorChoice(e, cursor, text, ref onScene);
        }
        switch (cursor)
        {
            case 0:
                bool eventOccurred = EventOccur(player1.CalculateProbability(player1.Luk));
                if (eventOccurred)
                {
                    workCount += 1;
                    Perfection += 1; //확률로 낮아질 수 있음
                    WarehouseWorkLoop();
                }
                else
                {
                    workCount += 1;
                    Perfection += 0;
                    Console.WriteLine("후임이 같은 상자를 계속 열고 닫고 있다.");
                    Console.ReadKey();
                    Console.WriteLine("오늘안에 끝나긴 글렀다.");
                    Console.ReadKey();
                    Console.WriteLine();
                    Console.WriteLine("press any Key to continue");
                    Console.ReadKey();
                    WarehouseWorkLoop();
                }
                break;

            case 1:
                bool eventOccurred_ = EventOccur(player1.CalculateProbability(player1.Luk));
                if (eventOccurred_)
                {
                    workCount += 1;
                    Perfection += 1;
                    WarehouseWorkLoop();
                }
                else
                {
                    workCount += 1;
                    Perfection += 1;
                    Console.WriteLine("우리 중대에 전설같은 존재인 김굳건 병장의 AAA급 장구류를 발견했다.");
                    Console.ReadKey();
                    //아이템 획득
                    Console.WriteLine();
                    Console.WriteLine("press any Key to continue");
                    Console.ReadKey();
                    WarehouseWorkLoop();
                }
                break;
            default:
                break;
        }
    }

    static void WarehouseWorkLoop()
    {
        Random random = new Random();
        if (workCount < 9)
        {
            if (Perfection < 10)
            {
                int randomChoice = random.Next(0, 2);

                if (randomChoice == 0)
                {
                    WarehouseWokr4();
                }
                else
                {
                    WarehouseWokr5();
                }
            }
            else
            {
                Perfection = 0;
                workCount = 0;
                Console.Clear();
                Console.WriteLine("작업이 완료되었다.");
                Console.ReadKey();
                Console.WriteLine($"개인정비까지 {9 - workCount}시간 남았으니 휴식하자");
                Console.ReadKey();
                Console.WriteLine("능력치 상승 & 스트레스 감소");
                Console.ReadKey();
                Console.WriteLine("Press any key to contiune");
                Console.ReadKey();
                Home();
            }

        }
        else
        {
            if (Perfection >= 10)
            {
                Perfection = 0;
                workCount = 0;
                Console.Clear();
                Console.WriteLine("작업이 완료되었다.");
                Console.ReadKey();
                Console.WriteLine($"개인정비까지 {9}시간 남았으니 휴식하자");
                Console.ReadKey();
                Console.WriteLine("능력치 상승 & 스트레스 감소");
                Console.ReadKey();
                Console.WriteLine("Press any key to contiune");
                Console.ReadKey();
                Home();
            }
            else if (Perfection < 10 && Perfection >= 7)
            {
                Perfection = 0;
                workCount = 0;
                Console.Clear();
                Console.WriteLine("일과가 마무리 되었다. 작업물이 살짝 아쉽지만 완벽한 가라는 진짜랬다.");
                Console.ReadKey();
                Console.WriteLine("들키지만 않으면 아무렴 어떠한가");
                Console.ReadKey();
                Console.WriteLine("능력치 상승 & 스트레스 감소");
                Console.ReadKey();
                Console.WriteLine("Press any key to contiune");
                Console.ReadKey();
                Home();
            }
            else
            {
                Perfection = 0;
                workCount = 0;
                Console.Clear();
                Console.WriteLine("시작이 반이고 가만히 있으면 반이라도 간다고 한다.");
                Console.ReadKey();
                Console.WriteLine("시작하고 가만히 있었겄만 결과가 터무니 없다.");
                Console.ReadKey();
                Console.WriteLine("개인정비 시간때 행보관님과 공구리 작업을 치게 되었다.");
                Console.ReadKey();
                Console.WriteLine("능력치 상승 & 스트레스 상승");
                Console.ReadKey();
                Console.WriteLine("Press any key to contiune");
                Console.ReadKey();
                Home();
            }
        }
    }
    #endregion
    #region 말출
    static void LastLeave1()
    {
        int cursor = 0;
        bool onScene = true;

        string[] text = { "1. 일단 집으로 간다.", "2. 친구들과 연락을 한다." };

        while (onScene)
        {
            Console.Clear();
            Console.WriteLine("말년휴가를 나오게 되었다.");
            Console.WriteLine("가족들을 놀래켜주려고 아무한테도 말을 하지 않았다.");
            Console.WriteLine("친구들과 먼저 밥이나 한끼할까?");
            Console.WriteLine();
            TextChoice(cursor, text);
            e = Console.ReadKey();
            cursor = CursorChoice(e, cursor, text, ref onScene);
        }
        switch (cursor)
        {
            case 0:
                Console.WriteLine("집에 아무도 없다");
                Console.WriteLine("엄마에게 전화하니 날 빼고 가족여행을 갔다고 한다.");
                Console.WriteLine("미리 말 못한 내 잘못이지");
                Console.WriteLine();
                Console.WriteLine("press any Key to continue");
                Console.ReadKey();
                LastLeave2();
                break;

            case 1:
                bool eventOccurred = EventOccur(player1.CalculateProbability(player1.Luk));
                if (eventOccurred)
                {
                    Console.WriteLine("전화기가 꺼져있다.");
                    Console.WriteLine("인스타에 들어가니 입대했다고 한다.");
                    Console.WriteLine("ㅋㅋ ㅈ뺑이쳐라");
                    Console.WriteLine();
                    Console.WriteLine("press any Key to continue");
                    Console.ReadKey();
                    LastLeave2();
                }
                else
                {
                    Console.WriteLine("친구에게 전화했더니 바쁘다고 끊어라고 한다.");
                    Console.WriteLine("인스타그램에 들어가니 학생회를 하고 있었고");
                    Console.WriteLine("오늘 새터가 있는 날이라고 한다.");
                    Console.WriteLine("신입생들과 친해지게 나도 좀 불러주지");
                    Console.WriteLine();
                    Console.WriteLine("press any Key to continue");
                    Console.ReadKey();
                    LastLeave2();
                }
                break;
            default:
                break;
        }
    }
    static void LastLeave2()
    {
        int cursor = 0;
        bool onScene = true;

        string[] text = { "1. 말을 건다.", "2. 말을 걸지 않는다." };

        while (onScene)
        {
            Console.Clear();
            Console.WriteLine("번화가로 나오게 되었다.");
            Console.WriteLine("내 앞으로 이상형의 여성분이 지나간다.");
            Console.WriteLine();
            TextChoice(cursor, text);
            e = Console.ReadKey();
            cursor = CursorChoice(e, cursor, text, ref onScene);
        }
        switch (cursor)
        {
            case 0:
                Console.WriteLine("말을 거니 여성분이 불쾌하게 나를 보고 있다.");
                Console.WriteLine("너무 군인처럼 보였나? 나 말년 병장인데?");
                Console.WriteLine("press any Key to continue");
                Console.ReadKey();
                LastLeave3();
                break;

            case 1:
                Console.WriteLine("아직 민간인도 아닌데 뭔 작업이냐");
                Console.WriteLine("갈길이나 가자");
                Console.WriteLine("press any Key to continue");
                Console.ReadKey();
                LastLeave3();
                break;
            default:
                break;
        }
    }
    static void LastLeave3()
    {
        int cursor = 0;
        bool onScene = true;

        string[] text = { "1. 억울하다 싸운다.", "2. 사람 잘못봤습니다 죄송합니다." };

        while (onScene)
        {
            Console.Clear();
            Console.WriteLine("여성분의 남자친구와 눈이 마주쳤다.");
            Console.WriteLine("그리고 나를 밀쳐냈다.");
            Console.WriteLine("??? : 군바리가 누구한테 찝쩍대는거야!");
            Console.WriteLine();
            TextChoice(cursor, text);
            e = Console.ReadKey();
            cursor = CursorChoice(e, cursor, text, ref onScene);
        }
        switch (cursor)
        {
            case 0:
                MGBattle(player1, muscleguy);
                break;

            case 1:
                bool eventOccurred = EventOccur(player1.CalculateProbability(player1.Dex));
                if (eventOccurred)
                {
                    Console.WriteLine("겨우 빠져나왔다.");
                    Console.WriteLine("요즘 거리가 흉흉한것 같다.");
                    Console.WriteLine("집이나 가자");
                    Console.WriteLine();
                    Console.WriteLine("press any Key to continue");
                    Console.ReadKey();
                    LastLeave4();
                }
                else
                {
                    Console.WriteLine("붙잡혔다.");
                    Console.WriteLine("맞고만 있을 순 없지");
                    Console.WriteLine();
                    Console.WriteLine("press any Key to continue");
                    Console.ReadKey();
                    MGBattle(player1, muscleguy);
                }
                break;
            default:
                break;
        }

    }
    static void MGBattle(Character player, Enemy enemy)
    {
        int cursor = 0;
        bool onScene = true;
        string[] text = { "일반 공격", "스킬 공격", "회피" };
        while (onScene)
        {
            if (player.Hp > 0 && enemy.EnemyHp > 0)
            {

                Console.Clear();
                Console.WriteLine($"{enemy.EnemyName}");
                Console.WriteLine($"남은 체력 : {enemy.EnemyHp}");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine($"{player.Name}");
                Console.WriteLine($"남은 체력 : {player.Hp}");
                Console.WriteLine();
            }
            else if (player.Hp <= 0)
            {
                Console.Clear();
                Console.WriteLine("싸움에서 져버렸다.");
                Console.WriteLine();
                Console.ReadKey();
                Console.WriteLine("경찰서에 인계된 나는 행보관님의 도움으로 나올 수 있었다.");
                Console.WriteLine();
                Console.ReadKey();
                Console.WriteLine("복귀해야하는구나...");
                Console.WriteLine();
                Console.ReadKey();
                Console.WriteLine("press any Key to continue");
                Console.ReadKey();
                turn = 0;
                LastLeave5();
            }
            else
            {
                Console.Clear();
                Console.Write("남성을 쓰러뜨렸다.");
                Console.WriteLine();
                Console.ReadKey();
                Console.Write("별것도 아닌게 까불고 있어 나 대한민국 육군 병장이야!");
                Console.WriteLine();
                Console.ReadKey();
                Console.WriteLine("press any Key to continue");
                Console.ReadKey();
                turn = 0;
                LastLeave4();
            }
            TextChoice(cursor, text);
            e = Console.ReadKey();
            cursor = CursorChoice(e, cursor, text, ref onScene);
        }
        switch (cursor)
        {
            case 0:
                bool eventOccurred = EventOccur(player1.CalculateProbability(player1.Luk));
                if (eventOccurred)
                {
                    Console.Clear();
                    turn++;
                    Console.WriteLine($"{enemy.EnemyName}");
                    Console.WriteLine($"남은 체력 : {enemy.EnemyHp}");
                    Console.ReadKey();
                    Console.WriteLine();
                    Console.WriteLine("일반 공격!!");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("치명타가 들어갔다.");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine($"{player1.Str * 2}의 데미지");
                    Console.WriteLine();
                    enemy.EnemyHp -= player1.Str * 2;
                    Console.ReadKey();
                    Console.WriteLine("press any Key to continue");
                    Console.ReadKey();
                    MGTurn(turn);
                }
                else
                {
                    Console.Clear();

                    turn++;
                    Console.WriteLine($"{enemy.EnemyName}");
                    Console.WriteLine($"현재 체력 : {enemy.EnemyHp}");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("일반 공격!!");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("평범한 공격이었다.");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine($"{player1.Str}의 데미지");
                    Console.WriteLine();
                    enemy.EnemyHp -= player1.Str;
                    Console.ReadKey();
                    Console.WriteLine($"남은 체력 : {enemy.EnemyHp}");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("press any Key to continue");
                    Console.ReadKey();
                    MGTurn(turn);
                }
                break;
            case 1:
                bool _eventOccurred = EventOccur(player1.CalculateProbability(player1.Luk));
                if (_eventOccurred)
                {
                    Console.Clear();
                    enemy.EnemyHp -= player1.Str * 4;
                    turn++;
                    Console.WriteLine($"{enemy.EnemyName}");
                    Console.WriteLine($"남은 체력 : {enemy.EnemyHp}");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("스킬 공격!!");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("치명타가 들어갔다.");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("사실 미구현 ㅈㅅ ㅎㅎ;; 그냥 일반 공격 2배 데미지");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine($"{player1.Str * 2}의 데미지");
                    enemy.EnemyHp -= player1.Str * 2;
                    Console.ReadKey();
                    Console.WriteLine($"남은 체력 : {enemy.EnemyHp}");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("press any Key to continue");
                    Console.ReadKey();
                    MGTurn(turn);
                }
                else
                {
                    Console.Clear();
                    enemy.EnemyHp -= player1.Str * 2;
                    turn++;
                    Console.WriteLine($"{enemy.EnemyName}");
                    Console.WriteLine($"현재 체력 : {enemy.EnemyHp}");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("스킬 공격!!");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("평범한 공격이었다.");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("사실 미구현 ㅈㅅ ㅎㅎ;; 그냥 일반 공격 2배 데미지");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine($"{player1.Str}의 데미지");
                    enemy.EnemyHp -= player1.Str;
                    Console.ReadKey();
                    Console.WriteLine($"남은 체력 : {enemy.EnemyHp}");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("press any Key to continue");
                    Console.ReadKey();
                    MGTurn(turn);
                }
                break;
            case 2:
                bool __eventOccurred = EventOccur(player1.CalculateProbability(player1.Dex));
                if (__eventOccurred)
                {
                    Console.Clear();
                    turn = -1;
                    Console.WriteLine($"{enemy.EnemyName}");
                    Console.WriteLine($"현재 체력 : {enemy.EnemyHp}");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("회피!!");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("회피에 성공했다!!");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("남성이 공격의 반동으로 고통스러워 한다.");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine($"{(int)(player1.Str / 2)}의 데미지");
                    Console.WriteLine();
                    enemy.EnemyHp -= (int)(player1.Str / 2);
                    Console.ReadKey();
                    Console.WriteLine($"남은 체력 : {enemy.EnemyHp}");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("남성은 2턴 그로기 상태에 빠짐");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("press any Key to continue");
                    Console.ReadKey();
                    MGTurn(turn);
                }
                else
                {
                    Console.Clear();
                    turn++;
                    Console.WriteLine($"{enemy.EnemyName}");
                    Console.WriteLine($"현재 체력 : {enemy.EnemyHp}");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("회피!!");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("회피에 실패했다.");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("상대 턴으로 넘어갑니다.");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.WriteLine("press any Key to continue");
                    Console.ReadKey();
                    MGTurn(turn);
                }
                break;
            default:
                break;
        }
    }
    static void MGTurn(int Value)
    {
        if (Value > 0)
        {
            bool eventOccurred = EventOccur(player1.CalculateProbability(player1.Dex));
            if (eventOccurred)
            {
                Console.Clear();
                Console.WriteLine($"{player1.Name}");
                Console.WriteLine($"현재 체력 : {player1.Hp}");
                Console.WriteLine();
                Console.ReadKey();
                Console.WriteLine("남성이 자세를 잡았다.");
                Console.WriteLine();
                Console.ReadKey();
                Console.WriteLine("행보관님의 공격에 비해 너무 느리군");
                Console.WriteLine();
                Console.ReadKey();
                Console.WriteLine("남성의 주먹을 손날로 쳐냈다.");
                Console.WriteLine();
                Console.ReadKey();
                Console.WriteLine($"{(int)(muscleguy.EnemyAtk / 2)}의 데미지");
                Console.WriteLine();
                player1.Hp -= (int)(muscleguy.EnemyAtk / 2);
                Console.ReadKey();
                Console.WriteLine($"남은 체력 : {player1.Hp}");
                Console.WriteLine();
                Console.ReadKey();
                Console.WriteLine("press any Key to continue");
                Console.ReadKey();
                MGBattle(player1, muscleguy);
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"{player1.Name}");
                Console.WriteLine($"현재 체력 : {player1.Hp}");
                Console.WriteLine();
                Console.ReadKey();
                Console.WriteLine("남성이 자세를 잡았다.");
                Console.WriteLine();
                Console.ReadKey();
                Console.WriteLine("나의 특공무술을 보여주지!");
                Console.WriteLine();
                Console.ReadKey();
                Console.WriteLine("남자의 주먹이 내 손날을 가르고 들어왔다.");
                Console.WriteLine();
                Console.ReadKey();
                Console.WriteLine("집체교육때 더 열심히 임할걸 그랬다.");
                Console.WriteLine();
                Console.ReadKey();
                Console.WriteLine($"{(int)(muscleguy.EnemyAtk)}의 데미지");
                Console.WriteLine();
                player1.Hp -= (int)(muscleguy.EnemyAtk);
                Console.ReadKey();
                Console.WriteLine($"남은 체력 : {player1.Hp}");
                Console.WriteLine();
                Console.ReadKey();
                Console.WriteLine("press any Key to continue");
                Console.ReadKey();
                MGBattle(player1, muscleguy);
            }
        }
        else
        {
            Console.Clear();
            Console.WriteLine("남성은 그로기에 걸려 허우적대고 있다..");
            Console.WriteLine();
            Console.ReadKey();
            Console.WriteLine("press any Key to continue");
            Console.ReadKey();
            MGBattle(player1, muscleguy);
        }
    }
    static void LastLeave4()
    {
        Console.Clear();
        Console.WriteLine("밖은 위험하다 그냥 집에서 빈둥거리며 보내야겠다.");
        Console.WriteLine();
        Console.ReadKey();
        Console.WriteLine("press any Key to continue");
        Console.ReadKey();
        LastLeave5();
    }

    static void LastLeave5()
    {
        Console.Clear();
        Console.WriteLine("휴가 복귀를 하고 있다.");
        Console.WriteLine();
        Console.ReadKey();
        Console.WriteLine("뭔가 많은 일이 있었다.");
        Console.WriteLine();
        Console.ReadKey();
        Console.WriteLine("가끔은 부대 안이 더 좋지 않을까 라는 생각을 하게 된다.");
        Console.WriteLine();
        Console.ReadKey();
        Console.WriteLine("press any Key to continue");
        Console.ReadKey();
        Home();
    }
    #endregion
    #region 확률 구현
    static bool EventOccur(double probability)
    {
        Random random = new Random();
        return random.NextDouble() < probability;
    }

    #endregion

    #region 도박
    static void GambleDisplay()
    {
        int cursor = 0;
        bool onScene = true;
        string[] text = { "-----------입장-----------", "----------나가기----------" };

        while (onScene)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("     ※14boonran.com※");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("       ※일사분란※");
            Console.ResetColor();
            Console.WriteLine("        마틴  가능");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("       §신규EVENT§");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("    가입시 500마일리지");
            Console.ResetColor();
            Console.WriteLine("      ☆즉-시-지-급☆");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("    즉시 출금 즉시 입금");
            Console.ResetColor();
            Console.WriteLine("  홀짝, 그래프 상시 운영");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("충전금액 X 10.00% 마일리지");
            Console.ResetColor();
            Console.WriteLine();

            TextChoice(cursor, text);
            e = Console.ReadKey();
            cursor = CursorChoice(e, cursor, text, ref onScene);
        }
        switch (cursor)
        {
            case 0:
                GamebleMain();
                break;
            case 1:
                Home();
                break;
            default:
                break;
        }
    }
    static void GamebleMain()
    {
        int cursor = 0;
        bool onScene = true;

        string[] text = { "-----------홀짝-----------", "----------그래프----------", "-------충전 || 환전-------", "----------나가기----------" };

        while (onScene)
        {
            Console.Clear();
            Console.WriteLine($"COIN : {Coins} 마일리지 : {Mileages}");
            Console.WriteLine("화제 글");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("공지");
            Console.ResetColor();
            Console.Write(" 주소 변경 안내");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("(99+)");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" H");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("잡담");
            Console.ResetColor();
            Console.Write(" 형님들 다 잃었습니다 한강갑니다");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("(99+)");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" H");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("잡담");
            Console.ResetColor();
            Console.Write(" 주식으로 5억번 썰 푼다");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[99+]");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" H");
            Console.ResetColor(); Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(" N");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("잡담");
            Console.ResetColor();
            Console.Write(" 요즘 MZ세대 특 ㅁㅊ;;; ㄷㄷㄷㄷㄷㄷㄷㄷㄷㄷㄷ;;;;");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[99+]");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" H");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("잡담");
            Console.ResetColor();
            Console.Write(" 영포티<<에 긁힘?");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[99+]");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" H");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("잡담");
            Console.ResetColor();
            Console.Write(" 새삼 페이커가 대단하다고 느껴지네");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[99+]");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" H");
            Console.ResetColor();
            Console.ResetColor(); Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(" N");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("잡담");
            Console.ResetColor();
            Console.Write(" 속보)페리시치 사형 구형!");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[99+]");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" H");
            Console.ResetColor();
            Console.WriteLine();
            TextChoice(cursor, text);
            e = Console.ReadKey();
            cursor = CursorChoice(e, cursor, text, ref onScene);
        }
        switch (cursor)
        {
            case 0:
                EvenOdd();
                break;
            case 1:
                GraphGambleDisplay();
                break;
            case 2:
                CoinChargeExchange();
                break;
            case 3:
                Home();
                break;
            default:
                break;
        }
    }
    static void CoinChargeExchange()
    {
        int cursor = 0;
        bool onScene = true;
        string[] text = { "-----------충전-----------", "-----------환전-----------" };

        Console.WriteLine();
        Console.WriteLine();

        while (onScene)
        {
            Console.Clear();
            Console.Write($"Gold : ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{player1.Gold}");
            Console.ResetColor();
            Console.Write($" COIN : ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{Coins}");
            Console.ResetColor();
            Console.Write($" 마일리지 : ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{Mileages}");
            Console.ResetColor();
            Console.WriteLine($"충전/환전소에 오신것을 환영합니다.");
            Console.WriteLine("무엇을 도와드릴까요?");
            Console.WriteLine();
            TextChoice(cursor, text);
            e = Console.ReadKey();
            cursor = CursorChoice(e, cursor, text, ref onScene);
        }
        switch (cursor)
        {
            case 0:
                CoinCharge();
                break;
            case 1:
                CoinExchange();
                break;
            default:
                break;
        }
    }
    static void CoinCharge()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write($"Gold : ");
        Console.ResetColor();
        Console.WriteLine($"{player1.Gold}");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write($"COIN : ");
        Console.ResetColor();
        Console.Write($"{Coins}");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write($"마일리지 : ");
        Console.ResetColor();
        Console.WriteLine($"{Mileages}");
        Console.WriteLine($"충천 수수료 : 5% 마일리지 10.00% 지급");
        Console.WriteLine("100 Gold 단위로 충전 가능");
        Console.WriteLine();
        Console.WriteLine("충전하실 금액을 입력해주세요");
        double ChargeCoins = 0;
        double ChargeMileages = 0;

        int input;
        if (!int.TryParse(Console.ReadLine(), out input))
        {
            Console.WriteLine("유효하지 않은 입력입니다.");
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            CoinCharge();
        }

        if (input <= player1.Gold && input % 100 == 0 && input != 0)
        {
            int cursor = 0;
            bool onScene = true;
            string[] text = { "----------예----------", "--------아니오--------" };

            Console.WriteLine();
            Console.WriteLine();

            while (onScene)
            {
                Console.Clear();

                ChargeCoins = input * 0.95;
                ChargeMileages = input * 0.1;
                Console.Clear();
                Console.WriteLine($"{input}원을 충전하시면 코인 {ChargeCoins}개와 {ChargeMileages} 마일리지를 지급합니다");
                Console.WriteLine("충전하시겠습니까?");
                Console.WriteLine();

                TextChoice(cursor, text);
                e = Console.ReadKey();
                cursor = CursorChoice(e, cursor, text, ref onScene);
            }
            switch (cursor)
            {
                case 0:
                    player1.Gold -= input;
                    Coins += ChargeCoins;
                    Mileages += ChargeMileages;
                    Console.Clear();
                    Console.WriteLine("충전이 완료되어습니다.");
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    GamebleMain();
                    break;
                case 1:
                    GamebleMain();
                    break;
                default:
                    break;
            }
        }
        else
        {
            Console.WriteLine("잘못된 입력입니다. 100골드 단위로 올바른 금액을 입력해주세요.");
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            int cursor = 0;
            bool onScene = true;
            string[] text = { "----------네----------", "--------아니오--------" };

            Console.WriteLine();
            Console.WriteLine();

            while (onScene)
            {
                Console.Clear();
                Console.WriteLine("다시 충전하시겠습니까?");
                Console.WriteLine();

                TextChoice(cursor, text);
                e = Console.ReadKey();
                cursor = CursorChoice(e, cursor, text, ref onScene);
            }
            switch (cursor)
            {
                case 0:
                    CoinCharge();
                    break;
                case 1:
                    GamebleMain();
                    break;
                default:
                    break;
            }
        }
    }
    static void CoinExchange()
    {
        Console.Clear();
        Console.Write($"Gold : ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"{player1.Gold}");
        Console.ResetColor();
        Console.Write($" COIN : ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write($"{Coins}");
        Console.ResetColor();
        Console.Write($" 마일리지 : ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"{Mileages}");
        Console.ResetColor();
        Console.WriteLine($"환전 수수료 : 5% 마일리지는 환전이 불가능합니다.");
        Console.WriteLine("1000 Gold 단위로 환전 가능");
        Console.WriteLine();
        Console.WriteLine("환전하실 금액을 입력해주세요");
        double ChargeCoins = 0;

        int input;
        if (!int.TryParse(Console.ReadLine(), out input))
        {
            Console.WriteLine("유효하지 않은 입력입니다.");
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            CoinExchange();
        }

        if (input <= Coins && input % 1000 == 0 && input != 0)
        {
            int cursor = 0;
            bool onScene = true;
            string[] text = { "----------네----------", "--------아니오--------" };

            Console.WriteLine();
            Console.WriteLine();

            while (onScene)
            {
                Console.Clear();

                ChargeCoins = input * 0.95;
                Console.Clear();
                Console.WriteLine($"{input}코인을 환전하시면 골드 {ChargeCoins}개 입금됩니다");
                Console.WriteLine("환전하시겠습니까?");
                Console.WriteLine();

                TextChoice(cursor, text);
                e = Console.ReadKey();
                cursor = CursorChoice(e, cursor, text, ref onScene);
            }
            switch (cursor)
            {
                case 0:
                    Coins -= input;
                    player1.Gold += (int)ChargeCoins;
                    Console.Clear();
                    Console.WriteLine("환전이 완료되어습니다.");
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    GamebleMain();
                    break;
                case 1:
                    GamebleMain();
                    break;
                default:
                    break;
            }
        }
        else
        {
            Console.WriteLine("잘못된 입력입니다. 1000골드 단위로 올바른 금액을 입력해주세요.");
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            int cursor = 0;
            bool onScene = true;
            string[] text = { "----------네----------", "--------아니오--------" };

            Console.WriteLine();
            Console.WriteLine();

            while (onScene)
            {
                Console.Clear();
                Console.WriteLine("다시 환전하시겠습니까?");
                Console.WriteLine();

                TextChoice(cursor, text);
                e = Console.ReadKey();
                cursor = CursorChoice(e, cursor, text, ref onScene);
            }
            switch (cursor)
            {
                case 0:
                    CoinExchange();
                    break;
                case 1:
                    GamebleMain();
                    break;
                default:
                    break;
            }
        }
    }
    static void PlayAgain()
    {
        int cursor = 0;
        bool onScene = true;

        string[] text = { "---------다시---------", "--------나가기--------" };

        while (onScene)
        {
            Console.Clear();
            Console.Write($"COIN : ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{Coins}");
            Console.ResetColor();
            Console.Write($" 마일리지 : ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{Mileages}");
            Console.ResetColor();
            Console.WriteLine("다시하시겠습니까?");
            TextChoice(cursor, text);
            e = Console.ReadKey();
            cursor = CursorChoice(e, cursor, text, ref onScene);
        }
        switch (cursor)
        {
            case 0:
                EvenOdd();
                break;
            case 1:
                GamebleMain();
                break;
            default:
                break;
        }
    }

    static void EvenOdd()
    {
        bool evenodd = true;

        while (evenodd)
        {
            Console.Clear();
            Console.Write($"COIN : ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{Coins}");
            Console.ResetColor();
            Console.Write($" 마일리지 : ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{Mileages}");
            Console.ResetColor();
            Console.WriteLine("배당 1.8배 | 마틴 가능");
            Console.WriteLine("홀짝 게임에 오신걸 환영합니다!");
            Console.WriteLine("최소 단위 10코인");
            Console.WriteLine("배팅할 금액을 입력해 주세요.(마일리지 먼저 차감됩니다.)");

            int input;
            if (!int.TryParse(Console.ReadLine(), out input))
            {
                Console.WriteLine("유효하지 않은 입력입니다.");
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                EvenOdd();
            }
            if (input <= Mileages + Coins && input % 10 == 0 && input != 0)
            {
                Random random = new Random();
                int RanNum = random.Next(1, 11);
                int cursor = 0;
                bool onScene = true;

                if (Mileages >= input)
                {
                    Mileages -= input;
                }
                else
                {
                    int Balance = input - (int)Mileages;
                    Coins -= Balance;
                    Mileages = 0;
                }

                string[] text = { "----------홀----------", "----------짝----------" };

                while (onScene)
                {
                    Console.Clear();
                    TextChoice(cursor, text);
                    e = Console.ReadKey();
                    cursor = CursorChoice(e, cursor, text, ref onScene);
                }

                switch (cursor)
                {
                    case 0:
                        if (RanNum % 2 != 0)
                        {
                            Console.WriteLine("맞췄습니다.");
                            Console.WriteLine("홀입니다.");
                            Console.WriteLine($"{input} * 1.8배인 {input * 1.8}을 받으셨습니다.");
                            Coins += input * 1.8;
                            Console.WriteLine("Press any Key to continue");
                            Console.ReadKey();
                            PlayAgain();
                        }
                        else
                        {
                            Console.WriteLine("틀렸습니다.");
                            Console.WriteLine("짝입니다.");
                            Console.WriteLine("Press any Key to continue");
                            Console.ReadKey();
                            PlayAgain();
                        }
                        break;
                    case 1:
                        if (RanNum % 2 == 0)
                        {
                            Console.WriteLine("틀렸습니다.");
                            Console.WriteLine("홀입니다.");
                            Console.WriteLine("Press any Key to continue");
                            Console.ReadKey();
                            PlayAgain();
                        }
                        else
                        {
                            Console.WriteLine("맞췄습니다.");
                            Console.WriteLine("짝입니다.");
                            Console.WriteLine($"{input} * 1.8배인 {input * 1.8}을 받으셨습니다.");
                            Coins += input * 1.8;
                            Console.WriteLine("Press any Key to continue");
                            Console.ReadKey();
                            PlayAgain();
                        }
                        break;
                    default:
                        break;
                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다. 10골드 단위로 올바른 금액을 입력해주세요.");
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                int cursor = 0;
                bool onScene = true;
                string[] text = { "----------네----------", "--------아니오--------" };

                Console.WriteLine();
                Console.WriteLine();

                while (onScene)
                {
                    Console.Clear();
                    Console.WriteLine("다시 배팅하시겠습니까?");
                    Console.WriteLine();

                    TextChoice(cursor, text);
                    e = Console.ReadKey();
                    cursor = CursorChoice(e, cursor, text, ref onScene);
                }
                switch (cursor)
                {
                    case 0:
                        EvenOdd();
                        break;
                    case 1:
                        GamebleMain();
                        break;
                    default:
                        break;
                }
            }
        }
    }
    static void GraphGambleDisplay()
    {
        bool Graph = true;
        Rate = 0;
        GrahpCount = 0;

        while (Graph)
        {
            Console.Clear();
            Console.Write($"COIN : ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{Coins}");
            Console.ResetColor();
            Console.Write($" 마일리지 : ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{Mileages}");
            Console.ResetColor();
            Console.WriteLine("그래프 게임에 오신걸 환영합니다!");
            Console.WriteLine("10번 갱신 가능");
            Console.WriteLine("최소 단위 100코인");
            Console.WriteLine("배팅할 금액을 입력해 주세요.(마일리지 먼저 차감됩니다.)");

            int input;
            if (!int.TryParse(Console.ReadLine(), out input))
            {
                Console.WriteLine("유효하지 않은 입력입니다.");
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                GraphGambleDisplay();
            }
            if (input <= Mileages + Coins && input % 100 == 0 && input != 0)
            {
                int cursor = 0;
                bool onScene = true;
                if (Mileages >= input)
                {
                    Mileages -= input;
                }
                else
                {
                    int Balance = input - (int)Mileages;
                    Coins -= Balance;
                    Mileages = 0;
                }

                string[] text = { "----------GO----------", "---------STOP---------" };

                while (onScene)
                {
                    Console.Clear();
                    Console.WriteLine($"{input}");
                    Console.WriteLine($"수익률 : {Rate}");
                    Console.WriteLine($"남은 횟수 : {10 - GrahpCount}");
                    TextChoice(cursor, text);
                    e = Console.ReadKey();
                    cursor = CursorChoice(e, cursor, text, ref onScene);
                }

                switch (cursor)
                {
                    case 0:
                        GrahpCount++;
                        GraphGamble(input);
                        break;
                    case 1:
                        Coins += input + (int)(input * Rate / 100);
                        Console.WriteLine("게임이 종료되었습니다.");
                        Console.WriteLine($"원금 : {input}Coin 수익률 : {Rate:F2}%");
                        Console.WriteLine($"수익 : {input * Rate / 100:F0}");
                        Console.WriteLine($" Coin : {Coins}");
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                        GraphGambleDisplay();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다. 100골드 단위로 올바른 금액을 입력해주세요.");
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                int cursor = 0;
                bool onScene = true;
                string[] text = { "----------네----------", "--------아니오--------" };

                Console.WriteLine();
                Console.WriteLine();

                while (onScene)
                {
                    Console.Clear();
                    Console.WriteLine("다시 배팅하시겠습니까?");
                    Console.WriteLine();

                    TextChoice(cursor, text);
                    e = Console.ReadKey();
                    cursor = CursorChoice(e, cursor, text, ref onScene);
                }
                switch (cursor)
                {
                    case 0:
                        GraphGambleDisplay();
                        break;
                    case 1:
                        GamebleMain();
                        break;
                    default:
                        break;
                }
            }
        }
    }
    static void GraphGamble(int Value)
    {
        Random random = new Random();
        double RanRate = (random.NextDouble() * 0.2) - 0.1;
        Rate += RanRate * 100;
        int cursor = 0;
        bool onScene = true;

        string[] text = { "----------GO----------", "---------STOP---------" };


        while (onScene)
        {
            Console.Clear();
            Console.WriteLine($"{Value}");
            Console.WriteLine($"수익률 : {Rate:F2}%");
            Console.WriteLine($"남은 횟수 : {10 - GrahpCount}");
            TextChoice(cursor, text);
            e = Console.ReadKey();
            cursor = CursorChoice(e, cursor, text, ref onScene);
            if (GrahpCount >= 10)
            {
                cursor = 1;
                break;
            }
        }
        switch (cursor)
        {
            case 0:
                GrahpCount++;
                GraphGamble(Value);
                break;
            case 1:
                Coins += Value + (int)(Value * (Rate / 100));
                Console.WriteLine("게임이 종료되었습니다.");
                Console.WriteLine($"원금 : {Value}Coin 수익률 : {Rate:F2}%");
                Console.WriteLine($"수익 : {Value * Rate / 100:F0}");
                Console.WriteLine($" Coin : {Coins}");
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                GraphGambleDisplay();
                break;
            default:
                break;
        }
    }
    #endregion


    //엔딩씬//

    //해피엔딩//
    static void HappyEndingScene()
    {
        Console.Clear();

        Console.WriteLine("");
        Console.WriteLine("                 ██╗  ██╗ █████╗ ██████╗ ██████╗ ██╗   ██╗    ███████╗███╗   ██╗██████╗ ██╗███╗   ██╗ ██████╗    ");
        Console.WriteLine("                 ██║  ██║██╔══██╗██╔══██╗██╔══██╗╚██╗ ██╔╝    ██╔════╝████╗  ██║██╔══██╗██║████╗  ██║██╔════╝    ");
        Console.WriteLine("                 ███████║███████║██████╔╝██████╔╝ ╚████╔╝     █████╗  ██╔██╗ ██║██║  ██║██║██╔██╗ ██║██║  ███╗   ");
        Console.WriteLine("                 ██╔══██║██╔══██║██╔═══╝ ██╔═══╝   ╚██╔╝      ██╔══╝  ██║╚██╗██║██║  ██║██║██║╚██╗██║██║   ██║   ");
        Console.WriteLine("                 ██║  ██║██║  ██║██║     ██║        ██║       ███████╗██║ ╚████║██████╔╝██║██║ ╚████║╚██████╔╝   ");
        Console.WriteLine("                 ╚═╝  ╚═╝╚═╝  ╚═╝╚═╝     ╚═╝        ╚═╝       ╚══════╝╚═╝  ╚═══╝╚═════╝ ╚═╝╚═╝  ╚═══╝ ╚═════╝    ");
        Console.WriteLine("");


        Console.WriteLine(" 오늘은 전역 날이다.");
        Console.ReadKey();
        Console.WriteLine(" 상꺽 이후로 기상나팔소리보다 일찍 일어났다");
        Console.ReadKey();
        Console.WriteLine(" 현재시간은 6시 20분 ");
        Console.ReadKey();
        Console.WriteLine(" 일어나서 기분좋게 샤워하러 간다");
        Console.ReadKey();
        Console.WriteLine(" 샤워하는 도중에 기상나팔 소리가 들린다");
        Console.ReadKey();
        Console.WriteLine(" 원래 기상나팔소리가 이렇게 경쾌했나?라는 생각을 18개월만에 했다");
        Console.ReadKey();
        Console.WriteLine(" 샤워를 마치고 로션을 바른 후에 창밖을 바라보는데");
        Console.ReadKey();
        Console.WriteLine(" 세상이 이렇게 아름다울 수가 없다");
        Console.ReadKey();
        Console.WriteLine(" 옷갈아입고 아침점호하러 나가야겠다");
        Console.ReadKey();
        Console.WriteLine(" 근데 옷을 갈아입는데 어제한 멍석말이 때문에 온몸이 아프다");
        Console.ReadKey();
        Console.WriteLine(" 그래도 꾸역꾸역 아침점호를 나갔다 오늘은 전역 날이니까 ㅎㅎ");
        Console.ReadKey();
        Console.WriteLine(" 점호를 하는데 오늘따라 보급관님이 잘생겨보인다 확실히 제정신은 아닌듯하다");
        Console.ReadKey();
        Console.WriteLine(" 점호를 마치고 전역복과 전역모를 입고 전역 신고를 한 후에 대대장님과 면담을 하고");
        Console.ReadKey();
        Console.WriteLine(" 경례와 함께 모든 전역과정을 마쳤다");
        Console.ReadKey();
        Console.WriteLine("이제 후임들과 인사를 나누고 덕담을 나눈다");
        Console.ReadKey();
        Console.WriteLine(" (후임들이 모여서) 개부럽네 형 사회에서 보자 고생했어!");
        Console.ReadKey();
        Console.WriteLine(" 여기서 내가 해줄 수 있는 덕담은 그래 고맙고 오늘도 ㅈ뱅이 쳐~");
        Console.ReadKey();
        Console.WriteLine(" 아 그리고 생활관에 내 물품들 있는데 알아서 가져가거나 남은거 처리해줘");
        Console.ReadKey();
        Console.WriteLine(" 이 덕담은 나름 역사가 깊은 전역 덕담이다");
        Console.ReadKey();
        Console.WriteLine(" 후임들과 사진까지 찍고 이제 위병소 밖으로 걸어나간다!!");
        Console.ReadKey();
        Console.WriteLine(" 위병소까지 걸어가는데 마치 내가 세상의 주인공이 된것 같다");
        Console.ReadKey();
        Console.WriteLine(" 지금 누가와도 다 이길자신있다는 마인드다");
        Console.ReadKey();
        Console.WriteLine(" 그리고 위병소 밖으로 나오면서");
        Console.ReadKey();
        Console.WriteLine(" 이제 드디어 전역이다!!!!!!!!!!!!!!!!!!! 소리치고");
        Console.ReadKey();
        Console.WriteLine(" 하늘을 보니 세상이 이렇게 아름다울 수가 없다 냄새, 공기, 구름, 근무중인 짬지후임까지 세상이 아름답다");
        Console.ReadKey();
        Console.WriteLine(" ");
        Console.WriteLine(" 길었던 군생활도 드디어 끝이났다!!!");
        Console.WriteLine(" ");
        Console.ReadKey();



    }

    static void BadEnding()
    {
        Console.Clear();

        Console.WriteLine("");
        Console.WriteLine("                  ██████╗  █████╗ ██████╗     ███████╗███╗   ██╗██████╗ ██╗███╗   ██╗ ██████╗     ");
        Console.WriteLine("                  ██╔══██╗██╔══██╗██╔══██╗    ██╔════╝████╗  ██║██╔══██╗██║████╗  ██║██╔════╝     ");
        Console.WriteLine("                  ██████╔╝███████║██║  ██║    █████╗  ██╔██╗ ██║██║  ██║██║██╔██╗ ██║██║  ███╗    ");
        Console.WriteLine("                  ██╔══██╗██╔══██║██║  ██║    ██╔══╝  ██║╚██╗██║██║  ██║██║██║╚██╗██║██║   ██║    ");
        Console.WriteLine("                  ██████╔╝██║  ██║██████╔╝    ███████╗██║ ╚████║██████╔╝██║██║ ╚████║╚██████╔╝    ");
        Console.WriteLine("                  ╚═════╝ ╚═╝  ╚═╝╚═════╝     ╚══════╝╚═╝  ╚═══╝╚═════╝ ╚═╝╚═╝  ╚═══╝ ╚═════╝     ");
        Console.WriteLine("");

        Console.WriteLine(" 오늘은 전역날이다");
        Console.ReadKey();
        Console.WriteLine(" 드디어 전역이네 후.. 군생활 길었다");
        Console.ReadKey();
        Console.WriteLine(" 어제 멍석말이 할때 엉덩이 발로 찬놈 잡아야됐는데 ");
        Console.ReadKey();
        Console.WriteLine(" 아.. 엉덩이에 피멍들었네");
        Console.ReadKey();
        Console.WriteLine(" 그래도 전역날이니까 기분좋게 마무리하고 집에가자");
        Console.ReadKey();
        Console.WriteLine(" 현재시간 6시 40분 샤워나 하러 가야겠다");
        Console.ReadKey();
        Console.WriteLine(" 어차피 샤훠 10분컷이니까 딱 샤워하고 옷갈아입고 나가면 딱 좋네");
        Console.ReadKey();
        Console.WriteLine(" (샤워를 하는중..)");
        Console.ReadKey();
        Console.WriteLine(" 기분좋게 노래부르며서 양치를 하는데 갑자기 칫솔이 부러졌다..");
        Console.ReadKey();
        Console.WriteLine(" 하.. 갑자기 불길하게 칫솔이 부러지냐");
        Console.ReadKey();
        Console.WriteLine(" 그래도 오늘 전역하는 날이니까 너무 의미부여하지말자 ");
        Console.ReadKey();
        Console.WriteLine(" 샤워를 마치고 대충 전역복을 갈아입고 기분좋게 마지막 아침점호를 나선다~");
        Console.ReadKey();
        Console.WriteLine(" 오늘은 마치 내가 세상의 주인공이 된 것처럼 두려움이 없다");
        Console.ReadKey();
        Console.WriteLine(" 점호를 하는데 갑자기 보급관님이 내얼굴을 보면서 피식 웃는다");
        Console.ReadKey();
        Console.WriteLine(" (마음속으로) 왜 웃는거지? 그렇게 전문하사 해달라고 사정을 하시더니");
        Console.ReadKey();
        Console.WriteLine(" 그렇게 아침점호가 끝나고 알동기들과 전역과정 수료까지 마쳤는데");
        Console.ReadKey();
        Console.WriteLine(" 갑자기 대대장님이 대장실로 들어오라고 하신다");
        Console.ReadKey();
        Console.WriteLine(" ㅎㅎ 나를 위한 이벤트인가? 하고 따라 들어왔는데 대장님실에 간부님들이 다 모여있다..");
        Console.ReadKey();
        Console.WriteLine(" 그러고선 갑자기 전문하사 수여식을 진행한다... 이게 뭐지?? 깜짝 이벤트인가??");
        Console.ReadKey();
        Console.WriteLine(" 너무 당황해서 보급관님께 이 상황이 어떻게 된건지 설명을 해달라고 했다.");
        Console.ReadKey();
        Console.WriteLine(" (보급관): 아 너가 전문하사 안할 줄 알고 미리 너희 부모님과 상의해서 결정했어 ");
        Console.ReadKey();
        Console.WriteLine(" 예? 그게 무슨 말이죠? 에이 거짓말치지마요 아니아니 거짓말치지 마십쇼");
        Console.ReadKey();
        Console.WriteLine("");
        Console.ReadKey();
        Console.WriteLine(" (보급관 랩퍼 빙의하며)");
        Console.ReadKey();
        Console.WriteLine(" (보급관): 이미 다 얘기 끝났고 공문도 올렸고 수여식까지 했으니까 이제 너는 오늘부로 전문하사야");
        Console.ReadKey();
        Console.WriteLine(" (보급관): 부모님이 아직 정신개조가 덜된것 같다고 정신차리면 나오라고 하시네");
        Console.ReadKey();
        Console.WriteLine(" (보급관): 사회나가서 하고싶은 일도 없다면서 그리고 친구들한테 돈도 많이 빌려서 평판도 안좋다고 다 들었다");
        Console.ReadKey();
        Console.WriteLine(" (보급관): 어디 빠져나갈 생각하지 말고 같이 아침먹고 일과나 하자 오늘 연병장에 잡초가 많이 자랐더라");
        Console.ReadKey();
        Console.WriteLine(" (보급관): 아 그리고 부모님께서 원활히 협조를 잘해주셔서 한달에 한번 달팽이 크림 보내주기로 했단다~~");
        Console.ReadKey();
        Console.WriteLine(" 하.. 세상 아무도 믿지 말랬는데 오늘 제대로 느낀다..");
        Console.ReadKey();
        Console.WriteLine(" 근데 맞는 말을 하셔서 여기서 빠져나갈 수 있게 반박할 수가 없다...");
        Console.ReadKey();
        Console.WriteLine(" (대대장님): 전문하사가 된걸 축하한다!! 너는 이제 자랑스러운 군인이 되어야된다");
        Console.ReadKey();
        Console.WriteLine(" (울먹이며)옙 감사합니다!! 오늘부로 병장이 아닌 전문하사로 임무를 잘 수행하겠습니다!!");
        Console.ReadKey();
        Console.WriteLine(" (보급관) 이제 수료식도 했으니 연병장 잡초나 뽑으러 가자~ ㅎㅎ)");
        Console.ReadKey();
        Console.WriteLine(" (표정관리 실패하며)넵 장갑좀 가져오갰습니다!!");
        Console.ReadKey();
        Console.WriteLine(" 그러곤 몰래 분대폰으로 엄마한테 전화를 걸어봤지만 전화를 받지 않는다..");
        Console.ReadKey();
        Console.WriteLine(" 아빠한테도 전화를 걸어봤지만 전화를 받지 않는다...");
        Console.ReadKey();
        Console.WriteLine(" 그 자리에서 폰을 붙잡고 실성을 한다");
        Console.ReadKey();
        Console.WriteLine(" 세상은 이렇게 잔인한 거였다..");
        Console.ReadKey();
        Console.WriteLine(" 그러곤 방송으로 보급관님이 행정반으로 나를 부른다..");
        Console.ReadKey();
        Console.WriteLine(" 그래 나 오늘부로 전문하사지...? ");
        Console.ReadKey();
        Console.WriteLine(" 또한번 실성을 하고 이제 현실을 인정하고 작업에 나섰다");
        Console.ReadKey();
        Console.WriteLine(" 네~ 보급관님 갑니다~~");
        Console.ReadKey();
        Console.WriteLine(" 해는 쨍쨍했지만 내눈에는 세상이 어두워 보였다...");
        Console.ReadKey();








    }

    #region px로 가기
    static void PX()
    {
        Console.Clear();

        Console.WriteLine("                               _----_        ");
        Console.WriteLine("                              | _  _ |       ");
        Console.WriteLine("                              |  __  |       ");
        Console.WriteLine("                              ,'----'.       ");
        Console.WriteLine("                             |        |      ");
        Console.WriteLine(" -------------------------------------------- ");
        Console.WriteLine("                                           ");
        Console.WriteLine("   _____  __    __                        ");
        Console.WriteLine("  | ___ | | |  / /                     ");
        Console.WriteLine("  | |_/ /  | |/ /                      ");
        Console.WriteLine("  |  __/   /   /                      ");
        Console.WriteLine("  | |     / /| |                      ");
        Console.WriteLine("  |_|    /_/  |_|                    ");
        Console.WriteLine(" -------------------------------------------- ");
        Console.WriteLine();
        Console.WriteLine(" 이곳은 PX입니다.");
        Console.WriteLine(" 장비와 회복아이템을 살 수 있습니다.");
        Console.WriteLine();

        Console.WriteLine($" 현재 소지금: {player1._gold}G"); //소지금 표시
        Console.WriteLine("1. 무기코너");
        Console.WriteLine("2. 방어구코너");
        Console.WriteLine("3. 음식코너");
        Console.WriteLine("0. 막사");


        int input = CheckValidInput(0, 3);
        switch (input)
        {
            case 0:
                //막사로
                Home();
                break;
            case 1:
                // 무기상점
                WeaponShop();
                break;

            case 2:
                // 방어구상점
                ArmorShop();
                break;
            case 3:
                // 음식상점
                FoodPx();
                break;

        }
    }
    //무기코너
    static void WeaponShop()
    {
        Console.Clear();

        Console.WriteLine("무기 목록");
        Console.WriteLine("============================================================================================================");
        ConsoleTable buyWeaponTable = new ConsoleTable("순서", "이름", "가격", "설명", "효과");
        for (int i = 0; i < weapons.Count; i++)
        {
            var item = weapons[i];
            buyWeaponTable.AddRow($"{i + 1}", $"{item.ItemName}", $"{item.ItemGold}", $"{item.ItemDescription}", $"{item.ItemEffect}");
        }
        buyWeaponTable.Write();
        Console.WriteLine("============================================================================================================");
        Console.WriteLine("1. 구매하러가기");
        Console.WriteLine("0. 뒤로가기");


        int input = CheckValidInput(0, 1);
        switch (input)
        {
            case 0:
                //px입구
                PX();
                break;
            case 1:
                // 상점에서 아이템을 구매하는 메서드 호출
                BuyWeapon(player1);
                break;

        }


    }
    //방어구코너
    static void ArmorShop()
    {
        Console.Clear();
        Console.WriteLine("방어구 목록");
        Console.WriteLine("=====================================================================================");
        ConsoleTable buyWeaponTable = new ConsoleTable("순서", "이름", "가격", "설명", "효과");
        for (int i = 0; i < armors.Count; i++)
        {
            var item = armors[i];
            buyWeaponTable.AddRow($"{i + 1}", $"{item.ItemName}", $"{item.ItemGold}", $"{item.ItemDescription}", $"{item.ItemEffect}");
        }
        buyWeaponTable.Write();
        Console.WriteLine("=====================================================================================");
        Console.WriteLine("1. 구매하러가기");
        Console.WriteLine("0. 뒤로가기");
        int input = CheckValidInput(0, 1);
        switch (input)
        {
            case 0:
                //px입구
                PX();
                break;
            case 1:
                // 상점에서 아이템을 구매하는 메서드 호출
                BuyArmor(player1);
                break;

        }
    }


    //음식(PX)
    static void FoodPx()
    {
        //상점입니다.
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine("음식 코너");
        Console.WriteLine("=====================================================================================");
        ConsoleTable buyWeaponTable = new ConsoleTable("순서", "이름", "가격", "설명", "효과");
        for (int i = 0; i < foods.Count; i++)
        {
            var item = foods[i];
            buyWeaponTable.AddRow($"{i + 1}", $"{item.ItemName}", $"{item.ItemGold}", $"{item.ItemDescription}", $"{item.ItemEffect}");
        }
        buyWeaponTable.Write();
        Console.WriteLine("=====================================================================================");
        Console.WriteLine();
        Console.WriteLine(" 1. 구매하러가기");

        Console.WriteLine();
        Console.WriteLine(" 0. 뒤로가기");
        Console.Write(">>");

        int input = CheckValidInput(0, 1);
        switch (input)
        {
            case 0:
                //px입구
                PX();
                break;
            case 1:
                // 상점에서 아이템을 구매하는 메서드 호출
                BuyFood(player1);
                break;

        }
    }
    #endregion


    #region 아이템 구매 메소드
    //구매한 무기 인벤토리로 옮기기
    static void BuyWeapon(Character player)
    {
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine(" 구매할 아이템번호를 입력하세요");
        Console.WriteLine($" 현재 소지금 : {player1._gold}");
        Console.WriteLine();


        ConsoleTable buyWeaponTable = new ConsoleTable("순서", "이름", "가격", "설명", "효과");
        for (int i = 0; i < weapons.Count; i++)
        {
            var item = weapons[i];
            buyWeaponTable.AddRow($"{i + 1}", $"{item.ItemName}", $"{item.ItemGold}", $"{item.ItemDescription}", $"{item.ItemEffect}");
        }
        buyWeaponTable.Write();
        Console.WriteLine("=====================================================================================");

        Console.WriteLine();

        //숫자를 입력하면 그것보다 1작은 배열의 물건을 구매매
        int itemIndex = CheckValidInput(1, weapons.Count) - 1;

        Weapon selectedItem = weapons[itemIndex]; // 선택한 아이템 가져오기

        // 플레이어의 골드가 아이템 가격보다 많은지 확인
        if (player.Gold >= selectedItem.ItemGold)
        {
            player.Gold -= selectedItem.ItemGold; // 골드 차감
            player.AddToInventoryWeapon(selectedItem); // 인벤토리에 아이템 추가            
            weapons.Remove(selectedItem);//선택한 아이템 제거
            Console.WriteLine($" {selectedItem.ItemName}을(를) 구매했습니다!");
        }
        else
        {
            Console.WriteLine(" 돈이 부족합니다!");
        }

        Console.WriteLine(" Press Anykey to go Back.");
        Console.Write(">>");
        Console.ReadKey();
        PX(); // 다시 상점으로 돌아가기

    }

    //구매한 방어구 인벤토리로 옮기기
    static void BuyArmor(Character player)
    {
        Console.Clear();
        Console.WriteLine(" 구매할 아이템번호를 입력하세요");
        Console.WriteLine($" 현재 소지금: {player1._gold}");
        Console.WriteLine();
        ConsoleTable buyArmorTable = new ConsoleTable("순서", "이름", "가격", "설명", "효과");
        for (int i = 0; i < armors.Count; i++)
        {
            var item = armors[i];
            buyArmorTable.AddRow($"{i + 1}", $"{item.ItemName}", $"{item.ItemGold}", $"{item.ItemDescription}", $"{item.ItemEffect}");
        }
        buyArmorTable.Write();
        Console.WriteLine("=====================================================================================");

        Console.WriteLine();

        //숫자를 입력하면 그것보다 1작은 배열의 물건을 구매
        int itemIndex = CheckValidInput(1, armors.Count) - 1;

        Armor selectedItem = armors[itemIndex]; // 선택한 아이템 가져오기

        // 플레이어의 골드가 아이템 가격보다 많은지 확인
        if (player.Gold >= selectedItem.ItemGold)
        {
            player.Gold -= selectedItem.ItemGold; // 골드 차감
            player.AddToInventoryArmor(selectedItem); // 인벤토리에 아이템 추가            
            armors.Remove(selectedItem);//선택한 아이템 제거
            Console.WriteLine($" {selectedItem.ItemName}을(를) 구매했습니다!");
        }
        else
        {
            Console.WriteLine(" 돈이 부족합니다!");
        }

        Console.WriteLine(" Press Anykey to go Back.");
        Console.Write(">>");
        Console.ReadKey();
        PX(); // 다시 상점으로 돌아가기

    }
    //구매한 음식 인벤토리로 옮기기
    static void BuyFood(Character player)
    {
        Console.Clear();
        Console.WriteLine(" 구매할 아이템번호를 입력하세요");
        Console.WriteLine($" 현재 소지금: {player1._gold}");

        Console.WriteLine("=====================================================================================");
        Console.WriteLine();
        ConsoleTable buyWeaponTable = new ConsoleTable("순서", "이름", "가격", "설명", "효과");
        for (int i = 0; i < foods.Count; i++)
        {
            var item = foods[i];
            buyWeaponTable.AddRow($"{i + 1}", $"{item.ItemName}", $"{item.ItemGold}", $"{item.ItemDescription}", $"{item.ItemEffect}");
        }
        buyWeaponTable.Write();
        Console.WriteLine("=====================================================================================");
        Console.WriteLine();

        //숫자를 입력하면 그것보다 1작은 배열의 물건을 구매
        int itemIndex = CheckValidInput(1, foods.Count) - 1;

        Food selectedItem = foods[itemIndex]; // 선택한 아이템 가져오기

        // 플레이어의 골드가 아이템 가격보다 많은지 확인
        if (player.Gold >= selectedItem.ItemGold)
        {
            player.Gold -= selectedItem.ItemGold; // 골드 차감
            player.AddToInventoryFood(selectedItem); // 인벤토리에 아이템 추가
            foods.Remove(selectedItem);//선택한 아이템 제거
            Console.WriteLine($" {selectedItem.ItemName}을(를) 구매했습니다!");
        }
        else
        {
            Console.WriteLine(" 돈이 부족합니다!");
        }

        Console.WriteLine(" Press Anykey to go Back.");
        Console.Write(">>");
        Console.ReadKey();
        PX(); // 다시 상점으로 돌아가기

    }
    #endregion


}