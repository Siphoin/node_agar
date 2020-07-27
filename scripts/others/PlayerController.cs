using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Speed")]
    private NodeView view;

    Rigidbody2D rigidBody2d;
    private void Start()
    {
        view = GetComponent<NodeView>();
    }
    public float Speed = 0.05f;

    public Rigidbody2D RigidBody { get => rigidBody2d; }

    void Update()
    {
       if (view.myObject())
        {
MoveMent();
        }
        
    }

    private void Awake()
    {
        rigidBody2d = GetComponent<Rigidbody2D>();
    }

    private void MoveMent()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        transform.Translate(new Vector2(h, v) * Speed * Time.deltaTime);

        
    }

    private void FixedUpdate()
    {
        rigidBody2d.velocity = Vector2.zero;
    }
}
