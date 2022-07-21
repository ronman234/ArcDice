using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{

    public GameObject pauseObject;
    public GameObject pauseMenu;
    public GameObject levelEnd;


    private PlayerControls playerInput;
    int characterSelected = 0;
    public PlayerManager playerManager;

    public GameManager gameManager;
    public HUDManager hudManager;

    [SerializeField]
    string MainMenu = "UI1";

    private void Awake()
    {
        characterSelected = PlayerPrefs.GetInt("CharacterSelected");
        playerInput = new PlayerControls();
        //GameManager.Instance.pauseManager = gameObject;
        //GameManager.Instance.pauseMenu = pauseObject;
        //GameManager.Instance.resumeMenu = pauseMenu;
        //GameManager.Instance.endScreen = levelEnd;
        //gameManager = FindObjectOfType<GameManager>();

    }

    private void Start()
    {
        GameManager.Instance.pauseManager = gameObject;
        GameManager.Instance.pauseMenu = pauseObject;
        GameManager.Instance.resumeMenu = pauseMenu;
        GameManager.Instance.endScreen = levelEnd;
    }
    private void OnEnable()
    {
        playerInput.Enable();
        playerInput.Default.Pause.started += DoPause;
    }
    private void OnDisable()
    {
        playerInput.Disable();
        playerInput.Default.Pause.started += DoPause;
    }

    private void Update()
    {
        if (pauseObject.activeInHierarchy)
        {
            PauseGame();
        }
        if (!pauseObject.activeInHierarchy)
        {
            ResumeGame();
        }
    }

    private void DoPause(InputAction.CallbackContext obj)
    {
        if (!pauseObject.activeInHierarchy)
        {
            pauseObject.SetActive(true);
        }
        else if (pauseObject.activeInHierarchy)
        {
            pauseObject.SetActive(false);
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
        pauseObject.SetActive(true);
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseObject.SetActive(false);
    }

    public void QuitGame()
    {
        PlayerPrefs.SetInt("CharacterSelected", characterSelected);
        PlayerPrefs.SetInt("Level", playerManager.playerLevel);
        PlayerPrefs.Save();

        SceneManager.LoadScene(MainMenu, LoadSceneMode.Single);
    }
}
