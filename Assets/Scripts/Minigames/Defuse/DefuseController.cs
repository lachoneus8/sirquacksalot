using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class DefuseController : MonoBehaviour
{
    public MinigameController controller;
    public Transform playerPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.running)
        {
            var mousePoint=Input.mousePosition;
            var lookRay=Camera.main.ScreenPointToRay(mousePoint,Camera.MonoOrStereoscopicEye.Mono);
            RaycastHit lookHit;
            Physics.Raycast(lookRay, out lookHit);
            var lookPos = lookHit.point;
            lookPos.y=playerPos.transform.position.y;
            playerPos.LookAt(lookPos);
        }
    }
}
