using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    public enum Player {Blue, Red};
    bool isPlaying;
    public Player turn = Player.Red;
    public int force;
    Rigidbody2D rigid;
    int scoreP1 = 0, scoreP2 = 0;    
    Text scoreUIP1, scoreUIP2, tutorial;
    public GameObject gameOverPanel;
    public AudioClip audioBounce;
    public AudioClip audioScore;
    AudioSource MediaPlayer;
    MainMusic mainMusic;
 
    // Use this for initialization    
    void Start () {
        rigid = GetComponent<Rigidbody2D>();
        scoreUIP1 = GameObject.Find ("Score P1").GetComponent<Text> ();
        scoreUIP2 = GameObject.Find ("Score P2").GetComponent<Text> ();
        tutorial = GameObject.Find ("Tutorial").GetComponent<Text> ();
        MediaPlayer = gameObject.GetComponent<AudioSource>();
        ResetBall();

        GameObject go = GameObject.FindGameObjectWithTag("MainMusic");
        if (go !=null ) mainMusic = go.GetComponent<MainMusic>();
        if (mainMusic !=null ) mainMusic.PlayMusic();
    }

    void ResetBall() {
        isPlaying = false;
        if (turn == Player.Red) {
            transform.localPosition = new Vector2(-2, 0);
        } else {
            transform.localPosition = new Vector2(2, 0);
        }
        rigid.velocity = new Vector2(0, 0);
    }
  
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Space) && !isPlaying) {
            tutorial.gameObject.SetActive(false);
            isPlaying = true;
            Vector2 arah;
            if (turn == Player.Red) {
                arah = new Vector2(2, 0).normalized;
            } else {
                arah = new Vector2(-2, 0).normalized;
            }
            rigid.AddForce(arah * force);
        }
    }
 
    private void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.name == "Right Edge") {
            MediaPlayer.PlayOneShot(audioScore);
            scoreP1 += 1;
            scoreUIP1.text = scoreP1 + "";
            if (scoreP1 == 5) {
                gameOverPanel.SetActive(true);
                gameOverPanel.GetComponent<AudioSource>().Play();
                Text txWinner = gameOverPanel.transform.Find("Text").GetComponent<Text>();
                txWinner.text = "Red Player Win!";
                Destroy(gameObject);
                if (mainMusic !=null ) mainMusic.StopMusic();
                return;
            }
            turn = Player.Blue;
            ResetBall();
        }
        if (coll.gameObject.name == "Left Edge") {
            MediaPlayer.PlayOneShot(audioScore);
            scoreP2 += 1;
            scoreUIP2.text = scoreP2 + "";
            if (scoreP2 == 5) {
                gameOverPanel.SetActive(true);
                gameOverPanel.GetComponent<AudioSource>().Play();
                Text txWinner = gameOverPanel.transform.Find("Text").GetComponent<Text>();
                txWinner.text = "Blue Player Win!";
                Destroy(gameObject);
                if (mainMusic !=null ) mainMusic.StopMusic();
                return;
            }
            turn = Player.Red;
            ResetBall();
        }
        if (coll.gameObject.name == "Player 1" || coll.gameObject.name == "Player 2") {
            float sudut = (transform.position.y - coll.transform.position.y) * 5f;
            Vector2 arah = new Vector2(rigid.velocity.x, sudut).normalized;
            rigid.velocity = new Vector2(0, 0);    
            rigid.AddForce(arah * force * 2);
        }
        MediaPlayer.PlayOneShot(audioBounce);
    }
}