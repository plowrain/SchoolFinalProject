using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChoseTower : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Image leftImage;
    [SerializeField] private Image rightImage;
    private int leftNum;
    private int rightNum;
    public Castle castle;
    private int maxVar;
    public int stop = 0;
    [SerializeField] public int first;
    private void OnEnable()
    {

        Start();



    }
    void Start()
    {
        //towerSystem = GameObject.Find("Script").GetComponent<TowerSystem>();
        maxVar = castle.isLockPrefab.Length;

        if (first == 2)
            RandNum();
        if (first == 0)
        {
            leftNum = 0;
            rightNum = 9;



            leftImage.sprite = castle.prefabTower[leftNum].GetComponent<SpriteRenderer>().sprite;
            rightImage.sprite = castle.prefabTower[rightNum].GetComponent<SpriteRenderer>().sprite;
            first = 1;
        }


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void chooseBase()
    {

    }

    public void RandNum()
    {


        while (castle.isLockPrefab[leftNum] == true)
        {
            leftNum = Random.Range(0, maxVar - 1);

        }

        while (leftNum == rightNum || castle.isLockPrefab[rightNum] == true)
        {
            rightNum = Random.Range(0, maxVar - 1);

        }

        leftImage.sprite = castle.prefabTower[leftNum].GetComponent<SpriteRenderer>().sprite;
        rightImage.sprite = castle.prefabTower[rightNum].GetComponent<SpriteRenderer>().sprite;
    }
    public void StartNum(int var0, int var1)
    {



        leftNum = var0;
        rightNum = var1;
        leftImage.sprite = castle.prefabTower[leftNum].GetComponent<SpriteRenderer>().sprite;
        rightImage.sprite = castle.prefabTower[rightNum].GetComponent<SpriteRenderer>().sprite;
        
        if (first == 2) gameObject.SetActive(false);
    }


    public void LeftBt()
    {
        castle.isLockPrefab[leftNum] = true;
        castle.AddTower(leftNum);
 
        if (first == 2)
        {
            gameObject.SetActive(false);
            
        }
        if( first ==1)
        {
            StartNum(7, 11);
            first = 2;
        }
            


    }

    public void RightBt()
    {
        castle.isLockPrefab[rightNum] = true;
        castle.AddTower(rightNum);

        if (first == 2)
        {
            gameObject.SetActive(false);
            
        }
        if (first == 1)
        {
            StartNum(7, 11);
            first = 2;
        }
    }
}
