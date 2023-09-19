using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RGVA
{
    public static class Tools
    {
        private static Quaternion QuaternionDivideByW(Quaternion i_Quaternion)
        {
            i_Quaternion.x /= i_Quaternion.w;
            i_Quaternion.y /= i_Quaternion.w;
            i_Quaternion.z /= i_Quaternion.w;
            i_Quaternion.w = 1.0f;
            return i_Quaternion;
        }

        private static float AngleToRad2DegClamped(float i_Value, float i_Min, float i_Max)
        {
            float angleZ = 2.0f * Mathf.Rad2Deg * Mathf.Atan(i_Value);
            angleZ = Mathf.Clamp(angleZ, i_Min, i_Max);
            return Mathf.Tan(0.5f * Mathf.Deg2Rad * angleZ);
        }

        private static bool CheckIfDerivedFromMonoBehaviour<T>(T i_Object)
        {
            if ((i_Object as MonoBehaviour) == null && (i_Object as Component) == null)
            {
                Debug.LogWarning("The script needs to derived from MonoBehaviour");
                return false;
            }
            else
            {
                return true;
            }
        }

        #region ClampRotation

        public static Quaternion ClampRotationX(Quaternion i_Quaternion, float i_Min, float i_Max)
        {
            i_Quaternion = QuaternionDivideByW(i_Quaternion);
            i_Quaternion.x = AngleToRad2DegClamped(i_Quaternion.x, i_Min, i_Max);
            return i_Quaternion;
        }

        public static Quaternion ClampRotationY(Quaternion i_Quaternion, float i_Min, float i_Max)
        {
            i_Quaternion = QuaternionDivideByW(i_Quaternion);
            i_Quaternion.y = AngleToRad2DegClamped(i_Quaternion.y, i_Min, i_Max);
            return i_Quaternion;
        }

        public static Quaternion ClampRotationZ(Quaternion i_Quaternion, float i_Min, float i_Max)
        {
            i_Quaternion = QuaternionDivideByW(i_Quaternion);
            i_Quaternion.z = AngleToRad2DegClamped(i_Quaternion.z, i_Min, i_Max);
            return i_Quaternion;
        }

        public static Quaternion ClampRotation(Quaternion i_Quaternion, float i_MinX, float i_MaxX, float i_MinY, float i_MaxY, float i_MinZ, float i_MaxZ)
        {
            i_Quaternion = QuaternionDivideByW(i_Quaternion);

            i_Quaternion.x = AngleToRad2DegClamped(i_Quaternion.x, i_MinX, i_MaxX);
            i_Quaternion.y = AngleToRad2DegClamped(i_Quaternion.y, i_MinY, i_MaxY);
            i_Quaternion.z = AngleToRad2DegClamped(i_Quaternion.z, i_MinZ, i_MaxZ);
            return i_Quaternion;
        }
        #endregion

        #region Vector3

        /// <summary>
        /// Determines positions around the given point.
        /// </summary>
        /// <param name="i_CenterPoint">Position to encircle</param>
        /// <param name="i_Radius">Distance from center point</param>
        /// <param name="i_Length">The number of positions</param>
        /// <param name="i_Index">The index of position</param>
        /// <returns></returns>
        public static Vector3 Encircle(Vector3 i_CenterPoint, float i_Radius, int i_Length, int i_Index = 0)
        {
            float ang = 360 / i_Length * i_Index;
            Vector3 pos;
            pos.x = i_CenterPoint.x + i_Radius * Mathf.Sin(ang * Mathf.Deg2Rad);
            pos.y = i_CenterPoint.y;
            pos.z = i_CenterPoint.z + i_Radius * Mathf.Cos(ang * Mathf.Deg2Rad);
            return pos;
        }

        #region Clamp
        public static Vector3 ClampVectorX(Vector3 i_Vector, float i_MinX, float i_MaxX)
        {
            i_Vector.x = Mathf.Clamp(i_Vector.x, i_MinX, i_MaxX);
            return i_Vector;
        }
        public static Vector3 ClampVectorY(Vector3 i_Vector, float i_MinY, float i_MaxY)
        {
            i_Vector.y = Mathf.Clamp(i_Vector.y, i_MinY, i_MaxY);
            return i_Vector;
        }
        public static Vector3 ClampVectorZ(Vector3 i_Vector, float i_MinZ, float i_MaxZ)
        {
            i_Vector.z = Mathf.Clamp(i_Vector.z, i_MinZ, i_MaxZ);
            return i_Vector;
        }

        public static Vector3 ClampPosition(Vector3 i_Vector, float i_Min, float i_Max)
        {
            return ClampPosition(i_Vector, i_Min, i_Max, i_Min, i_Max, i_Min, i_Max);
        }

        public static Vector3 ClampPosition(Vector3 i_Vector, float i_MinX, float i_MaxX, float i_MinY, float i_MaxY, float i_MinZ, float i_MaxZ)
        {
            return new Vector3(Mathf.Clamp(i_Vector.x, i_MinX, i_MaxX), Mathf.Clamp(i_Vector.y, i_MinY, i_MaxY), Mathf.Clamp(i_Vector.z, i_MinZ, i_MaxZ));
        }
        #endregion

        #region Get Center Point

        public static Vector3 GetCenterPoint(Vector3 i_Origin, Vector3 i_Target)
        {
            return (i_Target + i_Origin) / 2;
        }

        public static Vector3 GetCenterPoint(Vector3[] i_Points)
        {
            Vector3 totalPoints = Vector3.zero;
            for (int i = 0; i < i_Points.Length; i++)
            {
                totalPoints += i_Points[i];
            }
            return totalPoints / i_Points.Length;
        }

        public static Vector3 GetCenterPoint(Transform[] i_Points)
        {
            Vector3[] positions = new Vector3[i_Points.Length];
            for (int i = 0; i < positions.Length; i++)
            {
                positions[i] = i_Points[i].position;
            }
            return GetCenterPoint(positions);
        }

        public static Vector3 GetCenterPoint(GameObject[] i_Points)
        {
            Vector3[] positions = new Vector3[i_Points.Length];
            for (int i = 0; i < positions.Length; i++)
            {
                positions[i] = i_Points[i].transform.position;
            }
            return GetCenterPoint(positions);
        }

        /// <summary>
        /// The scripts needs to be derived from Behaviour
        /// </summary>
        public static Vector3 GetCenterPoint<T>(T[] i_Objects)
        {
            if (!CheckIfDerivedFromMonoBehaviour(i_Objects[0]))
                return Vector3.zero;

            return GetCenterPoint(ToTransforms(i_Objects));
        }

        /// <summary>
        /// The scripts needs to be derived from Behaviour
        /// </summary>
        public static Vector3 GetCenterPoint<T>(List<T> i_Objects)
        {
            if (!CheckIfDerivedFromMonoBehaviour(i_Objects[0]))
                return Vector3.zero;

            return GetCenterPoint(ToListTransforms(i_Objects).ToArray());
        }
        #endregion

        #region Get Direction
        public static Vector3 GetDirection(Vector3 i_Origin, Vector3 i_Target, bool i_IncludeY = false, bool i_IsInverted = false)
        {
            Vector3 direction = Vector3.zero;
            if (i_IncludeY)
                direction = i_Target - i_Origin;
            else
                direction = new Vector3(i_Target.x, 0, i_Target.z) - new Vector3(i_Origin.x, 0, i_Origin.z);

            if (i_IsInverted)
                return -direction.normalized;
            else
                return direction.normalized;

        }
        #endregion


        private static float GetGreatestDistance(Vector3[] i_Targets)
        {
            var bounds = new Bounds(i_Targets[0], Vector3.zero);
            for (int i = 0; i < i_Targets.Length; i++)
            {
                bounds.Encapsulate(i_Targets[i]);
            }
            return bounds.size.x;
        }

        #endregion

        #region Vector2
        public static Vector2 ClampVectorX(Vector2 i_Vector, float i_MinX, float i_MaxX)
        {
            i_Vector.x = Mathf.Clamp(i_Vector.x, i_MinX, i_MaxX);
            return i_Vector;
        }
        public static Vector2 ClampVectorY(Vector2 i_Vector, float i_MinX, float i_MaxX)
        {
            i_Vector.y = Mathf.Clamp(i_Vector.y, i_MinX, i_MaxX);
            return i_Vector;
        }

        public static Vector2 ClampPosition(Vector2 i_Vector, float i_MinX, float i_MaxX, float i_MinY, float i_MaxY)
        {
            i_Vector.x = Mathf.Clamp(i_Vector.x, i_MinX, i_MaxX);
            i_Vector.y = Mathf.Clamp(i_Vector.y, i_MinY, i_MaxY);
            return i_Vector;
        }

        public static Vector2 GetCenterPoint(Vector2 i_Origin, Vector2 i_Target)
        {
            return (i_Target + i_Origin) / 2;
        }

        public static Vector2 GetCenterPoint(Vector2[] i_Points)
        {
            Vector2 totalPoints = Vector2.zero;
            for (int i = 0; i < i_Points.Length; i++)
            {
                totalPoints += i_Points[i];
            }
            return totalPoints / i_Points.Length;
        }

        #region Get Direction
        public static Vector2 GetDirection(Vector2 i_Origin, Vector2 i_Target, bool i_IsInverted = false)
        {
            if (i_IsInverted)
                return (i_Target - i_Origin).normalized;
            else
                return -(i_Target - i_Origin).normalized;
        }

        #endregion

        #endregion

        #region ArrayList Scripts Converter 

        /// <summary>
        /// The scripts needs to be derived from Behaviour
        /// </summary>
        public static Transform[] ToTransforms<T>(T[] i_Objects)
        {
            if (i_Objects == null)
                return null;
            if (!CheckIfDerivedFromMonoBehaviour(i_Objects[0]))
                return null;

            Transform[] transforms = new Transform[i_Objects.Length];
            for (int i = 0; i < transforms.Length; i++)
            {
                transforms[i] = (i_Objects[i] as MonoBehaviour) != null ? (i_Objects[i] as MonoBehaviour).transform : (i_Objects[i] as Component).transform;
            }
            return transforms;
        }

        /// <summary>
        /// The scripts needs to be derived from Behaviour
        /// </summary>
        public static List<Transform> ToListTransforms<T>(List<T> i_Objects)
        {
            if (i_Objects == null)
                return null;
            if (!CheckIfDerivedFromMonoBehaviour(i_Objects[0]))
                return null;

            List<Transform> transforms = new List<Transform>();
            for (int i = 0; i < i_Objects.Count; i++)
            {
                transforms.Add((i_Objects[i] as MonoBehaviour).transform);
            }
            return transforms;
        }
        #endregion

        #region Color
        public static Color SetColorAlpha(Color i_Color, float i_Value)
        {
            return new Color(i_Color.r, i_Color.g, i_Color.b, i_Value);
        }
        #endregion

        public static float ValueToPercentage(float i_CurrentValue, float i_TotalValue)
        {
            return i_CurrentValue / i_TotalValue;
        }

        public static Quaternion SmoothDampQuaternion(Quaternion current, Quaternion target, ref Vector3 currentVelocity, float smoothTime, float speed = 1f)
        {
            Vector3 c = current.eulerAngles;
            Vector3 t = target.eulerAngles;
            return Quaternion.Euler(
              Mathf.SmoothDampAngle(c.x, t.x, ref currentVelocity.x, smoothTime, speed),
              Mathf.SmoothDampAngle(c.y, t.y, ref currentVelocity.y, smoothTime, speed),
              Mathf.SmoothDampAngle(c.z, t.z, ref currentVelocity.z, smoothTime, speed)
            );
        }

        public static Vector3 SmoothDampEuler(Vector3 current, Vector3 target, ref Vector3 currentVelocity, float smoothTime, float speed = 1f)
        {
            return new Vector3(
              Mathf.SmoothDampAngle(current.x, target.x, ref currentVelocity.x, smoothTime, speed),
              Mathf.SmoothDampAngle(current.y, target.y, ref currentVelocity.y, smoothTime, speed),
              Mathf.SmoothDampAngle(current.z, target.z, ref currentVelocity.z, smoothTime, speed)
            );
        }


        public static void Shuffle<T>(this IList<T> list)
        {
            System.Random rng = new System.Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static List<T> Join<T>(this List<T> first, List<T> second)
        {
            if (first == null)
            {
                return second;
            }
            if (second == null)
            {
                return first;
            }

            return first.Concat(second).ToList();
        }
        public static Texture2D Resize(Texture2D texture2D, int targetX, int targetY)
        {
            RenderTexture rt = new RenderTexture(targetX, targetY, 24);
            RenderTexture.active = rt;
            Graphics.Blit(texture2D, rt);
            Texture2D result = new Texture2D(targetX, targetY);
            result.ReadPixels(new Rect(0, 0, targetX, targetY), 0, 0);
            result.Apply();
            return result;
        }
    }
}


