using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDroidBattery : MonoBehaviour
{
    public float totalBatteryLife = 10f;
    private float currentBatteryLife;
    [SerializeField] MouseDroidBehaviour mouseDroidBehaviour;
    // Start is called before the first frame update
    void Start()
    {   
        currentBatteryLife = totalBatteryLife;
        StartCoroutine(BatteryDepletion());        
    }

    private IEnumerator BatteryDepletion()
    {
        while (currentBatteryLife > 0)
        {
            yield return new WaitForSeconds(1f);
            currentBatteryLife--;
            Debug.Log(currentBatteryLife);
            Debug.Log("MouseDroid Battery Depleting");
        }
            Debug.Log("Battery has depleted");
            mouseDroidBehaviour.Die();
    }
}
