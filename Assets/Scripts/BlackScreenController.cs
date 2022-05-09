using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BlackScreenController : MonoBehaviour
{
    private Animator animator;

    [HideInInspector] public UnityEvent<bool> fadeOutEnd = new UnityEvent<bool>();

    private bool changeScene = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void FadeIn()
    {
        animator.SetTrigger("FadeIn");
    }

    public void FadeOut(bool changeScene)
    {
        this.changeScene = changeScene;
        animator.SetTrigger("FadeOut");
    }

    public void FadeOutEnd()
    {
        Debug.Log("ending fade out");
        fadeOutEnd.Invoke(changeScene);
    }

    private void OnDestroy()
    {
        fadeOutEnd.RemoveAllListeners();
    }
}
