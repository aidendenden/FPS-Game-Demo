using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosDoor : MonoBehaviour
{
    public int DoorNum;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Player")
        {
            Debug.Log("initx");
            other.gameObject.GetComponent<PlayerMove>().nowPos = DoorNum;
            other.gameObject.GetComponent<PlayerMove>().ReSetPos();
        }
    }
}
