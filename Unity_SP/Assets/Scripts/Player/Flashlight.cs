using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public Vector3 screenPosition;
    public Vector3 worldPosition;


    // Update is called once per frame
    void Update()
    {
        screenPosition = Input.mousePosition;
        screenPosition.z = 0;
        worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        worldPosition.z = 0;
        float angle = Mathf.Atan2(worldPosition.y - transform.position.y, worldPosition.x - transform.position.x) * Mathf.Rad2Deg - 90;
        //if(angle < 0)
        //{
        //    angle += 360f;
        //}
        //Quaternion target = Quaternion.AngleAxis(angle, Vector3.forward);
        //transform.rotation = target;

        float diff = Mathf.Abs(angle - transform.eulerAngles.z);
        if(diff > 360)
        {
            diff -= 360f;
        }
        if (diff > 5)
        {
            if (diff > 180)
            {
                transform.Rotate(new Vector3(0f, 0f, 1f), 2);
            }
            else
            {
                
                if (transform.eulerAngles.z <= 90)
                {
                    diff = angle - transform.eulerAngles.z;
                    Debug.Log(diff);
                    if(diff < 0)
                        transform.Rotate(new Vector3(0f, 0f, 1f), -2);
                    else
                        transform.Rotate(new Vector3(0f, 0f, 1f), 2);

                }
                else
                    transform.Rotate(new Vector3(0f, 0f, 1f), -2);
            }
        }
    }
}
