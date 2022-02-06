using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodWall : Tower
{
    [SerializeField] private float heal;
    [SerializeField] private float healTime;
    private float reHealTime;
    private bool levalup;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        levalup = false;
    }

    protected override void Update()
    {
        base.Update();
        StartCoroutine(Heal());
 
        if (levalup == false && level == 5)
        {
            levalup = true;
            healTime -= 1f;
        }
        if (levalup == false && level == 8)
        {
            levalup = true;
            heal *= 1.5f;
        }

    }

    IEnumerator Heal()
    {
        yield return 0;
        reHealTime += Time.deltaTime;
        if(reHealTime >= healTime)
        {
            health += heal;
            reHealTime = 0;
            if (health >= maxHealth) health = maxHealth;
        }

    }


}
