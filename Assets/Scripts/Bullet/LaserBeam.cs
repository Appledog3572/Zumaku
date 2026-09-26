using System.Collections;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    public float warningDuration = 2f;
    public float damageDuration = 1f;
    SpriteRenderer spriteRenderer;
    BoxCollider2D beamCollider;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        beamCollider = GetComponent<BoxCollider2D>();
        StartCoroutine(WarningAndDamage());
    }

    IEnumerator WarningAndDamage()
    {
        beamCollider.enabled = false;
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.3f);
        yield return new WaitForSeconds(warningDuration);
        beamCollider.enabled = true;
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
        yield return new WaitForSeconds(damageDuration);
        Destroy(gameObject);
    }
}
