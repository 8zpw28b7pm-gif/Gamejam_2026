using RF.GameLoop;
using UnityEngine;

namespace RF.Core
{
    public class DestroyTrigger : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.CompareTag("KitchenObject"))
            {
                
            }
        }

    }
}
