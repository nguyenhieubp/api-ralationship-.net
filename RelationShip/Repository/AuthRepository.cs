using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using RelationShip.Data;
using RelationShip.Dto;
using RelationShip.Interfaces;
using RelationShip.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RelationShip.Repository
{
    public class AuthRepository : IAuthReposirory
    {
        private readonly Bcypt _bcypt;
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMailRepository _mailRepository;

        public AuthRepository(
            Bcypt bcypt,
            ApplicationDbContext db,
            IMapper mapper,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor,
            IMailRepository mailRepository)
        {
            _bcypt = bcypt;
            _db = db;
            _mapper = mapper;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _mailRepository = mailRepository;
        }

        public string Login(AuthDto auth)
        {
            var valueAuth = CheckUser(auth);
            if(valueAuth != null)
            {
                var token = GenderToken(valueAuth);
                var refreshToken = GenderRefreshToken(valueAuth);
                
                var auths = _db.Auths.FirstOrDefault((auth) => auth.AuthId == valueAuth.AuthId);
                if(auths != null)
                {
                    auths.RefreshToken = refreshToken;
                    _db.SaveChanges();
                }
                
                SaveDataToCookie("jwt", token);
                SaveDataToSession("jwtSession", token);
                return token;
            }
            else
            {
                return "Login Fail";
            }
        }

        public bool Register(AuthDto valueAuth)
        {
            var password = _bcypt.HashPassword(valueAuth.Password);
            var authModel = _mapper.Map<Auth>(valueAuth);
            authModel.Password = password;
            _db.Auths.Add(authModel);
            _db.SaveChanges();
            return true;
        }

        public Auth CheckUser(AuthDto auth)
        {
            var user = _db.Auths.FirstOrDefault((au) => au.Email == auth.Email);
            if (user != null && BCrypt.Net.BCrypt.Verify(auth.Password, user.Password))
            {
                return user;
            }
            return null;   
        }

        public string GenderToken(Auth auth)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
           

                var claims = new[]
                {
                    new Claim("AuthId", auth.AuthId.ToString()),
                    new Claim("UserId", auth.UserId.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, auth.Email),
                };

                var tokenDescriptor = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(1),
                    issuer: issuer,
                    audience: audience,
                    signingCredentials: credentials
                );

                var tokenHandler = new JwtSecurityTokenHandler();
                var accessToken = tokenHandler.WriteToken(tokenDescriptor);
                return accessToken;
        }

        private string GenderRefreshToken(Auth auth)
        {
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

            var claims = new[]
            {
                    new Claim("AuthId", auth.AuthId.ToString()),
                    new Claim("UserId", auth.UserId.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, auth.Email),
                };

            var tokenDescriptor = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(15),
                issuer: issuer,
                audience: audience,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            var refreshToken = tokenHandler.WriteToken(tokenDescriptor);
            return refreshToken;
        }

        public JwtDto DecodeJwt()
        {
            var jwtToken = GetJwtTokenFromBearerHeader();

            if (jwtToken != null)
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(jwtToken) as JwtSecurityToken;

                if (jsonToken.ValidTo < DateTime.UtcNow)
                {
                    return null;
                }

                // Lấy thông tin từ claims
                var userId = jsonToken?.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;
                var authId = jsonToken?.Claims.FirstOrDefault(claim => claim.Type == "AuthId")?.Value;
                var authEmail = jsonToken?.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Email)?.Value;

                var JwtResponse = new JwtDto()
                {
                    Email = authEmail,
                    AuthId = authId,
                    UserId = userId,
                };

                return JwtResponse;
            }
            return null;
        }

        public int DecodeRefreshToken(string refreshToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(refreshToken) as JwtSecurityToken;
            // Lấy thông tin từ claims
            var authId = jsonToken?.Claims.FirstOrDefault(claim => claim.Type == "AuthId")?.Value;
         

            return Int32.Parse(authId);
        }

        public string GetJwtTokenFromBearerHeader()
        {
            // Lấy chuỗi token từ header Authorization nếu có
            var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault();

            if (authorizationHeader != null && authorizationHeader.StartsWith("Bearer "))
            {
                // Trích xuất chuỗi token từ header "Bearer"
                return authorizationHeader.Substring("Bearer ".Length).Trim();
            }

            return null;
        }

        public void SaveDataToCookie(string key, string value)
        {
            var options = new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(1),
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None
            };
            _httpContextAccessor.HttpContext.Response.Cookies.Append(key, value, options);
        }

        public void SaveDataToSession(string key, string value)
        {
            _httpContextAccessor.HttpContext.Session.SetString(key, value);
        }

        public string RefreshAccessToken()
        {
                GetJwtTokenFromBearerHeader();
                var authId = DecodeJwt().AuthId;
                if (authId != null)
                {
                    Auth auth = _db.Auths.FirstOrDefault((auth) => auth.AuthId.ToString() == authId);
                    var authIdRefresh = DecodeRefreshToken(auth.RefreshToken);
                    if(authIdRefresh == Int32.Parse(authId)) 
                    {
                        var accessToken = GenderToken(auth);
                        return accessToken;
                    }
                    return "Not auth matches";
                }
                return "FAIL";
           
        }

        public void GenerateOTP(OtpDto requestOtp)
        {
            var author = _db.Auths.FirstOrDefault((auth) => auth.AuthId == requestOtp.AuthId);

            DeleteOldOTPs(requestOtp.AuthId);

            Random random = new Random();
            int codeOtp = random.Next(100000, 999999);
            OTP otp = _mapper.Map<OTP>(requestOtp);

            otp.ExpirationTime = DateTime.UtcNow.AddMinutes(1);

            otp.Code = codeOtp.ToString();
            _db.Otps.Add(otp);
            _db.SaveChanges();

            _mailRepository.SendOTPEmail(author.Email, otp.Code);
        }

        public bool CheckOtp(OtpDto requestOtp)
        {
            DeleteExpiredOTPs(requestOtp.AuthId);

            var author = _db.Auths.FirstOrDefault((auth) => auth.AuthId == requestOtp.AuthId);
            var otp = _db.Otps.FirstOrDefault((otp) => otp.AuthId == requestOtp.AuthId);
            
            if (otp != null && requestOtp.Code == otp.Code && DateTime.UtcNow < otp.ExpirationTime)
            {
                _db.Otps.Remove(otp);
                _db.SaveChanges();
                return true;
            }
            else
            {
                if (otp != null)
                {
                    _db.Otps.Remove(otp);
                    _db.SaveChanges();
                }
            }

            return false;
        }

        private void DeleteExpiredOTPs(int authId)
        {
            var expiredOTPs = _db.Otps.FirstOrDefault(otp => DateTime.UtcNow >= otp.ExpirationTime && otp.AuthId == authId);
            if(expiredOTPs != null)
            {
                _db.Otps.Remove(expiredOTPs);
                _db.SaveChanges();
            }
        }

        private void DeleteOldOTPs(int authId)
        {
            var oldOtp = _db.Otps.FirstOrDefault(otp => otp.AuthId == authId);

            if (oldOtp != null)
            {
                _db.Otps.Remove(oldOtp);
                _db.SaveChanges();
            }
        }

        public bool Logout()
        {
            JwtDto jwtDecode = DecodeJwt();
            if(jwtDecode != null)
            {
                Console.WriteLine(jwtDecode.AuthId);
                var auth = _db.Auths.FirstOrDefault((auth) => auth.AuthId == int.Parse(jwtDecode.AuthId));
                auth.RefreshToken = String.Empty;
                _db.SaveChanges();
                _httpContextAccessor.HttpContext.Response.Cookies.Delete("jwt");
                _httpContextAccessor.HttpContext.Session.Remove("jwtSession");
                return true;
            }
            return false;
        }
    }
}
