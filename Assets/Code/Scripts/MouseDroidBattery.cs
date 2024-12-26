using System.Collections;
using UnityEngine;

public class MouseDroidBattery : MonoBehaviour
{
    public float totalBatteryLife = 10f;
    [SerializeField] private float currentBatteryLife;
    [SerializeField] private MouseDroidBehaviour mouseDroidBehaviour;
    [SerializeField] float batteryDepletionFactor;
    [SerializeField] float batteryRechargeFactor;

    private Coroutine batteryDepletionCoroutine;
    private Coroutine batteryRechargeCoroutine;

    private void Start()
    {
        currentBatteryLife = totalBatteryLife;
    }

    public void TriggerBatteryDepletion()
    {
        if (batteryRechargeCoroutine != null)
        {
            StopCoroutine(batteryRechargeCoroutine);
            batteryRechargeCoroutine = null;
        }

        if (batteryDepletionCoroutine == null)
        {
            batteryDepletionCoroutine = StartCoroutine(BatteryDepletion());
        }
    }

    public void TriggerBatteryRecharge()
    {
        if (batteryDepletionCoroutine != null)
        {
            StopCoroutine(batteryDepletionCoroutine);
            batteryDepletionCoroutine = null;
        }
        if (batteryRechargeCoroutine == null)
        {
            batteryRechargeCoroutine = StartCoroutine(BatteryRecharge());
        }
    }

    private IEnumerator BatteryDepletion()
    {
        if (mouseDroidBehaviour.currentDroidState == DroidMentalState.aggressive)
        {
            while (currentBatteryLife > 0)
            {
                yield return new WaitForSeconds(1f);
                currentBatteryLife -= batteryDepletionFactor;
                Debug.Log($"Battery Depleting: {currentBatteryLife}/{totalBatteryLife}");

                if (currentBatteryLife <= 0)
                {
                    Debug.Log("RabiDroid Battery has depleted");
                    mouseDroidBehaviour.Die();
                }
            }
        }
        batteryDepletionCoroutine = null;
    }

    private IEnumerator BatteryRecharge()
    {
        if (mouseDroidBehaviour.currentDroidState == DroidMentalState.peaceful)
        {
            while (currentBatteryLife < totalBatteryLife)
            {
                yield return new WaitForSeconds(1f);
                currentBatteryLife += batteryRechargeFactor;
                Debug.Log($"Recharging RabiDroid Battery: {currentBatteryLife}/{totalBatteryLife}");
            }

            Debug.Log("RabiDroid Battery has been fully recharged");
        }
        batteryRechargeCoroutine = null;
    }
}
