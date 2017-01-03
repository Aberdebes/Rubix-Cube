using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    class CameraController : MonoBehaviour
    {
        public float mouseSensitivity = 20f;
        public float limit = 0.7f;
        public float keyboardSensitivity = 10f;
        public float slerpSpeed = 1.0f;

        void Update()
        { 
            if (Input.GetMouseButtonDown(1))
            {
                Cursor.lockState = CursorLockMode.Locked;
            }

            if (Input.GetMouseButton(1))
            {
                transform.RotateAround(Vector3.zero, Vector3.up, mouseSensitivity * Input.GetAxis("Mouse X"));
                if (transform.forward.y >= limit && Input.GetAxis("Mouse Y") < 0)
                {
                    transform.RotateAround(Vector3.zero, -transform.right, mouseSensitivity * Input.GetAxis("Mouse Y"));
                }
                else if (transform.forward.y <= -limit && Input.GetAxis("Mouse Y") > 0)
                {
                    transform.RotateAround(Vector3.zero, -transform.right, mouseSensitivity * Input.GetAxis("Mouse Y"));
                }
                else if (-limit < transform.forward.y && transform.forward.y < limit)
                {
                    transform.RotateAround(Vector3.zero, -transform.right, mouseSensitivity * Input.GetAxis("Mouse Y"));
                }

                transform.LookAt(Vector3.zero);
            }

            if (Input.GetMouseButtonUp(1))
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                RubixController.rotateLock = true;
                transform.RotateAround(Vector3.zero, Vector3.up, keyboardSensitivity * Input.GetAxis("Horizontal"));
                if (transform.forward.y >= limit && Input.GetAxis("Vertical") < 0)
                {
                    transform.RotateAround(Vector3.zero, -transform.right, keyboardSensitivity * Input.GetAxis("Vertical"));
                }
                else if (transform.forward.y <= -limit && Input.GetAxis("Vertical") > 0)
                {
                    transform.RotateAround(Vector3.zero, -transform.right, keyboardSensitivity * Input.GetAxis("Vertical"));
                }
                else if (-limit < transform.forward.y && transform.forward.y < limit)
                {
                    transform.RotateAround(Vector3.zero, -transform.right, keyboardSensitivity * Input.GetAxis("Vertical"));
                }

                transform.LookAt(Vector3.zero);
            }

            if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
            {
                RubixController.rotateLock = false;
            }

            if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
            {
                float startTime = Time.time;
                StartCoroutine(LookAround(startTime));

            }
        }

        IEnumerator LookAround(float startTime)
        {
            Vector3 initialPosition = transform.position;
            Vector3 newPosition = -initialPosition;
            float fractionComplete = (Time.time - startTime) / slerpSpeed;
            while (fractionComplete < 0.99)
            {
                transform.position = Vector3.Slerp(initialPosition, newPosition, fractionComplete);
                transform.LookAt(Vector3.zero);
                fractionComplete = (Time.time - startTime) / slerpSpeed;
                yield return null;
            }
            transform.position = newPosition;
            transform.LookAt(Vector3.zero);
        }
    }
}
