using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
public class UIbottom : MonoBehaviour
{
    // Start is called before the first frame update


    
    public static int resolutions;

    public AudioSource bgMusicAudioSource;
    public Text Position;
    public GameObject CheckExit;
    public GameObject UpdateText;
    public GameObject Opensystem;
    public GameObject Developeredition;
    public Text _DeveloperEditionText;
    public Text _UpdateText;
    
    private bool isFirstmanu=true;
    bool CheckMute = false;

    [SerializeField]
    SystemData System_Data;

    [System.Serializable]
    public class SystemData
    {
        public int Resolution = 0;
        public string strvar = "aa";
    }


    void Start()
    {
        //System_Data.Resolution = SystemSet.resolutions;
        GameObject targetObj = new GameObject();
        targetObj = GameObject.Find("Dropdown");
        if (targetObj && targetObj.GetComponent<Dropdown>())
        {
            targetObj.GetComponent<Dropdown>().onValueChanged.AddListener(ConsoleResult);
        }
    
        
    


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("S");
            Debug.Log(resolutions);
        }

    }

    public void SystemSave() 
    {
        Debug.Log("儲存成功");
        FileStream fs = new FileStream(Application.dataPath + "/SaveSystem.txt", FileMode.Create);
        StreamWriter sw = new StreamWriter(fs);
        sw.WriteLine("1");
        sw.WriteLine(resolutions);
        
        System_Data.Resolution = resolutions;
        Debug.Log(resolutions);
        sw.Close();
        fs.Close();
        //PlayerPrefs.SetInt("ResolutionValue", System_Data.Resolution);

    }

    public void ClickNewGame()
    {
        FileStream fs = new FileStream(Application.dataPath + "/SaveData.txt", FileMode.Create);
        StreamWriter sw = new StreamWriter(fs);
        sw.WriteLine("");

        sw.Close();
        fs.Close();
        if (isFirstmanu == true)
        {
          
            SceneManager.LoadScene("TowerDefenseGames");
        }
        
    }

    public void OpenUpdate()
    {
        if (isFirstmanu == true)
            UpdateText.SetActive(true);
        isFirstmanu = false;
        _UpdateText.text = ReadText("/DateText.txt");
    }

    public void OpenSystemSet()
    {
        if (isFirstmanu == true)
            Opensystem.SetActive(true);
        isFirstmanu = false;
    }

    public void OpenDeveloperEdition()
    {
        if (isFirstmanu == true)
            Developeredition.SetActive(true);
        isFirstmanu = false;
        _DeveloperEditionText.text = ReadText("/DeveloperEditionText.txt");
    }

    public void CancelDeveloperEdition()
    {
        Developeredition.SetActive(false);
        isFirstmanu = true;
    }

    public void CancelSystem()
    {
        Opensystem.SetActive(false);
        isFirstmanu = true;
    }

    public void CancelUpdate()
    {
        UpdateText.SetActive(false);
        isFirstmanu = true;
    }

    public void Clickexit()
    {
        Debug.Log("點離開"+ isFirstmanu);
        //CheckExit.SetActive(false);
        Application.Quit();
    }

    public void Cancelexit()
    {
        CheckExit.SetActive(false);
        isFirstmanu = true;
    }

    public void Checkexit()
    {
        Debug.Log("點離開");
        if (isFirstmanu == true)
            CheckExit.SetActive(true);
        isFirstmanu = false;
    }

    public void OnEnable()
    {
        bgMusicAudioSource = GameObject.FindGameObjectWithTag("BackGroundMusic").GetComponent<AudioSource>();
        
        if(CheckMute)
        {
            bgMusicAudioSource.Pause();
            CheckMute = !CheckMute;
        }
        else 
        {
            bgMusicAudioSource.UnPause();
            CheckMute = !CheckMute;
        }
        

    }

    public void OnReading()
    {
        FileStream fs = new FileStream(Application.dataPath + "/SaveData.txt", FileMode.Open);
        StreamReader sr = new StreamReader(fs);
        var text = "";
        for (int i = 0; i <= 21; i++)
        {
            text = sr.ReadLine(); 
        }
        text = sr.ReadLine();
        Debug.Log("text = " + text);
        fs.Close();
        sr.Close();
        if(text == "森林")
            SceneManager.LoadScene("森林");
        else if(text == "地下城")
            SceneManager.LoadScene("地下城");
        else
        SceneManager.LoadScene("TowerDefenseGames");
    }

    public void ConsoleResult(int value)
    {
        //这里用 if else if也可，看自己喜欢
        //分别对应：第一项、第二项....以此类推
        resolutions = value;

        switch (value)
        {
            case 0:
                Debug.Log(value);
                print("第1页");
                break;
            case 1:
                print("第2页");
                break;
            case 2:
                print("第3页");
                break;
            case 3:
                print("第4页");
                break;
            //如果只设置的了4项，而代码中有第五个，是永远调用不到的
            //需要对应在 Dropdown组件中的 Options属性 中增加选择项即可
            case 4:
                print("第5页");
                break;
            case 5:
                print("第5页");
                break;
            case 6:
                print("第5页");
                break;
            case 7:
                print("第5页");
                break;
            case 8:
                print("第5页");
                break;
            case 9:
                print("第5页");
                break;
        }
        Debug.Log(resolutions+" "+ value);
    }

    public string ReadText(string _name)
    {
        FileStream fs = new FileStream(Application.dataPath + _name, FileMode.Open);
        StreamReader sr = new StreamReader(fs);
        string line = sr.ReadToEnd();
        Debug.Log(line);
        fs.Close();
        sr.Close();
        return line;
    }




}