using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class AmbiencePlugin
{
    static AmbiencePlugin()
    {
    }

    [MenuItem("GameObject/Ovani Ambience/Ambience Area")]
    private static void CreateAmbienceArea(MenuCommand cmd)
    {
        GameObject areaObj = new GameObject("Ambience Area", typeof(AmbienceArea));
        areaObj.transform.parent = (cmd.context as GameObject)?.transform;
        areaObj.transform.position = GetCamFrontOfPos();
        areaObj.transform.localRotation = Quaternion.identity;
        areaObj.transform.localScale = Vector3.one;

        GameObject areaCol = new GameObject("Collider", typeof(AmbienceCollider), typeof(BoxCollider));
        areaCol.transform.parent = areaObj.transform;
        areaCol.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        areaCol.transform.localScale = Vector3.one;

        Selection.activeObject = areaObj;
    }

    private static Vector3 GetCamFrontOfPos()
        => SceneView.lastActiveSceneView.camera.transform.position + SceneView.lastActiveSceneView.camera.transform.forward * 4;
}
