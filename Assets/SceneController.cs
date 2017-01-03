using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets
{

    public class SceneController : MonoBehaviour {

        public GameObject rubixCube;
        public GameObject currentCube;
        private RubixController currentCubeController;
        public float buttonDelay = 1.2f;

        void Start()
        {
            currentCube = Instantiate(rubixCube, Vector3.zero, Quaternion.identity);
            currentCubeController = currentCube.GetComponent<RubixController>();
        }


        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                Destroy(currentCube);
                currentCube = Instantiate(rubixCube, Vector3.zero, Quaternion.identity);
                RubixController.rotationHistory = "";
            }

            //////////////////////

            if (Input.GetKeyDown(KeyCode.F9))//Load the QuickSave from a fresh cube.
            {
                //GameObject backUp = currentCube;//What will happen here??
                try
                {
                    Destroy(currentCube);
                    currentCube = Instantiate(rubixCube, Vector3.zero, Quaternion.identity);
                    currentCubeController = currentCube.GetComponent<RubixController>();
                    StartCoroutine(currentCubeController.LoadState("QuickSave"));
                    RubixController.rotationHistory = "";
                    print(String.Format("F9: {0}",PlayerPrefs.GetString("QuickSave")));
                }
                catch (Exception ex)
                {
                    print("There was a problem loading the QuickSave");
                    print(ex);
                    
                }

            }

            if (Input.GetKeyDown(KeyCode.F5))
            {
                PlayerPrefs.SetString("QuickSave", RubixController.rotationHistory);
            }


            ///////////////////////
        }
    }
}