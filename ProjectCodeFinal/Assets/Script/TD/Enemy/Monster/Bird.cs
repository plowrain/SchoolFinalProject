using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : Enemy
{
    
    // Start is called before the first frame update
    protected override void Start()
    {
        //setAbility(_maxHealth:75 ,_atk: 7 ,_atkSpeed: 1.5f ,_def: 5 ,_speed: 3f ,_penetration: 0,_exp: 3,_fixDef: 0,_ele: 0);
        base.Start();
       
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}

