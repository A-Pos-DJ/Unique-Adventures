using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
public enum DebugMode
{
    KeyPress
};


public class DebugHelper : MonoBehaviour
{
    public bool keyPressed = false;

    //class constructor that sets the player and enemy characters for this combat
    public DebugHelper(DebugMode mode)
    {
        GameObject debugObject = Instantiate(new GameObject);

        switch (mode)
        {
            case DebugMode.KeyPress:
                //attach the coroutine to the gameobject that is using the debug helper
                debugObject.AddComponent<MonoBehaviour>().StartCoroutine("WaitForKeypressRoutine");


                return;
            default:
                Debug.Log("Error: No debug mode has been selected");
                Destroy(debugObject);
                return;
        }
    }


    //waits for a specified key to be pressed before continuing with the program
    public IEnumerator WaitForKeypressRoutine()
    {
        bool keyPressed = false;

        Debug.Log("Waiting for any key to be pressed");
        while (!keyPressed)
        {
            if (Input.anyKey)
            {
                print("a key has been pressed");
                keyPressed = true;
                yield return true;
            }
        }

        Destroy(gameObject);
        yield return false;
    }
}
*/
