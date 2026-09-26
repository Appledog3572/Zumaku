using UnityEngine;

public class AcceleratingBullet : MonoBehaviour
{
    private float currentSpeed;
    public float acceleration = 4f;
    public float maxSpeed = 10f;
    Rigidbody2D rb;
    private Camera mainCamera;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = rb.linearVelocity.magnitude;
        mainCamera = Camera.main;
    }

    void FixedUpdate()
    {
        Vector3 viewportPos = mainCamera.WorldToViewportPoint(transform.position);
        if (rb.linearVelocity.magnitude < maxSpeed)
        {
            currentSpeed = Mathf.Min(currentSpeed + acceleration * Time.fixedDeltaTime, maxSpeed);
            rb.linearVelocity = rb.linearVelocity.normalized * currentSpeed;
        }
        if(mainCamera != null)
        {
            if (viewportPos.x < 0 || viewportPos.x > 1)
            {
                rb.linearVelocity = new Vector2(-rb.linearVelocity.x, rb.linearVelocity.y);
            }
            if(viewportPos.y < 0 || viewportPos.y > 1)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, -rb.linearVelocity.y);
            }
        }
    }
}
