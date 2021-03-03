using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class FoodBehaviour : MonoBehaviour
{
    public float speed = 3f;
    public GameObject destroyEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - speed * Time.deltaTime, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Ground")
        {
            StartCoroutine(DestroyCorutine());
            speed = 0;
        }
    }

    private IEnumerator DestroyCorutine()
    {
        var particles = PhotonNetwork.Instantiate(destroyEffect.name, transform.position, transform.rotation);
        GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(1);
        PhotonNetwork.Destroy(this.gameObject);
        PhotonNetwork.Destroy(particles);
    }
}
