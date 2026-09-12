using RF.GameLoop;
using RF.Items;
using UnityEngine;

namespace RF.Core
{
    public class ObjectSpawner : MonoBehaviour
    {
        [SerializeField] private ItemSO[] itemsArray;

        [SerializeField] private float spawnInterval = 10f;

        [SerializeField] private float minX;
        [SerializeField] private float maxX;

        private float timeSinceSpawned = Mathf.Infinity;
        private BoxCollider2D boxCollider2D;

        private void Awake()
        {
            boxCollider2D = GetComponent<BoxCollider2D>();

            minX = -boxCollider2D.size.x / 2;
            maxX = boxCollider2D.size.x / 2;
        }
        private void Update()
        {
            timeSinceSpawned += Time.deltaTime;

            if (timeSinceSpawned >= spawnInterval)
            {
                timeSinceSpawned = 0f;
                SpawnKitchenObject();
            }
        }

        private void SpawnKitchenObject()
        {
            float spawnPosX = Random.Range(minX, maxX);
            float spawnPosY = transform.position.y;

            int randomIndex = Random.Range(0, itemsArray.Length);

            Instantiate(itemsArray[randomIndex].GetPrefab(), new Vector2(spawnPosX, spawnPosY), Quaternion.identity);
        }
    }
}
