//소비형 아이템
public class Food
{
    public string ItemName { get; } //아이템 이름
    public int ItemStr { get; } //아이템 공격력
    public int ItemDex { get; } //아이템 방어력
    public int ItemIq { get; } //아이템 방어력
    public int ItemLuk { get; } //아이템 방어력
    public int ItemMind { get; } //아이템 방어력
    public int ItemHp { get; } //아이템 방어
    public int ItemGold { get; } //아이템 가격
    public string ItemDescription { get; set; } //아이템 상세설명

    public Food(string itemName, int itemStr, int itemDex,int itemIq,int itemLuk,int itemMind, int itemGold,  string itemDescription)
    {
        ItemName = itemName;
        ItemAtk = itemAtk;
        ItemDef = itemDef;
        ItemGold = itemGold;

        ItemDescription = itemDescription;
    }
}


//장비형 아이템
public class Item
{
    public string ItemDescription { get; set; } //아이템 상세설명
    public string ItemName { get; } //아이템 이름
    public int ItemAtk { get; } //아이템 공격력
    public int ItemDef { get; } //아이템 방어력
    public int ItemGold { get; } //아이템 가격
    public bool IsEquipped { get; set; } //아이템 착용여부

    public Item(string itemName, int itemAtk, int itemDef, int itemGold, string itemDescription, bool IsEquipped = false)
    {
        ItemName = itemName;
        ItemAtk = itemAtk;
        ItemDef = itemDef;
        ItemGold = itemGold;
        ItemDescription = itemDescription;
    }
}
