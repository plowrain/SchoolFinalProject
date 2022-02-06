using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Enemy : Liver
{

    protected Animator animator;
    public Path path;//The path
    public int pathIndex = 0;                       //从初始位置触发
    protected float time;
    protected Scene scene;
    private int wards;
    public  bool Poisoning, Fire, Ice, Earth;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        scene = SceneManager.GetActiveScene();
        
        if (transform.GetComponent<Animator>() != null)
            animator = transform.GetComponent<Animator>();
        if(scene.name == "森林")
        {
            int _randNum = UnityEngine.Random.Range(1, 4);
            path = GameObject.Find("MonsterPath0" + _randNum).GetComponent<Path>();
            transform.position = path.GetPosition(0);
        }
        else if(scene.name == "TowerDefenseGames")
        {
            int _randNum = UnityEngine.Random.Range(1, 7);
            path = GameObject.Find("MonsterPath0" + _randNum).GetComponent<Path>();
            transform.position = path.GetPosition(0);
        }
        else if(scene.name == "地下城")
        {
            int _randNum = UnityEngine.Random.Range(1, 5);
            path = GameObject.Find("MonsterPath0" + _randNum).GetComponent<Path>();
            transform.position = path.GetPosition(0);
        }

    }
   
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (transform.position != path.GetPosition(pathIndex))
        {
            MoveToThePoints();
        }
        //到了数组指定index位置,改变index值，不断循环
        else
        {
            if (pathIndex < path.Length - 1)
                pathIndex++;
            else if (loop == true)
                pathIndex = 0;
            else
                return;
        }

    }

    public Vector2 getInsterPos()
    {
       

        return path.GetPosition(0);
    }
    protected void MoveToThePoints()
    {
        Vector2 lastTemp;
        //从当前位置按照指定速度移到index位置，记得speed * Time.deltaTime，不然会瞬移
        Vector2 temp = Vector2.MoveTowards(transform.position, path.GetPosition(pathIndex), speed * Time.deltaTime);
        lastTemp = transform.position;
        if(animator.GetInteger("Wards") !=5 )
        {
            if (lastTemp.x > temp.x)
            {
                wards = 2;
                animator.SetInteger("Wards", 2);
                Debug.Log("往左邊走");
            }
            else if (lastTemp.x < temp.x)
            {
                wards = 3;
                animator.SetInteger("Wards", 3);
                Debug.Log("往右邊走");
            }
            else if (lastTemp.y < temp.y)
            {
                wards = 0;
                animator.SetInteger("Wards", 0);
                Debug.Log("往上");
            }
            else if (lastTemp.y > temp.y)
            {
                wards = 1;
                animator.SetInteger("Wards", 1);
                Debug.Log("往下");
            }

        }
            
        
        

        //考虑到可能有碰撞检测，所以使用刚体的移动方式
        GetComponent<Rigidbody2D>().MovePosition(temp);
    }


    protected void TowerDie()
    {
        speed = lastSpeed;
        animator.SetInteger("Wards", wards);
    }

    IEnumerator timer3(Collider2D _col)
    {
        time += Time.deltaTime;

        yield return new WaitForSeconds(0);
        if(_col != null)
        {

            animator.SetInteger("Wards", 5);
            IDamageable iDamageable = _col.GetComponent<IDamageable>();
            
            
            if (time >= atkSpeed)
            {
               
                time = 0;
                iDamageable.TakenDamage(atk, penetration, elementa.ToString(), fixDef);

            }


        }

    }
    protected void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "End")
        {
            Die();

            IDamageable1 idamageable1 = col.GetComponent<IDamageable1>();
            if (level <= 1) level = 1;
            idamageable1.TakenDamage(2*level);

        }
        if(col.tag =="Tower")
        {
            GameObject _target = col.gameObject;

            //spwanEnemy.GetComponent<Enemy>().onDeath += EnemyDeath;
            col.GetComponent<Tower>().onDeath += TowerDie;
        }

    }

    protected void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Tower")
        {
            
            //位置"可能"不夠接近，就停下來 導致TRIG判斷有問題，明天用距離改 isWalk判斷。
            //if(Vector2.Distance(transform.position, col.transform.position) <= 0.3f)
            //    if(isWalk ==true)isWalk = false;
            var time = atkSpeed;
            StartCoroutine("timer3",col);
            speed = 0.001f;
            
            
            
        }

    }









}
