using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class ServerFeatures : MonoBehaviour
{
    public Text ServerNameText;
    public GameObject[] players;

    // Start is called before the first frame update
    void Start()
    {
        ServerNameText.text = PhotonNetwork.CurrentRoom.Name;   
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    private void Update()
    {
        print("Current players in lobby: " + players.Length);
    }

    public void AddPlayers()
    {
        //players.Clear();
    }
}
