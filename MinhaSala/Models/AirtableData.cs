using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MinhaSala.Models
{
    public class AirtableData
    {
        [JsonProperty("MATRICULA")]
        public int Matricula { get; set; }

        [JsonProperty("DATA NASCIM.")]
        public DateTime DataNascimento { get; set; }

        [JsonProperty("DISCIPLINA")]
        public string Disciplina { get; set; }

        [JsonProperty("UNIDADE")]
        public string Unidade { get; set; }

        [JsonProperty("CURSO")]
        public string Curso { get; set; }

        [JsonProperty("DIA")]
        public string Dia { get; set; }

        [JsonProperty("HOR. INICIO")]
        public string HoraInicio { get; set; }

        [JsonProperty("HOR.FINAL")]
        public string HoraFim { get; set; }

        [JsonProperty("Sala")]
        public string Sala { get; set; }
    }

    public class AirtableResponse
    {
        public List<AirtableRecord> Records { get; set; }
        public string Offset { get; set; }

    }

    public class AirtableRecord
    {
        public string Id { get; set; }
        public AirtableData Fields { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}

