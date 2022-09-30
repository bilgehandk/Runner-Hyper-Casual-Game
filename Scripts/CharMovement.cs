using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class CharMovement : MonoBehaviour
{

    [SerializeField] public float forwardSpeed;

    [SerializeField] public float rightleftSpeed;
    
    public RuntimeAnimatorController[] MyControllers;
    //[SerializeField] private GameObject[] playerCharacthers;
    //public int charIndex = 0;

    //public GameObject sandbag;

    public static CharMovement _instance;

    private string valueOfGate;
    private Animator animator;

    public int amountBullet = 0;
    
    
    public bool isFire = false;
    public bool runner = false;
    public bool runX = false;
    public bool breakGlass = false;
    public bool died = false;
    public bool finish = false;
    //public bool createdSandbag = false;
    public bool startGame = false;

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
        //BreakableObjectManager._instance.REPAIR = true;
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = MyControllers[0];
    }

    // Update is called once per frame
    void Update()
    {
        
        if (finish)
        {
            runner = false;
            runX = false;
            isFire = false;
        }
        
        if (died)
        {
            runner = false;
            runX = false;
            isFire = false;
        }

        //Game start and player is running
        if (runner)
        {
            Move();
        }

        if (runX)
        {
            animator.runtimeAnimatorController = MyControllers[1];
            MoveOnlyX();
        }
        

        //Fire Start
        if (isFire == true && amountBullet > 0)
        {
            FireSystem._instance.Shoot(isFire);
            //Set the fire animation
            animator.SetBool("Atesleme", true);
        }
        
        
    }

    public void IdleChar()
    {
        animator.runtimeAnimatorController = MyControllers[0];
    }
    private void Move()
    {
        float horizontalAxis = CrossPlatformInputManager.GetAxis("Horizontal")*rightleftSpeed*Time.deltaTime;

        this.transform.Translate(horizontalAxis, 0 ,forwardSpeed*Time.deltaTime);
        
        transform.position = new Vector3 (transform.position.x,(Mathf.Clamp(transform.position.y, 0.5f,0.53f)),(Mathf.Clamp(transform.position.z, -4.5f,4.5f)));
        
        animator.runtimeAnimatorController = MyControllers[1];
    }

    private void MoveOnlyX()
    {
        this.transform.Translate(0, 0 ,forwardSpeed*Time.deltaTime);
        
        transform.position = new Vector3 (transform.position.x,(Mathf.Clamp(transform.position.y, 0.5f,0.53f)),(Mathf.Clamp(transform.position.z, -4.5f,4.5f)));
    }
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "glass")
        {
            animator.runtimeAnimatorController = MyControllers[2];
            runner = false;
            CameraFollow._instance.distance.y = 4;
            CameraFollow._instance.distance.x = -7;

            died = true;
        }

        if (collision.gameObject.tag == "obstacle")
        {
            animator.runtimeAnimatorController = MyControllers[2];
            runner = false;
            CameraFollow._instance.distance.y = 4;
            CameraFollow._instance.distance.x = -7;

            died = true;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "gate")
        {
            //BreakableObjectManager._instance.REPAIR = false;
            //Decrease spped of charachter
            forwardSpeed = 4;
            
            //Create sandbag clone
            //var sandbagPos = new Vector3(other.gameObject.transform.position.x+2f, 0.5f,
            //    0f);
            //Instantiate(sandbag, sandbagPos, Quaternion.Euler(0f,270f,0f));
            //createdSandbag = true;
            
            //Move only x axis
            runner = false;
            runX = true;

            
            isFire = true;
            Destroy(other.gameObject);
            
            //Breake the glass
            StartCoroutine(BreakGlassObject());
            
            


            Debug.Log("Ateş edildi");

        }

        if (other.gameObject.tag == "block")
        {
            
            //Change speed and close the fire animation
            forwardSpeed = 12;
            isFire = false;
            //Change mover
            runner = true;
            runX = false;
            
            //createdSandbag = false;
            breakGlass = false;
            
            
            animator.SetBool("Atesleme", false);
        }

        
        
        if (other.gameObject.tag == "bullet")
        {
            Debug.Log("Çarptı");
            Destroy(other.gameObject);
            amountBullet+=5;
        }

        if (other.gameObject.tag == "finish")
        {
            animator.runtimeAnimatorController = MyControllers[0];
            GameManager._instance.money += amountBullet;
            runner = false;
            finish = true;
            
            
        }
    }
    
    
    IEnumerator BreakGlassObject()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(0.5f);
        breakGlass = true;

        
    }
}
