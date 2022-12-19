using UnityEngine;

namespace Scenes.Brick_Breaker._Scripts {
    public class MissZone : MonoBehaviour {
        public void OnCollisionEnter2D(Collision2D other) {
            if (!other.gameObject.CompareTag("Ball")) return;
            FindObjectOfType<GameManager>().Miss();
        }
    }
}
