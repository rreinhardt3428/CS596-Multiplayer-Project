using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;         // Bullet prefab to instantiate
    public float bulletSpeed = 10f;         // Speed of the bullet
    public float fireRate = 3f;             // Bullets per second
    public bool shootingEnabled = true;     // Indicates whether the player can shoot

    public string playerTag;                // Player tag (Set in Inspector: "Player1" or "Player2")
    public KeyCode shootKey;                // Custom shoot key for each player

    public AudioClip shootSound;            // Audio clip for shooting sound
    private AudioSource audioSource;        // Reference to the AudioSource

    private float nextFireTime = 0f;        // Time when the next bullet can be fired

    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Get AudioSource component
    }

    void Update()
    {
        if (!shootingEnabled) return;       // Disables firing if shooting is disabled

        if (Input.GetKey(shootKey))  // Check if assigned shoot key is held down
        {
            if (Time.time >= nextFireTime)
            {
                FireBullets();  // Fire 3 bullets at once
                nextFireTime = Time.time + (1f / fireRate); // Calculate next fire time
            }
        }
    }

    void FireBullets()
    {
        // Fire bullets at 3 different directions
        FireBullet(Vector2.right);  // Bullet going straight to the right
        FireBullet(Vector2.right + Vector2.up * 2f);  // Bullet going right and slightly upwards
        FireBullet(Vector2.right + Vector2.down * 2f);  // Bullet going right and slightly downwards
    }

    void FireBullet(Vector2 direction)
    {
        // Instantiate a bullet at the player's position
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();    // Get the Rigidbody2D component 

        if (rb != null)
        {
            // Set the velocity in the specified direction
            rb.velocity = direction.normalized * bulletSpeed;
        }

        // Play shoot sound
        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }
}
