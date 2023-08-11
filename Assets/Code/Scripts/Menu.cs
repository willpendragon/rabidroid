using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
[SerializeField] GameObject craftingMenu;
[SerializeField] GameObject remoteControlMenu;

void Update()
{
    if (Input.GetButtonDown("Fire2"))
    {
        OpenCraftingMenu();
    }
    if (Input.GetButtonDown("Fire3"))
    {
        OpenRemoteControl();
    }   
}
void OpenCraftingMenu()
{
    craftingMenu.SetActive(true);
}
void OpenRemoteControl()
{
    remoteControlMenu.SetActive(true);
}
}
