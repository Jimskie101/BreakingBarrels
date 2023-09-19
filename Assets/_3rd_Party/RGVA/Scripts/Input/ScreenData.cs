using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public struct ScreenData
{
    [ShowInInspector, ReadOnly] private Vector2 OriginalRes;
    [ShowInInspector, ReadOnly] private float OriginalDPI;

    [ShowInInspector, ReadOnly] private Vector2 FinalRes;
    [ShowInInspector, ReadOnly] public float FinalDPI;
    [ShowInInspector, ReadOnly] private Vector2 ScreenSizeInch;
    [ShowInInspector, ReadOnly] private Vector2 ScreenSizeCm;

    [ShowInInspector, ReadOnly] private Vector2 ScalingVector;
    [ShowInInspector, ReadOnly] public float Scaling;


    public void CalculateData(bool i_LogData = false)
    {
        OriginalRes = new Vector2(Screen.width, Screen.height);
        OriginalDPI = Screen.dpi;

#if UNITY_EDITOR
        ScalingVector = GameViewEditorRes.GetGameViewScale();
#else
        ScalingVector = Vector3.one;
#endif
        Scaling = (ScalingVector.x + ScalingVector.y) * .5f;

        FinalRes = new Vector2(Screen.width * ScalingVector.x, Screen.height * ScalingVector.y);

        if (Screen.dpi == 0)
        {
            FinalDPI = 320;
        }
        else
        {
            FinalDPI = Screen.dpi * Scaling;
        }

        ScreenSizeInch = new Vector2((FinalRes.x / FinalDPI) * ScalingVector.x, (FinalRes.y / FinalDPI) * ScalingVector.y);
        ScreenSizeCm = ScreenSizeInch * 2.54f;



        if (!i_LogData)
            return;

        Debug.LogError("Original Resolution - " + OriginalRes);
        Debug.LogError("Original DPI - " + OriginalDPI);
        Debug.LogError("Resolution - " + FinalRes);
        Debug.LogError("DPI - " + FinalDPI);
        Debug.LogError("Size Inches - " + ScreenSizeInch);
        Debug.LogError("Size Cm - " + ScreenSizeCm);

        Debug.LogError("Scaling Vector - " + ScalingVector);
        Debug.LogError("Scaling - " + Scaling);
    }
}
