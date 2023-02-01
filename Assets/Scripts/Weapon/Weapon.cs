using System.Linq;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    WeaponData weaponData;
    SpriteRenderer sr;
    PlayerController pc;

    private void Start()
    {
        pc = FindObjectOfType<PlayerController>();
        sr = GetComponent<SpriteRenderer>();
        
        //TODO test carico tutte le prefab e le assegno a caso alle armi
        //valutare se fare un manager
        WeaponData[] wd = Resources.LoadAll("Weapons", typeof(WeaponData)).Cast<WeaponData>().ToArray();

        if (wd.Length > 0 && sr)
        {
            weaponData = wd[Random.Range(0, wd.Length)];
            sr.sprite = weaponData.image;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        var player = collision.gameObject.GetComponent<PlayerController>();
        if (player)
        {
            if(player.weapon == this)
            {
                return;
            }
            else
            {
                if(player.weapon != null)
                {
                    player.weapon.sr.enabled = true;
                }
                sr.enabled = false;
                print(weaponData.weaponName);
                player.weapon = this;
            }

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //var player = collision.gameObject.GetComponent<PlayerController>();
        //if (player)
        //{
        //    player.weapon = null;
        //}
    }

    public void Attack()
    {
        print(weaponData.maxDamage);
        if (weaponData.projectile)
            Instantiate(weaponData.projectile, pc.transform.GetChild(0).transform.position + (pc.transform.GetChild(0).transform.up * 1.2f), pc.transform.GetChild(0).transform.rotation);
    }
}
