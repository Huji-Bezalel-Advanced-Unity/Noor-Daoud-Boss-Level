using BLE.Gamelogic.Providers;
using LTD.Core.Utils;
using LTD.GameLogic.BaseMono;
using UnityEngine;

namespace _LTD.Scripts.GameLogic.Controls
{
    public class LTDGameManagerComponent : LTDBaseMono
    {
        void Start()
        {
            CoreManager.GameManager.InstantiatePlayer();
            CoreManager.GameManager.InstantiateBoss();
        }
    }
}