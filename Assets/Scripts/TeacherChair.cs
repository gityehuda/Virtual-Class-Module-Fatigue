using Binus.WebGL.Service;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherChair : MonoBehaviour
{
    public int id;
    public bool isSit = false;

    public void CharacterSitting()
    {
        IPlayerManager playerManager = ServiceLocator.GetService<IPlayerManager>();

        GameObject tempCharacter = playerManager.character;
        playerManager.characterGesture = CustomClass.CharacterGesture.Sit;
        playerManager.idChair = id;

        tempCharacter.transform.position = transform.position;
        tempCharacter.transform.rotation = transform.rotation;
        tempCharacter.transform.localEulerAngles += new Vector3(0, 180f, 0);
        tempCharacter.transform.localPosition -= new Vector3(0, 0.360972f, 0);
        tempCharacter.GetComponent<CapsuleCollider>().isTrigger = true;

        playerManager.animator.SetInteger("Animation", 3);
    }
}
