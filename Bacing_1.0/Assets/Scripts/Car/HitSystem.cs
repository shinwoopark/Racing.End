using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSystem : MonoBehaviour
{
    public CarMoveSystem CarMoveSystem;

    public Rigidbody sphereCollider;

    public float NuckBack;

    public AudioSource HitSound;

    //public GameObject HitEffect;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            Vector3 dir = other.transform.position - transform.position;

            CarMoveSystem.CurrentSpeed = 0;
            sphereCollider.AddForce( -dir * NuckBack);

            if (gameObject.tag == "Player")
                HitSound.Play();     
        }

        if(other.gameObject.layer == 6 && gameObject.tag == "Player")
            HitSound.Play();

        if (other.gameObject.tag == "Finish")
        {
            if (gameObject.tag == "Player")
            {
                GameManager.manager.StartCoroutine(GameManager.manager.FinishRacing(true));
            }
            else
            {
                GameManager.manager.StartCoroutine(GameManager.manager.FinishRacing(false));
            }
        }
    }
}
