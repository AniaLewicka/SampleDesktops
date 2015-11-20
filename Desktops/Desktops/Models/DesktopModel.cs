using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace Desktops.Models
{

    public class DesktopModel
    {
        private string _name;
        private int _value;
        private string _comments;

        public string Name
        {
            get { return _name; }
        }

        public int Value
        {
            get { return _value; }
            set { SetAndSaveProperty(ref _value, value); }
        }

        public string Comments
        {
            get { return _comments; }
            set { SetAndSaveProperty(ref _comments, value); }
        }

        public DesktopModel(SerializableDesktop desktop)
        {
            _name = desktop.Name;
            _value = desktop.Value;
            _comments = desktop.Comments;
        }

        static public List<DesktopModel> GetDesktopModels()
        {
            HashSet<SerializableDesktop> desktopSet = GetSerializableDesktops();
            var list = new List<DesktopModel>();
            foreach (var desktop in desktopSet)
            {
                list.Add(new DesktopModel(desktop));
            }
            return list;
        }

        private void SetAndSaveProperty<T>(ref T field, T value, [CallerMemberName] string property = null)
        {
            if (object.Equals(field, value))
                return;

            var oldValue = field;
            field = value;
            bool saved = Save();

            if (!saved)
            {
                field = oldValue;
            }
        }

        private bool Save()
        {
            bool saved = false;
            HashSet<SerializableDesktop> desktopSet = GetSerializableDesktops();
            XmlSerializer serializer = new XmlSerializer(typeof(HashSet<SerializableDesktop>));

            var desktop = new SerializableDesktop(this);
            bool removed = desktopSet.Remove(desktop);
            bool added = desktopSet.Add(desktop);
            if (!(removed && added))
                return saved;

            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    serializer.Serialize(writer, desktopSet);
                    saved = true;
                }
            }
            catch (Exception _)
            {
            }

            return saved;
        }

        static private HashSet<SerializableDesktop> GetSerializableDesktops()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(HashSet<SerializableDesktop>));
            HashSet<SerializableDesktop> desktopSet = new HashSet<SerializableDesktop>();

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    desktopSet = (HashSet<SerializableDesktop>) serializer.Deserialize(reader);
                }
            }
            catch (Exception _)
            {
            }

            return desktopSet;
        }

        public class SerializableDesktop : IEquatable<SerializableDesktop>
        {
            public string Name;
            public int Value;
            public string Comments;

            // Required by XmlSerializer
            public SerializableDesktop() { }

            public SerializableDesktop(DesktopModel desktop)
            {
                Name = desktop._name;
                Value = desktop._value;
                Comments = desktop._comments;
            }

            override public int GetHashCode()
            {
                return Name.GetHashCode();
            }

            public bool Equals(SerializableDesktop other)
            {
                return Name == other.Name;
            }
        }
        
        static private string filePath = "DesktopsConfiguration.xml";
        
    }

        
}
