using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

[RequireComponent(typeof(Image), typeof(CanvasGroup))]
public class DialogueBubbleController : MonoBehaviour
{
    protected EPlayer player;

    [SerializeField] private Sprite bubbleLeft;
    [SerializeField] private Sprite bubbleRight;

    private Image bubbleGraphic;

    private CanvasGroup canvasGroup;

    public void Init(EPlayer player)
    {
        bubbleGraphic = GetComponent<Image>();
        canvasGroup = GetComponent<CanvasGroup>();

        this.player = player;
        bubbleGraphic.sprite = player == EPlayer.Left ? bubbleLeft : bubbleRight;

        SetUpLayout();
    }

    private void SetUpLayout()
    {
        VerticalLayoutGroup verticalGroup = GetComponent<VerticalLayoutGroup>();
        RectTransform rectTransform = GetComponent<RectTransform>();

        switch (player)
        {
            case EPlayer.Left:
                verticalGroup.childAlignment = TextAnchor.MiddleLeft;
                verticalGroup.padding = new RectOffset(
                    Utility.bubbleTextPaddingSide,
                    Utility.bubbleTextPaddingDefault,
                    Utility.bubbleTextPaddingDefault,
                    Utility.bubbleTextPaddingDefault
                );

                rectTransform.anchorMin = Vector2.zero;
                rectTransform.anchorMax = Vector2.zero;
                break;
            case EPlayer.Right:
                verticalGroup.childAlignment = TextAnchor.MiddleRight;
                verticalGroup.padding = new RectOffset(
                    Utility.bubbleTextPaddingDefault,
                    Utility.bubbleTextPaddingSide,
                    Utility.bubbleTextPaddingDefault,
                    Utility.bubbleTextPaddingDefault
                );

                rectTransform.anchorMin = new Vector2(1, 0);
                rectTransform.anchorMax = new Vector2(1, 0);
                break;
        }
    }

    public void Fade()
    {        
        canvasGroup.alpha = .5f;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
