using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement2 : MonoBehaviour
{
    public float bulletSpeed = 10f;  // Bullet speed
    public int damage = 1;

    void Update()
    {
        transform.position += Vector3.left * bulletSpeed * Time.deltaTime;  // Move bullet right
        Destroy(gameObject, 3f);  // Destroy bullet after 3 seconds
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Assuming you want to check if the bullet hits the opposite player.
        if (other.CompareTag("Player 1"))  // Check if the bullet hits Player1 or Player2
        {
            PlayerHealthAndShield player = other.GetComponent<PlayerHealthAndShield>();
            if (player != null)
            {
                player.TakeDamage(damage); // Call TakeDamage() from PlayerHealthAndShield
            }

            Destroy(gameObject); // Destroy bullet on impact
        }
    }
}
