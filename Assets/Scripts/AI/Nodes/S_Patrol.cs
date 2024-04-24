using UnityEngine.AI;
using UnityEngine;

public class S_Patrol : Node
{
    private NavMeshAgent agent;

    private Transform[] waypoints;
    private bool loop = true;
    private int currentWaypoint = 0;
    private int direction = 1;
    private float waitTime = 0.5f;

    public S_Patrol(NavMeshAgent agent, Transform[] waypoints)
    {
        this.agent = agent;
        this.waypoints = waypoints;
    }


    public override NodeState Evaluate()
    {
        //if IA is at the last waypoint, it goes back
        if (currentWaypoint >= waypoints.Length || currentWaypoint < 0)
        {
            if (loop)
            {
                direction *= -1;
                currentWaypoint += direction;
            }
        }

        //go to the next waypoint
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            if (waitTime > 0) //wait at the waypoint
            {
                waitTime -= Time.deltaTime;
            }
            else //go to the next waypoint
            {
                agent.SetDestination(waypoints[currentWaypoint].position);
                currentWaypoint += direction;
                waitTime = 0.5f;
            }
        }

        _nodeState = NodeState.SUCCESS;
        return _nodeState;
    }
}