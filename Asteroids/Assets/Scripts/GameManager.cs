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
        public GameObject gameOverPanel;

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
            player.transform.position = Vector3.zero;
            player.gameObject.layer = LayerMask.NameToLayer("Invicibility");
            player.gameObject.SetActive(true);
       
            Invoke(nameof(TurnOnCollision), respawnInvulnerabilityTime);
        }

        private void TurnOnCollision()
        {
            player.gameObject.layer = LayerMask.NameToLayer("Player");
        }

        private void GameOver()
        {
            lives = 3;
            score = 0;
            Invoke(nameof(Respawn), respawnTime);
            gameOverPanel.SetActive(true);
        }

        public void Restart()
        {
            SceneManager.LoadScene("Game");
        }

    }


