using System;


namespace Morpho25.Settings
{
    public class Location
    {
        public const string TIMEZONE_LONGITUDE = "15.00000";
        public const string PROJECTION_SYSTEM = "GCS_WGS_1984 (lat/long)";
        public const string REALWORLD_POINT = "0.00000";

        private double _latitude;
        private double _longitude;
        private string _timeZone;
        private double _modelRotation;

        public string LocationName { get; }
        public double Latitude {
            get { return _latitude; }
            private set
            {
                if (value > 90.0 || value < -90.0)
                    throw new ArgumentOutOfRangeException(
                          $"{nameof(value)} must be in range (-90, 90).");

                _latitude = value;
            }
        }
        public double Longitude {
            get { return _longitude; }
            private set
            {
                if (value > 180.0 || value < -180.0)
                    throw new ArgumentOutOfRangeException(
                          $"{nameof(value)} must be in range (-180, 180).");

                _longitude = value;
            }
        }
        public string TimeZone
        {
            get { return _timeZone; }
            private set
            {
                int val = Convert.ToInt32(value);
                if (val > 14 || val < -12)
                    throw new ArgumentOutOfRangeException($"{nameof(value)} must be in range (-12, 14).");

                if (val > 0)
                    _timeZone = "UTC+" + value;
                else if (val < 0)
                    _timeZone = "UTC-" + value;
                else
                    _timeZone = "GMT";
            }
        }
        public double ModelRotation {
            get { return _modelRotation; }
            set
            {
                if (value > 360.0 || value < 0.0)
                    throw new ArgumentOutOfRangeException(
                          $"{nameof(value)} must be in range (0, 360) Clockwise.");

                _modelRotation = value;
            }
        }

        public Location(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
            LocationName = "Envimet Location";
            TimeZone = "0";
            ModelRotation = 0.0;
        }

        public Location(double latitude, double longitude, string locationName)
        {
            Latitude = latitude;
            Longitude = longitude;
            LocationName = locationName;
            TimeZone = "0";
            ModelRotation = 0.0;
        }

        public Location(double latitude, double longitude, string locationName, string timeZone)
        {
            Latitude = latitude;
            Longitude = longitude;
            LocationName = locationName;
            TimeZone = timeZone;
            ModelRotation = 0.0;
        }

        public Location(double latitude, double longitude, string locationName, string timeZone, double modelRotation)
        {
            Latitude = latitude;
            Longitude = longitude;
            LocationName = locationName;
            TimeZone = timeZone;
            ModelRotation = modelRotation;
        }

        public override string ToString()
        {
            return String.Format("{0}::{1}::{2}::{3}", LocationName, Latitude, Longitude, ModelRotation);
        }
    }
}
