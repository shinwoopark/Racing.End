using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManger : MonoBehaviour
{
    public GameObject[] Hurdles;

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void SpawnHurdle(int hurdleNumber, Transform spawnPos, float spawnRotation)
    {
        if (hurdleNumber == 5)
            hurdleNumber = Random.Range(0, 5);

        Instantiate(Hurdles[hurdleNumber], spawnPos.position, Quaternion.Euler(0, spawnRotation, 0));
    }
}
