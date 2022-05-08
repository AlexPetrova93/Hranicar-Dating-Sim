using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class DialogueBubbleController : MonoBehaviour
{
    protected EPlayer player;

    [SerializeField] private Sprite bubbleLeft;
    [SerializeField] private Sprite bubbleRight;

    private Image bubbleGraphic;

    void Start()
    {
        bubbleGraphic = GetComponent<Image>();
    }

    public void Init(EPlayer player)
    {
        this.player = player;
        bubbleGraphic.sprite = player == EPlayer.Left ? bubbleLeft : bubbleRight;
    }
}
