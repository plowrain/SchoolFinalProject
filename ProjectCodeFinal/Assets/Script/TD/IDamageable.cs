public interface IDamageable 
{
    /// <summary>
    /// 怪物受到傷害寫在這裡，需傳入1.傷害2.穿透3.屬性4.固定傷害。
    /// 血 -（攻擊 ×（1 -（防禦×穿透%）%）×屬性）-固防） -固傷
    /// </summary>
    void TakenDamage(float _damageAmount, float _penetration, string _elementa, float _fixatk);

    /// <summary>
    /// 持續傷害 1.傷害 2.時間
    /// </summary>
    /// <param name="_damageAmount"></param>
    /// <param name="_time"></param>
    void KeepTakenDamage(float _damageAmount, float _time);
}

public interface IDamageable1
{
    void TakenDamage(float _damageAmount);
}


