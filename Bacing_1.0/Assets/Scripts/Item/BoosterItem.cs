using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BoosterItem : MonoBehaviour
{
    public int BoosterNumber;

    private void Update()
    {
        transform.eulerAngles += new Vector3(0, 45, 0) * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (GameInstence.instence.CurrentInventory[0] == 0)
                GameInstence.instence.CurrentInventory[0] = BoosterNumber;
            else if (GameInstence.instence.CurrentInventory[1] == 0)
                GameInstence.instence.CurrentInventory[1] = BoosterNumber;
            else
                return;

            Destroy(gameObject);
        }
    }
}
