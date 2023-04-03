namespace Centhora_Hotels.InternalServices.Upload_Image_To_AWS_S3
{
    public interface IUploadImage
    {
        Task<string> UploadImageToAWS_S3(IFormFile formFile, string bucketName);
    }
}
