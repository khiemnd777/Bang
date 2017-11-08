using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    public LayerMask movementMask;

    Camera cam;
	PlayerMotor motor;

    void Start()
    {
        cam = Camera.main;
		motor = GetComponent<PlayerMotor>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            var ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f, movementMask))
            {
                motor.MoveToPoint(hit.point);
            }
        }
		if (Input.GetMouseButtonDown(1))
        {
            var ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f))
            {
                
            }
        }
    }
}
