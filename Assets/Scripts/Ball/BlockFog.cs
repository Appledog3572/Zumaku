using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockFog : MonoBehaviour
{
    public List<float> diameterByPhase = new List<float> { 2f, 4f, 6f }; // 每個階段的直徑
    Ball ball;
    public SpriteRenderer fogSprite;
    public float onDuration = 2f;
    public float offDuration = 2f;
    private int lastPhase = -1;

    void Start()
    {
        ball = GetComponent<Ball>();
        StartCoroutine(ToggleFog());
    }

    private void Update()
    {
        if (ball.currentPhase != lastPhase)
        {
            float diameter = diameterByPhase[ball.currentPhase - 1];
            SetVisualDiameter(fogSprite, diameter);
            lastPhase = ball.currentPhase;
        }
    }

    void SetVisualDiameter(SpriteRenderer sr, float desiredDiameter)
    {
        float nativeDiameter = sr.sprite.bounds.size.x;
        float scaleFactor = desiredDiameter / nativeDiameter;
        sr.transform.localScale = new Vector3(scaleFactor, scaleFactor, 1f);
    }

    IEnumerator ToggleFog()
    {
        while (true)
        {
            fogSprite.enabled = true;
            yield return new WaitForSeconds(onDuration);
            fogSprite.enabled = false;
            yield return new WaitForSeconds(offDuration);
        }
    }
}