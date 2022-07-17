using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static string generatorLevel = "PlayerTest 1";
    public static string uiLevel = "UI";

    public int numberOfRooms;

    public LevelGenerator levelGenerator;
    public PlayerManager playerManager;

    public int currentPlayerLevel = 1;


    private void Awake()
    {
        int instancesInScene = FindObjectsOfType<GameManager>().Length;

        if (instancesInScene > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

        
    }

    public void UpdatePlayerLevel()
    {
        currentPlayerLevel++;
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}
