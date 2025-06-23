using UnityEngine;

namespace LTDCore.Managers
{
    public class SafeZone:MonoBehaviour
    {
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                print("SafeZone");
                Events.SafeZone?.Invoke();
            }
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                print("ResZone");
                Events.RedZone?.Invoke();
            }        
        }
    }
}