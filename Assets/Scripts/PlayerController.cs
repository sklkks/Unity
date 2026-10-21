using System;
using UnityEngine;

//1. 점프 뛰기 : 위로 속도를 가지게 한다. -> Rigidbody 속도 설정
//2. 애니메이션 제어
//3. 죽음 처리
public class PlayerController : MonoBehaviour
{
    [Header("점프 기능")]
    private Rigidbody2D rigid;
    
    [SerializeField] 
    [Range(0f, 20f)]
    private float jumpSpeed;
    
    [SerializeField]
    private AudioSource jumpSound;
    
    [SerializeField] 
    private int jumpCount;
    
    [SerializeField] 
    private bool isGrounded;
    
    [Header("애니메이션")]
    private Animator animator;
    
    [Header("게임 오버")]    
    [SerializeField] 
    private AudioSource dieSound;

    private GameManager gameManager;

    private void Awake()
    {
        // Vector2.MoveTowards();
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        GameObject findGo = GameObject.FindWithTag("GameController");
        gameManager = findGo.GetComponent<GameManager>();
    }
    private void Update()
    {//Input.GetMouseButtonDown("0")
        if (Input.GetButtonDown("Fire1") && jumpCount <= 1) //Input.GetButtonDown("Jump")
        // isGrounded == true) 점프 한 번만 가능함
        {
            Jump();            
        }
    }
    public void Jump()
    {
        rigid.linearVelocityY = jumpSpeed;
        jumpSound.Play();
        jumpCount++;        
    }
    public void Die()
    {
        animator.SetTrigger("Die");
        rigid.linearVelocity = Vector2.zero;
        rigid.simulated = false;
        dieSound.Play();
        gameManager.OnPlayerDead();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Platform" &&
           collision.contacts[0].normal.y > 0.7f)
        {
            isGrounded = true;        
            animator.SetBool("Grounded", true);
            jumpCount = 0;
        }        
    }
    void OnCollisionExit2D(Collision2D collision)
    {   
        if(collision.collider.tag == "Platform")
        {
            isGrounded = false;
            animator.SetBool("Grounded", false);            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Dead")
        {
            Die();
        }
    }
}