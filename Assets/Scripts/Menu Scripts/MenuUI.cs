using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;
public class MenuUI : MonoBehaviour
{
    public Button startButton;
    public TextMeshProUGUI playersName;
    public static MenuUI Instance;
    private TMP_InputField inputField;
    private string player;
    public GameObject playerName;
    public Text bestScoreText;
    private MainManager mainManagerScript;
    private string loadedPlayerName;
    private int loadedHighScore;
    bool saved = false;
    private void Awake()
    {

        LoadPrefs();
        Debug.Log(loadedPlayerName + loadedHighScore);
        bestScoreText.text = "Best score : " + loadedPlayerName + " : " + loadedHighScore;
        inputField = GetComponentInChildren<TMP_InputField>();
        if (inputField)
        {
            LoadName();
        }else
        {
            Debug.Log("No reference");
        }

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
        
    }


    // Start is called before the first frame update
    void Start()
    {
      
        
        startButton.onClick.AddListener(StartGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartGame()
    {
        SaveName();
        if (saved)
       {
            playerName.SetActive(false);
            SceneManager.LoadScene(1);
        }
    }

    void SaveName()
    {
        player = playersName.text;
        PlayerPrefs.SetString("PlayerName", player);
        saved = true;
    }

    void LoadName()
    {

        player = PlayerPrefs.GetString("PlayerName");
        inputField.text = player;
        
        
    }

    void LoadPrefs()
    {
       loadedPlayerName =  PlayerPrefs.GetString("loadedPlayerName");
       loadedHighScore =  PlayerPrefs.GetInt("loadedHighScore");
    }
   
    

}
