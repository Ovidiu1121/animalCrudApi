using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using AnimalCrudApi.Animals.Model;
using AnimalCrudApi.Dto;
using Newtonsoft.Json;
using tests.Infrastructure;
using Xunit;

namespace tests.IntegrationTests;

public class AnimalntegrationTests: IClassFixture<ApiWebApplicationFactory>
{
    
    private readonly HttpClient _client;

    public AnimalntegrationTests(ApiWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Post_Create_ValidRequest_ReturnsCreatedStatusCode_ValidProductContentResponse()
    {
        var request = "/api/v1/Animal/create";
        var animal = new CreateAnimalRequest { Name = "test new animal", Age = 77, Weight = 88 };
        var content = new StringContent(JsonConvert.SerializeObject(animal), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(request, content);
        
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Animal>(responseString);

        Assert.NotNull(result);
        Assert.Equal(animal.Name, result.Name);
        Assert.Equal(animal.Age, result.Age);
        Assert.Equal(animal.Weight, result.Weight);

    }
    
    [Fact]
    public async Task Post_Create_AnimalAlreadyExists_ReturnsBadRequestStatusCode()
    {
        var request = "/api/v1/Animal/create";
        var animal = new CreateAnimalRequest {  Name = "test new animal", Age = 77, Weight = 88 };
        var content = new StringContent(JsonConvert.SerializeObject(animal), Encoding.UTF8, "application/json");

        await _client.PostAsync(request, content);
        var response = await _client.PostAsync(request, content);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Put_Update_ValidRequest_ReturnsAcceptedStatusCode_ValidProductContentResponse()
    {

        var request = "/api/v1/Animal/create";
        var animal = new CreateAnimalRequest() { Name = "test new animal", Age = 77, Weight = 88  };
        var content = new StringContent(JsonConvert.SerializeObject(animal), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(request, content);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Animal>(responseString)!;
        
        request = "/api/v1/Animal/update/"+result.Id;
        var updateAnimal = new UpdateAnimalRequest { Age = 33, Weight = 44 };
        content = new StringContent(JsonConvert.SerializeObject(updateAnimal), Encoding.UTF8, "application/json");
        
        response = await _client.PutAsync(request, content);
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        responseString = await response.Content.ReadAsStringAsync();
        result = JsonConvert.DeserializeObject<Animal>(responseString)!;

        Assert.Equal(updateAnimal.Age, result.Age);
        Assert.Equal(updateAnimal.Weight, result.Weight);
        
    }

    [Fact]
    public async Task Put_Update_AnimalDoesNotExists_ReturnsNotFoundStatusCode()
    {
        
        var request = "/api/v1/Animal/update/4";
        var updateAnimal = new UpdateAnimalRequest() { Age = 77,Weight = 88};
        var content = new StringContent(JsonConvert.SerializeObject(updateAnimal), Encoding.UTF8, "application/json");

        var response = await _client.PutAsync(request, content);
        
        Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);
        
    }

    [Fact]
    public async Task Delete_Delete_AnimalExists_ReturnsDeletedAnimal()
    {
        
        var request = "/api/v1/Animal/create";
        var animal = new CreateAnimalRequest() { Name = "test new animal", Age = 77, Weight = 88 };
        var content = new StringContent(JsonConvert.SerializeObject(animal), Encoding.UTF8, "application/json");
    
        var response = await _client.PostAsync(request, content);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Animal>(responseString)!;

        request = "/api/v1/Animal/delete/" + result.Id;
        response = await _client.DeleteAsync(request);
        
        Assert.Equal(HttpStatusCode.Accepted,response.StatusCode);
        
    }
    
    [Fact]
    public async Task Delete_Delete_AnimalDoesNotExists_ReturnsNotFoundStatusCode()
    {

        var request = "/api/v1/Animal/delete/2";

        var response = await _client.DeleteAsync(request);

        Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);
        
    }

    [Fact]
    public async Task Get_GetById_ValidRequest_ReturnsOKStatusCode()
    {
        
        var request = "/api/v1/Animal/create";
        var animal = new CreateAnimalRequest() {  Name = "test new animal", Age = 77, Weight = 88 };
        var content = new StringContent(JsonConvert.SerializeObject(animal), Encoding.UTF8, "application/json");
        
        var response = await _client.PostAsync(request, content);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Animal>(responseString)!;

        request = "/api/v1/Animal/id/" + result.Id;
        response = await _client.GetAsync(request);

        Assert.Equal(HttpStatusCode.OK,response.StatusCode);
        
    }
    
    [Fact]
    public async Task Get_GetById_AnimalDoesNotExists_ReturnsNotFoundStatusCode()
    {

        var request = "/api/v1/Animal/id/5";

        var response = await _client.GetAsync(request);
        
        Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);

    }
    
    [Fact]
    public async Task Get_GetByName_ValidRequest_ReturnsOKStatusCode()
    {
        
        var request = "/api/v1/Animal/create";
        var animal = new CreateAnimalRequest() {  Name = "test new animal", Age = 77, Weight = 88 };
        var content = new StringContent(JsonConvert.SerializeObject(animal), Encoding.UTF8, "application/json");
        
        var response = await _client.PostAsync(request, content);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Animal>(responseString)!;

        request = "/api/v1/Animal/name/" + result.Name;
        response = await _client.GetAsync(request);

        Assert.Equal(HttpStatusCode.OK,response.StatusCode);
        
    }
    
    [Fact]
    public async Task Get_GetByName_AnimalDoesNotExists_ReturnsNotFoundStatusCode()
    {

        var request = "/api/v1/Animal/name/test";

        var response = await _client.GetAsync(request);
        
        Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);

    }

    [Fact]
    public async Task Get_GetAll_ValidRequest_ReturnsOKStatusCode()
    {
        
        var request = "/api/v1/Animal/create";
        var animal = new CreateAnimalRequest { Name = "test new animal", Age = 77, Weight = 88 };
        var content = new StringContent(JsonConvert.SerializeObject(animal), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(request, content);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Animal>(responseString)!;

        request = "/api/v1/Animal/all";
        response = await _client.GetAsync(request);

        Assert.Equal(HttpStatusCode.OK,response.StatusCode);
    }

    [Fact]
    public async Task Get_GetAll_NoAnimalsExists_ReturnsNotFoundStatusCode()
    {
        var request = "/api/v1/Animal/all";
        
        var response = await _client.GetAsync(request);
        
        Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);
    }
    
}