using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class shine : MonoBehaviour
{

    private float alpha = 0.2f;
    private float alphaSpeed = 8f;
    private bool isShow = true;//用来控制闪烁的内容
    private CanvasGroup cg;
    private bool isClick = false;//用来控制 彻底不需要走动画了
    private Image shanImg;
    public Sprite shan_sprite;  //外部传进来的
    void Start()
    {
        cg = this.transform.GetComponent<CanvasGroup>();
        shanImg = this.transform.GetComponent<Image>();
    }

    void Update()
    {
        if (isClick)
        {
            if (isShow)
            {
                if (alpha != cg.alpha)
                {
                    cg.alpha = Mathf.Lerp(cg.alpha, alpha, alphaSpeed * Time.deltaTime);  //这个方法表示的是一种简便过程 传入初始和想达到的
                    if (Mathf.Abs(alpha - cg.alpha) <= 0.01)
                    {
                        //Debug.Log("更新2===" + alpha + "===" + cg.alpha);
                        cg.alpha = alpha; isShow = false;
                    }
                }
            }
            else
            {
                if (1 != cg.alpha)
                {
                    cg.alpha = Mathf.Lerp(cg.alpha, 1, alphaSpeed * Time.deltaTime);  //这个方法表示的是一种简便过程 传入初始和想达到的
                    if (Mathf.Abs(1 - cg.alpha) <= 0.01)
                    {
                        //Debug.Log("更新3===" + alpha + "===" + cg.alpha);
                        cg.alpha = 1; isShow = true;
                    }
                }
            }
        }
    }

    public void ShowAnim()
    {
        //alpha = 1;
        isClick = true;
        shanImg.sprite = shan_sprite;
        //cg.blocksRaycasts = true;//可以和该UI对象交互
    }

    public void HideAnim()
    {
        isClick = false;
        //alpha = 0;

        //cg.blocksRaycasts = false;//不可以和该UI对象交互
    }
}