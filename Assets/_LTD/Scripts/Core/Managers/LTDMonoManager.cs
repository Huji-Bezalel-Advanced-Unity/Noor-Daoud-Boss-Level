using System;
using LTD.GameLogic.BaseMono;

namespace LTD.GameLogic
{
    public class LTDMonoManager : LTDBaseMono
    {
        public static LTDMonoManager Instance { get; private set; }

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