using RF.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace RF.UI
{
    public class InGameMenuUI : MonoBehaviour
    {
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button mainMenuButton;
        [SerializeField] private Button quitButton;

        private void Awake()
        {
            resumeButton.onClick.AddListener(() => GameManager.Instance.SetState(GameState.Running));
            mainMenuButton.onClick.AddListener(() => SceneManager.LoadScene(0));
            quitButton.onClick.AddListener(() => Application.Quit());
        }
    }
}