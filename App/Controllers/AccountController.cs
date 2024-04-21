using Infrastructure.Context;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApplication_G.ViewModels;

namespace WebApplication_G.Controllers;

[Authorize]

public class AccountController(UserManager<UserEntity> userManager, ApplicationContext context) : Controller
{
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly ApplicationContext _context = context;

    public async Task<IActionResult> Details()
    {
        var nameIdentifier = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var user = await _context.Users.Include(i => i.Adress).FirstOrDefaultAsync(x => x.Id == nameIdentifier);

        var viewModel = new AccountDetailsViewModel
        {
            Basic = new AccountBasicInfo
            {
                FirstName = user!.FirstName,
                LastName = user.LastName,
                Email = user.Email!,
                PhoneNumber = user.PhoneNumber,
                Bio = user.Bio,
            },
            Address = new AccountAddressInfo
            {
                AddressLine_1 = user.Adress?.AdressLine_1!,
                AddressLine_2 = user.Adress?.AdressLine_2!,
                PostalCode = user.Adress?.PostalCode!,
                City = user.Adress?.City!,
            }
        };
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateBasicInfo(AccountDetailsViewModel model)
    {
        if (TryValidateModel(model.Basic!))
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                user.FirstName = model.Basic!.FirstName;
                user.LastName = model.Basic!.LastName;
                user.Email = model.Basic!.Email;
                user.PhoneNumber = model.Basic!.PhoneNumber;
                user.UserName = model.Basic!.Email;
                user.Bio = model.Basic!.Bio;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    TempData["StatusMessage"] = "Updated basic information successfully.";
                }
                else
                {
                    TempData["StatusMessage"] = "Unable to save basic information.";
                }

            }
        }
        else
        {
            TempData["StatusMessage"] = "Unable to save basic information.";
        }
        return RedirectToAction("Details", "Account");
    }

    [HttpPost]
    public async Task<IActionResult> UpdateAddressInfo(AccountDetailsViewModel model)
    {
        if (TryValidateModel(model.Address!))
        {
            var nameIdentifier = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var user = await _context.Users.Include(i => i.Adress).FirstOrDefaultAsync(x => x.Id == nameIdentifier);
            if (user != null)
            {
                try
                {
                    if (user.Adress != null)
                    {
                        user.Adress.AdressLine_1 = model.Address!.AddressLine_1;
                        user.Adress.AdressLine_2 = model.Address!.AddressLine_2;
                        user.Adress.PostalCode = model.Address!.PostalCode;
                        user.Adress.City = model.Address!.City;
                    }
                    else
                    {
                        user.Adress = new AdressEntity
                        {
                            AdressLine_1 = model.Address!.AddressLine_1,
                            AdressLine_2 = model.Address!.AddressLine_2,
                            PostalCode = model.Address!.PostalCode,
                            City = model.Address!.City,
                        };
                    }

                    _context.Update(user);
                    await _context.SaveChangesAsync();

                    TempData["StatusMessage"] = "Updated address information successfully.";

                }
                catch
                {
                    TempData["StatusMessage"] = "Unable to save address information.";
                }
            }
        }
        else
        {
            TempData["StatusMessage"] = "Unable to save address information.";
        }
        return RedirectToAction("Details", "Account");
    }



    [HttpPost]
    public async Task<IActionResult> UploadProfileImage(IFormFile file)
    {
        var user = await _userManager.GetUserAsync(User);

        if (user != null && file != null && file.Length != 0)
        {
            var filename = $"p_{user.Id}_{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/uploads/profiles", filename);

            using var fs = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fs);

            user.ProfileImage = filename;
            await _userManager.UpdateAsync(user);
        }
        else
        {
            TempData["StatusMessage"] = "Unable to upload profile image.";
        }
        return RedirectToAction("Details", "Account");
    }
}
