using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class HurdleSystem : MonoBehaviour
{
    public CarMoveSystem CarMoveSystem;

    public GameObject Warning_gb;

    public float FowardSpeed;
    public float RotationSpeed;

    private bool _bMove;

    private Vector3 _dir;

    [HideInInspector]
    public GameObject Player_gb;
    public Transform Player_tr;

    public bool bFindPlayer;

    private void Awake()
    {
        Player_gb = GameObject.Find("Player");
        Player_tr = Player_gb.GetComponent<Transform>();
        _bMove = true;
    }

    void Update()
    {
        if (GameInstence.instence.bRacing && _bMove)
        {
            UpdateFindPlayer();
            WarningUI();
        }
            
    }

    private void FixedUpdate()
    {
        if (GameInstence.instence.bRacing && _bMove)
        {
            UpdateMove();
        }
            
    }

    private void UpdateFindPlayer()
    {
        _dir = Player_tr.position - transform.position;
    }

    private void UpdateMove()
    {
        CarMoveSystem.InputSpeed = FowardSpeed;

        if(bFindPlayer)
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_dir), RotationSpeed);
    }

    private void WarningUI()
    {
        if (bFindPlayer)
        {
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6)
        {
            _bMove = false;
        }
    }
}
