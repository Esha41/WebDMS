namespace NLog.LayoutRenderers
{
    using System.ComponentModel;
    using System.Globalization;
    using System.Text;
    using NLog.Config;
    using System;

    /// <summary>
    /// Current date and time.
    /// </summary>
    [LayoutRenderer("epoch")]
    [ThreadAgnostic]
    public class EPOCHDateLayoutRenderer : LayoutRenderer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EPOCHDateLayoutRenderer" /> class.
        /// </summary>
        public EPOCHDateLayoutRenderer()
        {
        }

        /// <summary>
        /// Renders the current date and appends it to the specified <see cref="StringBuilder" />.
        /// </summary>
        /// <param name="builder">The <see cref="StringBuilder"/> to append the rendered data to.</param>
        /// <param name="logEvent">Logging event.</param>
        protected override void Append(StringBuilder builder, LogEventInfo logEvent)
        {
            var epochTime = DateTimeOffset.Now.ToUnixTimeSeconds();
            builder.Append(epochTime);
        }
    }
}