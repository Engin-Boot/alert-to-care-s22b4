﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;
namespace AlertToCareAPI.Repository
{
    public class MonitoringRepository : IMonitoringRepository
    {

        IcuContext _db;
        public MonitoringRepository(IcuContext db)
        {
               _db = db;
           /* _db.Add(new Vitals
            {
                Mrn = new Guid("69AA3BA5-D51E-465E-8447-ECAA1939739A"),
                Spo2 = 10,
                Bpm=12,
                RespRate=134
            }) ; */
        }
        public IEnumerable<Vitals> GetAllVitals()
        {
            return _db.Vitals;
        }
        public string CheckVitals(Entities.Vitals vital)
           {
            string a=CheckSpo2(vital.Spo2);
            string b=CheckBpm(vital.Bpm);
            string c=CheckResprate(vital.RespRate);
           string s=a+ b+c;
            return s;
           }
        public string CheckSpo2(float spo2)
        {
                if (spo2 < 90)
                 return "Spo2 is low";
                return "";

        }
        public string CheckBpm(float bpm)
        {
            if (bpm < 70)
                return "bpm is low";
            if (bpm > 150)
                return "bpm is high";
            else
                return "";
        }
        public string CheckResprate(float resprate)
        {
            if (resprate < 30)
                return "resprate is low";
            if (resprate > 95)
                return "resprate is high";
            else
                return "";
        }
    }
}