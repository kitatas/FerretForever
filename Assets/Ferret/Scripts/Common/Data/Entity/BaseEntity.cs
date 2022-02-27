namespace Ferret.Common.Data.Entity
{
    public abstract class BaseEntity<T>
    {
        private T _t;

        public T Get() => _t;

        public void Set(T t) => _t = t;
    }
}