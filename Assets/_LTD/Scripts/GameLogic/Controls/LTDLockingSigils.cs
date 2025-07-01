using LTD.Core.BaseMono;
using LTD.Core.Managers;
using UnityEngine;

namespace _LTD.Scripts.GameLogic.Controls
{
    public class LTDLockingSigil : LTDBaseMono
    {
        [Header("Visuals")]
        [SerializeField] private SpriteRenderer spriteRenderer;
        //[SerializeField] private Sprite activatedSprite;
        [SerializeField] private Color highlightColor = Color.yellow;
        [SerializeField] private Color normalColor = Color.white;

        private bool _isPlayerNearby = false;
        private bool _isActivated = false;

        private void Update()
        {
            if (_isPlayerNearby && !_isActivated && Input.GetKeyDown(KeyCode.L))
            {
                ActivateSigil();
            }
        }

        private void ActivateSigil()
        {
            _isActivated = true;
          //  spriteRenderer.sprite = activatedSprite;
            spriteRenderer.color = normalColor; 
            Events.DecreaseDevilHealth?.Invoke();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_isActivated) return;
            _isPlayerNearby = true;
            spriteRenderer.color = highlightColor;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
     
                _isPlayerNearby = false;
                if (!_isActivated)
                {
                    spriteRenderer.color = normalColor;
                }
            
        }
    }
}