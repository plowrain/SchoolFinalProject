using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Tower : Liver
{ 
   
    protected enum Type
    {
        Tower = 0,
        Wall,
        another
    }
    [SerializeField] protected Type type;

    protected override void Start()
    {
        base.Start();
       
    }
    protected override void Update()
    {
        base.Update(); 
    }

    




   

   

            
    


}