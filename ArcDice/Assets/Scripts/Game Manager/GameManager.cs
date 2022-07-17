using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static string generatorLevel = "Game";
    public static string uiLevel = "Main Menu";

    public LevelGenerator levelGenerator;
    public PlayerManager playerManager;

    public int currentPlayerLevel = 1;

    public Hatch hatch;
    public GameObject pauseMenu;
    public GameObject resumeMenu;
    public GameObject endScreen;


    private void Awake()
    {
        //hatch = GameObject.FindGameObjectWithTag("Hatch").GetComponent<Hatch>();
        //hatch.enabled = false;
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

    private void Start()
    {
        playerManager.Health += 5;
    }

    public void UpdatePlayerLevel()
    {
        currentPlayerLevel++;
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    public void HatchOn()
    {
        Debug.Log("Here PLease");
        hatch.gameObject.GetComponent<Collider>().enabled = true;
        pauseMenu.SetActive(true);
        resumeMenu.SetActive(false);
        endScreen.SetActive(true);
    }
}
