using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnManager : MonoBehaviour
{
    public GameObject[] ItemSpawnPoses;

    public GameObject[] Items;

    private void Start()
    {
        Debug.Log(ItemSpawnPoses.Length);
        Debug.Log(Items.Length);

        for (int i = 0; i < ItemSpawnPoses.Length; i++)
        {
            for (int j = 0; j < Items.Length; j++)
            {
                int randomeItem = Random.Range(0, 5);

                if (randomeItem == 0 || randomeItem == 1)
                {
                    Debug.Log(randomeItem);
                    Instantiate(Items[randomeItem], ItemSpawnPoses[i].transform.GetChild(j).transform.position, Quaternion.Euler(0, 0, 40));
                }                   
                else
                    Instantiate(Items[randomeItem], ItemSpawnPoses[i].transform.GetChild(j).transform.position, Quaternion.identity);
            }     
        }
    }
}
