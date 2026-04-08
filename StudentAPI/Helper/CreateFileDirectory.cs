namespace StudentAPI.Helper
{
    public static class CreateFileDirectory
    {
        public static string CreateUploadFileDirectory(string uploadPathName)
        {
            string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), uploadPathName);

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            return uploadPath;
        }
    }
}
