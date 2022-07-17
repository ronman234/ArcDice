using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacter : MonoBehaviour
{
    public GameObject[] characterArray;
    int counter = 0;
    int nextCharacter;

    private void OnEnable()
    {
        characterArray[0].SetActive(true);
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
}
