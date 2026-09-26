// 黃色：直線分裂彈 (1 -> 3 -> 5)
using UnityEngine;

public class SplittingBullet : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 7f;
    public int bulletAngle = 15; // 子彈間隔的角度
    public float bulletLifetime = 5f;

    void Start()
    {
        Invoke(nameof(Split), 0.5f);
    }

    void Split()
    {
        for (int i = 0; i < 3; i++)
        {
            float angle = (i + 1) / 2 * bulletAngle * (i % 2 == 0 ? 1 : -1); // 計算子彈的角度
            Vector2 direction = Quaternion.Euler(0, 0, angle) * transform.right;
            Rigidbody2D bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg)).GetComponent<Rigidbody2D>();
            bullet.linearVelocity = direction * bulletSpeed;
            Destroy(bullet.gameObject, bulletLifetime); // 設定分裂子彈的生命週期
        }
        Destroy(gameObject); // 銷毀原始子彈
    }
}
