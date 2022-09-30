using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager _instance;
    public TextMeshProUGUI bulletAmount;
    

    public GameObject winScreen;
    public GameObject DiedScreen;
    public GameObject mainScreen;
    public GameObject settings;
    public GameObject Starter;
    public int money;

    private bool updateOnlyOnce = false;
    
    
    // Start is called before the first frame update
    void Awake()
    {
        
        
        if(_instance == null)
        {
            _instance = this;
        }
    }

    private void Start()
    {
        updateOnlyOnce = false;
    }

    // Update is called once per frame
    void Update()
    {
        bulletAmount.text = CharMovement._instance.amountBullet.ToString();
        
        //Open finish Canvas
        if (CharMovement._instance.finish == true && updateOnlyOnce == false)
        {
            StartCoroutine(Finish());
            updateOnlyOnce = true;
        }
        
        if (CharMovement._instance.died == true && updateOnlyOnce == false)
        {
            StartCoroutine(Died());
            updateOnlyOnce = true;
        }
    }
    
    

    IEnumerator Finish()
    {

        LevelManager.instance.coins.Emit(CharMovement._instance.amountBullet);
        
        //Increase level index
        InstantiateManager.instance.levelIndex++;
        PlayerPrefs.SetInt("level", InstantiateManager.instance.levelIndex);
        
        yield return new WaitForSeconds(3.5f);
        
        //Show rectscreen
        winScreen.gameObject.SetActive(true);
        mainScreen.gameObject.SetActive(false);
        


    }

    IEnumerator Died()
    {
        
        
        yield return new WaitForSeconds(3.5f);
        
        DiedScreen.gameObject.SetActive(true);
        mainScreen.gameObject.SetActive(false);
        
        
    }

    public void OpenSettings()
    {
        //if (CharMovement._instance.createdSandbag == true)
        //{
        //    sandbagController._instance.working = false;
        //}
        CharMovement._instance.isFire = false;
        CharMovement._instance.runner = false;
        CharMovement._instance.runX = false;
        CharMovement._instance.IdleChar();
        settings.gameObject.SetActive(true);
        mainScreen.gameObject.SetActive(false);
    }

    public void CloseSettings()
    {
        //if (CharMovement._instance.createdSandbag == true)
        //{
        //    sandbagController._instance.working = true;
        //}
        settings.gameObject.SetActive(false);
        mainScreen.gameObject.SetActive(true);
        CharMovement._instance.isFire = true;
        CharMovement._instance.runner = true;
        CharMovement._instance.runX = true;
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void ContinueAfterDied()
    {
        SceneManager.LoadScene(0);
    }

    public void StartGame()
    {
        CharMovement._instance.runner = true;
        Starter.gameObject.SetActive(false);
    }

    public void NextLevel()
    {
        
        SceneManager.LoadScene(0);
    }

    
    

}
