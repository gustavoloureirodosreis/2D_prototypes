using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Wordle.Scripts {
    public class PlayerController : MonoBehaviour
    {
        // A list populated with the text components of the keyboard letters
        public List<Button> keyboardCharacterButtons = new List<Button>();
        // Reference to gameController
        public GameController gameController;

        // All characters in the keyboard, named from top row to bottom row
        private string _characterNames = "QWERTYUIOPASDFGHJKLZXCVBNM";

        // Start is called before the first frame update
        void Start() {
            SetupButtons();
        }

        void SetupButtons() {
            // Starting from the top row, set the text of the keyboard-texts to the ones in the list
            for (int i = 0; i < keyboardCharacterButtons.Count; i++) {
                // Here we use GetChild and then GetComponent, it's not the most efficient way performance wise.
                // For setting things up and one shots it is usually fine, but doing it every frame inside of
                // Update() for example is not good practice and might give you dips in performance. Just a tip!
                keyboardCharacterButtons[i].transform.GetChild(0).GetComponent<TMP_Text>().text = _characterNames[i].ToString();
            }

            // Whenever we click a button, run the function ClickCharacter and output the character to the Console.
            foreach (var keyboardButton in keyboardCharacterButtons) {
                string letter = keyboardButton.transform.GetChild(0).GetComponent<TMP_Text>().text;
                keyboardButton.GetComponent<Button>().onClick.AddListener(() => ClickCharacter(letter));
            }
        }

        void ClickCharacter(string letter) {
            gameController.AddLetterToWordBox(letter);
        }

        // Update is called once per frame
        void Update() {
        
        }
    }
}
