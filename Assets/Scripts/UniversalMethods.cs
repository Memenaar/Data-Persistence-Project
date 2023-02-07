using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UniversalMethods : MonoBehaviour
{

    

    public static void SetLeaderBoard()
    {
        int n = 0;
        GameObject hsList = GameObject.Find("High Score Block");
        foreach (SaveDataManager.HighScore t in SaveDataManager._highScores)
        {
            hsList.gameObject.transform.GetChild(n).GetComponent<TextMeshProUGUI>().text = (n+1) + ". " + t.Name + " : " + t.Score;
            
            n++;
        }
    }

    public static void UpdateTopScore()
    {
        GameObject hsText = GameObject.Find("HighScoreText");
        
        if (SaveDataManager._highScores == null) 
        {
            hsText.GetComponent<Text>().text = "No High Score";
        } else {
            hsText.GetComponent<Text>().text = "Best Score: " + SaveDataManager._highScores[0].Name + " : " + SaveDataManager._highScores[0].Score;
        }
    }

    /*public string[] SetLeaderBoard()
    {
        public int n = 0;
        string[] lbSlots;
        
        foreach (SaveDataManager.HighScore t in SaveDataManager._highScores)
        {
            public GameObject hsList = GameObject.Find("High Score Block");
            hsList.gameObject.transform.GetChild(n).GetComponent<TextMeshProUGUI>().text = (n+1) + ". " + t.Name + " : " + t.Score;
            
            n++;
        }
        return lbSlots;
    }*/

}
