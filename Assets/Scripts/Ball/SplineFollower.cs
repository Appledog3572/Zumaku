using UnityEngine;
using UnityEngine.Splines;

public class SplineFollower : MonoBehaviour
{
    public SplineContainer splineContainer;
    public float speed = 0.1f;
    private float t = 0f;
    
    void Start()
    {
        
    }

    void Update()
    {
        t += speed * Time.deltaTime;
        transform.position = splineContainer.EvaluatePosition(t);
    }
}
