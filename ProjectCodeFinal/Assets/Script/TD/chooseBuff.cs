using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class chooseBuff : MonoBehaviour
{

    public Text leftText,midText,rightText;
    public int leftNum,midNum, rightNum;
    Castle castle;
    [Header("跳出勝利的OBJ")]
    [SerializeField] private GameObject victoryObj;//一大關通關後的BUFF
    TowerSystem towerSystem;
    Vector2 pos;
    // Start is called before the first frame update
    private void OnEnable()
    {
        Start();
    }

    void Start()
    {
        castle = GameObject.Find("Castle").GetComponent<Castle>();
        towerSystem = GameObject.Find("Script").GetComponent<TowerSystem>();
        pos = GameObject.Find("Main Camera").transform.position;
        transform.position = pos;
        changeText();
    }

    /*
     *     StartCoroutine(victory());
    IEnumerator victory()
    {
        yield return new WaitForSeconds(1f);
       
        
    }

    */

    void Update()
    {
        
    }

    public void SpecailBuff()
    {

        if (castle.isBuff[8] == true)
        {
            castle.Addhp();
            castle.isBuff[8] = false;
        }

        if (castle.isBuff[9] == true)
        {
            towerSystem.CostHealUp();
            castle.isBuff[9] = false;
        }

        if (castle.isBuff[10] == true)
        {
            //TowerSystem.totalcost += 20;
            //Castle.isBuff[10] = false;
        }

        if (castle.isBuff[11] == true)
        {
            towerSystem.CostMaxUp();
            castle.isBuff[11] = false;
        }

    }

    public void changeText()
    {
        /* 1.攻擊 +30%
    * 2.攻擊速度+20%
    * 3.生命力+40%
    * 4.防禦力+50%
    * 5.穿透率+30
    * 
    * */

        leftText.text  = "";
        midText.text = "";
        rightText.text = "";
        leftNum = Random.Range(0, 12);
        while(rightNum == leftNum)
            rightNum = Random.Range(0, 12);
        while (rightNum == midNum || midNum == leftNum)
            midNum = Random.Range(0,12);

        switch (leftNum)
        {
            case 0:
                leftText.text = "攻擊力 +20%";
                break;
            case 1:
                leftText.text = "攻擊速度+20%";
                break;
            case 2:
                leftText.text = "生命力 +30%";
                break;
            case 3:
                leftText.text = "防禦力 +50%";
                break;
            case 4:
                leftText.text = "穿透率 +30%";
                break;
            case 5:
                leftText.text = "怪物血量 -20%";
                break;
            case 6:
                leftText.text = "怪物防禦 -30%";
                break;
            case 7:
                leftText.text = "怪物緩速 -20%";
                break;
            case 8:
                leftText.text = "增加主堡生命 +30%";
                break;
            case 9:
                leftText.text = "cost每秒增加+1";
                break;
            case 10:
                leftText.text = "初始能量增加 +20";
                break;
            case 11:
                leftText.text = "cost增加上限";
                break;
        }

        switch (midNum)
        {
            case 0:
                midText.text = "攻擊力 +20%";
                break;
            case 1:
                midText.text = "攻擊速度+20%";
                break;
            case 2:
                midText.text = "生命力 +30%";
                break;
            case 3:
                midText.text = "防禦力 +50%";
                break;
            case 4:
                midText.text = "穿透率 +30%";
                break;
            case 5:
                midText.text = "怪物血量 -20%";
                break;
            case 6:
                midText.text = "怪物防禦 -30%";
                break;
            case 7:
                midText.text = "怪物緩速 -20%";
                break;
            case 8:
                midText.text = "增加主堡生命 +30%";
                break;
            case 9:
                midText.text = "cost每秒增加+1";
                break;
            case 10:
                midText.text = "初始能量增加 +20";
                break;
            case 11:
                midText.text = "cost增加上限";
                break;   
        }

        switch (rightNum)
        {
            case 0:
                rightText.text = "攻擊力 +20%";
                break;
            case 1:
                rightText.text = "攻擊速度+20%";
                break;
            case 2:
                rightText.text = "生命力 +30%";
                break;
            case 3:
                rightText.text = "防禦力 +50%";
                break;
            case 4:
                rightText.text = "穿透率 +30%";
                break;
            case 5:
                rightText.text = "怪物血量 -20%";
                break;
            case 6:
                rightText.text = "怪物防禦 -30%";
                break;
            case 7:
                rightText.text = "怪物緩速 -20%";
                break;
            case 8:
                rightText.text = "增加主堡生命 +30%";
                break;
            case 9:
                rightText.text = "cost每秒增加+1";
                break;
            case 10:
                rightText.text = "初始能量增加 +20";
                break;
            case 11:
                rightText.text = "cost增加上限";
                break;
        }


    }

    public void LeftBt()
    {
        
        

        castle.isBuff[leftNum] = true;
        FileStream fs = new FileStream(Application.dataPath + "/BuffData.txt", FileMode.Create);
        StreamWriter sw = new StreamWriter(fs);
        for (int i = 0; i <= castle.isBuff.Length - 1; i++)
        {
            sw.WriteLine(castle.isBuff[i]);
        }
        sw.Close();
        fs.Close();
        victoryObj.SetActive(true);
        gameObject.SetActive(false);
    }

    public void MidBt()
    {
        castle.isBuff[midNum] = true;
        FileStream fs = new FileStream(Application.dataPath + "/BuffData.txt", FileMode.Create);
        StreamWriter sw = new StreamWriter(fs);
        for (int i = 0; i <= castle.isBuff.Length - 1; i++)
        {
            sw.WriteLine(castle.isBuff[i]);
        }
        sw.Close();
        fs.Close();
        victoryObj.SetActive(true);
        gameObject.SetActive(false);
    }

    public void RightBt()
    {
        castle.isBuff[rightNum] = true;
        FileStream fs = new FileStream(Application.dataPath + "/BuffData.txt", FileMode.Create);
        StreamWriter sw = new StreamWriter(fs);
        for (int i = 0; i <= castle.isBuff.Length - 1; i++)
        {
            sw.WriteLine(castle.isBuff[i]);
        }
        sw.Close();
        fs.Close();
        victoryObj.SetActive(true);
        gameObject.SetActive(false);
    }


}

