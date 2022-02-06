using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System;


public class Castle : MonoBehaviour,IDamageable1
{

    [SerializeField] private GameObject prefabTowerObj;
    [SerializeField] private Text hpText;
    public int DeathNum;
    public float maxHealth;
    public float health;
    public static float totalExp;
    public Image HpEffectImage;
    private Scene scene;
    private bool death;
    [SerializeField]public GameObject[] prefabTower;
    public bool[] isLockPrefab;

    [SerializeField]
    public static bool[] tesla;
    public static int prefabTowerIndex;
    public bool[] isBuff;


    private void Awake()
    {


        FileStream fs = new FileStream(Application.dataPath + "/BuffData.txt", FileMode.Open);
        StreamReader sr = new StreamReader(fs);
        var text = "";
        for (int i = 0; i <= isBuff.Length - 1; i++)
        {
            text = sr.ReadLine();

            if (text == "True")
                isBuff[i] = true;
            else if (text == "False") isBuff[i] = false;
        }
        DeathNum = Convert.ToInt32(sr.ReadLine());

        sr.Close();
        fs.Close();
    }

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        
        scene = SceneManager.GetActiveScene();
        
        CheckLockTowerPrefab();
        death = false;


    }

    // Update is called once per frame
    void Update()
    {
        HpEffectImage.fillAmount = health / maxHealth;
        hpText.text = health + " / " + maxHealth;

    }

    public void Read()
    {
        
        FileStream fs = new FileStream(Application.dataPath + "/SaveData.txt", FileMode.Create);
        StreamWriter sw = new StreamWriter(fs);
        for(int i=0;i <= isLockPrefab.Length-1;i++)
        {
            sw.WriteLine(isLockPrefab[i]);
        }
        sw.WriteLine(scene.name);


        sw.Close();
        fs.Close();
    }

    public void Load()
    {
        FileStream fs = new FileStream(Application.dataPath + "/SaveData.txt", FileMode.Open);
        StreamReader sr = new StreamReader(fs);
        var text = "";
        for (int i=0;i<=isLockPrefab.Length - 1; i++)
        {
             text = sr.ReadLine();
          
            if (text == "True")
                isLockPrefab[i] = true;
            else if(text == "False") isLockPrefab[i] = false;
        }


        sr.Close();
        fs.Close();
    }

    public void CheckLockTowerPrefab()
    {
        prefabTowerIndex = 0;
        Load();
        for (int i = prefabTowerIndex; i <= isLockPrefab.Length - 1; i++)
        {

            if (isLockPrefab[i] == true)
            {
                GameObject _prefab;
                if (scene.name == "地下城")
                     _prefab = Instantiate(prefabTower[i], new Vector2(-8.5f + prefabTowerIndex, -4.5f), Quaternion.identity);
                else
                     _prefab = Instantiate(prefabTower[i], new Vector2(-13.45f + prefabTowerIndex, -9.45f), Quaternion.identity);
                _prefab.name = prefabTower[i].name;
                _prefab.transform.parent = prefabTowerObj.transform;
                prefabTowerIndex++;
                //_prefab.transform.parent = EmptyGameObj.transform;
            }
        }
        
        

    }

    public void AddTower(int _var)
    {


        GameObject _prefab = Instantiate(prefabTower[_var], new Vector2(-13.45f + prefabTowerIndex, -9.45f), Quaternion.identity);
        _prefab.name = prefabTower[_var].name;
        _prefab.transform.parent = prefabTowerObj.transform;
        prefabTowerIndex++;

        Read();
    }

    public void Addhp()
    {
        maxHealth *= 1.3f;
        health *= 1.3f;
    }

    public void TakenDamage(float _damageAmount)
    {

        health -= _damageAmount;
        HpEffectImage.fillAmount = health / maxHealth;
        GameObject a = GameObject.Find("Main Camera");
        a.GetComponent<CameraController>().Shake(0f);
        if(health <= 0)
        {

            FileStream fs = new FileStream(Application.dataPath + "/SaveData.txt", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            for (int i = 0; i <= isLockPrefab.Length - 1; i++)
            {
                sw.WriteLine(false);
            }
            sw.WriteLine("TowerDefenseGames");
            sw.Close();
            fs.Close();

            fs = new FileStream(Application.dataPath + "/BuffData.txt", FileMode.Create);
            sw = new StreamWriter(fs);
            for (int i = 0; i <= isBuff.Length - 1; i++)
            {
                sw.WriteLine(false);
            }
            sw.WriteLine(DeathNum);
            sw.Close();
            fs.Close();
            
            SceneManager.LoadScene("失敗");

        }
    }

}
