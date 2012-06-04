using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Xml;

namespace WSWP 
{
    public static class BGGApi
    {
        public static GameCollection GetUserGameList(string userName)
        {
            GameCollection list = new GameCollection();
            string xml;
            if (string.IsNullOrEmpty(userName))
                return list;
            const string URI = "http://www.boardgamegeek.com/xmlapi/collection/{0}?own=1";
            try
            {
                WebRequest request = HttpWebRequest.Create(string.Format(URI, userName));
                WebResponse response = request.GetResponse();
                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    xml = sr.ReadToEnd();
                }
                if (string.IsNullOrEmpty(xml))
                    return list;
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);
                foreach (XmlElement item in doc.DocumentElement.ChildNodes)
                {
                    Game game = new Game(item);
                    list.Add(game);
                }
            }
            catch (Exception ex)
            { throw new ApplicationException("There was an error while retrieving your game list from boardgamegeek.com", ex); }
            return list;
        }
    }
}
