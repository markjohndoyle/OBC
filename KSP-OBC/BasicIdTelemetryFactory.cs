using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KSP_OBC {

    class BasicIdTelemetryFactory : TelemetryFactory {

        public static int VESSEL = 0;
        public static int VSPEED = 1;

        public byte[] createTelemetry(int id, byte[] payload) {
            byte[] tm = new byte[sizeof(int) + sizeof(long) + payload.Length];

            // Add the payload ID as Big endian
            byte[] idBytes = BitConverter.GetBytes(id);
            Array.Reverse(idBytes);

            // Add the generation timestamp as Big endian
            byte[] timeStampBytes = createTimestamp();
            Array.Reverse(timeStampBytes);

            // Copy the ID, timestamp, and payload into the complete TM packet.
            Array.Copy(idBytes, 0, tm, 0, idBytes.Length);
            Array.Copy(timeStampBytes, 0, tm, idBytes.Length, timeStampBytes.Length);
            Array.Copy(payload, 0, tm, idBytes.Length + timeStampBytes.Length, payload.Length);

            return tm;
        }

        private byte[] createTimestamp() {
            // Find unix timestamp (seconds since 01/01/1970)
            long ticks = DateTime.UtcNow.Ticks - DateTime.Parse("01/01/1970 00:00:00").Ticks;
            // Convert windows ticks to seconds
            ticks /= 10000000;
            byte[] timeStampBytes = BitConverter.GetBytes(ticks);
            return timeStampBytes;
        }
    }
}
