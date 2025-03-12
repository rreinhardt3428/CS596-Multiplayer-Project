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
        float currentFireRate = fireRate;   // Assigns default fire rate

        if (!shootingEnabled) return;       // Disables firing if shooting is disabled

        if (Input.GetKey(shootKey))  // Check if assigned shoot key is held down
        {
            if (Time.time >= nextFireTime)
            {
                FireBullet();
                nextFireTime = Time.time + (1f / currentFireRate); // Calculate next fire time
            }
        }
    }

    void FireBullet()
    {
        // Instantiate a bullet at the player's position
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();    // Get the Rigidbody component 

        if (rb != null)
        {
            // Set the velocity to move always along the right direction
            rb.velocity = new Vector3(bulletSpeed, 0f, 0f);
        }

        // Set the shooterTag for the bullet
        BulletMovement bulletScript = bullet.GetComponent<BulletMovement>();
        if (bulletScript != null)
        {
            bulletScript.shooterTag = playerTag;
        }

        // Play shoot sound
        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }
}
