using System;
using System.Collections.Generic;
using System.Text;

namespace carga.Model
{
    public class Info
    {
        public Header Header { get; set; }
        public List<Series> Series { get; set; }
    }

    public class Header {
        public string Email { get; set; }
        public string Name { get; set; }
    }
    public class Series
    {
        public string Indicador { get; set; }
        public string Freq { get; set; }
        public string Topic { get; set; }
        public string Unit { get; set; }
        public string Unit_mult { get; set; }
        public string Note { get; set; }
        public string Lastupdate { get; set; }
        public string Source { get; set; }
        public string Status { get; set; }
        public List<Observations> Observations { get; set; }
    }

    public class Observations 
    {
        public string Cober_geo { get; set; }
        public string Obs_exception { get; set; }
        public string Obs_note { get; set; }
        public string Obs_source { get; set; }
        public string Obs_status { get; set; }
        public string Obs_value { get; set; }
        public string Time_period { get; set; }
    }
}
