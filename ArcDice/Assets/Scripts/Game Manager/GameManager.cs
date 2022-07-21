using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance is null)
                Debug.Log("No Game manager");
            return instance;
        }
    }

    public static string generatorLevel = "Game";
    public static string uiLevel = "Main Menu";

    public LevelGenerator levelGenerator;
    public PlayerManager playerManager;

    public int currentPlayerLevel = 1;

    public Hatch hatch;
    public GameObject pauseMenu;
    public GameObject pauseManager;
    public GameObject resumeMenu;
    public GameObject endScreen;


    private void Awake()
    {
        //hatch = GameObject.FindGameObjectWithTag("Hatch").GetComponent<Hatch>();
        //hatch.enabled = false;
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        pauseManager = GameObject.FindGameObjectWithTag("PauseManager");
        resumeMenu = pauseMenu.GetComponent<PauseManager>().pauseMenu;
        endScreen = pauseMenu.GetComponent<PauseManager>().levelEnd;
        pauseMenu = pauseMenu.GetComponent<PauseManager>().pauseObject;
        pauseManager.GetComponent<PauseManager>().hudManager.UpdateLevel();
        levelGenerator = GameObject.FindGameObjectWithTag("LevelGen").GetComponent<LevelGenerator>();
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
    }

    private void Start()
    {
        playerManager.Health += 5;
        pauseManager = GameObject.FindGameObjectWithTag("PauseManager");
        resumeMenu = pauseMenu.GetComponent<PauseManager>().pauseMenu;
        endScreen = pauseMenu.GetComponent<PauseManager>().levelEnd;
        pauseMenu = pauseMenu.GetComponent<PauseManager>().pauseObject;
        pauseManager.GetComponent<PauseManager>().hudManager.UpdateLevel();
        levelGenerator = GameObject.FindGameObjectWithTag("LevelGen").GetComponent<LevelGenerator>();
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
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
        UpdatePlayerLevel();
        hatch.gameObject.GetComponent<Collider>().enabled = true;
        pauseMenu.SetActive(true);
        resumeMenu.SetActive(false);
        endScreen.SetActive(true);
    }
}
