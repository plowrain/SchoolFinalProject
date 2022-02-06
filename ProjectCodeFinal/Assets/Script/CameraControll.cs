using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CameraControll : MonoBehaviour
{


    //控制视野缩放的速率
    public float view_value;
    //控制摄像机移动的速率
    public float move_speed;

    public int speed = 3;

    //关于鼠标滑轮的参数
    public float MouseWheelSensitivity = 0.0001f;
    public float MouseZoomMin = -2.4f;
    public float MouseZoomMax = 1.0f;
    public float normalDistance = -1.1f;

    //水平和垂直的移动速度
    public float horizontalMoveSpeed;
    public float verticalMoveSpeed;

    void Start()
    {
        move_speed = 1;
        horizontalMoveSpeed = 3f;
        verticalMoveSpeed = 3f;
    }

    void Update()
    {

        var x = 0.0f;
        var y = 0.0f;
        //放大、缩小
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            this.gameObject.transform.Translate(new Vector3(0, 0, Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * view_value));
        }
        //移动视角
        if (Input.GetMouseButton(0))
        {
            transform.Translate(Vector3.left * Input.GetAxis("Mouse X") * move_speed);
            transform.Translate(Vector3.up * Input.GetAxis("Mouse Y") * -move_speed);
        }

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            this.gameObject.transform.Translate(new Vector3(0, 0, Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * 500));
        }
        //获取cursor坐标
        var msPos = Input.mousePosition;

        //边界最小值
        var widthBorder = Screen.width / 50;
        var heightBorder = Screen.height / 50;



        if (widthBorder <= msPos.x && msPos.x <= Screen.width - widthBorder &&
            heightBorder <= msPos.y && msPos.y <= Screen.height - heightBorder)
        {
            transform.Translate(x, y, 0);
            //Debug.Log("asd" + msPos.x + " " + msPos.y);
        }
        else
        {

            if (msPos.y > Screen.height - heightBorder)
                y = verticalMoveSpeed;
            if (msPos.x < widthBorder)
                x = -horizontalMoveSpeed;
            if (msPos.y < heightBorder)
                y = -verticalMoveSpeed;
            if (msPos.x > Screen.width - widthBorder)
                x = verticalMoveSpeed;



            x *= speed * Time.deltaTime;
            y *= speed * Time.deltaTime;

        
            transform.Translate(x, y,0);
        }

    }
}
