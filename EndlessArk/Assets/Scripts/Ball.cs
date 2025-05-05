using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody m_Rigidbody;

    [Header("Speed Settings")]
    public float BallSpeed = 2.0f; // Initial speed set by MainManager
    public float SpeedIncreaseFactor = 1.01f; // Speed boost after each paddle hit
    public float MaxSpeed = 5.0f; // Cap maximum speed
    public float MinSpeed = 2.0f; // Cap minimum speed
    public float SpeedGrowthMultiplier = 1.05f; // Rate at which speed increase factor grows

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        LaunchBall(); // Launch the ball with an initial direction
    }

    private void LaunchBall()
    {
        // Start with a random horizontal angle, always moving upward
        Vector3 initialDirection = new Vector3(UnityEngine.Random.Range(-1f, 1f), 1, 0).normalized;
        m_Rigidbody.velocity = initialDirection * BallSpeed;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name != "Paddle") return;

        // Calculate bounce direction based on paddle hit position
        Vector3 newDirection = GetBounceDirection(other);

        // Update ball velocity based on new direction
        m_Rigidbody.velocity = newDirection * m_Rigidbody.velocity.magnitude;

        // Apply controlled speed increase
        AdjustSpeed();
    }

    private Vector3 GetBounceDirection(Collision paddleCollision)
    {
        ContactPoint contact = paddleCollision.contacts[0];

        float distanceFromCenter = contact.point.x - paddleCollision.transform.position.x;
        float paddleWidth = paddleCollision.collider.bounds.size.x;

        // Normalize distance to a value between -1 and 1
        float normalized = distanceFromCenter / paddleWidth;

        return new Vector3(normalized, 1, 0).normalized;
    }

    private void AdjustSpeed()
    {
        // Slightly increase the velocity each time
        Vector3 velocity = m_Rigidbody.velocity * SpeedIncreaseFactor;

        // Gradually increase the factor itself over time
        SpeedIncreaseFactor *= SpeedGrowthMultiplier;

        // Clamp speed within min and max values
        float clampedSpeed = Mathf.Clamp(velocity.magnitude, MinSpeed, MaxSpeed);
        m_Rigidbody.velocity = velocity.normalized * clampedSpeed;
    }
}
