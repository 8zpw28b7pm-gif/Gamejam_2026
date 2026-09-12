using RF.GameLoop;
using UnityEngine;

namespace RF.Items
{
    [CreateAssetMenu(menuName = "SO/New Item")]
    public class ItemSO : ScriptableObject
    {
        [SerializeField] private Item itemPrefab;
        [SerializeField] private GameObject sillhouettePrefab;

        public Item GetPrefab() => itemPrefab;
        public GameObject GetSillhouettePrefab() => sillhouettePrefab;
    }
}
