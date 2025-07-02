using System;
using UnityEngine;

namespace LTD.GameLogic.Controls
{
    public class LTDCoreManager
    {

        public static LTDCoreManager Instance { get; private set; }

        public LTDCoreManager()
        {
            Instance = this;
        }
        
        #region Core Manager Loading

        public void LoadManagers(Action onComplete)
        {
            Debug.Log("InitCoreManagers started.");

            GameObject temp = new GameObject("MonoManager");
            temp.AddComponent<LTDSGameManager>();
            Debug.Log("MonoManager GameObject created");

            onComplete?.Invoke();
        }

        #endregion
    }
}