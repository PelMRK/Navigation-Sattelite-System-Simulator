using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detecting : MonoBehaviour
{
    public int number = 0;
    List<Collider> triggerList = new List<Collider>();    

    private void OnTriggerEnter(Collider other)
    {
        if (!triggerList.Contains(other))
        {
            triggerList.Add(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (triggerList.Contains(other))
        {
            triggerList.Remove(other);
        }
    }

    void FixedUpdate()
    {
        number = triggerList.Count; // this gives the number of visible satellites
        triggerList.Clear();
    }
}
