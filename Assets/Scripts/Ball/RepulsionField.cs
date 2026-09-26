using System.Collections.Generic;
using UnityEngine;

public class RepulsionField : MonoBehaviour
{
    public List<float> radiusByPhase = new List<float> { 1.5f, 2f, 2.5f }; // 每個階段的半徑
    public List<float> pushStrength = new List<float> { 20f, 40f, 60f }; // 每個階段的推力
    Ball ball;
    public SpriteRenderer rangeIndicator;
    private int lastPhase = -1;

    void Start()
    {
        ball = GetComponent<Ball>();
    }

    void Update()
    {
        if (ball.currentPhase != lastPhase)
        {
            float radius = radiusByPhase[ball.currentPhase - 1];
            SetVisualDiameter(rangeIndicator, radius * 2f);
            lastPhase = ball.currentPhase;
        }
    }

    void FixedUpdate()
    {
        float radius = radiusByPhase[ball.currentPhase - 1];
        float strength = pushStrength[ball.currentPhase - 1];
        Collider2D[] bullets = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D bullet in bullets)
        {
            if (bullet.CompareTag("Bullet"))
            {
                Vector2 direction = (bullet.transform.position - transform.position).normalized;
                float distance = Vector2.Distance(bullet.transform.position, transform.position);
                float falloff = 1f - (distance / radius);
                bullet.attachedRigidbody.linearVelocity += direction * strength * falloff * Time.deltaTime;
            }
        }
    }

    void SetVisualDiameter(SpriteRenderer sr, float desiredDiameter)
    {
        float nativeDiameter = sr.sprite.bounds.size.x;
        float scaleFactor = desiredDiameter / nativeDiameter;
        sr.transform.localScale = new Vector3(scaleFactor, scaleFactor, 1f);
    }
}