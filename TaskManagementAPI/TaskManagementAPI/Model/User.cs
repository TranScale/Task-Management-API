namespace TaskManagementAPI.Model
{
    public class User
    {
        public int UserId { get; private set; } // Id của người dùng
        public string UserName { get; private set; } = string.Empty; // Tên người dùng

        // Constructor rỗng
        public User() { }

        // Constructor có tham số
        public User(string userName)
        {
            UserName = userName;
        }

        // Phương thức để cập nhật thông tin người dùng
        public void UpdateUser(string? userName)
        {
            if (userName != null)
                UserName = userName;
        }
    }
}
