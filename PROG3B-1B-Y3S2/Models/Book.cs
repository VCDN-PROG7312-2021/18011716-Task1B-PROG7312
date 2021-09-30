using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PROG3B_1B_Y3S2.Models
{
    public partial class Book
    {
        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("ddc")]
        public string Ddc { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        public Book(string image, string ddc, string title)
        {
            this.Image = image;
            this.Ddc = ddc;
            this.Title = title;
        }
    }

    //public partial class Book
    //{
    //    public static Book FromJson(string json) => JsonConvert.DeserializeObject<Book>(json, PROG3B_1B_Y3S2.c.Converter.Settings);
    //}

    //public static class Serialize
    //{
    //    public static string ToJson(this Book self) => JsonConvert.SerializeObject(self, PROG3B_1B_Y3S2.Models.Converter.Settings);
    //}

    //internal static class Converter
    //{
    //    public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
    //    {
    //        MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
    //        DateParseHandling = DateParseHandling.None,
    //        Converters =
    //        {
    //            new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
    //        },
    //    };
    //}
}
