using Business.Repositories.UserRepository.Contans;
using Business.Repositories.UserRepository.Validation.FluentValidation;
using Business.Utilities.File;
using Core.Aspect.Caching;
using Core.Aspect.Transaction;
using Core.Aspect.ValidationAspect;
using Core.Utilities.Hashing;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Repositories.UserRepository;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Repositories.UserRepository
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly IFileService _fileService;

        public UserManager(IUserDal userDal, IFileService fileService)
        {
            _userDal = userDal;
            _fileService = fileService;
        }

        public async Task Add(RegisterAuthDto registerDto)
        {
            string fileName = _fileService.FileSaveToServer(registerDto.Image, "./Content/Img/");
            //string fileName = _fileStream.FşleSaveToFtp(registorDto.Image);
            //byte[] fileByteArray = _fileService.FileSaveToDatabase(registerDto.Image);

            var user = CreateUser(registerDto, fileName);

            await _userDal.Add(user);
        }

        
        private User CreateUser(RegisterAuthDto registerDto, string fileName)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePassword(registerDto.Password, out passwordHash, out passwordSalt);

            User user = new User();
            user.Id = 0;
            user.Email = registerDto.Email;
            user.Name = registerDto.Name;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.ImageUrl = fileName;

            return user;
        }

        public async Task<User> GetByEmail(string email)
        {
            var result = await _userDal.Get(p => p.Email == email);
            return result;
        }

        [ValidationAspect(typeof(UserValidator))]
        [TransactionAspect]
        public async Task<IResult> Update(User user)
        {
            await _userDal.Update(user);
            return new SuccessResult(UserMessages.UpdatedUser);
        }

        public async Task<IResult> Delete(User user)
        {
            await _userDal.Delete(user);
            return new SuccessResult(UserMessages.DeletedUser);
        }

        [CacheAspect(60)]
        public async Task<IDataResult<List<User>>> GetList()
        {
            return new SuccessDataResult<List<User>>(await _userDal.GetAll());
        }

        public async Task<IDataResult<User>> GetById(int id)
        {
            return new SuccessDataResult<User>(await _userDal.Get(p=>p.Id==id));
        }

        [ValidationAspect(typeof(UserChangePasswordValidator))]
        public async Task<IResult> ChangePassword(UserChangePasswordDto userChangePasswordDto)
        {
            var user = await _userDal.Get(p => p.Id == userChangePasswordDto.UserId);
            bool result = HashingHelper.VerifyPasswordHash(userChangePasswordDto.CurrentPassword, user.PasswordHash, user.PasswordSalt);
            if (!result)
            {
                return new ErrorResult(UserMessages.WrongCurrentPassword);
            };

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePassword(userChangePasswordDto.NewPassword, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            await _userDal.Update(user);
            return new SuccessResult(UserMessages.PasswordChanged);
        }

        public async Task<List<OperationClaim>> GetUserOperationClaims(int userId)
        {
            return _userDal.GetUserOperationClaims(userId);
        }
    }
}
