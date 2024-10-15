using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]

    [SerializeField] int maxHealth = 100;
    [SerializeField] float speed = 1f;

    [Header("Charger")]

    [SerializeField] bool isCharger;

    [SerializeField] float distanceToCharge = 5f;
    [SerializeField] float chargeSpeed = 12f;
    [SerializeField] float prepareTime = 2f;

    bool isCharging = false;
    bool isPreparingCharge = false;


    private int currentHealth;

    Animator anim;
    Transform target;
    void Start()
    {
        currentHealth = maxHealth;
        target = GameObject.Find("Player").transform;
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (!WaveManager.Instance.WaveRunning()) return;
        if (isPreparingCharge) return;
        
        if (target != null)
        {
            Vector3 direction = target.position - transform.position;
            direction.Normalize();

            transform.position += direction * speed * Time.deltaTime;

            var playerToTheRight = target.position.x > transform.position.x;
            transform.localScale = new Vector2(playerToTheRight ? -1 : 1, 1);

            if (isCharger &&
                !isCharging &&
                Vector2.Distance(transform.position, target.position) < distanceToCharge)
            {
                isPreparingCharge = true;
                Invoke("StartCharing", prepareTime);
            }
        }

    }

      void StartCharging()
    {
        isPreparingCharge = false;
        isCharging = true;
        speed = chargeSpeed;
    }
    
        public void Hit(int damage)
        {
        currentHealth -= damage;
        anim.SetTrigger("hit");

        if (currentHealth <= 0)
            Destroy(gameObject);
        }
    
    
    }

