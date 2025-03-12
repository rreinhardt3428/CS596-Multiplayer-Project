using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 5; // Player starting health

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(gameObject.name + " took damage! Health: " + health);

        if (health <= 0)
        {
            Debug.Log(gameObject.name + " has been defeated!");
            Destroy(gameObject); // Replace this with a respawn system if needed
        }
    }
}
