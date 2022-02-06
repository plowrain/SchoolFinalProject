using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Liver : MonoBehaviour,IDamageable,ISkill
{



    //https://www.bilibili.com/video/BV1ia4y1j78A 參考網址
    //基礎屬性設定
    private Scene scene;
    public float maxHealth;//最大生命
    [SerializeField] protected float health;
    [SerializeField] protected float atk;//攻擊
    [SerializeField] protected float atkSpeed;//攻擊速度
    [Range(0f, 80f)]
    [SerializeField] protected float def;//防禦率 限制0~80
    [SerializeField] protected float fixDef;//固定防禦
    [SerializeField] protected int level;//等級
    protected float lastSpeed;//改變前的速度
    [SerializeField] protected float speed;//following speed
    [SerializeField] protected int exp;//經驗值
    [Range(0f, 50f)]
    [SerializeField] protected float penetration;//穿透
    [SerializeField] protected int cost;//蓋塔消耗的能量
    [SerializeField] protected float needExp;//升等經驗
    public GameObject dieEffectAniPrefab;
    public Castle castle;
    //MUSIC
    [SerializeField]protected Animator ani;
    [SerializeField] protected AudioClip takenDamMusic;
    [SerializeField] protected AudioSource music;
    protected SpriteRenderer sprite;

    protected enum Elementa
    {
        Null = 0,
        fire,
        wood,
        water,
        gold,
        earth
    }
    [SerializeField] protected Elementa elementa;

    [SerializeField] protected Image healthPointEffect;
    [SerializeField] protected bool loop = false;

    protected bool isDead;
    //private float time;

    //事件格式: public + event key + Delaqate +eventName(no + foo)
    public event Action onDeath;
    /*start{
     * if(a[i] == true) 
     * {
     *    upup
     * 
     * }
     * 
     * }
     * B C#  bool == true
     * */

    //public int pathIndex = 0;                       //从初始位置触发
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        dieEffectAniPrefab = (GameObject)Resources.Load("Prefabs/TD/Effect/DestroyEffect_0");
        sprite = GetComponent<SpriteRenderer>();
        music = GetComponent<AudioSource>();
        takenDamMusic = Resources.Load("MP3/怪物被打") as AudioClip;
        scene = SceneManager.GetActiveScene();
        music.clip = takenDamMusic;

        ani = GetComponent<Animator>();
        music.spatialBlend = 1;
        music.rolloffMode = AudioRolloffMode.Logarithmic;
        music.minDistance = 1;
        music.volume = 0.65f;
        castle = GameObject.Find("Castle").GetComponent<Castle>();
        health = maxHealth;
        lastSpeed = speed;
        getBuff();
    }

    // Update is called once per frame
    protected virtual void Update()
    {


        healthPointEffect.fillAmount = health / maxHealth;
        
    }

    /* 
     * 1.攻擊 +20%
     * 2.攻擊速度+20%
     * 3.生命力+30%
     * 4.防禦力+50%
     * 5.穿透率 +30%
     * 6.怪物血量 -20%
     * 7.怪物防禦 -30%
     * 8.怪物緩速 -20%
     * 9.增加主堡生命 +30%
     * 10.cost每秒增加+1
     * 11.初始能量增加 +20
     * 12.cost增加上限
     * 
     * */

    public void getBuff()
    {

        if(castle.isBuff[0] == true)
        {
            if(transform.tag == "Tower")
            {
                atk *= 1.2f;
            }
        }

        if(castle.isBuff[1] == true)
        {
            if(transform.tag == "Tower")
            {
                atkSpeed *= 0.8f;
            }
        }
        if (castle.isBuff[2] == true)
        {
            if(transform.tag == "Tower")
            {
                health *= 1.3f;
            }
        }
        if (castle.isBuff[3] == true)
        {
            if(transform.tag == "Tower")
            {
                def *= 1.5f;
            }

        }
        if (castle.isBuff[4] == true)
        {
            if(transform.tag == "Tower")
            {
                penetration += 30;
            }
        }

        if (castle.isBuff[5] == true)
        {
            if(transform.tag == "Enemy")
            {
                health *= 0.8f;
                atk *= 1.2f;
            }
        }
        if (castle.isBuff[6] == true)
        {
            if(transform.tag == "Enemy")
            {
                speed *= 0.7f;

            }

        }



    }

    protected virtual void LevelUpAdd()
    {

    }

    public float getability(int _var)
    {
        float[] _data = new float[11];
        _data[0] = maxHealth;
        _data[1] = health;
        _data[2] = atk;
        _data[3] = atkSpeed;
        _data[4] = def;
        _data[5] = fixDef;
        _data[6] = penetration;
        _data[7] = cost;
        _data[8] = (int)elementa;
        _data[9] = level;
        _data[10] = needExp;
        return _data[_var];
    }

    /// <summary>
    /// 初始化 1.最大生命 2.攻擊 3.攻擊速度 4.防禦力 5.移動速度 6.穿透 7.經驗值 8. 固定防禦力 9. 屬性
    /// Null = 0, fire=1, wood=2, water=3, gold=4 ,earth=5
    /// </summary>
     /*
    protected void setAbility(float _maxHealth, float _atk, float _atkSpeed, float _def, float _speed, float _penetration, float _exp, float _fixDef, int _ele)
    {
        maxHealth = _maxHealth;
        atk = _atk;
        atkSpeed = _atkSpeed;
        def = _def;
        speed = _speed;
        penetration = _penetration;
        exp = _exp;
        fixDef = _fixDef;
        elementa = (Elementa)_ele;
    }*/

    protected void Die()
    {
        isDead = true;
        Castle.totalExp += exp;
        if(transform.tag == "Enemy" )
        {
            Instantiate(dieEffectAniPrefab,transform.position, transform.rotation);
        }
        Destroy(gameObject);
        if (onDeath != null)
            onDeath();//事件，EVEN e1 在A裡面 A負責CALL  ，實現方法則在B裡面。
                      /* B有Fn1()， A.E1+=fn; 
                       * 把實現方法嘉進A的E1裡面
                       */

    }

    /// <summary>
    /// 怪物受到傷害寫在這裡，需傳入傷害跟屬性。
    /// 血 -（攻擊 ×（1 -（防禦×穿透%）%）×屬性）-固防） -固傷
    /// </summary>
    IEnumerator BackColor()
    {
        yield return new WaitForSeconds(0.1f);
        sprite.color = new Color(255, 255, 255, 1f);
    }

    public void TakenDamage(float _damageAmount, float _penetration, string _elementa, float _fixatk)
    {
        if(gameObject.tag == "Enemy")
        {
            sprite.color = new Color(255, 0, 0, 1f);
            
            StartCoroutine(BackColor());
        }
        var _ele = 1f;
        _ele = PropertyConfirmation(_elementa); //確認屬性相生相剋

        _damageAmount = _damageAmount * (1 - (def * (_penetration / 100) / 100) * _ele) - _fixatk + fixDef;
        if (_damageAmount <= 1) _damageAmount = 1;

        health -= _damageAmount;

        music.PlayOneShot(takenDamMusic);
   
        if (health <= 0 && isDead == false)
        {
            if(gameObject.tag == "Enemy")
            {
                Debug.Log("exp = "+ exp);
                TowerSystem.Exp += exp;
            }
            Die();
        }

    }
    /// <summary>
    /// 屬性相生相剋
    /// </summary>
    /// <param name="_elementa"></param>
    /// <returns></returns>
    public float PropertyConfirmation(string _elementa)
    {
        var _ele = 1f;
        //五個剋至 A為物件屬性  B為攻擊屬性  攻擊屬性被克制 傷害減少
        if (elementa == Elementa.water && _elementa == "fire")
        {
            _ele = 0.75f;
        }
        else if (elementa == Elementa.fire && _elementa == "water") _ele = 1.4f;

        if (elementa == Elementa.wood && _elementa == "earth")
        {
            _ele = 0.75f;
        }
        else if (elementa == Elementa.earth && _elementa == "wood") _ele = 1.4f;

        if (elementa == Elementa.fire && _elementa == "gold")
        {
            _ele = 0.75f;
        }
        else if (elementa == Elementa.gold && _elementa == "fire") _ele = 1.4f;

        if (elementa == Elementa.earth && _elementa == "water")
        {
            _ele = 0.75f;
        }
        else if (elementa == Elementa.water && _elementa == "earth") _ele = 1.4f;

        if (elementa == Elementa.gold && _elementa == "wood")
        {
            _ele = 0.75f;
        }
        else if (elementa == Elementa.wood && _elementa == "gold") _ele = 1.4f;



        //五個相生
        if (elementa == Elementa.earth && _elementa == "fire")
        {
           if(gameObject.tag == "Enemy")
            {
                ChangeAtk(5, false, 0);
                ChangeSpeed(3, false, 0);
            }
        }
        if (elementa == Elementa.gold && _elementa == "earth")
        {
            
        }
        if (elementa == Elementa.water && _elementa == "gold")
        {
           
        }
        if (elementa == Elementa.wood && _elementa == "water")
        {
           
        }
        if (elementa == Elementa.fire && _elementa == "wood")
        {
            
        }

        return _ele;
    }

    public void KeepTakenDamage(float _damageAmount, float _time)
    {
        StartCoroutine(keepDam(_damageAmount, _time));
       
    }

    IEnumerator keepDam(float _damageAmount, float _time)
    {


        
        
        for (float i = 0; i <= _time; i += Time.deltaTime)
        {

            
            health -= _damageAmount;
            if (health <= 0 && isDead == false)
            {
                Die();
            }
            yield return new WaitForSeconds(1f);
        }
    }


    public void ChangeSpeed(float _speed, bool _bool, float _time)
    {

        if (_bool == true)
        {

            var lastSpeed = speed * -_speed / 100;
            StartCoroutine(backset(_time, "speed", lastSpeed));
        }

        speed *= _speed / 100 + 1;

    }

    public void ChangeAtk(float _atk, bool _bool, float _time)
    {
        if (_bool == true)
        {

            var lastAtk = speed * -_atk / 100;
            StartCoroutine(backset(_time, "speed", lastAtk));
        }
        atk *= 1 + _atk / 100;
    }

    public void ChangeAtkSpeed(float _atkSpeed, bool _bool, float _time)
    {
        if (_bool == true)
        {

            var lastAtkSpeed = speed * -_atkSpeed / 100;
            StartCoroutine(backset(_time, "speed", lastAtkSpeed));
        }
        atkSpeed *= 1 + _atkSpeed /100;
    }

    public void ChangeDef(float _def, bool _bool, float _time)
    {
        if (_bool == true)
        {

            var lastDef = speed * -_def / 100;
            StartCoroutine(backset(_time, "speed", lastDef));
        }
        def *= (100 + def) / 100;
    }

    public void ChangePenetration(float _penetration, bool _bool, float _time)
    {
        if (_bool == true)
        {

            var lastPenetration = speed * -_penetration / 100;
            StartCoroutine(backset(_time, "speed", lastPenetration));
        }
        penetration *= _penetration / 100 + 1;
    }

    public void LevelUp()
    {
        level += 1;
        if(gameObject.tag == "Tower")
        {
            atk *= 1.2f;
            atkSpeed *= 0.8f;
            maxHealth *= 1.2f;
            health *= 1.2f;
            needExp *= 1.2f;
        }
        if(gameObject.tag == "Enemy")
        {
            if(scene.name== "森林")
            {
                atk *= 1.3f;
                atkSpeed *= 0.8f;
                maxHealth *= 1.55f;
                health *= 1.55f;
            }
            else if(scene.name == "地下城")
            {
                atk *= 1.2f;
                atkSpeed *= 0.9f;
                maxHealth *= 1.45f;
                health *= 1.45f;
            }
            else
            atk *= 1.1f;
            maxHealth *= 1.35f;
            health *= 1.35f;
        }
        
        LevelUpAdd();
    }

    IEnumerator backset(float _time, string _str, float _lastNum)
    {
        yield return new WaitForSeconds(_time);

        switch (_str)
        {
            case ("speed"):

                speed += _lastNum;
                break;
            case ("atk"):
                atk += _lastNum;
                break;
            case ("atkSpeed"):
                atkSpeed += _lastNum;
                break;
            case ("def"):
                def += _lastNum;
                break;
            case ("penetration"):
                penetration += _lastNum;
                break;
            case ("fixDef"):
                fixDef += _lastNum;
                break;


        }


    }

}
