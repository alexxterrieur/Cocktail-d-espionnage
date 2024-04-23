using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1000f;
    private Vector2 mouvementInput;

    [SerializeField] private Vector2 boxSize;
    [SerializeField] private float castDistance;
    [SerializeField] private LayerMask obstacleLayer;
    private Rigidbody2D rb2D;
    private Animator animator;

    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite spriteFront;
    [SerializeField] private Sprite spriteSide;
    [SerializeField] private Sprite spriteBack;
    

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
    }

    void FixedUpdate()
    {
        if (!IsObstacle())
        {
            PerformMouvement();
        }
    }

    private void PerformMouvement()
    {
        rb2D.AddForce(new Vector3(mouvementInput.x, mouvementInput.y, 0) * Time.deltaTime * speed);
        animator.SetFloat("Velocity", rb2D.velocity.magnitude);
    }

    public void Mouvement(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            mouvementInput = context.ReadValue<Vector2>();
            if (mouvementInput.x == 1)
            {
                spriteRenderer.sprite = spriteSide;
                spriteRenderer.flipX = false;
            }
            else if(mouvementInput.x == -1)
            {
                spriteRenderer.sprite = spriteSide;
                spriteRenderer.flipX = true;
            }
            else if (mouvementInput.y == 1)
            {
                spriteRenderer.sprite = spriteBack;
            }
            else if (mouvementInput.y == -1)
            {
                spriteRenderer.sprite = spriteFront;
            }
        }
    }

    public bool IsObstacle()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, mouvementInput, castDistance, obstacleLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + (Vector3) mouvementInput * castDistance, boxSize);
    }*/
}
