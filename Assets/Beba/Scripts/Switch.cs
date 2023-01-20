using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace bebaSpace
{
    public class Switch : MonoBehaviour
    {
        [SerializeField] private bool isActivated;
        [SerializeField] private BoolValue isActivatedStoredValue;
        [SerializeField] private Sprite activeSprite;
        [SerializeField] private Door theDoor;

        private SpriteRenderer spriteRenderer;

        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();

            isActivated = isActivatedStoredValue.RuntimeValue;

            if (isActivated)
            {
                ActivateSwitch();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                ActivateSwitch();
            }
        }

        private void ActivateSwitch()
        {
            isActivated = true;
            isActivatedStoredValue.RuntimeValue = isActivated;
            theDoor.OpenTheDoor();
            spriteRenderer.sprite = activeSprite;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;

        }
    }
}
