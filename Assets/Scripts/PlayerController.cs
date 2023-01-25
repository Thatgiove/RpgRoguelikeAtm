using System;
using UnityEngine;

public class PlayerController : Character
{
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject level;

   
    Rigidbody2D rb;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //InvokeRepeating("Shoot", 1.0f, 1f);
        var d = FindObjectOfType<DungeonManager>();
        if (d)
        {
            transform.position = d.spawnPoint;
        }

    }

    override protected void Update()
    {
        base.Update();
        HandleMovement();
        HandleAttack();
    }

    void HandleMovement()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaY = Input.GetAxis("Vertical") * speed;
        
        var movement = new Vector2(deltaX, deltaY);

        if(movement != Vector2.zero)
        {
            Quaternion toRot = Quaternion.LookRotation(Vector3.forward, movement);
            transform.GetChild(0).transform.rotation = Quaternion.RotateTowards(transform.GetChild(0).transform.rotation, toRot, 720 * Time.deltaTime);
        }
       
        rb.velocity = movement;
    }
    void HandleAttack()
    {
        if ((Input.GetMouseButtonDown(0)))
        {
            Shoot();
        }
    }
    void Shoot()
    {
        //Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //worldPosition.z= 0;
        //print(worldPosition);

        if (projectile)
        {
            Instantiate(projectile, transform.GetChild(0).transform.position + (transform.GetChild(0).transform.up * 1.2f ), transform.GetChild(0).transform.rotation);

        }
    }

    public void HandleDamage(float damage)
    {
        TakeDamage(damage);
    }
}
