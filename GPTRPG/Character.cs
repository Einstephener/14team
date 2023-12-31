﻿
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
    public int MaxHp { get; set; } //캐릭터 최대체력
    public int MaxMind { get; set; } //캐릭터 최대정신력
    public int Mind { get; set; } //캐릭터 정신력
    public int _gold; //소유 골드
    public List<Skill> Skills { get; set; }

    public int Gold //골드 증감
    {
        get { return _gold; }
        set { _gold = value; }
    }


    public List<Food> InventoryFood { get; } = new List<Food>(); // 인벤토리 리스트 추가
    public List<Armor> InventoryArmor { get; } = new List<Armor>(); // 인벤토리 리스트 추가
    public List<Weapon> InventoryWeapon { get; } = new List<Weapon>(); // 인벤토리 리스트 추가

    public Character(string name, string job, int str, int dex, int iq, int luk, int hp, int maxHp, int maxMind, int gold, int mind)
    {
        Name = name;
        Job = job;
        Str = str;
        Dex = dex;
        IQ = iq;
        Luk = luk;
        Hp = hp;
        MaxHp = maxHp;
        MaxMind = maxMind;
        _gold = gold;
        Mind = mind;
        Skills = new List<Skill>();
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

    //장비 장착
    public void EquipWeapon(int input)
    {
        //착용하려는 장비가 인벤토리 안에 있는 장비인가
        if (input > 0 && input <= InventoryWeapon.Count)
        {
            
            //배열은 0부터 시작하기 때문에 input-1
            var item = InventoryWeapon[input - 1];

            if (!item.isEquipped) //착용여부가 false일때
            {
                
                item.isEquipped = true; //착용여부를 true로 바꿈
                Console.WriteLine($" [{item.ItemName}]을(를) 장착했습니다.");
                //착용된 아이템의 스탯을 캐릭터 스탯에 적용
                
                Str += item.ItemStr;
                Dex += item.ItemDex;
                IQ += item.ItemIq;
                Luk += item.ItemLuk;
            }
            else
            {
                item.isEquipped = false; //착용여부를 false로 바꿈
                Console.WriteLine($" [{item.ItemName}]을(를) 해제했습니다.");
                //착용 해제된 아이템의 스탯을 캐릭터 스탯에 적용
                Str -= item.ItemStr;
                Dex -= item.ItemDex;
                IQ -= item.ItemIq;
                Luk -= item.ItemLuk;
            }
        }
        else
        {
            Console.WriteLine(" 잘못된 입력입니다.");
        }
        
        
    }
    public void EquipArmor(int input)
    {
        //착용하려는 장비가 인벤토리 안에 있는 장비인가
        if (input > 0 && input <= InventoryArmor.Count)
        {
            
            //배열은 0부터 시작하기 때문에 input-1
            var item = InventoryArmor[input - 1];

            if (!item.isEquipped) //착용여부가 false일때
            {
                
                item.isEquipped = true; //착용여부를 true로 바꿈
                Console.WriteLine($" [{item.ItemName}]을(를) 장착했습니다.");
                
                //착용된 아이템의 스탯을 캐릭터 스탯에 적용                
                MaxMind += item.ItemMind;
                MaxHp += item.ItemHp;
            }
            else
            {
                item.isEquipped = false; //착용여부를 false로 바꿈
                Console.WriteLine($" [{item.ItemName}]을(를) 해제했습니다.");
                
                //착용 해제된 아이템의 스탯을 캐릭터 스탯에 적용                
                MaxMind -= item.ItemMind;
                MaxHp -= item.ItemHp;
            }
        }
        else
        {
            Console.WriteLine(" 잘못된 입력입니다.");
        }
        
        
    }



    public double CalculateProbability(int Value)
    {
        return Math.Clamp(Math.Log(Value, 1.06) / 100, 0, 1);
        //로그 함수 넣은건 다른 게임들도 그렇게 되어있기도 하고 확률이 100%에 육박하면 노잼이라서 100찍으면 79% 확률
    }

    //공격력 메서드
    public int Attack()
    {
        Random rand = new Random();
        int critRate = rand.Next(1, 101);

        //럭을 사용해서 치명타 확률 결정
        int critical = Luk;

        if (critRate <= critical)
        {
            Console.WriteLine("");
            Console.WriteLine(" 크리티컬!");
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

        double damageWithVariation = Math.Round(baseDamage * RandomRange(0.9, 1.1));

        if (isCritical)
        {
            return (int)(damageWithVariation * 2);
        }
        else
        {
            return (int)damageWithVariation;
        }
    }
    private double RandomRange(double min, double max)
    {
        Random rand = new Random();
        return min + rand.NextDouble() * (max - min);
    }

    //회피여부 메서드
    public bool CheckEvade()
    {
        Random random = new Random();
        int evadeRate = random.Next(1, 101);

        return evadeRate <= Dex;
    }

    

}
public class Infantry : Character
{
    public Infantry(string name, string job, int str, int dex, int iq, int luk, int hp, int maxHp, int maxMind, int gold, int mind)
        : base(name, "보병", str, dex, iq, luk, hp, maxHp, maxMind, gold, mind)
    {
        Skills.Add(new CoordinationBarrageSkill());
    }
}

public class Artillery : Character
{
    public Artillery(string name, string job, int str, int dex, int iq, int luk, int hp, int maxHp, int maxMind, int gold, int mind)
            : base(name, "포병", str, dex, iq, luk, hp, maxHp, maxMind, gold, mind)
    {
        Skills.Add(new ArmorPiercerSkill());
    }
}

public class Transportation : Character
{
    public Transportation(string name, string job, int str, int dex, int iq, int luk, int hp, int maxHp, int maxMind, int gold, int mind)
        : base(name, "운전병", str, dex, iq, luk, hp, maxHp, maxMind, gold, mind)
    {
        Skills.Add(new K_511AttackSkill());
    }
}

public class Maintenence : Character
{
    public Maintenence(string name, string job, int str, int dex, int iq, int luk, int hp, int maxHp,int maxMind, int gold, int mind)
        : base(name, "정비병", str, dex, iq, luk, hp, maxHp, maxMind, gold, mind)
    {
        Skills.Add(new fuckSkill());
    }
}



