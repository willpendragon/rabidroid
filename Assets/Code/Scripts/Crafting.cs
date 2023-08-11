using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    [SerializeField] GameObject spawnedObject;
    [SerializeField] GameObject spawnedRobot;
    [SerializeField] GameObject spawnedTent;
    [SerializeField] Vector3 spawnOffset;
    [SerializeField] int carCraftingPrice;
    [SerializeField] int droidCraftingPrice;
    [SerializeField] int tentCraftingPrice;

    private Vector2 playerPosition;

    public void CraftCar()
    {
        if (GameManager.Instance.rockUnits >= carCraftingPrice)
        {
            //    Debug.Log("Crafted Car");
            //CheckPlayerModelPosition();
            GameManager.Instance.rockUnits -= 20;
            UIManager.Instance.UpdateRockUnitsCounter();
        }
        else
        {
            //    Debug.Log("Not enough resources");
        }
    }
    public void CraftRobot()
    {
        if (GameManager.Instance.rockUnits >= droidCraftingPrice)
        {
            //    Debug.Log("Crafted Robot");
            CheckPlayerModelPosition(spawnedRobot);
            //Instantiate(spawnedRobot, playerPosition, Quaternion.identity);
            GameManager.Instance.rockUnits -= droidCraftingPrice;
            UIManager.Instance.UpdateRockUnitsCounter();
        }
        else
        {
            //    Debug.Log("Not enough resources");
        }
    }
    public void CraftAugmentation()
    {
        //
    }
    public void CraftTent()
    {
        if (GameManager.Instance.rockUnits >= tentCraftingPrice)
        {
            CheckPlayerModelPosition(spawnedTent);
            GameManager.Instance.rockUnits -= tentCraftingPrice;
            UIManager.Instance.UpdateRockUnitsCounter();
            Debug.Log("Crafting Tent");
        }
        else
        {
            //    Debug.Log("Not enough resources");
        }
    }
    public void CheckPlayerModelPosition(GameObject spawnedPrefab)
    {
        Transform playerTransform = GameObject.FindGameObjectWithTag("PlayerModel").GetComponent<Transform>();
        Vector3 spawnPosition = playerTransform.position;
        Instantiate(spawnedPrefab, spawnPosition + spawnOffset, Quaternion.identity);
    }
}
