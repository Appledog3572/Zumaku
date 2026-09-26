using UnityEngine;

public abstract class BulletPatternSO : ScriptableObject
{
    public abstract void Fire(Transform origin, int phase, MonoBehaviour host);
}