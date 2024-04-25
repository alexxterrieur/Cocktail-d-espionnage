using UnityEngine;

public class S_DoSomething : Node
{

    public S_DoSomething()
    {

    }


    public override NodeState Evaluate()
    {
        Debug.Log("doSomething");

        _nodeState = NodeState.SUCCESS;
        return _nodeState;
    }
}
