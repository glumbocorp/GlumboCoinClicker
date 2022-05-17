using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RennerApp_AddImage : MonoBehaviour
{
    [SerializeField] GameObject imagePrefab;
    [SerializeField] float distanceBetweenImages = 0.1f;
    [SerializeField] Transform imageContainer;
    float a = 0;
    // Start is called before the first frame update
    void Start()
    {
        SpawnNew(); 
        SpawnNew(); 
        SpawnNew(); 
        SpawnNew();
    }

    public void SpawnNew()
    {
        a += 1;
        if (imageContainer.transform.childCount > 0)
        {
            GameObject prefab = Instantiate(imagePrefab, imageContainer.transform);
            prefab.GetComponentInChildren<SetSpriteSize>().ScaleSprite();
            prefab.GetComponent<ImageGroup>().ResizeBottom();

            prefab.transform.position = imageContainer.transform.GetChild(imageContainer.transform.childCount - 2).GetComponent<ImageGroup>().bottomTransform.position;
            Debug.Log(imageContainer.transform.GetChild(imageContainer.transform.childCount - 2).name);
            Debug.Log("index: " + (imageContainer.transform.childCount - 2).ToString());
            //Debug.Log(imageContainer.transform.GetChild(transform.childCount - 1).GetChild(0).position.ToString());
            prefab.name = a.ToString() + "," + prefab.transform.GetSiblingIndex().ToString();

        }
        else
        {
            GameObject prefab = Instantiate(imagePrefab, imageContainer.transform);
            prefab.GetComponentInChildren<SetSpriteSize>().ScaleSprite();
            prefab.GetComponent<ImageGroup>().ResizeBottom();
            prefab.transform.localPosition = Vector3.zero;
            prefab.name = a.ToString() +","+ prefab.transform.GetSiblingIndex().ToString();
        }
    }
}
