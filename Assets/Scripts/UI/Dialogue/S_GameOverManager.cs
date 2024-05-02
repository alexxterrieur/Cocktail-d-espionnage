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
                gameOverTxt = "Votre mission a �chou�. Le temps imparti s'est �coul�, et vous n'avez pas r�ussi � atteindre votre objectif. La pression du temps a eu raison de votre strat�gie.";
                break;
            case GameOver.Guard:
                gameOverTxt = "Les gardiens vous ont rep�r�. Votre discr�tion n'a pas suffi pour �chapper � leur vigilance. Votre pr�sence a �t� d�tect�e, et vos chances de succ�s ont �t� an�anties.";
                break;
            case GameOver.BossDesck:
                gameOverTxt = "La boss vous a surpris dans son bureau. Votre intrusion a �t� d�tect�e avant que vous ne puissiez r�aliser votre plan. Vous avez �t� pris au pi�ge dans son propre domaine.";
                break;
            case GameOver.BossHouse:
                gameOverTxt = "La boss vous a rep�r� dans sa maison. Vos efforts pour rester cach� ont �t� vains. Son flair pour le danger a fait de vous une proie facile.";
                break;
            case GameOver.FinalFight:
                gameOverTxt = "Vous avez �t� vaincu lors de ce combat �pique, mais peu orthodoxe, contre la boss. Les jets de soda et les secousses de cannette n'ont pas �t� suffisants pour renverser sa d�termination implacable.\r\n\r\n";
                break;
            case GameOver.WinButLose:
                gameOverTxt = "Malheureusement, votre qu�te de justice a �t� entrav�e par un manque de preuves. Malgr� vos efforts pour rassembler des �l�ments incriminants, la v�rit� reste dissimul�e dans l'ombre, et vous vous retrouvez incrimin� pour vos actes.";
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
