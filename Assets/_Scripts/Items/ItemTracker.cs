using System.Collections.Generic;
using RF.GameLoop;
using UnityEngine;

namespace RF.Core
{
    public class ItemTracker : MonoBehaviour
    {
        [SerializeField] private List<Item> itemsInScene;

        private void Awake()
        {
            GameManager.Instance.ItemTracker = this;
        }

        public int GetItemCount()
        {
            return itemsInScene.Count;
        }

        public void RegisterItem(Item item)
        {
            if (!itemsInScene.Contains(item))
            {
                itemsInScene.Add(item);
            }
        }

        public void DeregisterItem(Item item)
        {
            if (itemsInScene.Contains(item))
            {
                itemsInScene.Remove(item);
            }
        }
    }
}
