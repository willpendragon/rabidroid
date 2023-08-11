using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    public int attackPower;
    public Sprite weaponIcon;
    public string weaponName;
    public int weaponMaterials;
}