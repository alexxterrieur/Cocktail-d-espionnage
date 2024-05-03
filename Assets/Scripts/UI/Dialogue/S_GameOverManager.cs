using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class S_GameOverManager : MonoBehaviour
{
    public static S_GameOverManager Instance { get; private set; }

    [SerializeField] private GameOver _gameOverType;
    [SerializeField] private TextMeshProUGUI dialogueTxt;

    public GameOver GameOverType
    {
        set { _gameOverType = value; }
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

    // Start is called before the first frame update
    void Start()
    {
        string gameOverTxt = "";
        switch (_gameOverType)
        {
            case GameOver.OverTime:
                gameOverTxt = "Votre mission a échoué. Le temps imparti s'est écoulé, et vous n'avez pas réussi à atteindre votre objectif. La pression du temps a eu raison de votre stratégie.";
                break;
            case GameOver.Guard:
                gameOverTxt = "Les gardiens vous ont repéré. Votre discrétion n'a pas suffi pour échapper à leur vigilance. Votre présence a été détectée, et vos chances de succès ont été anéanties.";
                break;
            case GameOver.BossDesck:
                gameOverTxt = "La boss vous a surpris dans son bureau. Votre intrusion a été détectée avant que vous ne puissiez réaliser votre plan. Vous avez été pris au piège dans son propre domaine.";
                break;
            case GameOver.BossHouse:
                gameOverTxt = "La boss vous a repéré dans sa maison. Vos efforts pour rester caché ont été vains. Son flair pour le danger a fait de vous une proie facile.";
                break;
            case GameOver.FinalFight:
                gameOverTxt = "Vous avez été vaincu lors de ce combat épique, mais peu orthodoxe, contre la boss. Les jets de soda et les secousses de cannette n'ont pas été suffisants pour renverser sa détermination implacable.\r\n\r\n";
                break;
            case GameOver.WinButLose:
                gameOverTxt = "Malheureusement, votre quête de justice a été entravée par un manque de preuves. Malgré vos efforts pour rassembler des éléments incriminants, la vérité reste dissimulée dans l'ombre, et vous vous retrouvez incriminé pour vos actes.";
                break;
            default:
                gameOverTxt = "GameOver";
                break;
        }
        dialogueTxt.text = gameOverTxt;
        S_SaveDataExternal.Reset();
    }

    public void Retry()
    {
        SceneManager.LoadScene("Office");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
