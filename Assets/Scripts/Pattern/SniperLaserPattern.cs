// 綠色：狙擊雷射 (1 -> 3 -> 5)
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Danmaku/Patterns/Sniper Laser")]
public class SniperLaserPattern : BulletPatternSO
{
    public GameObject beamPrefab;
    public List<int> beamNumber = new List<int> { 1, 3, 5 };
    public float beamAngle = 7.5f; // 雷射間隔的角度
    public float beamLifetime = 2f;

    public override void Fire(Transform origin, int phase, MonoBehaviour host)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogWarning("Player not found in the scene.");
            return;
        }
        for (int i = 0; i < beamNumber[phase - 1]; i++)
        {
            float angle = (i + 1) / 2 * beamAngle * (i % 2 == 0 ? 1 : -1); // 計算雷射的角度
            Vector2 direction = Quaternion.Euler(0, 0, angle) * (player.transform.position - origin.transform.position).normalized;
            GameObject beam = Instantiate(beamPrefab, origin.position, Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg));
            beam.transform.position += new Vector3(direction.x, direction.y, 0) * (beam.transform.localScale.x / 2);
        }
    }
}