using Binus.WebGL.Service;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpManager : MonoBehaviour, IPopUpManager
{
    [SerializeField] GameObject popUp;

    private void Awake()
    {
        ServiceLocator.RegisterService<IPopUpManager>(this);
    }

    public void OpenPopUp(string message)
    {
        popUp.GetComponentInChildren<TextMeshProUGUI>().text = message;
        popUp.SetActive(true);
    }

    public void ClosePopUp()
    {
        popUp.SetActive(false);
    }
}
