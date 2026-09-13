using System.Linq;
using RF.Items;
using RF.UI;
using UnityEngine;

namespace RF.Core
{
    public class Shelf : MonoBehaviour
    {
        [SerializeField] private ShelfSlot[] shelfSlots;
        private float destroyDelay = 1f;

        [SerializeField] private ScoreDataSO scoreDataSO;
        [SerializeField] private FloatingText floatingTextPrefab;


        private void OnEnable()
        {
            foreach (var slot in shelfSlots)
            {
                slot.onSlotChanged += ShelfSlot_OnSlotChanged;
            }
        }

        private void OnDisable()
        {
            foreach (var slot in shelfSlots)
            {
                slot.onSlotChanged -= ShelfSlot_OnSlotChanged;
            }
        }

        public void Init(ItemListSO itemListSO)
        {
            InitialiseSlots(itemListSO);
        }

        private void InitialiseSlots(ItemListSO itemListSO)
        {
            for (int i = 0; i < shelfSlots.Length; i++)
            {
                int randomItemIndex = Random.Range(0, itemListSO.GetItemList().ToList<ItemSO>().Count);
                ItemSO randomItem = itemListSO.GetItemList().ToList<ItemSO>()[randomItemIndex];

                shelfSlots[i].SetPreferredItemSO(randomItem);
            }
        }

        private void ShelfSlot_OnSlotChanged(ShelfSlot shelfSlot)
        {
            if (!HasEmptySlot())
            {
                for (int i = 0; i < shelfSlots.Length; i++)
                {
                    if (shelfSlots[i].GetItemSO() != shelfSlots[i].GetPreferredItemSO())
                    {
                        AwardPoints(scoreDataSO.WrongItem);
                        break;
                    }
                    AwardPoints(scoreDataSO.CorrectLastItem);
                }
                DestroySelf();
                return;
            }

            if (shelfSlot.HasItem())
            {
                if (shelfSlot.GetItemSO() == shelfSlot.GetPreferredItemSO())
                {
                    AwardPoints(scoreDataSO.CorrectItem);
                }
                else
                {
                    AwardPoints(scoreDataSO.WrongItem);
                }
            }
        }

        public bool HasEmptySlot()
        {
            for (int i = 0; i < shelfSlots.Length; i++)
            {
                if (shelfSlots[i].GetItemSO() == null) return true;
            }

            return false;
        }

        public bool HasSpaceForItem(ItemSO itemSO)
        {
            foreach (var slot in shelfSlots)
            {
                if (slot.GetPreferredItemSO() == itemSO)
                {
                    return true;
                }
            }

            return false;
        }

        private void AwardPoints(int amount)
        {
            FloatingText spawnedText = Instantiate(floatingTextPrefab, new Vector2(transform.position.x, transform.position.y + 1f), Quaternion.identity, null);
            spawnedText.Init(amount);

            GameManager.Instance.ScoreManager.AddScore(amount);
        }

        private void DestroySelf()
        {
            Destroy(gameObject, destroyDelay);
        }


    }
}