using UnityEngine;

public class Player1Controller : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Update()
    {
        float moveX = 0f;
        float moveY = 0f;

        // Handle movement input
        if (Input.GetKey(KeyCode.W)) moveY = 1f;
        if (Input.GetKey(KeyCode.S)) moveY = -1f;
        if (Input.GetKey(KeyCode.A)) moveX = -1f;
        if (Input.GetKey(KeyCode.D)) moveX = 1f;

        // Apply movement directly to the transform
        Vector2 moveDirection = new Vector2(moveX, moveY).normalized;
        transform.position = (Vector2)transform.position + moveDirection * moveSpeed * Time.deltaTime;
    }
}
