using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterRunner : MonoBehaviour
{
    public Image icon;
    public Character character;
    [Range(0f, 1f)]
    public float deltaSpeed = 0.325f;
    [Range(1f, 2f)]
    public float deltaScale = 1.25f;

    public delegate void OnRunnerReached(CharacterRunner runner);
    public OnRunnerReached onRunnerReachedCallback;

    MarathonRunner marathonRunner;
    RectTransform runnerRectTranform;
    RectTransform rectTransform;
    float startTime;
    bool isStopped = true;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        marathonRunner = GetComponentInParent<MarathonRunner>();
        runnerRectTranform = marathonRunner.GetComponent<RectTransform>();
    }

    void Update()
    {
        Run();
    }

    public void ResetRunningPosition()
    {
        rectTransform.anchoredPosition = Vector2.zero;
    }

    public void StopRunning()
    {
        isStopped = true;
    }

    public void StartRunning()
    {
        isStopped = false;
    }

    void Run()
    {
        if (isStopped)
            return;
        transform.localScale = Vector3.one;
        var journeyLength = runnerRectTranform.GetWidth();
        var targetAnchoredPosition = new Vector2(journeyLength, 0f);
        var step = character.dexterity * deltaSpeed;
        rectTransform.anchoredPosition = rectTransform.anchoredPosition.x >= targetAnchoredPosition.x
            ? Vector2.zero
            : Vector2.MoveTowards(rectTransform.anchoredPosition, targetAnchoredPosition, step);

        if (rectTransform.anchoredPosition.x >= targetAnchoredPosition.x)
        {
            transform.localScale = Vector3.one * deltaScale;
            if (onRunnerReachedCallback != null)
            {
                onRunnerReachedCallback.Invoke(this);
            }
        }
    }
}