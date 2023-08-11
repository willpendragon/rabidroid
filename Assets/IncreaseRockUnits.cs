using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseRockUnits : MonoBehaviour
{
    public delegate void RockLoot();
    public static event RockLoot OnRockLoot;

    private GameManager gameManager;
    [SerializeField] int lootedRockUnits;
    [SerializeField] float lootDropLifetime;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        Destroy(this.gameObject, lootDropLifetime);
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Collision");
            AddRockUnitsToGameManager();
            OnRockLoot();
            Destroy(this.gameObject, 0.2f);
        }
    }

    // Update is called once per frame
    void AddRockUnitsToGameManager()
    {
        gameManager.IncreaseRockUnits(lootedRockUnits);
    }
}
