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
    private static Enemy demon;
    private static Enemy Orc;

    //캐릭터 선언
    private static Character player1;

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
        player1 = new Character("", "용사", 5, 5, 5, 5, 100, 0, 5);

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


        //몬스터들 정보 세팅


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
 
         Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@,@@--@.~@@~=~@@@~@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@ @:~@@*;@~*@@=@$@@~=@@@@@@@@@@,@@@@@@@@@@@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@~:@*# -@#@$@:@*@#!:#@-@! @@:;@,$,@:@@@@@@@@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@~@ #=@;@-!@#:$@#-@@@=!@@$;$:@~@;@:#*@!~@@@@@@@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@@@@@@@@@@@@@@-$-!#@-@@;=@$;@@@$@@@@*@@*;@#,@@@-$@:@## ::@@@@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@@@@@@@@@@@~~@*@*:@@!@@@=@@*@@@@@@@@@@@@@@@*@@@*@@@-@#;!#@@@@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@@@@@@@@@@@!#-#@@$@@@@@@@@@@@@@@@@@@@@@@@@@@@@@$@@@#@@#;=@@@@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@@@@@@@@@@@#@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@#@@=#$@@@@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@@@@@@@@@@@*@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@~@@@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@@@@@@@@@@@#@@@@@@@@@@@@@@@@$*;:~----~;!=#@@@@@@@@@@@@@@@!@@@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@@@@@@@@@@~@@@@@@@@@@#*;~,                 .~;!$@@@@@@@@@!@@@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@@@@@@@@@@~@@@@@@@@=,                          ..!@@@@@@@@@@@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@@@@@@@@@@!@@@@@#!,                               .;#@@@@@@@@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@$.                                   .=@@@@:@@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@@@@@@@@@-@@@@!                                      .*@@@:@@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@@@@@@@@@~@@@=                                        .@@@*@@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@@@@@@@@@=@@#                                          ~@@#@@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@@@@@@@@@@@@:             .:-           .              ,#@@@@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@@@@@@@@@$@@,            -#*=*       .*$$*             .$@$@@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@@@@@@@@@=@=.            ,-  =,      ;*..:              *@=@@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@@@@@@@@@@@*                 .       ~.                 ;@#@@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@@@@@@@@-@@,               ..                           .@@@@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@@@@@@@@:@#               :=*:        -==~               #@~@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@@@@@@@@$@:               * .$-      .* .*               !@~@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@@@@@@@@@@,               - -**      ~*! -               -@*@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@@@@@@@;@=.                 $#*      ;=@                 ,@@@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@@@@@@@!@!                  @@*      ;@@.                .=@ @@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@@@@@@@#@,                  *@-      .@@                  *@-@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@@@@@@@@@                 ~*,        .:*=:                :@:@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@@@@@@@$:                                 .               .@=@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@@@@@@@$.                       .....                      #@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@@@@@@@=                     ~~-~~-.                       !#@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@~*$*~:!                     ...~!==*;:,                   ,=~:*$$;!@@@@@@");
         Console.WriteLine("@@@@@@:$*!*=!:                  -;~~:~,                          .==;~--:$# @@@@");
         Console.WriteLine("@@@@@!#-   ~#.        .=                               -:        .=,     .##@@@@");
         Console.WriteLine("@@@@*$,     $         ~$                               ;,         ;       ;=@@@@");
         Console.WriteLine("@@@@!:     .:         =#,                              ;,         :.      -#@@@@");
         Console.WriteLine("@@@@#~.-.  ,.       -;$~*:~:-                      ,~;!==-        ..  :!~.,#@@@@");
         Console.WriteLine("@@@@@-;!@~ .          :*!   ,*$*~.             ,:=#$;:,!-!,          -*.  .#@@@@");
         Console.WriteLine("@@@@@- ,!$            .$!,      -:;;;::;;;;;!!!;:,   ~,*.            #=~  -=@@@@");
         Console.WriteLine("@@@@#~ ,.=             $-!;                         -=-!             @-.  :;@@@@");
         Console.WriteLine("@@@@*;   * .           ::.-!:~~.                 ,~!;.*~             ::   !$@@@@");
         Console.WriteLine("@@@@$$   . :           ,!   .,-;=$=;~..     .,;=$;,...#               .  ~$;@@@@");
         Console.WriteLine("@@@@:$;    !           .=.      ...-~:;!!!!!;:-..    -=            ,.   -=-@@@@@");
         Console.WriteLine("@@@@@=#:   ;.           *,              . ..         ;-            *.  ~=:@@@@@@");
         Console.WriteLine("@@@@@@=$!- ~!           :!             .      .      *.           .#::!$-@@@@@@@");
         Console.WriteLine("@@@@@@@!#!$$@.          .$..                        .*            :#!$* @@@@@@@@");
         Console.WriteLine("@@@@@@@@@@@-*;           =-                         !:           .*!@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@@@@,!=$:          -;  .                    ..#.           ,#:@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@@;*##$$#$~        .=. .                    .:;            *=@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@;=#;-,,-*#:        ;! .                     *.           ~@!@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@;=$~     .*=.        #-       ..,,,,...     ~;           .=# @@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@;$,       .$:        -*.   .-!*!!;;;!*!-.. ,#.           !#-@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@=!         :$         ;:..~=!-       .-*=- ;-           :@#@@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@!=         -$         .$;*:.          ..-!*!           -#*-@@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@!=*        ,$;          $-             ..~$.          ,@=@@@@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@!#~       ,$!*.        ,=:.          ..~*.          ~#=!@@@@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@@!$.      -$@;#-         !=,        .,;=.         .;@=@@@@@@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@@;=!      -;*==$*!,       ~!!:,....-;*:          ,=$$@@@@@@@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@@ $#,     ;=-   .*$~        .!$#$$#$:          .:@$@@@@@@@@@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@@@;#:    --       :=;.         ....          .;=$*@@@@@@@@@@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@@@,#:   ,          *#@!.                   .;#!,@@@@@@@@@@@@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@@@,#:              !$*$$*;-             -;;====*,@@@@@@@@@@@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@@@*#,             ~#$***!!$@$;,     ,:=*!;;;;;!#=~ @@@@@@@@@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@@~=$          .~!;;:;=*;!=!;!**==*!!!!$=;:;;;;;!=#!;@@@@@@@@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@@*$-       .~-*,     .#*#!!;;;;;;;;;!;;$*::::::;!*#!@@@@@@@@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@@!=         -~        :#;:;;;;;;;;;;:::;=!:::::;;!*#*@@@@@@@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@,$:                    #=;::::::::::::::;*#=:::;;!*$$-@@@@@@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@;$~                    *$;:::::::::::::~:~*=*::;;!*=$!@@@@@@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@*$,                    *$;::-,:::::::::,.-~#*::;;!**$*@@@@@@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@$$.                    $$!:~-.::::::~~~..--;*::::!*$$:@@@@@@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@=$.                ,!==#$!:,...-:::~......-~=:::;!*$$-@@@@@@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@!#,              ,:!:,.:==:::~-~:::~......-~*=!;!!=#!@@@@@@@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@:#~            .=~.     .$;::;::::::~:~...-~~*$*=$#; @@@@@@@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@@$;             ,,       ;$!!*!!!!*!:::...----!  !=~@@@@@@@@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@@;$                      :#$$*,,,,-#==$=*!:-~-!. .=;@@@@@@@@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@@=$:                     !#=$!,,,,,#======$$$*=!  ~$-@@@@@@@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@@;$#,                   .$$=$$!;;!*#*==$$$=*==$#.-.=*@@@@@@@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@@@-$!                  -$=!**=$$$$$*;;!!!*=$$===: :*$@@@@@@@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@@@@!#!               ~*=!;;!!!!!!;;;;;;!!*!;!*=#* ~!#@@@@@@@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@@@@@!#!.           .;#=:::;;;;;;::::::;=**!!!;;!#;*!#@@@@@@@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@@@@@~!#*-        ,!$$;::::;;;;;;::::::;=!***;;;!=;~$:@@@@@@@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@@@@@@@=$$=-,.,,~$$$!::::::;;;;;;;;::::;=!*$!;;;!=#$~@@@@@@@@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@@@@@@@@~$*=$$#$#;;::::::::;;;;;;;;;;;:;==;;;;;;;*#,@@@@@@@@@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@$#,~::::!;;;;;;;;;;;;;;!;;;::;;;;;!$;@@@@@@@@@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@;#::::::!!;********=!;;;;:::::;;--~$$@@@@@@@@@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@=$:::;!!;!;=========*;;;;::::::;---=$~@@@@@@@@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@#=:::;;;;**!====!*==*;;;;:*!:~.--~;$=;@@@@@@@@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@@@@@@@@@@@@@@-#*:;!;;;;;*$****;*==*;;:!$;:::~~~:*$$=@@@@@@@@@@@@@@@@@@@@@");
         Console.WriteLine("@@@@@@@@@@@@@@@@@@@@~#*:;;;;;;;;!====*=======!:::::::-~!=#$@@@@@@@@@@@@@@@@@@@@@");
 
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
                player.Job = "포병";
                Console.WriteLine("포병이다.");
                break;
            case 2:
                //보병 전직
                player.Job = "보병";
                Console.WriteLine("보병이다.");
                break;
            case 3:
                //운전병 전직
                player.Job = "운전병";
                Console.WriteLine("운전병이다.");
                break;
            case 4:
                //정비병 전직
                player.Job = "정비병";
                Console.WriteLine("정비병이다.");
                break;
        }
        Console.WriteLine("자대로 가서도 꼭 연락해!");

        Console.WriteLine("press any key to continue");
        Console.ReadKey();
        Basic(player1);

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

    #region 상태창
        //상태확인
        static void DisplayMyInfo()
        {
            Console.Clear();
            Rank myRank = new Rank(1);
            myRank.SetRank();
            string currentRank = myRank.rank; //현재 계급 가져오기
    
            Console.WriteLine("상태확인");
            Console.WriteLine("당신의 정보를 표시합니다.");
            Console.WriteLine();
            Console.WriteLine("====================================");
            Console.WriteLine($" {currentRank} | {player1.Name} ");
            Console.WriteLine();
            Console.WriteLine($" 힘 \t: {player1.Str}");
            Console.WriteLine($" 민첩 \t: {player1.Dex}");
            Console.WriteLine($" 지능 \t: {player1.IQ}");
            Console.WriteLine($" 운 \t: {player1.Luk}");
            Console.WriteLine($" 체력 \t\t: {player1.Hp}");
            Console.WriteLine($" 정신력 \t: {player1.Mind}");
            Console.WriteLine($" Gold \t\t: {player1.Gold} G");
            Console.WriteLine("====================================");
            Console.WriteLine();
            Console.WriteLine(" 0. 나가기");
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
                string equippedStatus = item.IsEquipped ? "[E]" : ""; // 아이템이 장착되었는지 여부에 따라 [E] 표시 추가 없으면 공백
                Console.Write($"{i + 1}. ");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write($"{equippedStatus}");
                Console.ResetColor();
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
            Console.WriteLine("아 미쳐 닦지 못했습니다..");
            Console.WriteLine("하.. 아침부터 큰일이네;; ");
        }
        // 만약 굳건이가 군화를 닦았다면 아무일도 일어나지 않는다
        // 굳건이가 군화를 안닦았다면 -hp  확률 50%

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
        Home();
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
        Console.WriteLine("온몸에 힘이 없다... 막사로 돌아가자.");
        Console.ReadKey();
        Home();
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

    }

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
        Home();

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
    //상병 스토리 - 체력검정
    static void CSTest()
    {
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine("오늘은 체력측정이 있는 날이다.");
        Console.WriteLine("시험관으로 저번달에 전입온 소위가 걸렸다.");
        Console.WriteLine("아 저 소위 FM인데...");
        Console.WriteLine("\"오늘은 체력 측정엔 가라는 없다! 알겠나!\"");
        Console.WriteLine("특급전사를 따야 휴가를 받는데.. 어떻게 가라를 쳐야 하나..");
        Console.WriteLine("");
    }




    //병장



    //    일병
    //    - 100일 휴가
    //    여자친구가 전화를 받지 않는다.(여자 친구 집 앞 찾아가기 50%확률로 바람,
    //    -사격
    //    (확률) 명중률 보상 20발 중 명중, 빗나감 개수에 따른 차등보상

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

            Console.WriteLine("드디어 100일 휴가를 나왔다!");
            Console.WriteLine("어떤 일을 먼저 해볼까?");

            for (int i = 0; i < text.Length; i++)
            {
                if (cursor == i) Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(text[i]);
                Console.ResetColor();
            }

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
                break;
            case 1:
                // 친구들 만나러
                OneHundredDaysEvent(randomNum, "오랜만에 친구들과 술 한잔하며 이야기했다.",
                "친구들과 Pc방에 가서 시간 가는 줄 모르고 놀았다.",
                "나는 친구가 없다...");
                break;
            case 2:
                // 본가로 간다
                OneHundredDaysEvent(randomNum, "오랜만에 집에 왔건만 군대에서 뭐했냐며 잔소리만 들었다...",
                "가족들과 오랜만에 식사하며 좋은 시간을 보냈다.",
                "내가 오는 줄 몰랐나..? 아무도 없다...");
                break;
            case 3:
                // 혼자 논다
                OneHundredDaysEvent(randomNum, "혼자 즐겁게 놀았다. 진짜 즐거운 거 맞다, 아마도..",
                "여기저기 구경 다니며 신나게 놀았다.",
                "생활관에 있을 때가 더 나은 거 같다 너무 외롭다..");
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

                for (int i = 0; i < text.Length; i++)
                {
                    if (cursor == i) Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("text[i]");
                    Console.ResetColor();
                }

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
    // 일병 스토리 - 대민지원
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
                Console.WriteLine("대민지원을 완료했습니다.");
                // Input 이전화면으로가서 스토리진행
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

    // 외박(선택지) : 1.여자친구 2. 가족 3. 친구 셋중 플레이어가 고르도록 유도하고 보너스 능력치를 주는 스토리 능력치는 세가지 전부 다르게(가족>친구) 여친은 꽝 -능력치(여친 도망간스토리)
    static void overnight()
    {
        Console.WriteLine("첫 외박날짜가 정해졌습니다. 기대와 설렘이 가득찬 그의 마음속에는");
        Console.WriteLine("어디를 가야할지, 누구를 만나야 할지에 대한 고민으로 가득차있습니다.");

    }






    static int workCount = 0;
    static int Perfection = 0;
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
                workCount += 1;
                Perfection = 0; //확률 넣어야함
                CementWork3();
                break;
            case 2:
                workCount += 2;
                Perfection += 3;
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
                foodPx();
                break;

        }
    }


    //상점(PX)
    static void EquipPx()
    {
        //군장점입니다.

        Console.WriteLine();
        Console.WriteLine("=====================================================================================");
        //반복문을 이용한 아이템 목록출력
        for (int i = 0; i < items.Count; i++)
        {
            var item = items[i];
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine($" {i + 1}. {item.ItemName} \t| 가격: {item.ItemGold}G \t| 아이템 설명: {item.ItemDescription} ");
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
                //막사로
                Home();
                break;
            case 1:
                // 상점에서 아이템을 구매하는 메서드 호출
                BuyItem(player1);
                break;

        }
    }


    //음식(PX)
    static void foodPx()
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
                //막사로
                Home();
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

    /*
    //상태창
    static void DisplayMyInfo()
    {
        Console.Clear();

        Console.WriteLine("상태창");
        Console.WriteLine("캐릭터의 정보를 표시합니다.");
        Console.WriteLine();
        Console.WriteLine("====================================");
        Console.WriteLine($" Lv. {player1.Level.ToString("000")} ");
        Console.WriteLine($" {player1.Name} | {player1.Job} |");
        Console.WriteLine();
        Console.WriteLine($" 공격력 \t: {player1.Atk}");
        Console.WriteLine($" 방어력 \t: {player1.Def}");
        Console.WriteLine($" 체력 \t\t: {player1.Hp}");
        Console.WriteLine($" Gold \t\t: {player1.Gold} G");
        Console.WriteLine("====================================");
        Console.WriteLine();
        Console.WriteLine(" 0. 나가기");
        Console.Write(">>");

        int input = CheckValidInput(0, 0);
        switch (input)
        {
            case 0:
                //마을로 돌아가기
                ShowVillageFirst();
                break;
        }
    }

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
            string equippedStatus = item.IsEquipped ? "[E]" : ""; // 아이템이 장착되었는지 여부에 따라 [E] 표시 추가 없으면 공백
            Console.Write($"{i + 1}. ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write($"{equippedStatus}");
            Console.ResetColor();
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
            player.EquipItem(input);
            Console.WriteLine();
            Console.WriteLine("Press AnyKey");
            Console.ReadKey();
            //인벤토리창 새로고침
            DisplayInventory(player);
        }
        else
        {
            //마을로 돌아가기
            ShowVillageFirst();
        }
    }
    */
    //입력 키 확인 메서드


    /*
    //몬스터 전투 메서드
    public static void DungeonField(Character player1, Monster monster)
    {
        Console.Clear();
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($" {monster.MonsterName} 등장!");
        Console.WriteLine();
        Console.ResetColor();
        Console.WriteLine(" 진행하려면 enter");
        Console.WriteLine();
        Console.ReadKey();
        Console.WriteLine(" 전투 시작");
        int turnCount = 0;
        int saveHp = monster.MonsterHp;
        while (player1.Hp > 0 && monster.MonsterHp > 0) // 플레이어나 몬스터가 죽을 때까지 반복
        {

            Console.WriteLine(" 당신의 공격!");
            Console.WriteLine();
            monster.MonsterHp -= player1.Atk;
            Console.WriteLine($" 당신은 {monster.MonsterName}에게 {player1.Atk}의 데미지를 주었다.");
            Console.WriteLine();
            Console.WriteLine($" {monster.MonsterName}의 남은 체력: {monster.MonsterHp}");
            Thread.Sleep(1000);  // 턴 사이에 1초 대기

            if (monster.MonsterHp <= 0) break;  // 몬스터가 죽었다면 턴 종료

            Console.WriteLine();
            Console.WriteLine($" {monster.MonsterName}의 공격!");
            int GetDamage = (monster.MonsterAtk) - player1.Def; //몬스터의 공격 - 플레이어의 방어력 = 깎이는 체력
            if (GetDamage <= 0)
            {
                GetDamage = 0;
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(" 방어성공!");
                Console.ResetColor();
            }
            player1.Hp = player1.Hp - GetDamage;
            Console.WriteLine($" 체력이 {GetDamage} 감소했다.");
            Console.WriteLine();
            Console.WriteLine($" 당신의 남은 체력 : {player1.Hp}");
            Console.WriteLine();

            Thread.Sleep(1000);  // 턴 사이에 1초 대기

            turnCount++;//진행된 턴수
        }

        if (player1.Hp <= 0)
        {
            Console.WriteLine(" 사망하셨습니다.");
            MedicalCost();
        }
        else if (monster.MonsterHp <= 0)
        {
            Console.WriteLine();
            Console.WriteLine($" {monster.MonsterName}을 처치했습니다.");
            Console.ForegroundColor = ConsoleColor.Blue;
            switch (turnCount) //턴에 따른 추가 보상
            {
                case 1:

                    player1.Gold = player1.Gold + 3 * monster.MonsterGold;
                    Console.WriteLine(" {0}G 획득", 3 * (monster.MonsterGold));

                    IncreaseExperience(player1, monster);
                    Console.WriteLine(" {0}EXP 획득", 3 * (monster.MonsterExperience));
                    break;
                case 2:
                    player1.Gold = player1.Gold + 2 * monster.MonsterGold;
                    Console.WriteLine(" {0}G 획득", 2 * (monster.MonsterGold));

                    IncreaseExperience(player1, monster);
                    Console.WriteLine(" {0}EXP 획득", 2 * (monster.MonsterExperience));
                    break;
                default:
                    player1.Gold = player1.Gold + monster.MonsterGold;
                    Console.WriteLine(" {0}G 획득", (monster.MonsterGold));

                    IncreaseExperience(player1, monster);
                    Console.WriteLine(" {0}EXP 획득", (monster.MonsterExperience));
                    break;

            }
            Console.ResetColor();
        }
        monster.MonsterHp = saveHp;
        Console.WriteLine();
        Console.WriteLine(" 진행하려면 enter");
        Console.WriteLine();
    }*/
}


