using System;
using UnityEngine;
using UnityEngine.UI;
namespace RGVA
{
    public static class Extensions
    {
        #region Transform Position

        #region Add
        public static void AddPositionX(this Transform i_Transform, float i_X)
        {
            i_Transform.position = new Vector3(i_Transform.position.x + i_X, i_Transform.position.y, i_Transform.position.z);
        }

        public static void AddPositionY(this Transform i_Transform, float i_Y)
        {
            i_Transform.position = new Vector3(i_Transform.position.x, i_Transform.position.y + i_Y, i_Transform.position.z);
        }

        public static void AddPositionZ(this Transform i_Transform, float i_Z)
        {
            i_Transform.position = new Vector3(i_Transform.position.x, i_Transform.position.y, i_Transform.position.z + i_Z);
        }

        public static void AddPosition(this Transform i_Transform, Vector3 i_Vector3)
        {
            i_Transform.position = new Vector3(i_Transform.position.x + i_Vector3.x, i_Transform.position.y + i_Vector3.y, i_Transform.position.z + i_Vector3.z);
        }

        public static void AddLocalPositionX(this Transform i_Transform, float i_X)
        {
            i_Transform.localPosition = new Vector3(i_Transform.localPosition.x + i_X, i_Transform.localPosition.y, i_Transform.localPosition.z);
        }

        public static void AddLocalPositionY(this Transform i_Transform, float i_Y)
        {
            i_Transform.localPosition = new Vector3(i_Transform.localPosition.x, i_Transform.localPosition.y + i_Y, i_Transform.localPosition.z);
        }

        public static void AddLocalPositionZ(this Transform i_Transform, float i_Z)
        {
            i_Transform.localPosition = new Vector3(i_Transform.localPosition.x, i_Transform.localPosition.y, i_Transform.localPosition.z + i_Z);
        }

        public static void AddLocalPosition(this Transform i_Transform, Vector3 i_Vector3)
        {
            i_Transform.localPosition = new Vector3(i_Transform.localPosition.x + i_Vector3.x, i_Transform.localPosition.y + i_Vector3.y, i_Transform.localPosition.z + i_Vector3.z);
        }

        public static void AddLocalPositionXZ(this Transform i_Transform, float i_X, float i_Z)
        {
            i_Transform.localPosition = new Vector3(i_Transform.localPosition.x + i_X, i_Transform.localPosition.y, i_Transform.localPosition.z + i_Z);
        }
        #endregion

        #region Set
        public static void SetPositionX(this Transform i_Transform, float i_X)
        {
            i_Transform.position = new Vector3(i_X, i_Transform.position.y, i_Transform.position.z);
        }

        public static void SetPositionY(this Transform i_Transform, float i_Y)
        {
            i_Transform.position = new Vector3(i_Transform.position.x, i_Y, i_Transform.position.z);
        }

        public static void SetPositionZ(this Transform i_Transform, float i_Z)
        {
            i_Transform.position = new Vector3(i_Transform.position.x, i_Transform.position.y, i_Z);
        }

        public static void SetPosition(this Transform i_Transform, Vector3 i_Vector3)
        {
            i_Transform.position = i_Vector3;
        }

        public static void SetPositionXZ(this Transform i_Transform, float i_X, float i_Z)
        {
            i_Transform.position = new Vector3(i_X, i_Transform.position.y, i_Z);
        }

        public static void SetLocalPositionX(this Transform i_Transform, float i_X)
        {
            i_Transform.localPosition = new Vector3(i_X, i_Transform.localPosition.y, i_Transform.localPosition.z);
        }

        public static void SetLocalPositionY(this Transform i_Transform, float i_Y)
        {
            i_Transform.localPosition = new Vector3(i_Transform.localPosition.x, i_Y, i_Transform.localPosition.z);
        }

        public static void SetLocalPositionZ(this Transform i_Transform, float i_Z)
        {
            i_Transform.localPosition = new Vector3(i_Transform.localPosition.x, i_Transform.localPosition.y, i_Z);
        }

        public static void SetLocalPosition(this Transform i_Transform, Vector3 i_Vector3)
        {
            i_Transform.localPosition = i_Vector3;
        }

        public static void SetLocalPositionXZ(this Transform i_Transform, float i_X, float i_Z)
        {
            i_Transform.localPosition = new Vector3(i_X, i_Transform.localPosition.y, i_Z);
        }
        #endregion

        #region Clamp

        public static void ClampPositionX(this Transform i_Transform, float i_Min, float i_Max)
        {
            i_Transform.position = Tools.ClampVectorX(i_Transform.position, i_Min, i_Max);
        }

        public static void ClampPositionY(this Transform i_Transform, float i_Min, float i_Max)
        {
            i_Transform.position = Tools.ClampVectorY(i_Transform.position, i_Min, i_Max);
        }

        public static void ClampPositionZ(this Transform i_Transform, float i_Min, float i_Max)
        {
            i_Transform.position = Tools.ClampVectorZ(i_Transform.position, i_Min, i_Max);
        }

        public static void ClampPosition(this Transform i_Transform, float i_MinX, float i_MaxX, float i_MinY, float i_MaxY, float i_MinZ, float i_MaxZ)
        {
            i_Transform.position = Tools.ClampPosition(i_Transform.position, i_MinX, i_MaxX, i_MinY, i_MaxY, i_MinZ, i_MaxZ);
        }

        public static void ClampLocalPositionX(this Transform i_Transform, float i_Min, float i_Max)
        {
            i_Transform.localPosition = Tools.ClampVectorX(i_Transform.localPosition, i_Min, i_Max);
        }

        public static void ClampLocalPositionY(this Transform i_Transform, float i_Min, float i_Max)
        {
            i_Transform.localPosition = Tools.ClampVectorY(i_Transform.localPosition, i_Min, i_Max);
        }

        public static void ClampLocalPositionZ(this Transform i_Transform, float i_Min, float i_Max)
        {
            i_Transform.localPosition = Tools.ClampVectorZ(i_Transform.localPosition, i_Min, i_Max);
        }

        public static void ClampLocalPosition(this Transform i_Transform, float i_MinX, float i_MaxX, float i_MinY, float i_MaxY, float i_MinZ, float i_MaxZ)
        {
            i_Transform.localPosition = Tools.ClampPosition(i_Transform.localPosition, i_MinX, i_MaxX, i_MinY, i_MaxY, i_MinZ, i_MaxZ);
        }

        #endregion

        #endregion

        #region Transform Rotation

        #region Add
        public static void AddRotationX(this Transform i_Transform, float i_X)
        {
            i_Transform.rotation = Quaternion.Euler(i_Transform.eulerAngles.x + i_X, i_Transform.eulerAngles.y, i_Transform.eulerAngles.z);
        }

        public static void AddRotationY(this Transform i_Transform, float i_Y)
        {
            i_Transform.rotation = Quaternion.Euler(i_Transform.eulerAngles.x, i_Transform.eulerAngles.y + i_Y, i_Transform.eulerAngles.z);
        }

        public static void AddRotationZ(this Transform i_Transform, float i_Z)
        {
            i_Transform.rotation = Quaternion.Euler(i_Transform.eulerAngles.x, i_Transform.eulerAngles.y, i_Transform.eulerAngles.z + i_Z);
        }

        public static void AddRotation(this Transform i_Transform, Vector3 i_Vector3)
        {
            i_Transform.rotation = Quaternion.Euler(i_Transform.eulerAngles.x + i_Vector3.x, i_Transform.eulerAngles.y + i_Vector3.y, i_Transform.eulerAngles.z + i_Vector3.z);
        }

        public static void AddLocalRotationX(this Transform i_Transform, float i_X)
        {
            i_Transform.localRotation = Quaternion.Euler(i_Transform.localEulerAngles.x + i_X, i_Transform.localEulerAngles.y, i_Transform.localEulerAngles.z);
        }

        public static void AddLocalRotationY(this Transform i_Transform, float i_Y)
        {
            i_Transform.localRotation = Quaternion.Euler(i_Transform.localEulerAngles.x, i_Transform.localEulerAngles.y + i_Y, i_Transform.localEulerAngles.z);
        }

        public static void AddLocalRotationZ(this Transform i_Transform, float i_Z)
        {
            i_Transform.localRotation = Quaternion.Euler(i_Transform.localEulerAngles.x, i_Transform.localEulerAngles.y, i_Transform.localEulerAngles.z + i_Z);
        }

        public static void AddLocalRotation(this Transform i_Transform, Vector3 i_Vector3)
        {
            i_Transform.localRotation = Quaternion.Euler(i_Transform.localEulerAngles.x + i_Vector3.x, i_Transform.localEulerAngles.y + i_Vector3.y, i_Transform.localEulerAngles.z + i_Vector3.z);
        }

        #endregion

        #region Set

        public static void SetRotationX(this Transform i_Transform, float i_X)
        {
            i_Transform.rotation = Quaternion.Euler(i_X, i_Transform.eulerAngles.y, i_Transform.eulerAngles.z);
        }

        public static void SetRotationY(this Transform i_Transform, float i_Y)
        {
            i_Transform.rotation = Quaternion.Euler(i_Transform.eulerAngles.x, i_Y, i_Transform.eulerAngles.z);
        }

        public static void SetRotationZ(this Transform i_Transform, float i_Z)
        {
            i_Transform.rotation = Quaternion.Euler(i_Transform.eulerAngles.x, i_Transform.eulerAngles.y, i_Z);
        }

        public static void SetRotation(this Transform i_Transform, Vector3 i_Vector3)
        {
            i_Transform.rotation = Quaternion.Euler(i_Vector3.x, i_Vector3.y, i_Vector3.z);
        }

        public static void SetLocalRotationX(this Transform i_Transform, float i_X)
        {
            i_Transform.localRotation = Quaternion.Euler(i_X, i_Transform.localEulerAngles.y, i_Transform.localEulerAngles.z);
        }

        public static void SetLocalRotationY(this Transform i_Transform, float i_Y)
        {
            i_Transform.localRotation = Quaternion.Euler(i_Transform.localEulerAngles.x, i_Y, i_Transform.localEulerAngles.z);
        }

        public static void SetLocalRotationZ(this Transform i_Transform, float i_Z)
        {
            i_Transform.localRotation = Quaternion.Euler(i_Transform.localEulerAngles.x, i_Transform.localEulerAngles.y, i_Z);
        }

        public static void SetLocalRotation(this Transform i_Transform, Vector3 i_Vector3)
        {
            i_Transform.localRotation = Quaternion.Euler(i_Vector3.x, i_Vector3.y, i_Vector3.z);
        }


        #endregion

        #region Clamp
        public static void ClampRotationX(this Transform i_Transform, float i_Min, float i_Max)
        {
            i_Transform.rotation = Tools.ClampRotationX(i_Transform.rotation, i_Min, i_Max);
        }

        public static void ClampRotationY(this Transform i_Transform, float i_Min, float i_Max)
        {
            i_Transform.rotation = Tools.ClampRotationY(i_Transform.rotation, i_Min, i_Max);
        }

        public static void ClampRotationZ(this Transform i_Transform, float i_Min, float i_Max)
        {
            i_Transform.rotation = Tools.ClampRotationZ(i_Transform.rotation, i_Min, i_Max);
        }

        public static void ClampRotation(this Transform i_Transform, float i_MinX, float i_MaxX, float i_MinY, float i_MaxY, float i_MinZ, float i_MaxZ)
        {
            i_Transform.rotation = Tools.ClampRotation(i_Transform.rotation, i_MinX, i_MaxX, i_MinY, i_MaxY, i_MinZ, i_MaxZ);
        }

        public static void ClampLocalRotationX(this Transform i_Transform, float i_Min, float i_Max)
        {
            i_Transform.localRotation = Tools.ClampRotationX(i_Transform.localRotation, i_Min, i_Max);
        }

        public static void ClampLocalRotationY(this Transform i_Transform, float i_Min, float i_Max)
        {
            i_Transform.localRotation = Tools.ClampRotationY(i_Transform.localRotation, i_Min, i_Max);
        }

        public static void ClampLocalRotationZ(this Transform i_Transform, float i_Min, float i_Max)
        {
            i_Transform.localRotation = Tools.ClampRotationZ(i_Transform.localRotation, i_Min, i_Max);
        }

        public static void ClampLocalRotation(this Transform i_Transform, float i_MinX, float i_MaxX, float i_MinY, float i_MaxY, float i_MinZ, float i_MaxZ)
        {
            i_Transform.localRotation = Tools.ClampRotation(i_Transform.localRotation, i_MinX, i_MaxX, i_MinY, i_MaxY, i_MinZ, i_MaxZ);
        }
        #endregion

        #endregion

        #region Transform Scale

        #region Add
        public static void AddLocalScaleX(this Transform i_Transform, float i_X)
        {
            i_Transform.localScale = new Vector3(i_Transform.lossyScale.x + i_X, i_Transform.lossyScale.y, i_Transform.lossyScale.z);
        }

        public static void AddLocalScaleY(this Transform i_Transform, float i_Y)
        {
            i_Transform.localScale = new Vector3(i_Transform.lossyScale.x, i_Transform.lossyScale.y + i_Y, i_Transform.lossyScale.z);
        }

        public static void AddLocalScaleZ(this Transform i_Transform, float i_Z)
        {
            i_Transform.localScale = new Vector3(i_Transform.lossyScale.x, i_Transform.lossyScale.y, i_Transform.lossyScale.z + i_Z);
        }

        public static void AddLocalScale(this Transform i_Transform, Vector3 i_Vector3)
        {
            i_Transform.localScale += i_Vector3;
        }
        #endregion

        #region Set
        public static void SetLocalScaleX(this Transform i_Transform, float i_X)
        {
            i_Transform.localScale = new Vector3(i_X, i_Transform.lossyScale.y, i_Transform.lossyScale.z);
        }

        public static void SetLocalScaleY(this Transform i_Transform, float i_Y)
        {
            i_Transform.localScale = new Vector3(i_Transform.lossyScale.x, i_Y, i_Transform.lossyScale.z);
        }

        public static void SetLocalScaleZ(this Transform i_Transform, float i_Z)
        {
            i_Transform.localScale = new Vector3(i_Transform.lossyScale.x, i_Transform.lossyScale.y, i_Z);
        }

        public static void SetLocalScale(this Transform i_Transform, Vector3 i_Vector3)
        {
            i_Transform.localScale = i_Vector3;
        }

        public static void SetLocalScale(this Transform i_Transform, float i_X = 1, float i_Y = 1, float i_Z = 1)
        {
            i_Transform.localScale = new Vector3(i_X, i_Y, i_Z); ;
        }

        #endregion

        #region Clamp

        public static void ClampLocalScaleX(this Transform i_Transform, float i_Min, float i_Max)
        {
            i_Transform.localScale = Tools.ClampVectorX(i_Transform.localScale, i_Min, i_Max);
        }

        public static void ClampLocalScaleY(this Transform i_Transform, float i_Min, float i_Max)
        {
            i_Transform.localScale = Tools.ClampVectorY(i_Transform.localScale, i_Min, i_Max);
        }

        public static void ClampLocalScaleZ(this Transform i_Transform, float i_Min, float i_Max)
        {
            i_Transform.localScale = Tools.ClampVectorZ(i_Transform.localScale, i_Min, i_Max);
        }
        public static void ClampLocalScale(this Transform i_Transform, float i_Min, float i_Max)
        {
            i_Transform.ClampLocalScale(i_Min, i_Max, i_Min, i_Max, i_Min, i_Max);
        }
        public static void ClampLocalScale(this Transform i_Transform, float i_MinX, float i_MaxX, float i_MinY, float i_MaxY, float i_MinZ, float i_MaxZ)
        {
            i_Transform.localScale = Tools.ClampPosition(i_Transform.localScale, i_MinX, i_MaxX, i_MinY, i_MaxY, i_MinZ, i_MaxZ);
        }
        #endregion

        #endregion

        #region Tranform FindDeepChild

        public static T FindDeepChild<T>(this Transform aParent, string aName, bool hasString = false)
        {
            T result = default(T);

            var transform = aParent.FindDeepChild(aName, hasString);

            if (transform != null)
            {
                result = (typeof(T) == typeof(GameObject)) ? (T)Convert.ChangeType(transform.gameObject, typeof(T)) : transform.GetComponent<T>();
            }

            if (result == null)
            {
                Debug.LogError($"FindDeepChild didn't find: '{aName}' on GameObject: '{aParent.name}'");
            }

            return result;
        }

        public static Transform FindDeepChild(this Transform aParent, string aName, bool hasString = false)
        {
            if (aParent != null)
            {
                var result = aParent.Find(aName);
                if (hasString)
                {
                    foreach (Transform child in aParent)
                    {
                        if (child.name.Contains(aName))
                        {
                            return child;
                        }
                    }
                }

                if (result != null)
                    return result;

                foreach (Transform child in aParent)
                {
                    result = child.FindDeepChild(aName, hasString);
                    if (result != null)
                        return result;
                }
            }

            return null;
        }

        #endregion

        #region Line Renderer

        public static void SetPositionsSmooth(this LineRenderer i_LineRenderer, Vector3[] i_KeyPoints, int i_Segments = 25)
        {
            Vector3[] Points = new Vector3[(i_KeyPoints.Length - 1) * i_Segments + i_KeyPoints.Length];
            for (int i = 1; i < i_KeyPoints.Length; i++)
            {
                Points[(i - 1) * i_Segments + i - 1] = new Vector3(i_KeyPoints[i - 1].x, 0, i_KeyPoints[i - 1].z);
                for (int j = 1; j <= i_Segments; j++)
                {
                    float x = i_KeyPoints[i - 1].x;
                    float y = i_KeyPoints[i - 1].y;
                    float z = i_KeyPoints[i - 1].z;
                    float dx = (i_KeyPoints[i].x - i_KeyPoints[i - 1].x) / i_Segments;
                    float dy = (i_KeyPoints[i].y - i_KeyPoints[i - 1].y) / i_Segments;
                    float dz = (i_KeyPoints[i].z - i_KeyPoints[i - 1].z) / i_Segments;
                    Points[(i - 1) * i_Segments + j + i - 1] = new Vector3(x + dx * j, y * dy, z + dz * j);
                }
            }
            Points[(i_KeyPoints.Length - 1) * i_Segments + i_KeyPoints.Length - 1] = new Vector3(i_KeyPoints[i_KeyPoints.Length - 1].x, 0, i_KeyPoints[i_KeyPoints.Length - 1].z);
            i_LineRenderer.SetPositions(Points);
        }

        public static void SetPositionsSmoothXZ(this LineRenderer i_LineRenderer, Vector3[] i_KeyPoints, int i_Segments = 25)
        {
            Vector3[] Points = new Vector3[(i_KeyPoints.Length - 1) * i_Segments + i_KeyPoints.Length];
            for (int i = 1; i < i_KeyPoints.Length; i++)
            {
                Points[(i - 1) * i_Segments + i - 1] = new Vector3(i_KeyPoints[i - 1].x, 0, i_KeyPoints[i - 1].z);
                for (int j = 1; j <= i_Segments; j++)
                {
                    float x = i_KeyPoints[i - 1].x;
                    float y = 0;//i_KeyPoints[i - 1].y;
                    float z = i_KeyPoints[i - 1].z;
                    float dx = (i_KeyPoints[i].x - i_KeyPoints[i - 1].x) / i_Segments;
                    float dy = 0;// (i_KeyPoints[i].y - i_KeyPoints[i - 1].y) / i_Segments;
                    float dz = (i_KeyPoints[i].z - i_KeyPoints[i - 1].z) / i_Segments;
                    Points[(i - 1) * i_Segments + j + i - 1] = new Vector3(x + dx * j, y * dy, z + dz * j);
                }
            }
            Points[(i_KeyPoints.Length - 1) * i_Segments + i_KeyPoints.Length - 1] = new Vector3(i_KeyPoints[i_KeyPoints.Length - 1].x, 0, i_KeyPoints[i_KeyPoints.Length - 1].z);
            i_LineRenderer.SetPositions(Points);
        }

        public static void SetColor(this LineRenderer i_LineRenderer, Color i_Color)
        {
            i_LineRenderer.startColor = i_Color;
            i_LineRenderer.endColor = i_Color;
        }

        #endregion

        public static void SetColorAlpha(this Image i_Image, float i_Value)
        {
            i_Image.color = Tools.SetColorAlpha(i_Image.color, i_Value);
        }

        public static void SetColorAlpha(this Material i_Material, float i_Value)
        {
            i_Material.color = Tools.SetColorAlpha(i_Material.color, i_Value);
        }


        public static void FillAmount(this Image i_Image, float i_CurrentValue, float i_MaxValue)
        {
            i_Image.fillAmount = Tools.ValueToPercentage(i_CurrentValue, i_MaxValue);
        }

        public static float Distance(this Transform i_From, Transform i_To)
        {
            return i_From.Distance(i_To.position);
        }

        public static float Distance(this Transform i_From, Vector3 i_To)
        {
            return Vector3.Distance(i_From.position, i_To);
        }
    }
}
