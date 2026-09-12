using UnityEngine;

namespace RF.UI
{
    [DefaultExecutionOrder(-8888)]
    public class UIHandler : MonoBehaviour
    {
        public static UIHandler Instance;

        public ScoreUI ScoreUI { get; set; }
        public HealthUI HealthUI { get; set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Debug.LogWarning("Multiple UIHandler Instances!");
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }
    }
}