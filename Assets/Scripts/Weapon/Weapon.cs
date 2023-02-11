using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] bool equipped = false;
    [SerializeField] WeaponData weaponData;
    WeaponManager wm;
    PlayerController pc;

    private void Start()
    {
        pc = FindObjectOfType<PlayerController>();
        wm = FindObjectOfType<WeaponManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerController>();

        if (player)
        {
            if (player.weapon != null) 
            {
                Drop(player.weapon);
            }
            player.weapon = this;
            equipped = true;
            wm.ToggleWeaponVisibility(this, false);
        }
    }

    //droppa l'arma
    void Drop(Weapon w)
    {
        w.transform.position = new Vector3(
            pc.transform.position.x + 1.5f, 
            pc.transform.position.y ,
            pc.transform.position.z);

        wm.ToggleWeaponVisibility(w, true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //var player = collision.gameObject.GetComponent<PlayerController>();
        //if (player)
        //{
        //    player.weapon = null;
        //}
    }
    public void SetWeaponData(WeaponData wd)
    {
        weaponData = wd;
    }
    public void Attack()
    {
        print(weaponData.weaponName);
        if (weaponData.projectile)
            Instantiate(weaponData.projectile, pc.transform.GetChild(0).transform.position + (pc.transform.GetChild(0).transform.up * 1.2f), pc.transform.GetChild(0).transform.rotation);
    }
}
