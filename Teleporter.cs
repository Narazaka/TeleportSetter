
using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace net.narazaka.vrchat.teleport_setter
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class Teleporter : UdonSharpBehaviour
    {
        [NonSerialized]
        public TeleportSetter TeleportSetter;

        public override void Interact()
        {
            Teleport();
        }

        public void Teleport()
        {
            var target = TeleportSetter.TeleportTargetOf(this);
            if (target == null) return;
            Networking.LocalPlayer.TeleportTo(target.position, target.rotation);
        }
    }
}
