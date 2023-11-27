using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed = 5f; // Скорость передвижения
    public float jumpForce = 5f; // Сила прыжка
    public LayerMask groundLayerMask;
    public float timeBetweenSteps;
    public ParticleSystem stepEffect;
    public Transform groundCheck;
    public float groundCheckRadius;

    private bool isJumping = false;
    private Rigidbody2D rb;
    private Animator animator;
    private float currentTimerSteps;

    private bool isKeyHaving = false;
    public Transform placeForKey;
    private Key key;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentTimerSteps = timeBetweenSteps;
    }

    private void Update()
    {
        // Получаем значение горизонтальной оси (-1 для клавиши "A" или стрелки влево, 1 для клавиши "D" или стрелки вправо)
        float moveHorizontal = Input.GetAxis("Horizontal");

        // Анимация
        if (moveHorizontal != 0)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        // Передвигаем игрока по горизонтальной оси
        Vector2 movement = new Vector2(moveHorizontal * speed, rb.velocity.y);
        rb.velocity = movement;

        if(moveHorizontal < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if(moveHorizontal > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        // Проверяем, была ли нажата клавиша прыжка
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            
        }

        // Анимация прыжка
        if(IsGrounded())
        {
            animator.SetBool("isJumping", false);
        }
        else
        {
            animator.SetBool("isJumping", true);
        }

        // Выполняем прыжок, если была нажата клавиша прыжка и игрок находится на земле
        if (isJumping && IsGrounded())
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isJumping = false;
        }

        //ключ
        if (key)
        {
            key.FollowPlayer(placeForKey.position);
        }
        //частицы при беге по траве
        if (IsGrounded() && moveHorizontal != 0)
        {
            currentTimerSteps -= Time.deltaTime;
            if (currentTimerSteps <= 0)
            {
                currentTimerSteps = timeBetweenSteps;
                Instantiate(stepEffect, groundCheck.position, Quaternion.identity);
            }
        }
    }

    private bool IsGrounded()
    {
        // Проверяем, находится ли игрок на земле
        var hit = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayerMask);
        var check = hit ? true : false;
        return check;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Key key))
        {
            this.key = key;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Door door))
        {
            if (key != null)
            {
                Dead();
            }
        }
    }

    public void Dead()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
