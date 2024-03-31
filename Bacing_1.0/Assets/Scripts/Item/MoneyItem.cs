using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyItem : MonoBehaviour
{
    public int MoneyAmount;

    private void Update()
    {
        transform.eulerAngles += new Vector3(0, 45, 0) * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameInstence.instence.CurrentMoney += MoneyAmount;
            Destroy(gameObject);
        }
    }
}
