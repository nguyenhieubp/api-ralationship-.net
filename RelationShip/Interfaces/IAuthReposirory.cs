using RelationShip.Dto;

namespace RelationShip.Interfaces
{
    public interface IAuthReposirory
    {
        public bool Register(AuthDto valueAuth);
        public string Login(AuthDto auth);
        public JwtDto DecodeJwt();
        public string RefreshAccessToken();
        public void GenerateOTP(OtpDto requestOtp);
        public bool CheckOtp(OtpDto requestOtp);
        public bool Logout();
    }
}
