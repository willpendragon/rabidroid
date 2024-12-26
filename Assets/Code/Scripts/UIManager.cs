using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI rockUnitsCounter;
    [SerializeField] Slider playerHPBar;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] TextMeshProUGUI experiencePointsCounter;
    [SerializeField] Inventory inventory;
    public GameObject prefab;
    public Transform content;

    public List<Weapon> weapons;
    public static UIManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        if (playerPrefab != null)
        {
            playerHPBar.value = playerPrefab.GetComponent<PlayerHealth>().currentHealth;
            playerHPBar.maxValue = playerPrefab.GetComponent<PlayerHealth>().maxHealth;
        }
        Inventory inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        weapons = inventory.weapons;
        PopulateWeaponList();
        UpdateRockUnitsCounter();
        UpdateExperiencePointsDisplay();
    }

    private void OnEnable()
    {
        Enemy.OnEnemyDeath += UpdateExperiencePointsDisplay;
        IncreaseRockUnits.OnRockLoot += UpdateRockUnitsCounter;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyDeath -= UpdateExperiencePointsDisplay;
        IncreaseRockUnits.OnRockLoot -= UpdateRockUnitsCounter;
    }
    public void UpdateRockUnitsCounter()
    {
        if (rockUnitsCounter != null)
        {
            rockUnitsCounter.text = GameManager.Instance.rockUnits.ToString();
        }
    }

    public void SetupPlayerHPBar(float maxPlayerHealth, float currentPlayerHealth)
    {
        if (playerPrefab != null)
        {
            playerHPBar.maxValue = maxPlayerHealth;
            playerHPBar.value = currentPlayerHealth;
        }
    }
    public void UpdatePlayerHPBar(float currentPlayerHealth)
    {
        {
            if (playerPrefab != null)
            {
                playerHPBar.value = currentPlayerHealth;
            }
        }
    }
    public void UpdateExperiencePointsDisplay()
    {
        experiencePointsCounter.text = GameManager.Instance.experiencePoints.ToString();
    }
    private void PopulateWeaponList()
    {
        foreach (Weapon weapon in weapons)
        {
            GameObject item = Instantiate(prefab, content);
            // Set the UI elements to display the weapon's image
            // For example:
            item.GetComponentInChildren<TextMeshProUGUI>().text = weapon.weaponName;
            // Set the button's Event Trigger to call the SelectWeapon method with the corresponding weapon as a parameter
            Button button = item.GetComponentInChildren<Button>();
            button.onClick.AddListener(() => inventory.SelectWeapon(weapon));
        }
    }
}