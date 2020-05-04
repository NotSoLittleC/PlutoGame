using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject playerObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        Vector3 playerPosition = playerObject.transform.position;
        transform.position = new Vector3(playerPosition.x, transform.position.y, transform.position.z);
    }
}
