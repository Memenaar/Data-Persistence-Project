using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Linq;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    private UniversalMethods universalMethods;

    public Text ScoreText;
    public Text HighScoreText;
    public GameObject GameOverBlock;
    public GameObject NameBlock;
    public GameObject HighScoreBlock;
    
    private bool m_Started = false;
    private int m_Points;
    public int finalScore;
    
    public static bool m_GameOver = false;
    private string playerName;

    public string Name;

    void Awake()
    {
        GameObject.Find("Canvas").GetComponent<MenuUIHandler>().mainManager = this;
        StateManager._gameState = StateManager.GameState.Active;
    }
    // Start is called before the first frame update
    void Start()
    {
        UniversalMethods.UpdateTopScore();
        SpawnBricks();
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        } else if ((GameObject.FindGameObjectsWithTag("brick").Length == 0) && (StateManager._gameState == StateManager.GameState.Active) && (Ball.transform.position.y < 2))
        {
            SpawnBricks();
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space) && StateManager._gameState != StateManager.GameState.Paused)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                m_GameOver = false;
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        finalScore = m_Points;
        if (SaveDataManager._highScores == null){
            StateManager._gameState = StateManager.GameState.Paused;
            NameBlock.SetActive(true);
        } else {
            if ((SaveDataManager._highScores.Count < 10) || (finalScore > (SaveDataManager._highScores[SaveDataManager._highScores.Count - 1].Score)))
            {
                StateManager._gameState = StateManager.GameState.Paused;
                NameBlock.SetActive(true);
            } else {
                HighScoreBlock.SetActive(true);
                UniversalMethods.SetLeaderBoard();
                GameOverBlock.SetActive(true);
                m_GameOver = true;
                StateManager._gameState = StateManager.GameState.Active;
            }
        }
    }

    public void SpawnBricks()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

}
