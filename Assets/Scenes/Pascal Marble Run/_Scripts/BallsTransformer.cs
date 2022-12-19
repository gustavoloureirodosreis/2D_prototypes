using UnityEngine;

namespace Scenes.Pascal_Marble_Run._Scripts {
    public class BallsTransformer : MonoBehaviour {

        [SerializeField] private GameObject bigBall;
        [SerializeField] Vector2 topLeft, bottomRight;

        private void OnTriggerEnter2D(Collider2D col) {
            if (!col.gameObject.CompareTag("Ball")) return;

            var results = Physics2D.OverlapAreaAll(topLeft, bottomRight);

            if (results.Length % 5 == 0) {
                foreach (var ballCollider in results) {
                    if (ballCollider.gameObject.CompareTag("Ball")) {
                        Destroy(ballCollider.gameObject);
                    }
                }
                Instantiate(bigBall, transform.position, Quaternion.identity);
            }
        }
    }
}
