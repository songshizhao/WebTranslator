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

namespace WebTranslator.Baidu
{

	public class BaiduTranslate{


		public static string appId = "";
		public static string secretKey = "";
		public static async Task<string> Baidu_Translate(string from, string to, string q)
		{

			try
			{
				Random rd = new Random();
				string salt = rd.Next(100000).ToString();
				// 改成您的密钥
				string sign = EncryptString(appId + q + salt + secretKey);
				string url = "http://api.fanyi.baidu.com/api/trans/vip/translate?";
				url += "q=" + HttpUtility.UrlEncode(q);
				url += "&from=" + from;
				url += "&to=" + to;
				url += "&appid=" + appId;
				url += "&salt=" + salt;
				url += "&sign=" + sign;
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
				request.Method = "GET";
				request.ContentType = "text/html;charset=UTF-8";
				request.UserAgent = null;
				request.Timeout = 6000;
				using (WebResponse response = await request.GetResponseAsync())
				{
					using (Stream myResponseStream = response.GetResponseStream())
					{
						using (StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8")))
						{
							string retString = myStreamReader.ReadToEnd();
							Debug.WriteLine(retString);
							var result = JsonConvert.DeserializeObject<Rootobject>(retString);
							return result.trans_result[0].dst;
						}
					}
				}
			}
			catch (Exception)
			{

				return "error";
			}

			






		}

		// 计算MD5值
		private static string EncryptString(string str)
		{
			MD5 md5 = MD5.Create();
			// 将字符串转换成字节数组
			byte[] byteOld = Encoding.UTF8.GetBytes(str);
			// 调用加密方法
			byte[] byteNew = md5.ComputeHash(byteOld);
			// 将加密结果转换为字符串
			StringBuilder sb = new StringBuilder();
			foreach (byte b in byteNew)
			{
				// 将字节转换成16进制表示的字符串，
				sb.Append(b.ToString("x2"));
			}
			// 返回加密的字符串
			return sb.ToString();
		}

	}

	public enum baidu_lan
	{
		/// <summary>
		/// 自动检测
		/// </summary>
		auto,
		zh,//  中文
		en,//  英语
		yue,// 粤语
		wyw,//文言文
		jp,//日语
		kor,//韩语
		fra,// 法语
		spa,//西班牙语
		th,//泰语
		ara,//阿拉伯语
		ru,//俄语
		pt,// 葡萄牙语
		de,//德语
		it,//意大利语
		el,// 希腊语
		nl,//荷兰语
		pl,//波兰语
		bul,//保加利亚语
		est,//爱沙尼亚语
		dan,//丹麦语
		fin,//芬兰语
		cs,// 捷克语
		rom,//罗马尼亚语
		slo,//斯洛文尼亚语
		swe,//瑞典语
		hu,//匈牙利语
		cht,//繁体中文
		vie,//
	}

	public class Rootobject
	{
		public string from { get; set; }
		public string to { get; set; }
		public string error_code { get; set; }
		public string error_msg { get; set; } = "";
		public Trans_Result[] trans_result { get; set; }
	}

	public class Trans_Result
	{
		public string src { get; set; }
		public string dst { get; set; }
	}





}

