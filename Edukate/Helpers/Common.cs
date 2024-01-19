namespace Edukate.Helpers
{
    public static class Common
    {
        public static bool IsValidId(this int? id)
            => id != null || id > 1;
    }
}
