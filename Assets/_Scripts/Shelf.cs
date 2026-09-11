using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.TextCore;

namespace RF.Core
{
    public class Shelf : MonoBehaviour
    {
        [SerializeField] private float shelfThreshold = 1f;
        [SerializeField] private int requiredAmount = 1;
        [SerializeField] private Transform cupContainer;
        [SerializeField] List<GameObject> cupsOnShelf;

        [SerializeField] private float destroyDelay = 1f;


        private void Update()
        {
            if (cupsOnShelf.Count >= requiredAmount)
            {
                DestroySelf();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.CompareTag("KitchenObject")) return;

            if (!cupsOnShelf.Contains(collision.gameObject))
            {
                collision.gameObject.transform.SetParent(this.transform);
                cupsOnShelf.Add(collision.gameObject);

            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.gameObject.CompareTag("KitchenObject")) return;

            if (cupsOnShelf.Contains(collision.gameObject))
            {
                cupsOnShelf.Remove(collision.gameObject);
            }
        }

        private void DestroySelf()
        {
            Destroy(gameObject, destroyDelay);
        }
    }
}
