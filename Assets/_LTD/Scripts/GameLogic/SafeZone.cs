using LTD.Core.BaseMono;
using LTD.Core.Managers;
using UnityEngine;

namespace BLE.Gamelogic.Zone
{
    public class SafeZone : LTDBaseMono
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            // if (other.gameObject.CompareTag("Player"))
            // {
            print("SafeZone");
            Events.SafeZone?.Invoke();
            // }
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            // if (other.gameObject.CompareTag("Player"))
            // {
            print("ResZone");
            Events.RedZone?.Invoke();
            // }
        }
    }
}