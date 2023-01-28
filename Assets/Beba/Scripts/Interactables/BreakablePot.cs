using UnityEngine;

namespace bebaSpace
{
    public class BreakablePot : MonoBehaviour, IDamageable
    {
        [SerializeField] private LootTable lootTable;

        private Animator animator;
        private Collider2D potCollider;

        private void Start()
        {
            animator = GetComponent<Animator>();
            potCollider = GetComponent<Collider2D>();
        }

        public void TakeDamage(float damage)
        {
            animator.SetTrigger("pot_break");
            potCollider.enabled = false;

            lootTable.GenerateLoot(lootTable, gameObject.transform);
        }

      
    }
}
