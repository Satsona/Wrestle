using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class S_ForceVCam : MonoBehaviour
{
    public CinemachineVirtualCamera myVCam;

    void Start()
    {
        foreach (var brain in FindObjectsOfType<CinemachineBrain>())
        {
            // Only allow this brain to see this camera's VCam
            if (brain.gameObject == this.gameObject && myVCam != null)
            {
                myVCam.Priority = 100;
            }
            else
            {
                myVCam.Priority = 0;
            }
        }
    }
}
