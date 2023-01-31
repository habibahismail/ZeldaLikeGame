using UnityEngine;

namespace bebaSpace
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private Rigidbody2D rb;


        public void Setup(Vector2 velocity, Vector3 direction)
        {
            rb.velocity = velocity.normalized * speed;
            transform.rotation = Quaternion.Euler(direction);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
                if(!collision.isTrigger)
                    Destroy(gameObject);
        }

        public Vector3 ProjectileDirection(float x, float y)
        {
            float zRotation = Mathf.Atan2(y,x) * Mathf.Rad2Deg;

            return new Vector3(0, 0, zRotation);
        }

    }
}
