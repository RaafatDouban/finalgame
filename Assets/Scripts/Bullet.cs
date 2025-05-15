using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float strength;

    // Use this for initialization
    void Update()
    {
            transform.Translate(Vector2.up * Time.deltaTime * speed);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Bolt Destroyer")
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }
}