using System;

namespace CrossCutting.Security.Identity
{
    /// <summary>
    /// Defines a user type
    /// </summary>
    [Flags]
    public enum UserType
    {
        /// <summary>
        /// No value. Mainly use is to check if has any value.
        /// </summary>
        None = 0,

        /// <summary>
        /// User account. Represents a person.
        /// </summary>
        User = 1, // 1

        /// <summary>
        /// Represents a resource like an API or a (internal) process
        /// Use case will be an API to API communication, similar to a machine to machine scenario
        /// </summary>
        Resource = 1 << 1, // 2

        /// <summary>
        /// Represents a machine like an application(s)/service(s) server
        /// Use case will be a machine to machine communication
        /// </summary>
        Machine = 1 << 2, // 3
    }
}
