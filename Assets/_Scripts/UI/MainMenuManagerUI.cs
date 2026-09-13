using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace RF.UI
{
    public class MainMenuManagerUI : MonoBehaviour
    {
        [Header("MENU GROUPS")]
        [SerializeField] private GameObject mainGroup;
        [SerializeField] private GameObject leaderboardGroup;

        [Header("BUTTONS")]
        [SerializeField] private Button playButton;
        [SerializeField] private Button leaderboardButton;
        [SerializeField] private Button quitButton;
        [SerializeField] private Button backButton;


        public enum MainMenuState
        {
            Main,
            Leaderboard
        }

        private MainMenuState state;
        public MainMenuState State => state;

        private void Awake()
        {
            playButton.onClick.AddListener(() => SceneManager.LoadScene(1));
            leaderboardButton.onClick.AddListener(() => SetMenuState(MainMenuState.Leaderboard));
            backButton.onClick.AddListener(() => SetMenuState(MainMenuState.Main));
            quitButton.onClick.AddListener(() => Application.Quit());
        }

        private void Start()
        {
            SetMenuState(MainMenuState.Main, true);
        }

        private void SetMenuState(MainMenuState newState, bool forceReset = false)
        {
            if (newState == state && !forceReset) return;

            state = newState;

            switch (state)
            {
                case MainMenuState.Main:
                    leaderboardGroup.SetActive(false);
                    mainGroup.SetActive(true);
                    break;
                case MainMenuState.Leaderboard:
                    backButton.gameObject.SetActive(true);
                    leaderboardGroup.SetActive(true);
                    mainGroup.SetActive(false);
                    break;
                default:
                    leaderboardGroup.SetActive(false);
                    mainGroup.SetActive(true);
                    break;
            }
        }

    }
}
