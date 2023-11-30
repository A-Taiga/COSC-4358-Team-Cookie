using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject exitScript;
    public Canvas shopCanvas;
    private bool browsing = false;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            browsing = true;
            exitScript.GetComponent<exitScript>().set_hasExited(false);
            shopCanvas.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(browsing && exitScript.GetComponent<exitScript>().get_hasExited() == false)
            {
                exitScript.GetComponent<exitScript>().exit();
                browsing = false;
                shopCanvas.gameObject.SetActive(false);
                exitScript.GetComponent<exitScript>().set_hasExited(true);

            }
            else if(browsing && exitScript.GetComponent<exitScript>().get_hasExited() == true)
            {
                browsing = false;
            }
        }
    }
}
