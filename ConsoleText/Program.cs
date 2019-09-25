using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebTranslator.Baidu;
using WebTranslator.Bing;

namespace ConsoleText
{
	class Program
	{
		static void Main(string[] args)
		{
			//Test for Baidu translate
			BaiduTranslate.appId = "replace your id";
			BaiduTranslate.secretKey = "replace your key";
			var dst=BaiduTranslate.Baidu_Translate(baidu_lan.auto.ToString(), baidu_lan.en.ToString(),"百度翻译测试").Result;
			Console.WriteLine(dst);
			Console.WriteLine("\n\r");
 
			//Test for Bing translate
			BingTranslate.Ocp_Apim_Subscription_Key= "replace your key";
			var result=BingTranslate.Bing_Translate("zh-Hans", bing_lan.en.ToString(),"必应翻译测试").Result;
			Console.WriteLine(result);
			Console.Read();
		}








	}
}
