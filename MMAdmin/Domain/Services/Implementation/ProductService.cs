


using System.Reflection;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;

namespace MMAdmin.Domain.Services.Implementation;


public class ProductService : IProductService
{
    private readonly FirebaseClient _firebaseClient;
    DriveService service = new DriveService();
    public ProductService()
    {
        _firebaseClient = new FirebaseClient(FirebaseWebApi.DatabaseLink, new FirebaseOptions
        {
            AuthTokenAsyncFactory = () => Task.FromResult(FirebaseWebApi.DatabaseSecret)
        });
        var jsonFileName = "MMAdmin.File.MMTrading.json";
        var assembly = IntrospectionExtensions.GetTypeInfo(typeof(ProductService)).Assembly;
        Stream stream = assembly.GetManifestResourceStream(jsonFileName);
        string jsonString = "";
        using (var reader = new System.IO.StreamReader(stream))
        {
            jsonString = reader.ReadToEnd();
        }
        var credential = GoogleCredential.FromJson(jsonString)
            .CreateScoped(DriveService.ScopeConstants.Drive);
        service = new DriveService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = "MMTrading",
        });
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        return (await _firebaseClient
            .Child("Products")
            .OnceAsync<Product>())
            .Select(item => new Product
            {
                Id = item.Object.Id,
                Name = item.Object.Name,
                Description = item.Object.Description,
                Price = item.Object.Price,
                StockQuantity = item.Object.StockQuantity,
                Category = item.Object.Category,
                ImageUrl = item.Object.ImageUrl
            }).ToList();
    }

    public async Task<Product> GetProductByIdAsync(Guid id)
    {
        var allProducts = await GetAllProductsAsync();
        return allProducts.FirstOrDefault(p => p.Id == id);
    }

    public async Task AddProductAsync(Product product)
    {
        product.Id = Guid.NewGuid();
        await _firebaseClient
            .Child("Products")
            .PostAsync(product);
    }

    public async Task UpdateProductAsync(Product product)
    {
        var toUpdateProduct = (await _firebaseClient
                .Child("Products")
                .OnceAsync<Product>())
            .FirstOrDefault(a => a.Object.Id == product.Id);

        if (toUpdateProduct != null)
        {
            var updates = new Dictionary<string, object>();

            // Update only the properties you need
            updates["Name"] = product.Name;
            updates["Description"] = product.Description;
            updates["Price"] = product.Price;
            updates["StockQuantity"] = product.StockQuantity;
            updates["Category"] = product.Category;
            updates["ImageUrl"] = product.ImageUrl;

            // Perform the update in Firebase
            await _firebaseClient
                .Child("Products")
                .Child(toUpdateProduct.Key)
                .PatchAsync(updates);
        }
    }
    public async Task DeleteProductAsync(Guid id)
    {
        var toDeleteProduct = (await _firebaseClient
            .Child("Products")
            .OnceAsync<Product>())
            .FirstOrDefault(a => a.Object.Id == id);

        await _firebaseClient
            .Child("Products")
            .Child(toDeleteProduct.Key)
            .DeleteAsync();
    }
    public async Task<byte[]> ProcessMediaAsync(Stream stream)
    {
        var memoryStream = new MemoryStream();
        await stream.CopyToAsync(memoryStream);
        return memoryStream.ToArray();
    }

    public async Task<string> CapturePhotoAsync()
    {
        if (!MediaPicker.IsCaptureSupported)
        {
            await Application.Current.MainPage.DisplayAlert("","Camera is not available","Ok");
            return null;
        }

        var photo = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions
        {
            Title = "Take a photo"
        });

        if (photo != null)
        {
            var stream = await photo.OpenReadAsync();
            var bytes = await ProcessMediaAsync(stream);
            return Convert.ToBase64String(bytes);
        }

        return null;
    }

    public async Task<string> PickPhotoAsync()
    {
        var photo = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
        {
            Title = "Select a photo"
        });

        if (photo != null)
        {
            var stream = await photo.OpenReadAsync();
            var bytes = await ProcessMediaAsync(stream);
            return Convert.ToBase64String(bytes);
        }

        return null;
    }
    public async Task RequestPermissionAsync()
    {
        var platform = DeviceInfo.Platform;
        if (platform == DevicePlatform.Android)
        {
            if (OperatingSystem.IsAndroidVersionAtLeast(33) || OperatingSystem.IsAndroidVersionAtLeast(34))
            {
                var mediaStatus = await Permissions.CheckStatusAsync<Permissions.Media>();
                if (mediaStatus == PermissionStatus.Granted)
                {
                    await RequestCameraPermissionAsync();
                }
                else
                {
                    mediaStatus = await Permissions.RequestAsync<Permissions.Media>();

                    if (Permissions.ShouldShowRationale<Permissions.Media>())
                    {
                        ShowErrorAsync("App needs media permission");
                    }

                    if (mediaStatus != PermissionStatus.Granted)
                    {
                        RequestPermissionAsync();
                        return;
                    }
                    if (mediaStatus == PermissionStatus.Granted)
                    {
                        RequestCameraPermissionAsync();
                    }
                }
            }
            else
            {
                var storageReadStatus = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
                if (storageReadStatus == PermissionStatus.Granted)
                {
                    await RequestCameraPermissionAsync();
                }
                else
                {
                    storageReadStatus = await Permissions.RequestAsync<Permissions.StorageRead>();

                    if (Permissions.ShouldShowRationale<Permissions.StorageRead>())
                    {
                        ShowErrorAsync("App needs storage permission");
                    }

                    if (storageReadStatus != PermissionStatus.Granted)
                    {
                        RequestPermissionAsync();
                        return;
                    }
                    if (storageReadStatus == PermissionStatus.Granted)
                    {
                        RequestCameraPermissionAsync();
                    }
                }
            }
        }
        else
        {
            var storageReadStatus = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
            if (storageReadStatus == PermissionStatus.Granted)
            {
                await RequestCameraPermissionAsync();
            }
            else
            {
                storageReadStatus = await Permissions.RequestAsync<Permissions.StorageRead>();

                if (Permissions.ShouldShowRationale<Permissions.StorageRead>())
                {
                    ShowErrorAsync("App needs storage permission");
                }

                if (storageReadStatus != PermissionStatus.Granted)
                {
                    RequestPermissionAsync();
                    return;
                }
                if (storageReadStatus == PermissionStatus.Granted)
                {
                    RequestCameraPermissionAsync();
                }
            }
        }
    }
    public async Task RequestCameraPermissionAsync()
    {
        var cameraStatus = await Permissions.CheckStatusAsync<Permissions.Camera>();
        if (cameraStatus == PermissionStatus.Granted)
        {
            return;
        }
        else
        {
            cameraStatus = await Permissions.RequestAsync<Permissions.Camera>();
        }

        if (Permissions.ShouldShowRationale<Permissions.Camera>())
        {
            ShowErrorAsync("App needs camera permission");
        }

        if (cameraStatus != PermissionStatus.Granted)
        {
            RequestCameraPermissionAsync();
            return;
        }
        
    }
    private void ShowErrorAsync(string message)
    {
        ToastService.ShowToastAsync(message);
    }

    private void ShowSuccessAsync(string message)
    {
        ToastService.ShowToastAsync(message);
    }
    

    // public async Task<string> UploadImageAsync(Stream imageStream, string fileName)
    // {
    //     // Firebase Storage URL
    //     var storageUrl = $"https://firebasestorage.googleapis.com/v0/b/mmtrading-e6263.appspot.com/o/{fileName}?uploadType=media";
    //
    //     using var httpClient = new HttpClient();
    //
    //     // Retrieve the token from Preferences
    //     var authToken = Preferences.Get("FirebaseToken", string.Empty);
    //     httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
    //
    //     var content = new StreamContent(imageStream);
    //     content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
    //
    //     var response = await httpClient.PostAsync(storageUrl, content);
    //
    //     if (response.IsSuccessStatusCode)
    //     {
    //         var result = await response.Content.ReadAsStringAsync();
    //         var json = JsonConvert.DeserializeObject<dynamic>(result);
    //         string downloadUrl = json["mediaLink"];
    //         return downloadUrl;
    //     }
    //
    //     return null;
    // }
    public  Task<bool> UploadImageAsync(Stream stream,string fileName)
    {
        var response = false;
        try
        {
          fileName=  fileName + ".jpeg";
            string folderId = "1GJ9rKtDEWKVcExYeSidZr81K24SwDZ6c";
            DateTime date = DateTime.Now;
            var dateString = date.ToString("dd.MM.yyyy");
            
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = Path.GetFileName(fileName),
                Parents = new List<string>() { folderId }
            };
           
            FilesResource.CreateMediaUpload request;
            request = service.Files.Create(fileMetadata, stream, "");
            request.Fields = "id";
        var result =    request.Upload();
            var responceBody = request.ResponseBody;
            response = true;
        }
        catch (Exception ex)
        {
            response = false;
        }

        return Task.FromResult(response);
    }

}

