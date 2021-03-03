using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IPunObservable
{
    public float horizontalSpeed = 4f;
    private int score = 0;
    public UnityEngine.UI.Text scoreText;
    private PhotonView photonView;
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<GameManager>().AddPlayer(this);
        scoreText = GameObject.Find("ScoreValue").GetComponent<UnityEngine.UI.Text>();
        photonView = GetComponent<PhotonView>();
        score = PlayerPrefs.GetInt("catched", 0);
        scoreText.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine) return;

        if (Input.GetAxis("Horizontal") < 0)
        {
            transform.position = new Vector3(transform.position.x - horizontalSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        }else if (Input.GetAxis("Horizontal") > 0)
        {
            transform.position = new Vector3(transform.position.x + horizontalSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Food"))
        {
            PlayerPrefs.SetInt("catched", ++score);
            scoreText.text = score.ToString();
            Destroy(other.gameObject);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(score);
        }
        else
        {
            score = (int)stream.ReceiveNext();
        }
    }
}
