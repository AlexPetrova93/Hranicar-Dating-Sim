using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    public static readonly int numCharacterChoices = 4;
    public static int mainLevelBuildIndex = 2;
    public static float axisDelta = .3f;

    public static int numDialogueOptions = 3;

    public static readonly float fadeTime = 1f;
    public static float sceneScreenStayTime = 1.5f;

    public static float typingTime = .05f;

    // dialogue gfx layout
    public static int bubbleTextPaddingDefault = 20;
    public static int bubbleTextPaddingSide = 50;

}

public static class Extensions
{
    public static int MoveIndex(this int current, int increment, int maxSize)
    {
        current = (current + increment) < 0 ? maxSize - 1 : (current + increment) % maxSize;
        return current;
    }

    public delegate void TweenFunction<T>(T from, T to, float factor);

    public static void Tween<T>(this MonoBehaviour monoBehaviour, TweenFunction<T> tweenFuction, T from, T to, float time, Action onEnd = null)
    {
        monoBehaviour.StartCoroutine(TweenInternal(tweenFuction, from, to, time, onEnd));
    }

    private static IEnumerator TweenInternal<T>(TweenFunction<T> tweenFuction, T from, T to, float time, Action onEnd)
    {
        float timeElapsed = 0;
        while (timeElapsed < time)
        {
            tweenFuction(from, to, timeElapsed / time);
            //canvasGroup.alpha = Mathf.Lerp(from, to, timeElapsed / time);

            timeElapsed += Time.deltaTime;

            yield return null;
        }

        tweenFuction(from, to, 1);
        onEnd?.Invoke();
    }

    public static void WaitThenContinue(this MonoBehaviour monoBehaviour, float time, Action continueAction)
    {
        monoBehaviour.StartCoroutine(WaitThenContinueInternal(time, continueAction));
    }

    private static IEnumerator WaitThenContinueInternal(float time, Action continueAction)
    {
        yield return new WaitForSeconds(time);

        continueAction.Invoke();
    }

}