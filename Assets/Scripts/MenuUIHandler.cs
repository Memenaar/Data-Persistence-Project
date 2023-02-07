using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
#if UNITY_EDITOR
    using UnityEditor;
#endif



// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    public MainManager mainManager;
    private SaveDataManager saveManager;
    private UniversalMethods universalMethods;
    private string nameInput;
    private int tempScore;

    // Start is called before the first frame update
    void Awake()
    {
        saveManager = GameObject.Find("SaveDataManager").GetComponent<SaveDataManager>();
        if (SaveDataManager._highScores != null)
        {

        }
        
    }
    
    public void StartNew()
    {
        SceneManager.LoadScene("main");

    }

    public void ToMenu()
    {
        SceneManager.LoadScene("menu");
    }

    public void ToScoreboard()
    {
        SceneManager.LoadScene("scoreboard");
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else 
            Application.Quit();
        #endif
    }

    public void ReadNameInput(string s)
    {
        bool invalid = string.IsNullOrWhiteSpace(s);
        if(invalid == true){
            nameInput = null;
        } else {
            nameInput = s;
        }
    }

    public void CommitScore()
    {
        if(nameInput != null)
        {
            tempScore = mainManager.finalScore;
            SaveDataManager.SaveScores(tempScore, nameInput);
            mainManager.NameBlock.SetActive(false);
            mainManager.HighScoreBlock.SetActive(true);
            UniversalMethods.SetLeaderBoard();
            mainManager.GameOverBlock.SetActive(true);
            MainManager.m_GameOver = true;
            StateManager._gameState = StateManager.GameState.Active;
        } else {
            GameObject.Find("NameError").GetComponent<TextMeshProUGUI>().text = "Please enter a name before saving!" ;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
