using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] List<ItemAugment> augmentations = new List<ItemAugment>();
    public List<Weapon> weapons = new List<Weapon>();

    public Weapon selectedWeapon;

    // Start is called before the first frame update
    public void CreateWeapon(int attackPower)
    {
        Weapon newSword = ScriptableObject.CreateInstance<Weapon>();
        newSword.attackPower = 200;
    }

    public void SelectWeapon(Weapon weapon)
    {
        Debug.Log("Weapon Selected");
        GameManager.Instance.rockUnits -= weapon.weaponMaterials;
        selectedWeapon = weapon;
        //Takes a list of Weapon
        //Clicking on the button adds the weapon
        EquipWeapon();
    }

    public void EquipWeapon()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        SwordAttack playerSword = player.GetComponent<SwordAttack>();
        playerSword.equippedWeapon = selectedWeapon;
    }
}
