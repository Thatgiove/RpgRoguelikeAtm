using UnityEngine;

public class Projectile : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, .09f);
    }


    void Update()
    {
        transform.Translate(Vector2.up * 7 * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(10);
        }
        else
            Destroy(gameObject);
    }
}
