//소비형 아이템
public class Food
{
    public string ItemName { get; } //아이템 이름

    public int ItemHp { get; } //아이템 체력

    public int ItemMind { get; } //아이템 체력

    public int ItemGold { get; } //아이템 가격

    public string ItemDescription { get; } //아이템 상세설명

    public string ItemEffect { get; } //아이템 효과

    public Food(string itemName, int itemHp, int itemMind, int itemGold, string itemDescription, string itemEffect)
    {
        ItemName = itemName;
        ItemHp = itemHp;
        ItemMind = itemMind;
        ItemGold = itemGold;
        ItemDescription = itemDescription;
        ItemEffect = itemEffect;
    }
}


//장비형 아이템
public class Item //부모 클래스
{
    public string ItemName { get; } // 아이템 이름
    public int ItemGold { get; } // 아이템 가격
    public string ItemDescription { get; } // 아이템 상세 설명

    public string ItemEffect { get; }

    public Item(string itemName, int itemGold, string itemDescription, string itemEffect)
    {
        ItemName = itemName;
        ItemGold = itemGold;
        ItemDescription = itemDescription;
        ItemEffect = itemEffect;
    }
}


public class Weapon : Item    // 무기 클래스 (아이템을 상속받음)
{
    public int ItemStr { get; } // 공격력
    public int ItemDex { get; } // 민첩성
    public int ItemIq { get; } // 지능
    public int ItemLuk { get; } // 행운
    public bool isEquipped { get; set; } //장착여부

    public Weapon(string itemName, int itemGold, string itemDescription, int itemStr, int itemDex, int itemIq, int itemLuk, string itemEffect, bool isEquipped = false)
        : base(itemName, itemGold, itemDescription, itemEffect)
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
    public bool isEquipped { get; set; } //장착여부
    public Armor(string itemName, int itemGold, string itemDescription, int itemMind, int itemHp, string itemEffect, bool isEquipped = false)
        : base(itemName, itemGold, itemDescription, itemEffect)
    {
        ItemMind = itemMind;
        ItemHp = itemHp;
    }
}