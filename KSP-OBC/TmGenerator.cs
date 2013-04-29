using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using KSP.IO;
using UnityEngine;
using KSP_OBC.telemetry;

using TML = KSP_OBC.telemetry.CompleteVesselLayout;

namespace KSP_OBC  {

    class TmGenerator {
        private Thread tmThread;
        private bool broadcasting = false;
        private TelemetryFactory tmFactory = new BasicIdTelemetryFactory();
        private SendLink sendLink = new UdpBroadcaster();
        public Vessel vessel { get; set; }

        public double verticalSpeed { get; set; }
        public double altitude { get; set; }
        public double atmDensity { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public int currentStage { get; set; }
        public Vector3 angularMomentum { get; set; }
        public Vector3 angularVelocity { get; set; }
        public Vector3d acceleration { get; set; }

        public void startTelemetry() {
            Debug.Log("Starting telemetry");
            broadcasting = true;
            tmThread = new Thread(new ThreadStart(sendTm));
            tmThread.Start();
        }

        private void stopSendingTelemetry() {
            broadcasting = false;
        }

        private void sendTm() {
            while (broadcasting) {
                if (HighLogic.LoadedSceneIsFlight) {
                    byte[] buffer = createCompleteVesselTmArray();
                    byte[] tm = tmFactory.createTelemetry(BasicIdTelemetryFactory.VESSEL, buffer);
                    //Array.Reverse(tm);
                    sendLink.send(tm);
                }
                Thread.Sleep(TimeSpan.FromSeconds(2));
            }
            Debug.Log("Broadcast thread is ending; exiting now");
        }

        /**
         * Creates an array of parameters comprising of the complete vessel payload.
         */
        private byte[] createCompleteVesselTmArray() {
            Debug.Log("creating complete vessel tm");
            byte[] buffer = new byte[TML.TOTAL_SIZE_OF_LAYOUT];
            Debug.Log("Buffer size = " + TML.TOTAL_SIZE_OF_LAYOUT);
            MemoryStream writer = new MemoryStream(buffer);

            Debug.Log(writer.Length + " length of writer");

            try {
                Debug.Log("Adding acceleration " + acceleration);
                writeParameterToBuffer(writer, BitConverter.GetBytes(acceleration.x));
                writeParameterToBuffer(writer, BitConverter.GetBytes(acceleration.y));
                writeParameterToBuffer(writer, BitConverter.GetBytes(acceleration.z));

                Debug.Log("Adding angular vel " + angularVelocity);
                writeParameterToBuffer(writer, BitConverter.GetBytes(angularVelocity.x));
                writeParameterToBuffer(writer, BitConverter.GetBytes(angularVelocity.y));
                writeParameterToBuffer(writer, BitConverter.GetBytes(angularVelocity.z));

                Debug.Log("Adding angular mom " + angularMomentum);
                writeParameterToBuffer(writer, BitConverter.GetBytes(angularMomentum.x));
                writeParameterToBuffer(writer, BitConverter.GetBytes(angularMomentum.y));
                writeParameterToBuffer(writer, BitConverter.GetBytes(angularMomentum.z));

                Debug.Log("Adding stage " + currentStage);
                writeParameterToBuffer(writer, BitConverter.GetBytes(currentStage));

                Debug.Log("Adding vspeed " + verticalSpeed);
                writeParameterToBuffer(writer, BitConverter.GetBytes(verticalSpeed));

                Debug.Log("Adding altitude " + altitude);
                writeParameterToBuffer(writer, BitConverter.GetBytes(altitude));

                Debug.Log("Adding longitude " + longitude);
                writeParameterToBuffer(writer, BitConverter.GetBytes(longitude));

                Debug.Log("Adding latitude " + latitude);
                writeParameterToBuffer(writer, BitConverter.GetBytes(latitude));
            }
            catch (Exception e) {
                Debug.Log("Exception creating complete TM buffer : " + e.ToString());
                throw e;
            }
            Debug.Log("complete tm created!");

            return writer.ToArray();
        }

        private static void writeParameterToBuffer(MemoryStream s, byte[] p) {
            Debug.Log("Writer length: " + s.Length + " writer pos: " + s.Position + " length:" + p.Length);
            Array.Reverse(p);
            s.Write(p, 0, p.Length);
        }

    } // end class

} // end namespace
