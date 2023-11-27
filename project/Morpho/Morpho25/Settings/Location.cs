using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Morpho25.Settings
{

    public class UTM
    {
        public UTM(double utmEsting, double utmNorthing, string utmZone)
        {
            UTMesting = utmEsting;
            UTMnorthing = utmNorthing;
            UTMzone = utmZone;
        }
        public double UTMesting { get; }
        public double UTMnorthing { get; }
        public string UTMzone { get; }

        public override string ToString()
        {
            return String.Format("UTM coordinate::{0}", UTMzone);
        }
    }


    public class Location : IEquatable<Location>
    {
        public const double TIMEZONE_LONGITUDE = 15.00000;
        public const string PROJECTION_SYSTEM = " ";
        public const string REALWORLD_POINT = "0.00000";

        private double _latitude;
        private double _longitude;
        private string _timeZone;
        private double _modelRotation;
        private double _timezoneRefence;

        [JsonIgnore]
        public UTM UTM { get; set; }

        [DisplayName("Name")]
        [Description("Location name.")]
        [JsonProperty("locationName")]
        public string LocationName { get; set; }

        [DisplayName("Latitude")]
        [Description("Latitude in deg.")]
        [Range(-90, 90, ErrorMessage = "Number in range(-90, 90) is required.")]
        [JsonProperty("latitude", Required = Required.Always)]
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

        [DisplayName("Longitude")]
        [Description("Longitude in deg.")]
        [Range(-180, 180, ErrorMessage = "Number in range(-180, 180) is required.")]
        [JsonProperty("longitude", Required = Required.Always)]
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

        [DisplayName("Timezone reference")]
        [Description("Longitude reference for time zone.")]
        [Range(-180, 180, ErrorMessage = "Number in range(-180, 180) is required.")]
        [JsonProperty("timezoneReference")]
        public double TimezoneReference
        {
            get { return _timezoneRefence; }
            set
            {
                if (value > 180.0 || value < -180.0)
                    throw new ArgumentOutOfRangeException(
                          $"{nameof(value)} must be in range (-180, 180).");

                _timezoneRefence = value;
            }
        }

        [DisplayName("Timezone")]
        [Description("Timezone.")]
        [JsonProperty("timeZone")]
        public string TimeZone
        {
            get { return _timeZone; }
            set
            {
                int val;
                if (value == "GMT")
                {
                    val = 0;
                } 
                else if (value.StartsWith("UTC"))
                {
                    var num = value.Split('C').Last();
                    val = Convert.ToInt32(num);
                } else
                {
                    val = Convert.ToInt32(value);
                }

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


        [DisplayName("Rotation")]
        [Description("Model rotation clockwise in deg.")]
        [Range(0, 360, ErrorMessage = "Number in range(0, 360) is required.")]
        [JsonProperty("modelRotation")]
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
            TimezoneReference = Location.TIMEZONE_LONGITUDE;
        }

        [JsonConstructor]
        public Location(double latitude, 
                        double longitude, 
                        string locationName="EnvimetLocation", 
                        string timeZone="0", 
                        double modelRotation=0,
                        double timezoneReference=Location.TIMEZONE_LONGITUDE) :
            this(latitude, longitude)
        {
            LocationName = locationName;
            TimeZone = timeZone;
            ModelRotation = modelRotation;
            TimezoneReference = timezoneReference;
        }


        public override string ToString()
        {
            return String.Format("{0}::{1}::{2}::{3}", LocationName, 
                Latitude, Longitude, ModelRotation);
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static Location Deserialize(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<Location>(json);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Equals(Location other)
        {
            if (other == null)
                return false;

            if (other != null
                && other.LocationName == this.LocationName
                && other.TimeZone == this.TimeZone
                && other.Latitude == this.Latitude
                && other.Longitude == this.Longitude
                && other.TimezoneReference == this.TimezoneReference
                && other.ModelRotation == this.ModelRotation)
                return true;
            else
                return false;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            var locObj = obj as Location;
            if (locObj == null)
                return false;
            else
                return Equals(locObj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + LocationName.GetHashCode();
                hash = hash * 23 + TimeZone.GetHashCode();
                hash = hash * 23 + Latitude.GetHashCode();
                hash = hash * 23 + Longitude.GetHashCode();
                hash = hash * 23 + TimezoneReference.GetHashCode();
                hash = hash * 23 + ModelRotation.GetHashCode();
                return hash;
            }
        }

        public static bool operator ==(Location loc1, Location loc2)
        {
            if (((object)loc1) == null || ((object)loc2) == null)
                return Object.Equals(loc1, loc2);

            return loc1.Equals(loc2);
        }

        public static bool operator !=(Location loc1, Location loc2)
        {
            return !(loc1 == loc2);
        }
    }
}
