
using System;
using LTD.GameLogic.BaseMono;
using UnityEngine;

namespace LTD.GameLogic.Controls
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