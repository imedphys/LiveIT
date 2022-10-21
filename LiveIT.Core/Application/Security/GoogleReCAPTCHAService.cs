using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LiveIT.Security
{
    public class GoogleReCAPTCHAService
    {
        private ReCAPTCHASettings _settings;

        public GoogleReCAPTCHAService(IOptions<ReCAPTCHASettings> settings)
        {
            _settings = settings.Value;
        }

        public virtual async Task<GoogleCAPTCHAResponse> VerifyReCAPTCHA(string _Token)
        {
            GoogleReCAPTCHAData _MyData = new GoogleReCAPTCHAData
            {
                response = _Token,
                secret = _settings.ReCAPTCHA_Secret_Key
            };

            HttpClient client = new HttpClient();

            var response = await client.GetStringAsync($"https://www.google.com/recaptcha/api/siteverify?secret={_MyData.secret}&response={_MyData.response}");

            var capresp = JsonConvert.DeserializeObject<GoogleCAPTCHAResponse>(response);

            return capresp;
        }
    }
    public class GoogleReCAPTCHAData
    {
        public string response { get; set; }//Token
        public string secret { get; set; }
    }

    public class GoogleCAPTCHAResponse
    {
        public bool success { get; set; }
        public double score { get; set; }
        public string action { get; set; }
        public DateTime challenge_ts { get; set; }
        public string hostname { get; set; }
    }
}
