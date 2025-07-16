using System.Collections;
using LTD.Core.BaseMono;
using LTD.Core.Managers.AudioManager;
using UnityEngine;

namespace _LTD.Scripts.GameLogic.Enemies
{
    public class LTDLockEffect : LTDBaseMono
    {
        private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            LTDEvents.DecreaseDevilHealth += ShowEffect;
            spriteRenderer = GetComponent<SpriteRenderer>();

            // Set initial alpha to 0 instead of disabling GameObject
            if (spriteRenderer != null)
            {
                Color c = spriteRenderer.color;
                spriteRenderer.color = new Color(c.r, c.g, c.b, 0f);
            }
        }

        private void OnDestroy()
        {
            LTDEvents.DecreaseDevilHealth -= ShowEffect;
        }

        private void ShowEffect()
        {
            // Don’t disable the GameObject — just fade in/out the renderer
            if (spriteRenderer != null)
            {
                StartCoroutine(FadeEffect(0.2f, 0.5f, 0.3f)); // Fade in, hold, fade out
            }
        }

        private IEnumerator FadeEffect(float fadeInDuration, float holdDuration, float fadeOutDuration)
        {
            Color color = spriteRenderer.color;

            // Fade in
            float t = 0f;
            while (t < fadeInDuration)
            {
                t += Time.deltaTime;
                float alpha = Mathf.Lerp(0f, 1f, t / fadeInDuration);
                spriteRenderer.color = new Color(color.r, color.g, color.b, alpha);
                yield return null;
            }

            yield return new WaitForSeconds(holdDuration);

            // Fade out
            t = 0f;
            while (t < fadeOutDuration)
            {
                t += Time.deltaTime;
                float alpha = Mathf.Lerp(1f, 0f, t / fadeOutDuration);
                spriteRenderer.color = new Color(color.r, color.g, color.b, alpha);
                yield return null;
            }

            // Optional: reset alpha
            spriteRenderer.color = new Color(color.r, color.g, color.b, 0f);
        }
    }
}
