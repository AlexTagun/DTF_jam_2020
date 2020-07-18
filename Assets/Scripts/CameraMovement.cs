﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _speed = 0f;
    private bool canMove = false;
    private RaycastHit2D hit;
    private Vector2 mousePos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetMouseButtonDown(0))
        {
            canMove = true;
            mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.Raycast(mousePos,Vector3.forward);
            if (hit.collider != null)
            {
                if (hit.collider.tag == "Child")
                {
                    canMove = false;
                }
            }
            Debug.Log(canMove);
        }
       if (Input.GetMouseButton(0))
        {
            if(canMove)
            {
                Vector2 newMousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
                Vector2 deltaMousePosition = newMousePosition - mousePos;
                //transform.position -= deltaMousePosition * _speed;
                _rigidbody2D.velocity -= deltaMousePosition * _speed; 
            }
        }
    }
    
}