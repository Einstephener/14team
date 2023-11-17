
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

        //인벤토리에 아이템 추가
        Inventory.Add(item);
    }

    public double CalculateProbability(int Value)
    {
        return Math.Clamp(Math.Log(Value, 1.8) / 10, 0, 1);
    }

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


