using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHolder : MonoBehaviour
{
    public int CollectedKeys;
    public void AddKey()
    {
        CollectedKeys =+ 1;
    }
    public void UseKey()
    {
        CollectedKeys =- 1;
    }
}
