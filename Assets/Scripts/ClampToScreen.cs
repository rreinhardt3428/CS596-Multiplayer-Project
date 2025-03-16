using UnityEngine;

public class ClampToScreen : MonoBehaviour
{
    private Camera mainCamera;
    private SpriteRenderer spriteRenderer;
    private float halfWidth;
    private float halfHeight;

    void Start()
    {
        // Cache references
        mainCamera = Camera.main;
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            // Calculate half the size of the sprite in world units
            halfWidth = spriteRenderer.bounds.extents.x / 2;
            halfHeight = spriteRenderer.bounds.extents.y / 2;
        }
    }

    void Update()
    {
        // Get the current position of the object in world space
        Vector3 position = transform.position;

        // Calculate screen boundaries in world units
        Vector3 screenBoundsMin = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)); // Bottom-left corner
        Vector3 screenBoundsMax = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0)); // Top-right corner

        // Clamp the position while accounting for the sprite's size
        float clampedX = Mathf.Clamp(position.x, screenBoundsMin.x + halfWidth, screenBoundsMax.x - halfWidth);
        float clampedY = Mathf.Clamp(position.y, screenBoundsMin.y + halfHeight, screenBoundsMax.y - halfHeight);

        // Set the clamped position back to the object
        transform.position = new Vector3(clampedX, clampedY, position.z);
    }
}
