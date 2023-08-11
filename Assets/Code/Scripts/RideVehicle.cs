using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RideVehicle : MonoBehaviour
{
private GameObject playerPrefab;
private bool vehicleIsMoving;

void Awake()
{
    playerPrefab = GameObject.FindGameObjectWithTag("Player");
}
void OnTriggerEnter2D(Collider2D other)
{
    GameManager.Instance.vehicleModeIsActive = true;
    //Debug.Log("Collision Detected"); 
}
void Update()
{
    if (Input.GetButtonDown("Fire1") && GameManager.Instance.vehicleModeIsActive == true && GameManager.Instance.gatheringModeIsActive != true)
    {
        StartVehicle();
    }
    else if (Input.GetButtonDown("Fire1") && vehicleIsMoving == true)
    {
        StopVehicle();
    }
}
void OnTriggerExit2D(Collider2D other)
{
    GameManager.Instance.vehicleModeIsActive = false;
}
void StartVehicle()
{
    Transform playerTransform = playerPrefab.GetComponent<Transform>();
    this.gameObject.transform.SetParent(playerTransform);
    playerPrefab.GetComponent<Movement>().speed = 40;
    vehicleIsMoving = true;
    //GameManager.Instance.RockUnits += 1;
    //UIManager.Instance.UpdateRockUnitsCounter();
}
void StopVehicle()
{
    Transform playerTransform = playerPrefab.GetComponent<Transform>();
    playerTransform.DetachChildren();
    playerPrefab.GetComponent<Movement>().speed = 5;
    vehicleIsMoving = false;
}
}
