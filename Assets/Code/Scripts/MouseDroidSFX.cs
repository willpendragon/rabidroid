using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDroidSFX : MonoBehaviour
{
    [SerializeField] AudioSource startupSound;
    [SerializeField] AudioSource attackSound;

    // Start is called before the first frame update
    void Start()
    {
        startupSound.Play();
    }
    public void PlayAttackSound()
    {
        attackSound.Play();
    }
}
