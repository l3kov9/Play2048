namespace Game2048.App.Extensions
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public static class ControllerExtensions
    {
        public static string GetSessionString(this Controller controller, string sessionKey)
        {
            return controller.HttpContext.Session.GetString(sessionKey);
        }

        public static int GetSessionInt(this Controller controller, string sessionKey)
        {
            return (int)controller.HttpContext.Session.GetInt32(sessionKey);
        }

        public static void SetSessionString(this Controller controller, string sessionKey, string sessionValue)
        {
            controller.HttpContext.Session.SetString(sessionKey, sessionValue);
        }

        public static void SetSessionInt(this Controller controller, string sessionKey, int sessionValue)
        {
            controller.HttpContext.Session.SetInt32(sessionKey, sessionValue);
        }
    }
}
