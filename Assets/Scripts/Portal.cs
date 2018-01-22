using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

//https://www.youtube.com/playlist?list=PLKIKuXdn4ZMhwJmPnYI0e7Ixv94ZFPvEP

public class Portal : MonoBehaviour
{
    public Transform device;
    bool wasInFront;
    bool inOtherWorld;
    bool isColliding;

    void Start()
    {
        SetMaterials(false);
    }

    void SetMaterials(bool fullRender)
    {
        var stencilTest = fullRender ? CompareFunction.NotEqual : CompareFunction.Equal;
        Shader.SetGlobalInt("_StencilTest", (int)stencilTest);
    }

    bool GetIsInFront()
    {
        Vector3 worldPos = device.position + device.forward * Camera.main.nearClipPlane;
        Vector3 pos = transform.InverseTransformPoint(worldPos);

        return pos.z >= 0 ? true : false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform != device)
            return;

        wasInFront = GetIsInFront();
        isColliding = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform != device)
            return;

        isColliding = false;
    }

    void WhileCameraColliding()
    {
        if (!isColliding)
            return;

        bool isInFront = GetIsInFront();

        if ((isInFront && !wasInFront) || (wasInFront && !isInFront))
        {
            inOtherWorld = !inOtherWorld;
            SetMaterials(inOtherWorld);
        }

        wasInFront = isInFront;
    }

    void OnDestroy()
    {
        SetMaterials(true);
    }

    void Update()
    {
        WhileCameraColliding();
    }
}
