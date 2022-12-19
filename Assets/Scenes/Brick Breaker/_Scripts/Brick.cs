using System;
using UnityEngine;

namespace Scenes.Brick_Breaker._Scripts {
    public class Brick : MonoBehaviour {
        
        public int health;
        public bool breakable;

        private void Start() {
            health = breakable ? 1 : 100;
        }

        private void TakeDamage(int damage) {
            health -= damage;
            if (health <= 0) {
                Destroy(gameObject);
            }
        }
        
        public void OnCollisionEnter2D(Collision2D other) {
            if (!other.gameObject.CompareTag("Ball")) return;
            if (breakable) {
                TakeDamage(1);
            }
        }
    }
}
