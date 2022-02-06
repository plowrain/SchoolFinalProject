using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randLight : MonoBehaviour
{

    [SerializeField] private GameObject light;
    [SerializeField] private float time,retime;
    [SerializeField] private int times;
    [SerializeField] private int RandAtk;
    [SerializeField] private GameObject atkPrefab;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(randlight() );
        
    }

    IEnumerator randlight()
    {
        yield return new WaitForSeconds(0.1f);
        retime += Time.deltaTime;
        
        if(retime >= time)
        {
            RandAtk = Random.Range(0, 100);

            if (RandAtk <= 15)
            {
                

                times = Random.Range(0, 3);
                for (int i = 0; i <= times; i++)
                {
                    var _x = Random.Range(-13, 13);
                    var _y = Random.Range(4.5f, -9);
                    var _scaleY = Random.Range(-1, 3);
                    GameObject _a = Instantiate(light, new Vector2(_x, _y), transform.rotation);
                    _a.transform.localScale = new Vector3(_scaleY, _a.transform.localScale.y, 0);
                    Instantiate(atkPrefab, new Vector2(0, 0), transform.rotation);
                    Destroy(_a, 1);
                }
                
            
                
            }
            retime = 0;
        }
        
    }
}
