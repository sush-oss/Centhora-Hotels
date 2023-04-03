using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;

namespace Centhora_Hotels.InternalServices.Upload_Image_To_AWS_S3
{
    public class UploadImage : IUploadImage
    {  
        public async Task<string> UploadImageToAWS_S3(IFormFile formFile, string bucketName)
        {
            if(formFile == null || formFile.Length == 0) throw new ArgumentNullException("No image file found in the request!");  
            
            var uploadedFileName = formFile.FileName;
            if (string.IsNullOrEmpty(uploadedFileName)) throw new ArgumentNullException("Invalid file name or file name not found!");

            var client = new AmazonS3Client();

            var objectRequest = new PutObjectRequest()
            {
                BucketName = bucketName,
                Key = uploadedFileName,
                InputStream = formFile.OpenReadStream(),
            };

            await client.PutObjectAsync(objectRequest);

            string URL = $"https://{bucketName}.s3.amazonaws.com/{uploadedFileName}";

            return URL ;
        }
    }
}
