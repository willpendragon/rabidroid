using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteControl : MonoBehaviour
{
    public delegate void AggressiveModeActivated();
    public static event AggressiveModeActivated OnAggressiveModeActivated;
    public delegate void PeacefulModeActivated();
    public static event PeacefulModeActivated OnPeacefulModeActivated;

    GameObject[] droidPrefabs;
    [SerializeField] string droidTag;
    [SerializeField] AudioSource remoteAggroSound;
    [SerializeField] AudioSource peacefulSound;
    GameObject droidPrefab;
    [SerializeField] ParticleSystem remoteCommandVFX;
    public void CommandAggressiveMode()
    {
        OnAggressiveModeActivated();
        remoteCommandVFX.Play();
        Debug.Log("Commanded RabiDroids to switch to Aggressive Mode");
    }
    public void CommandPeacefulMode()
    {
        OnPeacefulModeActivated();
        remoteCommandVFX.Play();
        Debug.Log("Commanded RabiDroids to switch to Peaceful Mode");

    }
}
