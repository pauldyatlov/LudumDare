using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	
	void Start ()
	{
	    City.DestroyedCity += Yolo;
	}

    private void Yolo(int index)
    {
        Debug.Log(index);
    }
}
