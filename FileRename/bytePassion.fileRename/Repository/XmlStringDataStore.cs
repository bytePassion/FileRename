using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;


namespace bytePassion.FileRename.Repository
{
	public class XmlStringDataStore
	{

		private readonly string filename;

		public XmlStringDataStore(string filename)
		{
			this.filename = filename;
		}

		private static XmlWriterSettings WriterSettings =>
            new XmlWriterSettings
            {
                Indent          = true,
                IndentChars     = "  ",
                NewLineChars    = "\r\n",
                NewLineHandling = NewLineHandling.Replace
            };

        private const string XmlRoot = "strings";

		private const string String         = "string";
		private const string ValueAttribute = "value";		

		public void Persist (IEnumerable<string> data)
		{

			var writer = XmlWriter.Create(filename, WriterSettings);

			writer.WriteStartDocument();

			writer.WriteStartElement(XmlRoot);

			foreach (var @string in data)
			{
				writer.WriteStartElement(String);
				writer.WriteAttributeString(ValueAttribute, @string);
				writer.WriteEndElement();
			}

			writer.WriteEndElement();

			writer.WriteEndDocument();
			writer.Close();
		}

		public IEnumerable<string> Load ()
		{
			IList<string> strings = new List<string>();

			if (!File.Exists(filename))
				return strings;
	
			var reader = XmlReader.Create(filename);

			while (reader.Read())
			{
				if (reader.NodeType != XmlNodeType.Element || reader.Name != XmlRoot) continue;

				while (reader.Read())
				{
					if (reader.NodeType != XmlNodeType.Element || reader.Name != String) continue;
					if (!reader.HasAttributes) continue;

					var value = string.Empty;					

					while (reader.MoveToNextAttribute())
					{
						if (reader.Name == ValueAttribute)
							value = reader.Value;
					}

					strings.Add(value);
				}
			}
			reader.Close();

			return strings;
		}
	}
}
