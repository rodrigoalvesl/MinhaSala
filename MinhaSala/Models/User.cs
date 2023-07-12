using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MinhaSala.Models
{

    public class User
    {
        public string Matricula { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
    }

    public class Record
    {
        [JsonProperty("fields")]
        public User Users { get; set; }
    }

    public class Root
    {

        [JsonProperty("records")]
        public List<Record> Records { get; set; }
    }
}

