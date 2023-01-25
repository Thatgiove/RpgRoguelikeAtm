using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerController>();
        if (player == null) return;

        var enemy = gameObject.GetComponentInParent(typeof(Enemy)) as Enemy;
        if(enemy == null) return;

        if (gameObject.name == "attackArea")
        {
            enemy.Attack();

        }
        else if (gameObject.name == "viewArea")
        {
            enemy.Follow();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerController>();
        if (player == null) return;

        var enemy = gameObject.GetComponentInParent(typeof(Enemy)) as Enemy;
        if (enemy == null) return;

        if (gameObject.name == "attackArea")
        {
            enemy.StopAttack();

        }
        else if (gameObject.name == "viewArea")
        {
            enemy.StopFollow();
        }
    }
}
