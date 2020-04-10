[System.Serializable]
public class FileReadException : System.Exception
{
    public FileReadException() { }
    public FileReadException(string message) : base(message) { }
    public FileReadException(string message, System.Exception inner) : base(message, inner) { }
    protected FileReadException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}