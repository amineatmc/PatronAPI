using Business.Abstract;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Mvc;
using Entities.Dtos;

namespace PatronApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private IHttpContextAccessor _httpContextAccessor;
        public UserController(IUserService userService)
        {
            _userService = userService;
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        [HttpPost("[action]")]
        public IActionResult Update([FromForm] UserUpdateDto userUpdateDto)
        {
            var userUpdate=_userService.Update(userUpdateDto);
            if (userUpdate.Success)
            {
                return Ok(userUpdate);
            }
            return BadRequest(userUpdate);
        }
        [HttpGet("[action]/{userId}")]
        public IActionResult Getlist(int userId)
        {
            var dataUser=_userService.GetList(userId);
            if (dataUser.Success)
            {
                return Ok(dataUser);
            }
            return BadRequest(dataUser);
        }
        //[HttpPost("[action]")]
        //public ActionResult GrabKey()
        //{
        //    string message;
        //    var header = _httpContextAccessor.HttpContext.Request.Headers.ToList();
        //    foreach (var key in header)
        //    {
        //        if (key.Key=="ApiKey")
        //        {
        //            message = key.Value;
        //        }
        //    }

        //     //TryGetValue("ApiKey",out var traceValue);
        //    //var Header = _httpContextAccessor.HttpContext.Request.Headers["ApiKey"];
            
        //    return Ok();
        //}
    }
}

    //    [HttpPost("[action]")]
    //    public async Task<IActionResult> TaxiGetData()
    //    {
    //        var client = new RestClient("http://89.43.78.152:3000/admin/taximeterhistorydevice");

    //        var options = new RestClientOptions("http://89.43.78.152:3000/admin/taximeterhistorydevice") 
    //        {
    //            ThrowOnAnyError = true,
    //            Timeout = -1
    //        };

    //        client = new RestClient(options);

    //        var request = new RestRequest("", Method.Post);

    //        request.AddHeader("Content-Type", "application/json");

    //        var postData = new Test();
    //        postData.deviceId = "62d6720a9cc6748bfd546ffe";

    //        request.AddParameter("application/json", postData, ParameterType.RequestBody);

    //        RestResponse response = client.Execute(request);

    //        //RestRequest restRequest = new RestRequest("admin/taximeterhistorydevice",Method.Post).AddJsonBody(postData);
    //        //var response =await client.ExecuteAsync(restRequest);
    //        var output = JsonConvert.DeserializeObject<Root>(response.Content);
           
    //        //iki türlü yöntem var. Bir HostService dediğimiz otomatik tetikleme. Site ayağa kaltığında belirlediğin sürede belirlediğin işi yapar
    //        //ama tam verimli çalışmıyor.
    //        //İkinci yöntem. Bir CMD programı yapacaksın ya da microsoft service. Bu controllerın apisini çağıracaksın istediğin sürede


    //        return Ok(response.Content);
    //    }
    //}

    //public class Test
    //{
    //    public string deviceId { get; set; }
    //}

    //public class Data
    //{
    //    public List<Datum> data { get; set; }
    //    public int fiyat { get; set; }
    //    public int toplamSure { get; set; }
    //    public int yillikSure { get; set; }
    //    public int aylikSure { get; set; }
    //    public int haftalikSure { get; set; }
    //    public int gunlukSure { get; set; }
    //    public double doluMesafe { get; set; }
    //    public int gunlukFiyat { get; set; }
    //    public int haftalikFiyat { get; set; }
    //    public double gunlukDoluMesafe { get; set; }
    //    public int aylikFiyat { get; set; }
    //    public double aylikDoluMesafe { get; set; }
    //    public int yillikFiyat { get; set; }
    //    public double yillikDoluMesafe { get; set; }
    //    public double haftalikDoluMesafe { get; set; }
    //    public int gunlukSayi { get; set; }
    //    public int yillikSayi { get; set; }
    //    public int aylikSayi { get; set; }
    //    public int haftalikSayi { get; set; }
    //    public int toplamSayi { get; set; }        
    //}

    //public class Datum
    //{
    //    public StartLocation startLocation { get; set; }
    //    public FinishLocation finishLocation { get; set; }
    //    public string _id { get; set; }
    //    public string deviceId { get; set; }
    //    public string driverId { get; set; }
    //    public string travelId { get; set; }
    //    public string plate { get; set; }
    //    public string statu { get; set; }
    //    public string price { get; set; }
    //    public string distance { get; set; }
    //    public string processId { get; set; }
    //    public DateTime startTime { get; set; }
    //    public int month { get; set; }
    //    public int day { get; set; }
    //    public int year { get; set; }
    //    public int week { get; set; }
    //    public DateTime created { get; set; }
    //    public int __v { get; set; }
    //    public string duration { get; set; }
    //    public DateTime finishTime { get; set; }
    //}

    //public class FinishLocation
    //{
    //    public string type { get; set; }
    //    public List<double> coordinates { get; set; }
    //}

    //public class Root
    //{
    //    public bool status { get; set; }
    //    public int statuscode { get; set; }
    //    public string message { get; set; }
    //    public Data data { get; set; }
    //}

    //public class StartLocation
    //{
    //    public string type { get; set; }
    //    public List<double> coordinates { get; set; }
    //}

