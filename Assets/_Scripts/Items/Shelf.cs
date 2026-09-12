using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using NUnit.Framework;
using RF.GameLoop;
using RF.Items;
using UnityEngine;
using UnityEngine.TextCore;

namespace RF.Core
{
    public class Shelf : MonoBehaviour
    {
        [SerializeField] private ShelfSlot[] shelfSlots;
        [SerializeField] private CombinationsListSO combinationsListSO;

        private float destroyDelay = 1f;

        private void OnEnable()
        {
            foreach (var slot in shelfSlots)
            {
                slot.onSlotChanged += EvaluateSlots;
            }
        }

        private void OnDisable()
        {
            foreach (var slot in shelfSlots)
            {
                slot.onSlotChanged -= EvaluateSlots;
            }
        }

        private void EvaluateSlots()
        {
            foreach (var slot in shelfSlots)
            {
                if (!slot.HasItem()) return;
            }

            Debug.Log($"{transform.name} all slots");

            CombinationSO matchingCombo = null;

            foreach (var combo in combinationsListSO.GetAllCombinations())
            {
                for (int i = 0; i < combo.GetItemList().Count(); i++)
                {
                    if (shelfSlots[i].GetItemSO() == combo.GetItemList().ToList<ItemSO>()[i])
                    {
                        matchingCombo = combo;
                        break;
                    }
                }
            }

            Debug.Log($"Found Matching Combo: {matchingCombo}");

            if (matchingCombo != null)
            {
                AwardPoints(matchingCombo);
                ClearShelf();
            }
        }

        private void AwardPoints(CombinationSO combinationSO)
        {
            GameManager.Instance.ScoreManager.AddScore(combinationSO.GetPoints());
        }

        private void ClearShelf()
        {
            foreach (var slot in shelfSlots)
            {
                slot.ClearSlot();
            }
        }

        private void DestroySelf()
        {
            Destroy(gameObject, destroyDelay);
        }
    }
}
