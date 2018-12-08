namespace TeduNetcore.Infrastructure.SharedKernel
{
    public class DomainEntity<T>
    {
        public T Id { get; set; }

        public bool IsTransient()
        {
            return this.Id.Equals(default(T));
        }
    }
}