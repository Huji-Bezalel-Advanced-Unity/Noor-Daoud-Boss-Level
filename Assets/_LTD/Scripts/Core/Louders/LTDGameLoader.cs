using LTD.Core.BaseMono;
using LTD.Core.Managers;
using UnityEngine.SceneManagement;

namespace LTD.Core.Louders
{
    public class LTDGameLoader : LTDBaseMono
    {
        private void Start()
        {
            var coreManager = new LTDCoreManager();
            coreManager.LoadManagers(() =>
            {
                int gameSceneIndex = 1;
                SceneManager.LoadScene(gameSceneIndex);
            });
        }
    }
}