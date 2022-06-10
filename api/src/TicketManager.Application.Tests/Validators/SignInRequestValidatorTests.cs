using FluentValidation.TestHelper;
using TicketManager.Application.Validators;
using TicketManager.Shared.DTOs.Auth;
using Xunit;

namespace TicketManager.Application.Tests.Validators
{
    public class SignInRequestValidatorTests
    {
        private SignInRequestValidator _sut;

        public SignInRequestValidatorTests()
        {
            _sut = new SignInRequestValidator();
        }

        [Fact]
        public void SignInRequest_ValidFields_DoNotThrowValidationErrors()
        {
            var request = new SignInRequest { Email = "user@email.com", Password = "123456" };
            var result = _sut.TestValidate(request);
            result.ShouldNotHaveAnyValidationErrors();
        }
        
        [Fact]
        public void SignInRequest_InvalidEmail_ThrowsValidationError()
        {
            var request = new SignInRequest { Email = "useremail", Password = "123456" };
            var result = _sut.TestValidate(request);
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void SignInRequest_EmptyEmail_ThrowsValidationError()
        {
            var request = new SignInRequest { Email = "", Password = "123456" };
            var result = _sut.TestValidate(request);
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void SignInRequest_NullEmail_ThrowsValidationError()
        {
            var request = new SignInRequest { Password = "123456" };
            var result = _sut.TestValidate(request);
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }
        
        [Fact]
        public void SignInRequest_EmptyPassword_ThrowsValidationError()
        {
            var request = new SignInRequest { Email = "user@email.com", Password = "" };
            var result = _sut.TestValidate(request);
            result.ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Fact]
        public void SignInRequest_NullPassword_ThrowsValidationError()
        {
            var request = new SignInRequest { Email = "user@email.com" };
            var result = _sut.TestValidate(request);
            result.ShouldHaveValidationErrorFor(x => x.Password);
        }
    }
}
