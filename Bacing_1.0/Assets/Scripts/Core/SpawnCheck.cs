using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCheck : MonoBehaviour
{
    public SpawnManger SpawnManger;
    public int HurdleNumber;
    public Transform SpawnPos;
    public float SpawnDir;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            SpawnManger.SpawnHurdle(HurdleNumber, SpawnPos, SpawnDir);
            Destroy(gameObject);
        }
            
    }
}
