using System.Collections.Generic;
using UnityEngine;

namespace RF.Items
{
    [CreateAssetMenu(menuName = "SO/New Combination List")]
    public class CombinationsListSO : ScriptableObject
    {
        [SerializeField] private CombinationSO[] allCombinations;

        public IEnumerable<CombinationSO> GetAllCombinations()
        {
            return allCombinations;
        }

    }
}