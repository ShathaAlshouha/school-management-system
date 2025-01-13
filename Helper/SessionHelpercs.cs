namespace SchoolProject.Helper
{
    public class SessionHelpercs
    {
        public static bool IsAdmin(HttpContext context)
        {
            return context.Session.Keys.Contains("isAdmin");
        }
       public static void SetAsUser(HttpContext context)
        {
            context.Session.SetString("isAdmin", "true");
        }
        public static bool IsTeacher(HttpContext context)
        {
            return context.Session.Keys.Contains("isTeacher");
        }
        public static void SetAsTeacher(HttpContext context)
        {
            context.Session.SetString("isTeacher", "true");
        }
        public static void ClearSession(HttpContext context)
        {
            context.Session.Clear();
        }

    }
}
