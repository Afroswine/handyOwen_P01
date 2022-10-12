using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : StateMachine
{
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            QuitGame();

        if (Input.GetKey(KeyCode.Backspace))
            Restart();
    }

    void QuitGame()
    {
        Application.Quit();
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
