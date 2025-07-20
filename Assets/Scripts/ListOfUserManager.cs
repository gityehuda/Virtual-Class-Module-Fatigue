using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ListOfUserManager : MonoBehaviour
{
    public static ListOfUserManager instance;

    [Header("Object")]
    [SerializeField] GameObject listOfUser;
    [SerializeField] GameObject contentListOfUser;

    [Header("Button")]
    [SerializeField] GameObject btnListOfUser;
    [SerializeField] GameObject btnReference;
    [SerializeField] GameObject btnClose;
    [SerializeField] GameObject btnAudioAll;
    [SerializeField] GameObject btnShareAll;

    [Header("Icons")]
    [SerializeField] Sprite audioIcon;
    [SerializeField] Sprite audioMutedIcon;
    [SerializeField] Sprite shareIcon;
    [SerializeField] Sprite shareDisableIcon;

    public Dictionary<int, GameObject> listGameObjectParent = new Dictionary<int, GameObject>();
    private bool isShare = false;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (MainData.instance.roleUsr != "1")
        {
            btnAudioAll.SetActive(false);
            btnShareAll.SetActive(false);
        }

        btnListOfUser.SetActive(true);

        var openListOfUserBtn = btnListOfUser.GetComponent<Button>();
        openListOfUserBtn.onClick.AddListener(delegate
        {
            btnListOfUser.GetComponent<Button>().interactable = false;
            listOfUser.SetActive(true);
            SpawnListOfUser();
        });

        var closeListOfUserBtn = btnClose.GetComponent<Button>();
        closeListOfUserBtn.onClick.AddListener(delegate
        {
            listOfUser.SetActive(false);
            btnListOfUser.GetComponent<Button>().interactable = true;
        });

        var audioBtn = btnAudioAll.GetComponent<Button>();
        audioBtn.onClick.AddListener(delegate
        {
            MuteOrUnmuteAll();
        });

        var shareBtn = btnShareAll.GetComponent<Button>();
        shareBtn.onClick.AddListener(delegate
        {
            ShareOrNotAll();
        });

    }

    public void SpawnListOfUser()
    {
        var data = contentListOfUser;
        listGameObjectParent = new Dictionary<int, GameObject>();
        foreach (Transform child in data.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (var child in GameplayManager.instance.listOfUser)
        {
            if (child.Value == null) continue;
            var childValues = child.Value;

            var parentGameObject = new GameObject();
            parentGameObject.name = childValues.externalSystemId + childValues.name;
            parentGameObject.transform.SetParent(contentListOfUser.transform);
            parentGameObject.transform.localScale = Vector3.one;

            var parentGameObjectRect = parentGameObject.AddComponent<RectTransform>();

            var textNimNameGameObject = new GameObject();
            textNimNameGameObject.transform.SetParent(parentGameObjectRect.transform);
            textNimNameGameObject.name = "textNimName";
            textNimNameGameObject.transform.localScale = Vector3.one;

            var textNimNameRect = textNimNameGameObject.AddComponent<RectTransform>();
            textNimNameRect.anchorMin = new Vector2(0, 1f);
            textNimNameRect.anchorMax = new Vector2(0, 1f);
            textNimNameRect.pivot = new Vector2(0, 1f);
            textNimNameRect.anchoredPosition = Vector2.zero;
            textNimNameRect.sizeDelta = new Vector2(300f, 35f);

            var textNimNameText = textNimNameGameObject.AddComponent<TextMeshProUGUI>();
            textNimNameText.text = childValues.externalSystemId + " - " + childValues.name;
            textNimNameText.fontSize = 18f;
            textNimNameText.verticalAlignment = VerticalAlignmentOptions.Middle;
            textNimNameText.enableWordWrapping = false;

            if(MainData.instance.roleUsr == "1")
            {
                var btnAudioObject = Instantiate(btnReference);
                btnAudioObject.name = "Audio Button";
                btnAudioObject.transform.SetParent(parentGameObject.transform);
                btnAudioObject.transform.localScale = Vector3.one;
                Destroy(btnAudioObject.transform.GetChild(0).gameObject);

                var btnAudioRect = btnAudioObject.GetComponent<RectTransform>();
                btnAudioRect.anchorMin = new Vector2(1, 0.5f);
                btnAudioRect.anchorMax = new Vector2(1, 0.5f);
                btnAudioRect.pivot = new Vector2(1, 0.5f);
                btnAudioRect.anchoredPosition = new Vector2(-50f, 0);

                var btnAudioImage = btnAudioObject.GetComponent<Image>();
                btnAudioImage.sprite = childValues.isAudio ? audioIcon : audioMutedIcon;

                var btnAudioBtn = btnAudioObject.GetComponent<Button>();
                btnAudioBtn.onClick.AddListener(delegate
                {
                    MuteOrUnmuteTarget(child.Key, childValues.isAudio);
                });

                var btnShareObject = Instantiate(btnReference);
                btnShareObject.name = "Share Button";
                btnShareObject.transform.SetParent(parentGameObject.transform);
                btnShareObject.transform.localScale = Vector3.one;
                Destroy(btnShareObject.transform.GetChild(0).gameObject);

                var btnShareRect = btnShareObject.GetComponent<RectTransform>();
                btnShareRect.anchorMin = new Vector2(1, 0.5f);
                btnShareRect.anchorMax = new Vector2(1, 0.5f);
                btnShareRect.pivot = new Vector2(1, 0.5f);
                btnShareRect.anchoredPosition = Vector2.zero;

                var btnShareImage = btnShareObject.GetComponent<Image>();
                btnShareImage.sprite = childValues.isShare ? shareIcon : shareDisableIcon;

                var btnShareBtn = btnShareObject.GetComponent<Button>();
                btnShareBtn.onClick.AddListener(delegate
                {
                    ShareTarget(child.Key, childValues.isShare);
                });
            }

            listGameObjectParent.Add(child.Key, parentGameObject);
        }
    }

    public void ChangeAudioIconSpecified(int actorNumberId, bool isAudio)
    {
        try
        {
            var currentGameObject = listGameObjectParent[actorNumberId];

            foreach (Transform child in currentGameObject.transform)
            {
                var childGameObject = child.gameObject;
                if (childGameObject.name == "Audio Button")
                {
                    var btnAudioImage = childGameObject.GetComponent<Image>();
                    btnAudioImage.sprite = isAudio ? audioIcon : audioMutedIcon;
                }
            }
        }
        catch
        {

        }
    }

    private void MuteOrUnmuteAll()
    {
        foreach (var child in GameplayManager.instance.listOfUser)
        {
            child.Value.isAudio = false;
        }

        foreach(var child1 in listGameObjectParent)
        {
            foreach(Transform child2 in child1.Value.transform)
            {
                var childGameObject = child2.gameObject;
                if (childGameObject.name == "Audio Button")
                {
                    var btnAudioImage = childGameObject.GetComponent<Image>();
                    btnAudioImage.sprite = audioMutedIcon;
                }
            }
        }

        var data = new CustomClass.DataGiveMute
        {
            actorNumberId = 0,
            isAudio = false,
        };

        PhotonManager.instance.SendData((byte)CustomClass.TypeData.GiveMuteAll, data);
    }

    private void MuteOrUnmuteTarget(int actorNumberId, bool isAudio)
    {
        CustomClass.DataGiveMute data = new CustomClass.DataGiveMute
        {
            actorNumberId = actorNumberId,
            isAudio = !isAudio
        };

        PhotonManager.instance.SendData((byte)CustomClass.TypeData.GiveMuteUnMuteTarget, data);
    }

    public void HandleSyncAudioAndShare(CustomClass.DataSyncAudioAndShare data)
    {
        GameplayManager.instance.listOfUser[data.actorNumberId].isShare = data.isShare;
        GameplayManager.instance.listOfUser[data.actorNumberId].isAudio = data.isAudio;

        ChangeAudioIconSpecified(data.actorNumberId, data.isAudio);
    }

    private void ShareOrNotAll()
    {
        isShare = !isShare;
        var shareBtn = btnShareAll.GetComponent<Image>();
        shareBtn.sprite = isShare ? shareIcon : shareDisableIcon;

        foreach (var child in GameplayManager.instance.listOfUser)
        {
            child.Value.isShare = isShare;
        }

        foreach (var child in listGameObjectParent)
        {
            foreach (Transform childObject in child.Value.transform)
            {
                if (childObject.gameObject.name == "Share Button")
                {
                    childObject.gameObject.GetComponent<Image>().sprite = isShare ? shareIcon : shareDisableIcon;
                }
            }
        }

        CustomClass.DataGiveShare dataShare = new CustomClass.DataGiveShare
        {
            actorNumberId = 0,
            isShare = isShare
        };

        PhotonManager.instance.SendData((byte)CustomClass.TypeData.GiveShareAll, dataShare);
    }

    private void ShareTarget(int actorNumberId, bool isShare)
    {
        var tempIsShare = !isShare;

        GameplayManager.instance.listOfUser[actorNumberId].isShare = tempIsShare;

        var currentGameObject = listGameObjectParent[actorNumberId];

        foreach (Transform child in currentGameObject.transform)
        {
            var childGameObject = child.gameObject;
            if (childGameObject.name == "Share Button")
            {
                var btnShareImage = childGameObject.GetComponent<Image>();
                btnShareImage.sprite = tempIsShare ? shareIcon : shareDisableIcon;
            }
        }

        CustomClass.DataGiveShare dataShare = new CustomClass.DataGiveShare
        {
            actorNumberId = actorNumberId,
            isShare = tempIsShare
        };

        PhotonManager.instance.SendData((byte)CustomClass.TypeData.GiveShareTarget, dataShare);
    }
}
