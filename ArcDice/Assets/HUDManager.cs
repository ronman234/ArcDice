using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{

    public TextMeshProUGUI healthText;
    string health = "Health ";
    public PlayerManager playerManager;
    public PlayerController playerController;
    public Image spellImage;
    public Image[] spelTypesImage;
    public Image element;
    public Image[] elementTypes;

    // Start is called before the first frame update
    void Start()
    {
        healthText.text = health + playerManager.Health;
        GetElement();
        GetSpell();
    }

    public void GetElement()
    {
        switch (playerController.GetComponent<Spell>().spellElement)
        {
            case "fire":
                element = elementTypes[0];
                break;
            case "ice":
                element = elementTypes[1];
                break;
            case "crystal":
                element = elementTypes[2];
                break;
            case "wind":
                element = elementTypes[3];
                break;
            case "holy":
                element = elementTypes[4];
                break;
            case "unholy":
                element = elementTypes[5];
                break;

        }
    }

    public void GetSpell()
    {
        switch (playerController.AttackTrigger)
        {
            case "Blade":
                spellImage = spelTypesImage[0];
                break;
            case "Bolt":
                spellImage = spelTypesImage[0];
                break;
            case "Wave":
                spellImage = spelTypesImage[0];
                break;
            case "Cone":
                spellImage = spelTypesImage[0];
                break;           

        }
    }

    public void UpdateHealth()
    {
        healthText.text = health + playerManager.Health;
    }
    
}
