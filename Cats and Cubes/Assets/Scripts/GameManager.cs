using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject food;
    public GameObject player;
    public List<PlayerMovement> players = new List<PlayerMovement>();

    public void AddPlayer(PlayerMovement player)
    {
        players.Add(player);
    }

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.Instantiate(player.name, new Vector3(Random.Range(-5, 5), -3, 0), Quaternion.identity);
        StartCoroutine(FoodSpawner());   
    }
    private IEnumerator FoodSpawner()
    {
        while (true)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == 2 && PhotonNetwork.IsMasterClient)
            {
                yield return new WaitForSecondsRealtime(2);
                PhotonNetwork.Instantiate(food.name, new Vector3(Random.Range(-9, 9), 6, 0), Quaternion.identity);
            }
            yield return null;
        }
    }
}
