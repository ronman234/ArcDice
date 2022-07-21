using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI LevelText;
    string health = "Health ";
    string Level = "Level ";

    public PlayerManager playerManager;
    public PlayerController playerController;
    public Image spellImage;
    public Sprite[] spelTypesImage;
    public Image element;
    public Sprite[] elementTypes;
    public GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<GameManager>();
        healthText.text = health + playerManager.Health;
        LevelText.text = Level + GameManager.Instance.currentPlayerLevel;
        GetElement();
        GetSpell();
    }
    public void GetElement()
    {
        Debug.Log(playerController.GetComponent<Spell>().spellElement);
        switch (playerController.GetComponent<Spell>().spellElement)
        {
            case "fire":
                element.sprite = elementTypes[0];
                break;
            case "ice":
                element.sprite = elementTypes[1];
                break;
            case "crystal":
                element.sprite = elementTypes[2];
                break;
            case "wind":
                element.sprite = elementTypes[3];
                break;
            case "holy":
                element.sprite = elementTypes[4];
                break;
            case "unholy":
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
        healthText.text = health + (int)playerManager.Health;
    }

    private void Update()
    {
        UpdateHealth();
    }

    public void UpdateLevel()
    {
        LevelText.text = Level + GameManager.Instance.currentPlayerLevel;
    }
}
