namespace ADB2CAuthorization
{
	public static class Constants
	{
		public static string ApplicationID = "5695f366-955a-4019-bf79-351f5fe51564";
		public static string[] Scopes = { ApplicationID };
		public static string SignUpSignInPolicy = "B2C_1_SiUpIn";
		public static string ResetPasswordPolicy = "<INSERT_ADB2C_POLICY_NAME_HERE>";
        //	public static string Authority = "https://login.microsoftonline.com/tfp/oauth2/nativeclient";

        public static string Authority = "https://login.microsoftonline.com/Ayubowold.onmicrosoft.com/oauth2/v2.0/authorize?p=B2C_1_SiUpIn&client_id=5695f366-955a-4019-bf79-351f5fe51564&nonce=defaultNonce&redirect_uri=urn%3Aietf%3Awg%3Aoauth%3A2.0%3Aoob&scope=openid&response_type=id_token&prompt=login";

    }
}
