using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject playerName;
    private MenuUI menuUIScript;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            playerName.SetActive(false);
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        menuUIScript = GetComponent<MenuUI>();
    }

  
}
