using MediLast.Abstractions.Interfaces;

namespace MediLast.Services
{
    public class SmsService : ISmsService
    {
        private readonly HttpClient _httpClient;

        public SmsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<bool> SendSmsAsync(string phoneNumber, string messageBody)
        {
            var values = new Dictionary<string, string>
            {
                { "secret", "" },
                { "device", "00000000-0000-0000-fd6c-2c18c58338f3" },
                { "sim", "1" },
                { "mode", "devices" },
                { "phone", phoneNumber },
                { "message", messageBody }
            };

            var content = new FormUrlEncodedContent(values);
            var response = await _httpClient.PostAsync("https://hahu.io/api/send/sms", content);

            return response.IsSuccessStatusCode;
        }
    }
}
