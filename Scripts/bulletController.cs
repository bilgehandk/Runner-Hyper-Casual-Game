using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class bulletController : MonoBehaviour
{
    public static bulletController _instance;
    private Rigidbody rb;

    public float bulletSpeed;
    private float destroyTime = 3f;
    private int collisionCount;
    

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Bullet is acquaring velocity
        rb = GetComponent<Rigidbody>();
        var dirX = Vector3.right;
        rb.velocity = dirX * bulletSpeed;

    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "glass")
        {
            Destroy(rb.gameObject, 0.1f);
        }

        if (collision.gameObject.tag == "shooting")
        {
            Destroy(collision.gameObject, 0.1f);
        }
        
       
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "gate")
        {
            Destroy(rb.gameObject);
        }
    }

    private void Update()
    {
        //prevent the breaking other glass
        Destroy(gameObject, destroyTime);
    }

    
}
