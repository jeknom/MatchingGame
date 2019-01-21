using UnityEngine.SceneManagement;

namespace MatchingGame
{
    public class Menus
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