public class Enemy
{
    public string EnemyName { get; } //적 이름
    public int EnemyAtk { get; } //적 공격력
    public int EnemyHp { get; set; } //적 체력

    public Enemy(string _enemyName, int _enemyAtk, int _enemyHp )
    {
        EnemyName = _enemyName;
        EnemyAtk = _enemyAtk;
        EnemyHp = _enemyHp;

    }
}
