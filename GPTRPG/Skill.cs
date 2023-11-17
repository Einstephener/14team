using System.Dynamic;

public class Skill
{
    public string Name{ get; protected set;}
    public int MindCost{get; protected set;}

    public virtual void Execute(Character caster, Character target)
    {
        int damage = CalculateDamage(caster);
        Console.WriteLine("{스킬이름} 을 사용해 {타겟} 에게 {damage}의 데미지를 입혔습니다.");
    }
    public virtual int CalculateDamage(Character caster)
    {
        //기본데미지 설정
        return 0;
    }
}

//포병스킬
public class ArmorPiercerSkill : Skill
{
    public ArmorPiercerSkill()
    {
        Name = "철갑탄 발사";
        MindCost = 10;

    }

    public override void Execute(Character caster, Character target)
    {
        Console.WriteLine($"{caster.Name}의 {Name} 스킬을 {target.Name}에게 사용");

        int damage = CalculateDamage(caster);

        Console.WriteLine("${damage}의 데미지를 입혔습니다.");
    }

    //데미지 계산
    public override int CalculateDamage(Character caster)
    {
        //특정 스텟에 기반한 데미지 계산
        int damage = caster.Str*2;

        return damage;
    }
}

//보병스킬
public class CoordinationBarrageSkill : Skill
{
    public CoordinationBarrageSkill()
    {
        Name = "연발 사격";
        MindCost = 10;

    }

    public override void Execute(Character caster, Character target)
    {
        for (int i = 0; i < 5; i++)
        {
            int damage = CalculateDamage(caster);
            

            Console.WriteLine($"{caster.Name}이(가) {target.Name}에게 {damage}의 데미지를 입혔습니다.");
        }
    }

    //데미지 계산
    public override int CalculateDamage(Character caster)
    {
        //특정 스텟에 기반한 데미지 계산
        int damage = caster.Dex*2;

        return damage;
    }
}

//운전병 스킬
public class K_511AttackSkill : Skill
{
    public K_511AttackSkill()
    {
        Name = "두돈반 돌진";
        MindCost = 10;

    }

    public override void Execute(Character caster, Character target)
    {
        Console.WriteLine($"{caster.Name}의 {Name} 스킬을 {target.Name}에게 사용");

        int damage = CalculateDamage(caster);

        Console.WriteLine("${damage}의 데미지를 입혔습니다.");
    }

    //데미지 계산
    public override int CalculateDamage(Character caster)
    {
        //특정 스텟에 기반한 데미지 계산
        int damage = caster.Luk*2;

        return damage;
    }
}

//정비병 스킬
public class fuckSkill : Skill
{
    public fuckSkill()
    {
        Name = "뭐만들지";
        MindCost = 10;

    }

    public override void Execute(Character caster, Character target)
    {
        Console.WriteLine($"{caster.Name}의 {Name} 스킬을 {target.Name}에게 사용");

        int damage = CalculateDamage(caster);

        Console.WriteLine("${damage}의 데미지를 입혔습니다.");
    }

    //데미지 계산
    public override int CalculateDamage(Character caster)
    {
        //특정 스텟에 기반한 데미지 계산
        int damage = caster.IQ*2;

        return damage;
    }
}