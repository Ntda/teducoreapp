namespace TeduNetcore.Application.ViewModels
{
    public class RoleViewModel
    {
        public string Id { set; get; }

        public string Name { set; get; }

        public string Description { set; get; }

        public override string ToString()
        {
            return $"Id:{Id} - Name:{Name} - Description:{Description}";
        }
    }
}
