using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI HealthText;

    Animator anim;
    Rigidbody2D rb;

    float moveSpeed = 6;
    int maxHealth = 100;
    int currentHealth;

    bool dead = false;

    float moveHorizontal, moveVertical;
    Vector2 movement;

    int facingDirection = 1;  // 1 = right, -1 = left
    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        currentHealth = maxHealth;
        HealthText.text = maxHealth.ToString();
    }

    private void Update()
    {
         // if (Input.GetKeyDown(KeyCode.Space))
          //  Hit(10);
        
        if (dead)
        {
            movement = Vector2.zero;
            anim.SetFloat("velocity", 0);
            return;
        }
    

    moveHorizontal = Input.GetAxisRaw("Horizontal");
    moveVertical = Input.GetAxisRaw("Vertical");

        movement = new Vector2(moveHorizontal, moveVertical).normalized;

        anim.SetFloat("velocity", movement.magnitude);

        if(movement.x !=0)
            facingDirection = movement.x > 0 ? 1 : -1;

        transform.localScale = new Vector2(facingDirection, 1);
}

private void FixedUpdate()
{
    rb.velocity = movement * moveSpeed;
}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();

        if (enemy != null)
            Hit(5);
    }

    private void Hit(int damage)
    {
        anim.SetTrigger("hit");
        currentHealth -= damage;
        HealthText.text = Mathf.Clamp(currentHealth, 0, maxHealth).ToString();

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        dead = true;
    }

}

