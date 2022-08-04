// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using App.Domain.Core.ApplicationUserAgg.Entities;
using App.Domain.Core.FileAgg.Entities;
using App.Infrastructures.Database.SqlServer.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Utility;

namespace App.EndPoints.Web.Mvc.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IUserEmailStore<IdentityUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AppDbContext _dbContext;
        

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            IUserStore<IdentityUser> userStore,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IWebHostEnvironment webHostEnvironment,
            AppDbContext dbContext

            )
        {
            _userManager = userManager;
            _userStore = userStore;
            _roleManager = roleManager;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _webHostEnvironment = webHostEnvironment;
            _dbContext = dbContext;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
            /// <summary>
            /// Added By Parsa For Cutomize Register Form
            /// </summary>
            [Display(Name = "نام")]
            public string FirstName { get; set; }
            [Display(Name = "نام خانوادگی")]
            public string Family { get; set; }
            [Display(Name = "آدرس کامل شامل نام شهر، خیابان، کد پستی و ...")]
            public string? FullAddress { get; set; }
            public string? PhoneNumber { get; set; }
            public string? Role { get; set; }

            /*IList<IFormFile>? uploadFiles { get; set; }*/


            [ValidateNever]
            public IEnumerable<SelectListItem> RoleList { get; set; }
        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            //Start Parsa Customize

            if (!_roleManager.RoleExistsAsync(ConstantProperty.Role_Admin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(ConstantProperty.Role_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(ConstantProperty.Role_Expert)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(ConstantProperty.Role_Customer)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(ConstantProperty.Role_Normal)).GetAwaiter().GetResult();
            }

            //End Customize
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            //Customized By Parsa -- Send Role List To A Drop Down in View Page
            Input = new InputModel()
            {
                RoleList = _roleManager.Roles.Select(i => i.Name).Select(i => new SelectListItem
                {
                    Text = i,
                    Value = i
                }),
                

            };
            //End Of Parsa Customization
        }

        public async Task<IActionResult> OnPostAsync(IList<IFormFile>? uploadFiles, string returnUrl = null )
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = CreateUser();

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);

                //Customize By Parsa  - Add More Property To Be Saved
                user.FirstName = Input.FirstName;
                user.Family = Input.Family;
                user.PhoneNumber = Input.PhoneNumber;
                user.FullAddress = Input.FullAddress;
                //End Of Parsa Customization
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    if (Input.Role == null)
                    {
                        await _userManager.AddToRoleAsync(user,ConstantProperty.Role_Customer);
                    }
                    else
                    {
                        //await _userManager.AddToRoleAsync(user, Input.Role);
                        await _userManager.AddToRoleAsync(user, Input.Role);
                    }

                    var userId = await _userManager.GetUserIdAsync(user);
                    ///
                    /// User Image Saved By Parsa
                    ///
                    //Start Add Image And Other Files By Parsa
                    string webRootPath = _webHostEnvironment.WebRootPath;
                    foreach (var file in uploadFiles)
                    {
                        string NewLocaton = webRootPath + ConstantProperty.ImageUserPath;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(file.FileName);
                        using (var fileStream = new FileStream(Path.Combine(NewLocaton, fileName + extension), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }
                        AppFile f = new AppFile()
                        {
                            Name = fileName + extension
                        };
                        await _dbContext.AppFiles.AddAsync(f);
                        await _dbContext.SaveChangesAsync();
                        var fileId = f.Id;
                        
                        UserFile userFiles = new UserFile()
                        {
                            AppFileId = fileId,
                            UserId=userId,
                            CreatedAt = DateTime.Now
                        };
                        await _dbContext.UserFiles.AddAsync(userFiles);
                        await _dbContext.SaveChangesAsync();
                    }
                    //End Add Image Files By Parsa
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        if (User.IsInRole(ConstantProperty.Role_Admin))
                        {
                            TempData["success"] = "کاربر جدید با موقیت ثبت شد";

                           // return RedirectToAction("Index", "UserManagement");
                        }
                        else
                        {
                            await _signInManager.SignInAsync(user, isPersistent: false);
                        }
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        /* private IdentityUser CreateUser()
         {
             try
             {
                 return Activator.CreateInstance<IdentityUser>();
             }
             catch
             {
                 throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                     $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                     $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
             }
         }*/

        // Customize Above Method By Parsa
        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                    $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }


        //End Customize

        private IUserEmailStore<IdentityUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<IdentityUser>)_userStore;
        }
    }
}
