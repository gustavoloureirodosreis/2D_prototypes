using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes.Brick_Breaker._Scripts  {
    public class GameManager : MonoBehaviour  {

        public int level = 1;
        public int score = 0;
        public int lives = 3;
        public Ball ball;
        public Paddle paddle;
    
        private void Awake() {
            DontDestroyOnLoad(this.gameObject);
            SceneManager.sceneLoaded += OnLevelLoaded;
        }

        private void OnLevelLoaded(Scene arg0, LoadSceneMode arg1) {
            ball = FindObjectOfType<Ball>();
            paddle = FindObjectOfType<Paddle>();
        }

        private void Start() {
            NewGame();
        }

        private void NewGame() {
            score = 0;
            lives = 3;
            LoadLevel(1);
        }

        private void LoadLevel(int levelIndex) {
            SceneManager.LoadScene("Level"+levelIndex);
        }

        public void Miss() {
            lives--;
            if (lives <= 0) {
                NewGame();
            } else {
                ResetPositions();
            }
        }

        private void ResetPositions() {
            ball.ResetPosition();
            paddle.ResetPosition();
        }
    }
}
