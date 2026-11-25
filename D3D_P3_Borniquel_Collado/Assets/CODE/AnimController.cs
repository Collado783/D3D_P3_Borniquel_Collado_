using UnityEngine;

public class PlayAnimation : MonoBehaviour
{
    Animation m_Animation;

    void Start()
    {
        m_Animation = GetComponent<Animation>();
        m_Animation.Play();
    }
}