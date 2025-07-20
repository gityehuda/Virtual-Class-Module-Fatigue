using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using TMPro;
using UnityEngine;

public class Module4SimManager : MonoBehaviour
{
    public ConversationPanel conversationPanel;
    private Module4Manager module4Manager;
    private Module4UIManager module4UIManager;
    private float speed;
    public float countdown = 60f;
    public float heartBeat = 80f;
    public int maxStepCount;
    public int stepCount = 0;
    private int sessionCount;
    public bool isStarted = false;
    private Transform legPosition;
    private float maxSpeed;
    public float firstHeartBeat;
    public float secondHeartBeat;
    public float thirdHeartBeat;
    public int simulationCount;
    public bool isMaleErgoDone = false;
    public bool isFemaleErgoDone = false;
    public bool isMaleFootstepDone = false;
    public bool isFemaleFootstepDone = false;

    // Start is called before the first frame update
    void Awake()
    {
        module4Manager = GetComponent<Module4Manager>();
        module4UIManager = GetComponent<Module4UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (module4Manager.selectedStudentEquipment != null)
        {
            SpeedRepetition();
        }

    }

    void SpeedRepetition()
    {
        if (conversationPanel == null)
        {
            print("no script attached");
        }
        if (module4Manager.equipment == module4Manager.footstep && conversationPanel.currentPanelIndex >= 26)
        {
            legPosition = GameObject.Find("mixamorig:Hips").GetComponent<Transform>().transform;
            if (conversationPanel.currentPanelIndex == 26 && isStarted == false && stepCount != maxStepCount)
            {
                sessionCount = 0;
                maxStepCount = 20;
                isStarted = true;
                print("Coroutine called");
                sessionCount++;
                module4UIManager.sessionText.text = "Sesi " + sessionCount;
                StartCoroutine(SpeedRepetitionIncrease());
                StartCoroutine(HeartBeatIncrease());

            }
            else if (conversationPanel.currentPanelIndex == 28 && isStarted == false && stepCount != maxStepCount)
            {
                isStarted = true;
                maxStepCount = 30;
                sessionCount++;
                module4UIManager.sessionText.text = "Sesi " + sessionCount;
                print("Coroutine called2");
                StartCoroutine(SpeedRepetitionIncrease());
                StartCoroutine(HeartBeatIncrease());
                secondHeartBeat = heartBeat;
            }
            else if (conversationPanel.currentPanelIndex == 30 && isStarted == false && stepCount != maxStepCount)
            {
                isStarted = true;
                sessionCount++;
                maxStepCount = 40;
                module4UIManager.sessionText.text = "Sesi" + sessionCount;
                print("Coroutine called");
                StartCoroutine(SpeedRepetitionIncrease());
                StartCoroutine(HeartBeatIncrease());
                thirdHeartBeat = heartBeat;
                simulationCount++;
                if (module4Manager.student == module4Manager.maleStudent)
                {
                    isMaleFootstepDone = true;
                }
                else if (module4Manager.student == module4Manager.femaleStudent)
                {
                    isFemaleFootstepDone = true;
                }
            }

        }

        if (conversationPanel.currentPanelIndex != 28 && conversationPanel.currentPanelIndex != 26 && conversationPanel.currentPanelIndex != 30)
        {
            //Debug.Log(stepCount);
            speed = 0;
            stepCount = 0;
            StopAllCoroutines();
            if (module4Manager.student == module4Manager.maleStudent && module4Manager.equipment == module4Manager.bike)
            {
                heartBeat = 88f;
            }
            else if (module4Manager.student == module4Manager.femaleStudent && module4Manager.equipment == module4Manager.bike)
            {
                heartBeat = 80f;
            }
            else if (module4Manager.student == module4Manager.maleStudent && module4Manager.equipment == module4Manager.footstep)
            {
                heartBeat = 84f;
                Debug.Log("Heartbeat: " + heartBeat);
            }
            else if (module4Manager.student == module4Manager.femaleStudent && module4Manager.equipment == module4Manager.footstep)
            {
                heartBeat = 82f;
                Debug.Log("Heartneat: " + heartBeat);
            }

            countdown = 60f;
        }


        if (module4Manager.equipment == module4Manager.bike)
        {

            if (conversationPanel.currentPanelIndex == 26 && isStarted == false && countdown == 60)
            {
                isStarted = true;
                sessionCount = 0;
                maxSpeed = 10;
                sessionCount++;
                module4UIManager.sessionText.text = "Sesi " + sessionCount;
                StartCoroutine(SpeedRepetitionIncrease());
                StartCoroutine(HeartBeatIncrease());

            }
            else if (conversationPanel.currentPanelIndex == 28 && isStarted == false && countdown == 60)
            {
                isStarted = true;
                sessionCount++;
                module4UIManager.sessionText.text = "Sesi " + sessionCount;
                print("Coroutine called");
                print("Speed: " + speed);
                maxSpeed = 15;
                StartCoroutine(SpeedRepetitionIncrease());
                StartCoroutine(HeartBeatIncrease());

            }
            else if (conversationPanel.currentPanelIndex == 30 && isStarted == false && countdown == 60)
            {
                isStarted = true;
                sessionCount++;
                module4UIManager.sessionText.text = "Sesi " + sessionCount;
                print("Coroutine called");
                maxSpeed = 20;
                StartCoroutine(SpeedRepetitionIncrease());
                StartCoroutine(HeartBeatIncrease());
                simulationCount++;
                Debug.Log("Simulation: " + simulationCount);
                if (module4Manager.student == module4Manager.maleStudent)
                {
                    isMaleErgoDone = true;
                }
                else if (module4Manager.student == module4Manager.femaleStudent)
                {
                    isFemaleErgoDone = true;
                }
            }

        }
    }

    IEnumerator SpeedRepetitionIncrease()
    {
        if (module4Manager.equipment == module4Manager.bike)
        {

            while (speed < maxSpeed)
            {
                speed++;
                int bikeSpeed = (int)speed;
                module4UIManager.speedRepetitionText.text = bikeSpeed.ToString();
                //Debug.Log(heartBeat);

                yield return new WaitForSeconds(1f);
            }

            Debug.Log("speed is 10");
            if (speed == maxSpeed)
            {
                Debug.Log("max speed");
                while (countdown > 0 && (conversationPanel.currentPanelIndex == 26 || conversationPanel.currentPanelIndex == 28 || conversationPanel.currentPanelIndex == 30))
                {
                    //Debug.Log("speed: " + speed);
                    countdown--;
                    Debug.Log("Countdown " + countdown);
                    yield return new WaitForSecondsRealtime(1f);
                }

            }
            yield break;

        }

        else if (module4Manager.equipment == module4Manager.footstep)
        {
            Debug.Log("simulation running");
            float halfRepPos = 0;
            float almostRepPos = 0;
            float fullRepPos = 0;
            if (module4Manager.student == module4Manager.femaleStudent)
            {
                halfRepPos = 81.79f;
                almostRepPos = 88.01f;
                fullRepPos = 82.34f;
            }

            if (module4Manager.student == module4Manager.maleStudent)
            {
                halfRepPos = 96.77f;
                almostRepPos = 101.59f;
                fullRepPos = 95.65f;
            }

            module4UIManager.speedRepetitionText.text = stepCount.ToString();
            bool isHalfRep = false;
            bool isAlmostRep = false;
            while (stepCount < maxStepCount)
            {
                if (isHalfRep == false && legPosition.localPosition.y <= halfRepPos)
                {
                    isHalfRep = true;
                    Debug.Log("isHalfRep: " + isHalfRep);
                    Debug.Log(legPosition.localPosition.y);
                }

                if (isHalfRep == true && legPosition.localPosition.y >= almostRepPos)
                {
                    isAlmostRep = true;
                }

                if (isHalfRep == true && isAlmostRep == true && legPosition.localPosition.y <= fullRepPos)
                {
                    stepCount++;
                    Debug.Log("Step Count: " + stepCount);
                    module4UIManager.speedRepetitionText.text = stepCount.ToString();
                    isHalfRep = false;
                    isAlmostRep = false;
                    Debug.Log("isHalfRep: " + isHalfRep);
                    Debug.Log(legPosition.localPosition.y);
                    yield return new WaitForSecondsRealtime(0.1f);
                }
                yield return null;

            }

        }

    }

    IEnumerator HeartBeatIncrease()
        {

            if (module4Manager.equipment == module4Manager.bike)
            {
                int stablePoint = 0;
                if (sessionCount == 1)
                {
                    if (module4Manager.student == module4Manager.maleStudent)
                    {
                        stablePoint = 109;
                    }
                    else if (module4Manager.student == module4Manager.femaleStudent)
                    {
                        stablePoint = 91;
                    }

                }
                else if (sessionCount == 2)
                {
                    if (module4Manager.student == module4Manager.maleStudent)
                    {
                        stablePoint = 116;
                    }
                    else if (module4Manager.student == module4Manager.femaleStudent)
                    {
                        stablePoint = 110;
                    }
                }
                else if (sessionCount == 3)
                {
                    if (module4Manager.student == module4Manager.maleStudent)
                    {
                        stablePoint = 129;
                    }
                    else if (module4Manager.student == module4Manager.femaleStudent)
                    {
                        stablePoint = 123;
                    }
                }

                Debug.Log(stablePoint);
                while (countdown > 0)
                {

                    while (heartBeat < stablePoint)
                    {
                        if (countdown == 0)
                        {
                            break;
                        }

                        heartBeat++;
                        module4UIManager.heartBeatText.text = heartBeat.ToString();
                        //Debug.Log("Heartbeat: " + heartBeat);   
                        yield return new WaitForSecondsRealtime(0.4f);

                    }

                    while (heartBeat >= stablePoint)
                    {
                        if (countdown == 0)
                        {
                            break;
                        }
                        int targetNumber = Random.Range(stablePoint - 1, stablePoint + 2);
                        Debug.Log("Target Number: " + targetNumber);
                        if (heartBeat < targetNumber)
                        {
                            heartBeat++;
                            yield return new WaitForSeconds(0.5f);
                        }
                        else if (heartBeat == targetNumber)
                        {
                            yield return new WaitForSeconds(1f);
                        }
                        else
                        {
                            heartBeat--;
                            yield return new WaitForSeconds(0.5f);
                        }
                        module4UIManager.heartBeatText.text = heartBeat.ToString();
                        yield return new WaitForSeconds(1f);
                    }


                }
                //yield break;
            }
            else if (module4Manager.equipment == module4Manager.footstep)
            {
                while (stepCount < maxStepCount)
                {
                    int increasedFactor = Random.Range(1, 4);
                    heartBeat++;
                    module4UIManager.heartBeatText.text = heartBeat.ToString();
                    //Debug.Log(heartBeat);
                    if (module4Manager.student == module4Manager.femaleStudent)
                    {
                        if (sessionCount == 1)
                        {
                            yield return new WaitForSecondsRealtime(1.6f);
                        }
                        else if (sessionCount == 2)
                        {
                            yield return new WaitForSecondsRealtime(1.85f);
                        }
                        else if (sessionCount == 3)
                        {
                            yield return new WaitForSecondsRealtime(1.77f);
                        }
                    }
                    else if (module4Manager.student == module4Manager.maleStudent)
                    {
                        if (sessionCount == 1)
                        {
                            yield return new WaitForSecondsRealtime(1.3f);
                        }
                        else if (sessionCount == 2)
                        {
                            yield return new WaitForSecondsRealtime(1.6f);
                        }
                        else if (sessionCount == 3)
                        {
                            yield return new WaitForSecondsRealtime(1.55f);
                        }
                    }
                }
            }

        }

}


    
