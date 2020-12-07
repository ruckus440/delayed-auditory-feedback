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
        public static BindingList<Preset> presets = new BindingList<Preset>();

        public Controller()
        {

        }

        public void CloseMainWindow()
        {
            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(BindingList<Preset>));
            System.IO.FileStream file = System.IO.File.Create("SavedPreset.xml");
            writer.Serialize(file, presets);
            file.Close();
            System.Windows.Forms.MessageBox.Show("Presets saved to: \n\n" + file.Name);
        }

        public void AddPreset()
        {
            Preset preset = new Preset();
            preset.Name = 
        }

        
    }
}
