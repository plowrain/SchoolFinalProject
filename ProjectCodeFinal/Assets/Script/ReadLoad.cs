using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
//using System.Runtime.Serialization.Formatters.Binary;

public class ReadLoad : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 PlayerPosition;
    public Text Position;
    [SerializeField]
    PlayerData Data;

    


    [System.Serializable]
    public class PlayerData
    {
        public string name="Plow";
        public string P_Position="0,0,0";
        public int level=1;

    }

    void Start()
    {

        Position.text = "0,0,0";
        //Vector3 PlayerPosition = GameObject.Find("Player").GetComponent<Transform>().localPosition;

    }

    // Update is called once per frame
    void Update()
    {
        //Position.text = ""+PlayerPosition;

        if(Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log(Data.name+"123");
            Position.text = "儲存成功";

            FileStream fs = new FileStream(Application.dataPath + "/Save.txt", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            PlayerPosition = GameObject.Find("Player").GetComponent<Transform>().localPosition;
            Data.P_Position = ""+GameObject.Find("Player").GetComponent<Transform>().localPosition;
            sw.WriteLine(Data.name);
            sw.WriteLine(Data.level);
            sw.WriteLine(Data.P_Position);
            sw.Close();
            fs.Close();
            PlayerPrefs.SetString("name", Data.name);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            FileStream fs = new FileStream(Application.dataPath + "/Save.txt", FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            Data.name = sr.ReadLine();
            Data.level = int.Parse(sr.ReadLine());
            Data.P_Position = sr.ReadLine();
            PlayerPosition = new Vector3(0, 0, 0);
            PlayerPrefs.GetString("name", Data.name);
        }

    }
}
