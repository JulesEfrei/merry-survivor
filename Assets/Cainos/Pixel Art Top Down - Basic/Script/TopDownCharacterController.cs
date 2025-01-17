using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cainos.PixelArtTopDown_Basic
{
    public class TopDownCharacterController : MonoBehaviour
    {
        public float speed;
        public float attackRatio = 1;

        private Animator animator;
        public GameObject fireballPrefab;
        public float fireCooldown = 0.5f;
        private float nextFireTime = 0f;
        private Vector2 movementDirection;
        private Vector2 lastMovementDirection = Vector2.down;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }


        private void Update()
        {
            Vector2 dir = Vector2.zero;
            if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow))
            {
                dir.x = -1;
                animator.SetInteger("Direction", 3);
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                dir.x = 1;
                animator.SetInteger("Direction", 2);
            }

            if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.UpArrow))
            {
                dir.y = 1;
                animator.SetInteger("Direction", 1);
            }
            else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                dir.y = -1;
                animator.SetInteger("Direction", 0);
            }

            movementDirection = dir.normalized;

            if (movementDirection != Vector2.zero)
            {
                lastMovementDirection = movementDirection;
            }


            HandleShooting();

            dir.Normalize();
            animator.SetBool("IsMoving", dir.magnitude > 0);

            GetComponent<Rigidbody2D>().linearVelocity = speed * dir;
        }

        private void HandleShooting()
        {
            if (Input.GetKey(KeyCode.Space) && Time.time > nextFireTime)
            {
                nextFireTime = Time.time + fireCooldown;
                ShootFireball();
            }
        }

        private void ShootFireball()
        {
            //Position
            Vector2 fireballPositionOffset = Vector2.zero;

            //Direction
            Vector2 fireballDirection = lastMovementDirection;

            if (Input.GetKey(KeyCode.O))
            {
                fireballDirection = Vector2.up;
            }
            else if (Input.GetKey(KeyCode.K))
            {
                fireballDirection = Vector2.left;
            }
            else if (Input.GetKey(KeyCode.L))
            {
                fireballDirection = Vector2.down;
            }
            else if (Input.GetKey(KeyCode.M))
            {
                fireballDirection = Vector2.right;
            }

            fireballPositionOffset = fireballDirection.normalized * 0.7f;
            Vector2 fireballPosition = (Vector2)this.gameObject.transform.position + fireballPositionOffset;

            //Instanciate
            GameObject fireball = Instantiate(fireballPrefab, fireballPosition, Quaternion.identity);

            //Fix layer
            SpriteRenderer fireballRenderer = fireball.GetComponent<SpriteRenderer>();
            if (fireballRenderer != null)
            {
                fireballRenderer.sortingLayerName = LayerMask.LayerToName(this.gameObject.layer);
            }

            FireballShot fireballShot = fireball.GetComponent<FireballShot>();
            if (fireballShot != null)
            {
                fireballShot.SetDirection(fireballDirection.normalized);
            }
        }

    }
}
