
using System;
using LTD.Core.BaseMono;
using UnityEngine;

namespace LTD.Core.Managers
{
    public class LTDStartingManager:LTDBaseMono
    {
        [SerializeField] private GameObject startMenu;
        public static LTDStartingManager Instance { get; private set; }
        
        
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