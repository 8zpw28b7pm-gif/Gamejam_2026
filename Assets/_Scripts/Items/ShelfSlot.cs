using System;
using RF.Core;
using RF.GameLoop;
using RF.Items;
using UnityEngine;

namespace RF.Items
{
    public class ShelfSlot : MonoBehaviour
    {
        [SerializeField] private ItemSO itemSO;
        [SerializeField] private ItemSO preferredItemSO;

        public event Action<ShelfSlot> onSlotChanged;

        public ItemSO GetPreferredItemSO()
        {
            return preferredItemSO;
        }

        public void SetPreferredItemSO(ItemSO itemSO)
        {
            preferredItemSO = itemSO;
            preferredItemSO.SpawnSilhouette(transform.position, transform);
        }

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

            if (itemSO == preferredItemSO)
            {
                GameManager.Instance.AudioManager.PlayPopSound();
            }

            onSlotChanged?.Invoke(this);
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
            if (!item.HasTouchedTrampoline()) return;

            if (!item.IsFalling() && item.GetItemSO() != GetPreferredItemSO())
            {
                return;
            }


            SetItemSO(item.GetItemSO());

            item.DestroySelf();

            onSlotChanged?.Invoke(this);
        }
    }
}