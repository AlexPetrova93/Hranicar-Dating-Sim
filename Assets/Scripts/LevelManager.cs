using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Image bgRenderer;
    [SerializeField] private Image characterLeftRenderer;
    [SerializeField] private Image characterRightRenderer;

    [SerializeField] private Sprite[] bgSprites;

    [SerializeField] private YarnProject[] yarnProjects;
    private DialogueRunner dialogueRunner;

    private GameManager gameManager;

    public void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void Init(GameManager gameManager, Sprite left, Sprite right)
    {
        characterLeftRenderer.sprite = left;
        characterRightRenderer.sprite = right;
        SetRandomBackground();

        this.gameManager = gameManager;
    }

    public void RestartScene()
    {
        gameManager.RestartScene();
    }

    private void SetRandomBackground()
    {
        bgRenderer.sprite = bgSprites[Random.Range(0, bgSprites.Length)];
    }

    [YarnCommand("scene")]
    public void SceneChange(string text)
    {
        Debug.Log(text);
        // fade out to black and show text
        gameManager.PlayersInControl = false;
    }

    public void SceneChangeEnd()
    {
        // fade from black, hide text and resume player control
    }
}
