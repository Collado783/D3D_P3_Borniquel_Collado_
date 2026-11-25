using UnityEngine;

public class CoinItem : Item
{

    public int CoinPoints = 1;
    MarioController m_Player;
    private void Start()
    {
        m_Player = FindObjectOfType<MarioController>();
    }
    public override bool CanPick()
    {
        return true;
    }
    public override void Pick()
    {
        base.Pick();
        //m_Player.Score+= CoinPoint;
        //m_UI.Coins+=CoinPoint; 

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && CanPick())
        {
            Pick();
        }
    }
}
