using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_GameOverDisplay : MonoBehaviour
{

    [SerializeField] private S_GameOverManager.GameOver _gameOverType;
    [SerializeField] private TextMeshProUGUI dialogueTxt;


    // Start is called before the first frame update
    void Start()
    {
        string gameOverTxt = "";
        switch (S_GameOverManager.Instance.GameOverType)
        {
            case S_GameOverManager.GameOver.OverTime:
                gameOverTxt = "Votre mission a échoué. Le temps imparti s'est écoulé, et vous n'avez pas réussi à atteindre votre objectif. La pression du temps a eu raison de votre stratégie.";
                break;
            case S_GameOverManager.GameOver.Guard:
                gameOverTxt = "Les gardiens vous ont repéré. Votre discrétion n'a pas suffi pour échapper à leur vigilance. Votre présence a été détectée, et vos chances de succès ont été anéanties.";
                break;
            case S_GameOverManager.GameOver.BossDesck:
                gameOverTxt = "La boss vous a surpris dans son bureau. Votre intrusion a été détectée avant que vous ne puissiez réaliser votre plan. Vous avez été pris au piège dans son propre domaine.";
                break;
            case S_GameOverManager.GameOver.BossHouse:
                gameOverTxt = "La boss vous a repéré dans sa maison. Vos efforts pour rester caché ont été vains. Son flair pour le danger a fait de vous une proie facile.";
                break;
            case S_GameOverManager.GameOver.FinalFight:
                gameOverTxt = "Vous avez été vaincu lors de ce combat épique, mais peu orthodoxe, contre la boss. Les jets de soda et les secousses de cannette n'ont pas été suffisants pour renverser sa détermination implacable.\r\n\r\n";
                break;
            case S_GameOverManager.GameOver.WinButLose:
                gameOverTxt = "Malheureusement, votre quête de justice a été entravée par un manque de preuves. Malgré vos efforts pour rassembler des éléments incriminants, la vérité reste dissimulée dans l'ombre, et vous vous retrouvez incriminé pour vos actes.";
                break; 
            case S_GameOverManager.GameOver.Win:
                gameOverTxt = "Après un combat acharné et des épreuves sans fin, vous avez enfin triomphé de la redoutable boss. Vos efforts, votre détermination et votre ingéniosité ont été récompensés par cette victoire bien méritée.";
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
