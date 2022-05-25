using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    [SerializeField]
    private float _speed = 5f;

    private bool _isGoingDown = true;


    public void AdjustSpeed(float diff)
    {
        _speed += diff;
    }

    public void SetDirectionUp()
    {
        _isGoingDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isGoingDown)
        {
            transform.position -= new Vector3(0, _speed * Time.deltaTime);
            if (transform.position.y < -6)
            {
                Destroy(gameObject);
            }
        }
        else
        {

            transform.position += new Vector3(0, _speed * Time.deltaTime);
            if (transform.position.y > 6)
            {
                Destroy(gameObject);
            }
        }

    }
}
