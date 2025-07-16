using System.Collections;
using LTD.GameLogic.BaseMono;
using LTD.GameLogic.Managers;
using UnityEngine;

namespace LTD.GameLogic.Enemies
{
    /// <summary>
    /// Handles a visual lock effect that fades in and out when the boss takes damage.
    /// Listens to the DecreaseDevilHealth event.
    /// </summary>
    public class LTDLockEffect : LTDBaseMono
    {

        /// <summary>
        /// Reference to the SpriteRenderer used for the visual effect.
        /// </summary>
        private SpriteRenderer spriteRenderer;
        

        /// <summary>
        /// Subscribes to events and initializes the sprite to be fully transparent.
        /// </summary>
        private void Awake()
        {
            LTDEvents.DecreaseDevilHealth += ShowEffect;
            spriteRenderer = GetComponent<SpriteRenderer>();

            if (spriteRenderer != null)
            {
                Color c = spriteRenderer.color;
                spriteRenderer.color = new Color(c.r, c.g, c.b, 0f);
            }
        }

        /// <summary>
        /// Unsubscribes from events to prevent memory leaks.
        /// </summary>
        private void OnDestroy()
        {
            LTDEvents.DecreaseDevilHealth -= ShowEffect;
        }


        #region Effect Logic

        /// <summary>
        /// Triggers the fade effect when the boss takes damage.
        /// </summary>
        private void ShowEffect()
        {
            if (spriteRenderer != null)
            {
                StartCoroutine(FadeEffect(0.2f, 0.5f, 0.3f)); // fadeIn, hold, fadeOut
            }
        }

        /// <summary>
        /// Coroutine that handles fading the sprite in, holding it visible, then fading it out.
        /// </summary>
        /// <param name="fadeInDuration">Time in seconds to fade in.</param>
        /// <param name="holdDuration">Time in seconds to stay fully visible.</param>
        /// <param name="fadeOutDuration">Time in seconds to fade out.</param>
        private IEnumerator FadeEffect(float fadeInDuration, float holdDuration, float fadeOutDuration)
        {
            Color color = spriteRenderer.color;
            
            float t = 0f;
            while (t < fadeInDuration)
            {
                t += Time.deltaTime;
                float alpha = Mathf.Lerp(0f, 1f, t / fadeInDuration);
                spriteRenderer.color = new Color(color.r, color.g, color.b, alpha);
                yield return null;
            }

            yield return new WaitForSeconds(holdDuration);

            t = 0f;
            while (t < fadeOutDuration)
            {
                t += Time.deltaTime;
                float alpha = Mathf.Lerp(1f, 0f, t / fadeOutDuration);
                spriteRenderer.color = new Color(color.r, color.g, color.b, alpha);
                yield return null;
            }

            spriteRenderer.color = new Color(color.r, color.g, color.b, 0f);
        }

        #endregion
    }
}
