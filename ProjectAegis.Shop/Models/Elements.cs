namespace ProjectAegis.Shop.Models
{
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;

    using Caliburn.Micro;

    using Shared.Interfaces;

    using Virtualization.Interfaces;

    public class Element
    {
        #region Private Members
        
        private string _name;

        #endregion

        public int Id { get; set; }
        public string Name
        {
            get { return _name.Replace("\0", ""); }
            set { _name = value; }
        }
    }

    public class ElementsList : IItemsProvider<Element>
    {
        public string Name { get; set; }
        public BindableCollection<Element> Items { get; set; }

        public ElementsList()
        {
            Items = new BindableCollection<Element>();
        }

        #region Implementation of IItemsProvider<Element>

        public int FetchCount()
        {
            return Items.Count;
        }

        public IList<Element> FetchRange(int startIndex, int count)
        {
            return Items.Skip(startIndex).Take(count).ToList();
        }

        #endregion
    }

    public class Elements : BindableCollection<ElementsList>, IBinaryModel
    {
        #region Implementation of IBinaryModel

        public void ReadModel(BinaryReader reader, int version = 0, params object[] parameters)
        {
            var localVersion = reader.ReadInt16();
            var signature = reader.ReadInt16();

            var fileConfig =
                Directory.GetFiles(Directory.GetCurrentDirectory() + @"\configs\", "PW_*_v" + localVersion + ".cfg")
                    .FirstOrDefault();
            
            if (fileConfig == null)
                throw new FileNotFoundException($"Не найдено подходящего конфига для версии: {localVersion}");

            using (var configReader = new StreamReader(new FileStream(fileConfig, FileMode.Open, FileAccess.Read)))
            {
                var listCount = configReader.ReadLine();
                var conversationIndex = configReader.ReadLine();

                int listIndex = 0;
                while (!configReader.EndOfStream)
                {
                    var line = configReader.ReadLine();

                    if (!string.IsNullOrEmpty(line) && !line.StartsWith("#"))
                    {
                        var listItem = new ElementsList {Name = line};

                        var offset = configReader.ReadLine();

                        var names = configReader.ReadLine().Split(';');
                        var types = configReader.ReadLine().Split(';');

                        if (offset != "AUTO")
                            if (int.Parse(offset) > 0)
                                reader.ReadBytes(int.Parse(offset));
                    
                        #region List20

                        if (listIndex == 20)
                        {
                            reader.ReadInt32();
                            var length = reader.ReadInt32();
                            reader.ReadBytes(length);
                            reader.ReadInt32();
                        }

                        #endregion
                                                
                        #region List100

                        if (listIndex == 100)
                        {
                            reader.ReadInt32();
                            var length = reader.ReadInt32();
                            reader.ReadBytes(length);
                        }

                        #endregion

                        #region Conversation

                        if (listIndex == int.Parse(conversationIndex))
                        {
                            byte[] pattern = Encoding.GetEncoding("GBK").GetBytes("facedata\\");

                            long sourcePosition = reader.BaseStream.Position;
                            int listLength = -72 - pattern.Length;
                            bool run = true;
                            while (run)
                            {
                                run = false;
                                for (int i = 0; i < pattern.Length; i++)
                                {
                                    listLength++;

                                    if (reader.ReadByte() != pattern[i])
                                    {
                                        run = true;
                                        break;
                                    }
                                }
                            }

                            long w = reader.BaseStream.Position - 81;
                            reader.BaseStream.Position = w;

                            listIndex++;
                            continue;
                        }

                        #endregion

                        int count = reader.ReadInt32();

                        for (int j = 0; j < count; j++)
                        {
                            var item = new Element();

                            if (int.Parse(conversationIndex) != listIndex)
                            {
                                for (int i = 0; i < types.Length; i++)
                                {
                                    if (names[i] == "ID")
                                        item.Id = (int) ReadValue(types[i], reader);
                                    else if (names[i] == "Name")
                                        item.Name = (string) ReadValue(types[i], reader);
                                    else
                                        ReadValue(types[i], reader);
                                }

                                listItem.Items.Add(item);
                            }
                        }

                        Add(listItem);

                        listIndex++;
                    }
                }
            }
        }

        public void WriteModel(BinaryWriter writer, int version = 0, params object[] parameters)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        public object ReadValue(string type, BinaryReader binaryReader)
        {
            if (type == "int16")
                return binaryReader.ReadInt16();
            if (type == "int32")
                return binaryReader.ReadInt32();
            if (type == "int64")
                return binaryReader.ReadInt64();
            if (type == "float")
                return binaryReader.ReadSingle();
            if (type == "double")
                return binaryReader.ReadDouble();
            if (type.Contains("byte:"))
                return Encoding.Unicode.GetString(binaryReader.ReadBytes(int.Parse(type.Substring(5))));
            if (type.Contains("wstring"))
                return Encoding.Unicode.GetString(binaryReader.ReadBytes(int.Parse(type.Substring(8))));
            if (type.Contains("string"))
                return Encoding.Unicode.GetString(binaryReader.ReadBytes(int.Parse(type.Substring(7))));

            return null;
        }
    }
}