using UnityEngine;
using UnityEngine.SceneManagement;

namespace MatchingGame
{
    public class Menus : MonoBehaviour
    {
        public void LoadMainMenu()
        {
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }

        public void LoadGame()
        {
            SceneManager.LoadScene("Game", LoadSceneMode.Single);
        }
    }
}