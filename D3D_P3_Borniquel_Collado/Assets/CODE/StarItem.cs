using UnityEngine;

public class StarItem : Item
{

    public int StarHealth = 1;
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
        //m_Player.Health+= StarHealth;
        //m_UI.Health.Heal(StarHeakth); 

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && CanPick()) // && m_Player.Health >100)
        {
            Pick();
        }
    }
}
