using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class PunchBag : MonoBehaviour {
    [Header("Damage Settings")]
    public Color hitColor = Color.red;
    public float hitFlashDuration = 0.2f;
    public LayerMask damageLayer;

    [Header("Knockback Settings")]
    [Range(0.0f, 10.0f)] public float knockbackForce = 2.0f;
    [Range(0.0f, 2.0f)] public float upwardMultiplier = 0.5f;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Color originalColor;
    private bool isFlashing = false;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        originalColor = spriteRenderer.color;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.isTrigger == true && ((1 << collision.gameObject.layer) & damageLayer) != 0) {
            Vector2 impactDirection = (transform.position - collision.transform.position).normalized;
            Vector2 knockbackDirection = (impactDirection + Vector2.up * upwardMultiplier).normalized;

            rb.AddForce(knockbackDirection * knockbackForce / rb.mass, ForceMode2D.Impulse);
            TakeDamage();
        }
    }

    private void TakeDamage() {
        if (!isFlashing) {
            StartCoroutine(FlashColor());
        }
    }

    private IEnumerator FlashColor() {
        isFlashing = true;
        spriteRenderer.color = hitColor;
        yield return new WaitForSeconds(hitFlashDuration);
        spriteRenderer.color = originalColor;
        isFlashing = false;
    }
}