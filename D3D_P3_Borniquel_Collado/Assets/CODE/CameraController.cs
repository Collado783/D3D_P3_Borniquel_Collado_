using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class CameraController : MonoBehaviour
{
    float m_Yaw = 0f;
    float m_Pitch = 0f;
    public float m_YawSpeed = 720f;
    public float m_PitchSpeed = 360f;
    public float m_MaxPitch = 720f;
    public float m_MinPitch = 720f;
    public float m_MinDistance = 8f;
    public float m_MaxDistance = 8f;
    public float m_AvoidWallsDistance = 10f;
    public LayerMask m_LayerMask;

    public float m_CameraDistance = 0.1f;
    public Transform m_LookAt;

    private void LateUpdate()
    {
        DeltaControl l_DeltaMouse = Mouse.current.delta;
        m_Yaw = m_Yaw + l_DeltaMouse.x.value * m_YawSpeed * Time.deltaTime;
        m_Pitch = m_Pitch + l_DeltaMouse.y.value * m_PitchSpeed * Time.deltaTime;

        m_Pitch = Mathf.Clamp(m_Pitch,m_MinPitch, m_MaxPitch);

        float l_YawRadians=m_Yaw*Mathf.Deg2Rad;
        float l_PitchRadians = m_Pitch * Mathf.Deg2Rad;

        Vector3 l_Direction = new Vector3(
            Mathf.Sin(l_YawRadians) * Mathf.Cos(l_PitchRadians),
            Mathf.Sin(l_PitchRadians), 
            Mathf.Cos(l_YawRadians)*Mathf.Cos(l_PitchRadians));

        float l_Distance = Vector3.Distance(m_LookAt.position, transform.position);
        float l_CameraDistance = Math.Clamp(l_Distance, m_MinDistance, m_MaxDistance) ;
        Vector3 l_DesiredPosition = m_LookAt.position + l_Direction* l_CameraDistance;

        Ray l_Ray = new Ray(m_LookAt.position, l_Direction);

        if(Physics.Raycast(l_Ray, out RaycastHit l_RaycastHit, l_CameraDistance, m_LayerMask.value, QueryTriggerInteraction.Ignore))
        {
            l_DesiredPosition = l_RaycastHit.point - l_Direction * m_AvoidWallsDistance;
        }

        transform.position = l_DesiredPosition;
        transform.LookAt(m_LookAt.position);
    }
}
