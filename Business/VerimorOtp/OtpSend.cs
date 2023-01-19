using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using Entities.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Business.VerimorOtp
{
    public class OtpSend:IOtpSend
    {
 
        public IResult SendOtp(VerimorOtpSend verimorOtpSend)
        {
            var smsIstegi = new SmsIstegi();
            smsIstegi.username = "908502420134";
            smsIstegi.password = "6PrP7SY2Wd";
            smsIstegi.source_addr = "08502420134";
            smsIstegi.messages = new Mesaj[] { new Mesaj(verimorOtpSend.Mesaj, verimorOtpSend.UserPhone) };
            IstegiGonder(smsIstegi);
            return new SuccessDataResult<VerimorOtpSend>(verimorOtpSend,smsIstegi.messages.ToString(),200);
        }

        private void IstegiGonder(SmsIstegi istek)
        {
            string payload = JsonConvert.SerializeObject(istek);
            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            wc.Headers["Content-Type"] = "application/json";
            try
            {
                string campaign_id = wc.UploadString("http://sms.verimor.com.tr/v2/send.json", payload);
                string mesaj = "Mesaj Gönderildi, kampanya_id" + campaign_id;
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError) // 400 hataları
                {
                    var responseBody = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                    string mesaj = "Mesaj gönderilemedi, dönen hata: " + responseBody;
                }
                else // diğer hatalar
                {
                    var mesaj = ex.Status;
                    throw;
                }
            }
        }

        class Mesaj
        {
            public string msg { get; set; }
            public string dest { get; set; }

            public Mesaj() { }

            public Mesaj(string msg, string dest)
            {
                this.msg = msg;
                this.dest = dest;
            }
        }
        class SmsIstegi
        {
            public string username { get; set; }
            public string password { get; set; }
            public string source_addr { get; set; }
            public Mesaj[] messages { get; set; }
        }
    }
}
