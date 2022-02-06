using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;


public class TowerSystem : MonoBehaviour
{


    [SerializeField] private GameObject isNotFullCostText;
    private Castle castle;
    // Start is called before the first frame update
    [SerializeField] private GameObject getChooiseTowerPrefab;//選塔的OBJ
    private Scene scene;
    [SerializeField] private Text ExpText;
    public static int Exp;
    [SerializeField] private GameObject putTower;
    public GameObject towerData; //塔的資訊
    public static GameObject targetTower;
    [Header("放置TargetPos")]
    public GameObject tile;
    public int _TowerMax;//塔的數量
    //public MapMaker _Mapmaker;//畫圖格子C#
    public string TowerNamePrefabs;//放置塔的名字
    //cost
    public float _time, _retime, _Moneytime;//每回合開始的時間,重置時間,金錢每N秒得到時間
    public Text costText;//我方能量條
    public float totalcost;
    public float maxCost;
    public float healCost;
    public float startCost;
    private float needcost;
    public float timeCost;
    //perfabTower 
  

    [SerializeField] private Image costEffect;

    enum Towerstatus
    {
        Null=0,
        InstantiateTower=1,
        gotTower

    }
    Towerstatus towerstatus;

    public  enum RoundStatusEnum
    { 
        Null = 0,
        PlaceTower,
        RoundStart,
        RoundEnd,

       
    }
    /// <summary>
    /// 回合狀態 0:空 1:放置塔 2:回合開始 3:回合結束
    /// Null , PlaceTower , RoundStart , RoundEnd
    /// </summary>
    public static RoundStatusEnum _RoundStatus;



    void Start()
    {
        scene = SceneManager.GetActiveScene();
        castle = GameObject.Find("Castle").GetComponent<Castle>();
        TowerNamePrefabs = "Null";
        _RoundStatus = RoundStatusEnum.Null;
        _TowerMax = 0;
        if (castle.isBuff[10] == true) totalcost += startCost;

        if(scene.name == "TowerDefenseGames")
            StartCoroutine(delay() );
        _Moneytime = 1;
    }

    // Update is called once per frame

    void Update()
    {
        getMoneyTimer(timeCost, healCost);
        
        if (Input.GetMouseButtonDown(0))//左鍵
        {
            getTowerData();
            getTowerNamePrefabs();
            getNewTowerPosition();
        }
        costText.text = totalcost + " / " + maxCost;
        costEffect.fillAmount = totalcost / maxCost;
        ExpText.text = Exp.ToString();
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(2);
        {
            getChooiseTowerPrefab.SetActive(true) ;
        }
    }

    ///<summary>
    ///初始化地圖
    /// </summary>
    /*
    public void InitMap()
    {
        for (int x = 0; x < _Mapmaker.getMapSize(0); x++)
        {
            for (int y = 0; y < _Mapmaker.getMapSize(1); y++)
            {
                //生成格子对象
                GameObject itemGo = Instantiate(HitMarkPosition, transform.position, transform.rotation);
                //设置属性 

                if (_isCheckObj[x,y] == true)
                {
                    itemGo.transform.position = _Mapmaker.CorretPositon(x * _Mapmaker.getMapSize(2), y * _Mapmaker.getMapSize(3) + _Mapmaker.getMapSize(4));
                    itemGo.transform.SetParent(transform);
                }
                
                _MarkPoslist[x,y] = itemGo;
            }
        }
        Debug.Log(_MarkPoslist.Length);
    }
    */


    /// <summary>
    /// 存放建置塔的obj,obj= TowerNamePrefabs
    /// </summary>
    public void getTowerNamePrefabs()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        /*
         * 下面為物件導引，來確認自己點到什麼
        RAY射線抓到物件位置，物件需要有collider 2D   
        Physics2D.Raycast(起點, 方向, 移動的最大距離, LAYER 圖層); 
        Layer 9 如果是在第9層 就是2^9=512 會被找到。
        或者用指定名字的方式如下
        targetMask = LayerMask.GetMask("target");
        */
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 10, LayerMask.GetMask("PreparePlace"));
        if (hit.collider)
        {
            Debug.Log(hit.collider.name);
            TowerNamePrefabs = hit.transform.name;
            Debug.Log("Spell = " + hit.collider.GetComponent<PrefabTower>().getCost());
            //確認能量<現有能量
            if (totalcost >= hit.collider.GetComponent<PrefabTower>().getCost())
            {
                needcost = hit.collider.GetComponent<PrefabTower>().getCost();
                switch (hit.transform.name)
                {
                    case "BaseWater":
                        TowerNamePrefabs = "BaseWater";
                        break;
                    case "WaterSoldier":
                        TowerNamePrefabs = "WaterSoldier";
                        break;
                    case "FireWall":
                        TowerNamePrefabs = "FireWall";
                        break;
                    case "BaseFire":
                        TowerNamePrefabs = "BaseFire";
                        break;
                    case "EarthLV1":
                        TowerNamePrefabs = "EarthLV1";
                        break;
                    case "elemental_1":
                        TowerNamePrefabs = "elemental_1";
                        break;
                    case "FireLV1":
                        TowerNamePrefabs = "FireLV1";
                        break;
                    case "monster_dknight2_0":
                        TowerNamePrefabs = "monster_dknight2_0";
                        break;
                    case "woodLv2":
                        TowerNamePrefabs = "woodLv2";
                        break;
                    case "WoodLv3":
                        TowerNamePrefabs = "WoodLv3";
                        break;
                    case "woodLv4":
                        TowerNamePrefabs = "woodLv4";
                        break;
                    case "WoodWall":
                        TowerNamePrefabs = "WoodWall";
                        break;
                    case "fireLV2":
                        TowerNamePrefabs = "fireLV2";
                        break;
                    case "WoodLv1":
                        TowerNamePrefabs = "WoodLv1";
                        break;
                    case "GoldLv3":
                        TowerNamePrefabs = "GoldLv3";
                        break;
                    case "GoldLV1":
                        TowerNamePrefabs = "GoldLV1";
                        break;
                    case "FireLv0":
                        TowerNamePrefabs = "FireLv0";
                        break;
                    case "TrapPos":
                        TowerNamePrefabs = "TrapPos";
                        break;
                    case "TrapFire":
                        TowerNamePrefabs = "TrapFire";
                        break;
                    case "TrapIce":
                        TowerNamePrefabs = "TrapIce";
                        break;
                    case "TrapEarth":
                        TowerNamePrefabs = "TrapEarth";
                        break;
                    case "StoneWall":
                        TowerNamePrefabs = "StoneWall";
                        break;





                    default:
                        Console.WriteLine("Default case");
                        break;
                }
                tile.active = true;
            }
            else
            {
                TowerNamePrefabs = "Null";
                isNotFullCostText.SetActive(true);

            } 
                
                
        }
    }
    
    /// <summary>
    /// 得到生成塔的位置,並生成塔
    /// </summary>
    /// <returns></returns>
    public void getNewTowerPosition()
    {

        //參考getTowerNamePrefabs()
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 10, LayerMask.GetMask("TargetPos"));
        Vector2 NewPosTower;
        if (hit.collider)
        {

            NewPosTower = new Vector2(hit.transform.position.x, hit.transform.position.y);
            hit.collider.GetComponent<TargetPos>().isObj = false;
            Collider2D col = Physics2D.OverlapCircle(NewPosTower, hit.collider.GetComponent<SpriteRenderer>().bounds.extents.x, LayerMask.GetMask("Fight") );
            if(col == null)
            {
                if (TowerNamePrefabs != "Null")
                {
                    totalcost -= needcost;
                    costEffect.fillAmount = totalcost / maxCost;
                    InstantiateTower(NewPosTower);
                }
            }
            
                
            tile.active = false;
            /*
            for(int x=0;x< (int)_Mapmaker.getMapSize(0);x++)
            {
                for(int y=0;y< (int)_Mapmaker.getMapSize(1);y++)
                {
                    _MarkPoslist[x, y].transform.position = new Vector2(-10, 5);
                }
            }
            */

        }
        
       
    }

    /// <summary>
    /// 生成塔 
    /// </summary>
    public void InstantiateTower(Vector2 pos)
    {
        var obj = Resources.Load("Prefabs/TD/Tower/" + TowerNamePrefabs);
        GameObject _GameObj;
        if (obj != null)
        {
            _GameObj = GameObject.Instantiate(obj, new Vector3(pos.x, pos.y, 10), new Quaternion(0, 0, 0, 0)) as GameObject;
            _GameObj.transform.parent = putTower.transform;

        }

    }

    public void getTowerData()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 10, LayerMask.GetMask("Fight"));

        if(hit.collider)
        {
            towerstatus = Towerstatus.gotTower;
            if (hit.collider.tag == "Tower" && towerstatus == Towerstatus.gotTower)
            {
                targetTower = hit.collider.gameObject;
                towerData.SetActive(true);

                towerData.GetComponent<TowerDatas>().getTowertarget(targetTower);
                
            }
        }
    }

    ///<summary>
    ///按鈕 回合開始
    ///</summary>
    public void isTimeStart()
    {
        //RoundStatus = 2;
        _RoundStatus = RoundStatusEnum.RoundStart;
        
    }

    ///<summary>
    ///計時器
    ///</summary>
    public void Timeup(float _time)
    {
        _retime = _retime - Time.deltaTime;      //把面板輸入的時間，“1”，減去0.0000X，直至為0，也就是1秒過去了，例項化複製這個子彈,1秒複製1個。
        if (_retime <= 0)
        {
            _retime = _time;     //重複賦值，重複執行
        }
    }

    ///<summary>
    ///每time秒 得到money元
    ///</summary>
    public void getMoneyTimer(float _time,float Money)
    {
        _Moneytime = _Moneytime - Time.deltaTime;
        if(totalcost >= maxCost)
        {
            totalcost = maxCost;
        }
        if (_Moneytime <= 0 && totalcost<=maxCost)
        {
            //costEffect.fillAmount = totalcost / maxCost;
            totalcost += Money;               //得到的金錢
            _Moneytime = _time;     //設定時間
        }

    }

    /// <summary>
    /// 回合結束，目前無內容，需要可補
    /// </summary>
    public void RoundStatusEnd()
    {
        _RoundStatus = RoundStatusEnum.Null;
        Debug.Log("End than = "+_RoundStatus);
    }

    public void CostHealUp()
    {
        healCost += 2;
    }

    public void CostMaxUp()
    {
        maxCost *= 1.3f;
    }

   


}
