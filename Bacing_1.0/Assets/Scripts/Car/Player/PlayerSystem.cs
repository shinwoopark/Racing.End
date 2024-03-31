using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSystem : MonoBehaviour
{
    public CarMoveSystem CarMoveSystem;

    public MainUI MainUI;

    private float _fowardPower;

    private bool bWhiper, bEnemyCam;

    public AudioSource GetItemSound, SandStormSound;

    public GameObject EnemyCam_gb;

    void Start()
    {
        
    }

    void Update()
    {
        switch (GameInstence.instence.CurrentEngineLevel)
        {
            case 0:
                _fowardPower = 8000;
                break;
            case 1:
                _fowardPower = 9500;
                break;
            case 2:
                _fowardPower = 11000;
                break;
            case 3:
                _fowardPower = 12500;
                break;
        }

        UpdateInput();
        UpdateEnemyCam();
    }

    private void UpdateInput()
    {
        //Foward
        if (Input.GetAxis("Vertical") > 0)
            CarMoveSystem.InputSpeed = Input.GetAxis("Vertical") * _fowardPower;
        else if (Input.GetAxis("Vertical") < 0)
            CarMoveSystem.InputSpeed = Input.GetAxis("Vertical") * _fowardPower * 0.5f;

        //Turn
        if (Input.GetAxis("Horizontal") != 0)
            CarMoveSystem.InputTurn = Input.GetAxis("Horizontal");
        else
            CarMoveSystem.InputTurn = 0;

        //Drift
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (CarMoveSystem.bGround && Input.GetAxis("Vertical") > 0 && Input.GetAxis("Horizontal") != 0)
            {
                CarMoveSystem.SphereCollider.AddForce(transform.up * 15000);

                CarMoveSystem.CurrentState = CarMoveSystem.State.Drift;
            }
        }

        if(CarMoveSystem.CurrentState == CarMoveSystem.State.Drift)
        {
            if (Input.GetKeyUp(KeyCode.Space) ||
            Input.GetAxis("Horizontal") == 0 ||
            Input.GetAxis("Vertical") == 0)
            {
                CarMoveSystem.CurrentState = CarMoveSystem.State.Move;
            }
        }
       

        //Booster
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (GameInstence.instence.CurrentInventory[0] != 0)
            {
                if (GameInstence.instence.CurrentInventory[0] == 1)
                {
                    CarMoveSystem.BoosterPower = _fowardPower * 1.5f;
                    CarMoveSystem.BoosterTime = 2;
                    CarMoveSystem.CurrentState = CarMoveSystem.State.Booster;
                }
                else if (GameInstence.instence.CurrentInventory[0] == 2)
                {
                    CarMoveSystem.BoosterPower = _fowardPower * 2.5f;
                    CarMoveSystem.BoosterTime = 3;
                    CarMoveSystem.CurrentState = CarMoveSystem.State.Booster;
                }

                GameInstence.instence.CurrentInventory[0] = GameInstence.instence.CurrentInventory[1];
                GameInstence.instence.CurrentInventory[1] = 0;
            }
        }

        //DesertOhter
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (GameInstence.instence.bDesertOther && bWhiper)
            {               
                bWhiper = false;
                MainUI.OtherItem(1, bWhiper);
            }
            else if (GameInstence.instence.bDesertOther && !bWhiper)
            {
                bWhiper = true;
                MainUI.OtherItem(1, bWhiper);
            }
        }

        //MountainOhter
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (GameInstence.instence.bMountainOther && bEnemyCam)
            {
                bEnemyCam = false;
                MainUI.OtherItem(1, bEnemyCam);
            }
            else if (GameInstence.instence.bMountainOther && !bEnemyCam)
            {
                bEnemyCam = true;
                MainUI.OtherItem(1, bEnemyCam);
            }
        }
    }

    private void UpdateEnemyCam()
    {
        if(bEnemyCam)
            EnemyCam_gb.SetActive(true);
        else
            EnemyCam_gb.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "SandStorm")
        {
            MainUI.SandStorm(true, bWhiper);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SandStorm")
        {
            SandStormSound.Play();
        }

        if (other.gameObject.layer == 9)
        {
            GetItemSound.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "SandStorm")
        {
            MainUI.SandStorm(false, bWhiper);
            SandStormSound.Stop();
        }
    }
}
