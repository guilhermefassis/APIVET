using Newtonsoft.Json;

namespace VetApi.Models
{
    public partial class Breed
    {
        [JsonProperty("weight")]
        public Eight Weight { get; set; }

        [JsonProperty("height")]
        public Eight Height { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("bred_for")]
        public string BredFor { get; set; }

        [JsonProperty("breed_group")]
        public string BreedGroup { get; set; }

        [JsonProperty("life_span")]
        public string LifeSpan { get; set; }

        [JsonProperty("temperament")]
        public string Temperament { get; set; }

        [JsonProperty("reference_image_id")]
        public string ReferenceImageId { get; set; }
    }

    public partial class Eight
    {
        [JsonProperty("imperial")]
        public string Imperial { get; set; }

        [JsonProperty("metric")]
        public string Metric { get; set; }
    }
}