using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WebTranslator.Baidu;

namespace WebTranslator.Bing
{
	public class BingTranslate {


		//Ocp-Apim-Subscription-Key", "582851a2a66547dba353a9d307f77bda



		
		public static string Ocp_Apim_Subscription_Key="";

		//public static async Task<string> GetBingToken()
		//{

		//	string result = "";
		//	string host = "https://api.cognitive.microsoft.com/sts/v1.0/issueToken?Subscription-Key=57ec489540294ec98dd096028b36decc";

		//	HttpWebRequest request = WebRequest.Create(host) as HttpWebRequest;
		//	Encoding myEncoding = Encoding.UTF8;
		//	request.Method = "POST";
		//	request.ContentType = "application/json";
		//	using (WebResponse response = await request.GetResponseAsync())
		//	{
		//		Stream myResponseStream = response.GetResponseStream();
		//		using (StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8))
		//		{
		//			result = myStreamReader.ReadToEnd();
		//		}
		//	}
		//	Debug.WriteLine(result);
		//	return result;
		//}

		private class tContent
		{
			public string text { get; set; }
		}


		public static async Task<string> Bing_Translate(string from, string to, string content)
		{


			List<RootClass> result = null;
			//string token = await GetBingToken();

			var url = new Uri($"https://api.cognitive.microsofttranslator.com/translate?api-version=3.0&from={from}&to={to}", UriKind.Absolute);
			var httpClient = new System.Net.Http.HttpClient();


			tContent tc = new tContent { text = content };
			List<tContent> tcs = new List<tContent> { tc };
			string json = JsonConvert.SerializeObject(tcs);

			HttpContent httpContent = new StringContent(json);

			//设置Http的内容标头
			httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
			httpContent.Headers.Add("Ocp-Apim-Subscription-Key", Ocp_Apim_Subscription_Key);




			var reponse = await httpClient.PostAsync(url, httpContent);
			string r_json = await reponse.Content.ReadAsStringAsync();
			//Debug.WriteLine(r_json);
			try
			{
				result = JsonConvert.DeserializeObject<List<Bing.RootClass>>(r_json);
			}
			catch (Exception ex)
			{

				return "error";
			}



			return result[0].translations[0].text;

		}
	}

	public enum bing_lan
	{
		/// <summary>
		/// do not use 'auto' for bing, use "" instead.
		/// </summary>
		auto,
		af,
		ar,
		bn,
		bs,
		bg,
		yue,
		ca,
		/// <summary>
		/// zh-Hans
		/// </summary>
		zh_Hans,
		ans,
		/// <summary>
		/// zh-Hant
		/// </summary>
		zh_Hant,
		ant,
		hr,
		cs,
		da,
		nl,
		en,
		et,
		fj,
		fil,
		fi,
		fr,
		de,
		el,
		ht,
		he,
		hi,
		mww,
		hu,
		/// <summary>
		/// is instead _is
		/// </summary>
        _is,
		id,
		it,
		ja,
		sw,
		tlh,
		/// <summary>
		/// tlh-Qaak instead not tlh_Qaak
		/// </summary>
		tlh_Qaak,
		aak,
		ko,
		lv,
		lt,
		mg,
		ms,
		mt,
		nb,
		fa,
		pl,
		pt,
		otq,
		ro,
		ru,
		sm,
		/// <summary>
		/// sr-Latn
		/// </summary>
		sr_Latn,
		yrl,
		/// <summary>
		/// sr-Cyrl
		/// </summary>
		sr_Cyrl,
		atn,
		sk,
		sl,
		es,
		sv,
		ty,
		ta,
		te,
		th,
		to,
		tr,
		uk,
		ur,
		vi,
		cy,
		yua
	}




	public class RootClass
	{
		public Detectedlanguage detectedLanguage { get; set; }
		public Translation[] translations { get; set; }


		public Error error { get; set; }
	}

	public class Detectedlanguage
	{
		public string language { get; set; }
		public float score { get; set; }
	}

	public class Translation
	{
		public string text { get; set; }
		public Transliteration transliteration { get; set; }
		public string to { get; set; }
	}

	public class Transliteration
	{
		public string script { get; set; }
		public string text { get; set; }
	}

	public class Error
	{
		public int code { get; set; }
		public string message { get; set; }
	}

}


