using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : MonoBehaviour
{

public GameObject collectibleItem;
private bool itemsIsCollectible;

void OnTriggerEnter2D (Collider2D other)
{
    if (other.gameObject.tag == "Collectible")
    {
        collectibleItem = other.gameObject;
        itemsIsCollectible = true;
    }
}
void OnTriggerExit2D (Collider2D other)
{
    if (other.gameObject.tag == "Collectible")
    {
        collectibleItem = other.gameObject;
        itemsIsCollectible = false;
    }
}

void Update()
{
    if (Input.GetButtonDown("Fire1") && itemsIsCollectible == true)
    {
        AddItem();
    }
}
void AddItem()
{
    float resourceQuantity = collectibleItem.gameObject.GetComponent<ItemStats>().resourceQuantity;
    string resourceType = collectibleItem.gameObject.GetComponent<ItemStats>().resourceType;
    GameManager.Instance.IncreaseResource(resourceQuantity, resourceType);
    resourceQuantity -= 1;
    CheckQuantity(resourceQuantity);
}
void CheckQuantity(float resources)
{
    if (resources <= 0)
    {
        itemsIsCollectible = false;
        StartCoroutine("DestroyItem");
    }
}

IEnumerator DestroyItem()
{
    yield return new WaitForSeconds(0.5f);
    Destroy(collectibleItem);

}
}
