using UnityEngine;

namespace Scenes.Snake._Scripts {
    public class SnakeBody : MonoBehaviour {
        private Vector3 _nextBodyPos;
        private int _waitPreviousParts;

        void Start() {
            _nextBodyPos = transform.position;
        }

        public void WaitHeadUpdateCycles(int value) {
            _waitPreviousParts = value;
        }
        
        public void SetTargetPosition(Vector3 targetPosition) {
            if (_waitPreviousParts > 0) {
                _waitPreviousParts--;
                return;
            }

            _nextBodyPos = targetPosition;
        }

        void Update() {
            transform.position = Vector3.MoveTowards(transform.position, _nextBodyPos, SnakeController.Vel * Time.deltaTime);
        }
    }
}
