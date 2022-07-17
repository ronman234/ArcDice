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
    public Sprite[] spelTypesImage;
    public Image element;
    public Sprite[] elementTypes;

    // Start is called before the first frame update
    void Start()
    {
        healthText.text = health + playerManager.Health;
        GetElement();
        GetSpell();
    }

    public void GetElement()
    {
        Debug.Log(playerController.GetComponent<Spell>().spellElement);
        switch (playerController.GetComponent<Spell>().spellElement)
        {
            case "Fire":
                element.sprite = elementTypes[0];
                break;
            case "Ice":
                element.sprite = elementTypes[1];
                break;
            case "Crystal":
                element.sprite = elementTypes[2];
                break;
            case "Wind":
                element.sprite = elementTypes[3];
                break;
            case "Holy":
                element.sprite = elementTypes[4];
                break;
            case "Unholy":
                element.sprite = elementTypes[5];
                break;
        }
    }

    public void GetSpell()
    {
        Debug.Log(playerController.AttackTrigger);
        switch (playerController.AttackTrigger)
        {
            case "Blade":
                spellImage.sprite = spelTypesImage[0];
                break;
            case "Bolt":
                spellImage.sprite = spelTypesImage[1];
                break;
            case "Wave":
                spellImage.sprite = spelTypesImage[2];
                break;
            case "Cone":
                spellImage.sprite = spelTypesImage[3];
                break;
        }
    }

    public void UpdateHealth()
    {
        healthText.text = health + playerManager.Health;
    }

    private void Update()
    {
        UpdateHealth();
    }
}
