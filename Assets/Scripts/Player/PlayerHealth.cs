using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("Laser"))
        {
            currentHealth--;
            Destroy(collision.gameObject);
            if (currentHealth <= 0)
            {
                Debug.Log("Player is dead");
            }
        }
    }
}
