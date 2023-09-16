using System.Runtime.Serialization;

namespace TaskApiCdek.Data
{
    [DataContract]
    public class PackageSize
    {
        [DataMember(Name = "weight")]
        public double Weight { get; set; }

        [DataMember(Name = "height")]
        public int Height { get; set; }

        [DataMember(Name = "width")]
        public int Width { get; set; }

        [DataMember(Name = "length")]
        public int Length { get; set; }
    }
}
