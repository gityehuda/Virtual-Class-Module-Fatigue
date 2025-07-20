using Binus.WebGL.Service;
using Photon.Pun;using UnityEngine;
using UnityEngine.UI;

public class SittingManager : MonoBehaviour, ISittingManager
{
    public GameObject buttonSitting;

    private void Start()
    {
        ServiceLocator.RegisterService<ISittingManager>(this);

        if(MainData.instance.roleUsr != "1")
        {
            buttonSitting.SetActive(false);
            return;
        }

        buttonSitting.SetActive(true);

        buttonSitting.GetComponent<Button>().onClick.AddListener(delegate
        {
            ButtonSitting();
        });
    }
    public void HandleOtherCharacterSit(CustomClass.DataCharacterSit data)
    {
        GameplayManager.instance.studentChairs[data.idChairs].isSit = data.isSit;
        GameplayManager.instance.listOfUser[data.actorNumber].characterGesture = (data.isSit ? CustomClass.CharacterGesture.Sit : CustomClass.CharacterGesture.Idle);
    }

    public void HandleNeedStudentChair(CustomClass.DataNeedStudentSit data)
    {
        if(data.actorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
        {
            GameplayManager.instance.studentChairs[data.idChairs].CharacterSitting();
        }
        else
        {
            CustomClass.DataCharacterSit anotherPlayerData = new CustomClass.DataCharacterSit
            {
                actorNumber = data.actorNumber,
                idChairs = data.idChairs,
                isSit = true,
            };

            HandleOtherCharacterSit(anotherPlayerData);
        }
    }

    public void ButtonSitting()
    {
        foreach(var child in GameplayManager.instance.listOfUser)
        {
            if(child.Value.characterGesture != CustomClass.CharacterGesture.Sit)
            {
                foreach(var childChair in GameplayManager.instance.studentChairs)
                {
                    if(childChair.isSit == false)
                    {
                        child.Value.characterGesture = CustomClass.CharacterGesture.Sit;
                        childChair.isSit = true;

                        CustomClass.DataNeedStudentSit data = new CustomClass.DataNeedStudentSit
                        {
                            actorNumber = child.Key,
                            idChairs = childChair.id
                        };

                        PhotonManager.instance.SendData((byte)CustomClass.TypeData.NeedStudentSit, data);

                        break;
                    }
                }
            }
        }
    }
}
