using System;
using UnityEngine;
using System.Collections;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class Enemy : MonoBehaviour
{

    public event Action OnDie;

    public event Action OnReachPlayer;

    public GameObject Target;

    public float MovementSpeed = 1.5f;

    public bool isDead = false;

    // Use this for initialization
	void Start () {
	
	}
    
	// Update is called once per frame
	void Update ()
	{
	    Vector3 direction = (transform.position - Target.transform.position).normalized;
        transform.LookAt(direction);
        transform.Translate(direction * MovementSpeed * Time.deltaTime);
	}

    void OnCollisionEnter(Collision collision)
    {
//        Debug.Log("COLLISION!");
        if (collision.transform.tag == "Projectile")
        {
            Destroy(collision.gameObject);
            isDead = true;
            gameObject.SetActive(false);
        }
    }

    protected virtual void OnOnDie()
    {
        if (OnDie != null)
        {
            OnDie();
        }
    }

    protected virtual void OnOnReachPlayer()
    {
        if (OnReachPlayer != null)
        {
            OnReachPlayer();
        }
    }
}
