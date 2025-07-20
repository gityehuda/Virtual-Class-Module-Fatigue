using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class HelpManager : MonoBehaviour
{
    public static HelpManager instance;

    [SerializeField] Button buttonHelp;
    [SerializeField] Button buttonClose;
    [SerializeField] GameObject uiHelp;

    [SerializeField] ButtonHelpList[] buttons;

    ButtonHelpList tempButton;

    private void Awake()
    {
        instance = this;

        buttonHelp.onClick.AddListener(delegate
        {
            OpenHelp();
        });

        buttonClose.onClick.AddListener(delegate
        {
            CloseHelp();
        });

        AssignButtons(buttons);
    }

    void OpenHelp()
    {
        ClearButtons(buttons);
        foreach(var child in buttons)
        {
            child.buttonParent.SetActive(true);
        }
        GameplayManager.instance.ActivateWindow(WindowList.Help);
        uiHelp.SetActive(true);
    }

    void CloseHelp()
    {
        GameplayManager.instance.DeActivateWindow();
        uiHelp.SetActive(false);
    }

    void ClearButtons(ButtonHelpList[] parent)
    {
        foreach(var child in parent)
        {
            child.buttonParent.SetActive(false);
            if(child.bgDescription != null) child.bgDescription.SetActive(false);
            if(child.buttonChild.Length != 0) ClearButtons(child.buttonChild);
        }
    }

    void AssignButtons(ButtonHelpList[] parent)
    {
        foreach(var child in parent)
        {
            child.buttonParent.GetComponent<Button>().onClick.AddListener(delegate
            {
                if(child.buttonChild.Length == 0 && child.bgDescription == null)
                {
                    ClearButtons(buttons);
                    foreach (var child in buttons)
                    {
                        child.buttonParent.SetActive(true);
                    }
                }
                else if(child.buttonChild.Length != 0)
                {
                    ClearButtons(buttons);
                    foreach(var child2 in child.buttonChild)
                    {
                        child2.buttonParent.SetActive(true);
                    }
                }
                else
                {
                    if(tempButton != null)
                    {
                        tempButton.buttonParent.GetComponent<Image>().color = Color.white;
                        tempButton.buttonParent.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
                        tempButton.bgDescription.SetActive(false);
                    }
                    child.buttonParent.GetComponent<Image>().color = Color.red;
                    child.buttonParent.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
                    child.bgDescription.SetActive(true);
                    child.bgDescription.GetComponentInParent<ScrollRect>().content = child.bgDescription.GetComponent<RectTransform>();
                    foreach(var child1 in child.descriptionVideo)
                    {
                        string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, child1.videoAsset);
                        child1.videoPlayer.url = videoPath;
                        child1.videoPlayer.Play();
                    }
                    tempButton = child;
                }
            });
            AssignButtons(child.buttonChild);
        }
    }

    [Serializable]
    public class ButtonHelpList
    {
        public GameObject buttonParent;
        public GameObject bgDescription;
        public DescriptionVideo[] descriptionVideo;
        public ButtonHelpList[] buttonChild;
    }

    [Serializable]
    public class DescriptionVideo
    {
        public VideoPlayer videoPlayer;
        public string videoAsset;
    }
}
