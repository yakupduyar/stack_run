using UnityEngine;
using UnityEngine.SceneManagement;

public class SuccessPanel : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name,LoadSceneMode.Single);
    }

    public void NextLevel()
    {
        StackController.Instance.NextLevel();
        GameManager.Instance.StartLevel();
    }
}
