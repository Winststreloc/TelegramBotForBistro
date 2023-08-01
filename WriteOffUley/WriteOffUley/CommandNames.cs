namespace WriteOffUley;

public static class CommandNames
{
    public const string StartCommand = "/start";
    public const string SelectCategoryCommand = "select-category-product";
    public const string SelectCountProductsCommand = "select-count-products";
    public const string FinishOperationCommand = "finish-write-off";
    public const string SelectProductCommand = "select-product";
    public const string DeleteWriteOffCommand = "delete-write-off";
    public const string CreateNewProduct = "create-new-product";
    
    //analytic
    public const string SelectDeleteWriteOffCommand = "select-delete-write-off";
    public const string OpenAllWriteOffDayCommand = "open-all-write-off-day";
    public const string AnalyticsCommand = "get-analytics";
    public const string SelectAnalyticsCommand = "select-analytics";

    public const string SelectWriteOffsCommand = "select-write-off";
    public const string GetWriteOffsCommand = "get-write-off";
    
    //storage
    public const string StorageCommand = "get-storage";
    public const string AddStorageCommand = "add-storage";
    public const string DeleteStorageCommand = "delete-storage";
}