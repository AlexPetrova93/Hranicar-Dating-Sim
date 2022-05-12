using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Image bgRenderer;
    [SerializeField] private Image characterLeftRenderer;
    [SerializeField] private Image characterRightRenderer;

    [SerializeField] private Sprite[] bgGallerySprites;
    [SerializeField] private Sprite[] bgStreerSprites;
    [SerializeField] private Sprite[] bgPubSprites;

    [SerializeField] private YarnProject[] yarnProjects;
    private DialogueRunner dialogueRunner;

    private GameManager gameManager;

    [SerializeField] private CanvasGroup sceneChangeCanvas;
    [SerializeField] private TMPro.TextMeshProUGUI sceneChangeText;

    public void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        dialogueRunner = FindObjectOfType<DialogueRunner>();
    }

    public void Init(GameManager gameManager, Sprite left, Sprite right)
    {
        characterLeftRenderer.sprite = left;
        characterRightRenderer.sprite = right;
        SetRandomBackground(EScene.Gallery);

        this.gameManager = gameManager;
        dialogueRunner = FindObjectOfType<DialogueRunner>();
    }

    public void RestartScene()
    {
        gameManager.RestartScene();
    }

    private void SetRandomBackground(EScene scene)
    {
        switch (scene)
        {
            case EScene.Gallery:
                bgRenderer.sprite = bgGallerySprites[UnityEngine.Random.Range(0, bgGallerySprites.Length)];
                break;
            case EScene.Street:
                bgRenderer.sprite = bgStreerSprites[UnityEngine.Random.Range(0, bgStreerSprites.Length)];
                break;
            case EScene.Pub:
                bgRenderer.sprite = bgPubSprites[UnityEngine.Random.Range(0, bgPubSprites.Length)];
                break;
        }
    }

    [YarnCommand("scenery_change")]
    public void SceneryChange(string text)
    {
        Debug.Log(text);

        // set correct text
        sceneChangeText.text = text;

        // fade to and from black
        BlackInterlude(
            Utility.fadeTime,
            Utility.sceneScreenStayTime,
            () => SetRandomBackground(text.ToEScene())
        );
    }

    [YarnCommand("end_segment")]
    public void EndSegment(string text)
    {
        Debug.Log(text);

        // set correct text
        sceneChangeText.text = text;

        // fade to and from black
        BlackInterlude(
            Utility.fadeTime,
            Utility.sceneScreenStayTime,
            () => 
            {
                SetRandomBackground(text.ToEScene());

                // TODO - change yarn project to the next scene and start the dialogue again
                dialogueRunner.SetProject(yarnProjects[0]);
                dialogueRunner.Stop();
                dialogueRunner.StartDialogue("Start");

                //gameManager.RestartScene();
            }
        );
    }

    [YarnCommand("outro_message")]
    public void OutroMessage(string text)
    {
        Debug.Log(text);


    }

    [YarnCommand("end_game")]
    public void EndGame(string text)
    {
        Debug.Log(text);

        // fade to black + return to main menu

    }

    private void BlackInterlude(float fadeTime, float waitTime, Action whenInBlack)
    {
        // players can't interact during interlude
        gameManager.PlayersInControl = false;

        // fade to black
        this.Tween<float>(
            (from, to, t) => sceneChangeCanvas.alpha = Mathf.Lerp(from, to, t),
            0,
            1,
            fadeTime,
            () => 
            {
                whenInBlack?.Invoke();

                // wait then fade from black
                this.WaitThenContinue
                (
                    waitTime,
                    () => {
                        this.Tween<float>(
                            (from, to, t) => sceneChangeCanvas.alpha = Mathf.Lerp(from, to, t),
                            1,
                            0,
                            fadeTime,
                            () => gameManager.PlayersInControl = true                            
                        );
                    }
                );                                  
            }
        );        
    }



}
