using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Ereditata sia da player che enemy
public class Character : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] GameObject fillBar;
    float minHealth = 0;
    float maxHealth = 100;
    
    void Start()
    {
        health = maxHealth;
    }

    virtual protected void Update()
    {
        if (fillBar)
        {
            fillBar.GetComponent<Image>().fillAmount = NormalizedHealth(health);
        }
            
    }

    protected void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            print("DIE!");
            //Destroy(gameObject);
        }
            
    }

    //health normalizzata range 0 - 1
    protected float NormalizedHealth(float health)
    {
        return (health - minHealth) / (maxHealth - minHealth);
    }
}
