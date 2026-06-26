using System;

namespace DolphinScript.Models
{
    public class UpdatePackage
    {
        public UpdatePackage(string version, object payload)
        {
            if (string.IsNullOrWhiteSpace(version))
            {
                throw new ArgumentException("Update version is required.", nameof(version));
            }

            Version = version;
            Payload = payload ?? throw new ArgumentNullException(nameof(payload));
        }

        public string Version { get; }

        internal object Payload { get; }
    }
}
