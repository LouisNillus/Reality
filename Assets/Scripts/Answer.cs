using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Answer : MonoBehaviour
{
    Image image;
    public bool isChecked;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void Check()
    {
        isChecked = !isChecked;
        image.color = isChecked ? new Color(0.85f, 1f, 0.85f, 0.85f) : Color.white;
    }
}
