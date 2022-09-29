using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;
    public List<Vector2> spawnPoints;

    private void Start()
    {
        int randomPoint = Random.Range(0, spawnPoints.Count);
        PhotonNetwork.Instantiate(playerPrefab.name, spawnPoints[randomPoint], Quaternion.identity);
    }
}
