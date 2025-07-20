using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SetDataValue : MonoBehaviour
{
    public TextMeshProUGUI matakuliahText;
    public TextMeshProUGUI kodeMatakuliahText;
    public TextMeshProUGUI kelasText;
    public TextMeshProUGUI tanggalText;
    public TextMeshProUGUI jamText;
    public GameObject joinButton;

    public VerticalLayoutGroup verticalLayoutGroup;

    private void Start()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(verticalLayoutGroup.GetComponent<RectTransform>());
    }
}
