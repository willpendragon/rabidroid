using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
[SerializeField] string sceneName;

void OnTriggerEnter (Collider other)
{
    if (other.tag == "PlayerModel")
    {
        ChangeScene();
    }
}
public void ChangeScene()
{
    SceneManager.LoadScene(sceneName);
}
}
