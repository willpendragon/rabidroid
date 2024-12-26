using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Weather
{
    sunny,
    rain
}
public class WeatherManager : MonoBehaviour
{
    public ParticleSystem rainParticleSystem;

    public Weather currentWeather;
    [SerializeField] float rainTimer;
    [SerializeField] float rainLength;
    [SerializeField] float resetRainTimer;

    void Start()
    {
        if (currentWeather != Weather.rain)
        {
            StartCoroutine("RainCountdown");
        }
    }

    void Update()
    {
        if (currentWeather == Weather.rain)
        {
            DamagePlayer();
        }
    }
    IEnumerator RainCountdown()
    {
        yield return new WaitForSeconds(rainTimer);
        Rain();
    }
    void Rain()
    {
        currentWeather = Weather.rain;
        rainParticleSystem.Play();
        Debug.Log("Rain Started");
        StartCoroutine("ResetRainCountdown");
        StartCoroutine("DamagePlayer");
    }
    IEnumerator ResetRainCountdown()
    {
        yield return new WaitForSeconds(resetRainTimer);
        rainParticleSystem.Stop();
        rainTimer = 5;
        Debug.Log("Rain Stopped");
        currentWeather = Weather.sunny;
        StartCoroutine("RainCountdown");
    }
    void DamagePlayer()
    {
        PlayerHealth playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        playerHealth.currentHealth -= 1 * Time.deltaTime;
        UIManager uiManager = GameObject.FindGameObjectWithTag("GameUI").GetComponent<UIManager>();
        uiManager.UpdatePlayerHPBar(playerHealth.currentHealth);
    }
}