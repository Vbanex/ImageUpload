namespace ImageUpload.VM
{
    public class UserDataModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string About { get; set; }

        public IFormFile profilePicture { get; set; }
    }
}
