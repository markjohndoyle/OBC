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
            byte[] tm = new byte[sizeof(int) + sizeof(long) + payload.Length];
            byte[] idBytes = BitConverter.GetBytes(id);
            Array.Reverse(idBytes);

            //Find unix timestamp (seconds since 01/01/1970)
            long ticks = DateTime.UtcNow.Ticks - DateTime.Parse("01/01/1970 00:00:00").Ticks;
            ticks /= 10000000; //Convert windows ticks to seconds
            byte[] timeStampBytes = BitConverter.GetBytes(ticks);
            Array.Reverse(timeStampBytes);

            Array.Copy(idBytes, 0, tm, 0, idBytes.Length);
            Array.Copy(timeStampBytes, 0, tm, idBytes.Length, timeStampBytes.Length);
            Array.Copy(payload, 0, tm, idBytes.Length + timeStampBytes.Length, payload.Length);
            return tm;
        }


    }
}
