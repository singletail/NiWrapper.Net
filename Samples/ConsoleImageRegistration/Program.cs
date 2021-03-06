﻿using System;
using OpenNIWrapper;

namespace ConsoleImageRegistration
{
    #region

    #endregion

    public static class Program
    {
        #region Public Methods and Operators

        public static void Main(string[] args)
        {
            try
            {
                OpenNI.Initialize();
                device = Device.Open(Device.AnyDevice);

                depthStream = device.CreateVideoStream(Device.SensorType.Depth);
                depthStream.VideoMode = new VideoMode
                {
                    DataPixelFormat = VideoMode.PixelFormat.Depth1Mm,
                    Fps = 30,
                    Resolution = new Size(640, 480)
                };

                colorStream = device.CreateVideoStream(Device.SensorType.Color);
                colorStream.VideoMode = new VideoMode
                {
                    DataPixelFormat = VideoMode.PixelFormat.Rgb888,
                    Fps = 30,
                    Resolution = new Size(640, 480)
                };
                device.DepthColorSyncEnabled = true;
                depthStream.Start();
                colorStream.Start();
                device.ImageRegistration = Device.ImageRegistrationMode.DepthToColor;

                Console.WriteLine("Image registration is active and working well.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();

            if (device != null)
            {
                device.Close();
            }

            OpenNI.Shutdown();
        }

        #endregion

        #region Static Fields

        private static VideoStream colorStream;

        private static VideoStream depthStream;

        private static Device device;

        #endregion
    }
}