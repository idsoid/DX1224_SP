using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingSquare : MonoBehaviour
{
    [SerializeField] private GameObject delayedBullet;
    [SerializeField] private GameObject target;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float initialDelay;
    [SerializeField] private float subsequentDelay;

    private void Start()
    {
        // Spawn in bullets around player
        GameObject newBullet;
        DelayedBullet newBulletScript;
        float spawnX;
        float spawnY;

        for (int i = 0; i < 8; i++)
        {
            switch(i)
            {
                case 0:
                    spawnX = transform.position.x - 3.5f;
                    spawnY = transform.position.y - 3.5f;
                    break;
                case 1:
                    spawnX = transform.position.x;
                    spawnY = transform.position.y - 3.5f;
                    break;
                case 2:
                    spawnX = transform.position.x + 3.5f;
                    spawnY = transform.position.y - 3.5f;
                    break;
                case 3:
                    spawnX = transform.position.x + 3.5f;
                    spawnY = transform.position.y;
                    break;
                case 4:
                    spawnX = transform.position.x + 3.5f;
                    spawnY = transform.position.y + 3.5f;
                    break;
                case 5:
                    spawnX = transform.position.x;
                    spawnY = transform.position.y + 3.5f;
                    break;
                case 6:
                    spawnX = transform.position.x - 3.5f;
                    spawnY = transform.position.y + 3.5f;
                    break;
                case 7:
                    spawnX = transform.position.x - 3.5f;
                    spawnY = transform.position.y;
                    break;
                default:
                    spawnX = 0;
                    spawnY = 0;
                    Debug.Log("Error while setting bullet position");
                    break;
            }

            newBullet = Instantiate(delayedBullet, new Vector2(spawnX, spawnY), Quaternion.identity);
            newBullet.transform.parent = transform;
            newBulletScript = newBullet.GetComponent<DelayedBullet>();
            newBulletScript.SetTarget(target);
            newBulletScript.SetTimeToActivate(initialDelay + (subsequentDelay * i));
        }
    }

    private void Update()
    {
        transform.Rotate(rotationSpeed * new Vector3(0f, 0f, 1f) * Time.deltaTime);
    }

    public void SetTarget(GameObject newTarget)
    {
        target = newTarget;
    }
}
