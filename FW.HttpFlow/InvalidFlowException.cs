using System;

namespace FW.HttpFlow
{
    /// <summary>
    /// InvalidFlowException represent the exception to throw when there is 
    /// an invalid flow in the HttpFlow layer
    /// </summary>
    public class InvalidFlowException : Exception
    {
        /// <summary>
        /// Creates a new InvalidFlowException object
        /// </summary>
        public InvalidFlowException()
            : base()
        {
        }


        /// <summary>
        /// Creates a new InvalidFlowException object
        /// </summary>
        /// <param name="message">
        /// The message that describes the error.
        /// </param>
        public InvalidFlowException(string message)
            : base(message)
        {
        }


        /// <summary>
        /// Creates a new InvalidFlowException object
        /// </summary>
        /// <param name="message">
        /// The message that describes the error.
        /// </param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, 
        /// or a null reference (Nothing in Visual Basic) if no inner exception is specified.
        /// </param>
        public InvalidFlowException(
            string message,
            Exception innerException)
            : base(message,
                   innerException)
        {
        }


        /// <summary>
        /// Creates a new InvalidFlowException object
        /// </summary>
        /// <param name="info">
        /// Holds the serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// Contextual information about the source or destination.
        /// </param>
        protected InvalidFlowException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
            : base(info,
                   context)
        {
        }
    }


}