// 藍色：連續自機狙擊彈 (3 -> 5 -> 7)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Danmaku/Patterns/Sniper Shot")]
public class SniperShotPattern1 : BulletPatternSO
{
    public GameObject bulletPrefab;
    public List<int> bulletNumber = new List<int> { 3, 5, 7 };
    public float bulletSpeed = 6f;
    public float bulletLifetime = 5f;
    public float fireInterval = 0.2f; // 每發射一顆子彈後等待的時間

    public override void Fire(Transform origin, int phase, MonoBehaviour host)
    {
        host.StartCoroutine(FireContinuously(origin, phase));
    }

    IEnumerator FireContinuously(Transform origin, int phase)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogWarning("Player not found in the scene.");
            yield break; // 改為 yield break
        }
        for (int i = 0; i < bulletNumber[phase - 1]; i++)
        {
            Vector2 direction = (player.transform.position - origin.transform.position).normalized;
            Rigidbody2D bullet = Instantiate(bulletPrefab, origin.position, Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg)).GetComponent<Rigidbody2D>();
            bullet.linearVelocity = direction * bulletSpeed;
            Destroy(bullet.gameObject, bulletLifetime);
            yield return new WaitForSeconds(fireInterval);
        }
    }
}