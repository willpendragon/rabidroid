using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    private static GameManager _instance;
    public int rockUnits;
    public float fuelUnits;
    public float experiencePoints;
    public bool vehicleModeIsActive;
    public bool gatheringModeIsActive;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            Debug.LogError("Game Manager is Null.");

            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(this);
    }

    public void GameManagerTest()
    {
        Debug.Log("Game Manager Test");
    }
    public void IncreaseResource(float resourceQuantity, string resourceType)
    {
        if (resourceType == "Fuel")
        {
            fuelUnits += resourceQuantity;
        }

    }

    public void IncreaseRockUnits(int rockUnitsIncrement)
    {
        rockUnits += rockUnitsIncrement;
    }
    void Start()
    {
        Application.targetFrameRate = 60; // or another desired frame rate
    }
}
