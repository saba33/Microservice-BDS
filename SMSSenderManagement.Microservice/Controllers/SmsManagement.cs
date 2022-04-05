using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMSSenderManagement.Domain;
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

  

        [HttpPost("/SendOtp")]
        public async Task<IActionResult> SentOpt(SendOtpRequest otpModelRequest)
        {
            string result;
            if (ModelState.IsValid)
            {
                try
                {
                    result = await _smsService.SendOtp(otpModelRequest);
                    return Ok(result);
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }

            return BadRequest("Modelstate is not valid");
        }


        [HttpPost("ValidateOtp")]
        public async Task<IActionResult> ValidateOtp(ValidateOtpRequest request)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var result =  await _smsService.ValidateOtp(request);
                    if(result != true)
                        return Ok(result);
                    return Ok(result);
                }
                catch (Exception e)
                {
                    throw new Exception();
                }
            }
            return BadRequest();
        }

        [HttpPost("/SendSms")]
        public async Task<IActionResult> SentSms(SmsSentRequest request)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _smsService.SendSms(request);
                    if (result != true)
                        return Ok(result);
                    return Ok(result);
                }
                catch (Exception e)
                {
                    throw new Exception();
                }
            }
            return BadRequest();
        }

    }

}
