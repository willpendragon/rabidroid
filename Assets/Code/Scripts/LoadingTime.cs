using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingTime : MonoBehaviour
{
    [SerializeField] SpriteRenderer tentSprite;
    [SerializeField] Slider loadingSlider;
    public float countdownDuration = 5f;
    private float elapsedTime = 0f;

    public void Start()
    {
        tentSprite.color = Color.black;
        Debug.Log("Tent Object Instantiated");
        loadingSlider.maxValue = countdownDuration;
        loadingSlider.value = countdownDuration;
        StartCoroutine("ConstructionDelay");
    }
    IEnumerator ConstructionDelay()
    {
        while (elapsedTime < countdownDuration)
        {
            elapsedTime += Time.deltaTime;
            loadingSlider.value = loadingSlider.maxValue - (elapsedTime / countdownDuration);
            yield return null;
        }
        loadingSlider.value = 0;
        FinishTent();
    }
    public void FinishTent()
    {
        tentSprite.color = Color.white;
    }
}
