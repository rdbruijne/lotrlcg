using System.Collections.Generic;
using System.Drawing;
using Newtonsoft.Json.Linq;

namespace lotrlcg
{
	class Card
	{
		private readonly JToken Json = null;

		// card properties
		public int    Attack      => Json?.Value<int>("attack") ?? default;
		public string Code        => Json?.Value<string>("code") ?? default;
		public string Cost        => Json?.Value<string>("cost") ?? default;
		public int    DeckLimit   => Json?.Value<int>("deck_limit") ?? default;
		public int    Defense     => Json?.Value<int>("defense") ?? default;
		public string Flavor      => Json?.Value<string>("flavor") ?? default;
		public bool   HasErrata   => Json?.Value<bool>("has_errata") ?? default;
		public int    Health      => Json?.Value<int>("health") ?? default;
		public string Illustrator => Json?.Value<string>("illustrator") ?? default;
		public string Imagesrc    => Json?.Value<string>("imagesrc") ?? default;
		public string IsUnique    => Json?.Value<string>("is_unique") ?? default;
		public string Name        => Json?.Value<string>("name") ?? default;
		public string OctgnID     => Json?.Value<string>("octgnid") ?? default;
		public string PackCode    => Json?.Value<string>("pack_code") ?? default;
		public string PackName    => Json?.Value<string>("pack_name") ?? default;
		public int    Position    => Json?.Value<int>("position") ?? default;
		public int    Quantity    => Json?.Value<int>("quantity") ?? default;
		public string SphereCode  => Json?.Value<string>("sphere_code") ?? default;
		public string SphereName  => Json?.Value<string>("sphere_name") ?? default;
		public string Text        => Json?.Value<string>("text") ?? default;
		public string Threat      => Json?.Value<string>("threat") ?? default;
		public string Traits      => Json?.Value<string>("traits") ?? default;
		public string TypeCode    => Json?.Value<string>("type_code") ?? default;
		public string TypeName    => Json?.Value<string>("type_name") ?? default;
		public string Url         => Json?.Value<string>("url") ?? default;
		public int    Willpower   => Json?.Value<int>("willpower") ?? default;



		//
		// public
		//
		public Card(JToken jsonToken = null)
		{
			Json = jsonToken;
		}



		public static float CardHeight(float fontHeight)
		{
			return (new Card().GetPrintText().Count + 1) * fontHeight;
		}



		public void Print(Graphics graphics, Pen pen, Brush brush, Font font, float xPos, float yPos, float width, float height)
		{
			graphics.DrawRectangle(pen, xPos, yPos, width, height);

			if (Json != null)
			{
				List<string> text = GetPrintText();
				float fontHeight = font.GetHeight(graphics);
				float yPosText = yPos + fontHeight * 0.5f;
				for (int i = 0; i < text.Count; i++)
					graphics.DrawString(" " + text[i], font, brush, xPos, yPosText + (fontHeight * i));
			}
			else
			{
				graphics.DrawString("JSON error", font, brush, xPos, yPos);
			}
		}



		//
		// private
		//
		private List<string> GetPrintText()
		{
			return new List<string>
			{
				Name.Truncate(26, "..."),
#if false
				$"{TypeName} / {SphereName} / {Cost}{Threat}",
				$"{PackCode} {Position}"
#endif
			};
		}
	}
}
