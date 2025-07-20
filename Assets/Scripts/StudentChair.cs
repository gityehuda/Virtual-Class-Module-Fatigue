using Binus.WebGL.Service;
using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;

public class StudentChair : MonoBehaviour
{
    public int id;
    public bool isSit = false;

    private void Update()
    {
        // === PC Mouse Input ===
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            {
                // Clicked UI — skip
                return;
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastToSelf(ray);
        }

        // === Mobile Touch Input ===
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                // Touched UI — skip
                return;
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastToSelf(ray);
        }
    }

    private void RaycastToSelf(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject == this.gameObject)
            {
                OnClicked();
            }
        }
    }

    private void OnClicked()
    {
        // Your real logic here
        if (isSit || ShareScreenManager.instance.isMyShared || GameplayManager.instance.CheckSomeWindowActive()) return;

        IPlayerManager playerManager = ServiceLocator.GetService<IPlayerManager>();

        if (playerManager.characterGesture == CustomClass.CharacterGesture.Sit)
        {
            CustomClass.DataCharacterSit dataLocal = new CustomClass.DataCharacterSit
            {
                actorNumber = PhotonNetwork.LocalPlayer.ActorNumber,
                idChairs = playerManager.idChair,
                isSit = false
            };

            PhotonManager.instance.SendData((byte)CustomClass.TypeData.CharacterSit, dataLocal);
        }

        CharacterSitting();

        CustomClass.DataCharacterSit data = new CustomClass.DataCharacterSit
        {
            actorNumber = PhotonNetwork.LocalPlayer.ActorNumber,
            idChairs = id,
            isSit = true
        };

        PhotonManager.instance.SendData((byte)CustomClass.TypeData.CharacterSit, data);
    }

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
        tempCharacter.GetComponent<CapsuleCollider>().enabled = false;
        playerManager.animator.SetInteger("Animation", 3);
    }
}
