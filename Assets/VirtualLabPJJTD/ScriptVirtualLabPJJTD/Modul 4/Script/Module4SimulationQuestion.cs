using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using System.ComponentModel;

public class Module4SimulationQuestion : MonoBehaviour
{
    private double DnoErgobikePria = 88;
    private double DnoErgobikeWanita = 80;
    private double DniErgobikePria1 = 109;
    private int DniErgobikePria2 = 116;
    private int DniErgobikePria3 = 129;
    private int DniErgobikeWanita1 = 91;
    private int DniErgobikeWnaita2 = 110;
    private int DniErgobikeWanita3 = 123;
    private bool isChecked = false;
    private bool isFinalPanel = false;
    private double DnoFootstepPria = 85;
    private int DnoFootstepWanita = 83;
    private double DniFootstepPria1 = 122;
    private double DniFootstepPria2 = 131;
    private double DniFootstepPria3 = 148;
    private double DniFootstepWanita1 = 113;
    private double DniFootstepWanita2 = 122;
    private double DniFootstepWanita3 = 138;
    private Module4UIManager module4Uimanager;
    private Module4UIManager2 module4Uimanager2;

    public TMP_InputField answerErgobike1;
    public TMP_InputField answerErgobike2;
    public TMP_InputField answerErgobike3;
    public TMP_InputField answerErgobike4;
    public TMP_InputField answerErgobike5;
    public TMP_InputField answerErgobike6;
    public TMP_InputField answerEAverageErgobike1;
    public TMP_InputField answerEAverageErgobike2;

    public TMP_InputField answerFootstep1;
    public TMP_InputField answerFootstep2;
    public TMP_InputField answerFootstep3;
    public TMP_InputField answerFootstep4;
    public TMP_InputField answerFootstep5;
    public TMP_InputField answerFootstep6;
    public TMP_InputField answerEAverageFootstep1;
    public TMP_InputField answerEAverageFootstep2;

    public TMP_InputField answerErgobikeMale1;
    public TMP_InputField answerErgobikeMale2;
    public TMP_InputField answerErgobikeMale3;
    public TMP_InputField answerEAverageErgobikeMale;

    public TMP_InputField answerErgobikeFemale1;
    public TMP_InputField answerErgobikeFemale2;
    public TMP_InputField answerErgobikeFemale3;
    public TMP_InputField answerEAverageErgobikeFemale;

    public TMP_InputField answerFootstepMale1;
    public TMP_InputField answerFootstepMale2;
    public TMP_InputField answerFootstepMale3;
    public TMP_InputField answerEAverageFootstepMale;

    public TMP_InputField answerFootstepFemale1;
    public TMP_InputField answerFootstepFemale2;
    public TMP_InputField answerFootstepFemale3;
    public TMP_InputField answerEAverageFootstepFemale;

    public TMP_InputField RAnswerErgoMale;
    public TMP_InputField RAnswerErgoFemale;
    public TMP_InputField RAnswerFootstepMale;
    public TMP_InputField RAnswerFootstepFemale;

    public TMP_InputField breakTimeErgoMale;
    public TMP_InputField breakTimeErgoFemale;
    public TMP_InputField breakTimeFootstepMale;
    public TMP_InputField breakTimeFootstepFemale;

    public ConversationPanel conversationPanel;
    private double Time;

    [HideInInspector] public bool ergoTableExist, footstepTableExist, ergoTableMaleExist, ergoTableFemaleExist, footstepTableMaleExist, foostepTableFemaleExist = false;
    [HideInInspector] public float correctCount;
    [SerializeField] private GameObject breakTimeQuestionMaleErgo;
    [SerializeField] private GameObject breakTimeQuestionFemaleErgo;
    [SerializeField] private GameObject breakTimeQuestionMaleFootstep;
    [SerializeField] private GameObject breakTimeQuestionFemaleFootstep;
    [HideInInspector] public bool isCheck1, isCheck2, isCheck3, isCheck4, isCheck5, isCheck6, isCheck7, isCheck8, isCheck9, isCheck10, isCheck11, isCheck12, isCheck13, isCheck14, isCheck15, isCheck16, isCheck17, isCheck18, isCheck19, isCheck20 = false;
    [HideInInspector] public bool isCheck21, isCheck22, isCheck23, isCheck24, isCheck25, isCheck26, isCheck27, isCheck28, isCheck29, isCheck30, isCheck31, isCheck32, isCheck33, isCheck34, isCheck35, isCheck36, isCheck37, isCheck38, isCheck39, isCheck40 = false;

    double xSquaredPria1 = 10 * 10;
    double xSquaredPria2 = 15 * 15;
    double xSquaredPria3 = 20 * 20;
    double xSquaredWanita1 = 10 * 10;
    double xSquaredWanita2 = 15 * 15;
    double xSquaredWanita3 = 20 * 20;
    double xSquaredFootstepPria1 = 20 * 20;
    double xSquaredFootstepPria2 = 30 * 30;
    double xSquaredFootstepPria3 = 40 * 40;
    double xSquaredFootstepWanita1 = 20 * 20;
    double xSquaredFootstepWanita2 = 30 * 30;
    double xSquaredFootstepWanita3 = 40 * 40;
    double xSumErgo = 10 + 15 + 20;
    double xSumFootstep = 20 + 30 + 40;
    double correctAnswerFootstepMale1, correctAnswerFootstepMale2, correctAnswerFootstepMale3;
    double correctAnswerEAverageFootstepMale;
    double correctBreakTimeErgoMale;
    double correctBreakTimeErgoFemale;
    double correctBreakTimeFootstepMale;
    double correctBreakTimeFootstepFemale;
    double correctRAnswerErgoMale, correctRAnswerErgoFemale, correctRAnswerFootstepMale, correctRAnswerFootstepFemale;
    // Start is called before the first frame update
    void Start()
    {
        module4Uimanager = GetComponent<Module4UIManager>();
        module4Uimanager2 = GetComponent<Module4UIManager2>();
    }

    // Update is called once per frame
    void Update()
    {
        BreakTimeOption();
        // Debug.Log("Correct Count: " + correctCount);
        /*  if (conversationPanel.currentDialogueIndex == 1 && conversationPanel.currentPanelIndex == conversationPanel.dialogues[1].panels.Length - 1)
          {
              if (conversationPanel.dialogues[1].panels[conversationPanel.currentPanelIndex].activeSelf == false)
              {
                  isFinalPanel = true;
              }
              else
              {
                  isFinalPanel = false;
              }
              //Debug.Log("Jawaban 1: " + correctAnswerFootstepMale1);
              //Debug.Log("Jawaban 2: " + correctAnswerFootstepMale2);
              //Debug.Log("Jawaban 3: " + correctAnswerFootstepMale3);
              // Debug.Log("Jawaban 4: " + correctRAnswer);
          }*/
        if (conversationPanel.currentDialogueIndex == 2)
        {
            CheckAnswers();
        }
    }

    public void CheckAnswers()
    {
        double.TryParse(module4Uimanager.time.text, out Time);
        double correctAnswerErgobikeMale1 = 1.33 + (DniErgobikePria1 - DnoErgobikePria) / 10;
        double correctAnswerErgobikeMale2 = 1.33 + (DniErgobikePria2 - DnoErgobikePria) / 10;
        double correctAnswerErgobikeMale3 = 1.33 + (DniErgobikePria3 - DnoErgobikePria) / 10;
        double correctAnswerEAverageErgobikeMale = (correctAnswerErgobikeMale1 + correctAnswerErgobikeMale2 + correctAnswerErgobikeMale3) / 3;
        double correctAnswerErgobikeFemale1 = 1.33 + (DniErgobikeWanita1 - DnoErgobikeWanita) / 10;
        double correctAnswerErgobikeFemale2 = 1.33 + (DniErgobikeWnaita2 - DnoErgobikeWanita) / 10;
        double correctAnswerErgobikeFemale3 = 1.33 + (DniErgobikeWanita3 - DnoErgobikeWanita) / 10;
        double correctAnswerEAverageErgobikeFemale = (correctAnswerErgobikeFemale1 + correctAnswerErgobikeFemale2 + correctAnswerErgobikeFemale3) / 3;
        correctAnswerFootstepMale1 = 1.33 + (DniFootstepPria1 - DnoFootstepPria) / 10;
        correctAnswerFootstepMale2 = 1.33 + (DniFootstepPria2 - DnoFootstepPria) / 10;
        correctAnswerFootstepMale3 = 1.33 + (DniFootstepPria3 - DnoFootstepPria) / 10;
        correctAnswerEAverageFootstepMale = (correctAnswerFootstepMale1 + correctAnswerFootstepMale2 + correctAnswerFootstepMale3) / 3;
        correctAnswerEAverageFootstepMale = Math.Round(correctAnswerEAverageFootstepMale, 2);
        double correctAnswerFootstepFemale1 = 1.33 + (DniFootstepWanita1 - DnoFootstepWanita) / 10;
        double correctAnswerFootstepFemale2 = 1.33 + (DniFootstepWanita2 - DnoFootstepWanita) / 10;
        double correctAnswerFootstepFemale3 = 1.33 + (DniFootstepWanita3 - DnoFootstepWanita) / 10;
        double correctAnswerEAverageFootstepFemale = (correctAnswerFootstepFemale1 + correctAnswerFootstepFemale2 + correctAnswerFootstepFemale3) / 3;
        correctAnswerEAverageFootstepFemale = Math.Round(correctAnswerEAverageFootstepFemale, 2);
        correctBreakTimeErgoMale = ((correctAnswerEAverageErgobikeMale - 5.33) / (correctAnswerEAverageErgobikeMale - 1.33)) * Time;
        correctBreakTimeErgoFemale = (correctAnswerEAverageErgobikeFemale - 4) / (correctAnswerEAverageErgobikeFemale - 1.33) * Time;
        correctBreakTimeFootstepMale = (correctAnswerEAverageFootstepMale - 5.33) / (correctAnswerEAverageFootstepMale - 1.33) * Time;
        correctBreakTimeFootstepFemale = (correctAnswerEAverageFootstepFemale - 4) / (correctAnswerEAverageFootstepFemale - 1.33) * Time;
        correctBreakTimeErgoMale = Math.Round(correctBreakTimeErgoMale, 2);
        correctBreakTimeErgoFemale = Math.Round(correctBreakTimeErgoFemale, 2);
        correctBreakTimeFootstepMale = Math.Round(correctBreakTimeFootstepMale, 2);
        correctBreakTimeFootstepFemale = Math.Round(correctBreakTimeFootstepFemale, 2);
        Debug.Log("Break Time Ergo Pria: " + correctBreakTimeErgoMale);

        if (conversationPanel.currentDialogueIndex == 1)
        {
            double ySumMale = correctAnswerErgobikeMale1 + correctAnswerErgobikeMale2 + correctAnswerErgobikeMale3;
            double ySumFootstepMale = correctAnswerFootstepMale1 + correctAnswerFootstepMale2 + correctAnswerFootstepMale3;
            double ySumFemale = correctAnswerErgobikeFemale1 + correctAnswerErgobikeFemale2 + correctAnswerErgobikeFemale3;
            double ySumFootstepFemale = correctAnswerFootstepFemale1 + correctAnswerFootstepFemale2 + correctAnswerFootstepFemale3;
            double ySquaredPria1 = Math.Round(correctAnswerErgobikeMale1 * correctAnswerErgobikeMale1, 2);
            double ySquaredPria2 = Math.Round(correctAnswerErgobikeMale2 * correctAnswerErgobikeMale2, 2);
            double ySquaredPria3 = Math.Round(correctAnswerErgobikeMale3 * correctAnswerErgobikeMale3, 2);
            double ySquaredFootstepPria1 = Math.Round(correctAnswerFootstepMale1 * correctAnswerFootstepMale1, 2);
            double ySquaredFootstepPria2 = Math.Round(correctAnswerFootstepMale2 * correctAnswerFootstepMale2, 2);
            double ySquaredFootstepPria3 = Math.Round(correctAnswerFootstepMale3 * correctAnswerFootstepMale3, 2);
            double ySquaredFootstepWanita1 = Math.Round(correctAnswerFootstepFemale1 * correctAnswerFootstepFemale1, 2);
            double ySquaredFootstepWanita2 = Math.Round(correctAnswerFootstepFemale2 * correctAnswerFootstepFemale2, 2);
            double ySquaredFootstepWanita3 = Math.Round(correctAnswerFootstepFemale3 * correctAnswerFootstepFemale3, 2);
            double xyErgoPria1 = correctAnswerErgobikeMale1 * 10;
            double xyErgoPria2 = correctAnswerErgobikeMale2 * 15;
            double xyErgoPria3 = correctAnswerErgobikeMale3 * 20;
            double xyErgoWanita1 = correctAnswerErgobikeFemale1 * 10;
            double xyErgoWanita2 = correctAnswerErgobikeFemale2 * 15;
            double xyErgoWanita3 = correctAnswerErgobikeFemale3 * 20;
            double xyFootstepPria1 = correctAnswerFootstepMale1 * 20;
            double xyFootstepPria2 = correctAnswerFootstepMale2 * 30;
            double xyFootstepPria3 = correctAnswerFootstepMale3 * 40;
            double xyFootstepWanita1 = correctAnswerFootstepFemale1 * 20;
            double xyFootstepWanita2 = correctAnswerFootstepFemale2 * 30;
            double xyFootstepWanita3 = correctAnswerFootstepFemale3 * 40;
            double denominatorRErgoMale = (3 * (xSquaredPria1 + xSquaredPria2 + xSquaredPria3) - xSumErgo * xSumErgo) * (3 * (ySquaredPria1 + ySquaredPria2 + ySquaredPria3) - ySumMale * ySumMale);
            double ySquaredWanita1 = Math.Round(2.43 * 2.43, 2);
            double ySquaredWanita2 = Math.Round(4.33 * 4.33, 2);
            double ySquaredWanita3 = Math.Round(5.63 * 5.63, 2);
            double denominatorRErgoFemale = (3 * (xSquaredWanita1 + xSquaredWanita2 + xSquaredWanita3) - xSumErgo * xSumErgo) * (3 * (ySquaredWanita1 + ySquaredWanita2 + ySquaredWanita3) - ySumFemale * ySumFemale);
            correctRAnswerErgoMale = (3 * (xyErgoPria1 + xyErgoPria2 + xyErgoPria3) - xSumErgo * ySumMale) / Math.Sqrt(denominatorRErgoMale);
            correctRAnswerErgoMale = Math.Round(correctRAnswerErgoMale, 3);
            correctRAnswerErgoFemale = (3 * (xyErgoWanita1 + xyErgoWanita2 + xyErgoWanita3) - xSumErgo * ySumFemale) / Math.Sqrt(denominatorRErgoFemale);
            correctRAnswerErgoFemale = Math.Round(correctRAnswerErgoFemale, 3);
            double denominatorRFootstepMale = (3 * (xSquaredFootstepPria1 + xSquaredFootstepPria2 + xSquaredFootstepPria3) - xSumFootstep * xSumFootstep) * (3 * (ySquaredFootstepPria1 + ySquaredFootstepPria2 + ySquaredFootstepPria3) - ySumFootstepMale * ySumFootstepMale);
            correctRAnswerFootstepMale = (3 * (xyFootstepPria1 + xyFootstepPria2 + xyFootstepPria3) - xSumFootstep * ySumFootstepMale) / Math.Sqrt(denominatorRFootstepMale);
            correctRAnswerFootstepMale = Math.Round(correctRAnswerFootstepMale, 3);
            double denominatorRFootstepFemale = (3 * (xSquaredFootstepWanita1 + xSquaredFootstepWanita2 + xSquaredFootstepWanita3) - xSumFootstep * xSumFootstep) * (3 * (ySquaredFootstepWanita1 + ySquaredFootstepWanita2 + ySquaredFootstepWanita3) - ySumFootstepFemale * ySumFootstepFemale);
            correctRAnswerFootstepFemale = (3 * (xyFootstepWanita1 + xyFootstepWanita2 + xyFootstepWanita3) - xSumFootstep * ySumFootstepFemale) / Math.Sqrt(denominatorRFootstepFemale);
            correctRAnswerFootstepFemale = Math.Round(correctRAnswerFootstepFemale, 3);
        }

        if (module4Uimanager2.simTableErgo.activeSelf == true)
        {

            if (answerErgobike1.text == correctAnswerErgobikeMale1.ToString() && isCheck1 == false)
            {
                correctCount++;
                isCheck1 = true;
            }
            if (answerErgobike2.text == correctAnswerErgobikeMale2.ToString() && isCheck2 == false)
            {
                correctCount++;
                isCheck2 = true;
            }
            if (answerErgobike3.text == correctAnswerErgobikeMale3.ToString() && isCheck3 == false)
            {
                correctCount++;
                isCheck3 = true;
            }
            if (answerEAverageErgobike1.text == correctAnswerEAverageErgobikeMale.ToString() && isCheck4 == false)
            {
                correctCount++;
                isCheck4 = true;
            }
            if (answerErgobike4.text == correctAnswerErgobikeFemale1.ToString() && isCheck5 == false)
            {
                correctCount++;
                isCheck5 = true;
            }
            if (answerErgobike5.text == correctAnswerErgobikeFemale2.ToString() && isCheck6 == false)
            {
                correctCount++;
                isCheck6 = true;
            }
            if (answerErgobike6.text == correctAnswerErgobikeFemale3.ToString() && isCheck7 == false)
            {
                correctCount++;
                isCheck7 = true;
            }
            if (answerEAverageErgobike2.text == correctAnswerEAverageErgobikeFemale.ToString() && isCheck8 == false)
            {
                correctCount++;
                isCheck8 = true;
            }
            ergoTableExist = true;
            isChecked = true;
        }

        Debug.Log(correctCount);

        if (module4Uimanager2.simTableFootstep.activeSelf == true)
        {
            if (answerFootstep1.text == correctAnswerFootstepMale1.ToString() && isCheck9 == false)
            {
                correctCount++;
                isCheck9 = true;
            }
            if (answerFootstep2.text == correctAnswerFootstepMale2.ToString() && isCheck10 == false)
            {
                correctCount++;
                isCheck10 = true;
            }
            if (answerFootstep3.text == correctAnswerFootstepMale3.ToString() && isCheck11 == false)
            {
                correctCount++;
                isCheck11 = true;
            }
            if (answerFootstep4.text == correctAnswerFootstepFemale1.ToString() && isCheck12 == false)
            {
                correctCount++;
                isCheck12 = true;
            }
            if (answerFootstep5.text == correctAnswerFootstepFemale2.ToString() && isCheck13 == false)
            {
                correctCount++;
                isCheck13 = true;
            }
            if (answerFootstep6.text == correctAnswerFootstepFemale3.ToString() && isCheck14 == false)
            {
                correctCount++;
                isCheck14 = true;
            }
            if (answerEAverageFootstep1.text == correctAnswerEAverageFootstepMale.ToString() && isCheck15 == false)
            {
                correctCount++;
                isCheck15 = true;
            }
            if (answerEAverageFootstep2.text == correctAnswerEAverageFootstepFemale.ToString() && isCheck16 == false)
            {
                correctCount++;
                isCheck16 = true;
            }
            footstepTableExist = true;
        }

        if (module4Uimanager2.simTableErgoMale.activeSelf == true)
        {
            if (answerErgobikeMale1.text == correctAnswerErgobikeMale1.ToString() && isCheck17 == false)
            {
                correctCount++;
                isCheck17 = true;
            }
            if (answerErgobikeMale2.text == correctAnswerErgobikeMale2.ToString() && isCheck18 == false)
            {
                correctCount++;
                isCheck18 = true;
            }
            if (answerErgobikeMale3.text == correctAnswerErgobikeMale3.ToString() && isCheck19 == false)
            {
                correctCount++;
                isCheck19 = true;
            }
            if (answerEAverageErgobikeMale.text == correctAnswerEAverageErgobikeMale.ToString() && isCheck20 == false)
            {
                correctCount++;
                isCheck20 = true;
            }
            ergoTableMaleExist = true;
        }
        if (module4Uimanager2.simTableErgoFemale.activeSelf == true)
        {
            if (answerErgobikeFemale1.text == correctAnswerErgobikeFemale1.ToString() && isCheck21 == false)
            {
                correctCount++;
                isCheck21 = true;
            }
            if (answerErgobikeFemale2.text == correctAnswerErgobikeFemale2.ToString() && isCheck22 == false)
            {
                correctCount++;
                isCheck22 = true;
                Debug.Log(correctAnswerErgobikeFemale2.ToString());
            }
            if (answerErgobikeFemale3.text == correctAnswerErgobikeFemale3.ToString() && isCheck23 == false)
            {
                correctCount++;
                isCheck23 = true;
                Debug.Log(correctAnswerErgobikeFemale3.ToString());
            }
            if (answerEAverageErgobikeFemale.text == correctAnswerEAverageErgobikeFemale.ToString() && isCheck24 == false)
            {
                correctCount++;
                isCheck24 = true;
                Debug.Log(correctAnswerEAverageErgobikeFemale.ToString());
            }
            else
            {
                Debug.Log(isCheck2);
                Debug.Log(isCheck3);
                Debug.Log(isCheck4);
            }
            ergoTableFemaleExist = true;
        }
        if (module4Uimanager2.simTableFootstepMale.activeSelf == true)
        {
            if (answerFootstepMale1.text == correctAnswerFootstepMale1.ToString() && isCheck25 == false)
            {
                correctCount++;
                isCheck25 = true;
            }
            if (answerFootstepMale2.text == correctAnswerFootstepMale2.ToString() && isCheck26 == false)
            {
                correctCount++;
                isCheck26 = true;
            }
            if (answerFootstepMale3.text == correctAnswerFootstepMale3.ToString() && isCheck27 == false)
            {
                correctCount++;
                isCheck27 = true;
            }
            if (answerEAverageFootstepMale.text == correctAnswerEAverageFootstepMale.ToString() && isCheck28 == false)
            {
                correctCount++;
                isCheck28 = true;
            }
            footstepTableMaleExist = true;
        }
        if (module4Uimanager2.simTableFootstepFemale.activeSelf == true)
        {
            if (answerFootstepFemale1.text == correctAnswerFootstepFemale1.ToString() && isCheck29 == false)
            {
                correctCount++;
                isCheck29 = true;
            }
            if (answerFootstepFemale2.text == correctAnswerFootstepFemale2.ToString() && isCheck30 == false)
            {
                correctCount++;
                isCheck30 = true;
            }
            if (answerFootstepFemale3.text == correctAnswerFootstepFemale3.ToString() && isCheck31 == false)
            {
                correctCount++;
                isCheck31 = true;
            }
            if (answerEAverageFootstepFemale.text == correctAnswerEAverageFootstepFemale.ToString() && isCheck32 == false)
            {
                correctCount++;
                isCheck32 = true;
            }
            foostepTableFemaleExist = true;
        }

        if (module4Uimanager2.RTableErgoMale.activeSelf == true)
        {
            if (RAnswerErgoMale.text == correctRAnswerErgoMale.ToString() && isCheck33 == false)
            {
                correctCount++;
                isCheck33 = true;
            }
        }

        if (module4Uimanager2.RTableErgoFemale.activeSelf == true)
        {
            if (RAnswerErgoFemale.text == correctRAnswerErgoFemale.ToString() && isCheck34 == false)
            {
                correctCount++;
                isCheck34 = true;
            }
        }

        if (module4Uimanager2.BreakTimeTableErgoMale.activeSelf == true)
        {
            if (breakTimeErgoMale.text == correctBreakTimeErgoMale.ToString() && isCheck35 == false)
            {
                correctCount++;
                isCheck35 = true;
            }
        }

        if (module4Uimanager2.RTableFootstepMale.activeSelf == true)
        {
            if (RAnswerFootstepMale.text == correctRAnswerFootstepMale.ToString() && isCheck36 == false)
            {
                correctCount++;
                isCheck36 = true;
            }
        }

        if (module4Uimanager2.RTableFootstepFemale.activeSelf == true)
        {
            if (RAnswerFootstepFemale.text == correctRAnswerFootstepFemale.ToString() && isCheck37 == false)
            {
                correctCount++;
                isCheck37 = true;
            }
        }

        if (module4Uimanager2.BreakTimeTableErgoFemale.activeSelf == true)
        {
            if (breakTimeErgoFemale.text == correctBreakTimeErgoFemale.ToString() && isCheck38 == false)
            {
                correctCount++;
                isCheck38 = true;
            }
        }

        if (module4Uimanager2.BreakTimeTableFootstepMale.activeSelf == true)
        {
            if (breakTimeFootstepMale.text == correctBreakTimeFootstepMale.ToString() && isCheck39 == false)
            {
                correctCount++;
                isCheck39 = true;
            }
        }

        if (module4Uimanager2.BreakTimeTableFootstepFemale.activeSelf == true)
        {
            if (breakTimeFootstepFemale.text == correctBreakTimeFootstepFemale.ToString() && isCheck40 == false)
            {
                correctCount++;
                isCheck40 = true;
            }
        }

    }

    public void ShowFinalQuestion()
    {
        if (conversationPanel.currentDialogueIndex == 1)
        {
        /*    if (conversationPanel.dialogues[1].panels[conversationPanel.currentPanelIndex].activeSelf == false)
            {
                isFinalPanel = true;
            }
            else
            {
                isFinalPanel = false;
            }*/

            if (conversationPanel.currentPanelIndex == conversationPanel.dialogues[1].panels.Length - 2 && isFinalPanel == true)
            {
                conversationPanel.ShowNextPanel();
            }

        }
    }
    public double[] OptionErgoMale = new double[3];
    public double[] OptionErgoFemale = new double[3];
    public double[] OptionFootstepMale = new double[3];
    public double[] OptionFootstepFemale = new double[3];

    private void BreakTimeOption()
    {

        if (conversationPanel.currentDialogueIndex == 1)
        {
            OptionErgoMale[0] = Math.Round(correctBreakTimeErgoMale + (correctBreakTimeErgoMale * 0.07), 2);
            OptionErgoMale[1] = Math.Round(correctBreakTimeErgoMale + (correctBreakTimeErgoMale * 0.03), 2);
            OptionErgoMale[2] = Math.Round(correctBreakTimeErgoMale + (correctBreakTimeErgoMale * 0.05), 2);
            OptionErgoFemale[0] = Math.Round(correctBreakTimeErgoFemale + (correctBreakTimeErgoFemale * 0.07), 2);
            OptionErgoFemale[1] = Math.Round(correctBreakTimeErgoFemale + (correctBreakTimeErgoFemale * 0.03), 2);
            OptionErgoFemale[2] = Math.Round(correctBreakTimeErgoFemale + (correctBreakTimeErgoFemale * 0.05), 2);
            OptionFootstepMale[0] = Math.Round(correctBreakTimeFootstepMale + (correctBreakTimeFootstepMale * 0.07), 2);
            OptionFootstepMale[1] = Math.Round(correctBreakTimeFootstepMale + (correctBreakTimeFootstepMale * 0.03), 2);
            OptionFootstepMale[2] = Math.Round(correctBreakTimeFootstepMale + (correctBreakTimeFootstepMale * 0.05), 2);
            OptionFootstepFemale[0] = Math.Round(correctBreakTimeFootstepFemale + (correctBreakTimeFootstepFemale * 0.07), 2);
            OptionFootstepFemale[1] = Math.Round(correctBreakTimeFootstepFemale + (correctBreakTimeFootstepFemale * 0.03), 2);
            OptionFootstepFemale[2] = Math.Round(correctBreakTimeFootstepFemale + (correctBreakTimeFootstepFemale * 0.05), 2);
            for (int i = 0; i < OptionErgoMale.Length; i++)
            {
                breakTimeQuestionMaleErgo.transform.GetChild(2 + i).GetChild(0).GetComponent<TMP_Text>().text = OptionErgoMale[i].ToString();
            }
            for (int i = 0; i < OptionErgoFemale.Length; i++)
            {
                breakTimeQuestionFemaleErgo.transform.GetChild(2 + i).GetChild(0).GetComponent<TMP_Text>().text = OptionErgoFemale[i].ToString();
            }
            for (int i = 0; i < OptionFootstepMale.Length; i++)
            {
                breakTimeQuestionMaleFootstep.transform.GetChild(2 + i).GetChild(0).GetComponent<TMP_Text>().text = OptionFootstepMale[i].ToString();
            }
            for (int i = 0; i < OptionFootstepFemale.Length; i++)
            {
                breakTimeQuestionFemaleFootstep.transform.GetChild(2 + i).GetChild(0).GetComponent<TMP_Text>().text = OptionFootstepFemale[i].ToString();
            }
            breakTimeQuestionMaleErgo.transform.GetChild(5).GetChild(0).GetComponent<TMP_Text>().text = correctBreakTimeErgoMale.ToString();
            breakTimeQuestionFemaleErgo.transform.GetChild(5).GetChild(0).GetComponent<TMP_Text>().text = correctBreakTimeErgoFemale.ToString();
            breakTimeQuestionMaleFootstep.transform.GetChild(5).GetChild(0).GetComponent<TMP_Text>().text = correctBreakTimeFootstepMale.ToString();
            breakTimeQuestionFemaleFootstep.transform.GetChild(5).GetChild(0).GetComponent<TMP_Text>().text = correctBreakTimeFootstepFemale.ToString();
        }
    }

    public void ChooseAnswer(string value)
    {
        if(module4Uimanager2.RTableErgoMale.activeSelf == true)
        {
            RAnswerErgoMale.text = value;
        }
        if(module4Uimanager2.RTableErgoFemale.activeSelf == true)
        {
            RAnswerErgoFemale.text = value;
        }
        if(module4Uimanager2.RTableFootstepFemale.activeSelf == true)
        {
            RAnswerFootstepFemale.text = value;
        }
        if(module4Uimanager2.RTableFootstepMale.activeSelf == true)
        {
            RAnswerFootstepMale.text = value;
        }
    }

    public void GetValue()
    {
        GameObject objectValue = module4Uimanager.GetUI();
        if(module4Uimanager2.BreakTimeTableErgoMale.activeSelf == true)
        {
            Debug.Log(objectValue.name);
            breakTimeErgoMale.text = objectValue.GetComponent<TMP_Text>().text; 
        }
        if(module4Uimanager2.BreakTimeTableErgoFemale.activeSelf == true)
        {
            Debug.Log(objectValue.name);
            breakTimeErgoFemale.text = objectValue.GetComponent<TMP_Text>().text;
        }
        if(module4Uimanager2.BreakTimeTableFootstepMale.activeSelf == true)
        {
            Debug.Log(objectValue.name);
            breakTimeFootstepMale.text = objectValue.GetComponent<TMP_Text>().text;
        }
        if(module4Uimanager2.BreakTimeTableFootstepFemale.activeSelf == true)
        {
            breakTimeFootstepFemale.text = objectValue.GetComponent<TMP_Text>().text;
        }
    }

}
