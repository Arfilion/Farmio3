using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;

    public float speedRotation, minRot, maxRot;
    float _rotX;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        //Rotacion
        float x = Input.GetAxis("Mouse X");
        player.Rotate(Vector3.up * x * speedRotation);

        //Rotacion camara
        float y = Input.GetAxis("Mouse Y");

        _rotX += -y * speedRotation;
        _rotX = Mathf.Clamp(_rotX, minRot, maxRot);
        transform.localEulerAngles = new Vector3(_rotX, 0, 0);

    }
}
