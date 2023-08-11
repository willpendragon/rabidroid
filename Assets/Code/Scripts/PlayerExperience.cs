using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
   public void IncreaseExperiencePoints(float gainedExperiencePoints)
    {
        GameManager.Instance.experiencePoints += gainedExperiencePoints;
    }
}

