using RF.Core;
using UnityEngine;

namespace RF.Items
{
    [CreateAssetMenu(menuName = "SO/New Shelf")]
    public class ShelfSO : ScriptableObject
    {
        [SerializeField] private Shelf shelfPrefab;
        [SerializeField] private ItemSO preferredItemSO;
        [SerializeField] private int requiredItemAmount;

        public Shelf GetPrefab() => shelfPrefab;
        public ItemSO GetPreferredItemSO() => preferredItemSO;
        public int GetRequiredItemAmount() => requiredItemAmount;
    }
}
