using UnityEngine;
using UnityEngine.InputSystem;

public class QuitApplication : MonoBehaviour
{
    private void Update()
    {
        QuitTheGame();
    }

    private static void QuitTheGame()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Debug.Log("You're Quit The Game!");
            Application.Quit();
        }
    }
}
