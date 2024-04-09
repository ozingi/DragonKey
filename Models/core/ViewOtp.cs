using OtpNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonKey.Models.core
{
    internal class ViewOtp
    {
        public string Filename { get; set; }
        public string SecretKey { get; set; }
        public string CountDown { get; set; }
        public double progressbar { get; set; }
        public string FakeCountDown { get; set; }
        public string TotpCode { get; set; }
    }

}
