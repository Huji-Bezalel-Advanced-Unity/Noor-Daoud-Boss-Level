using BLE.Gamelogic.Providers;
using LTD.Core.Utils;
using LTD.GameLogic.BaseMono;
using LTD.GameLogic.Controls;
using UnityEngine;
using UnityEngine.Audio;

namespace _LTD.Scripts.GameLogic.Controls
{
    public class LTDGameManagerComponent : LTDBaseMono
    {
        void Start()
        {
            CoreManager.GameManager.InstantiatePlayer();
            CoreManager.GameManager.InstantiateBoss();
            LTDAudioManager.Instance.PlayGameMusic();
        }
    }
}