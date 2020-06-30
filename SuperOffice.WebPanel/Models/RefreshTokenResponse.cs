using System;
using System.Collections.Generic;
using System.Text;

namespace SuperOffice.WebPanel.Models
{
    public class RefreshTokenResponse
    {
        public string token_type { get; set; }
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string id_token { get; set; }
    }
}
