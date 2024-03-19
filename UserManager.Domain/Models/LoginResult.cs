using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace UserManager.Domain.Models
{
    public class LoginResult
    {
        [JsonIgnore]
        public bool Valid
        {
            get
            {
                return !string.IsNullOrEmpty(AccessToken);
            }
        }

        [JsonPropertyName("access_token")]
        public string AccessToken { get; internal set; } = string.Empty;

        public LoginResult(string accessToken)
        {
            AccessToken = accessToken;
        }
    }
}
