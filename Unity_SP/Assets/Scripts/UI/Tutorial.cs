using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial : MonoBehaviour
{
    [SerializeField]
    private PlayerData playerData;
    [SerializeField]  
    private GameObject wood;
    [SerializeField]  
    private GameObject food;
    [SerializeField]  
    private GameObject fireplace;
    [SerializeField]  
    private GameObject lockObj;
    [SerializeField]
    private Fireplace fp;
    [SerializeField]
    private Checkpoint checkpoint;
    [SerializeField]
    private TMP_Text text;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (wood != null)
        {
            Vector3 temp = wood.transform.position;
            temp.y += 0.5f;
            transform.position = temp;

            text.text = "Grab items by walking to them.";
        }
        else if (!fp.GetLit())
        {
            Vector3 temp = fireplace.transform.position;
            temp.y += 0.5f;
            transform.position = temp;

            text.text = "Light the fireplace by [E] adding wood";
        }
        else if (!playerData.HasPlayerSaved())
        {
            Vector3 temp = checkpoint.gameObject.transform.position;
            temp.y += 0.5f;
            transform.position = temp;

            text.text = "Save game by [E] interacting with this bed.";
        }
        else if (food!=null)
        {
            Vector3 temp = food.transform.position;
            temp.y += 0.5f;
            transform.position = temp;

            text.text = "Eat food by [Tab] open inventory and click to use item";
        }

        else if (lockObj!=null)
        {
            Vector3 temp = lockObj.transform.position;
            temp.y += 0.5f;
            transform.position = temp;

            text.text = "Find a key to unlock this door.";
        }
        else
        {
            playerData.Save();
            Destroy(gameObject);
        }
    }
}
