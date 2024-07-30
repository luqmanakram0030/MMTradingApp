


namespace MMAdmin.Domain.Services.Implementation;


public class ProductService : IProductService
{
    private readonly FirebaseClient _firebaseClient;
    public ProductService()
    {
        _firebaseClient = new FirebaseClient(FirebaseWebApi.DatabaseLink, new FirebaseOptions
        {
            AuthTokenAsyncFactory = () => Task.FromResult(FirebaseWebApi.DatabaseSecret)
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

        await _firebaseClient
            .Child("Products")
            .Child(toUpdateProduct.Key)
            .PutAsync(product);
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
}

