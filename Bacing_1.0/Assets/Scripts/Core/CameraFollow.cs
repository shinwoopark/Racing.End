using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target_tr;

    public float MoveSpeed;

    private Vector3 _targetPos;

    void Start()
    {
        
    }

    void Update()
    {
        _targetPos = new Vector3(Target_tr.position.x, Target_tr.position.y, Target_tr.position.z);

        transform.position = Vector3.Lerp(transform.position, _targetPos, MoveSpeed * Time.deltaTime);

        transform.rotation = Quaternion.Lerp(transform.rotation, Target_tr.rotation, MoveSpeed * Time.deltaTime);
    }
}
