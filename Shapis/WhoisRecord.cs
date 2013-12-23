using System.Collections;

namespace Shapis
{
    public class WhoisRecord
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WhoisRecord"/> class.
        /// </summary>
        public WhoisRecord()
	    {
            Text = new ArrayList();
	    }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public ArrayList Text { get; set; }

        /// <summary>
        /// Gets or sets the domain.
        /// </summary>
        /// <value>The domain.</value>
        public string Domain { get; set; }

        /// <summary>
        /// Gets or sets the server.
        /// </summary>
        /// <value>The server.</value>
        public string Server { get; set; }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            return Text.ToString();
        }
    }
}

