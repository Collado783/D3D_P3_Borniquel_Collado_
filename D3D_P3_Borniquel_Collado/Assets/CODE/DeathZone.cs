using System.Net.Sockets;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    GameController gameController;
    MarioController m_player;
    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameController.GameOverUI();

            Debug.Log("Muerte");
        }

    }
}
