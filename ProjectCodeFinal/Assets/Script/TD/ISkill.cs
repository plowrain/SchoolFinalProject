public interface ISkill
{
    /// <summary>
    /// 速度*(1+速度)/100
    /// </summary>
    void ChangeSpeed(float _speed, bool _bool,float _time);

    void ChangeAtk(float _atk, bool _bool, float _time);

    void ChangeAtkSpeed(float _atkSpeed, bool _bool, float _time);

    void ChangeDef(float _def, bool _bool, float _time);

    void ChangePenetration(float _penetration, bool _bool, float _time);

    void LevelUp();

}



