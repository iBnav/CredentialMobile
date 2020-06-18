using System;
using System.Collections.Generic;
using System.Text;

namespace AbbottCredentialMobile.Classes
{
    public class Pessoa
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public long CRM { get; set; }
        public long upi { get; set; }
        public long CPF { get; set; }
        public string Estado { get; set; }
    }
}
