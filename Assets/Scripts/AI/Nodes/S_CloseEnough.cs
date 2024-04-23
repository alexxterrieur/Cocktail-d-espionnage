using UnityEngine;
using UnityEngine.AI;

public class S_CloseEnough : Node
{
    private NavMeshAgent agent;
    private GameObject player;
    private float closeRange;

    public S_CloseEnough(NavMeshAgent agent, GameObject player, float closeRange)
    {
        this.agent = agent;
        this.player = player;
        this.closeRange = closeRange;
    }

    public override NodeState Evaluate()
    {
        float distance = Vector3.Distance(agent.transform.position, player.transform.position);

        if (distance < closeRange)
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
