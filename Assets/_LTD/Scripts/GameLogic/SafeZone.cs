using LTD.Core.BaseMono;
using LTD.Core.Managers;
using UnityEngine;

namespace BLE.Gamelogic.Zone
{
    public class SafeZone : LTDBaseMono
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            print("SafeZone");
            Events.SafeZone?.Invoke();
            
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            print("ResZone");
            Events.RedZone?.Invoke();
        }
    }
}