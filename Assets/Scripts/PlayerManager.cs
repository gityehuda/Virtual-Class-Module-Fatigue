using Binus.WebGL.Service;
using Photon.Pun;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IPlayerManager
{
    [SerializeField] private float speed = 2f;

    public Animator animator { get; set; }
    public Rigidbody rigidBody;

    public bool isNotWalking { get; set; } = true;

    public GameObject character { get; set; }
    public Rigidbody characterRigidbody;
    public Collider characterCollider;

    public float maxInteractionDistance = 15.0f;

    [SerializeField] GameObject almameter;
    [SerializeField] GameObject batikUngu;
    [SerializeField] GameObject batikPutih;

    public bool isMine = true;
    public string roleUsr;
    public string sexUsr;

    public CustomClass.CharacterGesture characterGesture { get; set; } = CustomClass.CharacterGesture.Idle;
    public int idChair { get; set; } = -1;

    private FixedJoystick fixedJoystick;

    private void Start()
    {
        ServiceLocator.RegisterService<IPlayerManager>(this);

        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
        character = gameObject;
        characterRigidbody = GetComponent<Rigidbody>();
        characterCollider = GetComponent<CapsuleCollider>();
        characterCollider.isTrigger = false;
        maxInteractionDistance = 1;

        almameter = transform.GetChild(2).transform.GetChild(0).gameObject;
        batikUngu = transform.GetChild(2).transform.GetChild(1).gameObject;
        batikPutih = transform.GetChild(2).transform.GetChild(2).gameObject;

        almameter.SetActive(false);
        batikUngu.SetActive(false);
        batikPutih.SetActive(false);
        if (MainData.instance.roleUsr == "1")
        {
            (MainData.instance.sexUsr == "F" ? batikPutih : batikUngu).SetActive(true);
        }
        else
        {
            almameter.SetActive(true);
        }

#if UNITY_ANDROID || UNITY_IOS
        MobileSearchForFixedJoystick();
#endif
    }

    private void MobileSearchForFixedJoystick()
    {
        IFixedJoystick fixedJoystickInterface = ServiceLocator.GetService<IFixedJoystick>();
        fixedJoystick = (fixedJoystickInterface as FixedJoystick);
        if (fixedJoystick == null)
        {
            Debug.LogWarning("FixedJoystick not found");
        }
    }

    private void FixedUpdate()
    {
#if UNITY_WEBGL
        WebGLMoving();
#elif UNITY_ANDROID || UNITY_IOS
        MobileMoving();
#endif
    }

    private void WebGLMoving()
    {
        if (!isNotWalking || Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
            return;

        var horizontal = 0;
        var vertical = 0;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            vertical = 1;
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            vertical = -1;
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            horizontal = 1;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            horizontal = -1;
        }

        var cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0;

        var moveDirection = (horizontal * Camera.main.transform.right + vertical * cameraForward).normalized;

        if (vertical == 0 && horizontal == 0)
        {
            if (characterGesture != CustomClass.CharacterGesture.Sit)
            {
                animator.SetInteger("Animation", 0);

                transform.localPosition = new Vector3(transform.localPosition.x, GameplayManager.instance.humanYPosition, transform.localPosition.z);
            }
        }
        else
        {
            if (characterGesture == CustomClass.CharacterGesture.Sit)
            {
                StandUp();

                CustomClass.DataCharacterSit data = new CustomClass.DataCharacterSit
                {
                    actorNumber = PhotonNetwork.LocalPlayer.ActorNumber,
                    idChairs = idChair,
                    isSit = false
                };

                PhotonManager.instance.SendData((byte)CustomClass.TypeData.CharacterSit, data);
                return;
            }
            animator.SetInteger("Animation", 1);
            Quaternion newRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = newRotation;
        }

        rigidBody.velocity = (moveDirection * speed);
    }

    private void MobileMoving()
    {
        // Joystick Controller Mode
        if (!isNotWalking || fixedJoystick == null)
            return;

        float horizontal = fixedJoystick.Horizontal;
        float vertical = fixedJoystick.Vertical;

        if (horizontal == 0 && vertical == 0)
        {
            if (characterGesture != CustomClass.CharacterGesture.Sit)
            {
                animator.SetInteger("Animation", 0);

                transform.localPosition = new Vector3(transform.localPosition.x, GameplayManager.instance.humanYPosition, transform.localPosition.z);
            }
            return;
        }
        else
        {
            if (characterGesture == CustomClass.CharacterGesture.Sit)
            {
                StandUp();

                CustomClass.DataCharacterSit data = new CustomClass.DataCharacterSit
                {
                    actorNumber = PhotonNetwork.LocalPlayer.ActorNumber,
                    idChairs = idChair,
                    isSit = false
                };

                PhotonManager.instance.SendData((byte)CustomClass.TypeData.CharacterSit, data);
                return;
            }
        }

        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0;

        Vector3 moveDirection = (horizontal * Camera.main.transform.right + vertical * cameraForward).normalized;

        animator.SetInteger("Animation", 1);

        Quaternion newRotation = Quaternion.LookRotation(moveDirection);
        transform.rotation = newRotation;

        rigidBody.velocity = moveDirection * speed;
    }

    public void StandUp()
    {
        characterGesture = CustomClass.CharacterGesture.Idle;
        animator.SetInteger("Animation", 0);
        GetComponent<CapsuleCollider>().enabled = true;
    }
}
    
