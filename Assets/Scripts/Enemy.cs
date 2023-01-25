using Pathfinding;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    GameObject player;
    [SerializeField] float health = 50;
    float minHealth = 0;
    float maxHealth = 50;
    [SerializeField] bool isDead;
    [SerializeField] Image fillBar;
    [SerializeField] GameObject enemyProjectile; //TODO rimuovere 

    bool follow;
    bool attacking;
    private float attackTime = 0.0f;
    private float nextActionTime = 1f;
    public float period = 1f;

    GameObject rotationAxes;

    public float radius = 20;

    IAstarAI ai;



    Vector3 PickRandomPoint()
    {
        var point = Random.insideUnitSphere * radius;

        point.y = 0;
        point += ai.position;
        return point;
    }


    void Start()
    {
        ai = GetComponent<IAstarAI>();
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerController>()?.gameObject;

        rotationAxes = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        if (fillBar)
            fillBar.GetComponent<Image>().fillAmount = NormalizedHealth(health);

        // Update the destination of the AI if
        // the AI is not already calculating a path and
        // the ai has reached the end of the path or it has no path at all
        //if (!ai.pathPending && (ai.reachedEndOfPath || !ai.hasPath))
        //{
        //    ai.destination = PickRandomPoint();
        //    ai.SearchPath();
        //}

        if (attacking)
        {
            attackTime += Time.deltaTime;

            if (attackTime > nextActionTime)
            {
                nextActionTime += period;
                Shoot();
            }


        }
    }
    void LateUpdate()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        
        //if (follow)
        //{
        //    rb.MovePosition(transform.position + direction * 2f * Time.deltaTime);
        //}

        if (attacking)
        {
            if(enemyProjectile && rotationAxes)
            {
                
                Quaternion toRot = Quaternion.LookRotation(Vector3.forward, direction);
                rotationAxes.transform.rotation = Quaternion.RotateTowards(rotationAxes.transform.rotation, toRot, 720 * Time.deltaTime);
                
                attackTime += Time.deltaTime;
                if (attackTime > nextActionTime)
                {
                    nextActionTime += period;
                    Shoot();
                }

            }
        }

    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0) 
            Destroy(gameObject);
    }

    //health normalizzata range 0 - 1
    float NormalizedHealth(float health)
    {
        return (health - minHealth) / (maxHealth - minHealth);
    }

    public void Attack()
    {
        attacking = true;
    }
    public void StopAttack()
    {
        attacking = false;
        attackTime = 0;
        nextActionTime = period;
    }
    public void Follow()
    {
        follow = true;

    }
    public void StopFollow()
    {
        follow = false;
    }

    void Shoot()
    {
        if (enemyProjectile && attacking)
        {
            Instantiate(enemyProjectile, (rotationAxes.transform.position + rotationAxes.transform.up), rotationAxes.transform.rotation);
        }
    }
}
