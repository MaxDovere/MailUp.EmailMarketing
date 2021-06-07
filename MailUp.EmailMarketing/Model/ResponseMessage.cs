using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryMailUp.Models
{
    public class ResponseMessage : HttpResponseMessage
    {
        public new string Content { get; set; } 
        public string Type { get; set; }
    }
}
