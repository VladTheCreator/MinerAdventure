using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Panel : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private Button resatartButton;
    [SerializeField] private Button quitButton;
    public TMP_Text GetMainText()
    {
        return text;
    }
    public Button GetRestartButton()
    {
        return resatartButton;
    }
    public Button GetQuitButton()
    {
        return quitButton;  
    }
}
