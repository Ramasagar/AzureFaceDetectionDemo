namespace FaceDetection
{
    /// <summary>
    /// Config class for the Azure Cognative Services
    /// </summary>
    public class AzureCognativeServicesConfig
    {
        /// <summary>
        /// Gets or sets the face api config.
        /// </summary>
        /// <value>
        /// The face api config.
        /// </value>
        public FaceConfig Face { get; set; }
    }

    /// <summary>
    /// Config class for the Face API
    /// </summary>
    public class FaceConfig
    {
        /// <summary>
        /// Gets or sets the base URI.
        /// </summary>
        /// <value>
        /// The base URI.
        /// </value>
        public string BaseUri { get; set; }

        /// <summary>
        /// Gets or sets the API key.
        /// </summary>
        /// <value>
        /// The API key.
        /// </value>
        public string ApiKey { get; set; }
    }
}
