
using System;
using LTD.Core.BaseMono;
using UnityEngine;

namespace LTD.Core.Managers
{
    public class LTDSGameManager:LTDBaseMono
    {
        public static LTDSGameManager Instance { get; private set; }
        
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }
        
    }
}