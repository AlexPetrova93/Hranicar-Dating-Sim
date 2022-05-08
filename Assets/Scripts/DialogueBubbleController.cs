using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class DialogueBubbleController : MonoBehaviour
{
    protected EPlayer player;

    [SerializeField] private Sprite bubbleLeft;
    [SerializeField] private Sprite bubbleRight;

    [SerializeField] private Image bubbleGraphic;
    [SerializeField] private VerticalLayoutGroup thisLayoutGroup;
    [SerializeField] private VerticalLayoutGroup childLayoutGroup;

    private CanvasGroup canvasGroup;

    public void Init(EPlayer player)
    {
        canvasGroup = GetComponent<CanvasGroup>();

        this.player = player;

        switch (player)
        {
            case EPlayer.Left:
                // bubble sprite
                bubbleGraphic.sprite = bubbleLeft;

                // anchor left
                thisLayoutGroup.childAlignment = TextAnchor.LowerLeft;

                // padding left
                childLayoutGroup.padding = new RectOffset(
                    Utility.bubbleTextPaddingSide,
                    Utility.bubbleTextPaddingDefault,
                    Utility.bubbleTextPaddingDefault,
                    Utility.bubbleTextPaddingDefault
                );

                break;
            case EPlayer.Right:
                // bubble sprite
                bubbleGraphic.sprite = bubbleRight;

                // anchor right
                thisLayoutGroup.childAlignment = TextAnchor.LowerRight;

                // padding right
                childLayoutGroup.padding = new RectOffset(
                    Utility.bubbleTextPaddingDefault,
                    Utility.bubbleTextPaddingSide,
                    Utility.bubbleTextPaddingDefault,
                    Utility.bubbleTextPaddingDefault
                );

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
