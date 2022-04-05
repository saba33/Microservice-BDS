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


        public async Task<bool> SendSms(SmsSentRequest sentRequest)
        {
        
            var numbers = sentRequest.receivers.ToList();
            using (var client = new HttpClient())
            {
                try 
                {
                        var model = new SentSmsRequestWithSender() { receivers = sentRequest.receivers , sender = _sender, text = sentRequest.text};
                        client.DefaultRequestHeaders.Add("Authorization", _apiKey);
                        var jsonModel = JsonConvert.SerializeObject(model);
                        var dataModel = new StringContent(jsonModel, Encoding.UTF8, "application/json");
                        var responseModel = await client.PostAsync(_url, dataModel);

                    foreach (var number in numbers)
                    {
                        MessagesInfo info = new MessagesInfo() { Id = Guid.NewGuid(), PhoneNumber = number, Text = sentRequest.text , CreateOn = DateTime.Now};
                        await _repo.AddAsync(info);
                    }
                }
                catch (Exception)
                {

                    return false;
                }
            }

            return await Task.FromResult(true);
        }

        public async Task<string> SendOtp(SendOtpRequest otpSentRequestModel)
        {
            var otp = GenerateOtp();
            SentSmsRequestWithSender smsModelToInsert = new SentSmsRequestWithSender();
            smsModelToInsert.sender = _sender;
            smsModelToInsert.text = otpSentRequestModel.text + otp.ToString();
            smsModelToInsert.receivers = otpSentRequestModel.receivers;
            var json = JsonConvert.SerializeObject(smsModelToInsert);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            string sessionId;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", _apiKey);
                var response = await client.PostAsync(_url, data);
                sessionId = await response.Content.ReadAsStringAsync();
            }
            SmsSentHistoryWithotp sentRequest = new SmsSentHistoryWithotp();
            sentRequest.Otp = otp;
            sentRequest.Id = Guid.NewGuid();
            sentRequest.CreatedOn = DateTime.Now;
            sentRequest.ValidateOn = DateTime.Now;
            sentRequest.PhoneNumber = smsModelToInsert.receivers.FirstOrDefault();
            sentRequest.Text = String.Format(smsModelToInsert.text);
            sentRequest.SmsProvider = FormatString(sessionId);
            sessionId = FormatString(sessionId);
            await _repo.AddAsync(sentRequest);
            return sessionId;
            //return Guid.Parse(sessionId);
        }

        public async Task<bool> ValidateOtp(ValidateOtpRequest request)
        {
            return await _repo.GetAsyncWithSessionId(request);
        }


        #endregion

        #region Additional public functions


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

        string FormatString(string text)
        {
            string myString = text;
            int startIndex = myString.Length - 42;
            int endIndex = myString.Length - startIndex - 2;
            string id = myString.Substring(startIndex, endIndex);
            return id;
        }

       
        #endregion

    }
}
