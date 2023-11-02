using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace net.narazaka.vrchat.teleport_setter
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class TeleportSetter : UdonSharpBehaviour
    {
        [SerializeField]
        public Teleporter[] Teleporters;
        [SerializeField]
        public Transform[] TeleportTargets;
        [SerializeField]
        public int[] TeleportTargetIndexes;

        void OnEnable()
        {
            foreach (var teleporter in Teleporters)
            {
                teleporter.TeleportSetter = this;
            }
        }

        public Transform TeleportTargetOf(Teleporter teleporter)
        {
            var teleporterIndex = Array.IndexOf(Teleporters, teleporter);
            if (teleporterIndex < 0 || teleporterIndex >= TeleportTargetIndexes.Length)
            {
                Debug.LogError("Teleporter not found");
                return null;
            }
            var teleportTargetIndex = TeleportTargetIndexes[teleporterIndex];
            if (teleportTargetIndex >= TeleportTargets.Length)
            {
                Debug.LogError("Teleport target not found");
                return null;
            }
            if (teleportTargetIndex < 0)
            {
                Debug.LogError("Teleport target not set");
                return null;
            }
            return TeleportTargets[teleportTargetIndex];
        }
    }
}
