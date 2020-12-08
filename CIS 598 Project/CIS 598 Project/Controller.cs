using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIS_598_Project
{
    public class Controller
    {
        public static BindingList<Preset> presets { get; set; } = new BindingList<Preset>();

        public delegate void Callback(BindingList<Preset> bl);

        static Controller()
        {
            
        }

        public void GetPresets(Callback obj)
        {
            obj(presets);
        }

        public void CloseMainWindow()
        {
            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(BindingList<Preset>));
            System.IO.FileStream file = System.IO.File.Create("SavedPresets.xml");
            writer.Serialize(file, presets);
            file.Close();
            System.Windows.Forms.MessageBox.Show("Presets saved to: \n\n" + file.Name);
        }

        public void AddPreset(string s, int d, int f)
        {
            Preset newPreset = new Preset();
            newPreset.Name = s;
            newPreset.Delay = d;
            newPreset.Frequency = f;
            presets.Add(newPreset);
        }

        public void LoadMainWindow()
        {
            ReadSavedPresets();
        }

        public void ReadSavedPresets()
        {
            try
            {
                System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(BindingList<Preset>));
                System.IO.StreamReader file = new System.IO.StreamReader("SavedPresets.xml");
                presets = (BindingList<Preset>)reader.Deserialize(file);
                file.Close();
            }
            catch (System.IO.FileNotFoundException)
            {

            }
        }

    }
}
