using UnityEngine;

namespace RF.Core
{
    [CreateAssetMenu(menuName = "SO/New Score Data")]
    public class ScoreDataSO : ScriptableObject
    {
        [SerializeField] private int correctItem = 50;
        [SerializeField] private int wrongItem = -20;
        [SerializeField] private int correctLastItem = 100;

        public int CorrectItem => correctItem;
        public int WrongItem => wrongItem;
        public int CorrectLastItem => correctItem;
    }
}
