using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Documents;
using System.Windows.Media;
using Caliburn.Micro;

namespace ProjectAegis.Shop.Models
{
    using Shared.Extensions;
    using Shared.Interfaces;

    using System.IO;
    using System.Collections.ObjectModel;

    public class Item : PropertyChangedBase, IBinaryModel
    {
        #region Private Members

        private byte[] _name;
        private byte[] _texture;
        private byte[] _description;

        #endregion

        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public int ItemId { get; set; }
        public int ItemAmount { get; set; }
        public int Status { get; set; }

        public string Texture
        {
            get { return Encoding.GetEncoding("GBK").GetString(_texture).Replace("\0", ""); }
            set
            {
                var en = Encoding.GetEncoding("GBK");

                var endSource = new byte[128];
                var temp = en.GetBytes(value.Replace("\0", ""));

                Array.Copy(temp, endSource, temp.Length > endSource.Length ? endSource.Length : temp.Length);

                _texture = endSource;
            }
        }
        public string TextureForIcon => Encoding.GetEncoding("GBK").GetString(_texture).Replace("\0", "").Replace(".dds", ".jpg");
        public string Description
        {
            get
            {
                return Encoding.Unicode.GetString(_description).Replace("\0", "");
            }
            set
            {
                var en = Encoding.Unicode;

                var endSource = new byte[1024];
                var temp = en.GetBytes(value.Replace("\0", ""));

                Array.Copy(temp, endSource, temp.Length > endSource.Length ? endSource.Length : temp.Length);

                _description = endSource;

                NotifyOfPropertyChange();
                NotifyOfPropertyChange(nameof(ColorizedDescription));
            }
        }

        public FlowDocument ColorizedDescription => new FlowDocument(BuildColor());

        public string Name
        {
            get
            {
                return Encoding.Unicode.GetString(_name).Replace("\0", "");
            }
            set
            {
                var en = Encoding.Unicode;

                var endSource = new byte[64];
                var temp = en.GetBytes(value);

                Array.Copy(temp, endSource, temp.Length > endSource.Length ? endSource.Length : temp.Length);

                _name = endSource;

                NotifyOfPropertyChange();
            }
        }
        public byte[] Unk { get; set; }

        public BindableCollection<Price> Prices { get; set; }

        public int GiftId { get; set; }
        public int GiftAmount { get; set; }
        public int GiftDuration { get; set; }
        public int LogPrice { get; set; }

        public Item()
        {
            _name = new byte[0];
            _description = new byte[0];
            _texture = new byte[0];

            Prices = new BindableCollection<Price>()
            {
                new Price(),
                new Price(),
                new Price(),
                new Price()
            };
        }

        public Paragraph BuildColor()
        {
            var descriptions = Description.Replace("\\r", "\n").Split(new[] {"^"}, StringSplitOptions.None);

            var paragraph = new Paragraph();
            var brushConverter = new BrushConverter();

            if (descriptions.Length > 0)
            {
                bool check = true;

                for (int i = 0; i < descriptions[0].Length; i++)
                {
                    if (descriptions[0][i] != '\n')
                        check = false;
                }

                if (!check)
                    for (int i = 0; i < descriptions[0].Length; i++)
                    {
                        if (descriptions[0][i] != '\n')
                        {
                            descriptions[0] = descriptions[0].Substring(i);
                            break;
                        }
                    }
                else
                    descriptions[0] = "";


                foreach (var description in descriptions)
                {
                    var rx = new Regex(@"(?i)(\d|[A-F]){6}$");

                    if (description.Length > 7)
                    {
                        var colors = description.Split('^');

                        foreach (var color in colors)
                        {
                            if (color != "")
                            {
                                if (rx.IsMatch(color.Substring(0, 6)))
                                {
                                    try
                                    {
                                        var brush =
                                            (Brush)brushConverter.ConvertFromString("#" + color.Substring(0, 6));
                                        var text = color.Substring(6);

                                        paragraph.Inlines.Add(new Run(text) { Foreground = brush });
                                    }
                                    catch { }
                                }
                                else
                                    paragraph.Inlines.Add(new Run(color) { Foreground = Brushes.White });
                            }
                        }
                    }
                    else
                        paragraph.Inlines.Add(new Run(description) { Foreground = Brushes.White });

                }
            }
            return paragraph;
        }

        #region Implementation of IBinaryModel

        public void ReadModel(BinaryReader reader, int version = 0)
        {
            Id = reader.ReadInt32();

            CategoryId = reader.ReadInt32();
            SubCategoryId = reader.ReadInt32();

            _texture = reader.ReadBytes(128);

            ItemId = reader.ReadInt32();
            ItemAmount = reader.ReadInt32();

            Prices.Clear();
            for (int i = 0; i < 4; i++)
                Prices.Add(reader.ReadModel<Price>(version));

            if (version == 126)
                Status = reader.ReadInt32();

            _description = reader.ReadBytes(1024);
            _name = reader.ReadBytes(64).Clear(64);

            if (version >= 144)
            {
                GiftId = reader.ReadInt32();
                GiftAmount = reader.ReadInt32();
                GiftDuration = reader.ReadInt32();
                LogPrice = reader.ReadInt32();
            }
        }

        public void WriteModel(BinaryWriter writer, int version = 0)
        {
            writer.Write(Id);

            writer.Write(CategoryId);
            writer.Write(SubCategoryId);

            writer.Write(_texture);

            writer.Write(ItemId);
            writer.Write(ItemAmount);

            foreach (var price in Prices)
                writer.WriteModel(price, version);

            if (version == 126)
            {
                writer.Write(Unk);
                writer.Write(Status);
            }

            writer.Write(_description);
            writer.Write(_name);

            if (version >= 144)
            {
                writer.Write(GiftId);
                writer.Write(GiftAmount);
                writer.Write(GiftDuration);
                writer.Write(LogPrice);
            }
        }

        #endregion
    }
}