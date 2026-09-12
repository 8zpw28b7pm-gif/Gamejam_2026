using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RF.UI
{
    public class GameOverMenuUI : MonoBehaviour
    {
        [SerializeField] private Button playAgainButton;
        [SerializeField] private Button mainMenuButton;
        [SerializeField] private Button quitButton;

        private void Awake()
        {
            playAgainButton.onClick.AddListener(() => SceneManager.LoadScene(1));
            mainMenuButton.onClick.AddListener(() => SceneManager.LoadScene(0));
            quitButton.onClick.AddListener(() => Application.Quit());
        } 
    }
}