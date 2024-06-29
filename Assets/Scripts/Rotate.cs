using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Vector3 vector;
    public int x, y, z;

    // Start is called before the first frame update
    void Start()
    {
        vector = new Vector3(x, y, z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(vector * Time.deltaTime);
    }
}
