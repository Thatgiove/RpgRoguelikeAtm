using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, .09f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * 7 * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            player.HandleDamage(10);
        }
        else
            Destroy(gameObject);
    }
}
