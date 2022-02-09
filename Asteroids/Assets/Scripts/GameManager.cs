using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


    public class GameManager : MonoBehaviour
    {
        public Player player;

        public ParticleSystem explosion;
        public float respawnTime = 3.0f;
        public float respawnInvulnerabilityTime = 3.0f;
        
        public int lives = 3;
        public int score = 0;
        
        public Text scoreText;
        public Text livesImage;
        public Text highScoreListText;
        public GameObject gameOverPanel;
        public GameObject newHighScorePanel;
        public InputField highScoreInput;
        public GameManager gm;
        

        public void Start()
        {
            scoreText.text = "" + score;
            livesImage.text = "" + lives;

        }
        public void AsteroidsDestroyed(Asteroid asteroid)
        {
            explosion.transform.position = asteroid.transform.position;
            explosion.Play();

            if (asteroid.size < 0.75f)
                score += 100;
            else if (asteroid.size < 1.2f)
                score += 50;
            else
                score += 25;
            scoreText.text = "" + score;
        }
        
        private void ScorePoints(int pointsToAdd)
        {
            score += pointsToAdd;
            scoreText.text = "" + score;
        }
        public void PlayerDied()
        {
            explosion.transform.position = player.transform.position;
            explosion.Play();
        
            lives--;
            livesImage.text = "" + lives;
            if (lives < 0)
            {
                GameOver();
            }else{
                Invoke(nameof(Respawn), respawnTime);
            }
        
        }

        
        private void Respawn()
        {
            if (lives >= 0)
            {
                player.transform.position = Vector3.zero;
                player.gameObject.layer = LayerMask.NameToLayer("Invicibility");
                player.gameObject.SetActive(true);
       
                Invoke(nameof(TurnOnCollision), respawnInvulnerabilityTime);
            }
            
        }

        private void TurnOnCollision()
        {
            player.gameObject.layer = LayerMask.NameToLayer("Player");
        }

        private void GameOver()
        {
                        
            Invoke(nameof(Respawn), respawnTime);
            //Tell GameManager to check for high scores
            if (gm.CheckForHightScore(score))
            {
                newHighScorePanel.SetActive(true);
            }
            else
            {
                gameOverPanel.SetActive(true);
                highScoreListText.text = "High Score" + "\n" + "\n" + PlayerPrefs.GetString("highscoreName") + " " + PlayerPrefs.GetInt("highscore");
            }
            
        }

        public void HighScoreInput()
        {
            string newInput = highScoreInput.text;
            Debug.Log (newInput);
            newHighScorePanel.SetActive(false);
            gameOverPanel.SetActive(true);
            PlayerPrefs.SetString("highscoreName", newInput);
            PlayerPrefs.SetInt("highscore", score);
            highScoreListText.text = "High Score" + "\n" + "\n" + PlayerPrefs.GetString("highscoreName") + " " + PlayerPrefs.GetInt("highscore");

        }
        

        public void Restart()
        {
            SceneManager.LoadScene("Game");
        }

        public void MainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public bool CheckForHightScore(int score)
        {
            int highScore = PlayerPrefs.GetInt("highscore");
            if (score > highScore)
            {
                return true;
            }
            return false;
        }

    }


