using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Spwaner : MonoBehaviour
{
    public AudioSource backMusic;
    public AudioClip Winer;
    public GameObject enemyPrefab;
    public GameObject victoryObj;
    private Scene scene;
    [SerializeField] private int DieNum;
    [SerializeField] private GameObject getChooiseTowerPrefab;//選塔的OBJ
    [SerializeField] private GameObject getBuffObj;//一大關通關後的BUFF
    [SerializeField] private GameObject EmptyEmeny;
    [SerializeField]public static GameObject[] enemy;
    [SerializeField] private Text MaxWaves;
    [SerializeField] private Text NowWaves;
    //怪物波數控制
    public Wave[] waves;
    public static int monsterCount;
    [Header("Just For Check  !!")]
    [SerializeField]private Wave currentWave;
    [SerializeField]private int currentIndex;
    private bool delaytime;
    public int waitSpawneNum;//剩餘多少敵人 =0 沒有敵人
    public int spawnAliveNum;//當前存活敵人 =0 下一波
    public float nextSpawnTime;//
    Castle castle;
    private void Start()
    {
        scene = SceneManager.GetActiveScene();
        NextWave();
        GetEnemy();
        monsterCount = 0;
        castle = GameObject.Find("Castle").GetComponent<Castle>();
        MaxWaves.text = "/ "+waves.Length.ToString();
        StartCoroutine(delay1());
        //getTowerPrefab.SetActive(true);
    }

    private void Update()
    {
        if (delaytime == true) Spwan();
    }

    IEnumerator delay1()
    {
        yield return new WaitForSeconds(10);
        delaytime = true;
    }
    
    public void Spwan()
    {
        if (waitSpawneNum > 0 && Time.time > nextSpawnTime)
        {
            waitSpawneNum--;
            monsterCount++;
            GameObject spwanEnemy = Instantiate(waves[currentIndex - 1].targetEnemy, transform.position, Quaternion.identity);
            //敵人生成 把事件訂閱到onDeanth;
            //spwanEnemy.GetComponent<Enemy>().path.GetPosition(0);
            //path.GetPosition(pathIndex)) -14.78f, -8.64f
            for (int i = 0; i < currentIndex - 1; i++)
                spwanEnemy.GetComponent<Enemy>().LevelUp();
            spwanEnemy.transform.position = new Vector2(-14.78f, -8.64f);
            spwanEnemy.name = enemyPrefab.name + "_" + monsterCount.ToString();
            spwanEnemy.transform.parent = EmptyEmeny.transform;
            enemy[monsterCount - 1] = spwanEnemy;

            spwanEnemy.GetComponent<Enemy>().onDeath += EnemyDeath;
            nextSpawnTime = Time.time + currentWave.timeBtwSpawn; //控制怪物時間
        }
    }


    private void NextWave()
    {
        currentIndex++;
        NowWaves.text = currentIndex.ToString();
        Debug.Log("Current Wave " + currentIndex);

        if(currentIndex - 1 < waves.Length)
        {
            
            currentWave = waves[currentIndex - 1];
            waitSpawneNum = currentWave.enemyNum;
            spawnAliveNum = currentWave.enemyNum;
        }
        if (currentIndex - 1 == waves.Length)
        {
            Debug.Log("Victory");
            if(castle.health>0)
            {
                if(scene.name != "森林")
                {
                    getBuffObj.SetActive(true);
                    StartCoroutine(victory());
                }
                else if(scene.name == "森林")
                {
                    SceneManager.LoadScene("GoodEnd");
                }
                    
            }
            
        }

    }
    
    IEnumerator victory()
    {
        yield return new WaitForSeconds(1f);
 
        backMusic.clip = Winer;
        backMusic.PlayOneShot(Winer);
    }

    private void GetEnemy()
    {
        var Num = 0;
        for(int i=0;i<=waves.Length-1;i++)
        {
            Num += waves[i].enemyNum;
        }
        enemy = new GameObject[Num];
    }

    private void EnemyDeath()
    {
        spawnAliveNum--;
        DieNum++;
        if(DieNum % 30 == 0)
        {
            getChooiseTowerPrefab.SetActive(true);
        }
        if (spawnAliveNum <=0)
        {
            NextWave();
        }
    }





}
