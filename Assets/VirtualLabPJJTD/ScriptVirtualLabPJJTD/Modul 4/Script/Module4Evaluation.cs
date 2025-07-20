using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Module4Evaluation : MonoBehaviour
{
    public ConversationPanel conversationPanel;
    private Module4UIManager2 module4UImanager2;
    private Module4SimulationQuestion simQuestion;
    public TMP_Text evaluationText;
    public TMP_Text evaluationTextESession2;
    public TMP_Text evaluationTextESession3;
    public TMP_Text evaluationTextESession4;
    public TMP_Text evaluationTextESession5;
    public TMP_Text evaluationTextESession6;
    public TMP_Text evaluationTextESession7;
    public TMP_Text evaluationTextESession8;
    [SerializeField] private TMP_Text score;
    [SerializeField] private Button alterNextButton;
    [SerializeField] private Button alterPrevButton;
    private string qusetionText;
    private bool isNextButtonPressed = false;
    private bool isUP = false;
    private bool isTableUp = false;
    [SerializeField] private GameObject evaluationBox;
    [SerializeField] private GameObject canvasTable;
    [SerializeField] private GameObject InputMale;
    [SerializeField] private GameObject InputFemale;
    [SerializeField] private GameObject InputMaleFootstep;
    [SerializeField] private GameObject InputFemaleFootstep;
    [SerializeField] private GameObject InputMaleErgo;
    [SerializeField] private GameObject InputFemaleErgo;
    [SerializeField] private GameObject InputFootstepMale;
    [SerializeField] private GameObject InputFootstepFemale;
    [SerializeField] private GameObject[] correctInputMale;
    [SerializeField] private GameObject[] correctInputFemale;
    //[SerializeField] private GameObject RMaleErgo;
    //[SerializeField] private GameObject RFemaleErgo;
    //[SerializeField] private GameObject RMaleFootstep;
    //[SerializeField] private GameObject RFemaleFootstep;
    //[SerializeField] private GameObject breakTimeMale;
    //[SerializeField] private GameObject breakTimeFemale;
    //[SerializeField] private GameObject breakTimeMaleFootstep;
    //[SerializeField] private GameObject breakTimeFemaleFootstep;
    private GameObject[] table;
    // Start is called before the first frame update
    void Start()
    {
        module4UImanager2 = GetComponent<Module4UIManager2>();
        simQuestion = GetComponent<Module4SimulationQuestion>();
    }

    // Update is called once per frame
    void Update()
    {
        EvaluationText();
        EvaluationBox();
        if (conversationPanel.currentDialogueIndex == 2)
        {
            score.text = Score();
        }
    }

    private void EvaluationBox()
    {
       /* if(conversationPanel.currentDialogueIndex == 2)
        {
            for(int i = 1; i < conversationPanel.dialogues[2].panels.Length; i++)
            {
               *//* conversationPanel.dialogues[2].panels[i].transform.GetChild(0).position += new Vector3(0, 74f, 0);
                conversationPanel.dialogues[2].panels[i].transform.GetChild(1).position += new Vector3(0, 74f, 0);*//*
                conversationPanel.dialogues[2].panels[i].GetComponent<Image>().enabled = false;
            }
            evaluationBox.SetActive(true);                  
            //isUP = true;            
        }*/
    }

    private void TableCanvas()
    {
       /* if(conversationPanel.currentDialogueIndex == 2 && isTableUp == false)
        {
            isTableUp = true;
            for(int i = 0; i < 17; i++)
            {
                canvasTable.transform.GetChild(i).position += new Vector3(0, 76f, 0);
            }
        }*/
    }

    private void EvaluationText()
    {
        if (conversationPanel.currentDialogueIndex == 2)
        {
            if (module4UImanager2.simTableErgo.activeSelf == true)
            {
                if (simQuestion.isCheck1 == false)
                {
                    simQuestion.answerErgobike1.GetComponent<RectTransform>().sizeDelta = new Vector2(50f, simQuestion.answerErgobike1.GetComponent<RectTransform>().sizeDelta.y);
                    InputMaleErgo.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector3(-80.1f, 31.79f, 0);
                    evaluationText.text = "3.43";
                    evaluationText.rectTransform.anchoredPosition = new Vector3(186f, 36f, 0);
                    correctInputMale[0].SetActive(true);
                }
                if (simQuestion.isCheck2 == false)
                {
                    simQuestion.answerErgobike2.GetComponent<RectTransform>().sizeDelta = new Vector2(50f, simQuestion.answerErgobike2.GetComponent<RectTransform>().sizeDelta.y);
                    evaluationTextESession2.text = "4.13";
                    InputMaleErgo.transform.GetChild(1).GetComponent<RectTransform>().anchoredPosition = new Vector3(-80.1f, -1f, 0);
                    //evaluationTextESession2.gameObject.SetActive(true);
                    correctInputMale[1].SetActive(true);
                }
                if (simQuestion.isCheck3 == false)
                {
                    simQuestion.answerErgobike3.GetComponent<RectTransform>().sizeDelta = new Vector2(50f, simQuestion.answerErgobike3.GetComponent<RectTransform>().sizeDelta.y);
                    evaluationTextESession3.text = "5.43";
                    InputMaleErgo.transform.GetChild(2).GetComponent<RectTransform>().anchoredPosition = new Vector3(-80.1f, -31.79f, 0);
                    //evaluationTextESession3.gameObject.SetActive(true);
                    correctInputMale[2].SetActive(true);
                }
                if (simQuestion.isCheck4 == false)
                {
                    simQuestion.answerErgobike4.GetComponent<RectTransform>().sizeDelta = new Vector2(50f, simQuestion.answerErgobike4.GetComponent<RectTransform>().sizeDelta.y);
                    InputFemaleErgo.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector3(-80.1f, 32.8f, 0);
                    evaluationTextESession4.text = "4.33";
                    //evaluationTextESession4.gameObject.SetActive(true);
                    correctInputMale[3].SetActive(true);
                }

                if (simQuestion.isCheck5 == false)
                {
                    simQuestion.answerErgobike5.GetComponent<RectTransform>().sizeDelta = new Vector2(50f, simQuestion.answerErgobike5.GetComponent<RectTransform>().sizeDelta.y);
                    evaluationTextESession5.text = "2.43";
                    InputFemaleErgo.transform.GetChild(1).GetComponent<RectTransform>().anchoredPosition = new Vector3(-80.1f, 0.4f, 0);
                    //evaluationTextESession5.gameObject.SetActive(true);
                }
                if (simQuestion.isCheck6 == false)
                {
                    simQuestion.answerErgobike6.GetComponent<RectTransform>().sizeDelta = new Vector2(50, simQuestion.answerErgobike6.GetComponent<RectTransform>().sizeDelta.y);
                    evaluationTextESession6.text = "4.33";
                    InputFemaleErgo.transform.GetChild(2).GetComponent<RectTransform>().anchoredPosition = new Vector3(-80.1f, -32.8f, 0);
                    // evaluationTextESession6.gameObject.SetActive(true);
                }
                if (simQuestion.isCheck7 == false)
                {
                    simQuestion.answerEAverageErgobike1.GetComponent<RectTransform>().sizeDelta = new Vector2(50f, simQuestion.answerEAverageErgobike1.GetComponent<RectTransform>().sizeDelta.y);
                    InputMaleErgo.transform.GetChild(3).GetComponent<RectTransform>().anchoredPosition = new Vector3(38f, 0.9f, 0);
                    evaluationTextESession7.text = "5.63";
                    // evaluationTextESession7.gameObject.SetActive(true);
                }
                if (simQuestion.isCheck8 == false)
                {
                    simQuestion.answerEAverageErgobike2.GetComponent<RectTransform>().sizeDelta = new Vector2(50f, simQuestion.answerEAverageErgobike2.GetComponent<RectTransform>().sizeDelta.y);
                    evaluationTextESession8.text = "4.13";
                    InputFemaleErgo.transform.GetChild(3).GetComponent<RectTransform>().anchoredPosition = new Vector3(38f, 2.29f, 0);
                    // evaluationTextESession8.gameObject.SetActive(true);
                }
            }
            else
            {
                evaluationTextESession2.gameObject.SetActive(false);
                evaluationTextESession3.gameObject.SetActive(false);
                evaluationTextESession4.gameObject.SetActive(false);
                evaluationTextESession5.gameObject.SetActive(false);
                evaluationTextESession6.gameObject.SetActive(false);
                evaluationTextESession7.gameObject.SetActive(false);
                evaluationTextESession8.gameObject.SetActive(false);
            }
            if (module4UImanager2.simTableFootstep.activeSelf == true)
            {
                if (simQuestion.isCheck9 == false)
                {
                    simQuestion.answerFootstep1.GetComponent<RectTransform>().sizeDelta = new Vector2(50f, simQuestion.answerFootstep1.GetComponent<RectTransform>().sizeDelta.y);
                    InputFootstepMale.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector3(-23f, 78.1f, 0);
                    evaluationText.rectTransform.anchoredPosition = new Vector3(198f, 56f, 0);
                    evaluationText.text = "5.03";
                    //evaluationText.gameObject.SetActive(true);
                }
                if (simQuestion.isCheck10 == false)
                {
                    simQuestion.answerFootstep2.GetComponent<RectTransform>().sizeDelta = new Vector2(50f, simQuestion.answerFootstep2.GetComponent<RectTransform>().sizeDelta.y);
                    InputFootstepMale.transform.GetChild(1).GetComponent<RectTransform>().anchoredPosition = new Vector3(-23f, 37.82f, 0);
                    evaluationTextESession2.rectTransform.anchoredPosition = new Vector3(184f, 27f, 0);
                    evaluationTextESession2.text = "5.93";
                    //evaluationTextESession2.gameObject.SetActive(true);
                }
                if (simQuestion.isCheck11 == false)
                {
                    simQuestion.answerFootstep3.GetComponent<RectTransform>().sizeDelta = new Vector2(50f, simQuestion.answerFootstep3.GetComponent<RectTransform>().sizeDelta.y);
                    InputFootstepMale.transform.GetChild(2).GetComponent<RectTransform>().anchoredPosition = new Vector3(-23f, 2.58f, 0);
                    evaluationTextESession3.rectTransform.anchoredPosition = new Vector3(198f, 0, 0);
                    evaluationTextESession3.text = "7.63";
                    //evaluationTextESession3.gameObject.SetActive(true);
                }
                if (simQuestion.isCheck12 == false)
                {
                    simQuestion.answerFootstep4.GetComponent<RectTransform>().sizeDelta = new Vector2(50f, simQuestion.answerFootstep4.GetComponent<RectTransform>().sizeDelta.y);
                    InputFootstepFemale.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector3(-23f, 80.55f, 0);
                    evaluationTextESession4.rectTransform.anchoredPosition = new Vector3(199f, -27f, 0);
                    evaluationTextESession4.text = "6.20";
                    // evaluationTextESession4.gameObject.SetActive(true);
                }
                if (simQuestion.isCheck13 == false)
                {
                    simQuestion.answerFootstep5.GetComponent<RectTransform>().sizeDelta = new Vector2(50f, simQuestion.answerFootstep5.GetComponent<RectTransform>().sizeDelta.y);
                    evaluationTextESession5.rectTransform.anchoredPosition = new Vector3(198f, -56f, 0);
                    InputFootstepFemale.transform.GetChild(1).GetComponent<RectTransform>().anchoredPosition = new Vector3(-23f, 40.77f, 0);
                    evaluationTextESession5.text = "4.33";
                    // evaluationTextESession5.gameObject.SetActive(true);
                }
                if (simQuestion.isCheck14 == false)
                {
                    simQuestion.answerFootstep6.GetComponent<RectTransform>().sizeDelta = new Vector2(50f, simQuestion.answerFootstep6.GetComponent<RectTransform>().sizeDelta.y);
                    evaluationTextESession6.rectTransform.anchoredPosition = new Vector3(199f, -85f, 0);
                    InputFootstepFemale.transform.GetChild(2).GetComponent<RectTransform>().anchoredPosition = new Vector3(-23f, 0, 0);
                    evaluationTextESession6.text = "5.23";
                    // evaluationTextESession6.gameObject.SetActive(true);
                }
                if (simQuestion.isCheck15 == false)
                {
                    simQuestion.answerEAverageFootstep1.GetComponent<RectTransform>().sizeDelta = new Vector2(50f, simQuestion.answerEAverageFootstep1.GetComponent<RectTransform>().sizeDelta.y);
                    InputFootstepMale.transform.GetChild(3).GetComponent<RectTransform>().anchoredPosition = new Vector3(116f, 37.02f, 0);
                    evaluationTextESession7.rectTransform.anchoredPosition = new Vector3(295f, 27f, 0);
                    evaluationTextESession7.text = "6.83";
                    // evaluationTextESession7.gameObject.SetActive(true);
                }
                if (simQuestion.isCheck16 == false)
                {
                    simQuestion.answerEAverageFootstep2.GetComponent<RectTransform>().sizeDelta = new Vector2(50f, simQuestion.answerEAverageFootstep2.GetComponent<RectTransform>().sizeDelta.y);
                    evaluationTextESession8.rectTransform.anchoredPosition = new Vector3(295f, -57f, 0);
                    InputFootstepFemale.transform.GetChild(3).GetComponent<RectTransform>().anchoredPosition = new Vector3(116f, 38.82f, 0);
                    evaluationTextESession8.text = "5.46";
                    //evaluationTextESession8.gameObject.SetActive(true);
                }
            }
            if (module4UImanager2.simTableErgoMale.activeSelf == true)
            {

                if (simQuestion.isCheck17 == false)
                {
                    evaluationText.rectTransform.anchoredPosition = new Vector3(228f, 9f, 0);
                    simQuestion.answerErgobikeMale1.GetComponent<RectTransform>().sizeDelta = new Vector2(50f, simQuestion.answerErgobikeMale1.GetComponent<RectTransform>().sizeDelta.y);
                    InputMale.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector3(-9f, 15.51f, 0);
                    evaluationText.text = "3.43";
                    correctInputMale[0].SetActive(true);
                }
                if (simQuestion.isCheck18 == false)
                {
                    evaluationTextESession2.rectTransform.anchoredPosition = new Vector3(210f, -22f, 0);
                    simQuestion.answerErgobikeMale2.GetComponent<RectTransform>().sizeDelta = new Vector2(50f, simQuestion.answerErgobikeMale2.GetComponent<RectTransform>().sizeDelta.y);
                    InputMale.transform.GetChild(1).GetComponent<RectTransform>().anchoredPosition = new Vector3(-9f, -0.48f, 0);
                    evaluationTextESession2.text = " 4.13";
                    correctInputMale[1].SetActive(true);
                }
                if (simQuestion.isCheck19 == false)
                {
                    evaluationTextESession3.rectTransform.anchoredPosition = new Vector3(223f, -54f, 0);
                    simQuestion.answerErgobikeMale3.GetComponent<RectTransform>().sizeDelta = new Vector2(50f, simQuestion.answerErgobikeMale3.GetComponent<RectTransform>().sizeDelta.y);
                    InputMale.transform.GetChild(2).GetComponent<RectTransform>().anchoredPosition = new Vector3(-9f, -15.51f, 0);
                    evaluationTextESession3.text = " 5.43";
                    correctInputMale[2].SetActive(true);
                }
                if (simQuestion.isCheck20 == false)
                {
                    evaluationTextESession4.rectTransform.anchoredPosition = new Vector3(319f, -21f, 0);
                    simQuestion.answerEAverageErgobikeMale.GetComponent<RectTransform>().sizeDelta = new Vector2(50f, simQuestion.answerEAverageErgobikeMale.GetComponent<RectTransform>().sizeDelta.y);
                    InputMale.transform.GetChild(3).GetComponent<RectTransform>().anchoredPosition = new Vector3(4f, 0.43f, 0);
                    evaluationTextESession4.text = " 4.33";
                    correctInputMale[3].SetActive(true);
                }

            }
            else
            {
                evaluationTextESession2.gameObject.SetActive(false);
                evaluationTextESession3.gameObject.SetActive(false);
                evaluationTextESession4.gameObject.SetActive(false);
            }
            if (module4UImanager2.simTableErgoFemale.activeSelf == true)
            {
                if (simQuestion.isCheck21 == false)
                {
                    evaluationText.rectTransform.anchoredPosition = new Vector3(216f, 16f, 0);
                    simQuestion.answerErgobikeFemale1.GetComponent<RectTransform>().sizeDelta = new Vector2(50f, simQuestion.answerErgobikeFemale1.GetComponent<RectTransform>().sizeDelta.y);
                    InputFemale.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector3(-10.3f, 33.2f, 0);
                    evaluationText.text = " 2.43";
                    correctInputFemale[0].SetActive(true);
                }
                if (simQuestion.isCheck22 == false)
                {
                    evaluationTextESession2.rectTransform.anchoredPosition = new Vector3(203f, -19f, 0);
                    simQuestion.answerErgobikeFemale2.GetComponent<RectTransform>().sizeDelta = new Vector3(50f, simQuestion.answerErgobikeFemale2.GetComponent<RectTransform>().sizeDelta.y);
                    InputFemale.transform.GetChild(1).GetComponent<RectTransform>().anchoredPosition = new Vector3(-10.3f, -1.69f, 0);
                    evaluationTextESession2.text = " 4.33";
                    correctInputFemale[1].SetActive(true);
                }
                if (simQuestion.isCheck23 == false)
                {
                    evaluationTextESession3.rectTransform.anchoredPosition = new Vector3(216f, -50f, 0);
                    simQuestion.answerErgobikeFemale3.GetComponent<RectTransform>().sizeDelta = new Vector3(50f, simQuestion.answerErgobikeFemale3.GetComponent<RectTransform>().sizeDelta.y);
                    InputFemale.transform.GetChild(2).GetComponent<RectTransform>().anchoredPosition = new Vector3(-10.3f, -33f, 0);
                    evaluationTextESession3.text = " 5.63";
                    correctInputFemale[2].SetActive(true);
                }
                if (simQuestion.isCheck24 == false)
                {
                    evaluationTextESession4.rectTransform.anchoredPosition = new Vector3(314f, -19f, 0);
                    simQuestion.answerEAverageErgobikeFemale.GetComponent<RectTransform>().sizeDelta = new Vector3(50f, simQuestion.answerEAverageErgobikeFemale.GetComponent<RectTransform>().sizeDelta.y);
                    InputFemale.transform.GetChild(3).GetComponent<RectTransform>().anchoredPosition = new Vector3(3.2f, -1.5f, 0);
                    evaluationTextESession4.text = " 4.13";
                    correctInputFemale[3].SetActive(true);
                }

            }
            if (module4UImanager2.simTableFootstepMale.activeSelf == true)
            {
                if (simQuestion.isCheck25 == false)
                {
                    evaluationText.text = " 5.03";
                    simQuestion.answerFootstepMale1.GetComponent<RectTransform>().sizeDelta = new Vector3(55f, simQuestion.answerFootstepMale1.GetComponent<RectTransform>().sizeDelta.y);
                    InputMaleFootstep.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector3(-9.1f, 19.61f, 0);
                    correctInputMale[0].SetActive(true);
                }
                if (simQuestion.isCheck26 == false)
                {
                    evaluationText.text = " 5.93";
                    simQuestion.answerFootstepMale2.GetComponent<RectTransform>().sizeDelta = new Vector2(55f, simQuestion.answerFootstepMale2.GetComponent<RectTransform>().sizeDelta.y);
                    InputMaleFootstep.transform.GetChild(1).GetComponent<RectTransform>().anchoredPosition = new Vector3(-9.1f, 0.01f, 0);
                    correctInputMale[1].SetActive(true);
                }
                if (simQuestion.isCheck27 == false)
                {
                    evaluationText.text = " 7.63";
                    simQuestion.answerFootstepMale3.GetComponent<RectTransform>().sizeDelta = new Vector2(55f, simQuestion.answerFootstepMale3.GetComponent<RectTransform>().sizeDelta.y);
                    InputMaleFootstep.transform.GetChild(2).GetComponent<RectTransform>().anchoredPosition = new Vector3(-9.1f, -20.21f, 0);
                    correctInputMale[2].SetActive(true);
                }
                if (simQuestion.isCheck28 == false)
                {
                    evaluationText.text = " 6.20";
                    simQuestion.answerEAverageFootstepMale.GetComponent<RectTransform>().sizeDelta = new Vector2(55f, simQuestion.answerEAverageFootstepMale.GetComponent<RectTransform>().sizeDelta.y);
                    InputMaleFootstep.transform.GetChild(3).GetComponent<RectTransform>().anchoredPosition = new Vector3(4.1f, -0.18f, 0);
                    correctInputMale[3].SetActive(true);
                }
            }
            if (module4UImanager2.simTableFootstepFemale.activeSelf == true)
            {
                if (simQuestion.isCheck29 == false)
                {
                    evaluationText.text = " 4.33";
                    simQuestion.answerFootstepFemale1.GetComponent<RectTransform>().sizeDelta = new Vector3(55f, simQuestion.answerFootstepFemale1.GetComponent<RectTransform>().sizeDelta.y);
                    InputFemaleFootstep.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector3(-9.1f, 15.51f, 0);
                    correctInputFemale[0].SetActive(true);
                }
                if (simQuestion.isCheck30 == false)
                {
                    evaluationText.text = " 5.23";
                    simQuestion.answerFootstepFemale2.GetComponent<RectTransform>().sizeDelta = new Vector3(55f, simQuestion.answerFootstepFemale2.GetComponent<RectTransform>().sizeDelta.y);
                    InputFemaleFootstep.transform.GetChild(1).GetComponent<RectTransform>().anchoredPosition = new Vector3(-9.1f, -0.48f, 0);
                    correctInputFemale[1].SetActive(true);
                }
                if (simQuestion.isCheck31 == false)
                {
                    evaluationText.text = " 6.83";
                    simQuestion.answerFootstepFemale3.GetComponent<RectTransform>().sizeDelta = new Vector3(55f, simQuestion.answerFootstepFemale3.GetComponent<RectTransform>().sizeDelta.y);
                    InputFemaleFootstep.transform.GetChild(2).GetComponent<RectTransform>().anchoredPosition = new Vector3(-9.1f, -15.51f, 0);
                    correctInputFemale[2].SetActive(true);
                }
                if (simQuestion.isCheck32 == false)
                {
                    evaluationText.text = " 5.46";
                    simQuestion.answerEAverageFootstepFemale.GetComponent<RectTransform>().sizeDelta = new Vector3(55f, simQuestion.answerEAverageFootstepFemale.GetComponent<RectTransform>().sizeDelta.y);
                    InputFemaleFootstep.transform.GetChild(3).GetComponent<RectTransform>().anchoredPosition = new Vector3(4.35f, -0.48f, 0);
                    correctInputFemale[3].SetActive(true);
                }

            }
            if (module4UImanager2.RTableErgoMale.activeSelf == true)
            {
                if (simQuestion.isCheck33 == false)
                {

                    if (simQuestion.RAnswerErgoMale.text == "1.0857")
                    {
                        Debug.Log("wrong answer");
                        conversationPanel.dialogues[2].panels[conversationPanel.currentPanelIndex].transform.GetChild(2).GetChild(0).GetComponent<TMP_Text>().color = new Color(1f, 0, 0);
                    }
                    else if (simQuestion.RAnswerErgoMale.text == "1.03635")
                    {
                        conversationPanel.dialogues[2].panels[conversationPanel.currentPanelIndex].transform.GetChild(4).GetChild(0).GetComponent<TMP_Text>().color = new Color(1f, 0, 0);
                    }
                    else if (simQuestion.RAnswerErgoMale.text == "1.13505")
                    {
                        conversationPanel.dialogues[2].panels[conversationPanel.currentPanelIndex].transform.GetChild(5).GetChild(0).GetComponent<TMP_Text>().color = new Color(1f, 0f, 0f);
                    }

                }
                conversationPanel.dialogues[2].panels[conversationPanel.currentPanelIndex].transform.GetChild(3).GetChild(0).GetComponent<TMP_Text>().color = new Color(0f, 1f, 0f);
            }
            if (module4UImanager2.RTableErgoFemale.activeSelf == true)
            {
                if (simQuestion.isCheck34 == false)
                {
                    if (simQuestion.RAnswerErgoFemale.text == "1.0934")
                    {
                        conversationPanel.dialogues[2].panels[conversationPanel.currentPanelIndex].transform.GetChild(2).GetChild(0).GetComponent<TMP_Text>().color = new Color(1f, 0, 0);
                    }
                    else if (simQuestion.RAnswerErgoFemale.text == "1.1431")
                    {
                        conversationPanel.dialogues[2].panels[conversationPanel.currentPanelIndex].transform.GetChild(3).GetChild(0).GetComponent<TMP_Text>().color = new Color(1f, 0, 0);
                    }
                    else if (simQuestion.RAnswerErgoFemale.text == "1.0437")
                    {
                        conversationPanel.dialogues[2].panels[conversationPanel.currentPanelIndex].transform.GetChild(4).GetChild(0).GetComponent<TMP_Text>().color = new Color(1f, 0, 0);
                    }

                }
                conversationPanel.dialogues[2].panels[conversationPanel.currentPanelIndex].transform.GetChild(5).GetChild(0).GetComponent<TMP_Text>().color = new Color(0, 1f, 0);
            }
            if (module4UImanager2.RTableFootstepMale.activeSelf == true)
            {
                if (simQuestion.isCheck36 == false)
                {
                    if (simQuestion.RAnswerFootstepMale.text == "1.0835")
                    {
                        conversationPanel.dialogues[2].panels[conversationPanel.currentPanelIndex].transform.GetChild(2).GetChild(0).GetComponent<TMP_Text>().color = new Color(1f, 0, 0);
                    }
                    else if (simQuestion.RAnswerFootstepMale.text == "1.03425")
                    {
                        conversationPanel.dialogues[2].panels[conversationPanel.currentPanelIndex].transform.GetChild(4).GetChild(0).GetComponent<TMP_Text>().color = new Color(1f, 0, 0);
                    }
                    else if (simQuestion.RAnswerFootstepMale.text == "1.13275")
                    {
                        conversationPanel.dialogues[2].panels[conversationPanel.currentPanelIndex].transform.GetChild(5).GetChild(0).GetComponent<TMP_Text>().color = new Color(1f, 0, 0);
                    }

                }
                conversationPanel.dialogues[2].panels[conversationPanel.currentPanelIndex].transform.GetChild(3).GetChild(0).GetComponent<TMP_Text>().color = new Color(0, 1f, 0);
            }

            if (module4UImanager2.RTableFootstepFemale.activeSelf == true)
            {
                if (simQuestion.isCheck37 == false)
                {
                    if (simQuestion.RAnswerFootstepFemale.text == "1.0857")
                    {
                        conversationPanel.dialogues[2].panels[conversationPanel.currentPanelIndex].transform.GetChild(2).GetChild(0).GetComponent<TMP_Text>().color = new Color(1f, 0, 0);
                    }
                    else if (simQuestion.RAnswerFootstepFemale.text == "1.13505")
                    {
                        conversationPanel.dialogues[2].panels[conversationPanel.currentPanelIndex].transform.GetChild(3).GetChild(0).GetComponent<TMP_Text>().color = new Color(1f, 0f, 0f);
                    }
                    else if (simQuestion.RAnswerFootstepFemale.text == "1.03635")
                    {
                        conversationPanel.dialogues[2].panels[conversationPanel.currentPanelIndex].transform.GetChild(4).GetChild(0).GetComponent<TMP_Text>().color = new Color(1f, 0, 0);
                    }

                }
                conversationPanel.dialogues[2].panels[conversationPanel.currentPanelIndex].transform.GetChild(5).GetChild(0).GetComponent<TMP_Text>().color = new Color(0, 1f, 0);
            }
            if (module4UImanager2.BreakTimeTableErgoMale.activeSelf == true)
            {
                Debug.Log("wrong Answer:" + simQuestion.OptionErgoMale[2]);
                if (simQuestion.isCheck35 == false)
                {
                    Debug.Log("Wrong answer: " + simQuestion.OptionErgoMale[2]);
                    if (simQuestion.breakTimeErgoMale.text == simQuestion.OptionErgoMale[0].ToString())
                    {
                        conversationPanel.dialogues[2].panels[conversationPanel.currentPanelIndex].transform.GetChild(2).GetChild(0).GetComponent<TMP_Text>().color = new Color(1f, 0, 0);
                    }
                    else if (simQuestion.breakTimeErgoMale.text == simQuestion.OptionErgoMale[1].ToString())
                    {
                        conversationPanel.dialogues[2].panels[conversationPanel.currentPanelIndex].transform.GetChild(3).GetChild(0).GetComponent<TMP_Text>().color = new Color(1f, 0f, 0f);
                    }
                    else if (simQuestion.breakTimeErgoMale.text == simQuestion.OptionErgoMale[2].ToString())
                    {
                        Debug.Log("wrong answer: " + simQuestion.OptionErgoMale[2]);
                        conversationPanel.dialogues[2].panels[conversationPanel.currentPanelIndex].transform.GetChild(4).GetChild(0).GetComponent<TMP_Text>().color = new Color(1f, 0, 0);
                    }

                }
                conversationPanel.dialogues[2].panels[conversationPanel.currentPanelIndex].transform.GetChild(5).GetChild(0).GetComponent<TMP_Text>().color = new Color(0, 1f, 0);
            }
            if (module4UImanager2.BreakTimeTableFootstepMale.activeSelf == true)
            {
                if (simQuestion.isCheck38 == false)
                {
                    if (simQuestion.breakTimeFootstepMale.text == simQuestion.OptionFootstepMale[0].ToString())
                    {
                        conversationPanel.dialogues[2].panels[conversationPanel.currentPanelIndex].transform.GetChild(2).GetChild(0).GetComponent<TMP_Text>().color = new Color(1f, 0, 0);
                    }
                    else if (simQuestion.breakTimeFootstepMale.text == simQuestion.OptionFootstepMale[1].ToString())
                    {
                        conversationPanel.dialogues[2].panels[conversationPanel.currentPanelIndex].transform.GetChild(3).GetChild(0).GetComponent<TMP_Text>().color = new Color(1f, 0, 0);
                    }
                    else if (simQuestion.breakTimeFootstepMale.text == simQuestion.OptionFootstepMale[2].ToString())
                    {
                        conversationPanel.dialogues[2].panels[conversationPanel.currentPanelIndex].transform.GetChild(4).GetChild(0).GetComponent<TMP_Text>().color = new Color(1f, 0, 0);
                    }

                }
                conversationPanel.dialogues[2].panels[conversationPanel.currentPanelIndex].transform.GetChild(5).GetChild(0).GetComponent<TMP_Text>().color = new Color(0, 1f, 0);
            }
            if (module4UImanager2.BreakTimeTableErgoFemale.activeSelf == true)
            {
                if (simQuestion.isCheck39 == false)
                {
                    if (simQuestion.breakTimeErgoFemale.text == simQuestion.OptionErgoFemale[0].ToString())
                    {
                        conversationPanel.dialogues[2].panels[conversationPanel.currentPanelIndex].transform.GetChild(2).GetChild(0).GetComponent<TMP_Text>().color = new Color(1f, 0, 0);
                    }
                    else if (simQuestion.breakTimeErgoFemale.text == simQuestion.OptionErgoFemale[1].ToString())
                    {
                        conversationPanel.dialogues[2].panels[conversationPanel.currentPanelIndex].transform.GetChild(3).GetChild(0).GetComponent<TMP_Text>().color = new Color(1f, 0, 0);
                    }
                    else if (simQuestion.breakTimeErgoFemale.text == simQuestion.OptionErgoFemale[2].ToString())
                    {
                        conversationPanel.dialogues[2].panels[conversationPanel.currentPanelIndex].transform.GetChild(4).GetChild(0).GetComponent<TMP_Text>().color = new Color(1f, 0, 0);
                    }

                }
                conversationPanel.dialogues[2].panels[conversationPanel.currentPanelIndex].transform.GetChild(5).GetChild(0).GetComponent<TMP_Text>().color = new Color(0, 1f, 0);
            }
            if (module4UImanager2.BreakTimeTableFootstepFemale.activeSelf == true)
            {
                if (simQuestion.isCheck40 == false)
                {
                    if (simQuestion.breakTimeFootstepFemale.text == simQuestion.OptionFootstepFemale[0].ToString())
                    {
                        conversationPanel.dialogues[2].panels[conversationPanel.currentPanelIndex].transform.GetChild(2).GetChild(0).GetComponent<TMP_Text>().color = new Color(1f, 0, 0);
                    }
                    else if (simQuestion.breakTimeFootstepFemale.text == simQuestion.OptionFootstepFemale[1].ToString())
                    {
                        conversationPanel.dialogues[2].panels[conversationPanel.currentPanelIndex].transform.GetChild(3).GetChild(0).GetComponent<TMP_Text>().color = new Color(1f, 0, 0);
                    }
                    else if (simQuestion.breakTimeFootstepFemale.text == simQuestion.OptionFootstepFemale[2].ToString())
                    {
                        conversationPanel.dialogues[2].panels[conversationPanel.currentPanelIndex].transform.GetChild(4).GetChild(0).GetComponent<TMP_Text>().color = new Color(1f, 0, 0);
                    }

                }
                conversationPanel.dialogues[2].panels[conversationPanel.currentPanelIndex].transform.GetChild(5).GetChild(0).GetComponent<TMP_Text>().color = new Color(0, 1f, 0);
            }

        }
    }
    private bool isScored = false;
    private int additionalQuestion = 0;
    private string Score()
    {
       
        if (isScored == false)
        {
            if (simQuestion.ergoTableExist == true)
            {
                additionalQuestion += 7;
            }
            if (simQuestion.footstepTableExist == true)
            {
                additionalQuestion += 7;
            }
            if (simQuestion.ergoTableMaleExist == true)
            {
                additionalQuestion += 3;
            }
            if (simQuestion.ergoTableFemaleExist == true)
            {
                additionalQuestion += 3;
            }
            if (simQuestion.footstepTableMaleExist == true)
            {
                additionalQuestion += 3;
            }
            if (simQuestion.foostepTableFemaleExist == true)
            {
                additionalQuestion += 3;
            }
            isScored = true;
          
        }


        float score = (simQuestion.correctCount / (conversationPanel.dialogues[1].panels.Length+additionalQuestion)) * 100;
        Debug.Log(simQuestion.correctCount / conversationPanel.dialogues[1].panels.Length+additionalQuestion);
        Debug.Log("Question: " + (conversationPanel.dialogues[1].panels.Length+additionalQuestion));
        Debug.Log("Additional Question: " + additionalQuestion);
        string scoreText = score.ToString();
        Debug.Log(scoreText);
        return scoreText;
    }

}



