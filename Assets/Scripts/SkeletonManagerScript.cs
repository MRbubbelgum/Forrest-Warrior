using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonManagerScript : MonoBehaviour
{
    public GameObject skeletonOriginal;
    public Transform parent;
    public Vector3 newPosition;


    // Start is called before the first frame update
    void Start()
    {
       //  CreateSkeletonClone();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void CreateSkeletonClone()
    {
        GameObject skeletonManager = Instantiate(skeletonOriginal);
    }
    
}
