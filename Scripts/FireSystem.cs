using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSystem : MonoBehaviour
{
    public Transform firePoint;

    public GameObject bullet;

    public static FireSystem _instance;
    public float fireRate = 0.01F;
    private float nextFire = 0.0F;
    
    int mermiSayısı = 0;
    
    

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
        
    }

    // Update is called once per frame
    void Update()
    {
        Shoot(CharMovement._instance.isFire);
    }

    public void Shoot(bool isFire)
    {
        //Debug.Log("Zaman: " + Time.time + " Zaman 2 = " + Time.deltaTime);
        if (isFire && Time.time > nextFire && CharMovement._instance.amountBullet > 0)
        {
            nextFire = Time.time + fireRate;
            Instantiate(bullet, firePoint.transform.position, Quaternion.Euler(new Vector3(0, 0, 270)));
            mermiSayısı++;
            Debug.Log("Mermi miktarı: " + mermiSayısı);
            Debug.Log("Klone oluşturuldu ");
        }
        
        
        
      
        
    }
}
