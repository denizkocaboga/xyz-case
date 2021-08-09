using Microsoft.Extensions.Options;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XyzCase.Tweeldata.ApiClient.Models;
using XyzCase.Tweeldata.ApiClient.Settings;

namespace XyzCase.Tweeldata.ApiClient
{
    public interface IApiClient
    {
        Task<GetPricesResponse> GetPricesAsync(IEnumerable<string> symbols, string interval);
        GetPricesResponse GetPrices(IEnumerable<string> symbols, string interval);
    }
    public class ApiClient : IApiClient
    {
        private readonly IRestClient _client;
        private readonly TweeldataApiSettings _settings;
        public ApiClient(IOptions<TweeldataApiSettings> options, IRestClient client)
        {
            _settings = options.Value;
            _client = client;
            _client.BaseUrl = new Uri(_settings.BaseUrl);
        }

        public GetPricesResponse GetPrices(IEnumerable<string> symbols, string interval)
        {
            var request = CreateRequest(symbols, interval);

            var response = _client.Execute<Dictionary<string, Symbol>>(request);

            GetPricesResponse result = CreateResponse(response);
            return result;
        }

        public async Task<GetPricesResponse> GetPricesAsync(IEnumerable<string> symbols, string interval)
        {
            var request = CreateRequest(symbols, interval);

            var response = await _client.ExecuteAsync<Dictionary<string, Symbol>>(request);

            GetPricesResponse result = CreateResponse(response);
            return result;
        }
        #region Privates        
        private static GetPricesResponse CreateResponse(IRestResponse<Dictionary<string, Symbol>> response)
        {
            GetPricesResponse result;
            if (response.IsSuccessful)
                result = new GetPricesResponse(response.StatusCode, response.Data);
            else
                result = new GetPricesResponse(response.StatusCode, response.ErrorException, response.ErrorMessage, response.Content);

            return result;
        }

        private IRestRequest CreateRequest(IEnumerable<string> symbols, string interval)
        {
            IRestRequest request = new RestRequest("time_series", Method.GET);

            string symbolsParam = string.Join(",", symbols);
            request.AddQueryParameter("symbol", symbolsParam);
            request.AddQueryParameter("interval", interval.ToString());
            request.AddQueryParameter("apikey", _settings.ApiKey);
            return request;
        }

        #endregion
    }


}
