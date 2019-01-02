using System;

namespace TeduNetcore.Application.ViewModels
{
    public class PermissionViewModel
    {
        public int Id { get; set; }
        public Guid RoleId { get; set; }

        public string FunctionId { get; set; }

        public bool CanCreate { set; get; }
        public bool CanRead { set; get; }

        public bool CanUpdate { set; get; }
        public bool CanDelete { set; get; }

        public bool CanCheckAll { get; set; }

        public override string ToString()
        {
            return $"RoleId: {RoleId} - FunctionId:{FunctionId}" +
                   $" - CanCreate:{CanCreate} - CanRead:{CanRead}" +
                   $" - CanUpdate:{CanUpdate} - CanDelete:{CanDelete}";
        }
    }
}