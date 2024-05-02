using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class S_EnemyAI : MonoBehaviour
{
    public GameObject player;
    public NavMeshAgent agent;
    public float closeRange;
    public float sightRange;
    public Transform[] waypoints;
    public Node start;

    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite spriteFront;
    [SerializeField] private Sprite spriteSide;
    [SerializeField] private Sprite spriteBack;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        //BT
        S_Patrol patrol = new S_Patrol(agent, waypoints);
        S_SeePlayer seePlayer = new S_SeePlayer(agent, player, sightRange);
        S_ChasePlayer chasePlayer = new S_ChasePlayer(agent, player);
        S_CloseEnough closeEnough = new S_CloseEnough(agent, player, closeRange);
        S_DoSomething doSomething = new S_DoSomething();

        Sequence sequence1 = new Sequence(new List<Node> { closeEnough, doSomething });
        Selector selector1 = new Selector(new List<Node> { sequence1, chasePlayer });
        Sequence sequence2 = new Sequence(new List<Node> { seePlayer, selector1 });
        Selector selector2 = new Selector(new List<Node> { sequence2, patrol });

        start = selector2;

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        start.Evaluate();

        
        Vector3 agentVelocity = agent.velocity.normalized;
        if (agentVelocity.y > 0)
        {
            spriteRenderer.sprite = spriteBack;
        }
        else if (agentVelocity.y < 0)
        {
            spriteRenderer.sprite = spriteFront;
        }
        else if (agentVelocity.x > 0)
        {
            spriteRenderer.sprite = spriteSide;
            spriteRenderer.flipX = false;
        }
        else if (agentVelocity.x < 0)
        {
            spriteRenderer.sprite = spriteSide;
            spriteRenderer.flipX = true;
        }
    }
}
