//소비형 아이템
public class Food
{
    public string ItemName { get; } //아이템 이름
    public int ItemStr { get; } //아이템 공격력
       
    public int ItemHp { get; } //아이템 체력
    public int ItemGold { get; } //아이템 가격
    public string ItemDescription { get; } //아이템 상세설명

    public Food(string itemName, int itemStr, int itemHp, int itemGold, string itemDescription)
    {
        ItemName = itemName;
        ItemStr = itemStr;
        ItemHp = itemHp;
        ItemGold = itemGold;
        ItemDescription = itemDescription;
    }
}


//장비형 아이템
public class Item //부모 클래스
{
    public string ItemName { get; } // 아이템 이름
    public int ItemGold { get; } // 아이템 가격
    public string ItemDescription { get; } // 아이템 상세 설명

    public Item(string itemName, int itemGold, string itemDescription)
    {
        ItemName = itemName;
        ItemGold = itemGold;
        ItemDescription = itemDescription;
    }
}


public class Weapon : Item    // 무기 클래스 (아이템을 상속받음)
{
    public int ItemStr { get; } // 공격력
    public int ItemDex { get; } // 민첩성
    public int ItemIq { get; } // 지능
    public int ItemLuk { get; } // 행운
    public bool isEquipped{get; set;} //장착여부

    public Weapon(string itemName, int itemGold, string itemDescription, int itemStr, int itemDex, int itemIq, int itemLuk, bool isEquipped = false)
        : base(itemName, itemGold, itemDescription)
    {
        ItemStr = itemStr;
        ItemDex = itemDex;
        ItemIq = itemIq;
        ItemLuk = itemLuk;
    }
}


public class Armor : Item         // 방어구 클래스 (아이템을 상속받음)
{
    public int ItemMind { get; } // 정신력
    public int ItemHp { get; } // 체력
    public bool isEquipped{get; set;} //장착여부
    public Armor(string itemName, int itemGold, string itemDescription, int itemMind, int itemHp, bool isEquipped = false)
        : base(itemName, itemGold, itemDescription)
    {
        ItemMind = itemMind;
        ItemHp = itemHp;
    }
}