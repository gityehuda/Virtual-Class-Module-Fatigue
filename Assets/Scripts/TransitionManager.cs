using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(WaitForJoin());
    }

    IEnumerator WaitForJoin()
    {
        yield return new WaitForSeconds(2);
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(MainData.instance.roomName);
    }
}
