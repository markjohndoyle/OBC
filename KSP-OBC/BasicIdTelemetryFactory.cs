using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KSP_OBC {
    class BasicIdTelemetryFactory : TelemetryFactory {

        public static int VSPEED = 1;

        public byte[] createTelemetry(int id, byte[] payload) {
            byte[] tm = new byte[sizeof(int) + payload.Length];
            byte[] idBytes = BitConverter.GetBytes(id);
            Array.Copy(idBytes, 0, tm, 0, idBytes.Length);
            Array.Copy(payload, 0, tm, idBytes.Length, payload.Length);
            return tm;
        }


    }
}
