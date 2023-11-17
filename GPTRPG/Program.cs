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

    //아이템 리스트
    private static List<Item> items = new List<Item>();
    private static List<Food> foods = new List<Food>();
    //몬스터 리스트
    private static List<Enemy> enemys = new List<Enemy>();

    //아이템들 선언
    private static Item ROKA;
    private static Item RPG;
    private static Item K2;

    private static Food iceChicken;
    private static Food cupNoddle;
    private static Food egg;

    //몬스터들 선언
    private static Enemy slime;
    private static Enemy gobline;
    private static Enemy newCommander;
    private static Enemy french;

    //캐릭터 선언
    private static Character player1;
    private static Rank rank1;    

    // ConsoleKeyInfo 선언
    static ConsoleKeyInfo e;

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
        //player1 = new Character("", "용사", 5, 5, 5, 5, 100, 0, 5);

        // 아이템 정보 세팅
        ROKA = new Item("로카 반팔티", 4, 4, 0, 0, 10, 10, 3000, "로카티. 잘때 입으면 편할 것 같다.");
        RPG = new Item("RPG", 10, 0, 0, 10, 0, 0, 3000, "알라의 요술봉(모조품). 가뿐히 적을 잡을 수 있을 것 같다.");

        //리스트에 아이템들 추가
        items.Add(ROKA);
        items.Add(RPG);
        
        //아이템 정보 세팅
        iceChicken = new Food("슈넬치킨", 3, 0, 0, 0, 10, 10, 1000, "맛있는 슈넬치킨. 요즘엔 더 맛있는 것도 많아졌다.");
        cupNoddle = new Food("신라면 블랙", 3, 0, 0, 0, 10, 10, 1000, "전자레인지에 돌려먹으면 더 맛있는 신라면 블랙.");
        egg = new Food("참숯란", 10, 0, 0, 0, 0, 40, 4000, "군대의 몇 안되는 단백질 보급원. 헬창들에게 인기가 많다.");
        //리스트에 음식 추가
        foods.Add(iceChicken);
        foods.Add(cupNoddle);
        foods.Add(egg);

        Rank myRank = new Rank(1);


        //몬스터들 정보 세팅
        newCommander = new Enemy("초임 소위", 100, 100);
        french = new Enemy("참호", 5, 100);

        //몬스터 추가
        enemys.Add(french);
        enemys.Add(newCommander);

        
        


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
         Console.Clear();
         //시작화면
         Console.ForegroundColor = ConsoleColor.Red;
 
         Console.WriteLine("                                                                                ");
         Console.WriteLine("                         Press Any Key to start the game.                       ");
         Console.WriteLine("                                                                                ");
         Console.Write("                                                                              >>");
 
         Console.ReadKey();
         Console.ResetColor();
         TrainingSchool(player1);//줄거리로 이동
 
     }
   #endregion

    //훈련소
    static void TrainingSchool(Character player)
    {
        Console.Clear();
        Console.WriteLine("139번 훈련병!");
        Console.WriteLine("너 이름이 뭐야?");
        Console.WriteLine("이름을 입력 하세요...");
        player.Name = Console.ReadLine();
        Console.WriteLine($"이제부터는 이병 {player.Name}이네.");
        Console.WriteLine("너 보직은 뭐야?");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("보직을 선택하세요...");
        Console.WriteLine();
        Console.WriteLine("1. 포병");
        Console.WriteLine("2. 보병");
        Console.WriteLine("3. 운전병");
        Console.WriteLine("4. 정비병");
        Console.ResetColor();
        int input = CheckValidInput(1, 4);
        switch (input)
        {
            case 1:
                //포병 전직
                player1 = new Artillery(player.Name,"포병", 5, 5, 5, 5, 100, 0, 5);
                Console.WriteLine("포병이다.");
                break;
            case 2:
                //보병 전직
                player1 = new Infantry(player.Name,"보병", 5, 5, 5, 5, 100, 0, 5);
                Console.WriteLine("보병이다.");
                break;
            case 3:
                //운전병 전직
                player1 = new Transportation(player.Name,"운전병", 5, 5, 5, 5, 100, 0, 5);
                Console.WriteLine("운전병이다.");
                break;
            case 4:
                //정비병 전직
                player1 = new Maintenence(player.Name,"정비병", 5, 5, 5, 5, 100, 0, 5);
                Console.WriteLine("정비병이다.");
                break;
        }
        Console.WriteLine("자대로 가서도 꼭 연락해!");

        Console.WriteLine("press any key to continue");
        Console.ReadKey();
        Home();

    }
    #region 막사/생활관
        //막사 매서드
        static void Home()
        {
            Console.Clear();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("막사");
            Console.ResetColor();
            Console.WriteLine($"계급: {Rank.rank} 이름: {player1.Name}");
            Console.WriteLine($"군생활 {Rank.month}개월 째");
            Console.WriteLine("무엇을 할 것인가?");
            Console.WriteLine();
            Console.WriteLine("1. 스토리 진행하기");
            Console.WriteLine("2. 일과하기");
            Console.WriteLine("3. 인벤토리");
            Console.WriteLine("4. 상태확인");
            Console.WriteLine("5. PX가기");
    
            int input = CheckValidInput(1, 5);
            switch (input)
            {
                case 1:
                    //스토리 진행
                    StoryPlay();
                    break;
                case 2:
                    //일과 진행
                    break;
                case 3:
                    //인벤토리
                    DisplayInventory(player1);
                    break;
                case 4:
                    //상태확인
                    DisplayMyInfo();
                    break;
                case 5:
                    //px
                    PX();
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
            FStoryPullSecurity();
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
            CStoryKCTC();
            break;
            case 10:            //10개월
            CSDefcon(player1);
            break;
            case 11:            //11개월
            CSschool();
            break;
            case 12:            //12개월
            CSNewCommander(player1, newCommander);
            break;
            case 13:            //13개월
            CSTest();
            break;
            case 14:            //14개월
            CStoryPhysicalExamination(player1);
            break;
            //병장
            case 15:            //15개월
            break;
            case 16:            //16개월
            break;
            case 17:            //17개월
            break;
            case 18:            //18개월
            break;

        }


    }

    //한달 지나기
    static void OneMonthLater()
    {
        Rank.month++;
        Console.WriteLine("한달이 흘렀다...");
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
            Console.WriteLine(" [인벤토리]");
            Console.WriteLine($"\t\t\t\t\t\t[소지금:{player1._gold}G]");
            Console.WriteLine();
            Console.WriteLine("============================================================================");
            Console.WriteLine(" 소지중인 아이템 목록:");
            Console.WriteLine();
            Console.WriteLine("--------아이템 이름--------------------------아이템 설명---------------------");
            Console.WriteLine();
            //인벤토리 리스트에 있는 아이템들 나열
            for (int i = 0; i < player.Inventory.Count; i++)
            {
                var item = player.Inventory[i];
                Console.Write($"{i + 1}. ");
                Console.WriteLine($" \t {item.ItemName} \t | {item.ItemDescription}"); //아이템 부가 정보
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("============================================================================");
            Console.WriteLine();
            Console.WriteLine(" 장착/해제를 원하는 아이템을 입력해주세요.");
            Console.WriteLine();
            Console.WriteLine(" 0. 뒤로가기");
            Console.Write(">>");
    
            int input = CheckValidInput(0, player.Inventory.Count);
            if (input > 0)
            {
    
                Console.WriteLine();
                Console.WriteLine("Press AnyKey");
                Console.ReadKey();
                //인벤토리창 새로고침
                DisplayInventory(player);
            }
            else
            {
                //막사로 돌아가기
                Home();
            }
        }
    #endregion

    #region 일과 ( 상시 이벤트 )
    // 일과 ( 상시 이벤트 )
    static void DailyRoutineScene()
    {
        // Cursor && Scene 초기화 값
        int cursor = 0;
        bool onSecne = true;

        // Text 배열
        string[] text = { "1. 체력 단련", "2. 주특기 훈련", "3. 행보관님 작업" };

        while (onSecne)
        {
            // 화면 초기화
            Console.Clear();

            Console.WriteLine("오늘 하루도 힘내보자.");
            Console.WriteLine("");
            Console.WriteLine("어떤일을 해볼까?");
            Console.WriteLine("");

            // Text[] Output
            TextChoice(Cursor, text);
            // Key Input
            e = Console.ReadKey();
            // Cursor Index
            cursor = CursorChoice(e, cursor, text, onSecne);
        }

        switch(cursor)
        {
            case 0:
                PhysicalTrainingScene();
                break;
            case 1:
                SpecialityScene();
                break;
            case 2:
                WorkScene();
                break;
            default:
                break;
        }
    }
    #endregion

    #region 체력 단련 ( 상시 이벤트 선택지 )
    // 체력 단련 ( 상시 이벤트 선택지 )
    static void PhysicalTrainingScene()
    {
        // Cursor && Scene 초기화 값
        int cursor = 0;
        bool onSecne = true;

        // Text 배열
        string[] text = { "1. 구보", "2. 팔굽혀펴기", "3. 윗몸 일으키기", "4. 턱걸이" };

        while (onSecne)
        {
            Console.WriteLine("건강한 육체에 건강한 정신이 깃든다!");
            Console.WriteLine("오늘은 어떠한 운동으로 나의 육체를 단련해 볼까?");
            Console.WriteLine("");

            // Text 배열 출력
            TextChoice(cursor, text);
            // Key 입력
            e = Console.ReadKey();
            // Cursor index
            cursor = CursorChoice(e, cursor, text, onScene);
        }

        switch (cursor)
        {
            case 0:
                // 구보
                break;
            case 1:
                // 팔굽
                break;
            case 2:
                // 윗몸
                break;
            case 3:
                // 턱걸이
                break;
            default:
                break;
        }
    }
    #endregion

    #region 주특기 훈련 ( 상시 이벤트 선택지 )
    // 주특기 훈련 ( 상시 이벤트 선택지 )
    static void SpecialityScene()
    {
        // Cursor && Scene 초기화 값
        int cursor = 0;
        bool onSecne = true;

        // Text 배열
        string[] text = { "1. 본 주특기", "2. 공통 주특기"};

        while (onSecne)
        {
            Console.WriteLine("실전처럼 훈련하고 훈련한 대로 싸운다!");
            Console.WriteLine("The Only Easy Day Was YesterDay");
            Console.WriteLine("어떤 주특기를 훈련할까?");
            Console.WriteLine("");

            // Text 배열 출력
            TextChoice(cursor, text);
            // Key 입력
            e = Console.ReadKey();
            // Cursor index
            cursor = CursorChoice(e, cursor, text, onScene);
        }

        switch (cursor)
        {
            case 0:
                // 본 주특기
                break;
            case 1:
                // 공통 주특기
                break;
            default:
                break;
        }
    }
    #endregion

    #region 작업 ( 상시 이벤트 선택지)
    // 작업 ( 상시 이벤트 선택지)
    static void WorkScene()
    {
        // Cursor && Scene 초기화 값
        int cursor = 0;
        bool onSecne = true;

        // Text 배열
        string[] text = { "1. 예초", "2. 제설", "3. 삽질" };

        while (onSecne)
        {
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
            cursor = CursorChoice(e, cursor, text, onScene);
        }

        switch (cursor)
        {
            case 0:
                // 예초
                break;
            case 1:
                // 제설
                break;
            case 2:
                // 삽질
                break;
            default:
                break;
        }
    }
    #endregion

    //이등병 스토리
    static void Basic(Character player)
    {
        Console.Clear();

        Console.WriteLine("드디어 훈련병 생활이 끝났군");
        Console.Write("이제 자대에서 열심히 해보자!");
        Console.WriteLine("");
        Console.WriteLine("자대배치 후 첫 아침점호 시간이다");
        Console.WriteLine("");
        Console.WriteLine("긴장한 상태로 열을 맞춰서있다..");
        Console.WriteLine("그때 한 선임이 \"굳건이 군화 닦았어 ? \"");
        Console.WriteLine("");
        Console.WriteLine("그 순간 머리가 하애졌다..");
        Console.WriteLine("");

        Random Shoes = new Random();
        int number = Shoes.Next(2);
        if (number == 0)
        {
            Console.WriteLine("네 닦았습니다!");
            Console.WriteLine("아무일도 일어나지 않았다");
        }
        else
        {
            player1.Hp -= 30;
            Console.WriteLine("");
            Console.WriteLine("아 미쳐 닦지 못했습니다..");
            Console.WriteLine("하.. 아침부터 큰일이네;; ");
            Console.WriteLine("체력이 30 감소했습니다.");
        }
        // 만약 굳건이가 군화를 닦았다면 아무일도 일어나지 않는다
        // 굳건이가 군화를 안닦았다면 -hp  확률 50%

        OneMonthLater();

    }

    static void Basicstory(Character player)
    {
        Console.Clear();

        Console.WriteLine("오늘도 다사다난한 하루였고만");
        Console.WriteLine("이제 군화닦고 청소를 시작해보자");
        Console.WriteLine("오늘은 화장실 청소하는 날이네");
        Console.WriteLine("선임한테 잘보이기 위해 내가 변기를 맡아서 열심히 닦아야겠다");
        Console.WriteLine("아니 오줌 조준못하는건 이해하는데 똥 조준은 왜 못하는거야?");
        Console.WriteLine("라는 생각과 함꼐 청소가 끝났다!!");
        Console.WriteLine("드디어 오늘 하루도 끝나가는군!!");
        Console.WriteLine("");
        Console.WriteLine("----(저녁 점호 시간)-----)");
        Console.WriteLine();
        Console.Write("정적이 흐른다... 그때");
        Console.WriteLine("분대장: 굳건이 혹시 여자친구나 여동생이나 누나 있어?");
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
        Console.WriteLine("지옥의 PT체조가 시작됐다.");
        Console.WriteLine("\"지금부터 대답은 \'네\'가 아니라 \'악\'으로 대체합니다.\"");
        Console.WriteLine("악!");
        Console.WriteLine("\"PT체조 8번 온몸비틀기 준비!\"");
        Console.WriteLine("교관은 쉽게 갈 생각이 없는거같다 살아남자!");
        Console.WriteLine();
        Console.WriteLine("1.유-격!");
        //확률에 따라 성공 혹은 실패
        //실패마다 정신력, 체력 감소
        //실패시 출력멘트
        while (true)
        {
            double randomValue = random.NextDouble(); //0.0 이상 1.0 미만의 랜덤 실수

            if (randomValue < success)
            {
                Console.WriteLine("\"교육생들 수고 많았습니다.\"");
                Console.WriteLine("\"본 교관 나쁜사람 아닙니다.\"");
                Console.WriteLine("\"교육생들 막사로 가서 쉬도록합니다.\"");
                Console.WriteLine("");
                Console.WriteLine("지옥같은 유격훈련이 끝났다... 돌아가자.");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("  를 선택해 막사로 돌아가자");
                //성공시 스텟증가 추가해야됨
                Console.ReadKey();
                Home();
                return;
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
                //추가로 실패시 정신력, 체력 감소 추가해야됨
            }
        }
        
        //switch (randomNumber)
        //{
        //    case 0:
        //        Console.WriteLine("교육생들 수고 많았습니다.");
        //        break; //성공 스텟증가
        //    case int n when n >= 1 && n <= 6:
        //        Console.WriteLine("목소리 크게 합니다. 다시!");
        //        break; //실패1 정신력 체력 감소
        //   case int n when n >= 7 && n <= 13:
        //       Console.WriteLine("누가 마지막 구호를 외쳐! 다시!");
        //       break; //실패2 정신력 체력 감소
        //   case int n when n >= 14 && n <= 19:
        //      Console.WriteLine("똑바로 합니다. 다시!");
        //      break; //실패3 정신력 체력 감소
        //}
    }

    //일병 스토리 - 경계근무
    static void FStoryPullSecurity()
    {
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine("어두운 새벽 경계근무중...");
        Console.WriteLine("저 앞 풀숲에서 부스럭거리는 소리가 난다.");
        Console.WriteLine("야생의 고라니와 멧돼지가 나타났다!");
        Console.WriteLine("전투 시작!");
        Console.WriteLine();
        Console.WriteLine("1.소리지르기");
        Console.WriteLine("2.돌 던지기");
        //전투

    }

    #region 상병 스토리
    //상병 스토리 - KCTC
    static void CStoryKCTC()
    {
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine("당신은 KCTC 훈련에 참여했다.");
        Console.WriteLine("훈련 2일차, 앞에 대항군을 발견했다는 무전이 들어왔다.");
        Console.WriteLine("전투 시작!");
        Console.WriteLine();

        Console.WriteLine("적을 공격한다.");
        Console.WriteLine("자주포 폭격 10% / 수류탄 투척 20% / K-2사격 50% / 매복으로 인한 패배 20%");
        Console.WriteLine("");
        //직업 별, 다른 확률
        Random rand = new Random();
        int number = rand.Next(10);
        switch (number)
        {
            case 0: //10%
                Console.WriteLine("폭격 지원 요청 성공!");
                Console.WriteLine("적 대대 소탕 완료.");
                //제일 큰 보상
                break;

            case 1: case 2: //20%
                Console.WriteLine("수류탄 투척!");
                Console.WriteLine("적 분대 소탕 완료.");
                //중간 보상
                break;

            case 3: case 4: case 5: case 6: case 7: //50%
                Console.WriteLine("K-2로 적 사살");
                Console.WriteLine("대항군 한명 사살.");
                //보상 조금
                break;

            case 8: case 9: //20%
                Console.WriteLine("아군 전멸");
                //패널티
                break;
        }
        Console.WriteLine("훈련이 끝났다. 막사로 돌아가자.");
        Console.ReadKey();
        OneMonthLater();

    }
    //상병 스토리- 상검
    static void CStoryPhysicalExamination(Character player)
    {
        Console.Clear();
        Console.WriteLine("상병 신검 날이 되었다.");
        Console.WriteLine("국군 병원으로 가는 버스에 탔다.");
        Console.WriteLine("...");
        Console.WriteLine("병원에 도착했다.");
        Console.WriteLine("잠시 대기 후 신체검사를 시작했다.");
        Console.WriteLine("...");
        Console.WriteLine("========================");
        Console.WriteLine($"이름: {player.Name}");
        Console.WriteLine("체중: 70.3kg");
        Console.WriteLine("키: 175.4cm");
        Console.WriteLine("...");
        Console.WriteLine("========================");

        Console.WriteLine("상검이 끝나고 국군병원 근처에서 몰래 치킨을 먹으려 한다.");
        Console.WriteLine("시도해볼까?");
        Console.WriteLine("1. 몰래 탈출해 치킨을 먹는다."); //(성공확률 40% 실패확률 60%)

        Console.WriteLine("2. 얌전히 부대로 가서 짬밥을 먹는다.");
        int input = CheckValidInput(1, 2);
        Random rand = new Random();
        int chicken = rand.Next(5);
        switch (input)
        {
            case 1:
                //치킨시도
                switch (chicken)
                { 
                    case 0: case 1://성공 40퍼
                        Console.WriteLine("성공!");
                        Console.WriteLine("맛있는 치킨을 먹었다.");
                        Console.WriteLine("정신력이 증가한다.");
                        Console.WriteLine("체력이 회복되었다.");
                        //정신력 5 증가 체력 증가
                        player1.Mind += 5;
                        player1.Hp += 10;
                        player1.Gold -= 100;
                        break;

                    case 2: case 3: case 4: //실패 60퍼
                        Console.WriteLine("실패!");
                        Console.WriteLine("간부에게 죽도록 털렸다.");
                        Console.WriteLine("정신력이 감소했다.");
                        Console.WriteLine("체력이 감소했다.");

                        //정신력 1 감소, 체력 감소
                        player1.Mind--;
                        player1.Hp -= 10;
                        break;
                }
                break;

            case 2:
                //부대에서 짬밥
                Console.WriteLine("얌전히 부대로 복귀한다.");
                Console.WriteLine("맛없는 똥국이다...");
                Console.WriteLine("정신력이 감소했다.");
                Console.WriteLine("체력이 회복되었다.");
                player1.Mind--;
                player1.Hp += 10;
                break;
        }
        OneMonthLater();

    }
    //상병 스토리-전준태
    static void CSDefcon(Character player)
    {
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine("웨에에에엥-");
        Console.WriteLine("화스트 페이스. 화스트 페이스.");
        Console.WriteLine("타다다닥-");
        Console.WriteLine("생활관으로 달려가서 개인 군장을 챙기고 물자를 챙긴다.");
        Console.WriteLine("물자를 챙기는 와중에 팔이 뻐근함을 느낀다.");
        Console.WriteLine("체력 테스트. 체력 n 이상"); //수정가능
        if (player1.Str >= 10)
        {
            Console.WriteLine("아슬아슬했지만 안정적으로 물자를 다 옮기는데 성공했다.");
        }
        else
        {
            Console.WriteLine("물자를 옮기던 와중 쏟아버렸다.");
            Console.WriteLine("간부의 엄청난 쿠사리가 쏟아진다.");

        }
    }
    //상병 스토리 - 대침투 훈련
    static void CSTest()
    {
        
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine("오늘은 대침투 훈련을 한다.");
        Console.WriteLine("부대 근처 야산으로 가서 총을 거치 하고 참호를 파기 시작한다");
        //땅파기
        French(french);
        Console.WriteLine("\"거수자 발견시 보고하고. 알지? 위장한 간부 잡으면 포상인거?\"");
        Console.WriteLine("의욕이 셈솟기 시작한다.");
        Console.WriteLine("하염없이 숨어있으니, 길 너머 풀숲에서 부스럭 거리는 소리가 들린다.");
        Console.WriteLine("조심스럽게 접근한다.");
        Console.ReadKey();
        Random rand = new Random();
        int attack = rand.Next(2);
        switch(attack){
            case 1:
            Console.WriteLine("기습성공");
            Console.WriteLine("안정적으로 거수자를 제압했다.");

            break;
            case 2:
            Console.WriteLine("기습실패");
            
            break;
        }


    }
    //참호 전투
    static void French(Enemy enemy)
    {
        //판 깊이. = 100cm - 적 체력
        //남은 깊이 = 적 체력

        int depth = (100 - enemy.EnemyHp) / 10;

        Console.Clear();
        Console.WriteLine();
        Console.Write("의 남은 깊이: ");
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine($"{enemy.EnemyHp} cm");
        Console.ResetColor();
        Console.WriteLine($"남은 기회: {enemy.EnemyAtk}");
        Console.WriteLine("=땅===------------------=====");

        for (int i = 0; i <= depth; i++)
        {
            Console.WriteLine("     =                  =    ");
        }
        Console.WriteLine("     ====================");
        Console.WriteLine();
        Console.WriteLine("삽질하기");
        Console.WriteLine();
        Console.ReadKey();

        if(enemy.EnemyHp > 0)
        {
            if (enemy.EnemyAtk > 0)
            {
                enemy.EnemyHp -= player1.Str;
                if (enemy.EnemyHp < 0)
                {
                    Console.WriteLine($"땅을{player1.Str + enemy.EnemyHp}cm 만큼 팠습니다.");
                    enemy.EnemyHp = 0;
                }
                else
                {
                    Console.WriteLine($"땅을 {player1.Str}cm 만큼 팠습니다.");
                }

                Console.ReadKey();
                enemy.EnemyAtk--;
                French(french);
            }
            else
            {
                Console.WriteLine("제한 시간 내에 땅을 다 못팠다.");
            }
        }
        else
        {
            Console.WriteLine("땅파기 끝");
        }

    }


    
    //상병 스토리- 분대장 교육
    static void CSschool()
    {
        Console.Clear();
        Console.WriteLine("어쩌다보니 분대장으로 뽑혔다.");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("분대장 교육대로 이동중...");
        Console.ResetColor();
        Console.WriteLine("분대장 교육대에서 받은 성적을 통해 추가 보상이 있을 예정이다.");


    }
    //상병 스토리- 보스몹 초임 소대장
    static void CSNewCommander(Character player, Enemy enemy)
    {
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine("오늘은 새로운 소위가 임관하는 날이다.");
        Console.WriteLine("관상부터 FM인게 보인다.");
        Console.WriteLine("ㅈ된듯 하다.");
        Console.WriteLine("얼마 후...");
        Console.WriteLine($"{enemy.EnemyName}: 이봐 {player.Name} 상병. ");
        Console.WriteLine($"상병 {player.Name}. 무슨일이십니까? ");
        Console.WriteLine($"{enemy.EnemyName}: 배수로 작업 하러 가지.");
        Console.WriteLine("얼마 후...");
        Console.WriteLine("얼마 후...");
        Console.WriteLine("얼마 후...");
        Console.WriteLine("얼마 후...");
        Console.WriteLine("얼마 후...");

    }
    static void CSCommanderKill(Character player, Enemy enemy)
    {
        if(player1.Hp > 0 && enemy.EnemyHp > 0)
        {
            Console.Clear();
            Console.WriteLine("전투시작");
            Console.WriteLine("================================");
            Console.Write("소대장의 체력: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{enemy.EnemyHp}");
            Console.ResetColor();
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"{player.Name}의 체력: {player.Hp}");
            Console.ResetColor();
            Console.WriteLine("================================");
            Console.WriteLine("이번턴에 하실 행동을 골라주세요");
            Console.WriteLine("");
            Console.WriteLine("1. 공격하기");
            Console.WriteLine("2. 방어하기");
            Console.WriteLine("");
        
            int input = CheckValidInput(1,2);
            if (input==1)
            {
                Random rand = new Random();
                int number = rand.Next(player.Luk);
                if(number <= 5) //따로 추가 스탯 없을경우 평타
                {
                    enemy.EnemyHp -= player.Str;
                    Console.WriteLine("당신의 공격!");
                    Thread.Sleep(500);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"{player.Str}");
                    Console.ResetColor();
                    Console.WriteLine("의 데미지를 주었다.");
                    Thread.Sleep(500);
                    Console.WriteLine($"{enemy.EnemyName}의 공격!");
                    Console.WriteLine($"{enemy.EnemyAtk}만큼의 데미지를 입었다.");
                    Console.ReadKey();
                    CSCommanderKill(player1, newCommander);

                }

                else //치명타 (luk의 추가 스탯이 많을 수록 확률이 올라감)
                {
                    enemy.EnemyHp -= player.Str + player.Luk;
                    Console.WriteLine("당신의 공격");
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("치명타!");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("{0}", player1.Str + player.Luk);
                    Console.ResetColor();
                    Console.WriteLine("만큼의 데미지를 주었다.");
                    Thread.Sleep(500);
                    Console.WriteLine($"{enemy.EnemyName}의 공격!");
                    Console.WriteLine($"{enemy.EnemyAtk}만큼의 데미지를 입었다.");
                    Console.WriteLine();
                    Console.ReadKey();
                    CSCommanderKill(player1, newCommander);
                    
                }

            }
        }
        OneMonthLater();

    }


    #endregion

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


        while (onScene)
        {
            // Random Number 설정
            randomNum = random.Next(1, 10);

            //화면 초기화
            Console.Clear();

            Console.WriteLine("드디어 100일 휴가를 나왔다!");
            Console.WriteLine("어떤 일을 먼저 해볼까?");

            // Text[] Output
            TextChoice(cursor, text);
            // Key Input
            e = Console.ReadKey();


            switch (e.Key)
            {
                case ConsoleKey.UpArrow:
                    cursor--;
                    if (cursor < 0) cursor = text.Length - 1;
                    break;
                case ConsoleKey.DownArrow:
                    cursor++;
                    if (cursor > text.Length - 1) cursor = 0;
                    break;
                case ConsoleKey.Enter:
                    onScene = false;
                    break;
                default:
                    break;
            }

            // Cursor Index
            cursor = CursorChoice(e, cursor, text, onScene);


            // Cursor Index
            cursor = CursorChoice(e, cursor, text, onScene);
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
                break;
            case 1:
                // 친구들 만나러
                OneHundredDaysEvent(randomNum, "오랜만에 친구들과 술 한잔하며 이야기했다.",
                "친구들과 Pc방에 가서 시간 가는 줄 모르고 놀았다.",
                "나는 친구가 없다...");
                // Scene이동
                break;
            case 2:
                // 본가로 간다
                OneHundredDaysEvent(randomNum, "오랜만에 집에 왔건만 군대에서 뭐했냐며 잔소리만 들었다...",
                "가족들과 오랜만에 식사하며 좋은 시간을 보냈다.",
                "내가 오는 줄 몰랐나..? 아무도 없다...");
                // Scene이동
                break;
            case 3:
                // 혼자 논다
                OneHundredDaysEvent(randomNum, "혼자 즐겁게 놀았다. 진짜 즐거운 거 맞다, 아마도..",
                "여기저기 구경 다니며 신나게 놀았다.",
                "생활관에 있을 때가 더 나은 거 같다 너무 외롭다..");
                // Scene이동
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
        Console.ReadKey();

        for (int currentWave = 1; currentWave <= totalWave; currentWave++)
        {
            // 10웨이브 반복
            while (onScene)
            {
                // 화면 초기화
                Console.Clear();

                // cursor위치 초기화
                cursor = 0;

                // Random 거리 초기화 ( 200m , 100m, 50m )
                num = random.Next(0, 2);

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

                switch (e.Key)
                {
                    case ConsoleKey.UpArrow:
                        cursor--;
                        if (cursor < 0) cursor = text.Length - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        cursor++;
                        if (cursor > text.Length - 1) cursor = 0;
                        break;
                    case ConsoleKey.Enter:
                        onScene = false;
                        break;
                    default:
                        break;
                }

                // Cursor index
                cursor = CursorChoice(e, cursor, text, onScene);

            }

            // 화면 지우기
            Console.Clear();

            // 사격 로직 및 명중 횟수++
            hitCount = ShootingEvent(num, hitCount, cursor);

            // 웨이브 + 1
            currentWave++;

            onScene = true;
        }

        // hitCount(명중 횟수)에 따른 보상 로직 작성.
        // 1~5 폐급, 6~8 평균, 9~10 특등사수 
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

    /*
    static void CursorChoice(ReadKey e, int _cursor, string _text[])

    #endregion

    #region Cursor선택 캡슐화
    // Cursor선택 메서드
    static int CursorChoice(ConsoleKeyInfo e, int _cursor, string[] _text, ref bool _onScene)


    {
        switch (e.Key)
        {
            case ConsoleKey.UpArrow:
                cursor--;
                if (_cursor < 0) cursor = _text.Length - 1;
                break;
            case ConsoleKey.DownArrow:
                cursor++;
                if (_cursor > _text.Length - 1) cursor = 0;
                break;
            case ConsoleKey.Enter:
                onScene = false;
                break;
            default:
                break;
        }
    }


    */


    #endregion

    #region Text 선택지 출력 캡슐화
    // Text 출력 캡슐화
    static void TextChoice(int _cursor, string[] _text)
    {
        for (int i = 0; i < _text.Length; i++)
        {
            if (_cursor == i) Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(_text[i]);
            Console.Clear();
        }
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

        Console.WriteLine("=======================================");
        Console.WriteLine("10번의 삽질을 시도해서 6번 성공하세요!");
        Console.WriteLine("=======================================");
        Console.WriteLine("");
        Console.ReadKey();
        Console.Clear();

        Console.WriteLine("삽질 시작하기");
        Console.ReadKey();


        int sucessCount = 0;
        Random random = new Random();

        string userInput = Console.ReadLine();
        while (true)
        {

            for (int i = 0; i <= 10; i++)
            {
                bool fireControlSuccess = random.Next(0, 2) == 0; // 50%확률로 성공

                if (fireControlSuccess)
                {
                    sucessCount++;
                    Console.WriteLine($"{i}. 삽질에 성공했습니다!");
                }
                else
                {
                    Console.WriteLine($"{i}. 삽질에 실패했습니다.");
                }
                break;
            }

            Console.WriteLine("======================================================");
            Console.WriteLine($"결과: 10번에 삽질 중 {sucessCount}번 성공했습니다!");
            Console.WriteLine("======================================================");
            Console.WriteLine("1. 결과확인하기");
            Console.ReadLine();
            Console.Clear();

            if (sucessCount >= 6)
            {
                Console.WriteLine("");
                Console.WriteLine("대민지원을 완료했습니다.");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("체력이 50증가합니다.");
                Console.ResetColor();
                Console.WriteLine("");
                player1.Hp += 50;

            }
            else
            {
                Console.WriteLine("대민지원을 실패했습니다.");
                Console.WriteLine("다시 시도하시겠습니까? (Y)");
                Console.WriteLine("나가시겠습니까? (N)");
                string response = Console.ReadLine();
                if (response.ToUpper() == "Y")
                {
                    continue; // 실패시 다시 시작
                }
                else
                {
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
                    Console.WriteLine("가족을 선택하셨습니다.");
                    Console.WriteLine("");
                    Console.WriteLine("가족은 당신의 안정과 지지를 의미합니다.");
                    Console.ReadKey();
                    Console.Clear();
                    Console.WriteLine("그들과 함께하는 시간은 당신에게 힘을 주고");
                    Console.WriteLine("");
                    Console.WriteLine("당신은 그들을 위해 힘든 시간을 견디려고 노력할 것입니다.");
                    Console.ReadKey();
                    Console.Clear();
                    Console.WriteLine("");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("힘 능력치가 10 상승하였습니다.");
                    Console.ResetColor();
                    player1.Str += 10;
                    isValidInput = false;
                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine("");
                    Console.WriteLine("당신은 친구를 선택했습니다.");
                    Console.WriteLine("");
                    Console.WriteLine("김밥천국가서 대충먹고 PC방가서 날밤을 깠습니다.");
                    Console.ReadKey();
                    Console.Clear();
                    Console.WriteLine("");

                    Console.WriteLine("친구들과 함께하는 시간은 즐거웠습니다.");
                    Console.ReadKey();
                    Console.Clear();
                    Console.WriteLine("");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("정신력 능력치가 10 상승하였습니다.");
                    player1.Mind += 10;
                    isValidInput = false;
                    break;
                case "3":
                    Console.Clear();
                    Console.WriteLine("여자친구를 선택하셨습니다.");
                    Console.WriteLine("");
                    Console.WriteLine("그녀에게 전화 했습니다. 전화를 안받습니다...");
                    Console.ReadKey();
                    Console.Clear();
                    Console.WriteLine("다시 한번 전화를 걸었습니다...");
                    Console.WriteLine("");
                    Console.ReadKey();
                    Console.WriteLine("전화를 받았습니다!!");
                    Console.ReadKey();
                    Console.Clear();
                    Console.WriteLine("");
                    Console.WriteLine("나 남자친구 생겼어 이제 전화하지말아줬으면 좋겠어 미안 툭 뚜..뚜..뚜..뚜");
                    Console.ReadKey();
                    Console.WriteLine("");
                    Console.WriteLine("이로 인해 당신은 좌절하고 실망하며 슬픔을 겪게 됩니다.");
                    Console.WriteLine("");
                    Console.ReadKey();
                    Console.Clear();
                    Console.WriteLine("");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("정신력 능력치가 10 하락하였습니다.");
                    Console.ResetColor();
                    player1.Mind -= 10;
                    isValidInput = false;
                    break;

                default:
                    Console.WriteLine("");
                    Console.WriteLine("잘못된 선택입니다.");
                    break;
            }
        }
    }






    static int workCount = 0;
    static int Perfection = 0;
#region 혹한기
    static void ColdWeatherTraining1()
    {
        Console.Clear();
        Console.WriteLine("혹한기 훈련이 시작되었다.");
        Console.WriteLine("혹한기 일정동안 px는 잠시 폐쇠한다고 한다.");
        Console.WriteLine();
        Console.WriteLine($"1. 다행이다 미리 준비를 해놨어");
        Console.WriteLine($"2. 큰일이다 이번 혹한기는 살아남을 수 있을까");
        Console.WriteLine();
        int input = CheckValidInput(1, 2);
        switch (input)
        {
            case 1:
                ColdWeatherTraining2(); //인벤토리에 장비와 음식이 구비되어 있으면 선택 가능
                break;
            case 2:
                ColdWeatherTraining2();
                break;
        }
    }
    static void ColdWeatherTraining2()
    {
        Console.Clear();
        Console.WriteLine("뒷산에 24인용 텐트로 중대 본부를 설치해야한다.");
        Console.WriteLine("우리 중대장님이 힘이 없는건가, 땅 밑에 바위가 많은 곳을 배정받았다.");
        Console.WriteLine("바위를 깨야한다.");
        Console.WriteLine();
        Console.WriteLine($"1. 경계근무라고 이빨친 뒤 작업에서 빠진다.");
        Console.WriteLine($"2. 간부님들도 많은데 내가 빠지기엔 눈치 보인다.");
        Console.WriteLine();
        int input = CheckValidInput(1, 2);
        switch (input)
        {
            case 1:
                ColdWeatherTrainingBattle1(player1.Dex);
                break;
            case 2:
                ColdWeatherTraining3();
                break;
        }
    }
    static void ColdWeatherTrainingBattle1(int stat)
    {
        bool eventOccurred = EventOccur(player1.CalculateProbability(stat));
        if (eventOccurred)
    {
Console.WriteLine("행보관님에게 걸렸다.");
        }
        else
        {
            Console.WriteLine("개꿀 일과 빼먹었다.");
            Console.WriteLine("press any Key");
            
        }

        //행보관님과 배틀
    }
    static void ColdWeatherTraining3()
    {
        Console.Clear();
        Console.WriteLine("행보관님이 곡괭이를 들고 땅을 내리치고 있다.");
        Console.WriteLine("옆에선 소대장님이 철조망을 치려고 병사들을 부르고 있다.");
        Console.WriteLine("누굴 도와야 할까");
        Console.WriteLine();
        Console.WriteLine($"1. 행보관님을 도와 바위를 깬다.");
        Console.WriteLine($"2. 소대장님과 같이 철조망을 친다.");
        Console.WriteLine();
        int input = CheckValidInput(1, 2);
        switch (input)
        {
            case 1:
                ColdWeatherTrainingNCORoot();
                break;
            case 2:
                ColdWeatherTrainingNCORoot();
                break;
        }
    }

    static void ColdWeatherTrainingNCORoot()
    {
        Console.Clear();
        Console.WriteLine("행보관님께서 손 다친다며 장갑을 주셨고 곡괭이를 넘겨 받아 바위를 깨기 시작했다.");
        Console.WriteLine("곡괭이질 몇번하니 힘이 빠지기 시작했다. 내가 힘이 빠지는게 보이자");
        Console.WriteLine("행보관님께서 다시 교대를 하자고 하신다.");
        Console.WriteLine();
        Console.WriteLine($"1. 이 바위까지만 제가 깨겠습니다.");
        Console.WriteLine($"2. 행보관님께 바로 곡괭이를 드린다.");
        Console.WriteLine();
        int input = CheckValidInput(1, 2);
        switch (input)
        {
            case 1:
                ColdWeatherTrainingNCORoot1();
                break;
            case 2:
                ColdWeatherTrainingNCORoot2();
                break;
        }
    }

    static void ColdWeatherTrainingNCORoot1()
    {
        Console.Clear();
        Console.WriteLine("내가 곡괭이질 하는 동안 행보관님께서 막사에 다녀오셨다.");
        Console.WriteLine("막사에 다녀온 행보관님께서 착암기를 가져오셨다.");
        Console.WriteLine("역시 행보관님이야");
        Console.WriteLine();
        Console.WriteLine($"1. 착암기로 돌을 부순다.");
        Console.WriteLine();
        int input = CheckValidInput(1, 1);
        switch (input)
        {
            case 1:
                ColdWeatherTraining5();
                break;

        }
    }
    static void ColdWeatherTrainingNCORoot2()
    {
        Console.Clear();
        Console.WriteLine("행보관님과 번갈아 곡괭이 질을 하기 시작했다.");
        Console.WriteLine("오랜 작업으로 힘이 많이 빠졌다.");
        Console.WriteLine("병장인데 이런 고생을 해야하나");
        Console.WriteLine();
        Console.WriteLine($"1. 소대장님한테 붙을걸 그랬다.");
        Console.WriteLine();
        int input = CheckValidInput(1, 1);
        switch (input)
        {
            case 1:
                ColdWeatherTraining5();
                break;
        }
    }
    static void ColdWeatherTrainingCORoot()
    {
        Console.Clear();
        Console.WriteLine("여기가 사람이 많아서 더 쉬워보인다.");
        Console.WriteLine("2단3열 윤형 철조망을 쳐야한다.");
        Console.WriteLine("소대장님이 나서서 뭔가를 하려고 한다.");
        Console.WriteLine();
        Console.WriteLine($"1. 가만히 지켜본다.");
        Console.WriteLine($"2. 소대장님을 도와준다.");
        Console.WriteLine();
        int input = CheckValidInput(1, 2);
        switch (input)
        {
            case 1:
                ColdWeatherTrainingCORoot1();
                break;
            case 2:
                ColdWeatherTrainingCORoot1();
                break;
        }
    }
    static void ColdWeatherTrainingCORoot1()
    {
        Console.Clear();
        //Console.WriteLine("소대장님이 사고를 쳤다.");
        //Console.WriteLine("철조망이 서로 얽혀서 모양이 이쁘지 않게 되었다.");

        Console.WriteLine("1단에 두개의 철조망을 깔고 2단에 한개의 철조망을 올려야한다. ");
        Console.WriteLine();
        Console.WriteLine($"1. 가만히 지켜본다.");
        Console.WriteLine($"2. 같이 작업을 시작한다.");
        Console.WriteLine();
        int input = CheckValidInput(1, 2);
        switch (input)
        {
            case 1:
                ColdWeatherTraining5();
                break;
            case 2:
                ColdWeatherTraining5();
                break;
        }
    }

    static void ColdWeatherTraining4()
    {
        Console.Clear();
        Console.WriteLine("숙영하기 위해 D형 텐트를 쳐야한다.");
        Console.WriteLine("숙영하기 위해 D형 텐트를 쳐야한다.");
        Console.WriteLine("옆에선 소대장님이 철조망을 치려고 병사들을 부르고 있다.");
        Console.WriteLine("누굴 도와야 할까");
        Console.WriteLine();
        Console.WriteLine($"1. 행보관님을 도와 바위를 깬다.");
        Console.WriteLine($"2. 소대장님과 같이 철조망을 친다.");
        Console.WriteLine();
        int input = CheckValidInput(1, 2);
        switch (input)
        {
            case 1:
                ColdWeatherTrainingNCORoot();
                break;
            case 2:
                ColdWeatherTrainingCORoot();
                break;
        }
    }

    static void ColdWeatherTraining5()
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
        Console.WriteLine($"1. 무시한다.");
        Console.WriteLine($"2. 경계한다.");
        Console.WriteLine();
        int input = CheckValidInput(1, 2);
        switch (input)
        {
            case 1:
                ColdWeatherTrainingBattle2(); //데미지 받고 시작
                break;
            case 2:
                ColdWeatherTrainingBattle2(); //데미지 받지 않고 시작
                break;
        }
    }
    static void ColdWeatherTrainingBattle2()
    {
        //고라니와 배틀
    }

    static void ColdWeatherTraining6()
    {
        //혹한기 훈련 끝
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
        Console.WriteLine($"1. 무시한다.");
        Console.WriteLine($"2. 경계한다.");
        Console.WriteLine();
        int input = CheckValidInput(1, 2);
        switch (input)
        {
            case 1:
                ColdWeatherTrainingBattle2(); //데미지 받고 시작
                break;
            case 2:
                ColdWeatherTrainingBattle2(); //데미지 받지 않고 시작
                break;
        }
    }
#endregion
    #region 작업
    static void HardWork()
    {
        workCount++;
        Console.Clear();
        Console.WriteLine("아침에 행정반을 가니 행보관님계서 두가지 선택권을 주셨다.");
        Console.WriteLine("하나는 행보관님과 공구리를 치는것이고 하나는 보급병과 창고정리를 하는 것이다.");
        Console.WriteLine();
        Console.WriteLine($"1. 행보관님과 공구리 작업");
        Console.WriteLine($"2. 보급병과 창고정리");
        Console.WriteLine();
        int input = CheckValidInput(1, 2);
        switch (input)
        {
            case 1:
                CementWork1();
                break;
            case 2:
                WarehouseWokr1();
                break;
        }
    }

    static void CementWork1()
    {
        Console.Clear();
        Console.WriteLine($"완성도 : {Perfection} 남은 시간 : {10 - workCount}");
        Console.WriteLine("시멘트 포대를 옮겨야 한다.");
        Console.WriteLine("말년에는 떨어지는 낙엽도 조심하라고 하는데 나에게는 너무 가혹한 일이다.");
        Console.WriteLine("나와 같이 배정받은 후임들이 보인다.");
        Console.WriteLine();
        Console.WriteLine($"1. 후임들에게 시키고 관리 감독을 한다");
        Console.WriteLine($"2. 후임들과 함께 포대를 옮긴다.");
        Console.WriteLine();
        int input = CheckValidInput(1, 2);
        switch (input)
        {
            case 1:
                workCount += 1;
                Perfection += 1;
                CementWork2();
                break;
            case 2:
                workCount += 1;
                Perfection += 3;
                CementWork2();
                break;
        }
    }
    static void CementWork2()
    {
        Console.Clear();
        Console.WriteLine($"완성도 : {Perfection} 남은 시간 : {10 - workCount}");
        Console.WriteLine("시멘트를 물과 섞어야한다.");
        Console.WriteLine("옆에는 교회가 있고 군종병이 청소를 한다고 문을 열어뒀다.");
        Console.WriteLine("주변을 둘러보니 간부는 보이지 않는다.");
        Console.WriteLine();
        Console.WriteLine("1. 교회에 들어가서 한숨 잔다.");
        Console.WriteLine("2. 후임들을 믿을 수 없다 직접 시멘트를 만든다.");
        int input = CheckValidInput(1, 2);
        switch (input)
        {
            case 1:
                workCount += 2;
                Perfection = 1; //확률 넣어야함
                CementWork3();
                break;
            case 2:
                workCount += 1;
                Perfection += 2;
                CementWork3();
                break;
        }
    }
    static void CementWork3()
    {
        Console.Clear();
        Console.WriteLine($"완성도 : {Perfection} 남은 시간 : {10 - workCount}");
        Console.WriteLine("점심먹고 오후 작업을 시작해야한다.");
        Console.WriteLine("하지만 점심 먹고 슬 잠이 쏟아진다.");
        Console.WriteLine();
        Console.WriteLine("1. 생활관에 숨어서 계속 잠을 잔다.");
        Console.WriteLine("2. 후임들을 통솔하고 작업하러 떠난다.");
        int input = CheckValidInput(1, 2);
        switch (input)
        {
            case 1:
                workCount += 3;
                Perfection += 3; //확률 넣어야함
                CementWorkLoop();
                break;
            case 2:
                workCount += 1;
                Perfection += 3;
                CementWorkLoop();
                break;
        }

    }
    static void CementWork4()
    {
        Console.Clear();
        Console.WriteLine($"완성도 : {Perfection} 남은 시간 : {10 - workCount}");
        Console.WriteLine("후임들이 곤란해 하는 것 같다.");
        Console.WriteLine("도와주면 쉽게 끝낼 수 있을 것 같다.");
        Console.WriteLine();
        Console.WriteLine("1. 계속 지켜본다.");
        Console.WriteLine("2. 후임들에게 시범을 보여준다.");
        int input = CheckValidInput(1, 2);
        switch (input)
        {
            case 1:
                workCount += 1;
                Perfection += 2;
                CementWorkLoop();
                break;
            case 2:
                workCount += 1;
                Perfection += 1; //확률로 마이너스
                CementWorkLoop();
                break;
        }

    }
    static void CementWork5()
    {
        Console.Clear();
        Console.WriteLine($"완성도 : {Perfection} 남은 시간 : {10 - workCount}");
        Console.WriteLine("아직 작업량이 많이 남은 것 같다.");
        Console.WriteLine("시간 내로 끝내려면 나도 거들어야 한다.");
        Console.WriteLine();
        Console.WriteLine("1. 계속 지켜본다");
        Console.WriteLine("2. 후임들을 도와 작업을 마무리한다");
        int input = CheckValidInput(1, 2);
        switch (input)
        {
            case 1:
                workCount += 1;
                Perfection += 3;
                CementWorkLoop();
                break;
            case 2:
                workCount += 1;
                Perfection += 1;
                CementWorkLoop();
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
        Console.Clear();
        Console.WriteLine($"완성도 : {Perfection} 남은 시간 : {10 - workCount}");
        Console.WriteLine("보급병이 창고 현황판을 뽑고 있다.");
        Console.WriteLine("그 동안 창고 열쇠를 챙기고 출발할 준비를 하자.");
        Console.WriteLine("중대장님에게 상단키를 받아야한다. 중대장님이랑 마주치기 껄끄러운데...");
        Console.WriteLine();
        Console.WriteLine($"1. 후임에게 시킨다.");
        Console.WriteLine($"2. 직접 상단키를 받아 창고 열쇠를 챙긴다.");
        Console.WriteLine();
        int input = CheckValidInput(1, 2);
        switch (input)
        {
            case 1:
                workCount += 1;
                Perfection += 1; //일정 확률로 진행도 workcount +1
                WarehouseWokr2();
                break;
            case 2:
                workCount += 1;
                Perfection += 1;
                WarehouseWokr2();
                break;
        }
    }

    static void WarehouseWokr2()
    {
        Console.Clear();
        Console.WriteLine($"완성도 : {Perfection} 남은 시간 : {10 - workCount}");
        //workcount가 2일때 후임이 안들고왔다고 알림
        Console.WriteLine("한겨울의 컨테이너 한기가 느껴진다.");
        Console.WriteLine("창고 문을 열자 먼지가 날리고 냄새가 난다.");
        Console.WriteLine("보급병이 창고 물건을 다 꺼내서 재고파악을 하려고 한다.");
        Console.WriteLine();
        Console.WriteLine($"1. 후임에게 시키고 간이 막사에서 한숨 잔다.");
        Console.WriteLine($"2. A급 장비가 있는지 궁금하다. 같이 작업을 시작한다.");
        Console.WriteLine();
        int input = CheckValidInput(1, 2);
        switch (input)
        {
            case 1:
                workCount += 2;
                Perfection += 1;
                WarehouseWokr3();
                break;
            case 2:
                workCount += 1;
                Perfection += 2; //확률로 A급 장비 얻을 수 있음
                WarehouseWokr3();
                break;
        }
    }

    static void WarehouseWokr3()
    {
        Console.Clear();
        Console.WriteLine($"완성도 : {Perfection} 남은 시간 : {10 - workCount}");
        Console.WriteLine("점심먹고 오후 작업을 시작해야한다.");
        Console.WriteLine("한기가 느껴졌던 컨테이너도 오후가 되니 열을 뿜고 있었고.");
        Console.WriteLine("날이 풀려 몸이 따뜻해지고 슬 잠이 쏟아지기 시작한다.");
        Console.WriteLine();
        Console.WriteLine("1. 오후에도 뭔 일이 있겠냐 간이 막사에서 한숨 잔다.");
        Console.WriteLine("2. 할 일이 많이 남아 보인다. 같이 작업을 시작한다.");
        Console.WriteLine();
        int input = CheckValidInput(1, 2);
        switch (input)
        {
            case 1:
                workCount += 2;
                Perfection += 1;
                WarehouseWokr4();
                break;
            case 2:
                workCount += 1;
                Perfection += 2; //확률로 A급 장비 얻을 수 있음
                WarehouseWokr4();
                break;
        }
    }

    static void WarehouseWokr4()
    {
        Console.Clear();
        Console.WriteLine($"완성도 : {Perfection} 남은 시간 : {10 - workCount}");
        Console.WriteLine("재고가 안맞는것 같다.");
        Console.WriteLine("보급병은 그걸 또 다시 세고 있다.");
        Console.WriteLine();
        Console.WriteLine("1. 계속 지켜본다.");
        Console.WriteLine("2. 보급병에게 가라의 정석을 알려준다.");
        int input = CheckValidInput(1, 2);
        switch (input)
        {
            case 1:
                workCount += 1;
                Perfection += 3; //확률로 낮아질 수 있음
                WarehouseWorkLoop();
                break;
            case 2:
                workCount += 1;
                Perfection += 1;
                WarehouseWorkLoop();
                break;
        }
    }
    static void WarehouseWokr5()
    {
        Console.Clear();
        Console.WriteLine($"완성도 : {Perfection} 남은 시간 : {10 - workCount}");
        Console.WriteLine("아직 작업량이 많이 남은 것 같다.");
        Console.WriteLine("시간 내로 끝내려면 나도 거들어야 한다.");
        Console.WriteLine();
        Console.WriteLine("1. 계속 지켜본다");
        Console.WriteLine("2. 후임들을 도와 작업을 마무리한다");
        int input = CheckValidInput(1, 2);
        switch (input)
        {
            case 1:
                workCount += 1;
                Perfection += 2; //확률로 낮아질 수 있음
                WarehouseWorkLoop();
                break;
            case 2:
                workCount += 1;
                Perfection += 1; //확률로 A급 장비 얻을 수 있음
                WarehouseWorkLoop();
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
        Console.Clear();
        Console.WriteLine("말년휴가를 나오게 되었다.");
        Console.WriteLine();
        Console.WriteLine($"1. 일단 집으로 간다.");
        Console.WriteLine($"2. 친구들과 연락을 한다.");
        Console.WriteLine();
        int input = CheckValidInput(1, 2);
        switch (input)
        {
            case 1:
                LastLeave2();
                break;
            case 2:
                LastLeave2();
                break;
        }
    }
    static void LastLeave2()
    {
        Console.Clear();
        //아무도 연락되지 않음 그래서 혼자서 번화가로 나옴
        Console.WriteLine("번화가로 나오게 되었다.");
        Console.WriteLine("내 앞으로 이상형의 여성분이 지나간다.");
        Console.WriteLine();
        Console.WriteLine($"1. 말을 건다.");
        Console.WriteLine($"2. 말을 걸지 않는다.");
        Console.WriteLine();
        int input = CheckValidInput(1, 2);
        switch (input)
        {
            case 1:
                //여성분이 불쾌하게 처다봄
                LastLeave3();
                break;
            case 2:
                LastLeave3();
                break;
        }
    }
    static void LastLeave3()
    {
        Console.Clear();
        //아무도 연락되지 않음 그래서 혼자서 번화가로 나옴
        Console.WriteLine("여성분의 남자친구와 눈이 마주쳤다.");
        Console.WriteLine("그리고 나를 밀쳐냈다.");
        Console.WriteLine("군바리가 누구한테 찝쩍대는거야!");
        Console.WriteLine();
        Console.WriteLine($"1. 억울하다 싸운다.");
        Console.WriteLine($"2. 사람 잘못봤습니다 죄송합니다.");
        Console.WriteLine();
        int input = CheckValidInput(1, 2);
        switch (input)
        {
            case 1:
                LastLeaveBattle1();
                break;
            case 2:
                LastLeave4();
                break;
        }
    }
    static void LastLeaveBattle1()
    {
        Console.Clear();
        //남자와 배틀
        //지면 경찰서에 끌려가서 행보관한테 복귀엔딩
        //이기면 집으로 돌가는 엔딩
        int input = CheckValidInput(1, 2);
        switch (input)
        {
            case 1:
                LastLeaveBattle2(); //이김
                break;
            case 2:
                LastLeave4(); //짐
                break;
        }
    }
    static void LastLeaveBattle2()
    {
        Console.Clear();
        //남자와 배틀
        //지면 경찰서에 끌려가서 행보관한테 복귀엔딩
        //이기면 집으로 돌가는 엔딩
        int input = CheckValidInput(1, 2);
        switch (input)
        {
            case 1:
                LastLeave3();
                break;
            case 2:
                LastLeave3();
                break;
        }
    }
    static void LastLeave4()
    {
        Console.Clear();
        //이 상황에 질려서 집에서 말년 보내는 엔딩
        Console.WriteLine("밖은 위험하다 그냥 집에서 빈둥거리며 보내야겠다.");
    }
#endregion
    #region 확률 구현
    static bool EventOccur(double probability)
    {
        Random random = new Random();
        return random.NextDouble() < probability;
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
        Console.WriteLine("   _____  __   __                        ");
        Console.WriteLine("  | ___ | | | / /                     ");
        Console.WriteLine("  | |_/ /  | V /                      ");
        Console.WriteLine("  |  __/   /   |                      ");
        Console.WriteLine("  | |     / /^| |                      ");
        Console.WriteLine("  |_|    /_/   |_|                    ");
        Console.WriteLine(" -------------------------------------------- ");
        Console.WriteLine();
        Console.WriteLine(" 이곳은 PX입니다.");
        Console.WriteLine(" 장비와 회복아이템을 살 수 있습니다.");
        Console.WriteLine();

        Console.WriteLine($" 현재 소지금: {player1._gold}G"); //소지금 표시
        Console.WriteLine("1. 장비코너");
        Console.WriteLine("2. 음식코너");
        Console.WriteLine("0. 막사");
        

        int input = CheckValidInput(0, 2);
        switch (input)
        {
            case 0:
                //막사로
                Home();
                break;
            case 1:
                // 상점에서 아이템을 구매하는 메서드 호출
                EquipPx();
                break;
                            
            case 2:
                // 상점에서 아이템을 구매하는 메서드 호출
                FoodPx();
                break;

        }
    }


    //상점(PX)
    static void EquipPx()
    {
        //군장점입니다.
List<Weapon> weapons = new List<Weapon>
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
        };                                                                                                           // 능력치들은 밸런스에맞게 조정해야됨
        List<Armor> armors = new List<Armor>
        {
            new Armor("생활복", 50, "평범한 옷", 5, 10),
            new Armor("로카티", 150, "강화된 방어복", 15, 20),
            new Armor("화생방 보호의", 200, "생화학적 위협으로부터 보호하는 의복", 20, 25),
            new Armor("깔깔이", 100, "특수 재료로 만든 방어복", 10, 15),
            new Armor("신형 전투복", 300, "최신형 전투용 갑옷", 25, 30),
            new Armor("개구리 전투복", 120, "개구리 가죽으로 만든 방어복", 12, 18),
            new Armor("특전사 이준호 전투복", 9999, "특전사 이준호님의 전투복", 999, 999)
        };
        Console.WriteLine("군장점 코너");
        Console.WriteLine("=====================================================================================");
        //반복문을 이용한 아이템 목록출력
        Console.WriteLine("무기 목록");
        for (int i = 0; i < weapons.Count; i++)
        {
            var weapon = weapons[i];
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine($" {i + 1}. {weapon.ItemName} \t| 가격: {weapon.ItemGold}G \t| 아이템 설명: {weapon.ItemDescription} ");
        }

        // 방어구 목록 출력
        Console.WriteLine("방어구 목록");
        for (int i = 0; i < armors.Count; i++)
        {
            var armor = armors[i];
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine($" {i + 1}. {armor.ItemName} \t| 가격: {armor.ItemGold}G \t| 아이템 설명: {armor.ItemDescription} ");
        }

        Console.WriteLine("------------------------------------------------------------------");
        Console.WriteLine("=====================================================================================");
        Console.WriteLine();
        Console.WriteLine(" 1. 아이템 구매하기");

        Console.WriteLine();
        Console.WriteLine(" 0. 뒤로가기");
        Console.Write(">>");

        int input = CheckValidInput(0, 2);
        switch (input)
        {
            case 0:
                //px입구로
                PX();
                break;
            case 1:
                // 상점에서 아이템을 구매하는 메서드 호출
                BuyItem(player1);
                break;

        }
    }


    //음식(PX)
    static void FoodPx()
    {
        //상점입니다.

List<Food> foods = new List<Food>
        {
             new Food("건빵", 5, 10, 20, "긴급 상황을 위한 비상식량"),
             new Food("전투식량", 10, 15, 30, "체력과 공격력을 강화하는 식사"),
             new Food("", 3, 5, 15, "체력 회복을 위한 탄수화물 보충"),
             new Food("단백질 바", 4, 7, 18, "체력 및 공격력 강화를 위한 단백질 섭취"),                                   // 능력치들은 밸런스에맞게 조정해야됨
             new Food("야간식량", 8, 12, 25, "야간에 먹는 음식, 체력 및 방어력 강화"),
             new Food("특급 식사", 15, 20, 50, "전투에 최적화된 특별한 식사"),
             new Food("야전식량", 12, 18, 40, "야외 전투에 적합한 식사")
        };
        Console.WriteLine();
        Console.WriteLine("식재료 코너");
        Console.WriteLine("=====================================================================================");
        //반복문을 이용한 아이템 목록출력
        for (int i = 0; i < foods.Count; i++)
        {
            var food = foods[i];
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine($" {i + 1}. {food.ItemName} \t| 가격: {food.ItemGold}G \t| 아이템 설명: {food.ItemDescription} ");
        }
        Console.WriteLine("------------------------------------------------------------------");
        Console.WriteLine("=====================================================================================");
        Console.WriteLine();
        Console.WriteLine(" 1. 아이템 구매하기");

        Console.WriteLine();
        Console.WriteLine(" 0. 뒤로가기");
        Console.Write(">>");

        int input = CheckValidInput(0, 2);
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
     //구매한 아이템 인벤토리로 옮기기
     static void BuyItem(Character player)
     {
         Console.Clear();
         Console.WriteLine(" 구매할 아이템을 선택하세요:");
         Console.WriteLine($" 현재 소지금: {player1._gold}");
         Console.WriteLine();
         Console.WriteLine("=====================================================================================");
         Console.WriteLine();
         for (int i = 0; i < items.Count; i++)
         {
             var item = items[i];
             Console.WriteLine($" {i + 1}. {item.ItemName} \t| 가격: {item.ItemGold}G");
             Console.WriteLine();
         }
         Console.WriteLine("=====================================================================================");

         Console.WriteLine();
 
         //1~7의 숫자를 입력하면 0~6번째의 아이템을 구매
         int itemIndex = CheckValidInput(1, 7) - 1;
 
         Item selectedItem = items[itemIndex]; // 선택한 아이템 가져오기
 
         // 플레이어의 골드가 아이템 가격보다 많은지 확인
         if (player.Gold >= selectedItem.ItemGold)
         {
             player.Gold -= selectedItem.ItemGold; // 골드 차감
             player.AddToInventory(selectedItem); // 인벤토리에 아이템 추가
             items.Remove(selectedItem);//선택한 아이템 제거
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
          //구매한 아이템 인벤토리로 옮기기
     static void BuyFood(Character player)
     {
         Console.Clear();
         Console.WriteLine(" 구매할 아이템을 선택하세요:");
         Console.WriteLine($" 현재 소지금: {player1._gold}");

         Console.WriteLine("=====================================================================================");
           Console.WriteLine();
         for (int i = 0; i < foods.Count; i++)
         {
             var food = items[i];
             Console.WriteLine($" {i + 1}. {food.ItemName} \t| 가격: {food.ItemGold}G");
             Console.WriteLine();
         }
          Console.WriteLine("=====================================================================================");
         Console.WriteLine();
 
         //1~7의 숫자를 입력하면 0~6번째의 아이템을 구매
         int itemIndex = CheckValidInput(1, 7) - 1;
 
         Item selectedItem = items[itemIndex]; // 선택한 아이템 가져오기
 
         // 플레이어의 골드가 아이템 가격보다 많은지 확인
         if (player.Gold >= selectedItem.ItemGold)
         {
             player.Gold -= selectedItem.ItemGold; // 골드 차감
             player.AddToInventory(selectedItem); // 인벤토리에 아이템 추가
             items.Remove(selectedItem);//선택한 아이템 제거
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


