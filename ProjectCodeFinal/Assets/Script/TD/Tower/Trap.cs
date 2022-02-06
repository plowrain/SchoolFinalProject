using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private bool isBoom;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    
    IEnumerator TrapPoisoningAtk()
    {
        yield return new WaitForSeconds(2f);
        
        for (int i = 0; i <= Spwaner.enemy.Length - 1; i++)
        {
            if (Spwaner.enemy[i] == true && Vector2.Distance(Spwaner.enemy[i].transform.position, transform.position) <= 2f)
            {


                GameObject _monster = Spwaner.enemy[i];
                if (_monster.GetComponent<Enemy>().Poisoning == false)
                {
                    IDamageable iDamageable = Spwaner.enemy[i].GetComponent<IDamageable>();
                    iDamageable.KeepTakenDamage(5, 5f);
                    _monster.GetComponent<Enemy>().Poisoning = true;
                }
            }
        }
        Destroy(gameObject);
    }

    IEnumerator TrapFireAtk()
    {
        yield return new WaitForSeconds(2f);
        for (int i = 0; i <= Spwaner.enemy.Length - 1; i++)
        {
            if (Spwaner.enemy[i] == true && Vector2.Distance(Spwaner.enemy[i].transform.position, transform.position) <= 2f)
            {
                GameObject _monster = Spwaner.enemy[i];
                if(_monster.GetComponent<Enemy>().Fire == false)
                {
                    IDamageable iDamageable = Spwaner.enemy[i].GetComponent<IDamageable>();
                    iDamageable.KeepTakenDamage(15, 1f);
                    _monster.GetComponent<Enemy>().Fire = true;
                }
                
                
            }
        }
        Destroy(gameObject);
    }

    IEnumerator TrapIceAtk()
    {
        yield return new WaitForSeconds(2f);
        for (int i = 0; i <= Spwaner.enemy.Length - 1; i++)
        {
            if (Spwaner.enemy[i] == true && Vector2.Distance(Spwaner.enemy[i].transform.position, transform.position) <= 2f)
            {
                

                GameObject _monster = Spwaner.enemy[i];
                if (_monster.GetComponent<Enemy>().Ice == false)
                {
                    ISkill iskill = Spwaner.enemy[i].GetComponent<ISkill>();
                    iskill.ChangeSpeed(-100, true, -2f);
                    _monster.GetComponent<Enemy>().Ice = true;
                }
            }
        }
        Destroy(gameObject);
    }

    IEnumerator TrapEarthAtk()
    {
        yield return new WaitForSeconds(2f);
        for (int i = 0; i <= Spwaner.enemy.Length - 1; i++)
        {
            if (Spwaner.enemy[i] == true && Vector2.Distance(Spwaner.enemy[i].transform.position, transform.position) <= 2f)
            {
                

                GameObject _monster = Spwaner.enemy[i];
                if (_monster.GetComponent<Enemy>().Earth == false)
                {
                    ISkill iskill = Spwaner.enemy[i].GetComponent<ISkill>();
                    iskill.ChangeDef(-30, true, 4f);
                    iskill.ChangeSpeed(-30, true, 2f);
                    iskill.ChangeAtkSpeed(-50, true, 2f);
                    _monster.GetComponent<Enemy>().Earth = true;
                }
            }
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy" && isBoom == false)
        {
            if (transform.name == "TrapPoisoning")
            {
                StartCoroutine(TrapPoisoningAtk());
            }
            else if (transform.name == "TrapFire")
            {
                StartCoroutine(TrapFireAtk());
            }
            else if (transform.name == "TrapIce")
            {
                StartCoroutine(TrapIceAtk());
            }
            else if (transform.name == "TrapEarth")
            {
                StartCoroutine(TrapEarthAtk());
            }
            isBoom = true;
        }

        
    }

}
