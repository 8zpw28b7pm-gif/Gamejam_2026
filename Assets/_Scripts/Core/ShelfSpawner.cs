using System.Collections.Generic;
using RF.Items;
using Unity.VisualScripting;
using UnityEngine;

namespace RF.Core
{
    public class ShelfSpawner : MonoBehaviour
    {
        [SerializeField] private Shelf[] shelfPrefabs;
        [SerializeField] private Transform[] shelfSlots;
        [SerializeField] private ItemListSO itemListSO;

        [SerializeField] private float oneShelfMark;
        [SerializeField] private float twoShelfMark;
        [SerializeField] private float threeShelfMark;

        [SerializeField] private float spawnInterval = 10f;
        private float timeSinceLastSpawned = 0;

        private List<Shelf> spawnedShelves;

        private int shelfRange = 0;

        private void Awake()
        {
            GameManager.Instance.ShelfSpawner = this;
        }

        private void Start()
        {
            SpawnShelf();
        }

        private void Update()
        {
            timeSinceLastSpawned += Time.deltaTime;

            if (timeSinceLastSpawned > spawnInterval)
            {
                timeSinceLastSpawned = 0;
                SpawnShelf();
            }

            if (GameManager.Instance.GetTimeSinceGameStart() < oneShelfMark)
            {
                shelfRange = 0;
            }
            else if (GameManager.Instance.GetTimeSinceGameStart() > oneShelfMark && GameManager.Instance.GetTimeSinceGameStart() < twoShelfMark)
            {
                shelfRange = 1;
            }
            else
            {
                shelfRange = 2;
            }
        }

        private void SpawnShelf()
        {
            if (shelfPrefabs.Length == 0) return;

            foreach (Transform slot in shelfSlots)
            {
                if (slot == null || slot.childCount > 0) continue;

                int availablePrefabs = Mathf.Min(shelfRange + 1, shelfPrefabs.Length);
                int randomIndex = Random.Range(0, availablePrefabs);

                Shelf spawnedShelf = Instantiate(
                    shelfPrefabs[randomIndex],
                    slot.position,
                    slot.rotation,
                    slot
                );

                spawnedShelf.Init(itemListSO);
                break;
            }
        }
    }
}