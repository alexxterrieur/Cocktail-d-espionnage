using UnityEngine;
using UnityEngine.AI;

public class S_SeePlayer : Node
{
    private NavMeshAgent agent;
    private GameObject player;
    private float sightRange;

    public S_SeePlayer(NavMeshAgent agent, GameObject player, float sightRange)
    {
        this.agent = agent;
        this.player = player;
        this.sightRange = sightRange;
    }

    public override NodeState Evaluate()
    {
        //distance enemy-player
        float distanceToPlayer = Vector3.Distance(agent.transform.position, player.transform.position);

        //check if player is in sight range
        if (distanceToPlayer <= sightRange)
        {
            _nodeState = NodeState.SUCCESS;
        }
        else
        {
            _nodeState = NodeState.FAILURE;
        }

        return _nodeState;
    }
}
