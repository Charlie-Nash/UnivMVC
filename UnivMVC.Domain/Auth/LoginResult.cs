﻿using System.Net;

namespace UnivMVC.Domain.Auth
{
    public class LoginResult
    {
        public string usr { get; set; } = "";
        public string secreto { get; set; } = "";
        public bool tfa { get; set; } = true;

        public HttpStatusCode status { get; set; }
        public string mensaje { get; set; } = "";
    }
}
