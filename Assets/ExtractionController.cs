using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtractionController : MonoBehaviour
{
    private enum MiningPointStatus
    {
        Minable,
        NotMinable
    }

    private enum MiningPointCoolDownStatus

    {
        MiningCooldownActive,
        MiningCooldownNotActive
    }


    [SerializeField] float extractionTimer;
    [SerializeField] int rocksReward;

    private MiningPointStatus miningPointStatus;
    private MiningPointCoolDownStatus miningPointCoolDownStatus;
    private void Start()
    {
        miningPointStatus = MiningPointStatus.NotMinable;
        miningPointCoolDownStatus = MiningPointCoolDownStatus.MiningCooldownNotActive;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.tag == "Player")
        {
            miningPointStatus = MiningPointStatus.Minable;
            Debug.Log("Allow Mining for Player");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other != null && other.tag == "Player")
        {
            miningPointStatus = MiningPointStatus.NotMinable;
            Debug.Log("Disable Mining for Player");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && miningPointStatus == MiningPointStatus.Minable && miningPointCoolDownStatus != MiningPointCoolDownStatus.MiningCooldownActive)
        {
            StartCoroutine(Mining());
            Debug.Log("Mining Crystal");
        }
    }

    IEnumerator Mining()
    {
        miningPointCoolDownStatus = MiningPointCoolDownStatus.MiningCooldownActive;
        yield return new WaitForSeconds(extractionTimer);
        IncreaseRockUnits();
        miningPointCoolDownStatus = MiningPointCoolDownStatus.MiningCooldownNotActive;
    }

    private void IncreaseRockUnits()
    {
        if (miningPointStatus != MiningPointStatus.NotMinable)
        {
            GameManager.Instance.rockUnits += rocksReward;
            UIManager.Instance.UpdateRockUnitsCounter();
        }
    }
}

