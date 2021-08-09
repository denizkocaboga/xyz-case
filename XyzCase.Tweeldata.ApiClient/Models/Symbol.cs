using RestSharp.Deserializers;
using System.Collections.Generic;

namespace XyzCase.Tweeldata.ApiClient.Models
{
    public class Symbol
    {
        public Meta Meta { get; set; }

        [DeserializeAs(Name = "values")]
        public IEnumerable<PriceInfo> Prices { get; set; }
        public string Status { get; set; }
    }
}
