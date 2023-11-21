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

internal class Program
{
    private static List<Weapon> weapons = new List<Weapon> //이름, 가격, 설명, 힘, 민첩, 지능, 운
        {
            new Weapon("야전삽", 50, "전투용 삽", 10, 5, 3, 2),
            new Weapon("K2", 200, "국산 소총", 20, 10, 5, 3),
            new Weapon("AK47", 300, "돌격소총", 25, 15, 5, 1),
            new Weapon("샷건", 150, "원거리 전투용 산탄총", 15, 5, 2, 1),
            new Weapon("M60", 400, "무거운 기관총", 30, 5, 2, 1),
            new Weapon("AWP", 500, "저격소총", 40, 5, 2, 1),
            new Weapon("판처파우스트", 600, "고급 소총", 50, 20, 10, 5),
            new Weapon("발칸", 450, "군용 소총", 35, 15, 8, 3),
            new Weapon("K-9자주포", 700, "대형 포탄 발사기", 60, 10, 5, 2),
            new Weapon("현무 극초음속 순항 미사일", 1000, "최첨단 미사일", 100, 50, 30, 10),
            new Weapon("마음의편지", 9999, "최강의 무기", 999, 999, 999, 999)
        };

    // 능력치들은 밸런스에맞게 조정해야됨
    private static List<Armor> armors = new List<Armor> //이름, 가격, 설명, 정신력, 체력
        {
            new Armor("생활복", 50, "평범한 옷", 5, 10),
            new Armor("로카티", 150, "강화된 방어복", 15, 20),
            new Armor("화생방 보호의", 200, "생화학적 위협으로부터 보호하는 의복", 20, 25),
            new Armor("깔깔이", 100, "특수 재료로 만든 방어복", 10, 15),
            new Armor("신형 전투복", 300, "최신형 전투용 갑옷", 25, 30),
            new Armor("개구리 전투복", 120, "개구리 가죽으로 만든 방어복", 12, 18),
            new Armor("특전사 이준호 전투복", 9999, "특전사 이준호님의 전투복", 999, 999)
        };

    //아이템 리스트

    private static List<Food> foods = new List<Food>
        {
             new Food("건빵", 10, 20, "긴급 상황을 위한 비상식량"),
             new Food("전투식량", 15, 30, "맛은 없지만 체력이 충분히 올라가는 식사"),
             new Food("감자", 5, 15, "체력 회복을 위한 탄수화물 보충"),
             new Food("단백질 바", 7, 18, "체력을 위한 단백질 섭취"),                                   // 능력치들은 밸런스에맞게 조정해야됨
             new Food("야간식량", 12, 25, "야간에 먹는 음식, 체력 회복"),
             new Food("특급 식사", 20, 50, "전투에 최적화된 특별한 식사"),
             new Food("야전식량", 18, 40, "야외 전투에 적합한 식사")
        };
    //몬스터 리스트
    private static List<Enemy> enemys = new List<Enemy>
        {
            new Enemy("초임 소위", 100, 100),
            new Enemy("참호", 5, 100),
            new Enemy("맞선임", 4, 10),
            new Enemy("멧돼지", 50, 100),
            new Enemy("고라니", 50, 100)
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



    //아이템들 선언

    private static Armor greenStrap;

    //캐릭터 선언
    private static Character player1;

    private static int howhard;
    static int workCount = 0;
    static int Perfection = 0;
    static double Rate = 0;
    static double Coins = 0;
    static double Mileages = 0;
    // ConsoleKeyInfo 선언
    static ConsoleKeyInfo e;

    static bool frenchSuccess;
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
        player1 = new Character("", "용사", 5, 5, 5, 5, 100, 0, 5);

        // 녹견 세팅
        greenStrap = new Armor("분대장 견장", 0, "분대장의 상징인 녹견", 10, 10);

        //리스트에 아이템들 추가


        //아이템 정보 세팅


        //직업 별 아이템 설정//

        /*
        //포병 
        revisePin = new Item("크리크 수정핀","포병", 3, 0, 5, 2, 0, 150, 10, "사격장에서 흔히 볼수 있는 도구");// 기본템
        shovel = new Item("각삽","포병", 20, 0, 0, 20, 0, 1000, 30, "지형을 수정하거나 만드는 도구 (사실 만능)");
        grain = new Item("곡갱이","포병", 35, 0, 0, 25, 0, 1600, 40, "고르지 못한 땅을 평탕화할 때 유용한 도구");

        //보병
        broom = new Item("빗자루", "보병", 3 ,0, 0, 5, 0, 0, 200, 10, "부대에서 흔히 볼수 있는 도구"); // 기본템
        buttstock = new Item("개머리판","보병", 10, 5, 0, 5, 0, 1000, 30, "머리보다 단단한 개머리판");
        sword = new Item("대검","보병", 30, 5, 0, 10, 0 , 0, 1500, 40, "근무중에 흔히 볼 수 있는 대검")

        //운전병
        wiper = new Item("부러진 와이퍼", "운전병", 3, 0, 0, 5, 0, 200, 10, "운전병 창고에서 흔히 볼수 있는 도구");// 기본템
        chain = new Item("미끄럼 방지체인","운전병", 15, 0, 0, 10, 0 ,1200, 30, "겨울에 볼수 있는 길고 단단한 체인");
        lever = new Item("지렛대", "운전병", 30, 0, 0, 20, 0, 1500, 40, "정비할 때 자주쓰이는 도구");

        //정비병 무기
        Screwdriver = new Item(" +드라이버", "정비병", 3, 0, 5, 0, 0, 200, 10, "정비소에 가면 흔히 볼수 있는 도구");// 기본템
        Spanner = new Item(" 9/16 스페너", "정비병", 10, 0, 10, 20, 0, 1000, 30, "자주 사용하는 사이즈의 스페너");
        Hammer = new Item("오함마", "정비병", 30, 0, 25, 0, 30, 0, 1700, 40,"정비하다가 끼거나 막히면 해결해주는 해결사 도구");
        */

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

    #region 굳건이 시작화면

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
                player1 = new Infantry(player.Name, "보병", 5, 5, 5, 5, 100, 0, 5);
                Console.WriteLine(" 보병을 선택했다.");
                break;
            case 1:
                //포병 전직
                player1 = new Artillery(player.Name, "포병", 5, 5, 5, 5, 100, 0, 5);
                Console.WriteLine(" 포병을 선택했다.");
                break;
            case 2:
                //운전병 전직
                player1 = new Transportation(player.Name, "운전병", 5, 5, 5, 5, 100, 0, 5);
                Console.WriteLine(" 운전병을 선택했다.");
                break;
            case 3:
                //정비병 전직
                player1 = new Maintenence(player.Name, "정비병", 5, 5, 5, 5, 100, 0, 5);
                Console.WriteLine(" 정비병을 선택했다.");
                break;
            default:
                break;
        }
        Console.WriteLine("");
        Console.WriteLine(" 자대로 가서도 꼭 연락해!");

        Console.WriteLine("");
        Console.WriteLine(" press any key to continue");
        Console.ReadKey();
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
        Console.Clear();
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("막사");
        Console.ResetColor();
        Console.WriteLine($" 계급: {Rank.rank} 이름: {player1.Name}");
        Console.WriteLine($" 군생활 {Rank.month}개월 째");
        Console.WriteLine(" 무엇을 할 것인가?");
        Console.WriteLine();
        Console.WriteLine("1. 스토리 진행하기");
        Console.WriteLine("2. 일과하기");
        Console.WriteLine("3. 인벤토리");
        Console.WriteLine("4. 상태확인");
        Console.WriteLine("5. PX가기");
        Console.WriteLine("6. 인터넷도박");

        while(onScene)
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
                FStoryRangerTraining();
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
                HardWork();
                break;
            case 17:            //17개월
                LastLeave1();
                break;
            case 18:            //18개월
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
        Console.WriteLine("========================");
        Console.Write("|        ");
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.Write("weapon");
        Console.ResetColor();
        Console.WriteLine("        |");
        Console.WriteLine("|======================|");
        Console.WriteLine("|--------------|       |");
        Console.WriteLine("|              |     | |");
        Console.WriteLine("|              |       |");
        Console.WriteLine("|              |  ");
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.Write("food");
        Console.ResetColor();
        Console.WriteLine(" |");
        Console.WriteLine("|              |       |");
        Console.WriteLine("|              |=======|");
        Console.WriteLine("|    ");
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.Write("armor");
        Console.ResetColor();
        Console.WriteLine("     |       |");
        Console.WriteLine("|              |       |");
        Console.WriteLine("|              |       |");
        Console.WriteLine("|              |=======|");
        Console.WriteLine("|              |       |");
        Console.WriteLine("|              |=======|");
        Console.WriteLine("|              |   -   |");
        Console.WriteLine("========================");
        Console.WriteLine("|                      |");

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
            //막사로 돌아가기
            Home();
        }
    }

    //방어구 인벤
    static void DisplayArmor(Character player)
    {
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
            //막사로 돌아가기
            Home();
        }
    }
    //음식 인벤
    static void DisplayFood(Character player)
    {
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

            Console.WriteLine();
            Console.WriteLine("Press AnyKey");
            Console.ReadKey();
            //인벤토리창 새로고침
            DisplayFood(player1);
        }
        else
        {
            //막사로 돌아가기
            Home();
        }
    }

    static void EatFood()
    {

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
            if (x >= 80) break;
            // 타임 오버시
            if (time <= 0)
            {
                Console.Clear();
                Console.WriteLine("완주 실패....");
                Thread.Sleep(2000);
                Home();
            }
            Thread.Sleep(10);
        }
        Console.Clear();
        Console.WriteLine("완주 완료!!");
        Console.WriteLine("보상 계산중.... 잠시만 기다려주십시오.");
        Thread.Sleep(2000);

        Console.ReadKey();
        Console.WriteLine("남은 시간 : {0}", time.ToString("F"));
        Console.WriteLine("");
        Console.WriteLine(">> Press the Any key to proceed <<");
        Console.ReadKey(true);

        // 남은 시간에 따른 보상 및 씬이동 로직 추가예정
        // Home Scene이동
        Home();
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
        Console.WriteLine(" 남은 시간이 끝나면 시작되며 보기가 사라집니다.");

        // 타이머 표시
        while (time >= 0)
        {
            Console.SetCursorPosition(0, 8);
            Console.WriteLine($"남은 시간 : {time:F}");
            time -= 0.01f;
            Thread.Sleep(10);
        }

        // 화면 초기화
        Console.Clear();

        // 입력 로직
        Console.WriteLine(" 알맞는 키를 입력하시오.");
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
        Console.WriteLine("총 맞춘 횟수 : {0}", hitCount);

        Console.ReadKey();
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
                Console.WriteLine("");
                Console.WriteLine(" 총 성공 횟수 : {0} ", hitCount);
                Console.WriteLine(" 횟수에 맞게 보상을 지급합니다!");
            }
            else
            {
                // 대기시간
                Console.WriteLine("");
                Console.WriteLine(" 현재 라운드 {0} / {1}   성공 횟수 : {2} ", i, 5, hitCount);
                Console.WriteLine(" 잠시후 다시 시작합니다!");
                Thread.Sleep(1000);
            }
        }

        Console.ReadKey();
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

    //이등병 스토리
    static void Basic(Character player)
    {
        
        // 텍스트 설정 값
        string texts = "\n 드디어 훈련병 생활이 끝났군 \n\n 이제 자대에서 열심히 해보자!";
        char[] text = texts.ToCharArray();


        // 화면 초기화
        Console.Clear();

        foreach (char index in texts)
        {
            Console.Write(index);
            Thread.Sleep(80);
        }
        Console.WriteLine(" >>");
        Console.ReadKey();

        Console.Clear();
        Console.WriteLine();           
        Console.WriteLine("자대배치 후 첫 아침점호 시간이다");
        Console.ReadKey();
        Console.WriteLine("긴장한 상태로 열을 맞춰서있다..");
        Console.ReadKey();
        Console.WriteLine($"그때 한 선임이 \"너 군화 닦았어 ?\" 라고 물어본다.");
        Console.ReadKey();
        Console.WriteLine("옙. 닦았습니다.");
        Console.ReadKey();
        Console.WriteLine("\"진짜? 확인해봐서 안닦였으면 뒤진다\"");
        Console.ReadKey();
        Console.WriteLine("\"이거 봐봐. 이게 닦은거야?\"");
        Console.ReadKey();
        Console.WriteLine("죄.. 죄송합니다.");
        Console.ReadKey();
        Console.WriteLine("\"너 점호끝나고 보자\"");
        Console.ReadKey();
        Console.WriteLine("");//맞선임:~~대충 대사
        Console.WriteLine("");//대충 전투시작

        SeniorFight(player1, senior);

        OneMonthLater();
    }
    //맞선임 보스전


    static void SeniorFight(Character player, Enemy enemy)
    {
        while (true)
        {
            enemy.EnemyHp -= player.Str; //플레이어가 맞선임 공격
            if (enemy.EnemyHp <= 0)
            {
                Console.WriteLine("승리했습니다");
                Console.WriteLine($"남은 체력:{player.Hp}");
                break;
            }
            player.Hp -= enemy.EnemyAtk;//맞선임이 플레이어 공격
            if (player1.Hp <= 0)
            {
                Console.WriteLine("패배했습니다");
                break;
            }
        }
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
    static void FStoryRangerTraining()
    {

        Random random = new Random();
        double success = 0.01; //초기 성공 확률 1%


        Console.Clear();
        Console.WriteLine("당신은 유격훈련에 참가했다.");
        Console.ReadKey();
        Console.WriteLine("지옥의 PT체조가 시작됐다.");
        Console.ReadKey();
        Console.WriteLine("\"지금부터 대답은 \'네\'가 아니라 \'악\'으로 대체합니다.\"");
        Console.ReadKey();
        Console.WriteLine("악!");
        Console.ReadKey();
        Console.WriteLine("\"PT체조 8번 온몸비틀기 준비!\"");
        Console.ReadKey();
        Console.WriteLine("교관은 쉽게 갈 생각이 없는거같다 살아남자!");
        Console.ReadKey();
        Console.WriteLine();
        Console.WriteLine("유-격!");
        Console.ReadKey();
        //확률에 따라 성공 혹은 실패
        //실패마다 정신력, 체력 감소
        //실패시 출력멘트
        while (true)
        {
            double randomValue = random.NextDouble(); //0.0 이상 1.0 미만의 랜덤 실수

            if (randomValue < success)
            {
                Console.WriteLine("\"교육생들 수고 많았습니다.\"");
                Console.ReadKey();
                Console.WriteLine("\"본 교관 나쁜사람 아닙니다.\"");
                Console.ReadKey();
                Console.WriteLine("\"교육생들 막사로 가서 쉬도록합니다.\"");
                Console.ReadKey();
                Console.WriteLine("");
                Console.WriteLine("지옥같은 유격훈련이 끝났다... 돌아가자.");
                Console.ReadKey();
                //성공시 스텟증가 추가해야됨
                OneMonthLater();
                break;

            }
            else
            {
                //실패문구 랜덤생성
                string[] failMessages ={
                    "목소리 크게 합니다. 다시!",
                    "누가 마지막 구호를 외쳐! 다시!",
                    "자세 똑바로 합니다. 다시!",
                    "누가 한숨쉬었습니까. 다시!",
                    "목소리가 작습니다. 다시!",
                    "교관 실망시킬겁니까. 다시!"
                };
                int randomIndex = random.Next(failMessages.Length);
                Console.WriteLine(failMessages[randomIndex]);
                success += 0.03; //실패시 성공확률 3%씩 증가
                Console.ReadKey();
                //추가로 실패시 정신력, 체력 감소 추가해야됨
            }
        }
    }

    //일병 스토리 - 경계근무
    static void FStoryPullSecurity(Character player1, Enemy wildBoar, Enemy waterDeer)
    {
        // Cursor 선택 설정 값
        int cursor = 0;
        bool onScene = true;
        string[] text = { " ==공격==\n", " ==스킬==" };
        
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
        

        while (player1.Hp > 0 && (wildBoar.EnemyHp > 0 || waterDeer.EnemyHp > 0))
        {
            //내 턴
            while(onScene)
            {
                // 화면 초기화
                Console.Clear();

                Console.WriteLine($"{wildBoar.EnemyName}: HP {wildBoar.EnemyHp}, {waterDeer.EnemyName}: HP {waterDeer.EnemyHp}");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine($"{player1.Name}: HP {player1.Hp}");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("플레이어의 턴입니다. 행동을 선택하세요\n");
                Console.WriteLine("==============================================");
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
                    break;
            }

            // 화면 초기화
            Console.Clear();
            //몬스터 턴
            if (wildBoar.EnemyHp > 0)
            {
                Console.WriteLine($" \n {wildBoar.EnemyName}의 공격!\n");
                int enemyDamage1 = wildBoar.EnemyAtk;
                player1.Hp -= enemyDamage1;
                Console.WriteLine($" {wildBoar.EnemyName}이(가) 플레이어에게 {enemyDamage1}의 데미지를 입혔습니다.\n");
            }

            if (waterDeer.EnemyHp > 0)
            {
                Console.WriteLine($" \n {waterDeer.EnemyName}의 공격!\n");
                int enemyDamage2 = waterDeer.EnemyAtk;
                player1.Hp -= enemyDamage2;
                Console.WriteLine($" {waterDeer.EnemyName}이(가) 플레이어에게 {enemyDamage2}의 데미지를 입혔습니다.\n");
            }
            Console.Write("\n            :::::Press any key:::::");
            Console.ReadKey();
            // bool값 및 Cursor값 초기화
            onScene = true;
            cursor = 0;
        }

        //전투 결과
        DisplayResult(player1.Hp, wildBoar, waterDeer);

    }
    //공격선택
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
            Console.WriteLine("\n 어떤 몬스터를 공격하시겠습니까? \n ");

            TextChoice(cursor, text);
            e = Console.ReadKey();
            cursor = CursorChoice(e, cursor, text, ref onScene);
        }

        int playerDamage = player1.Attack();
        enemies[cursor].EnemyHp -= playerDamage;
        Console.WriteLine($" \n 플레이어가 {enemies[cursor].EnemyName}에게 {playerDamage}의 데미지를 입혔습니다.");
    }
    private static void SkillAction(Character player1, params Enemy[] enemies)
    {
        // 스킬 추가하고 여기에 구현
        Console.WriteLine("스킬 낫띵");
    }

    private static void DisplayResult(int playerHp, params Enemy[] enemies)
    {
        if (playerHp <= 0)
        {
            Console.WriteLine("전투에서 패배했습니다. 게임 오버!");
            Console.ReadKey();
            Home();
            return;
        }
        else
        {
            Console.WriteLine("적을 격파했습니다. 전투에서 승리!");
            // 몬스터별 보상 처리
            foreach (var enemy in enemies)
            {
                //player1.Gold += enemy.GoldReward;
                // 경험치 또는 다른 보상 처리도 추가 가능
            }
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
        string[] text = {"1. 여자친구를 만나러 간다.",
        "2. 친구들을 만나러 간다.",
        "3. 본가로 간다.",
        "4. 혼자 논다."};

        // 게임 시작
        // 화면 초기화
        Console.Clear();

        while (onScene)
        {
            // Random Number 설정
            randomNum = random.Next(1, 11);

            //화면 초기화
            Console.Clear();

            Console.WriteLine("드디어 100일 휴가를 나왔다!");
            Console.WriteLine("어떤 일을 먼저 해볼까?");

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
                OneHundredDaysEvent(randomNum, "여자친구가 다른 남자와 다정하게 걷고 있다...",
                "여자친구와 즐거운 시간을 보냈다.",
                "나는 여자친구가 없다...");
                // Scene이동
                OneMonthLater();
                break;
            case 1:
                // 친구들 만나러
                OneHundredDaysEvent(randomNum, "오랜만에 친구들과 술 한잔하며 이야기했다.",
                "친구들과 Pc방에 가서 시간 가는 줄 모르고 놀았다.",
                "나는 친구가 없다...");
                // Scene이동
                OneMonthLater();
                break;
            case 2:
                // 본가로 간다
                OneHundredDaysEvent(randomNum, "오랜만에 집에 왔건만 군대에서 뭐했냐며 잔소리만 들었다...",
                "가족들과 오랜만에 식사하며 좋은 시간을 보냈다.",
                "내가 오는 줄 몰랐나..? 아무도 없다...");
                // Scene이동
                OneMonthLater();
                break;
            case 3:
                // 혼자 논다
                OneHundredDaysEvent(randomNum, "혼자 즐겁게 놀았다. 진짜 즐거운 거 맞다, 아마도..",
                "여기저기 구경 다니며 신나게 놀았다.",
                "생활관에 있을 때가 더 나은 거 같다 너무 외롭다..");
                // Scene이동
                OneMonthLater();
                break;
            default:
                break;

        }
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

        // 선택지 Text
        string[] text = { "1. 머리 조준", "2. 몸통 조준", "3. 바닥 경계선 조준" };

        // 초기 씬 셋팅값
        int cursor = 0;
        bool onScene = true;
        Console.Clear();
        Console.WriteLine("오늘은 사격훈련을 진행하겠다.");
        Console.WriteLine("");
        Console.ReadKey();
        Console.WriteLine("한발 한발 신중하게 쏠 수 있도록 한다.");
        Console.WriteLine("");
        Console.ReadKey();
        Console.WriteLine("탄약을 분배 받은 사수는 각자 위치로!");
        Console.WriteLine("");
        Console.ReadKey();
        Console.WriteLine("준비된 사수는 사격 개시!");
        Console.WriteLine("");
        Console.WriteLine(":::아무 키나 눌러주십시오:::");
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

                Console.WriteLine(" 현재 사격 시도 : {0} / {1}   명중 횟수 : {2}", currentWave, totalWave, hitCount);
                Console.WriteLine("");
                Console.WriteLine("사격 거리 : {0}m ", distance[num]);
                Console.WriteLine("");
                Console.WriteLine("어디를 조준하고 사격할까?");
                Console.WriteLine("");

                // Text[] Output
                TextChoice(cursor, text);
                // Key Input
                e = Console.ReadKey();
                // Cursor index
                cursor = CursorChoice(e, cursor, text, ref onScene);

            }

            // 화면 지우기
            Console.Clear();

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
            Console.WriteLine("명중!!!");
            _hitCount++;
            Console.ReadKey();
        }
        else
        {
            Console.WriteLine("빗나갔다...");
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
                player1.Hp += 50;
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





    #region 혹한기
    static void ColdWeatherTraining1()
    {
        int cursor = 0;
        bool onScene = true;

        string[] text = { "1. 다행이다 미리 준비를 해놨어", "큰일이다 이번 혹한기는 살아남을 수 있을까" };

        while (onScene)
        {
            Console.Clear();
            Console.WriteLine("혹한기 훈련이 시작되었다.");
            Console.WriteLine("혹한기 일정동안 px는 잠시 폐쇠한다고 한다.");
            Console.WriteLine();
            Console.WriteLine($"1. 다행이다 미리 준비를 해놨어");
            Console.WriteLine($"2. 큰일이다 이번 혹한기는 살아남을 수 있을까");
            Console.WriteLine();
            TextChoice(cursor, text);
            e = Console.ReadKey();
            cursor = CursorChoice(e, cursor, text, ref onScene);
        }
        switch (cursor)
        {
            case 0:
                ColdWeatherTraining2(); //아무튼 보너스
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
            Console.WriteLine("혹한기 일정동안 px는 잠시 폐쇠한다고 한다.");
            Console.WriteLine();
            Console.WriteLine($"1. 다행이다 미리 준비를 해놨어");
            Console.WriteLine($"2. 큰일이다 이번 혹한기는 살아남을 수 있을까");
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
                    Console.WriteLine("행보관님에게 걸렸다.");
                    ColdWeatherTrainingBattle1();
                    Console.WriteLine("press any Key to continue");
                }
                else
                {
                    Console.WriteLine("개꿀 일과 빼먹었다.");
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
    static void ColdWeatherTrainingBattle1()
    {
        Console.WriteLine("미구현 ㅈㅅ ㅎㅎ;;");
        Console.WriteLine("press and key to continue");
        Console.ReadKey();
        ColdWeatherTraining3();
        //행보관님과 배틀
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
            Console.WriteLine("옆에선 소대장님이 철조망을 치려고 병사들을 부르고 있다.");
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
            Console.WriteLine("곡괭이질 몇번하니 힘이 빠지기 시작했다. 내가 힘이 빠지는게 보이자");
            Console.WriteLine("행보관님께서 다시 교대를 하자고 하신다.");
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
                Console.WriteLine("막사에 다녀온 행보관님께서 착암기를 가져오셨다.");
                Console.WriteLine("역시 행보관님이야");
                Console.WriteLine();
                Console.WriteLine("press and key to continue");
                Console.ReadKey();
                Home();
                break;
            case 1:
                Console.Clear();
                Console.WriteLine("행보관님과 번갈아 곡괭이 질을 하기 시작했다.");
                Console.WriteLine("오랜 작업으로 힘이 많이 빠졌다.");
                Console.WriteLine("소대장님한테 붙을걸 그랬나.");
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
            Console.WriteLine("2단3열 윤형 철조망을 쳐야한다.");
            Console.WriteLine("소대장님이 일병들과 철조망 작업을 하고 있다..");
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
                    Console.WriteLine("막내가 손을 다쳤다.");
                    Console.WriteLine("맞선임이 막내를 데리고 의무대로 빠졌다.");
                    Console.WriteLine("나도 같이 도와야겠네");
                    Console.WriteLine();
                    Console.WriteLine("press any Key to continue");
                    Console.ReadKey();
                    ColdWeatherTraining4();
                }
                else
                {
                    Console.WriteLine("개꿀 일과 빼먹었다.");
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
            Console.WriteLine("숙영을 하기 떄문에 저녁식사 추진 후 바로 취침이다.");
            Console.WriteLine("그래서 불침번 근무가 많아졌기에 나도 들어가야한다.");
            Console.WriteLine();
            Console.WriteLine($"??? : 김굳건 병장님 일어나셔야합니다.");
            Console.WriteLine("불침번이 내 차례까지 왔다.");
            Console.WriteLine("후임에게 인원체크를 시키고 구석에 쪼그려 앉았다.");
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
            Console.WriteLine("아무 일도 일어나지 않았다.");
            Console.WriteLine();
            Console.WriteLine("Press and key to continue");
            Console.ReadKey();
            ColdWeatherTraining5();
        }
        else
        {
            Console.WriteLine("야생의 고라니가 나타났다!");
            Console.WriteLine("미구현 ㅈㅅ ㅎㅎ;;");
            Console.ReadKey();
            ColdWeatherTraining5();
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
    static void HardWork()
    {
        workCount++;
        int cursor = 0;
        bool onScene = true;

        string[] text = { "1. 행보관님과 공구리 작업", "2. 보급병과 창고정리" };

        while (onScene)
        {
            Console.Clear();
            Console.WriteLine("아침에 행정반을 가니 행보관님계서 두가지 선택권을 주셨다.");
            Console.WriteLine("하나는 행보관님과 공구리를 치는것이고 하나는 보급병과 창고정리를 하는 것이다.");
            Console.WriteLine();
            TextChoice(cursor, text);
            e = Console.ReadKey();
            cursor = CursorChoice(e, cursor, text, ref onScene);
        }
        switch (cursor)
        {
            case 0:
                CementWork1();
                break;
            case 1:
                WarehouseWokr1();
                break;
            default:
                break;
        }
    }

    static void CementWork1()
    {
        int cursor = 0;
        bool onScene = true;

        string[] text = { "1. 후임들에게 시키고 관리 감독을 한다", "2. 후임들과 함께 포대를 옮긴다." };

        while (onScene)
        {
            Console.Clear();
            Console.WriteLine($"완성도 : {Perfection} 남은 시간 : {10 - workCount}");
            Console.WriteLine("시멘트 포대를 옮겨야 한다.");
            Console.WriteLine("말년에는 떨어지는 낙엽도 조심하라고 하는데 나에게는 너무 가혹한 일이다.");
            Console.WriteLine("나와 같이 배정받은 후임들이 보인다.");
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
            Console.WriteLine("옆에는 교회가 있고 군종병이 청소를 한다고 문을 열어뒀다.");
            Console.WriteLine("주변을 둘러보니 간부는 보이지 않는다.");
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
                    Console.WriteLine("오전일과를 통으로 빼먹었다.");
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
                    Console.WriteLine("시멘트부터 다시 챙겨와야 겠다.");
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
            Console.WriteLine("하지만 점심 먹고나니 잠이 쏟아진다.");
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
                    Console.WriteLine("오후 일과 절반을 보내버렸다.");
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
                    Console.WriteLine("작업모를 못챙겨서 잠깐 들어왔다고 거짓말을 한다.");
                    Console.WriteLine("행보관님한테 몇대 맞고 작업장으로 복귀한다");
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
            Console.WriteLine("도와주면 쉽게 끝낼 수 있을 것 같다.");
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
            Console.WriteLine("시간 내로 끝내려면 나도 거들어야 한다.");
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
                Console.WriteLine($"개인정비까지 {9 - workCount}시간 남았으니 휴식하자");
                Console.WriteLine("능력치 상승 & 스트레스 감소");
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
                Console.WriteLine($"개인정비까지 {9}시간 남았으니 휴식하자");
                Console.WriteLine("능력치 상승 & 스트레스 감소");
                Perfection = 0;
                workCount = 0;
            }
            else if (Perfection < 10 && Perfection >= 7)
            {
                Perfection = 0;
                workCount = 0;
                Console.Clear();
                Console.WriteLine("일과가 마무리 되었다. 작업물이 살짝 아쉽지만 완벽한 가라는 진짜랬다.");
                Console.WriteLine("들키지만 않으면 아무렴 어떠한가");
                Console.WriteLine("능력치 상승 & 스트레스 감소");
            }
            else
            {
                Perfection = 0;
                workCount = 0;
                Console.Clear();
                Console.WriteLine("시작이 반이고 가만히 있으면 반이라도 간다고 한다.");
                Console.WriteLine("시작하고 가만히 있었겄만 결과가 터무니 없다.");
                Console.WriteLine("개인정비 시간때 행보관님과 공구리 작업을 치게 되었다.");
                Console.WriteLine("능력치 상승 & 스트레스 상승");
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
            Console.WriteLine("보급병이 창고 현황판을 뽑고 있다.");
            Console.WriteLine("그 동안 창고 열쇠를 챙기고 출발할 준비를 하자.");
            Console.WriteLine("중대장님에게 상단키를 받아야한다. 중대장님이랑 마주치기 껄끄러운데...");
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
            Console.WriteLine("창고 문을 열자 먼지가 날리고 냄새가 난다.");
            Console.WriteLine("보급병이 창고 물건을 다 꺼내서 재고파악을 하려고 한다.");
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
            Console.WriteLine("한기가 느껴졌던 컨테이너도 오후가 되니 열을 뿜고 있었고.");
            Console.WriteLine("날이 풀려 몸이 따뜻해지고 슬 잠이 쏟아지기 시작한다.");
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
            Console.WriteLine("보급병은 그걸 또 다시 세고 있다.");
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
            Console.WriteLine("시간 내로 끝내려면 나도 거들어야 한다.");
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
                    Console.WriteLine("오늘안에 끝나긴 글렀다.");
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
                Console.Clear();
                Console.WriteLine("작업이 완료되었다.");
                Console.WriteLine($"개인정비까지 {9 - workCount}시간 남았으니 휴식하자");
                Console.WriteLine("능력치 상승 & 스트레스 감소");
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
                Console.WriteLine($"개인정비까지 {9}시간 남았으니 휴식하자");
                Console.WriteLine("능력치 상승 & 스트레스 감소");
                Perfection = 0;
                workCount = 0;
            }
            else if (Perfection < 10 && Perfection >= 7)
            {
                Perfection = 0;
                workCount = 0;
                Console.Clear();
                Console.WriteLine("일과가 마무리 되었다. 작업물이 살짝 아쉽지만 완벽한 가라는 진짜랬다.");
                Console.WriteLine("들키지만 않으면 아무렴 어떠한가");
                Console.WriteLine("능력치 상승 & 스트레스 감소");
            }
            else
            {
                Perfection = 0;
                workCount = 0;
                Console.Clear();
                Console.WriteLine("시작이 반이고 가만히 있으면 반이라도 간다고 한다.");
                Console.WriteLine("시작하고 가만히 있었겄만 결과가 터무니 없다.");
                Console.WriteLine("개인정비 시간때 행보관님과 공구리 작업을 치게 되었다.");
                Console.WriteLine("능력치 상승 & 스트레스 상승");
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
                LastLeaveBattle1();
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
                    LastLeaveBattle1();
                }
                break;
            default:
                break;
        }

    }
    static void LastLeaveBattle1()
    {
        Console.Clear();
        Console.WriteLine("미구현 ㅈㅅ ㅎㅎ;");
        Console.WriteLine("press any Key to continue");
        Console.ReadKey();
        Home();
        //남자와 배틀
        //지면 경찰서에 끌려가서 행보관한테 복귀엔딩
        //이기면 집으로 돌가는 엔딩

    }
    static void LastLeave4()
    {
        Console.Clear();
        //이 상황에 질려서 집에서 말년 보내는 엔딩
        Console.WriteLine("밖은 위험하다 그냥 집에서 빈둥거리며 보내야겠다.");
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
        string[] text = { "---------입장---------", "--------나가기--------" };

        Console.WriteLine();
        Console.WriteLine();

        while (onScene)
        {
            Console.Clear();

            Console.WriteLine("※14boonran.com※");
            Console.WriteLine("※일사분란※");
            Console.WriteLine("선충전, 후입금");
            Console.WriteLine("§첫충EVENT§");
            Console.WriteLine("충전금액 X 10.00% 마일리지");
            Console.WriteLine("☆즉★시☆지★급☆");
            Console.WriteLine("홀짝, 그래프 상시 운영");
            Console.WriteLine("마틴 가능 | 즉시 출금 가능");
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
                //나가기
                break;
            default:
                break;
        }
    }

    static void GamebleMain()
    {
        int cursor = 0;
        bool onScene = true;

        string[] text = { "---------홀짝---------", "--------그래프--------", "--------충전/환전--------", "--------나가기--------" };

        while (onScene)
        {
            Console.Clear();
            Console.WriteLine($"COIN : {Coins} 마일리지 : {Mileages}");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
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
                CoinCharge();
                break;
            case 3:
                //나가기
                break;
            default:
                break;
        }
    }

    static void CoinCharge()
    {
        Console.Clear();
        Console.WriteLine($"Gold : {player1.Gold}");
        Console.WriteLine($"COIN : {Coins} 마일리지 : {Mileages}");
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
            string[] text = { "---------예---------", "--------아니오--------" };

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
            string[] text = { "---------네---------", "--------아니오--------" };

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
    static void PlayAgain()
    {
        int cursor = 0;
        bool onScene = true;

        string[] text = { "---------다시---------", "--------나가기--------" };

        while (onScene)
        {
            Console.Clear();
            Console.WriteLine($"COIN : {Coins} 마일리지 : {Mileages}");
            Console.WriteLine("다시하시겠습니까?");
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

    static void EvenOdd()
    {
        bool evenodd = true;

        while (evenodd)
        {
            Console.Clear();
            Console.WriteLine($"COIN : {Coins} 마일리지 : {Mileages}");
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
                Coins -= input;

                string[] text = { "---------홀---------", "--------짝--------" };

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
                string[] text = { "---------네---------", "--------아니오--------" };

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
        bool evenodd = true;
        Rate = 0;

        while (evenodd)
        {
            Console.Clear();
            Console.WriteLine($"COIN : {Coins} 마일리지 : {Mileages}");
            Console.WriteLine("그래프 게임에 오신걸 환영합니다!");
            Console.WriteLine("최소 단위 10코인");
            Console.WriteLine("배팅할 금액을 입력해 주세요.(마일리지 먼저 차감됩니다.)");

            int input;
            if (!int.TryParse(Console.ReadLine(), out input))
            {
                Console.WriteLine("유효하지 않은 입력입니다.");
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                GraphGambleDisplay();
            }
            if (input <= Mileages + Coins && input % 10 == 0 && input != 0)
            {
                int cursor = 0;
                bool onScene = true;
                Coins -= input;

                string[] text = { "---------GO---------", "--------STOP--------" };

                while (onScene)
                {
                    Console.Clear();
                    Console.WriteLine($"{input}");
                    Console.WriteLine($"수익률 : {Rate}");
                    TextChoice(cursor, text);
                    e = Console.ReadKey();
                    cursor = CursorChoice(e, cursor, text, ref onScene);
                }

                switch (cursor)
                {
                    case 0:
                        GraphGamble(input);
                        break;
                    case 1:
                        Coins += (input * Rate);
                        Console.WriteLine("게임이 종료되었습니다.");
                        Console.WriteLine($"원금 : {input}Coin 수익률 : {Rate:F2}%");
                        Console.WriteLine($"수익 : {input * Rate}");
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
                Console.WriteLine("잘못된 입력입니다. 10골드 단위로 올바른 금액을 입력해주세요.");
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                int cursor = 0;
                bool onScene = true;
                string[] text = { "---------네---------", "--------아니오--------" };

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
    static void GraphGamble(int Value)
    {
        Random random = new Random();
        double RanRate = (random.NextDouble() * 0.2) - 0.1;
        Rate += RanRate * 100;
        int cursor = 0;
        bool onScene = true;

        string[] text = { "---------GO---------", "--------STOP--------" };

        while (onScene)
        {
            Console.Clear();
            Console.WriteLine($"{Value}");
            Console.WriteLine($"수익률 : {Rate:F2}%");
            TextChoice(cursor, text);
            e = Console.ReadKey();
            cursor = CursorChoice(e, cursor, text, ref onScene);
        }

        switch (cursor)
        {
            case 0:
                GraphGamble(Value);
                break;
            case 1:
                Coins += (Value * Rate) % 1;
                Console.WriteLine("게임이 종료되었습니다.");
                Console.WriteLine($"원금 : {Value}Coin 수익률 : {Rate:F2}%");
                Console.WriteLine($"수익 : {Value * Rate / 100:F0}");
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                GraphGambleDisplay();
                break;
            default:
                break;
        }
    }
    #endregion
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
        Console.WriteLine("=====================================================================================");
        for (int i = 0; i < weapons.Count; i++)
        {
            var weapon = weapons[i];
            Console.WriteLine($"{i + 1}. {weapon.ItemName} \t| 가격: {weapon.ItemGold}G \t| 아이템 설명: {weapon.ItemDescription}");
            Console.WriteLine($"\t|힘:{weapon.ItemStr} \t|민첩:{weapon.ItemDex} \t|지능:{weapon.ItemIq} \t|운:{weapon.ItemLuk} ");
        }
        Console.WriteLine("=====================================================================================");
        Console.WriteLine("1. 구매하기");
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
        for (int i = 0; i < armors.Count; i++)
        {
            var armor = armors[i];
            Console.WriteLine($"{i + 1}. {armor.ItemName} \t| 가격: {armor.ItemGold}G \t| 아이템 설명: {armor.ItemDescription}");
            Console.WriteLine($"\t |정신력:{armor.ItemMind} \t|체력 증가량: {armor.ItemHp} ");
        }
        Console.WriteLine("=====================================================================================");
        Console.WriteLine("1. 구매하기");
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

        Console.WriteLine();
        Console.WriteLine("식재료 코너");
        Console.WriteLine("=====================================================================================");
        //반복문을 이용한 아이템 목록출력
        for (int i = 0; i < foods.Count; i++)
        {
            var food = foods[i];
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine($" {i + 1}. {food.ItemName} \t| 가격: {food.ItemGold}G \t| 아이템 설명: {food.ItemDescription} ");
            Console.WriteLine($"\t \t|증가하는 체력량{food.ItemHp} \t|");
        }
        Console.WriteLine("------------------------------------------------------------------");
        Console.WriteLine("=====================================================================================");
        Console.WriteLine();
        Console.WriteLine(" 1. 아이템 구매하기");

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
        Console.WriteLine(" 구매할 아이템을 선택하세요:");
        Console.WriteLine($" 현재 소지금: {player1._gold}");
        Console.WriteLine();
        Console.WriteLine("=====================================================================================");
        Console.WriteLine();
        for (int i = 0; i < weapons.Count; i++)
        {
            var item = weapons[i];
            Console.WriteLine($" {i + 1}. {item.ItemName} \t| 가격: {item.ItemGold}G");
            Console.WriteLine();
        }
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
        Console.WriteLine(" 구매할 아이템을 선택하세요:");
        Console.WriteLine($" 현재 소지금: {player1._gold}");
        Console.WriteLine();
        Console.WriteLine("=====================================================================================");
        Console.WriteLine();
        for (int i = 0; i < armors.Count; i++)
        {
            var item = armors[i];
            Console.WriteLine($" {i + 1}. {item.ItemName} \t| 가격: {item.ItemGold}G");
            Console.WriteLine();
        }
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
        Console.WriteLine(" 구매할 아이템을 선택하세요:");
        Console.WriteLine($" 현재 소지금: {player1._gold}");

        Console.WriteLine("=====================================================================================");
        Console.WriteLine();
        for (int i = 0; i < foods.Count; i++)
        {
            var food = foods[i];
            Console.WriteLine($" {i + 1}. {food.ItemName} \t| 가격: {food.ItemGold}G");
            Console.WriteLine();
        }
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