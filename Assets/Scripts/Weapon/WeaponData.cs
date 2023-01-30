using UnityEngine;

[CreateAssetMenu(fileName = "Weapon_Data", menuName = "ScriptableObjects/WeaponData", order = 1)]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public Sprite image;
    public string type;

    public float level = 1f;
    public float minDamage = 1f;
    public float maxDamage = 10f;
    public float attackTime = 1f;
    public float distance = 1f;
    public float critical = 1f;

    public GameObject projectile;
}
