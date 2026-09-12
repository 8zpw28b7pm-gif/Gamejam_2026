using System;
using RF.GameLoop;
using RF.Items;
using UnityEngine;

namespace RF.Core
{
    public class Hitbox : MonoBehaviour
    {
        public event Action<Item, Hitbox> onHitboxHit;

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.TryGetComponent<Item>(out Item item)) return;
            Debug.Log($"{item.GetItemSO()} Trigger Enter");

            onHitboxHit?.Invoke(item, this);
        }

        public void DisableHitbox()
        {
            GetComponent<Collider2D>().enabled = false;
        }

        public void EnableHitbox()
        {
            GetComponent<Collider2D>().enabled = true;
        }
    }
}