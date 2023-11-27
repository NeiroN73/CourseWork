using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed = 5f; // �������� ������������
    public float jumpForce = 5f; // ���� ������
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
        // �������� �������� �������������� ��� (-1 ��� ������� "A" ��� ������� �����, 1 ��� ������� "D" ��� ������� ������)
        float moveHorizontal = Input.GetAxis("Horizontal");

        // ��������
        if (moveHorizontal != 0)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        // ����������� ������ �� �������������� ���
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

        // ���������, ���� �� ������ ������� ������
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            
        }

        // �������� ������
        if(IsGrounded())
        {
            animator.SetBool("isJumping", false);
        }
        else
        {
            animator.SetBool("isJumping", true);
        }

        // ��������� ������, ���� ���� ������ ������� ������ � ����� ��������� �� �����
        if (isJumping && IsGrounded())
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isJumping = false;
        }

        //����
        if (key)
        {
            key.FollowPlayer(placeForKey.position);
        }
        //������� ��� ���� �� �����
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
        // ���������, ��������� �� ����� �� �����
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
