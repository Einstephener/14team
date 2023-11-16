public class Enemy
{
    public string EnemyName { get; } //적 이름
    public string HowHarder { get; } //출현 난이도
    public int EnemyAtk { get; } //적 공격력
    public int EnemyHp { get; set; } //적 체력

    public Enemy(string _enemyName, string howHarder, int _enemyAtk, int _enemyHp )
    {
        EnemyName = _enemyName;
        HowHarder = howHarder;
        EnemyAtk = _enemyAtk;
        EnemyHp = _enemyHp;

    }
}
