using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherMaterial : MonoBehaviour
{

[SerializeField] GameObject playerPrefab;
void OnTriggerEnter2D(Collider2D other)
{
    GameManager.Instance.gatheringModeIsActive = true;
    //Debug.Log("Collision Detected"); 
}
void Update()
{
    if (Input.GetButtonDown("Fire1") && GameManager.Instance.gatheringModeIsActive == true
    && GameManager.Instance.vehicleModeIsActive != true)
    {
        MineResource();
    }   
}
void OnTriggerExit2D(Collider2D other)
{
    Animator playerAnim = playerPrefab.GetComponent<Animator>();
    playerAnim.SetBool("Mining", false);
    GameManager.Instance.gatheringModeIsActive = false;
}
void MineResource()
{
    Animator playerAnim = playerPrefab.GetComponent<Animator>();
    playerAnim.SetBool("Mining", true);
    GameManager.Instance.rockUnits += 1;
    StartCoroutine("Cooldown");
    UIManager.Instance.UpdateRockUnitsCounter();
}
IEnumerator Cooldown()
{
    Animator playerAnim = playerPrefab.GetComponent<Animator>();
    yield return new WaitForSeconds(0.1f);
    playerAnim.SetBool("Mining", false);
}
}
