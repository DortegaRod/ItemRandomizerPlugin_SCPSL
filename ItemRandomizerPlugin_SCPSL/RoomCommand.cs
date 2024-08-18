using System;
using System.Windows.Input;
using CommandSystem;
using Exiled.API.Features;
using UnityEngine;

namespace JadeLib.Features.API.RoomPoint {
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class RoomPoint : CommandSystem.ICommand
    {
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response) {
            var player = Player.Get(sender);

            var cameraTransform = player.CameraTransform.transform;

            Physics.Raycast(cameraTransform.position, cameraTransform.forward, out var raycastHit, 100f);

            var point = new RoomPointObject(raycastHit.point + (Vector3.up * 0.1f));

            response = $"\nThe position you are looking at as RoomPoint:" +
                       $"\n  RoomType: {point.roomType}" +
                       $"\n  X: {point.relativePosition.X}" +
                       $"\n  Y: {point.relativePosition.Y}" +
                       $"\n  Z: {point.relativePosition.Z}";

            return true;
        }

        public string Command { get; } = "roompoint";

        public string[] Aliases { get; } = { "rp" };

        public string Description { get; } = "Gets the local position you're looking with the camera";
    }
}