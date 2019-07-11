using System;

namespace RsaSecureToken
{
    public class AuthenticationService
    {
        public bool IsValid(string account, string password)
        {
            // 根據 account 取得設定時間
            var profileDao = GetProfileDao();
            var registerMinutes = profileDao.GetRegisterTimeInMinutes(account);

            // 根據 registerMinutes 取得 RSA token 目前的亂數

            var rsaToken = GetRsaTokenDao();
            var seed = rsaToken.GetRandom(registerMinutes);

            // 驗證傳入的 password 是否等於 otp
            var isValid = password == seed.Next(0, 999999).ToString("000000"); ;

            return isValid;
        }

        protected virtual ProfileDao GetProfileDao()
        {
            var profileDao = new ProfileDao();
            return profileDao;
        }

        protected virtual RsaTokenDao GetRsaTokenDao()
        {
            var rsaToken = new RsaTokenDao();
            return rsaToken;
        }
    }

    public class ProfileDao
    {
        public virtual int GetRegisterTimeInMinutes(string account)
        {
            throw new InvalidOperationException();
        }
    }

    public class RsaTokenDao
    {
        public virtual Random GetRandom(int minutes)
        {
            return new Random((int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalMinutes - minutes);
        }
    }
}