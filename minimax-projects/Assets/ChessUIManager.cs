using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChessUIManager : MonoBehaviour
{
    [SerializeField] private GameObject UIParent;
    [SerializeField] private GameObject PromoteUIParent;
    [SerializeField] private Text resultText;

    public void HideUI(){
        UIParent.SetActive(false);
        PromoteUIParent.SetActive(false);
    }

    public void OnGameFinished(string winner){
        UIParent.SetActive(true);
        resultText.text = string.Format("{0} won", winner);
    }

    public void Promote()
    {
        PromoteUIParent.SetActive(true);
    }
}
