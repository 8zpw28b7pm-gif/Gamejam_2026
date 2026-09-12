using RF.Core;
using UnityEngine;

namespace RF.Items
{
    [CreateAssetMenu(menuName = "SO/New Shelf")]
    public class ShelfSO : ScriptableObject
    {
        [SerializeField] private Shelf shelfPrefab;
        [SerializeField] private ItemSO preferredItemSO;

        public Shelf GetPrefab() => shelfPrefab;
        public ItemSO GetPreferredItemSO() => preferredItemSO;
    }
}
