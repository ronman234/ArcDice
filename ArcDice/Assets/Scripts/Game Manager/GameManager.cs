using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static string generatorLevel = "PlayerTest 1";
    public static string uiLevel = "UI";

    public LevelGenerator levelGenerator;
    public PlayerManager playerManager;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void LoadScene(string scene)
    {
        levelGenerator.numRooms = 5 * playerManager.playerLevel / 2;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}
