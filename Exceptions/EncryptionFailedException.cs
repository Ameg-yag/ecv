namespace ecv.exceptions
{
    [System.Serializable]
    public class EncryptionFailedException : System.Exception
    {
        public EncryptionFailedException() { }
        public EncryptionFailedException(string message) : base(message) { }
        public EncryptionFailedException(string message, System.Exception inner) : base(message, inner) { }
        protected EncryptionFailedException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}