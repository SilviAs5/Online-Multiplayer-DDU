using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class ServerFeatures : MonoBehaviour
{
    public Text ServerNameText;
    public List<GameObject> players;
    private List<GameObject> tempList;

    // Start is called before the first frame update
    void Start()
    {
        ServerNameText.text = PhotonNetwork.CurrentRoom.Name;   
    }

    private void Update()
    {
        //AddPlayers();
    }

    public void AddPlayers()
    {
        GameObject g = GetComponentInParent<GameObject>(CompareTag("Player"));        
        players.Add(g);
    }
}
