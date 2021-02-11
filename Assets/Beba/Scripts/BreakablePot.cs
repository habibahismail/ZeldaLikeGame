using System.Collections;
using UnityEngine;

namespace bebaSpace
{
    public class BreakablePot : MonoBehaviour, IDamageable
    {
        private Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        public void TakeDamage(int damage)
        {
            animator.SetTrigger("pot_break");
            Destroy(gameObject, 0.3f);
        }

      
    }
}
