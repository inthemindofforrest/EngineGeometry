using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Pulse : MonoBehaviour
{
    float VignetteIntensity = 0;
    PostProcessVolume m_Volume;
    Vignette m_Vignette;

    void Start()
    {
        m_Vignette = ScriptableObject.CreateInstance<Vignette>();
        m_Vignette.enabled.Override(true);
        m_Vignette.intensity.Override(1f);

        m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, m_Vignette);
    }

    void Update()
    {
        m_Vignette.intensity.value = VignetteIntensity/*Mathf.Sin(Time.realtimeSinceStartup)*/;
        ModifyIntensity();
    }

    void OnDestroy()
    {
        RuntimeUtilities.DestroyVolume(m_Volume, true, true);
    }

    void ModifyIntensity()
    {
        VignetteIntensity += ((Input.GetKey(KeyCode.UpArrow)) ? 1 : (Input.GetKey(KeyCode.DownArrow)) ? -1 : 0) * Time.deltaTime;
        VignetteIntensity = Mathf.Clamp(VignetteIntensity, 0, 1);
    }
}
