using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Image _LivesImg;
    [SerializeField]
    private Sprite[] _livesSprites;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartGame;
    private GameManager _gameManager;
    // Start is called before the first frame update
    void Start() {
        _scoreText.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);
        _restartGame.gameObject.SetActive(false);
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (_gameManager == null) {
            Debug.LogError("GameManager is NULL.");
        }
    }

    public void UpdateScore(int playerScore) {
        _scoreText.text = "Score: " + playerScore.ToString();
    }

    public void UpdateLives(int CurrentLives) {
        _LivesImg.sprite = _livesSprites[CurrentLives];
        if (CurrentLives == 0) {
            GameOver();
        }
    }

    void GameOver() {
        _gameManager.GameOver();
        _gameOverText.gameObject.SetActive(true);
        _restartGame.gameObject.SetActive(true);
        StartCoroutine(GameOverFlicker());
    }

    IEnumerator GameOverFlicker() {
        while (true) {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
