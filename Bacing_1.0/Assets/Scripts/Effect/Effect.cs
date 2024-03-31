using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

public class Effect : MonoBehaviour
{
    public CarMoveSystem CarMoveSystem;

    public GameObject ParticleSystem;

    public Transform Target_tr;

    public float MoveSpeed;

    private Vector3 _targetPos;

    void Update()
    {
        UpdateEffect();

        _targetPos = new Vector3(Target_tr.position.x, Target_tr.position.y, Target_tr.position.z);

        transform.position = _targetPos;

        transform.rotation = Quaternion.Lerp(transform.rotation, Target_tr.rotation, MoveSpeed * Time.deltaTime);
    }

    public void UpdateEffect()
    {
        if (GameInstence.instence.bRacing &&
            Input.GetAxis("Vertical") > 0 &&
            CarMoveSystem.CurrentState == CarMoveSystem.State.Move)
        {
            ParticleSystem.SetActive(true);
        }
        else
        {
            ParticleSystem.SetActive(false);
        }
    }
}
