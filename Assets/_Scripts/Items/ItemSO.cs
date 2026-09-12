using RF.GameLoop;
using UnityEngine;

namespace RF.Items
{
    [CreateAssetMenu(menuName = "SO/New Item")]
    public class ItemSO : ScriptableObject
    {
        [SerializeField] private Item itemPrefab;
        [SerializeField] private GameObject itemVisualPrefab;

        public Item GetPrefab() => itemPrefab;

        public Transform SpawnVisual(Vector3 position, Transform parent)
        {
            return Instantiate(itemVisualPrefab.transform, position, Quaternion.identity, parent);
        }
    }
}
