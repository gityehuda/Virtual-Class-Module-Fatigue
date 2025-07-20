using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using System.Management.Instrumentation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Module4UIManager2 : MonoBehaviour
{
    private Module4SimManager simManager;
    private Module4Manager manager;
    private Module4UIManager module4UIManager;
    public ConversationPanel conversationPanel;
    private bool isChangedtoErgo;
    private bool isChangedtoFootstep;
    private bool isFirstSimFinished;
    public GameObject simTableErgo;
    public GameObject simTableFootstep;
    public GameObject simTableErgoMale;
    public GameObject simTableErgoFemale;
    public GameObject simTableFootstepMale;
    public GameObject simTableFootstepFemale;
    public GameObject RTableErgoMale;
    public GameObject RTableErgoFemale;
    public GameObject RTableErgo;
    public GameObject RTableFootstep;
    public GameObject RTableFootstepMale;
    public GameObject RTableFootstepFemale;
    public GameObject BreakTimeTableErgoMale;
    public GameObject BreakTimeTableErgoFemale;
    public GameObject BreakTimeTableFootstepMale;
    public GameObject BreakTimeTableFootstepFemale;
    [SerializeField] private List<TMP_Text> questionList;
    public GameObject[] evaluatedQuestion;
    [SerializeField] private GameObject overallScore;
    private bool isCalled = false;
    private Transform currentIndex;
    private Module4SimulationQuestion simQuestion;

    void Start()
    {
        simManager = GetComponent<Module4SimManager>();
        manager = GetComponent<Module4Manager>();
        module4UIManager = GetComponent<Module4UIManager>();
        simQuestion = GetComponent<Module4SimulationQuestion>();
    }

    void Update()
    {
        ShowScore();
        QuestionManager();
        QuestionIndex();
        Evaluation();
        // EvaluationText();
        ResetText();
    }

    public void Bright()
    {

        if (module4UIManager.selectionPanel.activeSelf == true)
        {
            if (module4UIManager.equipmentName.text != "")
            {
                module4UIManager.genderName.transform.parent.GetComponent<Image>().color = new Color(1f, 1f, 1f);
            }

            if (module4UIManager.genderName.text != "")
            {
                module4UIManager.time.transform.GetComponent<Image>().color = new Color(1f, 1f, 1f);
            }
            /* else
             {
                 genderName.transform.parent.GetComponent<Image>().color = new Color(0.71f, 0.71f, 0.71f);
             }*/

        }

    }

    private void ResetText()
    {
        if (module4UIManager.conversationPanel.currentPanelIndex > 25)
        {
            module4UIManager.simulationText.text = "Oke, sekarang kita akan melakukan simulasi praktikum menggunakan alat yang anda pilih yaitu menggunakan alat ergobike dengan subject eksperimen wanita dan lama waktu kegiatan adalah 30 menit. Pilihan ini akan mempengaruhi simulasi yang akan dijalankan dan memnugkinkan anda untuk mengamati perbedaan respons gender dan parameter alat yang digunakan";
            module4UIManager.equipmentText.text = "Sesuai dengan pilihan anda, alat yang digunakan pada simulasi kali ini adalah ";
            module4UIManager.speedText.text = "Grafik dan angka digital yang menampilkan detak jantung sebelum dan indicator kecepatan akan menampilkan kecepatan kayuhan secara real-time. Kedua indikator ini akan mencatat data fisiologis yang berkaitan dengan perubahan detak jantung akibat aktivitas fisik. ";
            module4UIManager.footstepSelected = false;
            module4UIManager.ergobikeSelected = false;
        }

    }

    public void ChangeText()
    {
        if (module4UIManager.equipmentName.text == "Ergobike" && module4UIManager.ergobikeSelected == false)
        {
            module4UIManager.timeText.text = module4UIManager.timeText.text.Replace("Footstep", "Ergobike");
            module4UIManager.simulationText.text = module4UIManager.simulationText.text.Replace("Footstep", "ergobike");
            module4UIManager.equipmentText.text = module4UIManager.equipmentText.text + "Ergobike. Ergobike digunakan untuk mengukur pengaruh aktivitas fisik terhadap detak jantung dan konsumsi energi. Ergobike memiliki pengaturan kecepatan yang dapat disesuaikan untuk melihat dampaknya terhadap fisiologi tubuh";
            module4UIManager.speedText.text = module4UIManager.speedText.text.Replace("repetisi akan menampilkan jumlah repetisi secara real time", "kecepatan akan menampilkan kecepatan kayuhan secara real time");
            module4UIManager.repetitionSpeedText.text = "Kecepatan       :";
            module4UIManager.speedRepetitionText.text = "0";
            module4UIManager.repetitionKiloperHourText.text = "km/h";
            module4UIManager.ergobikeSelected = true;
            module4UIManager.footstepSelected = false;
        }
        else if (module4UIManager.equipmentName.text == "Footstep" && module4UIManager.footstepSelected == false)
        {
            module4UIManager.timeText.text = module4UIManager.timeText.text.Replace("Ergobike", "Footstep");
            module4UIManager.simulationText.text = module4UIManager.simulationText.text.Replace("ergobike", "Footstep");
            module4UIManager.equipmentText.text = module4UIManager.equipmentText.text + "Footstep merupakan alat yang mensimulasikan gerakan naik turun tangga dengan variasi repetisi, digunakan untuk membandingkan efek kelelahan dengan metode latihan yang berbeda";
            module4UIManager.speedText.text = module4UIManager.speedText.text.Replace("kecepatan akan menampilkan kecepatan kayuhan secara real time", "repetisi akan menampilkan jumlah repetisi secara real time");
            module4UIManager.repetitionSpeedText.text = "Repetisi           :";
            module4UIManager.speedRepetitionText.text = "0";
            module4UIManager.repetitionKiloperHourText.text = "repetisi";
            module4UIManager.footstepSelected = true;
            module4UIManager.ergobikeSelected = false;
        }


        if (module4UIManager.genderName.text == "Male")
        {
            module4UIManager.simulationText.text = module4UIManager.simulationText.text.Replace("wanita", "pria");
        }
        else if (module4UIManager.genderName.text == "Female")
        {
            module4UIManager.simulationText.text = module4UIManager.simulationText.text.Replace("pria", "wanita");
        }

        Debug.Log(module4UIManager.simulationText.text);
        if (module4UIManager.time.text != "")
        {
            module4UIManager.simulationText.text = module4UIManager.simulationText.text.Replace("30", module4UIManager.time.text);
            Debug.Log(module4UIManager.time.text);
        }

    }

    private void ShowScore()
    {

        if (conversationPanel.currentDialogueIndex == 1 || conversationPanel.currentDialogueIndex == 2)
        {
            if ((simManager.isMaleErgoDone == true && simManager.isFemaleErgoDone == true))
            {
                if ((conversationPanel.dialogues[conversationPanel.currentDialogueIndex].panels[conversationPanel.currentPanelIndex].transform.GetChild(0).GetComponent<TMP_Text>().text.Contains("tabel") == true &&
                    conversationPanel.dialogues[conversationPanel.currentDialogueIndex].panels[conversationPanel.currentPanelIndex].transform.GetChild(0).GetComponent<TMP_Text>().text.Contains("Ergobike") == true))
                {
                    simTableErgo.SetActive(true);
                }
                else
                {
                    simTableErgo.SetActive(false);
                }

            }
            else if ((simManager.isFemaleErgoDone == true && simManager.isMaleErgoDone == false))
            {
                if ((conversationPanel.dialogues[conversationPanel.currentDialogueIndex].panels[conversationPanel.currentPanelIndex].transform.GetChild(0).GetComponent<TMP_Text>().text.Contains("tabel") == true &&
                    conversationPanel.dialogues[conversationPanel.currentDialogueIndex].panels[conversationPanel.currentPanelIndex].transform.GetChild(0).GetComponent<TMP_Text>().text.Contains("Ergobike") == true))
                {
                    simTableErgoFemale.SetActive(true);
                }
                else
                {
                    simTableErgoFemale.SetActive(false);
                }

            }
            else if ((simManager.isMaleErgoDone == true && simManager.isFemaleErgoDone == false))
            {
                if ((conversationPanel.dialogues[conversationPanel.currentDialogueIndex].panels[conversationPanel.currentPanelIndex].transform.GetChild(0).GetComponent<TMP_Text>().text.Contains("tabel") == true &&
                    conversationPanel.dialogues[conversationPanel.currentDialogueIndex].panels[conversationPanel.currentPanelIndex].transform.GetChild(0).GetComponent<TMP_Text>().text.Contains("Ergobike") == true))
                {
                    simTableErgoMale.SetActive(true);
                    Debug.Log("Sim Table Ergo Male");
                }
                else
                {
                    simTableErgoMale.SetActive(false);
                }
            }

            if ((simManager.isMaleFootstepDone == true && simManager.isFemaleFootstepDone == true))
            {
                if ((conversationPanel.dialogues[conversationPanel.currentDialogueIndex].panels[conversationPanel.currentPanelIndex].transform.GetChild(0).GetComponent<TMP_Text>().text.Contains("tabel") == true &&
                    conversationPanel.dialogues[conversationPanel.currentDialogueIndex].panels[conversationPanel.currentPanelIndex].transform.GetChild(0).GetComponent<TMP_Text>().text.Contains("Footstep") == true))
                {
                    simTableFootstep.SetActive(true);
                }
                else
                {
                    simTableFootstep.SetActive(false);
                }

            }
            else if (simManager.isMaleFootstepDone == true && simManager.isFemaleFootstepDone == false)
            {
                if (conversationPanel.dialogues[conversationPanel.currentDialogueIndex].panels[conversationPanel.currentPanelIndex].transform.GetChild(0).GetComponent<TMP_Text>().text.Contains("tabel") == true &&
                    conversationPanel.dialogues[conversationPanel.currentDialogueIndex].panels[conversationPanel.currentPanelIndex].transform.GetChild(0).GetComponent<TMP_Text>().text.Contains("Footstep.") == true)
                {
                    simTableFootstepMale.SetActive(true);
                }
                else
                {
                    simTableFootstepMale.SetActive(false);
                }
            }
            else if ((simManager.isMaleFootstepDone == false && simManager.isFemaleFootstepDone == true))
            {
                if ((conversationPanel.dialogues[conversationPanel.currentDialogueIndex].panels[conversationPanel.currentPanelIndex].transform.GetChild(0).GetComponent<TMP_Text>().text.Contains("tabel") == true &&
                    conversationPanel.dialogues[conversationPanel.currentDialogueIndex].panels[conversationPanel.currentPanelIndex].transform.GetChild(0).GetComponent<TMP_Text>().text.Contains("Footstep") == true))
                {
                    simTableFootstepFemale.SetActive(true);
                }
                else
                {
                    simTableFootstepFemale.SetActive(false);
                }
            }

        }

        if (conversationPanel.currentDialogueIndex == 1 || conversationPanel.currentDialogueIndex == 2)
        {
            if ((simManager.isMaleErgoDone == true && simManager.isFemaleErgoDone == false) || (simManager.isMaleErgoDone == true && simManager.isFemaleErgoDone == true))
            {
                if ((conversationPanel.dialogues[conversationPanel.currentDialogueIndex].panels[conversationPanel.currentPanelIndex].transform.GetChild(0).GetComponent<TMP_Text>().text.Contains("(r)") == true &&
                    conversationPanel.dialogues[conversationPanel.currentDialogueIndex].panels[conversationPanel.currentPanelIndex].transform.GetChild(0).GetComponent<TMP_Text>().text.Contains("Ergobike pria") == true))
                {
                    RTableErgoMale.SetActive(true);
                }
                else
                {
                    RTableErgoMale.SetActive(false);

                }

            }
            if (((simManager.isMaleErgoDone == false && simManager.isFemaleErgoDone == true) || (simManager.isMaleErgoDone == true && simManager.isFemaleErgoDone == true)))
            {
                if ((conversationPanel.dialogues[conversationPanel.currentDialogueIndex].panels[conversationPanel.currentPanelIndex].transform.GetChild(0).GetComponent<TMP_Text>().text.Contains("(r)") == true &&
                    conversationPanel.dialogues[conversationPanel.currentDialogueIndex].panels[conversationPanel.currentPanelIndex].transform.GetChild(0).GetComponent<TMP_Text>().text.Contains("Ergobike wanita") == true))
                {
                    RTableErgoFemale.SetActive(true);
                    simTableErgoFemale.SetActive(false);
                }
                else
                {
                    RTableErgoFemale.SetActive(false);
                }

            }

            if (((simManager.isMaleFootstepDone == true && simManager.isFemaleFootstepDone == false) || (simManager.isMaleFootstepDone == true && simManager.isFemaleFootstepDone == true)))
            {
                if ((conversationPanel.dialogues[conversationPanel.currentDialogueIndex].panels[conversationPanel.currentPanelIndex].transform.GetChild(0).GetComponent<TMP_Text>().text.Contains("(r)") == true &&
                    conversationPanel.dialogues[conversationPanel.currentDialogueIndex].panels[conversationPanel.currentPanelIndex].transform.GetChild(0).GetComponent<TMP_Text>().text.Contains("Footstep pria") == true))
                {
                    RTableFootstepMale.SetActive(true);
                    simTableFootstepMale.SetActive(false);
                }
                else
                {
                    RTableFootstepMale.SetActive(false);
                }

            }
            if ((simManager.isMaleFootstepDone == false && simManager.isFemaleFootstepDone == true) || (simManager.isMaleFootstepDone == true && simManager.isFemaleFootstepDone == true))
            {
                if ((conversationPanel.dialogues[conversationPanel.currentDialogueIndex].panels[conversationPanel.currentPanelIndex].transform.GetChild(0).GetComponent<TMP_Text>().text.Contains("(r)") == true &&
                    conversationPanel.dialogues[conversationPanel.currentDialogueIndex].panels[conversationPanel.currentPanelIndex].transform.GetChild(0).GetComponent<TMP_Text>().text.Contains("Footstep wanita") == true))
                {
                    RTableFootstepFemale.SetActive(true);
                    simTableFootstepFemale.SetActive(false);
                }
                else
                {
                    RTableFootstepFemale.SetActive(false);
                }

            }
        }

        if (conversationPanel.currentDialogueIndex == 1 || conversationPanel.currentDialogueIndex == 2)
        {
            if ((simManager.isMaleErgoDone == true && simManager.isFemaleErgoDone == false) || (simManager.isMaleErgoDone == true && simManager.isFemaleErgoDone == true))
            {
                if (conversationPanel.dialogues[conversationPanel.currentDialogueIndex].panels[conversationPanel.currentPanelIndex].transform.GetChild(0).GetComponent<TMP_Text>().text.Contains("break time") == true &&
                    conversationPanel.dialogues[conversationPanel.currentDialogueIndex].panels[conversationPanel.currentPanelIndex].transform.GetChild(0).GetComponent<TMP_Text>().text.Contains("Ergobike pria") == true)
                {
                    BreakTimeTableErgoMale.SetActive(true);
                    RTableErgoMale.SetActive(false);
                }
                else
                {
                    BreakTimeTableErgoMale.SetActive(false);
                }
            }
            if (((simManager.isMaleErgoDone == false && simManager.isFemaleErgoDone == true) || (simManager.isMaleErgoDone == true && simManager.isFemaleErgoDone == true)))
            {
                if ((conversationPanel.dialogues[conversationPanel.currentDialogueIndex].panels[conversationPanel.currentPanelIndex].transform.GetChild(0).GetComponent<TMP_Text>().text.Contains("break time") == true &&
                    conversationPanel.dialogues[conversationPanel.currentDialogueIndex].panels[conversationPanel.currentPanelIndex].transform.GetChild(0).GetComponent<TMP_Text>().text.Contains("Ergobike wanita") == true))
                {
                    BreakTimeTableErgoFemale.SetActive(true);
                }
                else
                {
                    BreakTimeTableErgoFemale.SetActive(false);
                }

            }
            if (((simManager.isMaleFootstepDone == true && simManager.isFemaleFootstepDone == false) || (simManager.isMaleFootstepDone == true && simManager.isFemaleFootstepDone == true)))
            {
                if ((conversationPanel.dialogues[conversationPanel.currentDialogueIndex].panels[conversationPanel.currentPanelIndex].transform.GetChild(0).GetComponent<TMP_Text>().text.Contains("break time") == true &&
                    conversationPanel.dialogues[conversationPanel.currentDialogueIndex].panels[conversationPanel.currentPanelIndex].transform.GetChild(0).GetComponent<TMP_Text>().text.Contains("Footstep pria") == true))
                {
                    BreakTimeTableFootstepMale.SetActive(true);
                }
                else
                {
                    BreakTimeTableFootstepMale.SetActive(false);
                }

            }
            if (((simManager.isMaleFootstepDone == false && simManager.isFemaleFootstepDone == true) || (simManager.isMaleFootstepDone == true && simManager.isFemaleFootstepDone == true)))
            {
                if ((conversationPanel.dialogues[conversationPanel.currentDialogueIndex].panels[conversationPanel.currentPanelIndex].transform.GetChild(0).GetComponent<TMP_Text>().text.Contains("break time") == true &&
                    conversationPanel.dialogues[conversationPanel.currentDialogueIndex].panels[conversationPanel.currentPanelIndex].transform.GetChild(0).GetComponent<TMP_Text>().text.Contains("Footstep wanita") == true))
                {
                    BreakTimeTableFootstepFemale.SetActive(true);
                }
                else
                {
                    BreakTimeTableFootstepFemale.SetActive(false);
                }

            }

        }
    }

    private void QuestionManager()
    {
        if (conversationPanel.currentPanelIndex == 33 && isCalled == false)
        {
            int erqoQuestionClear = 0;
            for (int i = 0; i < questionList.Count; i++)
            {
                if (simManager.isMaleErgoDone == true && simManager.isFemaleErgoDone == false)
                {
                    if (questionList[i].text.Contains("Ergobike wanita") == true)
                    {
                        questionList.RemoveAt(i);
                    }

                }
                if (simManager.isMaleErgoDone == false && simManager.isFemaleErgoDone == true)
                {
                    if (questionList[i].text.Contains("Ergobike pria") == true)
                    {
                        questionList.RemoveAt(i);
                    }
                }
                if (simManager.isMaleErgoDone == false && simManager.isFemaleErgoDone == false)
                {
                    if (questionList[i].text.Contains("Ergobike pria") == true)
                    {
                        Debug.Log(questionList[i].text);
                        questionList.RemoveAt(i);
                        Debug.Log("Index: " + i);
                        erqoQuestionClear++;
                    }
                    if (questionList[i].text.Contains("Ergobike wanita") == true)
                    {
                        Debug.Log(questionList[i].text);
                        questionList.RemoveAt(i);
                        Debug.Log("Index: " + i);
                        erqoQuestionClear++;
                    }
                    if (questionList[i].text.Contains("aktivitas Ergobike") == true)
                    {
                        questionList.RemoveAt(i);
                    }

                    Debug.Log("Ergo Questions Clear: " + erqoQuestionClear);
                    Debug.Log("Index: " + i);
                    Debug.Log("Question List: " + questionList.Count);
                }
                if (simManager.isMaleFootstepDone == true && simManager.isFemaleFootstepDone == false)
                {
                    if (questionList[i].text.Contains("Footstep wanita") == true)
                    {
                        questionList.RemoveAt(i);
                    }
                }
                if (simManager.isMaleFootstepDone == false && simManager.isFemaleFootstepDone == true)
                {
                    if (questionList[i].text.Contains("Footstep pria") == true)
                    {
                        questionList.RemoveAt(i);
                    }
                }
                if (simManager.isMaleFootstepDone == false && simManager.isFemaleFootstepDone == false)
                {
                    if (questionList[i].text.Contains("Footstep pria") == true)
                    {
                        Debug.Log(questionList[i].text);
                        questionList.RemoveAt(i);
                        Debug.Log("Index: " + i);
                    }
                    if (questionList[i].text.Contains("Footstep wanita") == true)
                    {
                        questionList.RemoveAt(i);
                        Debug.Log("Index" + i);

                    }
                    if (i > questionList.Count - 1)
                    {
                        i = 0;
                    }
                    if (questionList[i].text.Contains("aktivitas Footstep") == true)
                    {
                        questionList.RemoveAt(i);
                    }
                }
                if (i == questionList.Count && erqoQuestionClear < 5)
                {
                    i = 0;
                }
            }
            isCalled = true;

            Array.Resize(ref conversationPanel.dialogues[1].panels, questionList.Count);
            for (int i = 0; i < questionList.Count; i++)
            {
                conversationPanel.dialogues[1].panels[i] = questionList[i].transform.parent.gameObject;
                int length = conversationPanel.dialogues[1].panels.Length;
                length++;
                // conversationPanel.dialogues[1].panels.Length = length;  
            }
        }

    }

    private void QuestionIndex()
    {
        if (conversationPanel.currentDialogueIndex == 1)
        {
            if (conversationPanel.dialogues[conversationPanel.currentDialogueIndex].panels[conversationPanel.currentPanelIndex].transform.GetChild(1) != null)
            {
                currentIndex = conversationPanel.dialogues[conversationPanel.currentDialogueIndex].panels[conversationPanel.currentPanelIndex].transform.GetChild(1);
                int actualIndex = conversationPanel.currentPanelIndex + 1;
                currentIndex.GetComponent<TMP_Text>().text = actualIndex.ToString();
            }

        }
    }

    private void Evaluation()
    {
        if (conversationPanel.currentDialogueIndex == 1 && conversationPanel.currentPanelIndex == conversationPanel.dialogues[1].panels.Length - 1)
        {
            Array.Resize(ref evaluatedQuestion, conversationPanel.dialogues[1].panels.Length + 1);
            for (int i = 0; i <= conversationPanel.dialogues[1].panels.Length; i++)
            {
                if (i == 0)
                {
                    evaluatedQuestion[i] = overallScore;
                }
                if (i < conversationPanel.dialogues[1].panels.Length)
                {
                    evaluatedQuestion[i + 1] = conversationPanel.dialogues[1].panels[i];
                }

            }
            Array.Resize(ref conversationPanel.dialogues[2].panels, evaluatedQuestion.Length);

            for (int i = 0; i < evaluatedQuestion.Length; i++)
            {
                conversationPanel.dialogues[2].panels[i] = evaluatedQuestion[i];
            }
        }
    }

}
