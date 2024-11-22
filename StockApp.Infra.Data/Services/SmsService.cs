using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace StockApp.Domain.Interfaces
{
    public class SmsService : ISmsService
    {
        private readonly string _accountSid = "AC47a894d21c72dc8dce8f6733d5324fea";
        private readonly string _authToken = "35182dff0f81b617ab7996dc5c0884de";
        private readonly string _fromPhoneNumber = "+12165087258";

        public SmsService()
        {
            TwilioClient.Init(_accountSid, _authToken);
        }

        public async Task SendSmsAsync(string phoneNumber, string message)
        {
            try
            {
                var smsService = new SmsService();
                var messageResource = await MessageResource.CreateAsync(
                        body: message,
                        from: new Twilio.Types.PhoneNumber(_fromPhoneNumber),
                        to: new Twilio.Types.PhoneNumber(phoneNumber)
                    );
                Console.WriteLine($"SMS enviado para {phoneNumber}. SID: {messageResource.Sid}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao enviar SMS: {ex.Message}");
            }
        }
    }
}
