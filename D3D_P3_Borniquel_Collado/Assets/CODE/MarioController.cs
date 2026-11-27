using System.Threading;
using TreeEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using static GameController;

public class MarioController : MonoBehaviour, IRestartGameElement
{
    Vector3 m_StartPosition;
    Quaternion m_StartRotation;
    public Camera m_Camera;
    CharacterController m_CharacterController;
    Animator m_Animator;
    public Collider m_GroundCheckerCollider;

    public float m_PlatformMaxAngle = 30f;

    public float m_VerticalSpeed = 0f;
    public float m_JumpSpeed = 7f;

    bool m_OnGround = false;

    Checkpoint m_Checkpoint;
    Checkpoint m_CurrentCheckpoint;

    public float m_WalkSpeed = 4.0f;
    public float m_RunSpeed = 8f;
    public float m_RotationLerpPct = 10f;

    public GameObject m_RightHandCollider;
    public GameObject m_LeftHandCollider;
    public GameObject m_KickCollider;


    [Header("Input")]
    public Key m_RightKeyControl=Key.D;
    public Key m_LeftKeyControl = Key.A;
    public Key m_ForwardKeyControl = Key.W;
    public Key m_BackKeyControl = Key.S;
    public Key m_RunKeyControl = Key.LeftShift;
    public Key m_JumpKeyControl = Key.Space;
    public Key m_DebugKeyControl = Key.R;

    Collider m_AttachedPlatform;

    public int m_PunchMouseButton = 0;
    public bool m_IsPunching=false;
    float m_LastPunchInitTime=0;
    public float m_PunchCOmboTime = 1;
     int m_PunchId=0;
   

    private void Awake()
    {
        m_CharacterController = GetComponent<CharacterController>();
        m_Animator = GetComponent<Animator>();
    }

    private void Start()
    {
        m_RightHandCollider.SetActive(false);
        m_LeftHandCollider.SetActive(false);
        m_KickCollider.SetActive(false);

        transform.position=m_StartPosition;
        transform.rotation = m_StartRotation;
        GameController.GetGameController().AddRestartGameElement(this);

    }

    private void Update()
    {

        Vector3 l_Right = m_Camera.transform.right;
        Vector3 l_Forward = m_Camera.transform.forward;
        l_Right.y = 0;
        l_Forward.y = 0;
        l_Right.Normalize();
        l_Forward.Normalize();

        Vector3 l_Direction = Vector3.zero;


        UpdatePunch();
        if (Keyboard.current[m_RightKeyControl].isPressed)
        {
            l_Direction = l_Right;
        }
        else if (Keyboard.current[m_LeftKeyControl].isPressed)
        {
            l_Direction = -l_Right;
        }

        if (Keyboard.current[m_ForwardKeyControl].isPressed)
        {
            l_Direction += l_Forward;
        }
        else if (Keyboard.current[m_BackKeyControl].isPressed)
        {
            l_Direction -=l_Forward;
        }


        bool l_IsMoving = l_Direction.sqrMagnitude>0.5f;
        float l_AnimatorSpeed = 0;
        float l_Speed = m_WalkSpeed;
        l_Direction.Normalize();

        if (l_IsMoving)
        {
            if (Keyboard.current[m_RunKeyControl].isPressed && m_OnGround)
            {
                l_Speed = m_RunSpeed;
                l_AnimatorSpeed = 1f;
            }
            else
            {
                l_AnimatorSpeed = 0.3f;
            }
            Quaternion l_DesiredRotation = Quaternion.LookRotation(l_Direction); 
            transform.rotation = Quaternion.Lerp(transform.rotation ,l_DesiredRotation, m_RotationLerpPct*Time.deltaTime);
        }
        if (Keyboard.current[m_JumpKeyControl].isPressed && m_OnGround)
        {
            m_VerticalSpeed = m_JumpSpeed;
        }
        m_Animator.SetFloat("Speed", l_AnimatorSpeed);
        l_Direction.Normalize();

        l_Direction *= l_Speed * Time.deltaTime;
        m_VerticalSpeed += Physics.gravity.y * Time.deltaTime;
        l_Direction.y = m_VerticalSpeed * Time.deltaTime;
        CollisionFlags l_CollisionFlags = m_CharacterController.Move(l_Direction);

        m_OnGround = (l_CollisionFlags & CollisionFlags.Below) != 0;

        if (((l_CollisionFlags & CollisionFlags.Below) != 0) || (l_CollisionFlags & CollisionFlags.Above) != 0 & m_VerticalSpeed > 0f)
        {
            m_VerticalSpeed = 0;
        }
        m_CharacterController.Move(l_Direction);
    }


    private void OnCollisionEnter(Collision collision)
    {
        
    }
    private void Punch()
    {
        SetIsPunching(true);
        float l_TotalTimeSInceLastPunch = Time.time - m_LastPunchInitTime;
        if (l_TotalTimeSInceLastPunch <= m_PunchCOmboTime)
        {
            m_PunchId = (m_PunchId + 1) % 3;
        }
        else
            m_PunchId = 0;
            m_Animator.SetTrigger("Punch");
            m_Animator.SetInteger("Punch", m_PunchId);


    }
    public void SetIsPunching(bool isPunching)
    {
        m_IsPunching = isPunching;
        if (!isPunching)
        {
            m_LastPunchInitTime = Time.time;
        }
    }
    private bool CanPunch()
    {
        return m_IsPunching; 

    }
    void UpdatePunch()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame && CanPunch())
        {
            Punch();
        }
    }

    public void RestartGame()
    {
        m_CharacterController.enabled = false;
        if (m_CurrentCheckpoint!=null)
        {
            transform.position = m_CurrentCheckpoint.m_StartPosition.position;
            transform.rotation = m_CurrentCheckpoint.m_StartPosition.rotation;
        }
        else
        {
            transform.position = m_StartPosition;
            transform.rotation = m_StartRotation;
        }
            
        m_CharacterController.enabled = true;
        throw new System.NotImplementedException();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Elevator") && other==m_AttachedPlatform)
        {
            DetachPlatform();
        }
        else if (other.CompareTag("Checkpoint"))
        {
            m_CurrentCheckpoint=other.GetComponent<Checkpoint>();
        }
    }

    bool CanAttachToPlatform(Collider _Collider)
    {
        float l_DotAngle = Vector3.Dot(_Collider.transform.up, Vector3.up);
        return (l_DotAngle > Mathf.Cos(m_PlatformMaxAngle * Mathf.Deg2Rad));

    }
    void AttachPlatform(Collider _Collider)
    {
        m_AttachedPlatform =_Collider;
        transform.SetParent(m_AttachedPlatform.transform);
    }

    void UpdateAttachedPlatform()
    {
        if (m_AttachedPlatform != null)
        {
            Vector3 l_Euler = transform.rotation.eulerAngles;
            l_Euler.x = 0;
            l_Euler.z = 0;
            transform.rotation = Quaternion.Euler(l_Euler);
            transform.SetParent(null);
        }

    }

    void DetachPlatform()
    {
        m_AttachedPlatform=null;
        Vector3 l_Euler=transform.rotation.eulerAngles;
        l_Euler.x = 0;
        l_Euler.z = 0;
        transform.rotation = Quaternion.Euler(l_Euler);
        transform.SetParent(null);
    }

    void LateUpdate()
    {
        UpdateAttachedPlatform();
    }

    void Step()
    {
        
    }
}
