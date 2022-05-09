using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Sprite[] characterSprites;
    private Dictionary<EPlayer, int> charactersChosen;

    private BlackScreenController blackScreen;

    private int currentScene;

    private bool playersInControl;
    public bool PlayersInControl { get => playersInControl; set => playersInControl = value; }

    // level
    private LevelManager levelManager;


    void Start()
    {
        DontDestroyOnLoad(gameObject);
        PlayersInControl = true;
        charactersChosen = new Dictionary<EPlayer, int>() 
        {
            { EPlayer.Left, 0 },
            { EPlayer.Right, 0 }
        };

        blackScreen = FindObjectOfType<BlackScreenController>();
        blackScreen.fadeOutEnd.AddListener(ChangeScene);

        currentScene = 0;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("running init");

        blackScreen.FadeIn();

        levelManager = FindObjectOfType<LevelManager>();
        if (currentScene == Utility.mainLevelBuildIndex && levelManager)
        {
            levelManager.Init(
                this,
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
        blackScreen.FadeOut(true);
        //LoadNextScene();
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ChangeScene(bool changeScene)
    {
        if (changeScene)
        {
            ++currentScene;
            Debug.Log("loading scene with bi " + currentScene);
            SceneManager.LoadScene(currentScene);
        }
        else
        {
            levelManager.SceneChangeEnd();
        }
    }

}
