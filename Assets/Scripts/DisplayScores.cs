using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayScores : MonoBehaviour
{
    public GameObject hsHeadline;
    public GameObject hsEmpty;
    public GameObject hsList;
    public GameObject backButton;

    // Start is called before the first frame update
    void Awake()
    {
        if(SaveDataManager._highScores == null)
        {
            hsEmpty.SetActive(true);
            backButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -100, 0);
        } else {
            hsHeadline.SetActive(true);
            hsList.SetActive(true);

            SetLeaderBoard();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLeaderBoard()
    {
        int n = 0;
        foreach (SaveDataManager.HighScore t in SaveDataManager._highScores)
        {
            hsList.gameObject.transform.GetChild(n).GetComponent<TextMeshProUGUI>().text = (n+1) + ". " + t.Name + " : " + t.Score;
            n++;
        }

    }
}
