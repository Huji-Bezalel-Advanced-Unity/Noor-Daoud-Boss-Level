using System;
using UnityEngine;

namespace LTD.GameLogic
{
    public class LTDCoreManager
    {

        public static LTDCoreManager Instance { get; private set; }
        public LTDGameManager GameManager;

        public LTDCoreManager()
        {
            Instance = this;
        }

        #region Core Manager Loading

        public void LoadManagers(Action onComplete)
        {
            GameManager = new();
            onComplete?.Invoke();
        }

        #endregion
    }
}