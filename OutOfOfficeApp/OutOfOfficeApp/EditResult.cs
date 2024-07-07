namespace OutOfOfficeApp
{
    public class EditResult<T>
    {
        public bool IsSuccess { get; set; }
        public T Model { get; set; }
    }
}
