using System;
using UnityEngine;

namespace JadeLib.Features.API.RoomPoint {
    [Serializable]
    public class SerializedVector3 {
        public SerializedVector3(Vector3 vector) {
            this.X = vector.x;
            this.Y = vector.y;
            this.Z = vector.z;
        }

        public SerializedVector3(float x, float y, float z) {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public SerializedVector3() {
        }

        public Vector3 Parse() {
            return new Vector3(this.X, this.Y, this.Z);
        }

        public float X { get; set; }

        public float Y { get; set; }

        public float Z { get; set; }

        public static implicit operator Vector3(SerializedVector3 vector) {
            return vector?.Parse() ?? Vector3.zero;
        }

        public static implicit operator SerializedVector3(Vector3 vector) {
            return new SerializedVector3(vector);
        }

        public static implicit operator SerializedVector3(Quaternion rotation) {
            return new SerializedVector3(rotation.eulerAngles);
        }

        public static implicit operator Quaternion(SerializedVector3 vector) {
            return Quaternion.Euler(vector);
        }
    }
}