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

    private int currentSceneIndex;

    public void Init(GameManager gameManager, Sprite left, Sprite right)
    {
        characterLeftRenderer.sprite = left;
        characterRightRenderer.sprite = right;
        SetRandomBackground();

        this.gameManager = gameManager;
    }

    public void RestartScene()
    {
        if (!gameManager) gameManager = FindObjectOfType<GameManager>();

        gameManager.RestartScene();
    }

    public void StartNextScene()
    {
        ++currentSceneIndex;
    }

    private void SetRandomBackground()
    {
        bgRenderer.sprite = bgSprites[Random.Range(0, bgSprites.Length)];
    }

    [YarnCommand("scene")]
    public static void Scene(string text)
    {
        Debug.Log(text);
    }
}
