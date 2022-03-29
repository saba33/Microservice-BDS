using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMSSenderManagement.Domain.Requests;
using SMSSenderManagement.Services.Abstractions;

namespace SMSSenderManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SmsManagement : ControllerBase
    {
        private readonly ISmsService _smsService;
        #region ctor
        public SmsManagement(ISmsService smsService)
        {
            _smsService = smsService;
        }
        #endregion

        //public async Task<IActionResult> SendOtp()
        //{
        //    return Ok();
        //}

        //public async Task<IActionResult> ValidateOtp()
        //{
        //    return Ok();
        //}


        //[HttpPost]
        //public async Task<IActionResult> SentSms(SentSmsRequest smsModel)
        //{
        //    var result = await _smsService.SendSms(smsModel);
        //    if (result == false)
        //        return BadRequest("Request is not Valid please check and try again");
        //    else
        //        return Ok(result);
        //}

        [HttpPost]
        public async Task<IActionResult> SentOpt(SendOtpRequest otpModelRequest)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    await _smsService.SendOtp(otpModelRequest);
                }
                catch (Exception e)
                {
                     return BadRequest(e.Message);
                }
            }
            return Ok();
        }

    }

    //send otp
    //db - Id - guid, PhoneNumber, Text, Otp, CreatedOn = datetime.now, ValidateOn = datetime.now, smsprovider id
    //public class SendOtpRequest
    //{
    //    public string PhoneNumber { get; set; }

    //    public string SmsFormat { get; set; }
    //}

    //public class SendOtpResponse
    //{
    //    public Guid SessionId { get; set; }
    //}

    ////validate otp

    //public class ValidateOtpRequest
    //{
    //    public Guid SessionId { get; set; }

    //    public string Otp { get; set; }
    //}


    ////send sms

    ////Id, PhoneNumber, Text, CreateOn
    //public class SendSmsRequest1
    //{
    //    public string[] PhoneNumbers { get; set; }

    //    public string SmsText { get; set; }
    //}
}
