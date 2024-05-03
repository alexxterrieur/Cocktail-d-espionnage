using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 1000f;
    private Vector2 mouvementInput;
    private Vector2 direction;

    [SerializeField] private Vector2 boxSize;
    [SerializeField] private float castDistance;
    [SerializeField] private LayerMask obstacleLayer;
    private CircleCollider2D playerCollider;
    private Rigidbody2D rb2D;
    private Animator animator;

    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite spriteFront;
    [SerializeField] private Sprite spriteSide;
    [SerializeField] private Sprite spriteBack;

    private bool canMove = true;
    private bool canMovePanel = true;

    private Coroutine walkSfx;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        playerCollider = GetComponent<CircleCollider2D>();
    }

    void FixedUpdate()
    {
        if (!IsObstacle())
        {
            PerformMouvement();
        }
        // Debug.Log(SceneManager.GetSceneByName());
    }

    private void PerformMouvement()
    {
        if (canMove)
        {
            rb2D.AddForce(new Vector3(mouvementInput.x, mouvementInput.y, 0) * Time.deltaTime * speed);
        }
        animator.SetFloat("Velocity", rb2D.velocity.magnitude);

        if (rb2D.velocity.magnitude >= 0.1 && walkSfx == null)
        {
            walkSfx = StartCoroutine(WalkingAudio());
        }
    }

    public void Mouvement(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (canMove && canMovePanel )
            {
                mouvementInput = context.ReadValue<Vector2>();
                if (mouvementInput != Vector2.zero)
                {
                    //Keep the last direction set
                    direction = mouvementInput.normalized;
                }

                if (mouvementInput.x == 1)
                {
                    spriteRenderer.sprite = spriteSide;
                    spriteRenderer.flipX = false;
                }
                else if (mouvementInput.x == -1)
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
    }

    public bool IsObstacle()
    {
        if (Physics2D.BoxCast(playerCollider.bounds.center, boxSize, 0, mouvementInput, castDistance, obstacleLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public Vector2 GetDirection()
    {
        return direction;
    }

    public void SetCanMove(bool canMove)
    {
        this.canMove = canMove;
    }

    public void SetCanMovePanel(bool canMove)
    {
        this.canMovePanel = canMove;
    }

    private IEnumerator WalkingAudio()
    {
        S_SoundManager.Instance.PlaySoundEffect("Walking_SFX");
        yield return new WaitForSeconds(0.75f);
        walkSfx = null;
    }
}
