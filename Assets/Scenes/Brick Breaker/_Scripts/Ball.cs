using UnityEngine;

namespace Scenes.Brick_Breaker._Scripts {
    public class Ball : MonoBehaviour  {
    
        private Rigidbody2D _myRb;
        public Rigidbody2D MyRb => _myRb;

        public float speed = 500f;

        private void Awake() {
            _myRb = GetComponent<Rigidbody2D>();
        }

        private void Start() {
            Invoke(nameof(SetRandomTrajectory), 1f);
        }

        private void SetRandomTrajectory() {
            Vector2 force = Vector2.zero;
            force.x = Random.Range(-1f, 1f);
            force.y = -1;
        
            _myRb.AddForce(force.normalized * speed);
        }

        public void ResetPosition() {
            transform.position = new Vector2(0, -5);
            _myRb.velocity = Vector2.zero;
            Invoke(nameof(SetRandomTrajectory), 1f);
        }
    }
}
