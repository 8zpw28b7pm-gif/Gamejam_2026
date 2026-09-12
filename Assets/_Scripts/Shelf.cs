using System.Collections.Generic;
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
        [SerializeField] private ShelfSO shelfSO;

        [SerializeField] private Transform sillhouetteContainer;

        [SerializeField] private float shelfThreshold = 1f;
        [SerializeField] private int requiredAmount = 1;

        [SerializeField] private float destroyDelay = 1f;

        private bool hasReceivedItem = false;

        public ShelfSO GetShelfSO()
        {
            return shelfSO;
        }

        private void OnEnable()
        {
            Instantiate(shelfSO.GetPreferredItemSO().GetSillhouettePrefab(), sillhouetteContainer.position, Quaternion.identity, sillhouetteContainer);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (hasReceivedItem) return;
            if (!collision.gameObject.TryGetComponent<Item>(out Item item)) return;

            if (item.IsSettled())
            {
                hasReceivedItem = true;
                item.transform.SetParent(this.transform);

                if (item.GetItemSO() != shelfSO.GetPreferredItemSO())
                {
                    ScoreManager.Instance.RemoveScore();
                    return;
                }
                else
                {
                    CalculateScore(item);
                }
                
                DestroySelf();
            }
        }

        private void CalculateScore(Item item)
        {
            float distanceToSillhouette = Vector3.Distance(item.transform.position, sillhouetteContainer.position);
            Debug.Log($"Distance: {distanceToSillhouette}");
            ScoreManager.Instance.AddScore(distanceToSillhouette);
        }

        private void DestroySelf()
        {
            Destroy(gameObject, destroyDelay);
        }
    }
}
