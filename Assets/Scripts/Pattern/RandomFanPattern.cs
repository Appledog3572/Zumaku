// 橙色：90° 隨機慢速米彈 (3 -> 6 -> 12)
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Danmaku/Patterns/Random Fan")]
public class RandomFanPattern : BulletPatternSO
{
    public GameObject bulletPrefab;
    public List<int> bulletNumber = new List<int> { 3, 6, 12 };
    public float minSpeed = 2f;
    public float maxSpeed = 6f;
    public float fanAngle = 90f; // 子彈間隔的角度
    public float bulletLifetime = 5f;

    public override void Fire(Transform origin, int phase, MonoBehaviour host)
    {
        int[] reverse = { 1, -1 }; // 用於反向發射的方向
        foreach (int dir in reverse)
        {
            for (int i = 0; i < bulletNumber[phase - 1]; i++)
            {
                float angle = Random.Range(-fanAngle/2, fanAngle/2); // 計算子彈的角度
                Vector2 direction = Quaternion.Euler(0, 0, angle) * origin.right * dir;
                Rigidbody2D bullet = Instantiate(bulletPrefab, origin.position, Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg)).GetComponent<Rigidbody2D>();
                bullet.linearVelocity = direction * Random.Range(minSpeed, maxSpeed);
                Destroy(bullet.gameObject, bulletLifetime); // 設定子彈的生命週期
            }
        }
    }
}