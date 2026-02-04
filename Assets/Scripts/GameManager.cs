using System.Collections;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    int p1Score = 0;
    int p2Score = 0;
    
    [SerializeField] int MAX_POINT = 5;
    bool running = false;

    [SerializeField] GameObject pelota;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] TMP_Text p1ScoreText;
    [SerializeField] TMP_Text p2ScoreText;

    [SerializeField] TMP_Text message;


    public void AddPointToPlayer1()
    {
        p1Score++;
        p1ScoreText.text = p1Score.ToString();
    }   
    public void AddPointToPlayer2()
    {
        p2Score++;
        p2ScoreText.text = p2Score.ToString();
    }

    private void resetGame()
    {
        message.text = "";
        running=true;
        p1Score = 0;
        p1ScoreText.text = p1Score.ToString();
        
        p2Score = 0;
        p2ScoreText.text = p2Score.ToString();

        pelota.SetActive(true);
        
    }
    
    void Start()
    {
        message.text = "Jugador 1: WD - Gana quien llegue a "+  MAX_POINT + " puntos - Jugador 2 Up/Down";
        Cursor.visible = false;   
    }

    // Update is called once per frame
    void Update()
    {
        if(!running){
            if(Input.GetKeyDown("space")){
                resetGame();
            } 
        }
        if(Input.GetKeyDown(KeyCode.Escape)){
            running = false;
            Application.Quit();   
        }


        if(p1Score == MAX_POINT)
        {
            pelota.SetActive(false);
            running = false;
            message.text = "Ganó jugador 1";

        }
        if(p2Score == MAX_POINT)
        {
            pelota.SetActive(false);
            message.text = "Ganó jugador 2";
            running = false;
        }
    
    }
}
