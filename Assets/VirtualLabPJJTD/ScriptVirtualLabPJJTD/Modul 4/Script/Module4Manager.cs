using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Module4Manager : MonoBehaviour
{
    public ConversationPanel conversationPanel;
    private Module4SimManager simManager;
    private Module4UIManager manager;
    public GameObject femaleStudent;
    public GameObject maleStudent;
    public GameObject student;
    public GameObject equipment;
    public TMP_Dropdown equipmentChoice;
    public TMP_Dropdown genderChoice;
    [SerializeField] private GameObject lecturer;
    public GameObject bike;
    public GameObject footstep;
    [SerializeField] private GameObject spotLight;
    public TMP_InputField timeText;
    [SerializeField] private GameObject speedRepetitionPanel;
    [SerializeField] private GameObject heartBeatPanel;
    [SerializeField] private GameObject sessionPanel;
    public TMP_Text gender;
    public TMP_Text equipmentText;
    public bool isGenderSelected = false;
    public bool isEquipmentSelected = false; 
    public bool isObjectInstantiated = false;
    public bool isObjectIdle = true;
    private bool isLecturerSpawned = false;
    [SerializeField] private GameObject femaleErgobike;
    [SerializeField] private GameObject femaleFootstep;
    [SerializeField] private GameObject maleErgobike;
    [SerializeField] private GameObject maleFootstep;
    [SerializeField] private GameObject femaleFootstepIdle;
    [SerializeField] private GameObject maleFootstepIdle;
    [SerializeField] private GameObject femaleErgobikeIdle;
    [SerializeField] private GameObject maleErgobikeIdle;
    public GameObject selectedStudentEquipment;
    private GameObject selectedStudent;
    private GameObject selectedEquipment;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform spawnPointLecturer;
    [SerializeField] private GameObject submitButtonEquipment;

    void Start()
    {
        manager = GetComponent<Module4UIManager>();
        simManager = GetComponent<Module4SimManager>(); 
    }

    void Update()
    {
        ShowObject();
        if(isLecturerSpawned == false)
        {
            lecturer = Instantiate(lecturer, spawnPointLecturer);
            isLecturerSpawned = true;  
        }
     
        
    }

    void ShowObject()
    {
        if(conversationPanel.currentPanelIndex >= 16 || conversationPanel.currentDialogueIndex != 0) 
        {
            lecturer.SetActive(false);
 
        }
        else
        {
            lecturer.SetActive(true);
        }

        if (conversationPanel.currentPanelIndex >= 17 && conversationPanel.currentPanelIndex <= 23)
        {
            Destroy(selectedEquipment);
            Destroy(selectedStudent);


            if (isObjectInstantiated == false)
            {
                spawnPoint.rotation = Quaternion.Euler(0f, 90f, 0f);
                if (student == femaleStudent && equipment == bike)
                {
                    selectedStudentEquipment = Instantiate(femaleErgobikeIdle, spawnPoint.position, spawnPoint.rotation);
                }
                else if (student == femaleStudent && equipment == footstep)
                {
                    selectedStudentEquipment = Instantiate(femaleFootstepIdle, spawnPoint.position, spawnPoint.rotation);
                }
                else if (student == maleStudent && equipment == footstep)
                {
                    selectedStudentEquipment = Instantiate(maleFootstepIdle, spawnPoint.position, spawnPoint.rotation);
                }
                else if (student == maleStudent && equipment == bike)
                {
                    selectedStudentEquipment = Instantiate(maleErgobikeIdle, spawnPoint.position, spawnPoint.rotation);
                }
                isObjectInstantiated = true;
                Debug.Log(equipment);
            }

            if (conversationPanel.currentPanelIndex == 19)
            {

                spotLight.SetActive(true);
                if (equipment == bike)
                {
                    selectedStudentEquipment.transform.Find("Ergobike").GetComponent<Outline>().enabled = true;
                }
                else if (equipment == footstep)
                {
                    selectedStudentEquipment.transform.Find("Stepper").GetComponent<Outline>().enabled = true;
                }
            }
            else
            {
                spotLight.SetActive(false);

            }

        }
        RotateBike(selectedStudentEquipment);
        //Debug.Log("test");

        if (conversationPanel.currentPanelIndex == 26)
        {
            if (equipment == bike && student == maleStudent && isObjectIdle == true)
            {
                Destroy(selectedStudentEquipment);
                selectedStudentEquipment = Instantiate(maleErgobike, spawnPoint.position, spawnPoint.rotation);
                isObjectIdle = false;
            }
            else if (equipment == bike && student == femaleStudent && isObjectIdle == true)
            {
                Destroy(selectedStudentEquipment);
                selectedStudentEquipment = Instantiate(femaleErgobike, spawnPoint.position, spawnPoint.rotation);
                isObjectIdle = false;
            }

            if (equipment == footstep && student == maleStudent && isObjectIdle == true)
            {
                Destroy(selectedStudentEquipment);
                selectedStudentEquipment = Instantiate(maleFootstep, spawnPoint.position, spawnPoint.rotation);
                isObjectIdle = false;
            }
            else if (equipment == footstep && student == femaleStudent && isObjectIdle == true)
            {
                Destroy(selectedStudentEquipment);
                selectedStudentEquipment = Instantiate(femaleFootstep, spawnPoint.position, spawnPoint.rotation);
                isObjectIdle = false;
            }

        }

        if (manager.selectionPanel.activeSelf == true)
        {
            ItemSelection();
        
        }

    }

    void RotateBike(GameObject studentEquipment)
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if(Input.GetKey(KeyCode.A))
        {
            studentEquipment.transform.Rotate(0, 1f, 0);
            
        }
        else if(Input.GetKey(KeyCode.S))
        {
            studentEquipment.transform.Rotate(0, -1f, 0);
        }

    }

    void ItemSelection()
    {
            if (gender.text == "Female")
            {
                if (student == null)
                {
                    student = femaleStudent;
                    selectedStudent = Instantiate(femaleStudent, spawnPoint.position + new Vector3(0, 0, 3f), spawnPoint.rotation);
                    isGenderSelected = true;
                Debug.Log("Female chosen");
                }
                else if (student != femaleStudent)
                {
                    Destroy(selectedStudent);
                    student = femaleStudent;
                    selectedStudent = Instantiate(femaleStudent, spawnPoint.position + new Vector3(0, 0, 3f), spawnPoint.rotation);
                   
                }
            }
            else if (gender.text == "Male")
            {
                if (student == null)
                {
                    student = maleStudent;
                    selectedStudent = Instantiate(maleStudent, spawnPoint.position + new Vector3(0, 0, 3f), spawnPoint.rotation);
                    isGenderSelected = true;
                    
                }
                else if (student != maleStudent)
                {
                    Destroy(selectedStudent);
                    student = maleStudent;
                    selectedStudent = Instantiate(maleStudent, spawnPoint.position + new Vector3(0, 0, 3f), spawnPoint.rotation);
                   
                }
            }
   
   
        if (equipmentText.text == "Ergobike")
        {
            if (equipment == null)
            {
                equipment = bike; 
                selectedEquipment = Instantiate(bike, spawnPoint.position + new Vector3(0, 0, 1f), spawnPoint.rotation);
                isEquipmentSelected = true;
                
            }
            else if (equipment != bike)
            {
                Destroy(selectedEquipment);
                equipment = bike;
                selectedEquipment = Instantiate(bike, spawnPoint.position + new Vector3(0, 0, 1f), spawnPoint.rotation);
               
            }
        }
        else if (equipmentText.text == "Footstep")
        {

            if (equipment == null)
            {
                equipment = footstep;
                selectedEquipment = Instantiate(footstep, spawnPoint.position + new Vector3(0, 0, 1f), spawnPoint.rotation);
                isEquipmentSelected = true;
                
            }
            else if (equipment != footstep)
            {
                Destroy(selectedEquipment);
                equipment = footstep;
                selectedEquipment = Instantiate(footstep, spawnPoint.position + new Vector3(0, 0, 1f), spawnPoint.rotation);
               
            }
        }
  
    }


    public void RedoSimDiffGender()
    {
        if(simManager.isMaleErgoDone == false || simManager.isFemaleErgoDone == false || simManager.isMaleFootstepDone == false || simManager.isFemaleFootstepDone == false)
        {
            Destroy(selectedStudentEquipment);
            manager.enabled = false;
            conversationPanel.currentPanelIndex = 15;
            conversationPanel.ShowNextPanel();
        }
        else if(simManager.isMaleErgoDone == true && simManager.isFemaleErgoDone == true && simManager.isMaleFootstepDone == true && simManager.isFemaleFootstepDone == true)
        {
            conversationPanel.ShowNextPanel();
        }
     
      /*  if (student == maleStudent && equipment == footstep)
        {
            selectedStudentEquipment = Instantiate(femaleFootstep, spawnPoint.position, spawnPoint.rotation);
            student = femaleStudent;
        }
        else if(student == femaleStudent && equipment == footstep)
        {
            selectedStudentEquipment = Instantiate(maleFootstep, spawnPoint.position, spawnPoint.rotation);
            student = maleStudent;
        }
        else if (student == femaleStudent && equipment == bike)
        {
            selectedStudentEquipment = Instantiate(maleErgobike, spawnPoint.position, spawnPoint.rotation);
            student = maleStudent;
        }
        else if (student == maleStudent && equipment == bike)
        {
            selectedStudentEquipment = Instantiate(femaleErgobike, spawnPoint.position, spawnPoint.rotation);
            student = femaleStudent;
        }*/

        manager.enabled = true; 
    }
    
    public void RedoSimDiffEquip()
    {
        Destroy(selectedStudentEquipment);
        manager.enabled = false;
        //conversationPanel.currentPanelIndex = 24;
        conversationPanel.ShowNextPanel();
        if (student == maleStudent && equipment == bike)
        {
            selectedStudentEquipment = Instantiate(maleFootstep, spawnPoint.position, spawnPoint.rotation);
            manager.repetitionSpeedText.text = "Repetisi           :";
            manager.repetitionKiloperHourText.text = "repetisi";
            equipment = footstep;
        }
        else if (student == maleStudent && equipment == footstep)
        {
            selectedStudentEquipment = Instantiate(maleErgobike, spawnPoint.position, spawnPoint.rotation);
            manager.repetitionSpeedText.text = "Kecepatan       :";
            manager.repetitionKiloperHourText.text = "km/h";
            equipment = bike;
        }
        else if(student == femaleStudent && equipment == bike)
        {
            selectedStudentEquipment = Instantiate(femaleFootstep, spawnPoint.position, spawnPoint.rotation);
            manager.repetitionSpeedText.text = "Repetisi            :";
            manager.repetitionKiloperHourText.text = "repetisi";
            equipment = footstep;
        } 
        else if(student == femaleStudent && equipment == footstep)
        {
            selectedStudentEquipment = Instantiate(femaleErgobike, spawnPoint.position, spawnPoint.rotation);
            manager.repetitionSpeedText.text = "Kecepatan       :";
            manager.repetitionKiloperHourText.text = "km/h";
            equipment = bike;
        }

        manager.enabled = true;
    }

    private void ScoreCount()
    {

    }

}
