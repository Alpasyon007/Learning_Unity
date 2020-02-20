using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberWizard : MonoBehaviour {
		int max_num = 1000;
		int min_num = 1;
		int guess_num = 500;
	// Start is called before the first frame update
	void Start() {
		StartGame();
	}

	void StartGame() {
		max_num = 1000;
		min_num = 1;
		guess_num = 500;
		Debug.Log("Welcome to number wizard, yo");
		Debug.Log("Pick a number between " + max_num + "and " + min_num + ", don't tell me what it is...");
		Debug.Log("Tell me if your number is higher or lower than " + guess_num);
		Debug.Log("Push Up = Higher, Push Down = Lower, Push Enter = Correct");
		max_num = max_num + 1;
	}

	// Update is called once per frame
	void Update() {
		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			min_num = guess_num;
			NextGuess();
		}
		else if (Input.GetKeyDown(KeyCode.DownArrow)) {
			max_num = guess_num;
			NextGuess();
		}
		else if (Input.GetKeyDown(KeyCode.Return)) { 
			Debug.Log("Enter key was preesed.");
			StartGame();
		};
	}

	void NextGuess() {
		guess_num = (max_num + min_num) / 2;
		Debug.Log("Up Arrow key was pressed. " + guess_num);
	}
}
