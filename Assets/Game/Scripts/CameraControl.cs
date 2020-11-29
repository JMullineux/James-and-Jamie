using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private float mouseSpeed, xRotation, defaultFOV, aimFOV, aimSpeed;

    private float defaultMouseSpeed;

    public Transform playerBody;
    //public Transform playerCamera;

    // Start is called before the first frame update
    void Start()
    {
        LockCursor();

        this.GetComponent<Camera>().fieldOfView = defaultFOV;

        defaultMouseSpeed = mouseSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSpeed * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
        //playerCamera.Rotate(Vector3.right * -mouseY);

        if (Input.GetButton("Fire2"))
        {
            this.GetComponent<Camera>().fieldOfView = aimFOV;
            mouseSpeed = aimSpeed;

        }
        else
        {
            this.GetComponent<Camera>().fieldOfView = defaultFOV;
            mouseSpeed = defaultMouseSpeed;
        }
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
