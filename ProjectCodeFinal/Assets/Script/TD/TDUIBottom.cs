using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class TDUIBottom : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private AudioSource backMusic;
    [SerializeField] private GameObject systemManu;
 
    private Castle castle;
    private Scene scene;
    void Start()
    {
        castle = GameObject.Find("Castle").GetComponent<Castle>();
        scene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            systemManu.SetActive(true);
        }
    }

    public void CanerSystemMeny()
    {
        systemManu.SetActive(false);
    }

    public void SceneToMainManu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void WinerBackMain()
    {
        FileStream fs = new FileStream(Application.dataPath + "/SaveData.txt", FileMode.Create);
        StreamWriter sw = new StreamWriter(fs);


        if (scene.name == "TowerDefenseGames")
        {
            Debug.Log("TowerDefenseGames");
            for (int i = 0; i <= castle.isLockPrefab.Length - 1; i++)
            {
                sw.WriteLine(castle.isLockPrefab[i]);
            }
            sw.WriteLine("地下城");
        }

        if (scene.name == "地下城")
        {
            Debug.Log("地下城");
            for (int i = 0; i <= castle.isLockPrefab.Length - 1; i++)
            {
                sw.WriteLine(castle.isLockPrefab[i]);
            }
            sw.WriteLine("森林");
        }


        if (scene.name == "森林")
        {
            Debug.Log("森林");
            for (int i = 0; i <= castle.isLockPrefab.Length - 1; i++)
            {
                sw.WriteLine("False");
            }

        }


        sw.Close();
        fs.Close();

        SceneToMainManu();
    }

    public void SetTdData()
    {
        FileStream fs = new FileStream(Application.dataPath + "/SaveData.txt", FileMode.Create);
        StreamWriter sw = new StreamWriter(fs);


        if (scene.name == "森林")
        {
            Debug.Log("森林");
            for (int i = 0; i <= castle.isLockPrefab.Length - 1; i++)
            {
                sw.WriteLine(castle.isLockPrefab[i]);
            }
            sw.WriteLine("森林");
        }

        if (scene.name == "地下城")
        {
            Debug.Log("地下城");
            for (int i = 0; i <= castle.isLockPrefab.Length - 1; i++)
            {
                sw.WriteLine(castle.isLockPrefab[i]);
            }
            sw.WriteLine("地下城");
        }


        if (scene.name == "TowerDefenseGames")
        {
            Debug.Log("TowerDefenseGames");
            for (int i = 0; i <= castle.isLockPrefab.Length - 1; i++)
            {
                sw.WriteLine("False");
            }

        }

        
        sw.Close();
        fs.Close();

        SceneToMainManu();
    }

    public void NextScense()
    {
        if(scene.name == "地下城")
            SceneManager.LoadScene("森林");
        if(scene.name == "TowerDefenseGames")
            SceneManager.LoadScene("地下城");
        if (scene.name == "GoodEnd") ;
            SceneManager.LoadScene("GoodEnd");

    }

    



}
