using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollector : MonoBehaviour
{
    // Start is called before the first frame update
    
    void OnTriggerEnter (Collider other)
    {
        if (other.tag == "PlayerModel")
        {
            Debug.Log("Collision");
            other.GetComponent<KeyHolder>().AddKey();
            StartCoroutine("DestroyLootedObject");
        }
    }

    IEnumerator DestroyLootedObject()
    {
        yield return new WaitForSeconds(0.0001f);
        Destroy(this.gameObject);
        
    }
}
