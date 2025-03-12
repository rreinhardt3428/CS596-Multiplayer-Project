using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement2 : MonoBehaviour
{
    public float bulletSpeed = 10f;  // Bullet speed
    public string shooterTag;        // To prevent self-hit

    void Update()
    {
        transform.position += Vector3.left * bulletSpeed * Time.deltaTime;  // Move bullet right
        Destroy(gameObject, 3f);  // Destroy bullet after 3 seconds
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(shooterTag)) return; // Ignore collisions with the shooter

        PlayerHealth player = other.GetComponent<PlayerHealth>(); // Check if it's a player
        if (player != null)
        {
            player.TakeDamage(1); // Apply damage
            Destroy(gameObject);  // Destroy bullet on hit
        }
    }
}

