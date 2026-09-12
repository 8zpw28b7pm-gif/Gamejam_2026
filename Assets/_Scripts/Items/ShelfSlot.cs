using System;
using RF.GameLoop;
using RF.Items;
using UnityEngine;

namespace RF.Items
{
    public class ShelfSlot : MonoBehaviour
    {
        [SerializeField] private ItemSO itemSO;

        public event Action onSlotChanged;

        public bool HasItem()
        {
            return itemSO != null;
        }

        public ItemSO GetItemSO()
        {
            return itemSO;
        }

        public void SetItemSO(ItemSO itemSO)
        {
            this.itemSO = itemSO;
            itemSO.SpawnVisual(transform.position, transform);
        }

        public void ClearSlot()
        {
            itemSO = null;
            
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (HasItem()) return;
            if (!collision.TryGetComponent<Item>(out Item item)) return;
            if (!item.HasTouchedTrampoline() || item.IsRising()) return;

            SetItemSO(item.GetItemSO());

            item.DestroySelf();

            onSlotChanged?.Invoke();
        }
    }
}