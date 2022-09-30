using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateManager : MonoBehaviour
{
    public static InstantiateManager instance;

    public GameObject[] playerChar;
    public GameObject[] levelData;
    
    
    public int charIndex = 0;
    public int levelIndex = 0;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
        
        CreateCharacther();
        CreateLevel();
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("level", levelIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateCharacther()
    {
        //carIndex = PlayerPrefs.GetInt("cars", 0);
        //Instantiate Game Clone
        var StartPos = new Vector3(1f, 0.5f, 0f);
        var startRot = Quaternion.Euler(0, 90f, 0);
        Instantiate(playerChar[charIndex], StartPos, startRot);
    }

    void CreateLevel()
    {
        levelIndex = PlayerPrefs.GetInt("level");
        int limitedIndex = levelIndex;
        if (limitedIndex >= levelData.Length)
        {
            
            limitedIndex = levelIndex % levelData.Length;
            //levelIndex = limitedIndex;
            Debug.Log("limited : "+limitedIndex);
        }
        var StartPos = new Vector3(0f, 0f, 0f);
        var startRot = Quaternion.Euler(0, 0f, 0);
        Instantiate(levelData[limitedIndex], StartPos, startRot);
    }
}
