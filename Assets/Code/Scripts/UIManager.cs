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
private static UIManager _instance;
public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            Debug.LogError("UIManager is Null.");

            return _instance;
        }
    }
void Awake()
{
    _instance = this;
    DontDestroyOnLoad(this);
    playerHPBar.value = playerPrefab.GetComponent<PlayerHealth>().playerHP;
    playerHPBar.maxValue = playerPrefab.GetComponent<PlayerHealth>().playerHP;
   
}
void Start()
{
    Inventory inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
    weapons = inventory.weapons;
    PopulateWeaponList();
    UpdateRockUnitsCounter();
    UpdatePlayerHPBar();
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
public void UpdatePlayerHPBar()
{
    {
        playerHPBar.value = playerPrefab.GetComponent<PlayerHealth>().playerHP;
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