using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform parentTransform;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        this.transform.position = parentTransform.position;
        this.transform.rotation = new quaternion(parentTransform.rotation.x, parentTransform.rotation.y, parentTransform.rotation.z, parentTransform.rotation.w);
    }

}
