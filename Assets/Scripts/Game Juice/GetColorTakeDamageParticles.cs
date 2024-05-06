using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetColorTakeDamageParticles : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private ParticleSystem particle;

    [System.Obsolete]
    void Start()
    {
        spriteRenderer = GetComponentInParent<SpriteRenderer>();
        particle = GetComponent<ParticleSystem>();  
        if (spriteRenderer != null && spriteRenderer.sprite != null)
        {
            Texture2D texture = spriteRenderer.sprite.texture;
            Vector2 center = new Vector2(0.5f, 0.5f);
            Color color = texture.GetPixelBilinear(center.x, center.y);
            particle.startColor = color;
        }
    }
}
