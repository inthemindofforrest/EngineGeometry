using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Lightning : MonoBehaviour
{
    public float NextStrike = 5f;//Seconds until the next Lightning strike
    bool SecondStrike = false;//Whether the second strike has occured
    const float SecondStrikeDelay = 1.7f;//Seconds before the second lighting strike

    [Header("Visuals")]
    public bool Edges = true;//Whether Vignette effect is applied
    public bool GrayScale = true;//Whether ColorGrading Post Exposure effect is applied
    public bool DepthOfField = true;//Whether DepthOfField effect is applied
    public bool Exposure = true;//Whether ColorGrading Saturation effect is applied

    [SerializeField]//Serialized in order for it to show in the inspector
    PostProcessVolume Volume;//Volume for Post Processing
    Vignette Vig;//Vignette for Post Proccessing
    ColorGrading ColorG;//ColorGrading for Post Proccessing
    DepthOfField DOF;//DepthOfField for Post Proccessing

    MotionBlur Blur;//MotionBlur for Post Proccessing

    [Range(-100, 100)]//Clamp Inspector to the range
    public int GrayscaleRange = 0;//Targeted reset for Grayscaling (-100 Gray, 0 Normal, 100 Super Saturated)

    void Start()
    {
        #region Vignette Init
        Vig = ScriptableObject.CreateInstance<Vignette>();
        Vig.enabled.Override(true);
        Vig.intensity.Override(.45f);
        #endregion

        #region ColorGrading Init
        ColorG = ScriptableObject.CreateInstance<ColorGrading>();
        ColorG.enabled.Override(true);
        if(GrayScale)ColorG.saturation.Override(-100f);
        ColorG.postExposure.overrideState = true;
        #endregion

        #region DepthOfField Init
        DOF = ScriptableObject.CreateInstance<DepthOfField>();
        DOF.enabled.Override(true);
        DOF.focusDistance.Override(5f);
        #endregion

        #region MotionBlur Init
        Blur = ScriptableObject.CreateInstance<MotionBlur>();
        Blur.enabled.Override(true);
        #endregion

        #region Volume Init
        Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, Vig, ColorG, DOF, Blur);
        #endregion
    }

    void Update()
    {
        //Update the strike function
        StrikeUpdate();
    }

    //Sets Saturtation Normal, then back to low
    //Sets Brightness High, then low, then back to normal
    void Strike()
    {
        //Sets PPE (Post Proccessing Effects) sudenly to new value
        if (DepthOfField) DOF.focusDistance.value = 1f;
        if (Edges) Vig.intensity.value = .6f;
        if (GrayScale) ColorG.saturation.value = 100;
        if (Exposure) ColorG.postExposure.value = 1.8f;

        //PROBLEM: Can cause many strikes to happen one after another
        //Still a cool effect which is why it is left in
        SecondStrike = false;
    }

    void ResetingStrike()
    {
        //Lerp PPE back to its default values
        if (DepthOfField) DOF.focusDistance.value = Mathf.Lerp(DOF.focusDistance.value, 10f, .1f * Time.deltaTime);
        if (Edges) Vig.intensity.value = Mathf.Lerp(Vig.intensity.value, .5f, 1f * Time.deltaTime);
        if (GrayScale) ColorG.saturation.value = Mathf.Lerp(ColorG.saturation.value, GrayscaleRange, 1f * Time.deltaTime);
        if (Exposure) ColorG.postExposure.value = Mathf.Lerp(ColorG.postExposure.value, -.5f, 1f * Time.deltaTime);
    }

    void StrikeUpdate()
    {
        NextStrike -= Time.deltaTime;//Timer for the Strike
        if (NextStrike < 0)//If the next strike is suppose to happen
        {
            NextStrike = Random.Range(10, 25);//Resets the next strike timer
            Strike();//Begin strike
            if (!SecondStrike)//If the lightin has only striked once
            {
                if (Random.Range(0, 100) < 40)//40% chance to strike twice
                {
                    NextStrike = SecondStrikeDelay;//Sets the timer for a quick second strike 
                    SecondStrike = true;//Has done its second strike
                }

            }
        }
        ResetingStrike();//Resets the strike to its normal values
    }
}
