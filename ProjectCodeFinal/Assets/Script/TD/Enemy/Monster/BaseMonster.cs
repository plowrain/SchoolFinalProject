using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMonster : Enemy
{
    //扣血寫在主動方，被攻擊為被動方


    protected override void Start()
    {
        //setAbility(100,3,2,5,_speed: 3f,0,5,1,0);
        base.Start();
       
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    /// <summary>
    /// 怪物受到傷害寫在這裡，需傳入傷害跟屬性。
    /// </summary>
   
}
