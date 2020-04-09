namespace ecv.exceptions
{
    [System.Serializable]
    public class DecryptionFailedException : System.Exception
    {
        public DecryptionFailedException() { }
        public DecryptionFailedException(string message) : base(message) { }
        public DecryptionFailedException(string message, System.Exception inner) : base(message, inner) { }
        protected DecryptionFailedException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}