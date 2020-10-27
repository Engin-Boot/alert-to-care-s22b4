﻿using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.AlertManagement;
using DataModels;
using Newtonsoft.Json;
using Xunit;
namespace API.Tests
{
    public class PatientMonitoringControllerTest
    {
        [Fact]
        public async Task TestGetAllVitals()
        {
            var client = new TestClientProvider().Client;
            var response = await client.GetAsync("api/PatientMonitoring/Vitals");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task TestGetAllAlerts()
        {
            var client = new TestClientProvider().Client;
            var response = await client.GetAsync("api/PatientMonitoring/Alerts");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task UpdateVitalsByValidPatientIdReturnOk()
        {
            var vital = new Vital()
            {
                Bpm = 180,
                PatientId = "PID1",
                RespRate = 41,
                Spo2 = 98
            };

            var client = new TestClientProvider().Client;
            var content = new StringContent(JsonConvert.SerializeObject(vital), Encoding.UTF8, "application/json");
            var response = await client.PutAsync("api/PatientMonitoring/Vital/" + vital.PatientId, content);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task UpdateVitalsByInValidPatientIdReturnBadRequest()
        {
            var vital = new Vital()
            {
                Bpm = 180,
                PatientId = "PID11111",
                RespRate = 41,
                Spo2 = 98
            };

            var client = new TestClientProvider().Client;
            var content = new StringContent(JsonConvert.SerializeObject(vital), Encoding.UTF8, "application/json");
            var response = await client.PutAsync("api/PatientMonitoring/Vital/" + vital.PatientId, content);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async Task TestToggleAlertWithValidAlertId()
        {
            var alertManagementSqLite = new AlertManagementSqLite();
            var pid = "PID1";
            var alertId = 0;
            AlertManagementSqLite.AddToAlertsTable(pid);
            var alerts = alertManagementSqLite.GetAllAlerts();
            foreach (var alert in alerts)
            {
                if (alert.PatientId == pid)
                {
                    alertId = alert.AlertId;
                }
            }
            var client = new TestClientProvider().Client;
            var response = await client.PutAsync("api/PatientMonitoring/Alert/" + alertId,null);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            alertManagementSqLite.DeleteAlertByAlertId(alertId);
        }
        [Fact]
        public async Task TestToggleAlertWithInvalidAlertId()
        {
            var client = new TestClientProvider().Client;
            var invalidAlertId = "666";
            var response = await client.PutAsync("api/PatientMonitoring/Alert/" + invalidAlertId, null);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async Task TestDeleteAlertWithValidAlertId()
        {
            var alertManagementSqLite = new AlertManagementSqLite();
            var pid = "PID1";
            var alertId=0;
            AlertManagementSqLite.AddToAlertsTable(pid);

            var alerts = alertManagementSqLite.GetAllAlerts();
            foreach (var alert in alerts)
            {
                if (alert.PatientId == pid)
                {
                    alertId = alert.AlertId;
                }
            }
            var client = new TestClientProvider().Client;
            var response = await client.DeleteAsync("api/PatientMonitoring/Alert/" + alertId);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task TestDeleteAlertWithInValidAlertId()
        {
            var alertId = 999;
            var client = new TestClientProvider().Client;
            var response = await client.DeleteAsync("api/PatientMonitoring/Alert/" + alertId);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
