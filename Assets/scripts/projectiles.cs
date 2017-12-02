using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class projectiles : MonoBehaviour
{

    public float speed;
    public Transform owner;
    bool fired = false;

    public float damage;

    Vector3 direction;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        move();
	}

    public void fire(Transform t)
    {
        owner = t;
        direction = (transform.position - owner.transform.position).normalized;
        fired = true;
    }

    void move()
    {
        if(fired)
        {
            transform.Translate(direction * speed);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        bool isPlayerObject = PlayerMan.players.Select(x => x.gameObject).Contains(col.gameObject);
        if (isPlayerObject && col.gameObject != PlayerMan.localPlayer)
        {
            PlayerMan.players.Where(x => x.gameObject == col.gameObject).ToArray()[0].takeDamage(damage);
        }
    }
}
