using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingSquare : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;

    private void Update()
    {
        transform.Rotate(rotationSpeed * new Vector3(0f, 0f, 1f) * Time.deltaTime);
    }
}
