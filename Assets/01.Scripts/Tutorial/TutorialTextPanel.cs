using System.Collections;
using System.Collections.Generic;
using Febucci.UI;
using Febucci.UI.Core;
using UnityEngine;

public class TutorialTextPanel : MonoBehaviour
{
    private TextAnimator_TMP _mainTextAnimator;
    private TypewriterCore _typeWriter;
    public void Init()
    {
        Transform mainTextTrm = transform.Find("InfoText");
        
        _mainTextAnimator = mainTextTrm.GetComponent<TextAnimator_TMP>();
        _typeWriter = mainTextTrm.GetComponent<TypewriterCore>();
    }

    public void SetText(string text)
    {
        _mainTextAnimator.SetText(text);
        if (text != string.Empty)
        {
            _typeWriter.ShowText(text);
            _typeWriter.StartShowingText();
        }
    }
}
