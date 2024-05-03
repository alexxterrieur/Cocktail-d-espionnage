using UnityEngine;
using UnityEngine.SceneManagement;

public class S_DoSomething : Node
{

    private Transform _checkpointPlayer;
    private GameObject player;
    
    public S_DoSomething(Transform _checkpointPlayer, GameObject player)
    {
        this.player = player;
        this._checkpointPlayer = _checkpointPlayer;
    }


    public override NodeState Evaluate()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Office"))
        {
            player.transform.position = _checkpointPlayer.position;

        }
        else
        {
            S_GameOverManager.Instance.GameOverType = S_GameOverManager.GameOver.Guard;
            SceneManager.LoadScene("GameOver");
            //SceneManager.LoadScene("Office");
        }

        _nodeState = NodeState.SUCCESS;
        return _nodeState;
    }
}
