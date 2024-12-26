using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum MiningState
{
    active,
    inactive
}
public class Gathering : MonoBehaviour

{
    [SerializeField] GameObject raycastOrigin;
    [SerializeField] float maxDistance;
    [SerializeField] Animator weaponAnimator;
    [SerializeField] GameManager gameManager;
    [SerializeField] UIManager uiManager;
    [SerializeField] float miningCooldown; 
    [SerializeField] MiningState miningState;
    [SerializeField] ParticleSystem miningParticle;
    [SerializeField] AudioSource miningAudio;
    [SerializeField] float radius = 0.5f;
    
    void Awake()
    {
        miningState = MiningState.inactive;
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && miningState == MiningState.inactive)
        {
            Debug.Log("Looking for Extraction Point");
            Ray r = new Ray(raycastOrigin.transform.position, raycastOrigin.transform.forward);
            Debug.DrawRay(raycastOrigin.transform.position, raycastOrigin.transform.forward, color:Color.blue);
            if (Physics.Raycast(r, out RaycastHit hit, radius))
            {
                if (hit.collider.tag == "ExtractionPoint")
                {
                    Debug.Log("Found Extraction Point");
                    miningParticle.Play();
                    miningAudio.Play();
                    miningState = MiningState.active;
                    weaponAnimator.SetBool("mining", true);
                    //int extractedResource = hit.collider.gameObject.GetComponent<ExtractionPoint>().availableResource;
                    //gameManager.rockUnits += extractedResource;
                    uiManager.UpdateRockUnitsCounter();
                    StartCoroutine("MiningCooldown");
                }
            }
        }
    }
    IEnumerator MiningCooldown()
    {
        Debug.Log("Mining Cooldown");
        yield return new WaitForSeconds(miningCooldown);
        miningState = MiningState.inactive;
        weaponAnimator.SetBool("mining", false);
        miningParticle.Stop();
    }
}
