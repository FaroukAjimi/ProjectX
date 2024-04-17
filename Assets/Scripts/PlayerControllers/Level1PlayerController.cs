using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1PlayerController : MonoBehaviour
{
    public  CharacterController controller;
    public float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float hrzntl = Input.GetAxis("Horizontal");
        float vrtcl = Input.GetAxis("Vertical");
        Vector3 MoveDirection = new Vector3(hrzntl, 0, vrtcl);
        controller.Move(MoveDirection * Time.deltaTime * speed);
    }
}
