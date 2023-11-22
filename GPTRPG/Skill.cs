using System.Dynamic;

public class Skill
{
    public string Name{ get; protected set;}
    public int MindCost{get; protected set;}
    public string Description {  get; protected set;}

    public virtual void Execute(Character caster, Enemy target)
    {
        int damage = CalculateDamage(caster);
        Console.WriteLine("{Name} 을 사용해 {target.EnemyName} 에게 {damage}의 데미지를 입혔습니다.");
    }
    public virtual int CalculateDamage(Character caster)
    {
        //기본데미지 설정
        return 0;
    }

    public bool CanUseSkill(Character caster)
    {
        return caster.Mind >= MindCost;
    }
}

//포병스킬
public class ArmorPiercerSkill : Skill
{
    public ArmorPiercerSkill()
    {
        Name = "철갑탄 발사";
        MindCost = 10;
        Description = "힘 * 2 의 데미지를 줍니다.";
    }

    public override void Execute(Character caster, Enemy target)
    {
        if (!CanUseSkill(caster))
        {
            Console.WriteLine("");
            Console.WriteLine(" 정신력이 부족합니다.");
            Console.ReadKey();
            return;
        }

        int damage = CalculateDamage(caster);
        Console.WriteLine("");
        Console.WriteLine($" {caster.Name}의 {Name} 스킬을 {target.EnemyName}에게 사용");
        Console.WriteLine($" {caster.Name}이(가) {target.EnemyName}에게 {damage}의 데미지를 입혔습니다.");
        target.EnemyHp -= damage;
        caster.Mind -= MindCost;
    }

    //데미지 계산
    public override int CalculateDamage(Character caster)
    {
        //특정 스텟에 기반한 데미지 계산
        int skillDamage = caster.Str * 2;

        double trueDamage = Math.Round(skillDamage * RandomRange(0.9, 1.1));
        return (int)trueDamage;

    }
    private double RandomRange(double min, double max)
    {
        Random rand = new Random();
        return min + rand.NextDouble() * (max - min);
    }
}

//보병스킬
public class CoordinationBarrageSkill : Skill
{
    public CoordinationBarrageSkill()
    {
        Name = "연발 사격";
        MindCost = 10;
        Description = "힘 * 2 의 데미지를 줍니다.";
    }

    public override void Execute(Character caster, Enemy target)
    {
        if (!CanUseSkill(caster))
        {
            Console.WriteLine("");
            Console.WriteLine(" 정신력이 부족합니다.");
            Console.ReadKey();
            return;
        }
        int damage = CalculateDamage(caster);
        Console.WriteLine("");
        Console.WriteLine($" {caster.Name}의 {Name} 스킬을 {target.EnemyName}에게 사용");        
        Console.WriteLine($" {caster.Name}이(가) {target.EnemyName}에게 {damage}의 데미지를 입혔습니다.");        
        target.EnemyHp -= damage;
        caster.Mind -= MindCost;
    }

    //데미지 계산
    public override int CalculateDamage(Character caster)
    {
        //특정 스텟에 기반한 데미지 계산
        int skillDamage = caster.Str * 2;

        double trueDamage = Math.Round(skillDamage * RandomRange(0.9, 1.1));
        return (int)trueDamage;

    }
    private double RandomRange(double min, double max)
    {

        Random rand = new Random();
        return min + rand.NextDouble() * (max - min);
    }
}

//운전병 스킬
public class K_511AttackSkill : Skill
{
    public K_511AttackSkill()
    {
        Name = "두돈반 돌진";
        MindCost = 10;
        Description = "힘 * 2 의 데미지를 줍니다.";
    }

    public override void Execute(Character caster, Enemy target)
    {
        if (!CanUseSkill(caster))
        {
            Console.WriteLine("");
            Console.WriteLine(" 정신력이 부족합니다.");
            Console.ReadKey();
            return;
        }
        int damage = CalculateDamage(caster);
        Console.WriteLine("");
        Console.WriteLine($" {caster.Name}의 {Name} 스킬을 {target.EnemyName}에게 사용");
        Console.WriteLine($" {caster.Name}이(가) {target.EnemyName}에게 {damage}의 데미지를 입혔습니다.");
        target.EnemyHp -= damage;
        caster.Mind -= MindCost;
    }

    //데미지 계산
    public override int CalculateDamage(Character caster)
    {
        //특정 스텟에 기반한 데미지 계산
        int skillDamage = caster.Str*2;

        double trueDamage = Math.Round(skillDamage * RandomRange(0.9, 1.1));
        return (int)trueDamage;

    }
    private double RandomRange(double min, double max)
    {
        Random rand = new Random();
        return min + rand.NextDouble() * (max - min);
    }
}

//정비병 스킬
public class fuckSkill : Skill
{
    public fuckSkill()
    {
        Name = "뭐만들지";
        MindCost = 10;
        Description = "힘 * 2 의 데미지를 줍니다.";
    }

    public override void Execute(Character caster, Enemy target)
    {
        if (!CanUseSkill(caster))
        {
            Console.WriteLine("");
            Console.WriteLine(" 정신력이 부족합니다.");
            Console.ReadKey();
            return;
        }
        int damage = CalculateDamage(caster);
        Console.WriteLine("");
        Console.WriteLine($" {caster.Name}의 {Name} 스킬을 {target.EnemyName}에게 사용");
        Console.WriteLine($" {caster.Name}이(가) {target.EnemyName}에게 {damage}의 데미지를 입혔습니다.");
        target.EnemyHp -= damage;
        caster.Mind -= MindCost;
    }

    //데미지 계산
    public override int CalculateDamage(Character caster)
    {
        //특정 스텟에 기반한 데미지 계산
        int skillDamage = caster.Str * 2;

        double trueDamage = Math.Round(skillDamage * RandomRange(0.9, 1.1));
        return (int)trueDamage;

    }
    private double RandomRange(double min, double max)
    {
        Random rand = new Random();
        return min + rand.NextDouble() * (max - min);
    }
}