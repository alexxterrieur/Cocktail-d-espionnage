using UnityEngine;


public class S_GameOverManager : MonoBehaviour
{
    public static S_GameOverManager Instance { get; private set; }

    [SerializeField] private GameOver _gameOverType;

    public GameOver GameOverType
    {
        set { _gameOverType = value; }
        get { return _gameOverType; }
    }


    public enum GameOver
    {
        OverTime,
        Guard,
        BossDesck,
        BossHouse,
        FinalFight,
        WinButLose
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
}
