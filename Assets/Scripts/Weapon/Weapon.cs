using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] WeaponData weaponData;
    SpriteRenderer sr;
    PlayerController pc;

    private void Start()
    {
        if (weaponData == null) return;
        pc = FindObjectOfType<PlayerController>();
        sr = GetComponent<SpriteRenderer>();
        if (sr && weaponData)
        {
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
