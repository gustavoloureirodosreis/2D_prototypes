using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Scenes.Snake._Scripts {
    public class SnakeController : MonoBehaviour  {
    
        [SerializeField] private Transform bodyPrefab; 
        [SerializeField] private List<Transform> childList;
        [SerializeField] private Transform applePrefab; 
        private Transform _appleInGame;
        private Vector3 _direction;
        private Vector3 _savedDirection;
        private Vector3 _target;
        
        public const float Vel = 5f;

        // Start is called before the first frame update
        void Start() {
            _target = transform.position;
            _direction = Vector3.right;
            _appleInGame = SpawnApple();
        }

        private void OnMove(InputValue movement) {
            _direction = movement.Get<Vector2>();
        }
        // Update is called once per frame
        void Update() {
            transform.position = Vector3.MoveTowards(transform.position, _target, Vel * Time.deltaTime);
        
            if(_direction.x != 0) {
                _savedDirection = Vector3.right * _direction.x;
            }
            if (_direction.y != 0) {
                _savedDirection = Vector3.up * _direction.y;
            }

            if (transform.position != _target) return;
            
            _target += _savedDirection;
            ChecksOutOfBounds();
            SetChildrenTargets();
        }
        
        private void SetChildrenTargets() {
            if (childList.Count > 0) {
                childList[0].GetComponent<SnakeBody>().SetTargetPosition(transform.position); // Set head target
                
                for(int index = childList.Count-1; index > 0; index--) {
                    childList[index].GetComponent<SnakeBody>().SetTargetPosition(childList[index-1].position); // Set body targets
                }
            }
        }

        private void ChecksOutOfBounds() {
            if (_target.x is >= 9 or <= -9 || _target.y >= 4.8 || _target.y <= -4.8) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        private void OnTriggerEnter2D(Collider2D col) {
            if (col.CompareTag("Food")) {
                EatsFood();
            } if (col.CompareTag("Body")) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        private void EatsFood() {
            Destroy(_appleInGame.gameObject);
            
            var obj = Instantiate(bodyPrefab, transform.position, Quaternion.identity);
            obj.GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(ActivateBodyCollider(obj));
            obj.GetComponent<SnakeBody>().WaitHeadUpdateCycles(childList.Count);
            childList.Add(obj);
            
            _appleInGame = SpawnApple();
        }

        IEnumerator ActivateBodyCollider(Transform obj) {
            yield return new WaitForSeconds(0.5f);
            obj.GetComponent<BoxCollider2D>().enabled = true;
        }


        private Transform SpawnApple() {
            return Instantiate(applePrefab, new Vector3(Random.Range(-8, 8), Random.Range(-4, 4), 0), Quaternion.identity);
        }
    }
}