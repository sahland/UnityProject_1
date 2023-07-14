using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial
{
    public class movement : MonoBehaviour
    {
        [SerializeField] private float reloadTime;
        [SerializeField] private int startHealth;
        [SerializeField] private PlayerUI ui;

        private Animator animator;
        public float Force;
        public CharacterController controller;
        public float smoothTime;
        float smoothVelocity;
        public Transform firstCamera;
        private bool canHit = true;
        private int health;

        private void Start()
        {
            animator = GetComponent<Animator>();

            health = startHealth;
        }

        private void Update()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (direction.magnitude >= 0.1f)
            {
                float rotationAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + firstCamera.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationAngle, ref smoothVelocity, smoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                Vector3 move = Quaternion.Euler(0f, rotationAngle, 0f) * Vector3.forward;


                controller.Move(move.normalized * Force * Time.deltaTime);
                animator.SetBool("walking", true);
            }
            if (direction.magnitude <= 0f)
            {
                animator.SetBool("walking", false);
            }

            if (Input.GetMouseButton(0) && canHit == true)
            {
                animator.SetTrigger("hit");
                StartCoroutine(Reload());
            }
        }

        IEnumerator Reload()
        {
            canHit = false;
            yield return new WaitForSeconds(reloadTime);
            canHit = true;
        }

        public void GetDamage(int damage)
        {
            health -= damage;
            ui.SetHealth(health);

            if(health <= 0f)
            {
                Debug.Log("Game over");
            }
        }
    }
}