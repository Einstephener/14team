
//캐릭터 클래스
public class Character
{
    public string Name { get; set; } //캐릭터 이름
    public string Job { get; set; } //캐릭터 직업
    public int Str { get; set; } //캐릭터 힘
    public int Dex { get; set; } //캐릭터 민첩
    public int IQ { get; set; } //캐릭터 지능
    public int Luk { get; set; } //캐릭터 운
    public int Hp { get; set; } //캐릭터 체력
    public int Mind { get; set; } //캐릭터 정신력
    public int _gold; //소유 골드

    public int Gold //골드 증감
    {
        get { return _gold; }
        set { _gold = value; }
    }


    public List<Food> InventoryFood { get; } = new List<Food>(); // 인벤토리 리스트 추가
    public List<Armor> InventoryArmor { get; } = new List<Armor>(); // 인벤토리 리스트 추가
    public List<Weapon> InventoryWeapon { get; } = new List<Weapon>(); // 인벤토리 리스트 추가

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
    


    public void AddToInventoryFood(Food food)
    {
        //인벤토리에 아이템 추가
        InventoryFood.Add(food);
    }
    public void AddToInventoryArmor(Armor armor)
    {
        //인벤토리에 아이템 추가
        InventoryArmor.Add(armor);
    }
    public void AddToInventoryWeapon(Weapon weapon)
    {
        //인벤토리에 아이템 추가
        InventoryWeapon.Add(weapon);
    }

    public double CalculateProbability(int Value)
    {
        return Math.Clamp(Math.Log(Value, 1.06) / 100, 0, 1);
        //로그 함수 넣은건 다른 게임들도 그렇게 되어있기도 하고 확률이 100%에 육박하면 노잼이라서 100찍으면 79% 확률
    }

    public int Attack()
    {
        Random rand = new Random();
        int critRate = rand.Next(1, 101);

        //럭을 사용해서 치명타 확률 결정
        int critical = Luk;

        if (critRate <= critical)
        {
            Console.WriteLine("크리티컬!");
            return CalculateDamage(true);
        }
        else
        {
            return CalculateDamage(false);
        }
    }

    private int CalculateDamage(bool isCritical)
    {
        int baseDamage = Str;

        if (isCritical)
        {
            return baseDamage * 2;
        }
        else
        {
            return baseDamage;
        }
    }

}
public class Infantry : Character
{
    public Infantry(string name, string job, int str, int dex, int iq, int luk, int hp, int gold, int mind)
        : base(name, "보병", str, dex, iq, luk, hp, gold, mind)
    {
    }
}

public class Artillery : Character
{
    public Artillery(string name, string job, int str, int dex, int iq, int luk, int hp, int gold, int mind)
            : base(name, "포병", str, dex, iq, luk, hp, gold, mind)
    {
    }
}

public class Transportation : Character
{
    public Transportation(string name, string job, int str, int dex, int iq, int luk, int hp, int gold, int mind)
        : base(name, "운전병", str, dex, iq, luk, hp, gold, mind)
    {
    }
}

public class Maintenence : Character
{
    public Maintenence(string name, string job, int str, int dex, int iq, int luk, int hp, int gold, int mind)
        : base(name, "정비병", str, dex, iq, luk, hp, gold, mind)
    {
    }
}


