using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace VPE
{
	public static class Codepage
	{
		/// <summary>Array of allowed chars, the codepage. The basis of everything. Char count MUST be divisible by 2, for paired tables. Currently 720.</summary>
		public static readonly string[] CharSet =
		[
			" ", // Ordinary space
			// Uppercase latin letters, with diacritics.
			"A", "Á", "Ä", "À", "Å", "Ā", "Ã", "Â", "Ă", "Ą", "Ǻ", "Æ", "Ǽ", "B", "C", "Ć", "Č", "Ç", "Ċ", "D", "Ď", "Đ", "Ð", "ʤ", "E", "Ė", "É", "Ě", "Ë", "Ē", "Ê", "È", "Ę", "Ȅ", "Ə", "F", "G", "Ģ", "Ğ", "Ĝ", "Ġ", "H", "Ĥ", "Ħ", "I", "Í", "Ï", "Ī", "Ì", "İ", "Į", "Ĩ", "Î", "Ȉ", "Ĳ", "J", "K", "Ķ", "L", "Ł", "Ļ", "Ľ", "Ĺ", "Ŀ", "M", "N", "Ň", "Ñ", "Ņ", "Ŋ", "Ń", "ǋ", "O", "Ó", "Ö", "Ō", "Õ", "Ô", "Ò", "Ő", "Ø", "Ǿ", "Œ", "P", "Q", "R", "Ř", "Ŗ", "Ŕ", "S", "Š", "Ş", "Ș", "Ś", "ẞ", "T", "Ť", "Ţ", "Ț", "Ŧ", "Þ", "U", "Ú", "Ů", "Ü", "Ū", "Ù", "Ű", "Û", "Ų", "V", "W", "Ŵ", "Ẅ", "Ƿ", "X", "Y", "Ý", "Ÿ", "Ŷ", "Z", "Ż", "Ź", "Ž", "Ƶ", "Ǯ",
			// Lowercase latin letters, with diacritics.
			"a", "á", "ä", "à", "å", "ā", "ã", "â", "ă", "ą", "ǻ", "æ", "ǽ", "b", "c", "ć", "č", "ç", "ċ", "d", "ď", "đ", "ð", "ʣ", "e", "ė", "é", "ě", "ë", "ē", "ê", "è", "ę", "ȅ", "ə", "f", "g", "ģ", "ğ", "ĝ", "ġ", "h", "ĥ", "ħ", "i", "í", "ï", "ī", "ì", "ı", "į", "ĩ", "î", "ȉ", "ĳ", "j", "k", "ķ", "l", "ł", "ļ", "ľ", "ĺ", "ŀ", "m", "n", "ň", "ñ", "ņ", "ŋ", "ń", "ǌ", "o", "ó", "ö", "ō", "õ", "ô", "ò", "ő", "ø", "ǿ", "œ", "p", "q", "r", "ř", "ŗ", "ŕ", "s", "š", "ş", "ș", "ś", "ß", "t", "ť", "ţ", "ț", "ŧ", "þ", "u", "ú", "ů", "ü", "ū", "ù", "ű", "û", "ų", "v", "w", "ŵ", "ẅ", "ƿ", "x", "y", "ý", "ÿ", "ŷ", "z", "ż", "ź", "ž", "ƶ", "ǯ",
			// Uppercase Greek letters, with diacritics.
			"Α", "Ά", "Β", "Γ", "Δ", "Ε", "Έ", "Ζ", "Η", "Ή", "Θ", "Ι", "Ί", "Ϊ", "Κ", "Λ", "Μ", "Ν", "Ξ", "Ο", "Ό", "Π", "Ρ", "Σ", "Τ", "Υ", "Ύ", "Ϋ", "Φ", "Χ", "Ψ", "Ω", "Ώ",
			// Lowercase Greek letters, with diacritics.
			"α", "ά", "β", "γ", "δ", "ε", "έ", "ζ", "η", "ή", "θ", "ι", "ί", "ϊ", "ΐ", "κ", "λ", "μ", "ν", "ξ", "ο", "ό", "π", "ρ", "σ", "ς", "τ", "υ", "ύ", "ϋ", "ΰ", "φ", "χ", "ψ", "ω", "ώ",
			// Cyrilic, uppercase, all (I hope) variants.
			"А", "Ӑ", "Ӓ", "Б", "В", "Г", "Ѓ", "Ґ", "Д", "Ђ", "Е", "Ё", "Э", "Ж", "Ӂ", "З", "И", "Й", "Ӣ", "Ӥ", "К", "Л", "М", "Н", "О", "Ӧ", "П", "Р", "С", "Т", "У", "Ӱ", "Ф", "Х", "Ц", "Ч", "Ш", "Щ", "Ъ", "Ы", "Ь", "Ю", "Я", "Ә", "Ғ", "Қ", "Ң", "Ө", "Ұ", "Ү", "Һ", "І", "Ј", "Љ", "Њ", "Ћ", "Џ",
			// Cyrilic, lowercase, all (I hope) variants.
			"а", "ӑ", "ӓ", "б", "в", "г", "ѓ", "ґ", "д", "ђ", "е", "ё", "э", "ж", "ӂ", "з", "и", "й", "ӣ", "ӥ", "к", "л", "м", "н", "о", "ӧ", "п", "р", "с", "т", "у", "ӱ", "ф", "х", "ц", "ч", "ш", "щ", "ъ", "ы", "ь", "ю", "я", "ә", "ғ", "қ", "ң", "ө", "ұ", "ү", "һ", "і", "ј", "љ", "њ", "ћ", "џ",
			// Georgian. Upper or lowercase, IDK. Maybe not even the full set.
			"ა", "ბ", "გ", "დ", "ე", "ვ", "ზ", "თ", "ი", "კ", "ლ", "მ", "ნ", "ო", "პ", "ჟ", "რ", "ს", "ტ", "უ", "ფ", "ქ", "ღ", "ყ", "შ", "ჩ", "ც", "ძ", "წ", "ჭ", "ხ", "ჯ", "ჰ",
			// Armenian, upercase
			"Ա", "Բ", "Գ", "Դ", "Ե", "Զ", "Է", "Ը", "Թ", "Ժ", "Ի", "Լ", "Խ", "Ծ", "Կ", "Հ", "Ձ", "Ղ", "Ճ", "Մ", "Յ", "Ն", "Շ", "Ո", "Չ", "Պ", "Ջ", "Ռ", "Ս", "Վ", "Տ", "Ր", "Ց", "Ւ", "Փ", "Ք", "Օ", "Ֆ",
			// Armenian, lowercase
			"ա", "բ", "գ", "դ", "ե", "զ", "է", "ը", "թ", "ժ", "ի", "լ", "խ", "ծ", "կ", "հ", "ձ", "ղ", "ճ", "մ", "յ", "ն", "շ", "ո", "չ", "պ", "ջ", "ռ", "ս", "վ", "տ", "ր", "ց", "ւ", "փ", "ք", "օ", "ֆ",
			// Digits, different variants.
			"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "⁰", "¹", "²", "³", "⁴", "⁵", "⁶", "⁷", "⁸", "⁹", "₀", "₁", "₂", "₃", "₄", "₅", "₆", "₇", "₈", "₉", "⓪", "①", "②", "③", "④", "⑤", "⑥", "⑦", "⑧", "⑨", "Ⅰ", "Ⅱ", "Ⅲ", "Ⅳ", "Ⅴ", "Ⅵ", "Ⅶ", "Ⅷ", "Ⅸ", "Ⅹ", "Ⅺ", "Ⅻ", "Ⅼ", "Ⅽ", "Ⅾ", "Ⅿ",
			// Interpunction, parenthesis, symbols, currency symbols.
			",", ".", "-", "?", "¿", "!", "¡", "_", ":", ";", "(", ")", "[", "]", "{", "}", "<", ">", "«", "»", "‹", "›", "∈", "∉", "∫", "⌈", "⌉", "⌊", "⌋", "–", "—", "…", "„", "“", "”", "/", "\\", "|", "\'", "\"", "%", "‰", "=", "+", "*", "^", "@", "#", "§", "€", "¥", "₹", "₽", "₩", "₿", "₦", "₲", "$", "£", "₣", "₱", "¢", "°", "÷", "×", "©", "®", "™", "≠", "≈", "←", "↑", "→", "↓", "⭦", "⭧", "⭨", "⭩", "↔", "↕", "•", "○", "√", "∞", "≤", "≥", "±", "~", "‼", "⁈", "⁇", "¶", "✓", "✕", "☢", "☣", "∛", "∜", "¦", "□", "○", "△", "∀", "∃", "∐", "†",
			"’", // Apostophe symbol. Different from minute symbol and '.
			"′", // Minute symbol.
			"″", // Second symbol.
			"Ω", // Ohm sign, different from Greek letter.
			"µ", // Micro sign, different from Greek letter.
			"∆", // Absolute difference, different from Greek letter.
			"∂", // Relative difference, different from Greek letter.
			"∑", // Summation symbol, different from Greek letter.
			"∏", // Product symbol, different from Greek letter.
			" ", // Non-breaking space.
			" ", // Half-space.
			"\t", // Tab char.
			"\r\n", // New line. Done like this by design.
		];
		private static readonly ushort LimitV = Convert.ToUInt16(CharSet.Length);
		/// <summary>Gets the number of chars in the codepage. Currently 720, which is a highly composite number. Chosen by design.</summary>
		public static ushort Limit
		{
			get
			{
				return LimitV;
			}
		}
		/// <summary>Charset used for base-128 encoding. Aimed at maximum compatibility for different sites.</summary>
		private static readonly char[] Base128 =
		[
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
			'A', 'Á', 'Ä', 'Æ', 'B', 'C', 'Č', 'D', 'Ď', 'E', 'É', 'Ě', 'Ë', 'F', 'G', 'Ğ', 'H', 'I', 'Í', 'J', 'K', 'L', 'Ł', 'Ľ', 'M', 'N', 'Ň', 'Ñ', 'O', 'Ó', 'Ö', 'P', 'Q', 'R', 'Ř', 'S', 'Š', 'T', 'Ť', 'Þ', 'U', 'Ú', 'Ů', 'Ü', 'V', 'W', 'X', 'Y', 'Ý', 'Z', 'Ž',
			'a', 'á', 'ä', 'æ', 'b', 'c', 'č', 'd', 'ď', 'e', 'é', 'ě', 'ë', 'f', 'g', 'ğ', 'h', 'i', 'í', 'j', 'k', 'l', 'ł', 'ľ', 'm', 'n', 'ň', 'ñ', 'o', 'ó', 'ö', 'p', 'q', 'r', 'ř', 's', 'š', 't', 'ť', 'þ', 'u', 'ú', 'ů', 'ü', 'v', 'w', 'x', 'y', 'ý', 'z', 'ž',
			',', '.', '¿', '¡', '?', '!', ':', ';', '§', '¶', '°', '¢', '±', '«', '»', '€'
		];
		/// <summary>Converts text message to set of numbers using the codepage.</summary>
		/// <param name="text">Text message.</param>
		/// <returns>Numbers representing a message.</returns>
		public static List<ushort> ConvertToNums(string text)
		{
			List<ushort> result = new(text.Length);
			for (int i = 0; i < text.Length; i++)
			{
				char Ch = text[i];
				char N = i == (text.Length - 1) ? '\0' : text[i + 1];
				if (Ch == '\r')
				{
					result.Add((ushort)Array.IndexOf(CharSet, "\r\n"));
					i = (N == '\n') || (N == '\0') ? i + 1 : i;
					continue;
				}
				else if (Ch == '\n')
				{
					result.Add((ushort)Array.IndexOf(CharSet, "\r\n"));
					continue;
				}
				int index = Array.IndexOf(CharSet, Convert.ToString(Ch));
				if (index >= 0)
				{
					result.Add((ushort)index);
				}
				else
				{
					continue;
				}
			}
			return result;
		}
		/// <summary>Converts set of numbers to text message using the codepage.</summary>
		/// <param name="nums">Numbers representing a message.</param>
		/// <returns>Text message.</returns>
		public static string ConvertToString(List<ushort> nums)
		{
			StringBuilder result = new();
			foreach (ushort num in nums)
			{
				if (num < Limit)
				{
					result.Append(CharSet[num]);
				}
				else
				{ // ToDo: Throw some kind of error when invalid char is present.
					result.Append('✳'); // Placeholder for unknown char.
				}
			}
			return result.ToString();
		}
		/// <summary>Encodes 7 bit numbers to their representing chars in base-128.</summary>
		/// <param name="data">7 bit numbers to encode. If number is greater than 127, it will be skipped.</param>
		/// <param name="spaces">Add a space every 8th char?</param>
		/// <returns>Text encoding of the 7 bit numbers.</returns>
		public static string ConvertToBase128Text(byte[] data, bool spaces)
		{
			StringBuilder sb = new();
			if (spaces)
			{
				for (int i = 0; i < data.Length; i++)
				{
					if (data[i] < 128)
					{
						sb.Append(Base128[data[i]]);
					}
					if ((i % 8 == 0) && (i != 0))
					{
						sb.Append(' ');
					}
				}
			}
			else
			{
				for (int i = 0; i < data.Length; i++)
				{
					if (data[i] < 128)
					{
						sb.Append(Base128[data[i]]);
					}
				}
			}
			return sb.ToString();
		}
		/// <summary>Decodes base-128 chars to 7 bit numbers.</summary>
		/// <param name="data">Text representing base-128 encoding.</param>
		/// <returns>Decoded 7 bit numbers.</returns>
		public static byte[] ConvertFromBase128Text(string data)
		{
			byte[] result = new byte[data.Length];
			for (int i = 0; i < result.Length; i++)
			{
				if (data[i] == ' ')
				{
					continue;
				}
				else
				{
					int idx = Array.IndexOf(Base128, data[i]);
					if (idx >= 0 && idx <= 127)
					{
						result[i] = (byte)idx;
					}
				}
			}
			return result;
		}
		/// <summary>Wrapper, calculates message entropy from…well…message, as letters.</summary>
		/// <param name="message">Message as a string.</param>
		/// <returns>Array of 3 numbers: Entropy (normalized), number of unique chars, message length.</returns>
		public static double[] CalculateMessageEntropy(string message)
		{
			return CalculateMessageEntropy(ConvertToNums(message));
		}
		/// <summary>Calculates message entropy from numerical representation of the message.</summary>
		/// <param name="message">Message as numerical representation.</param>
		/// <returns>Array of 3 numbers: Entropy (normalized), number of unique chars, message length.</returns>
		public static double[] CalculateMessageEntropy(List<ushort> message)
		{
			IEnumerable<KeyValuePair<ushort, int>> counts = message.GroupBy(a => a).Select(group => new KeyValuePair<ushort, int>(group.Key, group.Count())).OrderBy(item => item.Key);
			double entropy = 0;
			foreach(KeyValuePair<ushort, int> pair in counts)
			{
				double ratio = (double)pair.Value / message.Count;
				entropy += ratio * Math.Log2(ratio);
			}
			entropy *= -1;
			return [entropy / Math.Log2(Limit), counts.Count(), message.Count];
		}
	}
}