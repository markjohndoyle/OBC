using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSP_OBC.telemetry {
    
    class CompleteVesselLayout {

        private CompleteVesselLayout() {
            // Utility class.
        }

        public static int TOTAL_SIZE_OF_LAYOUT = sizeof(double) * 7 + sizeof(float) * 6 + sizeof(int);
        
        public static int ACC_X_OFFSET = 0;
        public static int ACC_Y_OFFSET = sizeof(double);
        public static int ACC_Z_OFFSET = sizeof(double) + ACC_Y_OFFSET;
        public static int ANGV_X_OFFSET = sizeof(double) + ACC_Z_OFFSET;
        public static int ANGV_Y_OFFSET = sizeof(float) + ANGV_X_OFFSET;
        public static int ANGV_Z_OFFSET = sizeof(float) + ANGV_Y_OFFSET;
        public static int ANGM_X_OFFSET = sizeof(float) + ANGV_Z_OFFSET;
        public static int ANGM_Y_OFFSET = sizeof(float) + ANGM_X_OFFSET;
        public static int ANGM_Z_OFFSET = sizeof(float) + ANGM_Y_OFFSET;
        public static int CUR_STAGE_OFFSET = sizeof(float) + CUR_STAGE_OFFSET;
        public static int VSPEED_OFFSET = sizeof(int) + CUR_STAGE_OFFSET;
        public static int ALTITUDE_OFFSET = sizeof(double) + VSPEED_OFFSET;
        public static int LONGITUDE_OFFSET = sizeof(double) + ALTITUDE_OFFSET;
        public static int LATITUDE_OFFSET = sizeof(double) + LONGITUDE_OFFSET;
    }
}
