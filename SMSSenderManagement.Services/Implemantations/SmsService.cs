using SMSSenderManagement.Domain.Requests;
using SMSSenderManagement.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SMSSenderManagement.Domain.SMSResoinseModel;
using Microsoft.Extensions.Options;
using SMSSenderManagement.Domain;
using SMSSenderManagement.Repository.Abstractions;

namespace SMSSenderManagement.Services.Implemantations
{
    public class SmsService : ISmsService
    {
        #region private members
        private readonly string _apiKey;
        private readonly string _sender;
        private readonly string _url;
        private readonly ISmsRepository _repo;
        #endregion

        #region ctor
        public SmsService(IOptions<ApiTokenInfo> options, ISmsRepository repo)
        {
            _apiKey = options.Value.Authorization;
            _sender = options.Value.sender;
            _url = options.Value.Url;
            _repo = repo;
        }
        #endregion

        #region public members
        public async Task<bool> SendSms(SentSmsRequest sentRequest)
        {
            var otp = GenerateOtp();
            SentSmsRequestWithSender smsModelToInsert = new SentSmsRequestWithSender();
            smsModelToInsert.sender = _sender;
            smsModelToInsert.text = $"Hello, your otp is {otp}";
            smsModelToInsert.receivers = sentRequest.receivers;

            SmsSentHistoryWithotp otpValidationModel = new SmsSentHistoryWithotp();
            otpValidationModel.Otp = otp;
            otpValidationModel.Id = Guid.NewGuid();

            var json = JsonConvert.SerializeObject(smsModelToInsert);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", _apiKey);
                var response = await client.PostAsync(_url, data);
                var message = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    await _repo.AddAsync(otpValidationModel);
                    return true;
                }
            }
            return await Task.FromResult(false);
        }

        public async Task<string> SendOtp(SendOtpRequest otpSentRequestModel)
        {
            var otp = GenerateOtp();
            SentSmsRequestWithSender smsModelToInsert = new SentSmsRequestWithSender();
            smsModelToInsert.sender = _sender;
            smsModelToInsert.text = smsModelToInsert.text + otp.ToString();
            smsModelToInsert.receivers = otpSentRequestModel.receivers;
            var json = JsonConvert.SerializeObject(smsModelToInsert);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            string message;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", _apiKey);
                var response = await client.PostAsync(_url, data);
                message = await response.Content.ReadAsStringAsync();
            }
            SmsSentHistoryWithotp sentRequest = new SmsSentHistoryWithotp();
            sentRequest.Otp = otp;
            sentRequest.Id = Guid.NewGuid();
            sentRequest.CreatedOn = DateTime.Now;
            sentRequest.ValidateOn = DateTime.Now;
            sentRequest.PhoneNumber = smsModelToInsert.receivers.FirstOrDefault();
            sentRequest.Text = String.Format(smsModelToInsert.text + otp);
            sentRequest.SmsProvider = FormatString(message);
            message = FormatString(message);
            await _repo.AddAsync(sentRequest);
            return message;
        }
        #region public members
        string GenerateOtp()
        {
            Random rnd = new Random();
            return (rnd.Next(1000, 9999)).ToString();
        }
        StringContent RequestToJson(SendOtpRequest otpSentRequestModel)
        {
            var json = JsonConvert.SerializeObject(otpSentRequestModel);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            return data;
        }


        #endregion
        #endregion

        string FormatString(string text)
        {
            string myString = text;
            int startIndex = myString.Length - 42;
            int endIndex = myString.Length - startIndex - 2;
            string id = myString.Substring(startIndex , endIndex);
            return id;
        }

    }
}
