using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    class RubixControllerSide : MonoBehaviour
    {
        public GameObject mainCube;
        public GameObject[] adjacentCubesNorth;
        public GameObject[] adjacentCubesWest;
        public GameObject[] adjacentCubesEast;
        public GameObject[] adjacentCubesSouth;
        public GameObject[] adjacentCubeCentres;
        public RubixControllerSide[] adjacentSides;
        public bool isSelected = false;
        public int direction = 1;
        public int[] relativeEdges = new int[4];

        /////// SAVING  
        public string historyName;
        ///////

        void Start()
        {

            adjacentSides = new RubixControllerSide[4];
            for (int i = 0; i < 4; i++)
            {
                adjacentSides[i] = adjacentCubeCentres[i].GetComponent<RubixControllerSide>();
            }
        }

        void Update()
        {
            if (isSelected && !RubixController.rotateLock)
            {
                if (Input.GetButtonDown("Horizontal"))
                {
                    print(Input.GetAxisRaw("Horizontal"));
                    direction = (int)Input.GetAxisRaw("Horizontal");
                    if (direction == 1 || direction == -1)
                    {
                        StartCoroutine(RotateSide(direction));
                    }

                }
            }
        }

        public IEnumerator RotateSide(int direction, bool quickRotate = false)
        {
            yield return new WaitWhile(() => RubixController.rotateLock);
            RubixController.rotateLock = true;
            RubixController.rotationHistory += historyName;
            RubixController.rotationHistory += (direction == 1) ? "1" : "0";
            if (quickRotate)
            {
                for (int j = 0; j < 2; j++)
                {
                    adjacentCubesNorth[j].transform.RotateAround(Vector3.zero, transform.position, direction * 90);
                    adjacentCubesEast[j].transform.RotateAround(Vector3.zero, transform.position, direction * 90);
                    adjacentCubesWest[j].transform.RotateAround(Vector3.zero, transform.position, direction * 90);
                    adjacentCubesSouth[j].transform.RotateAround(Vector3.zero, transform.position, direction * 90);
                }
            }
            else
            { 
                for (int i = 0; i < RubixController.rotationSmoothing; i++)
                {
                    transform.RotateAround(Vector3.zero, transform.position, direction * (90/RubixController.rotationSmoothing));
                    for (int j = 0; j < 2; j++)
                    {
                        adjacentCubesNorth[j].transform.RotateAround(Vector3.zero, transform.position, direction * (90 / RubixController.rotationSmoothing));
                        adjacentCubesWest[j].transform.RotateAround(Vector3.zero, transform.position, direction * (90 / RubixController.rotationSmoothing));
                        adjacentCubesEast[j].transform.RotateAround(Vector3.zero, transform.position, direction * (90 / RubixController.rotationSmoothing));
                        adjacentCubesSouth[j].transform.RotateAround(Vector3.zero, transform.position, direction * (90 / RubixController.rotationSmoothing));
                    }
                    yield return new WaitForSeconds(RubixController.animationSpeed);
                }
            }
            ModifyAdjacencies(direction);
            PlayerPrefs.SetString("AutoSave", RubixController.rotationHistory);
            print(String.Format("RotateSide: {0}", RubixController.rotationHistory));////////////////////////////////////////DEBUG
            RubixController.rotateLock = false;

        }

        private void ModifyAdjacencies(int direction)
        {
            GameObject[] tempCubes;
            if (direction > 0)
            {
                tempCubes = SetSide(adjacentCubesWest);
                adjacentCubesWest = SetSide(adjacentCubesSouth);
                adjacentCubesSouth = SetSide(adjacentCubesEast);
                adjacentCubesEast = SetSide(adjacentCubesNorth);
                adjacentCubesNorth = SetSide(tempCubes);
            }
            else
            {
                tempCubes = SetSide(adjacentCubesEast);
                adjacentCubesEast = SetSide(adjacentCubesSouth);
                adjacentCubesSouth = SetSide(adjacentCubesWest);
                adjacentCubesWest = SetSide(adjacentCubesNorth);
                adjacentCubesNorth = SetSide(tempCubes);
            }
            ModifyExternalAdjacencies();
        }

        private void ModifyExternalAdjacencies()
        {
            GameObject[][] currentSides = { adjacentCubesNorth, adjacentCubesWest, adjacentCubesEast, adjacentCubesSouth };
            for (int i = 0; i < 4; i++)
            {
                switch (relativeEdges[i])
                {
                    case 1://sets the north set of the other side to the reverse of the specified side and changes the west and east sets
                        adjacentSides[i].adjacentCubesNorth = SetSide(currentSides[i][2], currentSides[i][1], currentSides[i][0]);
                        adjacentSides[i].adjacentCubesWest[2] = currentSides[i][2];
                        adjacentSides[i].adjacentCubesEast[0] = currentSides[i][0];
                        break;

                    case 2://sets the west set of the other side to the reverse of the specified side and changes the north and south sets.
                        adjacentSides[i].adjacentCubesWest = SetSide(currentSides[i][2], currentSides[i][1], currentSides[i][0]);
                        adjacentSides[i].adjacentCubesNorth[0] = currentSides[i][0];
                        adjacentSides[i].adjacentCubesSouth[2] = currentSides[i][2];
                        break;

                    case 3://sets the east set of the other side to the reverse blah blah
                        adjacentSides[i].adjacentCubesEast = SetSide(currentSides[i][2], currentSides[i][1], currentSides[i][0]);
                        adjacentSides[i].adjacentCubesNorth[2] = currentSides[i][2];
                        adjacentSides[i].adjacentCubesSouth[0] = currentSides[i][0];
                        break;

                    case 4://blah blah blah
                        adjacentSides[i].adjacentCubesSouth = SetSide(currentSides[i][2], currentSides[i][1], currentSides[i][0]);
                        adjacentSides[i].adjacentCubesWest[0] = currentSides[i][0];
                        adjacentSides[i].adjacentCubesEast[2] = currentSides[i][2];
                        break;
                }
            }
        }

        public GameObject[] SetSide(GameObject[] newSide)
        {
            return new GameObject[3] { newSide[0], newSide[1], newSide[2] };
        }

        public GameObject[] SetSide(GameObject newSide1, GameObject newSide2, GameObject newSide3)
        {
            return new GameObject[3] { newSide1, newSide2, newSide3 };
        }
    }
}