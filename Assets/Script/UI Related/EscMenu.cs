using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EscMenu : MonoBehaviour
{
    public Spawner s;

    public GameObject mainMenu, escapeMenu, lightHandler;

    public IntVar menuTimeScale;

    public BoolVar gameActiveSO, gameOverSO, EffectsToggleSO;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!escapeMenu.activeInHierarchy && gameActiveSO.value)
            {
                OpenEscMenu();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    public void OpenEscMenu()
    {
        if (gameActiveSO.value && !gameOverSO.value)
        {
            escapeMenu.SetActive(true);
            menuTimeScale.value = 0;
        }
    }

    public void ResetToMain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ResumeGame()
    {
        if(gameActiveSO)
        {
            escapeMenu.SetActive(false);
            menuTimeScale.value = 1;
        }
    }    

    public void ExitGame()
    {
        Application.Quit();
    }
}
