using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.DTOs.Facebook
{
    public class FacebookUserAccessTokenValidationDTO
    {
        [JsonPropertyName("data")]
        public FacebookUserAccessTokenValidationData Data { get; set; }
    }

    public class FacebookUserAccessTokenValidationData
    {
        [JsonPropertyName("data.is_valid")]
        public bool IsValid { get; set; }

        [JsonPropertyName("data.user_id")]
        public string UserId { get; set; }
    }
}
