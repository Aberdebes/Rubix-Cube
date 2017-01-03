using UnityEngine;
using System.Collections;

public class SideRotator : MonoBehaviour {

    //public GameObject mainCube;
    public float speed = 10;
    //private Rigidbody rb;
    private Vector3 currentMousePos;
    //void Start()
    //{
    //    //mainCube = GetComponent<GameObject>();
    //    rb = GetComponent<Rigidbody>();
    //}
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentMousePos = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            transform.Rotate(new Vector3(speed*(Input.mousePosition.magnitude - currentMousePos.magnitude),0,0) * Time.deltaTime);
            //rb.AddForce(currentMousePos - Input.mousePosition);
            //Debug.Log(currentMousePos - Input.mousePosition);
            currentMousePos = Input.mousePosition;
            print(transform.eulerAngles);
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (transform.eulerAngles.x >= 315 || transform.eulerAngles.x <= 45)
            {
                //transform.Rotate(new Vector3(360 - transform.eulerAngles.x, 0, 0));
                transform.LookAt(Vector3.up);
            }
            //Debug.Log(transform.localRotation);
            //Debug.Log(transform.eulerAngles);
            //print(360 - transform.eulerAngles.x);
            //print(270 - transform.eulerAngles.x);
            //print(180 - transform.eulerAngles.x);
            //print(90 - transform.eulerAngles.x);


        }
    }
}
