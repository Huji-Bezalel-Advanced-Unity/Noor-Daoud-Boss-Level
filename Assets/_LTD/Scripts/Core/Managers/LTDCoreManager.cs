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
            Debug.Log("InitCoreManagers started.");

            GameObject temp = new GameObject("MonoManager");
            temp.AddComponent<LTDMonoManager>();
            Debug.Log("MonoManager GameObject created");

            GameManager = new();

            onComplete?.Invoke();
        }

        #endregion
    }
}