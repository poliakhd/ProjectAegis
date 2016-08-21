namespace ProjectAegis.Shop.Providers
{
    using System;
    using System.IO;
    using System.Net;
    using System.Collections.Generic;

    using Interfaces;

    class BasicItemInformationProvider : IItemInformationProvider
    {
        #region Private Members

        private Dictionary<int, string> _itemsDescriptions;
        private List<string> _availableProviders;

        #endregion

        public BasicItemInformationProvider()
        {
            _itemsDescriptions = new Dictionary<int, string>();
            _availableProviders = new List<string>();

            Initialize();
        }

        #region Implementation of IItemInformationProvider

        public void Initialize()
        {
            #region item_ext_desc.txt

            _availableProviders.Add("item_ext_desc.txt");

            try
            {
                using (var streamReader = new StreamReader(new FileStream("item_ext_desc.txt", FileMode.Open, FileAccess.Read)))
                {
                    while (!streamReader.EndOfStream)
                    {
                        string item = streamReader.ReadLine();

                        if (!item.StartsWith("#") && !item.StartsWith("//") && item != "")
                        {
                            int id = int.Parse(item.Substring(0, item.IndexOf(" ")));

                            string description = item.Substring(item.IndexOf(" ") + 2);

                            if (!description.EndsWith("\""))
                            {
                                string nextDescriptionLine;

                                do
                                {
                                    nextDescriptionLine = streamReader.ReadLine();
                                    description += nextDescriptionLine;
                                } while (!nextDescriptionLine.EndsWith("\""));
                            }

                            description = description.Replace("\"", "");
                            
                            _itemsDescriptions.Add(id, description);
                        }
                    }
                }
            }
            catch (Exception)
            {
                _availableProviders.Remove("item_ext_desc.txt");
            }

            #endregion

            #region pwdatabase.com/ru
            
            _availableProviders.Add("pwdatabase.com/ru");

            #endregion

            #region pwdatabase.com/en

            _availableProviders.Add("pwdatabase.com/en");

            #endregion
        }

        public string GetName(int itemId, string provider)
        {
            var name = string.Empty;

            switch (provider)
            {
                case "pwdatabase.com/ru":
                    name = NameFromRuWeb(itemId);
                    break;
                case "pwdatabase.com/en":
                    name = NameFromEnWeb(itemId);
                    break;
            }

            return name;
        }
        public string GetDescription(int itemId, string provider)
        {
            var description = string.Empty;

            switch (provider)
            {
                case "item_ext_desc.txt":
                    description = DescriptionFromItemExtDesc(itemId);
                    break;
            }

            return description;
        }

        public IEnumerable<string> GetAvailableProviders()
        {
            return _availableProviders;
        }

        #endregion

        private string NameFromRuWeb(int itemId)
        {
            return NameFromWeb(itemId, "http://pwdatabase.com/ru");
        }
        private string NameFromEnWeb(int itemId)
        {
            return NameFromWeb(itemId, "http://pwdatabase.com/");
        }

        private string NameFromWeb(int itemId, string url)
        {
            string name = string.Empty;

            try
            {
                var request = (HttpWebRequest)WebRequest.Create($"{url}/items/" + itemId.ToString());
                var response = (HttpWebResponse)request.GetResponse();

                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    string content = streamReader.ReadToEnd();

                    int nameIndex = content.IndexOf("<th class=\"itemHeader\"") + 35;

                    int lenght;

                    if (nameIndex > 35)
                    {
                        lenght = content.IndexOf("<a href", nameIndex) - nameIndex;
                        name = content.Substring(nameIndex, lenght);

                        if (name.Contains("<span class="))
                        {
                            int len = name.IndexOf(">") + 1;
                            int lenTwo = name.IndexOf("</span>") - len;

                            name = name.Substring(len, lenTwo);
                        }

                        name = name.Replace("&#9734;", "");
                        name = name.Replace("\n", "").Replace("\r", "").Replace("\t", "");
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return name;

        }

        private string DescriptionFromItemExtDesc(int itemId)
        {
            var description = string.Empty;

            if (_itemsDescriptions.ContainsKey(itemId))
                description = _itemsDescriptions[itemId];

            return description;
        }
    }
}
