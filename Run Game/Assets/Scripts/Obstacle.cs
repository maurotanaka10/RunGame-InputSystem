using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float velocity;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * velocity * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            velocity *= -1;
        }
    }
}
