using System.Net;
using System.Text;
using Mango.Web.Models;
using Mango.Web.Services.IServices;
using Mango.Web.Utility;
using Newtonsoft.Json;

namespace Mango.Web.Services;

public class BaseService : IBaseService
{
    private IHttpClientFactory _httpClientFactory;

    public BaseService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<ResponseDto?> SendAsync(RequestDto requestDto)
    {
        var client = _httpClientFactory.CreateClient("MangoAPI");
        HttpRequestMessage message = new();

        message.Headers.Add("Accept", "application/json");
        message.RequestUri = new Uri(requestDto.Url);
        if (requestDto.Data != null)
        {
            message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");

        }

        HttpResponseMessage? response = null;

        switch (requestDto.ApiType)
        {
            case ApiType.Post: message.Method = HttpMethod.Post;
                break;
            case ApiType.Put: message.Method = HttpMethod.Put;
                break;
            case ApiType.Delete: message.Method = HttpMethod.Delete;
                break;
            default: message.Method = HttpMethod.Get;
                break;
        }

        response = await client.SendAsync(message);

        try
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    return new ResponseDto()
                    {
                        Success = false,
                        Message = "Not Found"
                    };
                case HttpStatusCode.Forbidden:
                    return new ResponseDto()
                    {
                        Success = false,
                        Message = "Access denied"
                    };
                case HttpStatusCode.Unauthorized:
                    return new ResponseDto()
                    {
                        Success = false,
                        Message = "Unauthorized"
                    };
                case HttpStatusCode.InternalServerError:
                    return new ResponseDto()
                    {
                        Success = false,
                        Message = "Internal Server Error"
                    };
                default:
                    var apiContent = await response.Content.ReadAsStringAsync();
                    var responseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                    return responseDto;
            }
        }
        catch (Exception ex)
        {
            var responseDto = new ResponseDto()
            {
                Success = false,
                Message = ex.Message
            };
            return responseDto;
        }
    }
}