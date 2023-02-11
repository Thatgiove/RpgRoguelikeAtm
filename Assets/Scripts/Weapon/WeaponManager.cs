using System.Linq;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] GameObject weaponTemplate;
    WeaponData[] weaponDataList;

    void Start()
    {
        //carico tutte le prefab e le assegno casualmente alle armi
        weaponDataList = Resources.LoadAll("Weapons", typeof(WeaponData)).Cast<WeaponData>().ToArray();
    }

    public void GenerateWeapon(Vector3 pos)
    {
        if (weaponDataList.Length > 0 && weaponTemplate)
        {
            var weapon = Instantiate(weaponTemplate, pos, Quaternion.identity);
            var randomWeaponData = weaponDataList[Random.Range(0, weaponDataList.Length)];

            weapon.GetComponent<Weapon>().SetWeaponData(randomWeaponData);
            weapon.GetComponent<SpriteRenderer>().sprite = randomWeaponData.image;
            weapon.transform.parent = transform;
        }
    }

    public void ToggleWeaponVisibility(Weapon _weapon, bool value)
    {
        _weapon.GetComponent<SpriteRenderer>().enabled = value;
    }

    void Update()
    {

    }
}
