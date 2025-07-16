using LTD.GameLogic.BaseMono;
using LTD.GameLogic.Managers;
using UnityEngine;

namespace LTD.GameLogic.Enemies
{
    /// <summary>
    /// Represents a magical locking sigil that the player can activate when nearby.
    /// On activation, it changes appearance, plays audio, and decreases the boss's health.
    /// </summary>
    public class LTDLockingSigil : LTDBaseMono
    {

        [Header("Visuals")]
        [Tooltip("The SpriteRenderer controlling the sigil's appearance.")]
        [SerializeField] private SpriteRenderer spriteRenderer;

        [Tooltip("The sprite to display when the sigil is activated.")]
        [SerializeField] private Sprite activatedSprite;

        [Tooltip("Color used to highlight the sigil when the player is nearby.")]
        [SerializeField] private Color highlightColor = Color.yellow;

        [Tooltip("Default sigil color when idle.")]
        [SerializeField] private Color normalColor = Color.white;
        
        private Color _activatedColor = Color.blue;
        private bool _isPlayerNearby = false;
        private bool _isActivated = false;

        
        /// <summary>
        /// Monitors input to activate the sigil if the player is nearby.
        /// </summary>
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && _isPlayerNearby && !_isActivated)
            {
                ActivateSigil();
            }
        }

        /// <summary>
        /// Called when another collider enters the sigil's trigger zone.
        /// Highlights the sigil and plays an SFX.
        /// </summary>
        private void OnTriggerEnter2D(Collider2D other)
        {
            LTDAudioManager.Instance.PlaySFX(LTDAudioManager.AudioClipType.Lock);
            if (_isActivated) return;

            _isPlayerNearby = true;
            spriteRenderer.color = highlightColor;
        }

        /// <summary>
        /// Called when the player leaves the sigil's trigger zone.
        /// Reverts the color if not activated.
        /// </summary>
        private void OnTriggerExit2D(Collider2D other)
        {
            _isPlayerNearby = false;

            if (!_isActivated)
            {
                spriteRenderer.color = normalColor;
            }
        }


        #region Activation Logic

        /// <summary>
        /// Activates the sigil: changes appearance and signals to damage the boss.
        /// </summary>
        private void ActivateSigil()
        {
            _isActivated = true;
            spriteRenderer.sprite = activatedSprite;
            spriteRenderer.color = _activatedColor;

            LTDEvents.DecreaseDevilHealth?.Invoke();
        }

        #endregion
    }
}
