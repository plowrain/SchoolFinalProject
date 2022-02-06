using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
public class EndText : MonoBehaviour
{

    private Text text;
    public Text DeathText;
    private float Alpha;
    public GameObject LeftText, rightBt;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        FileStream fs = new FileStream(Application.dataPath + "/BuffData.txt", FileMode.Open);
        StreamReader sr = new StreamReader(fs);
        var t = "";
        for (int i = 0; i <= 19; i++)
        {
            t = sr.ReadLine();
        }
        DeathText.text = "死亡次數 : "+sr.ReadLine();
        Debug.Log("DeathText = " +DeathText);

        sr.Close();
        fs.Close();
        text.color= new Color(255, 0, 0, 0);
        Alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Add());
    }
    IEnumerator Add()
    {
        for(int i=0;i<=10;i++)
        {
            yield return new WaitForSeconds(1f);
            text.color = new Color(255, 0, 0, Alpha / 255);
            Alpha += 1f;
        }
        LeftText.SetActive(true);
        rightBt.SetActive(true);
    }

    public void Exit()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
