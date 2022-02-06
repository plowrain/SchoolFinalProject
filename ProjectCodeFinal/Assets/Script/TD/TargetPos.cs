using UnityEngine;
using UnityEngine.SceneManagement;

public class TargetPos : MonoBehaviour
{
    public SpriteRenderer sprite;
    // Start is called before the first frame update
    public bool isObj;
    public LayerMask Fight;
    private Scene scene;
    [SerializeField] private Color color;

    private void Awake()
    {
        Start();

        //sprite.color = new Color(0, 253, 0, 0.25f);
    }
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        scene = SceneManager.GetActiveScene();

        //isObj = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if(isObj == true) checkObj();
        //if(isObj == false) sprite.color = new Color(253, 0, 0, 0); 
        checkObj();

    }

    private void OnMouseEnter()
    {

        //isObj = true;
        
    }




    public void checkObj()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, sprite.bounds.extents.x, Fight);

        if (collider != null)
            sprite.color = new Color(253, 0, 0, 0.5f);
        else
        {
            if(scene.name == "森林")
            {
                sprite.color = color;

            }        
            else if(scene.name == "TowerDefenseGames") sprite.color = new Color(0, 253, 0, 0.25f);
        }
        

    }
}
