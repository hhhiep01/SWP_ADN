namespace Application.Request.Role
{
    public class RoleUpdateRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
} 