using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Sprite[] characterSprites;
    private Dictionary<EPlayer, int> charactersChosen;

    private BlackScreenController blackScreen;

    private int currentScene;

    // level
    private LevelManager levelManager;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        charactersChosen = new Dictionary<EPlayer, int>() 
        {
            { EPlayer.Left, 0 },
            { EPlayer.Right, 0 }
        };

        blackScreen = FindObjectOfType<BlackScreenController>();
        blackScreen.fadeOutEnd.AddListener(LoadNextScene);

        currentScene = SceneManager.GetActiveScene().buildIndex;

        if (currentScene == 1)
        {
            levelManager = FindObjectOfType<LevelManager>();
            levelManager.SetRandomBackground();
            levelManager.SetCharacterSprites(
                characterSprites[charactersChosen[EPlayer.Left]], 
                characterSprites[charactersChosen[EPlayer.Right]]
            );
        }
    }

    public void ConfirmChoices(EPlayer player, bool confirm, int selection)
    {
        charactersChosen[player] = selection;
    }

    public void NextScene() 
    {
        blackScreen.FadeOut();
    }

    private void LoadNextScene()
    {
        ++currentScene;
        Debug.Log("loading scene with bi " + currentScene);
        SceneManager.LoadScene(currentScene);
    }

}
