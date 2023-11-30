using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Map : MonoBehaviour
{
    // Start is called before the first frame update


    public (string, int, int)[] playerCoords;

    public GameObject sceneName;

    public bool isShowing = false;
    void Start()
    {
        // playerCoords[0] = {"Village", -91, 284};
        // playerCoords[1] = {"North Beach", 142, 362};
        // playerCoords[2] = {"Castle", 142, 362};

        // playerCoords[7] = {"wetlands", 142, 362};
        // playerCoords[11] = {"northBeach", 142, 362};


        Debug.Log("hello new scene !");
        // print("THIS IS FROM MAP: " + scene.name);




    }

    // Update is called once per frame
    void Update()
    {

    }
    public void showButton()
    {
        if(isShowing)
        {
            isShowing = false;
            gameObject.SetActive(false);
        }
        else if(!isShowing)
        {
            isShowing = true;
            gameObject.SetActive(true);
        }
    }
}
