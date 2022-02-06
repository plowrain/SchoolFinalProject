using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    //參考 https://tedsieblog.wordpress.com/2016/07/10/path-following/
    //參考 https://blog.csdn.net/weixin_38778769/article/details/103540415
    //這個腳本掛到空物件
    //Display the path
    public bool showPath = true;
    public Color pathColor = Color.red;

    //The path is loop or not
    public bool loop = false;

    //The waypoint radius
    public float Radius = 2.0f;

    //Waypoints array
    public Transform[] wayPoints;

    //The Reset fuction is one of Unity API.
    //MonoBehaviour.Reset()
    //http://docs.unity3d.com/ScriptReference/MonoBehaviour.Reset.html
    //This function is only called in editor mode.

        //對這個腳本右鍵用RESET 可以直接搜尋"點"做更改，"點"名稱要是WayPoint_數字,tag = WayPoint
    void Reset()
    {

        //Reset the wayPoint array
        wayPoints = new Transform[gameObject.transform.childCount];
        //wayPoints.Length
        for (int cnt = 0; cnt < gameObject.transform.childCount; cnt++)
        {

            //wayPoints[cnt] = GameObject.Find("WayPoint_" + (cnt + 1).ToString()).transform;
            wayPoints[cnt] = gameObject.transform.GetChild(cnt).transform;
        }
        Debug.Log("wayPoints.Length is = "+wayPoints.Length);
    }

    //Get the length of wayPoint array
    public int Length
    {

        get
        {

            return wayPoints.Length;

        }

    }

    //Get the position in the array with its index number
    public Vector3 GetPosition(int index)
    {

        return wayPoints[index].position;
        
    }

    /*
     * 劃出虛擬路線
    //The OnDrawGizmos function is one of Unity API
    //MonoBehaviour.OnDrawGizmos()
    //http://docs.unity3d.com/ScriptReference/MonoBehaviour.OnDrawGizmos.html
    //This function will display gizmo in Scene and will not display in Game
    void OnDrawGizmos()
    {

        //If showPath is false, return
        if (!showPath) return;

        //Draw the path line
        for (int i = 0; i < wayPoints.Length; i++)
        {

            if (i + 1 < wayPoints.Length)
            {

                Debug.DrawLine(wayPoints[i].position, wayPoints[i + 1].position, pathColor);

            }
            else
            {

                if (loop)
                {

                    Debug.DrawLine(wayPoints[i].position, wayPoints[0].position, pathColor);

                }

            }

        }

    }

    */

}
