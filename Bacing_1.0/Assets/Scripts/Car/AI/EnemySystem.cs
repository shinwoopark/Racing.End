using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySystem : MonoBehaviour
{
    public CarMoveSystem CarMoveSystem;
    public Transform WayPoint;
    private int _wayPointNumber;

    public float FowardSpeed;
    public float RotationSpeed;
    public float RayLenth;

    private Vector3 _dir;

    void Update()
    {
        if (GameInstence.instence.bRacing)
        {
           
            UpdateSafe();
        }           
    }

    private void FixedUpdate()
    {
        if (GameInstence.instence.bRacing)
        {
            UpdateWayPoint();
            UpdateMove();
        }
            
    }

    private void UpdateWayPoint()
    {
        if (GameInstence.instence.bRacing)
        {
            _dir = WayPoint.GetChild(_wayPointNumber).transform.position - transform.position;
        }            
    }

    private void UpdateMove()
    {
        CarMoveSystem.InputSpeed = FowardSpeed;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_dir), Time.deltaTime * RotationSpeed);
    }

    private void UpdateSafe()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward * RayLenth, Color.red);

        if(Physics.Raycast(transform.position,transform.forward,out hit, RayLenth))
        {
            if (hit.transform.gameObject.tag == "Car")
            {
                int dir = Random.Range(0, 2);

                if (dir == 0)
                    dir = -1;

                CarMoveSystem.SphereCollider.AddForce(transform.right * 500 * dir);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == WayPoint.transform.GetChild(_wayPointNumber).gameObject)
        {
            if (GameInstence.instence.bRacing)
                _wayPointNumber++;
        }
    }
}
