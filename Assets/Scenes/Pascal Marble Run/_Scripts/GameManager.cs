using System.Collections;
using UnityEngine;

namespace Scenes.Pascal_Marble_Run._Scripts  {
    public class GameManager : MonoBehaviour {

        [SerializeField] private GameObject ball;
        [SerializeField] private int amountToSpawn;
        [SerializeField] private Transform spawnPoint;
        
        void Start() {
            StartCoroutine(SpawnBalls());
        }

        IEnumerator SpawnBalls() {
            var totalSpawned = 0;
            
            while (totalSpawned < amountToSpawn) {
                var position = spawnPoint.position;
                Instantiate(ball, new Vector3(Random.Range(-0.05f, 0.05f), position.y, 0f), Quaternion.identity);
                yield return new WaitForSeconds(0.1f);
                totalSpawned++;
            }

        }        // Update is called once per frame
    }
}
