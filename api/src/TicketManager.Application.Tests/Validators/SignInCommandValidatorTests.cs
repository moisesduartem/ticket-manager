using FluentValidation.TestHelper;
using TicketManager.Application.Validators;
using TicketManager.Shared.DTOs.Auth;
using Xunit;

namespace TicketManager.Application.Tests.Validators
{
    public class SignInCommandValidatorTests
    {
        private SignInCommandValidator _sut;

        public SignInCommandValidatorTests()
        {
            _sut = new SignInCommandValidator();
        }

        [Fact]
        public void SignInCommand_ValidFields_DoNotThrowValidationErrors()
        {
            var command = new SignInCommand { Email = "user@email.com", Password = "123456" };
            var result = _sut.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }
        
        [Fact]
        public void SignInCommand_InvalidEmail_ThrowsValidationError()
        {
            var command = new SignInCommand { Email = "useremail", Password = "123456" };
            var result = _sut.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void SignInCommand_EmptyEmail_ThrowsValidationError()
        {
            var command = new SignInCommand { Email = "", Password = "123456" };
            var result = _sut.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void SignInCommand_NullEmail_ThrowsValidationError()
        {
            var command = new SignInCommand { Password = "123456" };
            var result = _sut.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }
        
        [Fact]
        public void SignInCommand_EmptyPassword_ThrowsValidationError()
        {
            var command = new SignInCommand { Email = "user@email.com", Password = "" };
            var result = _sut.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Fact]
        public void SignInCommand_NullPassword_ThrowsValidationError()
        {
            var command = new SignInCommand { Email = "user@email.com" };
            var result = _sut.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Password);
        }
    }
}
