using System.ComponentModel.DataAnnotations;

namespace Genshin_Trade_Center.Models
{
    /// <summary>
    /// View Model responsible for storing 
    /// information for the Index View of the AccountController.
    /// </summary>
    /// <remarks></remarks>
    public class ManageViewModel
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        /// <remarks></remarks>
        public string Email { get; set; }
        /// <summary>
        /// Gets or sets the nickname.
        /// </summary>
        /// <value>
        /// The nickname.
        /// </value>
        /// <remarks></remarks>
        public string Nickname { get; set; }
    }

    /// <summary>
    /// View Model for the Login View.
    /// </summary>
    /// <remarks></remarks>
    public class AccountViewModel
    {
        /// <summary>
        /// Gets or sets the Login View Model.
        /// </summary>
        /// <value>
        /// The Login View Model.
        /// </value>
        /// <remarks></remarks>
        public LoginViewModel LoginViewModel { get; set; }
        /// <summary>
        /// Gets or sets the Register View Model.
        /// </summary>
        /// <value>
        /// Register View Model.
        /// </value>
        /// <remarks></remarks>
        public RegisterViewModel RegisterViewModel { get; set; }
    }

    /// <summary>
    /// View Model for logging into the website.
    /// Used in Login View.
    /// </summary>
    /// <remarks></remarks>
    public class LoginViewModel
    {
        /// <summary>
        /// Gets or sets the username.
        /// This is a required property.
        /// Displays as "Nickname".
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        /// <remarks></remarks>
        [Required]
        [Display(Name = "Nickname")]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// This is a required property.
        /// Displays as "Password".
        /// </summary>
        /// <value>
        /// the password.
        /// </value>
        /// <remarks></remarks>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }

    public class RegisterViewModel
    {
        /// <summary>
        /// Gets or sets the email.
        /// This is a required property.
        /// Displays as "Email".
        /// Validates as an email address.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        /// <remarks></remarks>
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        /// <summary>
        /// Gets or sets the username.
        /// This is a required property.
        /// Displays as "Nickname".
        /// Must be of length from 3 to 64.
        /// </summary>
        /// <value>
        /// the username.
        /// </value>
        /// <remarks></remarks>
        [Required]
        [StringLength(64, MinimumLength = 3,
                    ErrorMessage = "Name must be between 3 and 64 characters")]
        [Display(Name = "Nickname")]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// This is a required property.
        /// Displays as "Password".
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        /// <remarks></remarks>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least" +
                    " {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }


        /// <summary>
        /// Gets or sets the confirm password.
        /// It must be equal to the respective <see cref="Password"/>.
        /// </summary>
        /// <value>
        /// The confirm password.
        /// </value>
        /// <remarks></remarks>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password",
                    ErrorMessage = "The password and confirmation " +
                    "password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
