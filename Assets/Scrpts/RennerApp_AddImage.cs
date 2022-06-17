using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.TextCore;

public class RennerApp_AddImage : MonoBehaviour
{
    [SerializeField] GameObject imagePrefab;
    [SerializeField] Transform imageContainer;

    [SerializeField] private TextAsset names;
    [SerializeField] private TextAsset comments;

    string[] _names;
    string[] _comments;
    [SerializeField] Sprite[] postImages;
    [SerializeField] VideoClip postVideos;
    float a = 0;
    // Start is called before the first frame update

    string[] loadFromFile(TextAsset file)
    {
        var content = file.text;
        var AllWords = content.Split("\n");
        return AllWords;
    }

    void Start()
    {
        _names = loadFromFile(names);
        _comments = loadFromFile(comments);

        SpawnNew(); 
        SpawnNew(); 
        SpawnNew(); 
        SpawnNew();
        SpawnNew();
        SpawnNew();
        SpawnNew();
        SpawnNew();
        SpawnNew();
        SpawnNew();
        SpawnNew();
        SpawnNew();
        SpawnNew();

    }

    public void SpawnNew()
    {
        a += 1;

        GameObject prefab = Instantiate(imagePrefab, imageContainer.transform);
        SetSpriteSize setSpr = prefab.GetComponentInChildren<SetSpriteSize>();
        setSpr.SetSprite(postImages[Random.Range(0, postImages.Length)]);
        ImageGroup imgGroup = prefab.GetComponent<ImageGroup>();
        imgGroup.SetComment(_names[Random.Range(0, _names.Length)], _comments[Random.Range(0, _comments.Length)]);

        if (imageContainer.transform.childCount > 1)
        {
            prefab.transform.position = imageContainer.transform.GetChild(imageContainer.transform.childCount - 2).GetComponent<ImageGroup>().bottomTransform.position;
            prefab.name = a.ToString() + "," + prefab.transform.GetSiblingIndex().ToString();

        }
        else
        {
            prefab.transform.localPosition = Vector3.zero;
            prefab.name = a.ToString() +","+ prefab.transform.GetSiblingIndex().ToString();
        }
    }
}
