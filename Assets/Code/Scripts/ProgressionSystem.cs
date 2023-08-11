using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class LevelUpEvent : UnityEvent<int> {}

public class ProgressionSystem : MonoBehaviour
{
    public LevelUpEvent onLevelUp;
    public int level = 1;
    public int exp = 0;

    private const int BASE_EXP_REQUIRED = 100;
    private const float EXP_FACTOR = 1.25f;

    public void AddExperience(int expToAdd)
    {
        exp += expToAdd;
        int expToNextLevel = GetExpToNextLevel(level);
        while (exp >= expToNextLevel)
        {
            exp -= expToNextLevel;
            level++;
            expToNextLevel = GetExpToNextLevel(level);

            onLevelUp.Invoke(level);
        }
    }

    private static int GetExpToNextLevel(int level)
    {
        return (int)(BASE_EXP_REQUIRED * Mathf.Pow(level, EXP_FACTOR));
    }
}
