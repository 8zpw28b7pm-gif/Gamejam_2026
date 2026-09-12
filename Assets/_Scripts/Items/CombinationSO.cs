using System.Collections.Generic;
using Mono.Cecil.Cil;
using UnityEngine;

namespace RF.Items
{
    [CreateAssetMenu(menuName = "SO/New Combination")]
    public class CombinationSO : ScriptableObject
    {
        [SerializeField] private List<ItemSO> itemList = new();
        [SerializeField] private int points = 0;

        public IEnumerable<ItemSO> GetItemList() => itemList;
        public int GetPoints() => points;
    }
}