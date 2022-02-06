using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseWaterbullet : MonoBehaviour
{
    //子但追蹤 https://oblivious9.pixnet.net/blog/post/201395104-unity-%E8%AE%93%E7%89%A9%E4%BB%B6%E6%9C%9D%E8%91%97%E6%8C%87%E5%AE%9A%E8%B7%AF%E5%BE%91%E7%AD%89%E9%80%9F%E7%A7%BB%E5%8B%95%28%E5%8F%AF%E7%94%A8%E6%96%BC3d%E6%A8%A1
    // Start is called before the first frame update
    [SerializeField]public float atkDamage=10f;
    public float pen;//穿透
    public float fixDam;//固定傷害
    [SerializeField]public float speed;
    public string ele;
    public int _Team =1;
    public Vector2 tagetPos;
    public GameObject taget;
    private float firstSpeed;

    public GameObject FaGameObj;
    //public Text text_hp;

 
    void OnTriggerEnter2D(Collider2D other)
    {

        
        if (other.tag == "Enemy")
        {
            IDamageable iDamageable = other.GetComponent<IDamageable>();
            if (iDamageable != null && other.name == taget.name)
            {
                iDamageable.TakenDamage(atkDamage, pen, ele, 0);
            }
            
            Destroy(gameObject); //摧毀物件
            
        }
        if (other.tag == "Wall")
        {
            Destroy(gameObject);
        }
        //Destroy(collision.gameObject);
    }

    void Start()
    {
        FaGameObj = gameObject.transform.parent.gameObject;
        atkDamage = FaGameObj.GetComponent<Tower>().getability(2);
        pen = FaGameObj.GetComponent<Tower>().getability(6);
        ele = FaGameObj.GetComponent<Tower>().getability(8).ToString();
        speed = 0.05f;
        firstSpeed = Vector2.Distance(transform.position, taget.transform.position) * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (taget != null)
        {
          

            
            //transform.position = new Vector2(taget.transform.position.x * speed * Time.deltaTime, taget.transform.position.y * speed * Time.deltaTime);
            //transform.Translate(new Vector3(taget.transform.position.x * speed * Time.deltaTime, taget.transform.position.y * speed * Time.deltaTime,0f));
            transform.position = Vector2.Lerp(transform.position, taget.transform.position, speed);
            speed= calculateNewSpeed();
            
        }    
            
        if (transform.position.y > 10 || transform.position.y < -10)
        {
            //如果物件的Y值大於20或小於20 就將物件移除
            Destroy(gameObject);
        }
        if (taget == null) Destroy(gameObject);

    }



    private float calculateNewSpeed()
    {
        //因為每次移動都是 Obj 在移動，所以要取得 Obj 和 PathB 的距離
        float tmp = Vector2.Distance(transform.position, taget.transform.position);

        //當距離為0的時候，就代表已經移動到目的地了。
        if (tmp == 0)
            return tmp;
        else
            return (firstSpeed / tmp);
    }
}
