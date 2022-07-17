using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseObject;

    // Update is called once per frame
    void Update()
    {
        if(pauseObject.activeInHierarchy)
        {
            PauseGame();
        }
        if(!pauseObject.activeInHierarchy)
        {
            ResumeGame();
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseObject.SetActive(false);
    }
}
