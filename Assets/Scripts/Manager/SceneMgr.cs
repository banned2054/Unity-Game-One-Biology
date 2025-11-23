using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manager
{
    public class SceneMgr : MonoBehaviour
    {
        public void SwitchScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void GameOver()
        {
            Application.Quit();
        }
    }
}