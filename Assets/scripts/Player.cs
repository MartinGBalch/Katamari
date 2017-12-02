using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    public enum Team { Red, Blue }

    public Team team;

    public Action netUpdate;
    public float pickupableRatio = 1.5f;

    public Transform cameraPos;

    Transform player;

    Camera mainCam;
    Rigidbody rbody;
    public float speed;

    GameObject pickup;
    //Rigidbody pickupMass;
    
    [SyncVar(hook = "healthCheck")] private float health = 1;
    

    public projectiles bullet;

    public float maxHealth;

    Vector3 bulletSpawnPoint
    {
        get
        {
            Vector3 pos = transform.position;
            RaycastHit hit;
            Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition), out hit);
            pos -= new Vector3(hit.point.x, 0, hit.point.z) - pos;
            return transform.position + (pos.normalized * 0.5f);
        }
    }

	// Use this for initialization
	void Start ()
    {
        if (isLocalPlayer)
        {
            PlayerMan.localPlayer = this;
        }

        Getcomponents();

        if(isServer)
        {
            health = maxHealth;
        }
        if(isLocalPlayer)
        {
            netUpdate += updateLocal;
        }
        else
        {
            netUpdate += updateRemote;
        }
        
	}

     void Awake()
    {
        PlayerMan.players.Add(this);
    }

    // Update is called once per frame
    void Update ()
    {
        if (!isLocalPlayer) return;

        move();
        //shoot();
	}

    void updateLocal()
    {
        move();
       // shoot();
    }

    void updateRemote()
    {
        move();
       // shoot();
    }

    void move()
    {
        Vector2 movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rbody.AddForce(movementInput.x * speed, 0, movementInput.y * speed, ForceMode.Acceleration);
    }

    void shoot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            CmdShootRequest(bulletSpawnPoint);
        }
    }

    public void takeDamage(float d)
    {
        health -= d;
    }


    void healthCheck(float h)
    {
        bool alive = health > 0;
        if (alive && h <= 0)
        {
            Death();
        }
        else if (!alive && h > 0)
        {
            Revive();
        }
        
    }

    void Death()
    {
        Debug.Log("ya fuckin' died");
    }

    void Revive()
    {
        Debug.Log("you are no longer dead");
    }

    void CmdShootRequest(Vector3 pos)
    {
        projectiles bul = Instantiate<projectiles>(bullet, pos, Quaternion.identity) as projectiles;
        bul.fire(transform);
        NetworkServer.Spawn(bul.gameObject);
        
    }

    void Getcomponents()
    {
        rbody = GetComponent<Rigidbody>();
        mainCam = FindObjectOfType<Camera>();
       // pickupMass = pickup.GetComponent<Rigidbody>();
    }


    private void OnCollisionEnter(Collision RollUp)
    {
        if(RollUp.gameObject.tag != "pickup")
        {
            return;
        }

        bool hitTheBall = false;

        foreach(ContactPoint contact in RollUp.contacts)
        {
            if (contact.thisCollider == GetComponent<Collider>())
            {
                hitTheBall = true;
                break;
            }
        }

        if(hitTheBall)
        {
            if (RollUp.rigidbody.mass < rbody.mass * pickupableRatio && RollUp.rigidbody.isKinematic) 
            {
                float increase = RollUp.rigidbody.mass * .1f;
                rbody.mass += increase;
                //transform.localScale += new Vector3(increase, increase, increase);
                GetComponent<SphereCollider>().radius += increase;
                Destroy(RollUp.rigidbody);
                RollUp.transform.parent = transform;
            }

        }
    }
}
