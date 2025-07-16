using System.Collections;
using LTD.Core.BaseMono;
using LTD.Core.Managers.AudioManager;
using UnityEngine;

namespace LTD.Core.Enemies
{
    public class LTDBossHealth : LTDBaseMono
    {
        #region Animator Hashes

        private static readonly int Hurt = Animator.StringToHash("Hurt");

        #endregion
        
        [Header("Boss Animation")]
        [SerializeField] private Animator animator;
        private SpriteRenderer spriteRenderer;
        private Color _dieColor = Color.red;
        private int _currentHealth = 4;

        private void Awake()
        {
            LTDEvents.DecreaseDevilHealth += TakeDamage;
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        
        private void OnDestroy()
        {
            LTDEvents.DecreaseDevilHealth -= TakeDamage;
        }
        
        #region Damage Handling

        private void TakeDamage()
        {
            _currentHealth--;
            animator?.SetTrigger(Hurt);
           LTDAudioManager.Instance.PlaySFX(LTDAudioManager.AudioClipType.DevilsHurt);

           
            if (_currentHealth <= 0)
            {
                spriteRenderer.color = _dieColor;
                LTDEvents.DevilDies?.Invoke();
                Destroy(gameObject, 1.5f); 
            }
        }
        #endregion
    }
}
