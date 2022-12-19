using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

namespace Scenes.Wordle.Scripts {
    public class GameController : MonoBehaviour {
        
        public string correctWord;
        // All wordboxes
        public List<Transform> wordBoxes = new List<Transform>();
        
        // Current wordbox that we're inputting in
        private int _currentWordBox;
        // List with all the words
        private List<string> _dictionary = new List<string>();
        // List with words that can be chosen as correct words
        private List<string> _guessingWords = new List<string>();

        // Start is called before the first frame update
        void Start() {
            // Populate the dictionary
            AddWordsToList("Assets/Scenes/Wordle/Resources/dictionary.txt", _dictionary);

            // Populate the guessing words
            AddWordsToList("Assets/Scenes/Wordle/Resources/wordlist.txt", _guessingWords);
            
            // Choose a random correct word
            correctWord = GetRandomWord();
        }


        void AddWordsToList(string path, List<string> listOfWords) {
            // Read the text from the file
            StreamReader reader = new StreamReader(path);
            string text = reader.ReadToEnd();

            // Separate them for each ',' character
            char[] separator = { ',' };
            string[] singleWords = text.Split(separator);

            // Add everyone of them to the list provided as a variable
            foreach (string newWord in singleWords) {
                listOfWords.Add(newWord);
            }

            // Close the reader
            reader.Close();
        }
        
        string GetRandomWord() {
            string randomWord = _guessingWords[Random.Range(0, _guessingWords.Count)];
            Debug.Log(randomWord);
            return randomWord;
        }
        
        public void AddLetterToWordBox(string letter) {
            wordBoxes[_currentWordBox].GetChild(0).GetComponent<TMP_Text>().text = letter;
            _currentWordBox++;
        }
    }
}