using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class BleedingOut : MonoBehaviour
{
    public float Health = 100;
    public int SightDistortionLimit = 50;

    public int DistortionMultiplier = 25;

    PostProcessVolume Volume;
    ColorGrading ColorG;
    DepthOfField BlurOfVision;
    LensDistortion LensD;

    Vignette TVig;

    void Start()
    {
        #region BlurVision
        BlurOfVision = ScriptableObject.CreateInstance<DepthOfField>();
        BlurOfVision.enabled.Override(true);
        BlurOfVision.focusDistance.overrideState = true;
        #endregion

        #region LensD
        LensD = ScriptableObject.CreateInstance<LensDistortion>();
        LensD.enabled.Override(true);
        LensD.intensity.overrideState = true;
        #endregion

        #region ColorG
        ColorG = ScriptableObject.CreateInstance<ColorGrading>();
        ColorG.enabled.Override(true);
        ColorG.saturation.overrideState = true;
        #endregion

        #region TVig
        TVig = ScriptableObject.CreateInstance<Vignette>();
        TVig.enabled.Override(true);
        TVig.intensity.overrideState = true;
        #endregion

        Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, BlurOfVision, LensD, ColorG, TVig);
    }

    void Update()
    {
        BlurFunc();
        LensFunc();
        ColorFunc();
        VigFunc();

        Health = Mathf.Clamp(Health, 0, 100);

        ModifyIntensity();
    }
    void OnDestroy()
    {
        RuntimeUtilities.DestroyVolume(Volume, true, true);
    }

    void ModifyIntensity()
    {
        Health += ((Input.GetKey(KeyCode.UpArrow)) ? 30 : (Input.GetKey(KeyCode.DownArrow)) ? -30 : 0) * Time.deltaTime;
        Health = Mathf.Clamp(Health, 0, 100);
    }

    void BlurFunc()
    {
        BlurOfVision.focusDistance.value = 5.0f * ((float)Health / 100);
    }
    void LensFunc()
    {
        LensD.intensity.value = Mathf.Lerp(LensD.intensity.value, (Health <= SightDistortionLimit) ? 0 - ((Mathf.Sin(Time.realtimeSinceStartup) * 
            DistortionMultiplier)) * 2 * ((100 - Health) / 100) : 0, .75f * Time.deltaTime);
    }
    void ColorFunc()
    {
        ColorG.saturation.value = Mathf.Clamp((Health - 100), -100, 0);
    }
    void VigFunc()
    {
        TVig.intensity.value = Mathf.Lerp(TVig.intensity.value, (Health <= SightDistortionLimit) ? .55f - (Health / 100) : 0, 5 * Time.deltaTime);
    }


}
