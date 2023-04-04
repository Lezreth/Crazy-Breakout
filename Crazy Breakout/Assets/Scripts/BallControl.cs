using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class BallControl : MonoBehaviour
{
    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    void Start()
    {

    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    void Update()
    {
        if (transform.position.y < ScreenUtils.ScreenBottom)
        {
            Destroy(gameObject);
        }
    }


    public void SetDirection(Vector2 Direction)
    {
        Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
        float speed = rigidbody2D.velocity.magnitude;
        rigidbody2D.velocity = Direction * speed;
    }
}