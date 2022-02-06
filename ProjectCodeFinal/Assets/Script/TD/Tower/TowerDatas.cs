using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerDatas : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isMousein;
    public GameObject towerImage;
    public Text maxHealth, health, atk, atkSpeed, def, fixDef, penetration, cost, elementa, level, needexp;
    public GameObject towerTarget;
      
    private void Start()
    {
        
        correctData();
    }

    private void OnEnable()
    {
        Start();
    
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isMousein == false)//左鍵
        {
            gameObject.SetActive(false);
        }
        
    }

    public void getTowertarget(GameObject _var)
    {
        towerTarget = _var;
        correctData();
    }

    public void correctData()
    {
        if(towerTarget != null)
        {
            /*
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
      [10] = needexp
  */
            towerImage.GetComponent<SpriteRenderer>().sprite = towerTarget.GetComponent<SpriteRenderer>().sprite;
            maxHealth.text = "最大血量 : "+towerTarget.GetComponent<Tower>().getability(0).ToString("0");
            health.text = "血量 : " +towerTarget.GetComponent<Tower>().getability(1).ToString("0");
            atk.text = "攻擊力 : " +towerTarget.GetComponent<Tower>().getability(2).ToString("0.0");
            atkSpeed.text = "攻擊頻率 : "+towerTarget.GetComponent<Tower>().getability(3).ToString("0.0");
            def.text = "防禦率 : "+towerTarget.GetComponent<Tower>().getability(4).ToString("0");
            fixDef.text = "固定減傷 : " +towerTarget.GetComponent<Tower>().getability(5).ToString("0");
            penetration.text = "穿透率 : "+towerTarget.GetComponent<Tower>().getability(6).ToString("0");
            cost.text = "魔能 : " +towerTarget.GetComponent<Tower>().getability(7).ToString();
            float ele = towerTarget.GetComponent<Tower>().getability(8);
            switch (ele)
            {
                case 0:
                    elementa.text = "屬性 : 無";
                    break;
                case 1:
                    elementa.text = "屬性 : 火";
                    break;
                case 2:
                    elementa.text = "屬性 : 木";
                    break;
                case 3:
                    elementa.text = "屬性 : 水";
                    break;
                case 4:
                    elementa.text = "屬性 : 金";
                    break;
                case 5:
                    elementa.text = "屬性 : 土";
                    break;
            }
            
            level.text = "等級 : " +towerTarget.GetComponent<Tower>().getability(9).ToString();
            needexp.text = "升等經驗 : " +towerTarget.GetComponent<Tower>().getability(10).ToString("0");
            
        }
    }

    public void levelUpBt()
    {
        //之後補圖案
        if(towerTarget.GetComponent<Tower>().getability(9) < 10 && TowerSystem.Exp >= towerTarget.GetComponent<Tower>().getability(10))
        {
            //TowerSystem.Exp -= (int)(towerTarget.GetComponent<Tower>().getability(10) );
            ISkill iskill = towerTarget.GetComponent<ISkill>();
            iskill.LevelUp();
            correctData();
        }
  
        
    }

    private void OnMouseEnter()
    {

        isMousein = true;
    }

    private void OnMouseExit()
    {

        isMousein = false;
    }

}
