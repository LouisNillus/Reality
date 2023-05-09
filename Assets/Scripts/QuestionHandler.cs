using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class QuestionHandler : MonoBehaviour
{
    public List<AnswerChecker> checkers = new List<AnswerChecker>();

    public Image validateButton;
    public GameObject qcm;

    public UnityEvent OnQCMClosed;

    public void CheckAnswers()
    {
        foreach (AnswerChecker checker in checkers)
        {
            if (checker.Test() == false)
            {
                validateButton.DOKill();
                validateButton.DOColor(new Color(1f, 0.5f, 0.5f, 0.5f), 1f).OnComplete(() => validateButton.DOColor(Color.white, 0.5f));
                return;
            }
        }

        CloseQCM();
    }

    public void CloseQCM()
    {
        qcm.SetActive(false);
        OnQCMClosed?.Invoke();
    }
}

[Serializable]
public class AnswerChecker
{
    [SerializeField] Answer answer;
    [SerializeField] bool shouldBeChecked;

    public bool Test()
    {
        return answer.isChecked == shouldBeChecked;
    }
}
