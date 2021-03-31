using SchoolDiary.Domain.Data.Entities;

namespace SchoolDiary.Helpers
{
    public static class ExtensionMehtods
    {
        public static User WithoutPassword(this User user)
        {
            if (user == null)
            {
                return null;
            }
            user.Password = null;
            return user;
        }
    }
}
