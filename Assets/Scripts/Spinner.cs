using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField] float rotation;
    void Start()
    {
        //gameObject.GetComponent<Rigidbody2D>().rotation = rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //gameObject.GetComponent<Rigidbody2D>().rotation += rotation;
        transform.Rotate(0, 0, rotation * Time.deltaTime);
    }
}
