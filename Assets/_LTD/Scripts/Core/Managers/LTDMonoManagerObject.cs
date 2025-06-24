
using LTD.Core.BaseMono;
namespace LTD.Core.Managers
{
    public class LTDMonoManagerObject:LTDBaseMono
    {
        public static LTDMonoManagerObject Instance { get; private set; }

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