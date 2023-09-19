using UnityEngine;

namespace RGVA.Serialized.JSON
{
    public enum JSONContainerType { Array, Object }
	public partial class JSONNode
	{
        public static JSONContainerType VectorContainerType = JSONContainerType.Array;
        public static JSONContainerType QuaternionContainerType = JSONContainerType.Array;
        public static JSONContainerType RectContainerType = JSONContainerType.Array;
        private static JSONNode GetContainer(JSONContainerType aType)
        {
            if (aType == JSONContainerType.Array)
                return new JSONArray();
            return new JSONObject();
        }

        #region implicit conversion operators
        public static implicit operator JSONNode(Vector2 aVec)
		{
            JSONNode n = GetContainer(VectorContainerType);
            n.WriteVector2(aVec);
			return n;
		}
		public static implicit operator JSONNode(Vector3 aVec)
		{
            JSONNode n = GetContainer(VectorContainerType);
            n.WriteVector3(aVec);
            return n;
        }
        public static implicit operator JSONNode(Vector4 aVec)
		{
            JSONNode n = GetContainer(VectorContainerType);
            n.WriteVector4(aVec);
            return n;
        }
        public static implicit operator JSONNode(Quaternion aRot)
		{
            JSONNode n = GetContainer(QuaternionContainerType);
            n.WriteQuaternion(aRot);
            return n;
        }
        public static implicit operator JSONNode(Rect aRect)
		{
            JSONNode n = GetContainer(RectContainerType);
            n.WriteRect(aRect);
            return n;
        }
        public static implicit operator JSONNode(RectOffset aRect)
		{
            JSONNode n = GetContainer(RectContainerType);
            n.WriteRectOffset(aRect);
            return n;
        }

        public static implicit operator Vector2(JSONNode aNode)
        {
            return aNode.ReadVector2();
        }
        public static implicit operator Vector3(JSONNode aNode)
        {
            return aNode.ReadVector3();
        }
        public static implicit operator Vector4(JSONNode aNode)
        {
            return aNode.ReadVector4();
        }
        public static implicit operator Quaternion(JSONNode aNode)
        {
            return aNode.ReadQuaternion();
        }
        public static implicit operator Rect(JSONNode aNode)
        {
            return aNode.ReadRect();
        }
        public static implicit operator RectOffset(JSONNode aNode)
        {
            return aNode.ReadRectOffset();
        }
        #endregion implicit conversion operators

        #region Vector2
        public Vector2 ReadVector2(Vector2 aDefault)
        {
            if (IsObject)
                return new Vector2(this["x"].AsFloat, this["y"].AsFloat);
            if (IsArray)
                return new Vector2(this[0].AsFloat, this[1].AsFloat);
            return aDefault;
        }
        public Vector2 ReadVector2(string aXName, string aYName)
        {
            if (IsObject)
            {
                return new Vector2(this[aXName].AsFloat, this[aYName].AsFloat);
            }
            return Vector2.zero;
        }

        public Vector2 ReadVector2()
        {
            return ReadVector2(Vector2.zero);
        }
        public JSONNode WriteVector2(Vector2 aVec, string aXName = "x", string aYName = "y")
        {
            if (IsObject)
            {
                Inline = true;
                this[aXName].AsFloat = aVec.x;
                this[aYName].AsFloat = aVec.y;
            }
            else if (IsArray)
            {
                Inline = true;
                this[0].AsFloat = aVec.x;
                this[1].AsFloat = aVec.y;
            }
            return this;
        }
        #endregion Vector2

        #region Vector3
        public Vector3 ReadVector3(Vector3 aDefault)
        {
            if (IsObject)
                return new Vector3(this["x"].AsFloat, this["y"].AsFloat, this["z"].AsFloat);
            if (IsArray)
                return new Vector3(this[0].AsFloat, this[1].AsFloat, this[2].AsFloat);
            return aDefault;
        }
        public Vector3 ReadVector3(string aXName, string aYName, string aZName)
        {
            if (IsObject)
                return new Vector3(this[aXName].AsFloat, this[aYName].AsFloat, this[aZName].AsFloat);
            return Vector3.zero;
        }
        public Vector3 ReadVector3()
        {
            return ReadVector3(Vector3.zero);
        }
        public JSONNode WriteVector3(Vector3 aVec, string aXName = "x", string aYName = "y", string aZName = "z")
        {
            if (IsObject)
            {
                Inline = true;
                this[aXName].AsFloat = aVec.x;
                this[aYName].AsFloat = aVec.y;
                this[aZName].AsFloat = aVec.z;
            }
            else if (IsArray)
            {
                Inline = true;
                this[0].AsFloat = aVec.x;
                this[1].AsFloat = aVec.y;
                this[2].AsFloat = aVec.z;
            }
            return this;
        }
        #endregion Vector3

        #region Vector4
        public Vector4 ReadVector4(Vector4 aDefault)
        {
            if (IsObject)
                return new Vector4(this["x"].AsFloat, this["y"].AsFloat, this["z"].AsFloat, this["w"].AsFloat);
            if (IsArray)
                return new Vector4(this[0].AsFloat, this[1].AsFloat, this[2].AsFloat, this[3].AsFloat);
            return aDefault;
        }
        public Vector4 ReadVector4()
        {
            return ReadVector4(Vector4.zero);
        }
        public JSONNode WriteVector4(Vector4 aVec)
        {
            if (IsObject)
            {
                Inline = true;
                this["x"].AsFloat = aVec.x;
                this["y"].AsFloat = aVec.y;
                this["z"].AsFloat = aVec.z;
                this["w"].AsFloat = aVec.w;
            }
            else if (IsArray)
            {
                Inline = true;
                this[0].AsFloat = aVec.x;
                this[1].AsFloat = aVec.y;
                this[2].AsFloat = aVec.z;
                this[3].AsFloat = aVec.w;
            }
            return this;
        }
        #endregion Vector4

        #region Quaternion
        public Quaternion ReadQuaternion(Quaternion aDefault)
        {
            if (IsObject)
                return new Quaternion(this["x"].AsFloat, this["y"].AsFloat, this["z"].AsFloat, this["w"].AsFloat);
            if (IsArray)
                return new Quaternion(this[0].AsFloat, this[1].AsFloat, this[2].AsFloat, this[3].AsFloat);
            return aDefault;
        }
        public Quaternion ReadQuaternion()
        {
            return ReadQuaternion(Quaternion.identity);
        }
        public JSONNode WriteQuaternion(Quaternion aRot)
        {
            if (IsObject)
            {
                Inline = true;
                this["x"].AsFloat = aRot.x;
                this["y"].AsFloat = aRot.y;
                this["z"].AsFloat = aRot.z;
                this["w"].AsFloat = aRot.w;
            }
            else if (IsArray)
            {
                Inline = true;
                this[0].AsFloat = aRot.x;
                this[1].AsFloat = aRot.y;
                this[2].AsFloat = aRot.z;
                this[3].AsFloat = aRot.w;
            }
            return this;
        }
        #endregion Quaternion

        #region Rect
        public Rect ReadRect(Rect aDefault)
        {
            if (IsObject)
                return new Rect(this["x"].AsFloat, this["y"].AsFloat, this["width"].AsFloat, this["height"].AsFloat);
            if (IsArray)
                return new Rect(this[0].AsFloat, this[1].AsFloat, this[2].AsFloat, this[3].AsFloat);
            return aDefault;
        }
        public Rect ReadRect()
        {
            return ReadRect(new Rect());
        }
        public JSONNode WriteRect(Rect aRect)
        {
            if (IsObject)
            {
                Inline = true;
                this["x"].AsFloat = aRect.x;
                this["y"].AsFloat = aRect.y;
                this["width"].AsFloat = aRect.width;
                this["height"].AsFloat = aRect.height;
            }
            else if (IsArray)
            {
                Inline = true;
                this[0].AsFloat = aRect.x;
                this[1].AsFloat = aRect.y;
                this[2].AsFloat = aRect.width;
                this[3].AsFloat = aRect.height;
            }
            return this;
        }
        #endregion Rect

        #region RectOffset
        public RectOffset ReadRectOffset(RectOffset aDefault)
        {
            if (this is JSONObject)
                return new RectOffset(this["left"].AsInt, this["right"].AsInt, this["top"].AsInt, this["bottom"].AsInt);
            if (this is JSONArray)
                return new RectOffset(this[0].AsInt, this[1].AsInt, this[2].AsInt, this[3].AsInt);
            return aDefault;
        }
        public RectOffset ReadRectOffset()
        {
            return ReadRectOffset(new RectOffset());
        }
        public JSONNode WriteRectOffset(RectOffset aRect)
        {
            if (IsObject)
            {
                Inline = true;
                this["left"].AsInt = aRect.left;
                this["right"].AsInt = aRect.right;
                this["top"].AsInt = aRect.top;
                this["bottom"].AsInt = aRect.bottom;
            }
            else if (IsArray)
            {
                Inline = true;
                this[0].AsInt = aRect.left;
                this[1].AsInt = aRect.right;
                this[2].AsInt = aRect.top;
                this[3].AsInt = aRect.bottom;
            }
            return this;
        }
        #endregion RectOffset

        #region Matrix4x4
        public Matrix4x4 ReadMatrix()
        {
            Matrix4x4 result = Matrix4x4.identity;
            if (IsArray)
            {
                for (int i = 0; i < 16; i++)
                {
                    result[i] = this[i].AsFloat;
                }
            }
            return result;
        }
        public JSONNode WriteMatrix(Matrix4x4 aMatrix)
        {
            if (IsArray)
            {
                Inline = true;
                for (int i = 0; i < 16; i++)
                {
                    this[i].AsFloat = aMatrix[i];
                }
            }
            return this;
        }
        #endregion Matrix4x4
    }
}
