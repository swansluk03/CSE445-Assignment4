using System;
using System.Xml.Schema;
using System.Xml;
using Newtonsoft.Json;
using System.IO;



/**
 * This template file is created for ASU CSE445 Distributed SW Dev Assignment 4.
 * Please do not modify or delete any existing class/variable/method names. However, you can add more variables and functions.
 * Uploading this file directly will not pass the autograder's compilation check, resulting in a grade of 0.
 * **/


namespace ConsoleApp1
{


    public class Program
    {
        public static string xmlURL = "https://raw.githubusercontent.com/swansluk03/CSE445-Assignment4/main/Hotels.xml";
        public static string xmlErrorURL = "https://raw.githubusercontent.com/swansluk03/CSE445-Assignment4/main/HotelErrors.xml";
        public static string xsdURL = "https://raw.githubusercontent.com/swansluk03/CSE445-Assignment4/main/Hotels.xsd";

        public static void Main(string[] args)
        {
            string result = Verification(xmlURL, xsdURL);
            Console.WriteLine(result);


            result = Verification(xmlErrorURL, xsdURL);
            Console.WriteLine(result);


            result = Xml2Json(xmlURL);
            Console.WriteLine(result);
        }

        // Q2.1
    public static string Verification(string xmlUrl, string xsdUrl)
    {
        try
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.Schemas.Add(null, xsdUrl);
            settings.ValidationType = ValidationType.Schema;

            string errorMsg = "No Error";
            settings.ValidationEventHandler += (sender, e) => { errorMsg = e.Message; };

            using (XmlReader reader = XmlReader.Create(xmlUrl, settings))
            {
                while (reader.Read()) { }
            }

            return errorMsg;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static string Xml2Json(string xmlUrl)
    {
        try
        {
            using (var client = new System.Net.WebClient())
            {
                string xmlContent = client.DownloadString(xmlUrl);
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlContent);
                string jsonText = JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.Indented);
                return jsonText;
            }
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
    }

}
