// 黃色：直線分裂彈 (1 -> 3 -> 5)
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Danmaku/Patterns/Split Shot")]
public class SplitShotPattern : BulletPatternSO
{
    public GameObject bulletPrefab;
    public List<int> bulletNumber = new List<int> { 1, 3, 5 };
    public float bulletSpeed = 5f;
    public int bulletAngle = 15; // 子彈間隔的角度
    public float bulletLifetime = 5f;

    public override void Fire(Transform origin, int phase, MonoBehaviour host)
    {
        int[] reverse = { 1, -1 }; // 用於反向發射的方向
        foreach (int dir in reverse)
        {
            for (int i = 0; i < bulletNumber[phase - 1]; i++)
            {
                float angle = (i + 1) / 2 * bulletAngle * (i % 2 == 0 ? 1 : -1); // 計算子彈的角度
                Vector2 direction = Quaternion.Euler(0, 0, angle) * origin.right * dir;
                Rigidbody2D bullet = Instantiate(bulletPrefab, origin.position, Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg)).GetComponent<Rigidbody2D>();
                bullet.linearVelocity = direction * bulletSpeed;
                Destroy(bullet.gameObject, bulletLifetime); // 設定子彈的生命週期
            }
        }
    }
}