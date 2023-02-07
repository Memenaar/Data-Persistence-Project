using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using TMPro;

public class SaveDataManager : MonoBehaviour
{
    public static SaveDataManager Instance;
    public GameObject topScore;
    public GameObject buttons;
    
    [System.Serializable]
    public class HighScore 
    {
        public string Name;
        public int Score;
    }

    [System.Serializable]
    public class Leaderboard
    {
       public List<HighScore> lbList;
    }

    public static List<HighScore> _highScores = new List<HighScore>();

    private void Awake()
    {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            DontDestroyOnLoad(gameObject);

            LoadScores();
            if (_highScores != null)
            {
                topScore.SetActive(true);
                topScore.GetComponent<TextMeshProUGUI>().text = "Can you beat " + _highScores[0].Name +"'s high score of " + _highScores[0].Score + "?";
                buttons.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -50, 0);
            }
    }

   
    public static void SaveScores(int currentScore, string playerName)
    {
        if (_highScores == null)
        {
            _highScores = new List<HighScore>(1);
        }
        HighScore newHighScore = new HighScore();
        newHighScore.Score = currentScore;
        newHighScore.Name = playerName;
        _highScores.Add(newHighScore);
        _highScores = _highScores.OrderByDescending(x => x.Score).ToList();

        if (_highScores.Count > 10)
        {
            _highScores = _highScores.GetRange(0,10).ToList();
        }
        UniversalMethods.UpdateTopScore();
        WriteLeaderboard(_highScores);
    }

    public void LoadScores()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            Leaderboard loadedLB = new Leaderboard();
            string json = File.ReadAllText(path);
            loadedLB = JsonUtility.FromJson<Leaderboard>(json);
            _highScores = loadedLB.lbList;
        } else {
            _highScores = null;
        }

    }

   public static void WriteLeaderboard(List<HighScore> lbList)
    {
        Leaderboard newLB = new Leaderboard();
        newLB.lbList = _highScores;
        string json = JsonUtility.ToJson(newLB);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

}