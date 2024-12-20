using CoreWCF;
using CoreWCF.Configuration;
using CoreWCF.Description;
using ServiceContractLayer;
using SoapServiceLayer.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Configurar servicios
builder.Services.AddServiceModelServices(); // Agrega soporte para CoreWCF
builder.Services.AddServiceModelMetadata(); // Permite publicar el WSDL

var app = builder.Build();
app.Urls.Add("http://0.0.0.0:5272/");

// Configurar middleware de CoreWCF
app.UseServiceModel(serviceBuilder =>
{
    serviceBuilder.AddService<ProductController>(serviceOptions =>
    {
        serviceOptions.DebugBehavior.IncludeExceptionDetailInFaults = true;
    });

    serviceBuilder.AddService<CategoryController>(serviceOptions =>
    {
        serviceOptions.DebugBehavior.IncludeExceptionDetailInFaults = true;
    });

    serviceBuilder.AddServiceEndpoint<ProductController, IProductService>(new BasicHttpBinding(), "/ProductController.asmx");
    serviceBuilder.AddServiceEndpoint<CategoryController, ICategoryService>(new BasicHttpBinding(), "/CategoryController.asmx");

    // Agregar soporte para metadatos (WSDL)
    var serviceMetadataBehavior = app.Services.GetRequiredService<ServiceMetadataBehavior>();
    serviceMetadataBehavior.HttpGetEnabled = true;  // Publicar WSDL por HTTP
    serviceMetadataBehavior.HttpsGetEnabled = false; // Desactivar HTTPS si no está configurado
    // serviceMetadataBehavior.HttpGetUrl = new Uri("http://0.0.0.0:5272/"); // Desactivar HTTPS si no está configurado
});

app.Run();
