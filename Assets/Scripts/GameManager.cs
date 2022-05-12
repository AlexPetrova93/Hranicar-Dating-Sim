using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Sprite[] characterSprites;
    private Dictionary<EPlayer, int> charactersChosen;

    [SerializeField] private CanvasGroup blackScreen;

    private bool playersInControl;
    public bool PlayersInControl { get => playersInControl; set => playersInControl = value; }

    // level
    private LevelManager levelManager;

    public AudioManager audioManager;


    void Start()
    {
        DontDestroyOnLoad(gameObject);
        PlayersInControl = true;
        charactersChosen = new Dictionary<EPlayer, int>() 
        {
            { EPlayer.Left, 0 },
            { EPlayer.Right, 0 }
        };

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("running init");

        // fade from black
        this.Tween<float>(
            (from, to, t) => 
            {
                blackScreen.alpha = Mathf.Lerp(from, to, t);
                audioManager.LerpVolume(from, to, t);
            },
            1,
            0,
            Utility.fadeTime            
        );

        if (SceneManager.GetActiveScene().buildIndex < Utility.mainLevelBuildIndex)
        {
            audioManager.SetAmbience(ESetting.Menu);
        }

        levelManager = FindObjectOfType<LevelManager>();
        if (SceneManager.GetActiveScene().buildIndex == Utility.mainLevelBuildIndex && levelManager)
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

    public void ReturnToMain()
    {
        //fade to black then load next scene
        this.Tween<float>(
            (from, to, t) =>
            {
                blackScreen.alpha = Mathf.Lerp(from, to, t);
                audioManager.LerpVolume(from, to, t);
            },
            0,
            1,
            Utility.fadeTime,
            LoadIntroScreen
        );
    }

    public void NextScene() 
    {
        //fade to black then load next scene
        this.Tween<float>(
            (from, to, t) =>
            {
                blackScreen.alpha = Mathf.Lerp(from, to, t);
                audioManager.LerpVolume(from, to, t);
            },
            0,
            1,
            Utility.fadeTime,
            LoadNextScene
        );
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void LoadNextScene()
    {
        Debug.Log("loading scene with bi " + SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void LoadIntroScreen()
    {
        SceneManager.LoadScene(0);
    }

}
