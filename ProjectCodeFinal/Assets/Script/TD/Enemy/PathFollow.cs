using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollow : MonoBehaviour
{
    //參考 https://tedsieblog.wordpress.com/2016/07/10/path-following/
    //參考 https://blog.csdn.net/weixin_38778769/article/details/103540415
    public Path path;//The path

    public float speed = 5.0f;//following speed
   
    public bool isLooping = true;//the car will loop or not
    public int index = 0;                       //从初始位置触发
    

    //path=路徑， 尚未測試 想法架構，如果要做多重路徑 在Start的FIND換另外一個物件PATH名稱。
    void Start()
    {
        path = GameObject.Find("MonsterPath").GetComponent<Path>();

    }

    void Update()
    {
        if (transform.position != path.GetPosition(index) )
        {
            MoveToThePoints();
        }
        //到了数组指定index位置,改变index值，不断循环
        else
        {
            index = ++index % path.Length;
        }


    }
    void MoveToThePoints()
    {
        //从当前位置按照指定速度移到index位置，记得speed * Time.deltaTime，不然会瞬移
        Vector2 temp = Vector2.MoveTowards(transform.position, path.GetPosition(index), speed * Time.deltaTime);
        //考虑到可能有碰撞检测，所以使用刚体的移动方式
        GetComponent<Rigidbody2D>().MovePosition(temp);

    }




}
