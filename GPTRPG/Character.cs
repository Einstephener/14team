
//캐릭터 클래스
public class Character
{
    public string Name { get; set;} //캐릭터 이름
    public string Job { get; set;} //캐릭터 직업
    public int Str { get;  set; } //캐릭터 힘
    public int Dex { get;  set; } //캐릭터 민첩
    public int IQ { get;  set; } //캐릭터 지능
    public int Luk { get;  set; } //캐릭터 운
    public int Hp { get; set; } //캐릭터 체력
    public int Mind {get; set;} //캐릭터 정신력

    public int _gold; //소유 골드
    public List<Item> Inventory { get; } = new List<Item>(); // 인벤토리 리스트 추가

   
    public int Gold //골드 증감
    {
        get { return _gold; }
        set { _gold = value; }
    }


    public Character(string name, string job, int str, int dex, int iq, int luk, int hp, int gold, int mind)
    {
        Name = name;
        Job = job;
        Str = str;
        Dex = dex;
        IQ = iq;
        Luk = luk;
        Hp = hp;
         _gold = gold;
        Mind = mind;

    }

    //인벤토리에 아이템 추가
    public void AddToInventory(Item item)
    {
        //아이템이 처음 구매될때 isEquipped가 false로 고정
        item.IsEquipped = false;
        //인벤토리에 아이템 추가
        Inventory.Add(item);
    }

    /*
    //장비 장착
    public void EquipItem(int input)
    {
        //착용하려는 장비가 인벤토리 안에 있는 장비인가
        if (input > 0 && input <= Inventory.Count)
        {
            
            //배열은 0부터 시작하기 때문에 input-1
            var item = Inventory[input - 1];

            if (!item.IsEquipped) //착용여부가 false일때
            {
                
                item.IsEquipped = true;//착용여부를 true로 바꿈
                Console.WriteLine($" [{item.ItemName}]을(를) 장착했습니다.");
                //착용된 아이템의 스탯을 캐릭터 스탯에 적용
                
                Atk += item.ItemAtk;
                Def += item.ItemDef;
            }
            else
            {
                item.IsEquipped = false;//착용여부를 false로 바꿈
                Console.WriteLine($" [{item.ItemName}]을(를) 해제했습니다.");
                //착용 해제된 아이템의 스탯을 캐릭터 스탯에 적용
                Atk -= item.ItemAtk;
                Def -= item.ItemDef;
            }
        }
        else
        {
            Console.WriteLine(" 잘못된 인덱스입니다.");
        }
        
        
    }
    */

}
public class Infantry : Character
{
    public Infantry(string name, string job, int str, int dex, int iq, int luk, int hp, int gold, int mind)
        :base(name, "보병", str, dex, iq, luk, hp, gold, mind)
        {
        }
}

public class Artillery : Character
{
    public Artillery(string name, string job, int str, int dex, int iq, int luk, int hp, int gold, int mind)
            :base(name, "포병", str, dex, iq, luk, hp, gold, mind)
            {
            }
}

public class Transportation : Character
{
    public Transportation(string name, string job, int str, int dex, int iq, int luk, int hp, int gold, int mind)
        :base(name, "운전병", str, dex, iq, luk, hp, gold, mind)
        {
        }
}

public class Maintenence : Character
{
    public Maintenence(string name, string job, int str, int dex, int iq, int luk, int hp, int gold, int mind)
        :base(name, "정비병", str, dex, iq, luk, hp, gold, mind)
        {
        }
}

