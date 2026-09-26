using UnityEngine;

public class Ball : MonoBehaviour
{
    public BulletPatternSO pattern;
    public int currentPhase = 1;
    public float fireCooldown = 5f;
    float fireTimer;

    void Start()
    {
        fireTimer = 0f;
    }

    void Update()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireCooldown)
        {
            fireTimer = 0f;
            if(pattern != null)
            {
                pattern.Fire(this.transform, currentPhase, this);
            }
        }
    }
}
