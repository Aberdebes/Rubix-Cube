using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    class RubixController : MonoBehaviour
    {
        public GameObject preFab;
        public RubixControllerSide[] sides;
        public RubixControllerSide currentSelection = null;
        private Ray clickSelectionRay;
        private RaycastHit hit;
        public static int randomLimit = 30;
        public static bool rotateLock;
        public static float animationSpeed = 0.001f;
        public static int rotationSmoothing = 30; //must divide 90 for it to work properly.

        ///////// SAVING
        public static string rotationHistory = "";
        public float buttonDelay = 1.2f;
        /////////



        void Start()
        {
            for (int i = 0; i < sides.Length; i++)
            {
                sides[i].historyName = i.ToString();
            }
        }

        void Update()
        {
            ///////// DEBUG
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
            ///////////////

            clickSelectionRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(clickSelectionRay, out hit, Mathf.Infinity, 1 << 8))
                {
                    if (currentSelection != null)
                    {
                        currentSelection.isSelected = false;
                        currentSelection = hit.collider.GetComponentInParent<RubixControllerSide>();
                        currentSelection.isSelected = true;
                    }
                    else
                    {
                        currentSelection = hit.collider.GetComponentInParent<RubixControllerSide>();
                        currentSelection.isSelected = true;
                    }
                    //print(currentSelection.name);
                }
            }

            if (Input.GetKeyDown(KeyCode.R) && !rotateLock)
            {
                //rotateLock = true;
                StartCoroutine("RandomizeCube");
            }

            //if (Input.GetKeyDown(KeyCode.F9))//Load from a fresh cube.
            //{
            //    StartCoroutine(LoadState("QuickSave"));
            //}

            //if (Input.GetKeyDown(KeyCode.F5))
            //{
            //    PlayerPrefs.Save();
            //}

            //if (Input.GetKeyDown(KeyCode.F4))//Reset the autosave
            //{
            //    PlayerPrefs.SetString("AutoSave", "");
            //}


        }

        IEnumerator RandomizeCube()
        {
            System.Random random = new System.Random();
            int? lastSideSelection = null;
            int sideSelection = 0;
            double lastDirection = 1;
            double direction = 0;
            int count = 0;

            for (int i = 0; i < randomLimit; i++)
            {
                sideSelection = random.Next(0, 6);
                direction = (random.NextDouble() >= 0.5)? 1: -1;
                if (lastSideSelection != null)
                {
                    if (lastSideSelection == sideSelection && count <3)
                    {
                        direction = lastDirection;
                        count++;
                    }
                    else if(lastSideSelection == sideSelection)
                    {
                        sideSelection = (sideSelection + 1) % 6;
                        count = 0;
                    }
                }
                lastSideSelection = sideSelection;
                lastDirection = direction;
                yield return sides[sideSelection].StartCoroutine(sides[sideSelection].RotateSide((int)direction));
            }
            //rotateLock = false;

        }

        public IEnumerator LoadState(string saveGame)
        {
            //print(PlayerPrefs.GetString("AutoSave"));
            string Load = PlayerPrefs.GetString(saveGame);
            for (int i = 0; i < Load.Length; i += 2)
            {
                int side = Int32.Parse(Load.Substring(i, 1));
                int direction = Int32.Parse(Load.Substring(i + 1, 1));
                direction = (direction == 1) ? 1 : -1;
                yield return StartCoroutine(sides[side].RotateSide(direction, true));
            }
        }
    }
}