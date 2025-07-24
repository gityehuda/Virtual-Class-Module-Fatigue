using Photon.Voice;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Module4UIManager : MonoBehaviour
{
    public ConversationPanel conversationPanel;
    private Module4SimManager simManager;
    private Module4Manager manager;
    private Module4UIManager2 uiManager2;
    public GameObject selectionPanel;
   [SerializeField] private GameObject submitButton;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject prevButton;
    [SerializeField] private GameObject diffGenderPanel;
    public TMP_Text sessionText;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject downloadButton;
    public GameObject speedRepetitionPanel;
    public TMP_Text speedRepetitionText;
    public TMP_Text repetitionSpeedText;
    public TMP_Text repetitionKiloperHourText;
    [SerializeField] private GameObject heartBeatPanel;
    public TMP_Text heartBeatText;
    [SerializeField] private GameObject sessionPanel;
    [SerializeField] private Image[] uiImage;
    private float brightness = 1.2f;
    private bool genderSelected;
    public bool footstepSelected = false;
    public bool ergobikeSelected = false;
    private bool timeInputted = false;
    private TextMeshPro text;
    public TextMeshProUGUI genderName;
    public TextMeshProUGUI equipmentName;
    public TMP_InputField time;
    public TMP_Text equipmentText;
    public TMP_Text timeText;
    public TMP_Text simulationText;
    public TMP_Text speedText;
    [SerializeField] private TMP_Text genderPanelMessage;
    [SerializeField] private TMP_Text equipmentPanelMessage;
    private bool isFirstSimFinished = false;
    public bool isChanged = false;
    private bool isChangedtoErgo = false;
    private bool isChangedtoFootstep = false;

    // Start is called before the first frame update
    void Start()
    {
        manager = GetComponent<Module4Manager>();
        simManager = GetComponent<Module4SimManager>();
        uiManager2 = GetComponent<Module4UIManager2>();
    }

    // Update is called once per frame
    void Update()
    {
        //EquipmentGenderTimeSelection();
        HideButton();
        ShowPanel();
        GenderManage();
        DeleteTable();                      
    }

    void HideButton()
    {
        if (selectionPanel.activeSelf == true)
        {
            nextButton.SetActive(false);
            prevButton.SetActive(false);
        }
        if(startButton.activeSelf == true || diffGenderPanel.activeSelf == true)
        {
            nextButton.SetActive(false);
          //  prevButton.SetActive(false);
        }
    }

    void ShowPanel()
    {
        if (conversationPanel.currentPanelIndex >= 18 && conversationPanel.currentPanelIndex <= 23 || conversationPanel.currentPanelIndex == 26 || conversationPanel.currentPanelIndex == 28 || conversationPanel.currentPanelIndex == 30)
        {

            if (conversationPanel.currentPanelIndex == 20)
            {
                uiManager2.Bright();
            }
            if (conversationPanel.currentPanelIndex >= 18 && conversationPanel.currentPanelIndex <= 23)
            {
                heartBeatText.text = "0";
                speedRepetitionText.text = "0";
            }
            sessionPanel.SetActive(true);
        }
        else
        {
            sessionPanel.SetActive(false);
        }

     
        if (conversationPanel.currentPanelIndex == 0 || conversationPanel.currentPanelIndex == 17 || conversationPanel.currentPanelIndex == 26 || conversationPanel.currentPanelIndex == 28 || conversationPanel.currentPanelIndex == 30 || startButton.activeSelf == true)
        {
            conversationPanel.enabled = false;
            prevButton.SetActive(false);
        }
        else
        {
            conversationPanel.enabled = true;
            prevButton.SetActive(true);
        }

        if (selectionPanel.activeSelf == true)
        {
            uiManager2.Bright();
            if (manager.isEquipmentSelected == true)
            {
                manager.genderChoice.GetComponent<TMP_Dropdown>().enabled = true;
                equipmentPanelMessage.gameObject.SetActive(false);
                ClickLockedPanel();
                //Debug.Log(isChanged);
            }
            else
            {
                manager.genderChoice.GetComponent<TMP_Dropdown>().enabled = false;
                ClickLockedPanel();

            }
            if (manager.isGenderSelected == true)
            {
                manager.timeText.GetComponent<TMP_InputField>().enabled = true;
                genderPanelMessage.gameObject.SetActive(false);

            }
            else
            {
                uiManager2.Bright();
                manager.timeText.GetComponent<TMP_InputField>().enabled = false;
                ClickLockedPanel();
            }
            if (manager.timeText.text == "")
            {
                submitButton.SetActive(true);
            }

        }



        if ((conversationPanel.currentPanelIndex == 26 && (simManager.stepCount != simManager.maxStepCount || simManager.countdown != 0)) || conversationPanel.currentPanelIndex == 28 || conversationPanel.currentPanelIndex == 30)
        {
            conversationPanel.dialogues[conversationPanel.currentDialogueIndex].panels[conversationPanel.currentPanelIndex].SetActive(false);
            nextButton.SetActive(false);
            //Debug.Log("panel deactivated");

        }

        if (manager.equipment == manager.footstep || manager.equipment == manager.bike)
        {
            if ((simManager.stepCount == simManager.maxStepCount || simManager.countdown == 0) && diffGenderPanel.activeSelf == false || startButton.activeSelf == true && conversationPanel.currentPanelIndex >= 26)
            {
                simManager.isStarted = false;
                if (conversationPanel.currentPanelIndex == 30)
                {
                    SkiptoQuestion();
                }
                conversationPanel.dialogues[conversationPanel.currentDialogueIndex].panels[conversationPanel.currentPanelIndex].SetActive(true);
                if (conversationPanel.dialogues[conversationPanel.currentDialogueIndex].panels[conversationPanel.currentPanelIndex].activeSelf == true && (conversationPanel.dialogues[conversationPanel.currentDialogueIndex].panels[conversationPanel.currentPanelIndex] != startButton &&
                    conversationPanel.dialogues[conversationPanel.currentDialogueIndex].panels[conversationPanel.currentPanelIndex] != diffGenderPanel))
                {
                    nextButton.SetActive(true);
                }

                Time.timeScale = 0;
                Debug.Log("Step Count" + simManager.stepCount);
                Debug.Log("simulation done");
                //Debug.Log("time has stopped");
            }
            else
            {
                Time.timeScale = 1f;
            }

        }

        if(sessionPanel.activeSelf == true && conversationPanel.currentPanelIndex < 26)
        {
            sessionText.text = "Sesi 1";        
        }

    }

    public void ClickLockedPanel() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject clickedUI = GetUI();
            if(clickedUI != null)
            {
               if(clickedUI.name == "GenderSelection")
               {
                    if(clickedUI.transform.parent.GetComponent<TMP_Dropdown>().enabled == false)
                    {
                        equipmentPanelMessage.gameObject.SetActive(true);
                    }
                   
               }

                if ((clickedUI.name == "Gender" || clickedUI.name == "GenderSelection") && isFirstSimFinished == true)
               {
                    ShowSelectionPanel();
                    Debug.Log("Clicked UI: " + clickedUI.name);
               }

               if(clickedUI.name == "Time")
               {
                    Debug.Log(clickedUI.transform.parent.parent);
                    if(clickedUI.transform.parent.parent.GetComponent<TMP_InputField>().enabled == false)
                    {
                        genderPanelMessage.gameObject.SetActive(true);             
                    }
                   
               }

            }
        }

    }

    private void SkiptoQuestion()
    {
        if(simManager.isMaleErgoDone == true && simManager.isFemaleErgoDone == true && simManager.isFemaleFootstepDone == true && simManager.isMaleFootstepDone == true)
        {
            conversationPanel.ShowNextPanel();
        }
      

    }

    public void DeleteSelectionAfterSimFInished()
    {
        //equipmentName.text = "Choose";
        manager.isObjectInstantiated = false;
        if (manager.genderChoice.options.Count == 2)
        {
            manager.genderChoice.options.RemoveAt(0);
        }
        manager.genderChoice.options.RemoveAt(0);
        manager.genderChoice.AddOptions(dropOptions);
        if (simManager.isMaleErgoDone == true && simManager.isFemaleErgoDone == true)
        {
            equipmentName.text = "Footstep";
            manager.equipmentChoice.options.RemoveAt(0);
            if (simManager.isFemaleFootstepDone == false)
            {
                genderName.text = "Female";
            }
        }
        else if (simManager.isMaleFootstepDone == true && simManager.isFemaleFootstepDone == true)
        {
            equipmentName.text = "Ergobike";
            if (manager.equipmentChoice.options.Count == 2)
            {
                manager.equipmentChoice.options.RemoveAt(1);
            }
            if (simManager.isFemaleErgoDone == false)
            {
                genderName.text = "Female";
            }
        }
        else
        {
            genderName.text = "";
        }
        //equipmentName.text = "";
        manager.student = null;
        manager.equipment = null;
        manager.isObjectIdle = true;
        //selectionText1.transform.parent.GetComponent<TMP_Dropdown>().enabled = false;
        manager.isEquipmentSelected = false;
        isChangedtoErgo = false;
        isChangedtoFootstep = false;
        isFirstSimFinished = true;
        time.gameObject.SetActive(false);
    }
    private List<string> dropOptions = new List<string>() { "Male", "Female" };

    private void ShowSelectionPanel()
    {
        uiManager2.Bright();
        isChanged = false;

        Debug.Log("isChanged6");
        Debug.Log("Female Ergo Done: " + simManager.isFemaleErgoDone);
        manager.genderChoice.value = -1;

        if (simManager.isMaleErgoDone == false && simManager.isFemaleErgoDone == false && equipmentName.text == "Ergobike" && isChangedtoErgo == false)
        {
            if (manager.genderChoice.options.Count == 2)
            {
                manager.genderChoice.options.RemoveAt(0);
            }
            manager.genderChoice.options.RemoveAt(0);
            manager.genderChoice.AddOptions(dropOptions);
            Debug.Log("isChanged0");
            isChangedtoErgo = true;
            isChangedtoFootstep = false;
        }

        if (simManager.isMaleFootstepDone == false && simManager.isFemaleFootstepDone == false && equipmentName.text == "Footstep" && isChangedtoFootstep == false)
        {
            if (manager.genderChoice.options.Count == 2)
            {
                manager.genderChoice.options.RemoveAt(0);
            }
            manager.genderChoice.options.RemoveAt(0);
            manager.genderChoice.AddOptions(dropOptions);
            isChangedtoFootstep = true;
            isChangedtoErgo = false;
            Debug.Log("isChanged5");
        }

        if (simManager.isMaleErgoDone == true && simManager.isFemaleErgoDone == false && equipmentName.text == "Ergobike" && isChangedtoErgo == false)
        {
            if (manager.genderChoice.options.Count == 2)
            {
                manager.genderChoice.options.RemoveAt(0);
            }
            if (manager.genderChoice.options.Count == 1)
            {
                manager.genderChoice.options[0].text = "Female";
            }
            isChangedtoErgo = true;
            isChangedtoFootstep = false;
            Debug.Log("isChanged1");
        }

        if (simManager.isMaleErgoDone == false && simManager.isFemaleErgoDone == true && equipmentName.text == "Ergobike" && isChangedtoErgo == false)
        {
            if (manager.genderChoice.options.Count == 2)
            {
                manager.genderChoice.options.RemoveAt(1);
            }
            if (manager.genderChoice.options.Count == 1)
            {
                manager.genderChoice.options[0].text = "Male";
            }
            isChangedtoErgo = true;
            isChangedtoFootstep = false;
            Debug.Log("isChanged2");
        }

        if (simManager.isMaleFootstepDone == true && simManager.isFemaleFootstepDone == false && equipmentName.text == "Footstep" && isChangedtoFootstep == false)
        {
            if (manager.genderChoice.options.Count == 2)
            {
                manager.genderChoice.options.RemoveAt(0);
            }
            if (manager.genderChoice.options.Count == 1)
            {
                manager.genderChoice.options[0].text = "Female";
            }
            isChangedtoFootstep = true;
            isChangedtoErgo = false;
            Debug.Log("isChanged3");
        }

        if (simManager.isMaleFootstepDone == false && simManager.isFemaleFootstepDone == true && equipmentName.text == "Footstep" && isChangedtoFootstep == false)
        {
            if (manager.genderChoice.options.Count == 2)
            {
                manager.genderChoice.options.RemoveAt(1);
            }
            if (manager.genderChoice.options.Count == 1)
            {
                manager.genderChoice.options[0].text = "Male";
            }
            isChangedtoFootstep = true;
            isChangedtoErgo = false;
            Debug.Log("isChanged4");
        }

    }


    public GameObject GetUI()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;   
        List<RaycastResult> result = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, result);          

        if(result.Count > 0)
        {
            return result[0].gameObject;    
        }

        return null;            
    }

    private void GenderManage()
    {
        if (selectionPanel.activeSelf == true)
        {
            if (equipmentName.text == "Ergobike")
            {
                if (simManager.isMaleErgoDone == true && simManager.isFemaleErgoDone == false)
                {
                    genderName.text = "Female";
                    manager.genderChoice.transform.GetChild(1).gameObject.SetActive(false);     
                }
                else if(simManager.isMaleErgoDone == false && simManager.isFemaleErgoDone == true)
                {
                    genderName.text = "Male";
                    manager.genderChoice.transform.GetChild(1).gameObject.SetActive(false);
                }
            }
            if(equipmentName.text == "Footstep")
            {
                if(simManager.isMaleFootstepDone == true && simManager.isFemaleFootstepDone == false)
                {
                    genderName.text = "Female";
                    manager.genderChoice.transform.GetChild(1).gameObject.SetActive(false);
                }
                else if(simManager.isMaleFootstepDone == false && simManager.isFemaleFootstepDone == true)
                {
                    genderName.text = "Male";
                    manager.genderChoice.transform.GetChild(1).gameObject.SetActive(false);
                }
            }
        }
    }

    private void DeleteTable()
    {
        if (conversationPanel.currentDialogueIndex == 3)
        {
            if (uiManager2.BreakTimeTableErgoMale.activeSelf == true)
            {
                uiManager2.BreakTimeTableErgoMale.SetActive(false);
            }
            else if (uiManager2.BreakTimeTableErgoFemale.activeSelf == true)
            {
                uiManager2.BreakTimeTableErgoFemale.SetActive(false);
            }
            else if (uiManager2.BreakTimeTableFootstepMale.activeSelf == true)
            {
                uiManager2.BreakTimeTableFootstepMale.SetActive(false);
            }
            else if (uiManager2.BreakTimeTableFootstepFemale.activeSelf == true)
            {
                uiManager2.BreakTimeTableFootstepFemale.SetActive(false);
            }
        }
    }
}
