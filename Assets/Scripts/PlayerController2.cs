using UnityEngine;

public class Player2Controller : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Update()
    {
        float moveX = 0f;
        float moveY = 0f;

        // Handle movement input
        if (Input.GetKey(KeyCode.UpArrow)) moveY = 1f;
        if (Input.GetKey(KeyCode.DownArrow)) moveY = -1f;
        if (Input.GetKey(KeyCode.LeftArrow)) moveX = -1f;
        if (Input.GetKey(KeyCode.RightArrow)) moveX = 1f;

        // Apply movement directly to the transform
        Vector2 moveDirection = new Vector2(moveX, moveY).normalized;
        transform.position = (Vector2)transform.position + moveDirection * moveSpeed * Time.deltaTime;
    }
}
