using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Loading : MonoBehaviour
{
    // Start is called before the first frame update
    private int isFirst=0;
    private int Resolutions;
    void Start()
    {
        
        
        FileStream fs = new FileStream(Application.dataPath + "/SaveSystem.txt", FileMode.Open);
        StreamReader sr = new StreamReader(fs);
        isFirst = int.Parse(sr.ReadLine());
        Resolutions = int.Parse(sr.ReadLine());
        sr.Close();
        fs.Close();
        if(isFirst == 0)
        {
            Resolution[] resolutions = Screen.resolutions;
            foreach (Resolution res in resolutions)
            {
                print(res.width + "x" + res.height);
            }
            Screen.SetResolution(resolutions[0].width, resolutions[0].height, true);
        }
        if(isFirst == 1)
        {
            switch(Resolutions)
            {
                case 0:
                    Screen.SetResolution(1920, 1080, true);
                    break;
                case 1:
                    Screen.SetResolution(1920, 768, true);
                    break;
                case 2:
                    Screen.SetResolution(1600, 1024, true);
                    break;
                case 3:
                    Screen.SetResolution(1600, 900, true);
                    break;
                case 4:
                    Screen.SetResolution(1440, 900, true);
                    break;
                case 5:
                    Screen.SetResolution(1360, 768, true);
                    break;
                case 6:
                    Screen.SetResolution(1280, 960, true);
                    break;
                case 7:
                    Screen.SetResolution(1280, 800, true);
                    break;
                case 8:
                    Screen.SetResolution(1024, 768, true);
                    break;
                case 9:
                    Screen.SetResolution(800, 600, true);
                    break;
            }

        }
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
