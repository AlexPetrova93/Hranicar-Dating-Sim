using System;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private Sprite[] bgNoiseSprites;

    [SerializeField] private YarnProject projectSeznameni;
    [SerializeField] private YarnProject projectSexAVztahy;
    [SerializeField] private YarnProject projectNeurotickaGalerie;
    [SerializeField] private YarnProject projectNeurotickaHospoda;
    [SerializeField] private YarnProject projectZaver;

    private Dictionary<EAct, YarnProject> yarnProjects = null;
    private EAct currentAct;

    private DialogueRunner dialogueRunner;

    private GameManager gameManager;

    [SerializeField] private CanvasGroup sceneChangeCanvas;
    [SerializeField] private TMPro.TextMeshProUGUI sceneChangeText;

    [SerializeField] private CanvasGroup creditsCanvas;

    private bool wantsToReturnToMain = false;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        dialogueRunner = FindObjectOfType<DialogueRunner>();
        SetUpYarnProjectDictionary();
        currentAct = EAct.Seznameni;
        //StartNextAct(EAct.Seznameni);
    }

    private void Update()
    {
        if (wantsToReturnToMain && Input.GetButtonDown(EPlayer.Flavor.Confirm()))
        {
            wantsToReturnToMain = false;
            gameManager.ReturnToMain();
        }
    }

    public void Init(GameManager gameManager, Sprite left, Sprite right)
    {
        characterLeftRenderer.sprite = left;
        characterRightRenderer.sprite = right;
        SetRandomBackground(ESetting.Gallery);

        this.gameManager = gameManager;
        dialogueRunner = FindObjectOfType<DialogueRunner>();
        SetUpYarnProjectDictionary();
        //StartNextAct(EAct.Seznameni);
    }

    private void SetUpYarnProjectDictionary()
    {
        if (yarnProjects != null) return;

        yarnProjects = new Dictionary<EAct, YarnProject>
        {
            { EAct.Seznameni, projectSeznameni },
            { EAct.SexAVztahy, projectSexAVztahy },
            { EAct.NeurotickaGalerie, projectNeurotickaGalerie },
            { EAct.NeurotickaHospoda, projectNeurotickaHospoda },
            { EAct.Zaver, projectZaver }
        };
    }

    public void RestartScene()
    {
        gameManager.RestartScene();
    }

    private void SetRandomBackground(ESetting setting)
    {
        switch (setting)
        {
            case ESetting.Gallery:
                bgRenderer.sprite = bgGallerySprites[UnityEngine.Random.Range(0, bgGallerySprites.Length)];
                break;
            case ESetting.Street:
                bgRenderer.sprite = bgStreerSprites[UnityEngine.Random.Range(0, bgStreerSprites.Length)];
                break;
            case ESetting.Pub:
                bgRenderer.sprite = bgPubSprites[UnityEngine.Random.Range(0, bgPubSprites.Length)];
                break;
            case ESetting.Noise:
                bgRenderer.sprite = bgNoiseSprites[UnityEngine.Random.Range(0, bgNoiseSprites.Length)];
                break;
        }
    }

    [YarnCommand("setting_change")]
    public void SettingChange(string text)
    {
        Debug.Log(text);

        // set correct text
        sceneChangeText.text = text;

        // fade to and from black
        BlackInterlude(
            Utility.fadeTime,
            Utility.sceneScreenStayTime,
            whenInBlack: () => SetRandomBackground(text.ToESetting())            
        );
    }

    [YarnCommand("end_act")]
    public void EndAct(string text, string setting)
    {
        Debug.Log(text);

        // set correct text
        sceneChangeText.text = text;

        // fade to and from black
        BlackInterlude(
            Utility.fadeTime,
            Utility.sceneScreenStayTime,
            whenInBlack: () => 
            {
                SetRandomBackground(setting.ToESetting());
                StartNextAct(currentAct.ToNextEAct(setting.ToESetting()));
            }
        );
    }

    private void StartNextAct(EAct nextAct)
    {
        currentAct = nextAct;
        dialogueRunner.SetProject(yarnProjects[currentAct]);
        dialogueRunner.Stop();
        dialogueRunner.StartDialogue("Start");
    }

    [YarnCommand("end_game")]
    public void EndGame()
    {
        this.Tween<float>(
            (from, to, t) => creditsCanvas.alpha = Mathf.Lerp(from, to, t),
            0,
            1,
            Utility.fadeTime,
            onEnd: () =>
            {
                // wait for confirm press
                wantsToReturnToMain = true;
                gameManager.PlayersInControl = true;
            }
        );

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
            onEnd: () => 
            {
                whenInBlack?.Invoke();

                // wait then fade from black
                this.WaitThenContinue
                (
                    waitTime,
                    continueAction: () => {
                        this.Tween<float>(
                            (from, to, t) => sceneChangeCanvas.alpha = Mathf.Lerp(from, to, t),
                            1,
                            0,
                            fadeTime,
                            onEnd: () => gameManager.PlayersInControl = true
                        );
                    }
                );                                  
            }
        );        
    }



}
