using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSP_OBC {
    interface TelemetryFactory {
        byte[] createTelemetry(int id, byte[] payload);
    }
}
