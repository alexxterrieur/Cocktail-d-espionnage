using UnityEngine;
using UnityEngine.SceneManagement;

public class S_ChangeOfScene : MonoBehaviour
{
    public string SceneName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene(SceneName);
    }
}
