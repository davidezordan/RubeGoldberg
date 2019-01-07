using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class LoadScene : MonoBehaviour {
    public string SceneToLoad = "_Scenes/Level 1";

    void Start (){
        StartCoroutine(LoadSceneAsync());
    }
    
    IEnumerator LoadSceneAsync() {
        yield return new WaitForSeconds(4);
               
        SteamVR_LoadLevel.Begin(SceneToLoad);
    }
}
