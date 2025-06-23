using LTD.Core.Managers;
using UnityEngine.SceneManagement;
namespace LTD.Core
{
    public class LTDGameLoader : LTDBaseMono
    {
        private void Awake()
        {
            new LTDCoreManager();
            LoadNextScene();
        }

        private void LoadNextScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}