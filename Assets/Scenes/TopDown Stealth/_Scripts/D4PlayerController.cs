using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scenes.TopDown_Stealth._Scripts {
    public class D4PlayerController : MonoBehaviour {
        const string PlayerIdleDown = "IdleDown";
        const string PlayerIdleLeft = "IdleLeft";
        const string PlayerIdleRight = "IdleRight";
        const string PlayerIdleUp = "IdleUp";
        const string PlayerWalkDown = "WalkDown";
        const string PlayerWalkLeft = "WalkLeft";
        const string PlayerWalkRight = "WalkRight";
        const string PlayerWalkUp = "WalkUp";

        private Rigidbody2D _rb;
        private Vector2 _movementInput;
        private Animator _animator;
        private string _currentState;
        private string _currentFacing;

        public float speed = 1.5f;
        public float horizontalInput;
        public float verticalInput;
        
        // Start is called before the first frame update
        void Start() {
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }
        
        private void OnMove(InputValue movementValue) {
            _movementInput = movementValue.Get<Vector2>();
        }

        // Update is called once per frame
        void Update() {
            horizontalInput = _movementInput.x;
            verticalInput = _movementInput.y;
        }

        private void FixedUpdate() {
            if(horizontalInput != 0 || verticalInput != 0) {
                _rb.velocity = new Vector2(horizontalInput * speed, verticalInput * speed);
                DecideNextAnimationState(horizontalInput, verticalInput);
            } else {
                StopMovement();
            }
        }

        private void StopMovement() {
            _rb.velocity = Vector2.zero;
            ChangeAnimationState(_currentFacing);
        }

        private void DecideNextAnimationState(float hor, float ver) {
            switch (hor) {
                case > 0:
                    ChangeAnimationState(PlayerWalkRight);
                    _currentFacing = PlayerIdleRight;
                    break;
                case < 0:
                    ChangeAnimationState(PlayerWalkLeft);
                    _currentFacing = PlayerIdleLeft;
                    break;
                default: {
                    if (ver > 0) {
                        ChangeAnimationState(PlayerWalkUp);
                        _currentFacing = PlayerIdleUp;
                    } else if (ver < 0) {
                        ChangeAnimationState(PlayerWalkDown);
                        _currentFacing = PlayerIdleDown;
                    }
                    break;
                }
            }
        }

        private void ChangeAnimationState(string newState) {
            if(_currentState == newState) return;

            _animator.CrossFade(newState, 0.2f);
            _currentState = newState;
        }
    }
}
