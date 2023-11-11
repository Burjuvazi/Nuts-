

using AutoMapper;


using Kuruyemis.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Kuruyemis.Entities.Concrete.Dtos;
using Kuruyemis.Entities.Dtos;

namespace Kuruyemis.Business.Abstract
{
    public interface IAuthService
    {
        UserManager<AppUser> _userManager { get; set; }
        RoleManager<AppRole> _roleManager { get; set; }
        SignInManager<AppUser> _signInManager { get; set; }
        IMapper _mapper { get; set; }

        public async Task<AppUser> GetUser(string email)
        {
            AppUser user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == email);
            return user;
        }

        public async Task<AppUser> GetUserByUserName(string userName)
        {
            return await _userManager.Users.FirstOrDefaultAsync(user => user.UserName == userName);
        }

        public async Task<SignInResult> Login(LoginDto loginDto)
        {
            AppUser user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == loginDto.Email);

            return user == null ? new SignInResult() : await _signInManager.PasswordSignInAsync(user, loginDto.Password, false, false);
        }
        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
        public async Task<IdentityResult> AddRoleToUser(AppUser user, string role)
        {
            AppRole rol = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Name == role);
            if (rol is null)
            {
                await _roleManager.CreateAsync(new AppRole() { Name = role, NormalizedName = role.ToUpper() });
            }
            return await _userManager.AddToRoleAsync(user, role);
        }
        public async Task<IdentityResult> Register(RegisterDto registerDto)
        {
            AppUser user = _mapper.Map<AppUser>(registerDto);
            IdentityResult result = await _userManager.CreateAsync(user, registerDto.Password);
            if (result.Succeeded)
            {
                await AddRoleToUser(user, "User");
            }
            return result;
        }

        public async Task<IdentityResult> CustomerRegister(CustomerRegisterDto customerRegisterDto)
        {
            var user = new AppUser {
                UserName = customerRegisterDto.UserName,
                Email = customerRegisterDto.UserName,
                EmailConfirmed = true
            };
            var result = await _userManager.CreateAsync(user, customerRegisterDto.Password);

            if (result.Succeeded)
            {
                await AddRoleToUser(user, "Customer");
            }
            return result;
        }


        public async Task<IdentityResult> SellerRegister(SellerRegisterDto sellerRegisterDto)
        {
            var user = new AppUser
            {
                UserName = sellerRegisterDto.UserName,
                Email = sellerRegisterDto.Email
            };
            var seller = new Seller
            {

                Address = sellerRegisterDto.Address,
                FirstName = sellerRegisterDto.FirstName,
                LastName = sellerRegisterDto.LastName,
                SellerName = sellerRegisterDto.SellerName
            };
            user.Seller = seller; //Gelen user bir satıcı, bu user a satıcı bilgilerini ver
            var result = await _userManager.CreateAsync(user, sellerRegisterDto.Password);
            if (result.Succeeded)
            {
                await AddRoleToUser(user, "Seller");
            }
            return result;
        }

        public AppUser GetUserByEmail(string email)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Email == email);
            if (user is null)
            {
                return null;
            }
            return user;
        }

        public async Task<string> GetRoleByUserName(string userName)
        {
            var user = await GetUserByUserName(userName);
            var roleList = (await _userManager.GetRolesAsync(user));
            if (roleList.Count > 0)
            {
                return roleList[0];
            }
            return null;
        }


        public async Task<string> CreateToken(LoginDto loginDto)
        {
            string token = "";
            // Kullanıcı veritabanında varmı yokmu tespit ediyoruz.
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user is not null)
            {
                // Tespit edilen kullanıcının parola kontrolü yapılır.
                SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, user.LockoutEnabled);
                // Parola doğruysa
                if (result.Succeeded)
                {
                    // Oluşacak cookie için bir isimlendirme veriyoruz.
                    byte[] key = Encoding.UTF8.GetBytes("keynamevalue");

                    // Claimsler role bazlı yetkilendirmeler yetmediğinde kullanılırlar. Örneğin roller admin ama birinin yaşı 18 altında kaldıgı için girememesi gerekiyor. Bu tür durumlarda claimsler devreye girer.
                    //Burada claimsler vasıtasıyla kullanıcının bilgilerini oluşturuyoruz.
                    List<Claim> claims = new List<Claim>()
                    {
                        new Claim("UserId",user.Id.ToString()),
                        new Claim("Email",user.Email),
                        new Claim("UserName",user.UserName),
                        new Claim("PhoneNumber",user.PhoneNumber)
                    };
                    // Kullanıcı rollerini claimslere eklemek için çekiyoruz. Bir kullanının birden fazla rolü olabileceği için liste halinde dönmektedir.
                    var roles = await _userManager.GetRolesAsync(user);
                    foreach (string role in roles) claims.Add(new Claim(ClaimTypes.Role, role));
                    // Claimslerimizi toplayarak kimlik oluşturuyoruz.
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims);
                    // oluşacak token için özellikler ekliyoruz.
                    SecurityTokenDescriptor securityToken = new SecurityTokenDescriptor
                    {
                        Subject = claimsIdentity,
                        Expires = DateTime.Now.AddDays(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };
                    // Token oluşturmak amacı ile JwtSecurityTokenHandler nesnesinden yararlanıyoruz.
                    JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                    // Token oluşturulma aşaması
                    SecurityToken jwtToken = tokenHandler.CreateToken(securityToken);
                    // Kullanıcıya string olarak tokenı döndürebilmek için WriteToken metodundan yararlanıyoruz.
                    token = tokenHandler.WriteToken(jwtToken);
                    return token;
                }
            }
            return token;
        }

    }
}
