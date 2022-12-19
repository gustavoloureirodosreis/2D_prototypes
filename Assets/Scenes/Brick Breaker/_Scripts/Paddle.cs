using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scenes.Brick_Breaker._Scripts {
    public class Paddle : MonoBehaviour {
        
        private Rigidbody2D _myRb;
        private Vector2 _direction;
        private Vector2 _movementInput;
        public float speed = 30f;
        public float maxBounceAngle = 75f;

        private void Awake() {
            _myRb = GetComponent<Rigidbody2D>();
        }

        private void OnMove(InputValue movement) {
            _movementInput = movement.Get<Vector2>();
        }

        private void Update() {
            if (_movementInput.x < 0) {
                _direction = Vector2.left;
            } else if(_movementInput.x > 0) {
                _direction = Vector2.right;
            } else {
                _direction = Vector2.zero;
            }
        }

        private void FixedUpdate() {
            if(_direction == Vector2.zero) {
                return;
            }
            
            _myRb.AddForce(_direction * speed);
        }

        private void OnCollisionEnter2D(Collision2D col) {
            Ball ball = col.gameObject.GetComponent<Ball>();

            if (!ball) return;
            
            Vector3 paddlePos = transform.position;
            Vector2 contactPoint = col.contacts[0].point;
                
            float offsetX = contactPoint.x - paddlePos.x;
            float width = col.otherCollider.bounds.size.x;

            float currentAngle = Vector2.SignedAngle(Vector2.up, ball.MyRb.velocity);
            float bounceAngle = (offsetX / width) * maxBounceAngle;
            float newAngle = Mathf.Clamp(currentAngle + bounceAngle, -maxBounceAngle, maxBounceAngle);

            Quaternion rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);
            ball.MyRb.velocity = rotation * Vector2.up * ball.MyRb.velocity.magnitude;
        }

        public void ResetPosition() {
            transform.position = new Vector2(0, -8);
            _myRb.velocity = Vector2.zero;
        }
    }
}
