using System.Collections.Generic;
using UnityEngine;

namespace RF.Items
{
    [CreateAssetMenu(menuName = "SO/New Item List")]
    public class ItemListSO : ScriptableObject
    {
        [SerializeField] private List<ItemSO> itemList = new();
        public IEnumerable<ItemSO> GetItemList() => itemList;
    }
}