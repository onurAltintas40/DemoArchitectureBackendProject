using Business.Repositories.UserRepository;
using Business.ValidationRules.FluentValidation;
using Core.Aspect.ValidationAspect;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Hashing;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using Core.Utilities.Security.JWT;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Authentication
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenHandler _tokenHandler;
       
        public AuthManager(IUserService userService, ITokenHandler tokenHandler)
        {
            _userService = userService;
            _tokenHandler = tokenHandler;
        }

        public async Task<IDataResult<Token>> Login(LoginAuthDto loginDto)
        {
            var user = await _userService.GetByEmail(loginDto.Email);
            var result = HashingHelper.VerifyPasswordHash(loginDto.Password, user.PasswordHash, user.PasswordSalt);
            List<OperationClaim> operationClaims = await _userService.GetUserOperationClaims(user.Id);

            if (result)
            {
                Token token = new Token();
                token = _tokenHandler.CreateToken(user, operationClaims);
                return new SuccessDataResult<Token>(token);
            }

            return new ErrorDataResult<Token>("Kullanıcı emaili ya da şifre bilgisi yanlış.");
        }

        [ValidationAspect(typeof(AuthValidator))]
        public async Task<IResult> Register(RegisterAuthDto registerDto)
        {
            int imgSize = 1;

            IResult result = BusinessRules.Run(await CheckEmailExists(registerDto.Email), CheckIfImageExtensionsAllow(registerDto.Image.FileName), CheckIfImageSizeisLessThanOneMb(registerDto.Image.Length));

            if (result != null)
            {
                return result;
            }

            await _userService.Add(registerDto);
            return new SuccessResult("Kullanıcı başarıyla eklendi.");

        }

        private IResult CheckIfImageSizeisLessThanOneMb(long imgSize)
        {
            decimal imgMbSize = Convert.ToDecimal(imgSize * 0.000001);
            if (imgSize > 2)
            {
                return new ErrorResult("Resim boyutu en fazla 1 Mb olmalıdır.");
            }

            return new SuccessResult();
        }

        private async Task<IResult> CheckEmailExists(string email)
        {
            var list = await _userService.GetByEmail(email);
            if (list != null)
            {
                return new ErrorResult("Bu email adresi daha önce kullanılmış.");
            }
            return new SuccessResult();
        }

        private IResult CheckIfImageExtensionsAllow(string fileName)
        {
            var ext = fileName.Substring(fileName.LastIndexOf('.'));
            var extension = ext.ToLower();
            List<string> AllowFileExtensions = new List<string> { ".jpg", ".jpeg", ".gif", ".png" };
            if (!AllowFileExtensions.Contains(extension))
            {
                return new ErrorResult("Eklediğniz resim jpg, jpeg, png, gif uzantılı olmalıdır. ");
            }
            return new SuccessResult();
        }
    }
}
