using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class MapMaker : MonoBehaviour
{   //參考網頁
    //https://blog.csdn.net/zhanxxiao/article/details/104904097/ 
    //https://blog.csdn.net/qq_41251708/article/details/99362287
    // Start is called before the first frame update
    /// <summary>
    /// 线的材质
    /// </summary>
    public Material lineMat;
    
    /// <summary>
    /// 线的颜色
    /// </summary>
    public Color lineColor;

    //开关属性、画线开关
    public bool drawLine;
    //地图宽
    private float mapWidth;
    //高
    private float mapHeight;
    //自定義高
    private float _Newmapheight;
    private float _NewmapWidth;
    //格子宽
    private float gridWidth;
    //格子高
    private float gridHeight;
    //行数
    public const int yRow = 7;
    //列数
    public const int xColumn = 9;
    //塔的添加点
    public GameObject gridGo;

    public float[] _mapSize;
    

    //在游戏中不是单例、在工具中才是单例
    private static MapMaker _instance;
    public static MapMaker Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
        //InitMap();
    }

    public void Start()
    {
        _mapSize = new float[5];


    }


    //我:用來顯示可以放塔的地方
    //初始化地图
    public void InitMap()
    {
        CalculateSize();
        /*
        //生成格子
        for (int x = 0; x < xColumn; x++)
        {
            for (int y = 0; y < yRow; y++)
            {
                //未來補IF有塔則不顯示
                //生成格子对象
                GameObject itemGo = Instantiate(gridGo, transform.position, transform.rotation);
                //设置属性 
                itemGo.transform.position = CorretPositon(x * gridWidth, y * gridHeight+ _Newmapheight);
                itemGo.transform.SetParent(transform);
            }
        }
        */
    }
   
    public float getMapSize(int i)
    {
        CalculateSize();
        _mapSize = new float[5];
        _mapSize[0] = xColumn;
        _mapSize[1] = yRow;
        _mapSize[2] = gridWidth;
        _mapSize[3] = gridHeight;
        _mapSize[4] = _Newmapheight;


        return _mapSize[i];
    }

    //纠正预制件的起始位置
    public Vector3 CorretPositon(float x, float y)
    {
        return new Vector3(x - mapWidth / 2 + gridWidth / 2, y - mapHeight / 2 + gridHeight / 2);
    }


    //计算地图格子的宽高
    private void CalculateSize()
    {
        //左下角
        Vector3 leftDown = new Vector3(0, 0);
        //右上角
        Vector3 rightUp = new Vector3(1, 1);
        //自訂點
        Vector3 _newPos = new Vector3(0.1f, 0.15f);
        //视口坐标转世界坐标 、 左下角的世界坐标
        Vector3 posOne = Camera.main.ViewportToWorldPoint(leftDown);
        
        //右上角
        Vector3 posTwo = Camera.main.ViewportToWorldPoint(rightUp);
        
        Vector3 posThree = Camera.main.ViewportToWorldPoint(_newPos);
        //Debug.Log("posOne = " + posOne.ToString() + "右上角 =  " + posTwo.ToString());
        //地图宽
        mapWidth = posTwo.x - posOne.x;
        //Debug.Log("地图的宽度" + mapWidth );
        //地图高
        mapHeight = posTwo.y - posOne.y;
        _NewmapWidth = posThree.x - posOne.x;
        _Newmapheight = posThree.y- posOne.y;
        //格子的宽
        gridWidth = mapWidth / xColumn;
        //格子高
        gridHeight = (posTwo.y- posThree.y) / yRow;
    }

    void DrawLine(Vector2 posA, Vector2 posB, Color color)
    {

        GL.Begin(GL.LINES);
        lineMat.SetPass(0);
        GL.Color(color);
        GL.Vertex3(posA.x, posA.y, 0);
        GL.Vertex3(posB.x, posB.y, 0);
        GL.End();
    }
    void OnPostRender()
    {
        //Debug.Log("开始画格子");
        //画线
        
        CalculateSize();
        for (int y = 0; y <= yRow; y++)
        {

            //起始位置
            Vector3 startPos = new Vector3((-mapWidth / 2), (-mapHeight) / 2 + _Newmapheight + y * gridHeight);
            //终点坐标
            Vector3 endPos = new Vector3(mapWidth / 2, -mapHeight / 2 + _Newmapheight + y * gridHeight);
            //画线
            DrawLine(startPos, endPos, lineColor);
            //Gizmos.DrawLine(startPos, endPos);
            //Gizmos.DrawSphere(new Vector3(startPos.x, startPos.y, 0f), 1);
        }
        //画列 上下
        for (int x = 0; x <= xColumn; x++)
        {//
            Vector3 startPos = new Vector3(-mapWidth / 2 + gridWidth * x, (mapHeight) / 2);
            Vector3 endPos = new Vector3(-mapWidth / 2 + x * gridWidth, (-mapHeight) / 2 + _Newmapheight);
            DrawLine(startPos, endPos, lineColor);
            //Gizmos.DrawLine(startPos, endPos);
            //Gizmos.DrawSphere(new Vector3(startPos.x, startPos.y, 0f), 1);
        }

    }


    //画格子 
    /*
    private void OnDrawGizmos()
    {
        //Debug.Log("开始画格子");
        //画线
        if (drawLine)
        {
            //计算格子的大小
            CalculateSize();
            //格子的颜色
            Gizmos.color = Color.green;
            //画出行数   这里的值应该是要等于行数的 左右
            for (int y = 0; y <= yRow; y++)
            {
                
                //起始位置
                Vector3 startPos = new Vector3((-mapWidth / 2), (-mapHeight) / 2+ _Newmapheight + y * gridHeight);
                //终点坐标
                Vector3 endPos = new Vector3(mapWidth / 2, -mapHeight / 2+ _Newmapheight + y * gridHeight);
                //画线
                Gizmos.DrawLine(startPos, endPos);
                //Gizmos.DrawSphere(new Vector3(startPos.x, startPos.y, 0f), 1);
            }
            //画列 上下
            for (int x = 0; x <= xColumn; x++)
            {//
                Vector3 startPos = new Vector3(-mapWidth / 2 + gridWidth * x, (mapHeight) / 2  );
                Vector3 endPos = new Vector3(-mapWidth / 2 + x * gridWidth, (-mapHeight) /2 + _Newmapheight);
                Gizmos.DrawLine(startPos, endPos);
                //Gizmos.DrawSphere(new Vector3(startPos.x, startPos.y, 0f), 1);
            }

        }
        else
        {
            return;
        }
    }

    */
}
