using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class ServerFeatures : MonoBehaviour
{
    public Text ServerNameText;

    // Start is called before the first frame update
    void Start()
    {
        ServerNameText.text = PhotonNetwork.CurrentRoom.Name;   
    }

}
