using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sandbagController : MonoBehaviour
{
    private Rigidbody rb;
    public bool working = false;

    public static sandbagController _instance;

    
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
        rb = GetComponent<Rigidbody>();
        working = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (working)
        {
            var dirX = Vector3.right;
            this.transform.Translate(0, 0 ,-CharMovement._instance.forwardSpeed*Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 270f, 0);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "block")
        {
            Destroy(rb.gameObject);
        }
    }
}
