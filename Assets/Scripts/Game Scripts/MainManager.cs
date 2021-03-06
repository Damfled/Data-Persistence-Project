using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using TMPro;
public class MainManager : MonoBehaviour
{

    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;
    public Text ScoreText;
    public GameObject GameOverText;
    public Text bestScoreText;
    private MenuUI menuUiScript;
    private bool m_Started = false;
    private int m_Points;
    private int highScore;
    public int loadedHighscore;
    string playerName;
    public string loadedPlayerName;
    private bool m_GameOver = false;




    private void Awake()
    {
        LoadHighScore();
        SavePrefs();
    }
    // Start is called before the first frame update
    void Start()
    {
        
        bestScoreText.text = "Best score : " + loadedPlayerName + " : " + loadedHighscore;
        menuUiScript = GameObject.Find("Game Manager").GetComponent<MenuUI>();
        playerName = menuUiScript.playersName.text;
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
        }
        else if (m_GameOver)
        {
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

                
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
       if (m_Points > loadedHighscore)
        {
            highScore = m_Points;      
            bestScoreText.text = "Best score : " + playerName + " : " + highScore;
         
            SaveHighScore();
        }
        m_GameOver = true;
        GameOverText.SetActive(true);
        
    }

    void SavePrefs()
    {
        PlayerPrefs.SetString("loadedPlayerName", loadedPlayerName);
        PlayerPrefs.SetInt("loadedHighScore", loadedHighscore);
        PlayerPrefs.Save();
    }

    [System.Serializable]
    public class SaveData
    {
        public int highScore;
        public string playerName;
    }
    
    public void SaveHighScore()
    {
        SaveData data = new SaveData();
        data.highScore = highScore;
        data.playerName = playerName;
        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savedata.json", json);
        
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savedata.json";
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            loadedHighscore = data.highScore;
            loadedPlayerName = data.playerName;
            
        }
    }
}
