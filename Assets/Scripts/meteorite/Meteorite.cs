using UnityEngine;

public class Meteorite : MonoBehaviour
{
    public GameObject collisionSystem;

    public GameObject moveSystem;

    void OnCollisionEnter(Collision collision)
    {
        collisionSystem.SetActive(true);

        moveSystem.SetActive(false);

        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player");
        }

        Destroy(gameObject, 2);
    }
}
