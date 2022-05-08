using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BlackScreenController : MonoBehaviour
{
    private Animator animator;

    [HideInInspector] public UnityEvent fadeOutEnd = new UnityEvent();

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void FadeIn()
    {
        animator.SetTrigger("FadeIn");
    }

    public void FadeOut()
    {
        animator.SetTrigger("FadeOut");
    }

    public void FadeOutEnd()
    {
        Debug.Log("ending fade out");
        fadeOutEnd.Invoke();
    }

    private void OnDestroy()
    {
        fadeOutEnd.RemoveAllListeners();
    }
}
