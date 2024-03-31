using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class CarMoveSystem : MonoBehaviour
{
    public Rigidbody SphereCollider;

    public enum State {Move, Drift, Booster}
    public State CurrentState;

    public Transform[] FrontWheels;
    public Transform[] Wheels;

    public float CurrentSpeed;
    public float InputSpeed, InputTurn;
    private float _driftSlowDown = 1;
    private float _trapSlowDown = 1;

    private float TurnAmount = 90;

    public float BoosterPower;
    public float BoosterTime;

    public bool bGround;
    public LayerMask GroundCheck;
    public float RayLenth;

    public AudioSource FowardSound, BackSound, DriftSound, BoosterSound;
    bool bfowardSound = false;
    bool bfowardBack = false;
    bool bdrift = false;
    bool bbooster = false;

    public GameObject[] DriftEffects;
    public GameObject BoosterEffect;

    void Start()
    {
        
    }

    void Update()
    { 
        SphereCollider.transform.parent = null;

        gameObject.transform.position = SphereCollider.transform.position;

        if (GameInstence.instence.bRacing)
        {
            if (CurrentState == State.Booster)
            {
                CurrentSpeed = BoosterPower * 50;
            }
            else
            {
                CurrentSpeed = InputSpeed * _driftSlowDown * _trapSlowDown * 50;
            }           

            UpdateCheckGround();
            SoundUpdate();
            MoveUpdate();
        }            
    }

    private void FixedUpdate()
    {
        if (GameInstence.instence.bRacing)
        {
            
            UpdateWheels();
        }
    }

    private void MoveUpdate()
    {
        //Foward
        if(bGround)
        {
            SphereCollider.drag = 3;
            SphereCollider.AddForce(transform.forward * CurrentSpeed * Time.deltaTime);

            if (CurrentState == State.Booster)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, TurnAmount * InputTurn * Time.deltaTime, 0));
            }
            else
            {
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, TurnAmount * InputTurn * Time.deltaTime * Input.GetAxis("Vertical"), 0));
            }
        }
        else
        {
            SphereCollider.drag = 0.1f;
            SphereCollider.AddForce(transform.up * -3000);
        }

        if (CurrentState == State.Drift)
        {
            for(int i = 0; DriftEffects.Length > i; i++)
            {
                DriftEffects[i].SetActive(true);
            }
            
            _driftSlowDown -= 0.3333333f * Time.deltaTime;
            TurnAmount += 10 * Time.deltaTime;

            if (_driftSlowDown <= 0)
                CurrentState = State.Move;                
        }
        else
        {
            for (int i = 0; DriftEffects.Length > i; i++)
            {
                DriftEffects[i].SetActive(false);
            }
            _driftSlowDown = 1;
            TurnAmount = 90;
            TurnAmount -= InputSpeed / 200;
        }

        if(CurrentState == State.Booster)
        {
            BoosterTime -= Time.deltaTime;
            if (BoosterTime < 0)
            {
                BoosterTime = 0;
                BoosterPower = 1;
                CurrentState = State.Move;
            }              
        }
    }

    private void SoundUpdate()
    {
        if (gameObject.tag != "Player" || !GameInstence.instence.bRacing)
            return;

        switch (CurrentState)
        {
            case State.Move:
                DriftSound.Stop();
                bdrift = false;

                BoosterSound.Stop();
                bbooster = false;
                BoosterEffect.SetActive(false);

                if (CurrentSpeed > 0 && Time.timeScale != 0 && !bfowardSound)
                {
                    FowardSound.Play();
                    bfowardSound = true;
                }
                else if (CurrentSpeed <= 0 || Time.timeScale == 0)
                {
                    FowardSound.Stop();
                    FowardSound.pitch = 1;
                    bfowardSound = false;
                }

                if (CurrentSpeed < 0 && Time.timeScale != 0 && !bfowardBack)
                {
                    BackSound.Play();
                    bfowardBack = true;
                }
                else if (CurrentSpeed >= 0 || Time.timeScale == 0)
                {
                    BackSound.Stop();
                    bfowardBack = false;
                }
                break;
            case State.Drift:
                BoosterSound.Stop();
                bbooster = false;
                BoosterEffect.SetActive(false);

                if (Time.timeScale != 0 && !bdrift)
                {
                    DriftSound.Play();
                    bdrift = true;
                }
                else if (Time.timeScale == 0)
                {
                    DriftSound.Stop();
                    bdrift = false;
                }
                break;

            case State.Booster:
                if (Time.timeScale != 0 && !bbooster)
                {
                    BoosterEffect.SetActive(true);
                    BoosterSound.Play();
                    bbooster = true;
                }
                else if (Time.timeScale == 0)
                {
                    BoosterEffect.SetActive(false);
                    BoosterSound.Stop();
                    bbooster = false;
                }
                break;
        }

        if (bfowardSound)
        {
            if (FowardSound.pitch < 2)
                FowardSound.pitch += Time.deltaTime * 0.5f;
        }
    }

    private void UpdateCheckGround()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, -transform.up, Color.red, RayLenth);

        if (Physics.Raycast(transform.position, -transform.up, out hit, RayLenth, GroundCheck))
        {
            bGround = true;

            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;

            if (hit.transform.tag == "Trap")
            {
                if (gameObject.tag == "Player")
                {
                    switch (GameInstence.instence.CurrentStage)
                    {
                        case 1:
                            if (!GameInstence.instence.bDesertWheel)
                                _trapSlowDown = 0.5f;
                            else
                                _trapSlowDown = 1;
                            break;
                        case 2:
                            if (!GameInstence.instence.bMountainWheel)
                                _trapSlowDown = 0.5f;
                            else
                                _trapSlowDown = 1;
                            break;
                        case 3:
                            if (!GameInstence.instence.bCityWheel)
                                _trapSlowDown = 0.5f;
                            else
                                _trapSlowDown = 1;
                            break;
                    }
                }
                else
                    _trapSlowDown = 0.5f;
            }
            else if (hit.transform.tag == "Pool")
            {
                _trapSlowDown = 0.5f;
            }
            else
                _trapSlowDown = 1;

        }
        else
            bGround = false;
    }

    private void UpdateWheels()
    {
        if (gameObject.tag == "Player")
        {

        }
    }
}
