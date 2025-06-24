
using System;
using UnityEngine;

namespace LTD.Core.Managers
{
    public class LTDCoreManager
    {
         public static LTDCoreManager Instance { get; private set; }
        
         public LTDCoreManager()
         {
             Instance = this;
         }
         
         public void LoadManagers(Action onComplete)
         {
             Debug.Log("InitCoreManagers started.");

             GameObject temp = new GameObject("MonoManager");
             temp.AddComponent<LTDMonoManagerObject>();
             Debug.Log("MonoManager GameObject created");

             onComplete?.Invoke();
         }
    }
}