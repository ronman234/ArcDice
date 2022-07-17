using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectCharacter : MonoBehaviour
{
    public GameObject[] characterArray;
    int counter = 0;
    int nextCharacter;

    private void OnEnable()
    {
        counter = PlayerPrefs.GetInt("CharacterSelected");
        characterArray[counter].SetActive(true);
        //counter = 0;
    }
    public void Left()
    {
        characterArray[counter].SetActive(false);
        counter--;
        if (counter <= 0)
        {            
            counter = characterArray.Length - 1;
            
        }
        
        characterArray[counter].SetActive(true);
    }

    public void Right()
    {
        characterArray[counter].SetActive(false);
        counter++;
        if (counter == characterArray.Length - 1)
        {
            counter = 0;
        }
        
        characterArray[counter].SetActive(true);
    }

    public void StartGame()
    {
        //characterArray[PlayerPrefs.GetInt("CharacterSelected")].SetActive(false);
        PlayerPrefs.SetInt("CharacterSelected", counter);
        PlayerPrefs.Save();
        SceneManager.LoadScene("PlayerTest", LoadSceneMode.Single);
    }
}
