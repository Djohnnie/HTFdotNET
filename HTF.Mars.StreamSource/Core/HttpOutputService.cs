﻿using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using HTF.Mars.StreamSource.Contracts;
using Newtonsoft.Json;
using RestSharp;

namespace HTF.Mars.StreamSource.Core
{
    public class HttpOutputService : IOutputService
    {
        public Boolean IsValid(String destination)
        {
            try
            {
                var client = new RestClient(destination);
                var request = new RestRequest(Method.OPTIONS);
                var result = client.Execute(request);
                return result.StatusCode == HttpStatusCode.OK;
            }
            catch
            {
                return false;
            }
        }

        public async Task WriteSample(String destination, Sample sample)
        {
            await Task.Delay(1);

            try
            {
                var client = new RestClient(destination);
                var request = new RestRequest(Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(sample);
                await client.ExecuteTaskAsync(request);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private String SerializeSample(Sample sample)
        {
            return JsonConvert.SerializeObject(sample, Formatting.Indented);
        }
    }
}