using UnityEngine;
using UnityEngine.InputSystem;

namespace Scenes.Brick_Breaker_2._Scripts {
    public class PaddleController : MonoBehaviour {

        private Rigidbody2D _myRb;
        private Vector2 _direction;
        private Vector2 _myMovement;
        public float speed = 30f;
        
        void Start() {
            _myRb = GetComponent<Rigidbody2D>();
        }

        private void OnMove(InputValue movement) {
            _myMovement = movement.Get<Vector2>();
        }

        void Update() {
            if (_myMovement.x < 0) {
                _direction = Vector2.left;
            } else if(_myMovement.x > 0) {
                _direction = Vector2.right;
            } else {
                _direction = Vector2.zero;
            }
        }
        
        void FixedUpdate() {
            if(_direction == Vector2.zero) {
                return;
            }
            
            _myRb.AddForce(_direction * speed);
        }
    }
}
