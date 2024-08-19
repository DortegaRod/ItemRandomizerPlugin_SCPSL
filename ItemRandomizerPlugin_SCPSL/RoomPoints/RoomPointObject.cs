using Exiled.API.Enums;
using Exiled.API.Features;
using System;
using UnityEngine;

namespace ItemRandomizerPlugin_SCPSL.RoomPoints {

    [Serializable]
    public class RoomPointObject {
        public RoomPointObject() {
        }

        public RoomPointObject(RoomType type, Vector3 relative) {
            this.roomType = type;
            this.relativePosition = relative;
        }

        public RoomPointObject(Vector3 mapPosition) {
            this.relativePosition = mapPosition;
        }

        public RoomType roomType = RoomType.Unknown;

        public SerializedVector3 relativePosition = Vector3.zero;
    }
}
