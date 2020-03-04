namespace b0tweb
{
    /// <summary>
    /// The configuration of all the b0tweb variables.
    /// </summary>
    class Configuration
    {
        /// <summary>
        /// The IRC password to use for the <see cref="IRCConnectionController"/>
        /// </summary>
        public static string IRCPassword = "";

        /// <summary>
        /// The IRC server to use for the <see cref="IRCConnectionController"/>
        /// </summary>
        public static string IRCServer = "";

        /// <summary>
        /// The HTTP username to use for HTTP uploads with <see cref="HTTPUpload"/>
        /// </summary>
        public static string HTTPUsername = "";

        /// <summary>
        /// The HTTP password to use for HTTP uploads with <see cref="HTTPUpload"/>
        /// </summary>
        public static string HTTPPassword = "";

        /// <summary>
        /// The HTTP server to use for HTTP uploads with <see cref="HTTPUpload"/>
        /// </summary>
        public static string HTTPServer = "";
    }
}
