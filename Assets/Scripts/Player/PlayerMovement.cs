using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 mouvementInput;

    [SerializeField] private Vector2 boxSize;
    [SerializeField] private float castDistance;
    [SerializeField] private LayerMask obstacleLayer;

    void FixedUpdate()
    {
        if (!IsObstacle())
        {
            PerformMouvement();
        }
    }

    private void PerformMouvement()
    {
        gameObject.transform.Translate(new Vector3(mouvementInput.x, mouvementInput.y, 0) * Time.deltaTime * speed);
    }

    public void Mouvement(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            mouvementInput = context.ReadValue<Vector2>();
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
