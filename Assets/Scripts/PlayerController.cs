using System;
using UnityEngine;

//1. 점프 뛰기 : 위로 속도를 가지게 한다. -> Rigidbody 속도 설정
//2. 애니메이션 제어
//3. 죽음 처리
public class PlayerController : MonoBehaviour
{
    [Header("점프 기능")] 
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] [Range(0f, 20f)]float jumpSpeed;
    [SerializeField] AudioSource jumpSound;
    [SerializeField] int jumpCount;
    [SerializeField] bool isGrounded; 
    [Header("애니메이션")]
    [SerializeField] Animator animator;
    [Header("게임 오버")]
    [SerializeField] AudioSource dieSound;

   

    private void Awake()
    {
        // Vector2.MoveTowards();
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetButtonDown("Jump") && jumpCount <= 1)
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
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Platform")
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
