using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

[ExecuteAlways]
public class AmbienceArea : MonoBehaviour
{
    public AudioMixerGroup MyGroup;
    public AudioMixerSnapshot MySnap;
    public AmbienceArea ParentArea;
    public List<AmbienceCollider> AmbienceColliders = new List<AmbienceCollider>();
    public float TransitionTime = .5f;

    // Start is called before the first frame update
    void Start()
    {
        if (transform.parent)
            ParentArea = transform.parent.GetComponent<AmbienceArea>();
        for (int i = 0; i < transform.childCount; i++)
        {
            AmbienceCollider col = transform.GetChild(i).GetComponent<AmbienceCollider>();
            if (col)
                AmbienceColliders.Add(col);
        }
    }

#if UNITY_EDITOR
    private AudioMixer mixer;
    private string lastName;
    private void Update()
    {
        if (!mixer)
            mixer = GetSceneMixer();

        // Group Ensurement
        if (MyGroup == null)
        {
            MyGroup = GetGroups(mixer).FirstOrDefault(g => g.name == gameObject.name);
            if (MyGroup == null)
                MyGroup = CreateGroup(mixer, gameObject.name, false);
        }
        if (lastName != gameObject.name)
        {
            MyGroup.name = gameObject.name;
            AssetDatabase.SaveAssets();
        }
        else if (MyGroup.name != gameObject.name)
        {
            MySnap.name = gameObject.name = MyGroup.name;
            AssetDatabase.SaveAssets();
        }

        // Snap Ensurement
        if (MySnap == null)
        {   
            MySnap = GetSnaps(mixer).FirstOrDefault(g => g.name == gameObject.name);
            if (MySnap == null)
                MySnap = CreateSnap(mixer, gameObject.name, false);
        }
        if (lastName != gameObject.name)
        {
            MySnap.name = gameObject.name;
            AssetDatabase.SaveAssets();
        }
        else if (MySnap.name != gameObject.name)
        {
            MyGroup.name = gameObject.name = MySnap.name;
            AssetDatabase.SaveAssets();
        }


        lastName = gameObject.name;
    }
    
    private AudioMixer GetSceneMixer()
    {
        var existingMixers = AssetDatabase.FindAssets(gameObject.scene.name + "Ambience t:AudioMixerController")
                                          .Select(guid => AssetDatabase.GUIDToAssetPath(guid));

        if (existingMixers.Count() == 0)
            return CreateMixer(gameObject.scene.path.Replace(".unity", "Ambience.mixer"));
        else return AssetDatabase.LoadAssetAtPath<AudioMixer>(existingMixers.First());
    }
    private AudioMixer CreateMixer(string targetPath)
    {
        string sourcePath = Path.Combine(Application.dataPath, "OvaniAmbiencePlugin", "MixerTemplate.mixer.disabled");
        string targPrefixed = Path.Combine(Path.GetDirectoryName(targetPath), "MixerTemplate.mixer");
        File.Copy(sourcePath, Path.Combine(Application.dataPath, "..", targPrefixed));
        AssetDatabase.ImportAsset(targPrefixed);
        AssetDatabase.RenameAsset(targPrefixed, Path.GetFileNameWithoutExtension(targetPath));
        return AssetDatabase.LoadAssetAtPath<AudioMixer>(targetPath);
    }
    private List<AudioMixerGroup> GetGroups(AudioMixer mixer)
    {
        MethodInfo method = mixer.GetType().GetMethod("GetAllAudioGroupsSlow", (BindingFlags)(-1));
        object ret = method.Invoke(mixer, new object[] { });
        return ((IList)ret).OfType<AudioMixerGroup>().ToList();
    }
    private AudioMixerGroup CreateGroup(AudioMixer mixer, string name, bool storeUndo)
    {
        AudioMixerGroup group = (AudioMixerGroup)mixer.GetType().GetMethod("CreateNewGroup", (BindingFlags)(-1)).Invoke(mixer, new object[] { name, storeUndo });

        AudioMixerGroup master = (AudioMixerGroup)mixer.GetType().GetProperty("masterGroup", (BindingFlags)(-1)).GetValue(mixer);

        SerializedObject sObj = new SerializedObject(master);
        SerializedProperty childProp = sObj.FindProperty("m_Children");
        childProp.InsertArrayElementAtIndex(0);
        childProp.GetArrayElementAtIndex(0).objectReferenceValue = group;
        sObj.ApplyModifiedProperties();

        AssetDatabase.SaveAssets();
        return group;
    }
    private List<AudioMixerSnapshot> GetSnaps(AudioMixer mixer)
    {
        MethodInfo method = mixer.GetType().GetMethod("get_snapshots", (BindingFlags)(-1));
        object ret = method.Invoke(mixer, new object[] { });
        return ((IList)ret).OfType<AudioMixerSnapshot>().ToList();
    }
    private AudioMixerSnapshot CreateSnap(AudioMixer mixer, string name, bool storeUndo)
    {
        AudioMixerSnapshot startSnap = (AudioMixerSnapshot)mixer.GetType().GetProperty("startSnapshot", (BindingFlags)(-1)).GetValue(mixer);
        var targSnapProp = mixer.GetType().GetProperty("TargetSnapshot", (BindingFlags)(-1));
        object startTargProp = targSnapProp.GetValue(mixer);
        targSnapProp.SetValue(mixer, startSnap);
        string startSnapName = startSnap.name;
        startSnap.name = name;

        // create the new snapshot
        mixer.GetType().GetMethod("CloneNewSnapshotFromTarget", (BindingFlags)(-1)).Invoke(mixer, new object[] { false });

        startSnap.name = startSnapName;
        targSnapProp.SetValue(mixer, startTargProp);


        SerializedObject sObj = new SerializedObject(mixer);
        SerializedProperty snapProp = sObj.FindProperty("m_Snapshots");
        for (int i = 0; i < snapProp.arraySize; i++)
        {
            AudioMixerSnapshot curSnap = snapProp.GetArrayElementAtIndex(i).objectReferenceValue as AudioMixerSnapshot;
            if (curSnap.name == name + " - Copy")
            {
                curSnap.name = name;
                AssetDatabase.SaveAssets();
                return curSnap;
            }
        }


        throw new Exception("");
    }
    [MenuItem("CONTEXT/AmbienceArea/Auto Configure Audio Sources")]
    private static void AutoSetupAudioSources(MenuCommand cmd)
    {
        var audios = SceneManager.GetActiveScene().GetRootGameObjects().SelectMany(g => g.GetComponentsInChildren<AudioSource>(true));
        
        foreach (AudioSource audio in audios)
        {
            Ray scanner = new Ray(audio.transform.position + (Vector3.up * 9999), Vector3.down);

            Collider found = null;
            List<Collider> pendingFixes = new List<Collider>();
            while (Physics.Raycast(scanner, out RaycastHit hit) && hit.distance <= 9999)
            {
                pendingFixes.Add(hit.collider);
                hit.collider.enabled = false;
                if (hit.collider.GetComponent<AmbienceCollider>())
                    found = hit.collider;
            }

            if (found != null)
            {
                found.enabled = true;
                scanner.origin = audio.transform.position;
                scanner.direction = Vector3.up;
                if (Physics.Raycast(scanner, out RaycastHit hit2) && hit2.collider == found)
                    found = null;
                else found.enabled = false;
            }

            foreach (Collider col in pendingFixes)
                col.enabled = true;

            if (found == null)
                continue;

            AmbienceArea audiosArea = found.transform.parent.GetComponent<AmbienceArea>();
            if (audiosArea == null)
                continue;

            audio.outputAudioMixerGroup = audiosArea.MyGroup;
        }
    }
#endif

    public bool PlayerInside;

    public void OnPlayerColEntry()
    {
        if (!PlayerInside)
            OnPlayerEnter();
    }
    public void OnPlayerColExit()
    {
        if (!AmbienceColliders.Any(col => col.PlayerInside))
            OnPlayerExit();
    }

    public void OnPlayerEnter()
    {
        PlayerInside = true;
        ActivateAmbience();
    }
    public void OnPlayerExit()
    {
        PlayerInside = false;
        if (ParentArea)
            ParentArea.ActivateAmbience();
    }

    private void ActivateAmbience()
    {
        
        MySnap.TransitionTo(TransitionTime);
    }
}
