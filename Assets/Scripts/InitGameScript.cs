using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitGameScript : MonoBehaviour {

    [SerializeField] private List<Object> sceneObj_list;

    [SerializeField] private Object activeScene;
 
    private void Awake() {
        SceneManager.LoadScene(activeScene.name, LoadSceneMode.Single);

        foreach(Object scene in sceneObj_list) { 
            SceneManager.LoadScene(scene.name, LoadSceneMode.Additive);
        }

    }

    private void Start() {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(activeScene.name));
    }
}
