using System.Runtime.Serialization;

namespace TaskApiCdek.Data
{
    [DataContract]
    public class TariffRequest
    {
        [DataMember(Name = "version")]
        public string? Version { get; set; }

        [DataMember(Name = "senderCityId")]
        public int SenderCityId { get; set; }

        [DataMember(Name = "receiverCityId")]
        public int ReceiverCityId { get; set; }

        [DataMember(Name = "dateExecute")]
        public string? DateExecute { get; set; }

        [DataMember(Name = "tariffId")]
        public string? TariffId { get; set; }

        [DataMember(Name = "goods")]
        public List<PackageSize> Packages { get; set; } = new List<PackageSize>();
    }
}
