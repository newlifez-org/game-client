using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewLifeZ.MainMenu
{
    public class MM_DragView : MonoBehaviour
    {
        Vector3 mousePos;
        [SerializeField] float rotateSpeed = 40;
        [Range(0, 50)]
        [SerializeField] float autoRotateSpeed;
        Vector3 xRot;
        private void OnMouseDrag()
        {
            Debug.Log("Mouse Drag");
            xRot = Input.GetAxis("Mouse X") * rotateSpeed * Vector3.down;
            transform.Rotate(xRot);
        }
        
        void Update()
        {
            transform.Rotate(Vector3.up * autoRotateSpeed * Time.deltaTime);
        }
    }
}

