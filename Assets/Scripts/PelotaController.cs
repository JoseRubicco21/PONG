using System.Collections;
using UnityEngine;
using UnityEngine.Diagnostics;

public class BallController : MonoBehaviour
{

    private Rigidbody2D rb; 
    private AudioSource sfx;
    [SerializeField] float force;
    [SerializeField] float delay;

    [SerializeField] GameManager gameManager;
    [SerializeField] AudioClip paddleHitAudio;
    [SerializeField] AudioClip wallHitAudio;
    const float MIN_ANG = 25.0f; 
    const float MAX_ANG = 40.0f; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sfx = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        //throwBall();
        //Invoke("throwBall", delay);
        transform.position = new Vector3(0,0,0); //Vector3.zero;
         
         int directionX = Random.Range(0, 2) == 0 ? -1 : 1; // El límite superior es exclusivo (el 2 quedaría fuera).
         StartCoroutine(throwBall(directionX));
    }

    IEnumerator throwBall(int directionX){

        transform.position = new Vector3(0,0,0); //Vector3.zero;
        rb.linearVelocity = Vector2.zero;

        yield return new WaitForSeconds(delay);    
        
        float angulo = Random.Range(MIN_ANG, MAX_ANG) * Mathf.Deg2Rad;
        int directionY = Random.Range(0,2) == 0 ?-1:1;

        float x = Mathf.Cos(angulo) * directionX;
        float y = Mathf.Sin(angulo) * directionY;
         
        rb.AddForce(new Vector2(x,y) * force, ForceMode2D.Impulse);   

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        string tag = other.gameObject.tag;
        if(tag == "Pad" || tag == "Pad2"   )
        {
            sfx.clip = paddleHitAudio;
            sfx.Play();
            Debug.Log("Playing paddle hit");
        } else 
        {
            sfx.clip = wallHitAudio;
            sfx.Play();
            Debug.Log("Playing wall hit");

        }
    }

   private void OnTriggerEnter2D(Collider2D collider){
    Debug.Log("Gol en " +collider.tag + "!!");
    if(collider.tag.Equals("PorteriaIzquierda")){
        gameManager.AddPointToPlayer1();
        StartCoroutine(throwBall(1));
    }else if(collider.tag.Equals("PorteriaDerecha")){
        gameManager.AddPointToPlayer2();
        StartCoroutine(throwBall(-1));
    }

   
    
   }
}