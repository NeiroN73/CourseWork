using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator animator;
    private BoxCollider2D collider;

    private void Start()
    {
        animator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
    }

    public void Open()
    {
        animator.enabled = true;
        collider.isTrigger = true;
    }
}
