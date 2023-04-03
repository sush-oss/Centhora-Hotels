using Amazon.S3;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Centhora_Hotels.DB_Context;
using Centhora_Hotels.InternalServices.Upload_Image_To_AWS_S3;
using Centhora_Hotels.Models;
using Centhora_Hotels.Models.DTO;
using Centhora_Hotels.Repository.Interface;

namespace Centhora_Hotels.Repository.Repo
{
    public class UserRepository : IUserRepository, IDisposable
    {
        private readonly CenthoraDbContext centhora;
        private readonly IMapper mapper;
        private readonly IUploadImage uploadImageToAWS;
        private readonly IConfiguration _configuration;

        public UserRepository(CenthoraDbContext _centhora, IMapper _mapper, IUploadImage uploadImage, IConfiguration configuration)
        {
            this.centhora = _centhora;
            this.mapper = _mapper;
            this.uploadImageToAWS = uploadImage;
            _configuration = configuration;
        }

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            var allUsers = await centhora.Users.ToListAsync();
            return mapper.Map<IEnumerable<UserDto>>(allUsers);
        }

        public async Task<UserDto> GetById(int id)
        {
            var user = await centhora.Users.FirstOrDefaultAsync(u => u.UserId == id);
            var mappedDtoUser = mapper.Map<UserDto>(user);
            return mappedDtoUser;
        }

        public async Task<string> AddNewUser(PostUserDto postuserDto, IFormFile file)
        {
            // Checking if the user already exists
            if (await centhora.Users.AnyAsync(u => u.UserName == postuserDto.UserName))
            {
                return "User already exists";
            }

            string bucketName = _configuration.GetValue<string>("AWSs3:BucketName");
            
            // Mapping data
            var user = mapper.Map<User>(postuserDto);

            // Performing the user image upload function
            var uploadImageTask = uploadImageToAWS.UploadImageToAWS_S3(file, bucketName);

            // Waiting 10 seconds for the upload to compleste
            var imageUploadCompleted = await Task.WhenAny(uploadImageTask, Task.Delay(TimeSpan.FromSeconds(10)));

            // If the upload took place with in the 10 seconds proceed to save all the new user data.
            if (imageUploadCompleted == uploadImageTask)
            {
                string imgURL = await uploadImageTask;
                user.UserImageURL = imgURL;

                centhora.Users.Add(user);
                await centhora.SaveChangesAsync();

                return "Successfully created a new user";
            }
            else
            {
                // If the image upload did not happen with in the 10 seconds, the save the rest of the new user data to the DB.
                centhora.Users.Add(user);
                await centhora.SaveChangesAsync();

                return "Successfully created a new user, but unable to upload user's image. Try to upload the image later.";
            }
        }

        public async Task UpdateUser(int id, UserDto userDto)
        {
            var existingUser = await centhora.Users.FirstOrDefaultAsync(u => u.UserId == id);
            if (existingUser != null)
            {
                mapper.Map(userDto, existingUser);
                centhora.Entry(existingUser).State = EntityState.Modified;
                await centhora.SaveChangesAsync();
            }
        }


        public async Task DeleteUser(int id)
        {
            var existingUser = await centhora.Users.FirstOrDefaultAsync(u => u.UserId == id);
            if (existingUser != null)
            {
                centhora.Users.Remove(existingUser);
                await centhora.SaveChangesAsync();
            }
        }

        // Disposing and reclaming the resources
        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    centhora.Dispose();
                }
            }
            _disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        
    }
}
