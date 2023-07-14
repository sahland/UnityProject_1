using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField] private GameObject[] hearts;

        public void SetHealth(int health)
        {
            if (health > hearts.Length) return;

            for (int i = 0; i < hearts.Length; i++)
            {
                hearts[i].SetActive(health > i);
            }
        }
    }
}
