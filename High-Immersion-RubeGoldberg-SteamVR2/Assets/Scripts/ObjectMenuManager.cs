using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ObjectMenuManager : MonoBehaviour {

    private Dictionary<int, int> spawnCount;

    private bool isSelecting;

    private bool isSpawning;

    private SteamVR_Input_Sources inputSource;

    [Tooltip("List of game objects for menu")]
    public List<GameObject> objectList;

    [Tooltip("Prefabs related to the spawnable objects")]
    public List<GameObject> objectPrefabList;

    [Tooltip("Maximum number of spawnable objects for each type")]
    public List<int> maxSpawnCount;

    [Tooltip("Index of the object currently selected")]                                             
    public int currentObject = 0;

    [Tooltip("Joystick tolerance when selecting menu items")]     
    public float joystickTolerance = 0.7f;

    [Tooltip("Joystick tolerance for resetting the menu")]     
    public float menuResetTolerance = 0.3f;

    void Start() {
        spawnCount = new Dictionary<int, int>();

        for(int i=0; i < objectList.Count; i++) {
            spawnCount[i] = 0;
        }

        inputSource = SteamVR_Input_Sources.RightHand;
    }

    public void MenuLeft()
    {
        if (AreObjectsAvailable())
            SelectPreviousObject();
    }

    private void SelectPreviousObject()
    {
        do {
            currentObject--;
            if(currentObject < 0)
            {
                currentObject = objectList.Count - 1;
            }           
        } while(!isObjectValid(currentObject));
    }

    public void MenuRight()
    {
        if (AreObjectsAvailable())
            SelectNextObject();
    }

    private void SelectNextObject()
    {
        do {
            currentObject++;
            if (currentObject > objectList.Count - 1)
            {
                currentObject = 0;
            }
        } while(!isObjectValid(currentObject));
    }

    private bool isObjectValid(int index) {
        return maxSpawnCount[index] != 0 && spawnCount[index] < maxSpawnCount[index];
    }

    public void SpawnCurrentObject()
    {
        if (!isSpawning && isObjectValid(currentObject)) {
            isSpawning = true;

            Instantiate(objectPrefabList[currentObject], 
                objectList[currentObject].transform.position, 
                objectList[currentObject].transform.rotation);           

            spawnCount[currentObject]++;

            if (!AreObjectsAvailable()) {
                DisableMenu();   
                return;
            }

            if (!isObjectValid(currentObject)) {
                StartSelection();
                SelectNextObject();
                return;
            }
        }
    }
	
	void Update () {
        HandleMenu(inputSource);

        if (gameObject.activeSelf) {
            if (SteamVR_Actions.default_SpawnObject.GetState(inputSource)) {
                SpawnCurrentObject();
                return;
            } else {
                isSpawning = false;
            }

            if (Mathf.Abs(SteamVR_Actions.default_Joystick.GetAxis(inputSource).x) < menuResetTolerance) {
                isSelecting = false;
                HandleMenu(inputSource);
                return;
            }

            if (SteamVR_Actions.default_Joystick.GetAxis(inputSource).x < -joystickTolerance && !isSelecting) {
                StartSelection();
                MenuLeft();
                return;
            }

            if (SteamVR_Actions.default_Joystick.GetAxis(inputSource).x > joystickTolerance && !isSelecting) {
                objectList[currentObject].SetActive(false);
                isSelecting = true;
                MenuRight();
                return;
            }
        }
	}

    private void StartSelection() {
        objectList[currentObject].SetActive(false);
        isSelecting = true;        
    }

    private bool AreObjectsAvailable() {
        var isObjectAvailable = false;

        int i=0;
        while(!isObjectAvailable && i < spawnCount.Count) {
            isObjectAvailable = isObjectValid(i);
            i++;
        }

        return isObjectAvailable;
    }

    private void HandleMenu(SteamVR_Input_Sources inputSource) {
        if (isSelecting)
            return;

        if (!AreObjectsAvailable()) {
            DisableMenu();
            return;
        }

        if (SteamVR_Actions.default_ShowMenu.GetState(inputSource)) {
            EnableMenu();
        } else {
            DisableMenu();
        }
    }

    private void DisableMenu()
    {
        objectList[currentObject].SetActive(false);
        isSelecting = false;
        isSpawning = false;
    }

    private void EnableMenu()
    {
        objectList[currentObject].SetActive(true);
    }
}
