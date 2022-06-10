using AutoMapper;
using Moq;
using TicketManager.Application.Exceptions;
using TicketManager.Application.Services;
using TicketManager.Application.Utilities;
using TicketManager.Domain.Entities;
using TicketManager.Domain.Enums;
using TicketManager.Domain.Repositories;
using TicketManager.Domain.Services;
using TicketManager.Shared.DTOs.Auth;
using Xunit;

namespace TicketManager.Application.Tests.Services
{
    public class AuthServiceTests
    {
        private readonly AuthService _sut;
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<IAuthTokenService> _authTokenService;
        private readonly Mock<IBcrypt> _bcrypt;
        private readonly Mock<IMapper> _mapper;

        public AuthServiceTests()
        {
            _userRepository = new Mock<IUserRepository>();
            _authTokenService = new Mock<IAuthTokenService>();
            _bcrypt = new Mock<IBcrypt>();
            _mapper = new Mock<IMapper>();
            _sut = new AuthService(_userRepository.Object, _authTokenService.Object, _bcrypt.Object, _mapper.Object);
        }

        [Fact]
        public async Task SignInAsync_ValidScenario_ReturnViewModelWithToken()
        {
            var expectedToken = "USER_AUTH_TOKEN";
            var user = new User("User Name", "user@email.com", "abc", "def");
            var userDto = new AuthUserViewModel { Email = user.Email, Name = user.Name, Role = (int)UserRole.Regular };

            _userRepository.Setup(x => x.GetByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult(user));
            _bcrypt.Setup(x => x.Verify(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            _authTokenService.Setup(x => x.GenerateFor(It.IsAny<User>())).Returns(expectedToken);
            _mapper.Setup(x => x.Map<AuthUserViewModel>(It.IsAny<User>())).Returns(userDto);

            var request = new SignInRequest { Email = "user@email.com", Password = "123456" };
            var result = await _sut.SignInAsync(request);

            Assert.Equal(expected: expectedToken, actual: result.Value?.Token);
        }
        
        [Fact]
        public async Task SignInAsync_ValidScenario_ReturnViewModelWithUser()
        {
            var expectedToken = "USER_AUTH_TOKEN";
            var expectedUser = new User("User Name", "user@email.com", "abc", "def");
            var expectedUserDto = new AuthUserViewModel { Email = expectedUser.Email, Name = expectedUser.Name, Role = (int)UserRole.Regular };

            _userRepository.Setup(x => x.GetByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult(expectedUser));
            _bcrypt.Setup(x => x.Verify(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            _authTokenService.Setup(x => x.GenerateFor(It.IsAny<User>())).Returns(expectedToken);
            _mapper.Setup(x => x.Map<AuthUserViewModel>(It.IsAny<User>())).Returns(expectedUserDto);

            var request = new SignInRequest { Email = "user@email.com", Password = "123456" };
            var result = await _sut.SignInAsync(request);

            Assert.Equal(expected: expectedUserDto, actual: result.Value?.User);
        }

        [Fact]
        public async Task SignInAsync_NullUser_ReturnBadRequestResult()
        {
            _userRepository.Setup(x => x.GetByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult(null as User));
            var request = new SignInRequest { Email = "unexistent@email.com", Password = "123456" };
            var result = await _sut.SignInAsync(request);
            Assert.IsAssignableFrom<BadRequestException>(result.Exception);
        }

        [Fact]
        public async Task SignInAsync_NullUser_ReturnSpecificErrorMessage()
        {
            _userRepository.Setup(x => x.GetByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult(null as User));
            var request = new SignInRequest { Email = "unexistent@email.com", Password = "123456" };
            var result = await _sut.SignInAsync(request);
            Assert.Equal(expected: "Invalid email and/or password.", actual: result.Exception?.Message);
        }

        [Fact]
        public async Task SignInAsync_InvalidPassword_ReturnBadRequestResult()
        {
            var user = new User("User Name", "user@email.com", "abc", "def");
            _userRepository.Setup(x => x.GetByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult(user));
            _bcrypt.Setup(x => x.Verify(It.IsAny<string>(), It.IsAny<string>())).Returns(false);
            var request = new SignInRequest { Email = "unexistent@email.com", Password = "123456" };
            var result = await _sut.SignInAsync(request);
            Assert.IsAssignableFrom<BadRequestException>(result.Exception);
        }

        [Fact]
        public async Task SignInAsync_InvalidPassword_ReturnSpecificErrorMessage()
        {
            var user = new User("User Name", "user@email.com", "abc", "def");
            _userRepository.Setup(x => x.GetByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult(user));
            _bcrypt.Setup(x => x.Verify(It.IsAny<string>(), It.IsAny<string>())).Returns(false);
            var request = new SignInRequest { Email = "unexistent@email.com", Password = "123456" };
            var result = await _sut.SignInAsync(request);
            Assert.Equal(expected: "Invalid email and/or password.", actual: result.Exception?.Message);
        }
    }
}
