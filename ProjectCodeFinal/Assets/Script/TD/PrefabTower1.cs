using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabTower1 : MonoBehaviour
{
    [SerializeField] private Transform[] prefabTowerPos;
    [SerializeField] private GameObject[] prefabTower;
    public bool[] isLockPrefab;
    // Start is called before the first frame update
    void Start()
    {
        prefabTower = Resources.LoadAll<GameObject>("Prefabs/TD/OnMouseHitTower");
        isLockPrefab = new bool[prefabTower.Length];
        Instantiate(prefabTower[0], prefabTowerPos[0].position, Quaternion.identity);
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckLockTowerPrefab()
    {
        for (int i = 0; i <= isLockPrefab.Length - 1; i++)
        {
            isLockPrefab[0] = true;
            if (isLockPrefab[0] == true)
            {
                Instantiate(prefabTower[i], prefabTowerPos[i].position, Quaternion.identity);

            }
        }
        /*
         * 
         * */
    }
}
