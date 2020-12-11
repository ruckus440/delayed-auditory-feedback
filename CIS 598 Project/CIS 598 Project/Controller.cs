/*
 * Controller.cs
 * Author: Mike Ruckert
 * CIS 598 Senior Project - Kansas State University
 * 
 * Controller of the DAF/AAF app.
 */
using System.ComponentModel;

namespace CIS_598_Project
{
    public class Controller
    {
        /// <summary>
        /// The list of Presets.
        /// </summary>
        public static BindingList<Preset> Presets { get; set; } = new BindingList<Preset>();

        /// <summary>
        /// Delegate for passing the BindingList of Presets between the Controller and views.
        /// </summary>
        /// <param name="bl"></param>
        public delegate void Callback(BindingList<Preset> bl);

        /// <summary>
        /// Constructor.
        /// </summary>
        static Controller()
        {

        }

        /// <summary>
        /// Gets the saved Preset list by using the Callback delegate.
        /// </summary>
        /// <param name="obj"></param>
        public void GetPresets(Callback obj)
        {
            obj(Presets);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bl"></param>
        public void SetPresets(BindingList<Preset> bl)
        {
            Presets = bl;
        }

        /// <summary>
        /// Serialize the saved list of Presets to a serialized xml file.
        /// </summary>
        public void SerializeSavedPresets()
        {
            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(BindingList<Preset>));
            System.IO.FileStream file = System.IO.File.Create("SavedPresets.xml");
            writer.Serialize(file, Presets);
            file.Close();
        }

        /// <summary>
        /// Adds a Preset to the list using the selected name, delay and frequency.
        /// </summary>
        /// <param name="s">Name</param>
        /// <param name="d">Delay</param>
        /// <param name="f">Frequency</param>
        public void AddPreset(string s, int d, int f)
        {
            Preset newPreset = new Preset();
            newPreset.Name = s;
            newPreset.Delay = d;
            newPreset.Frequency = f;
            Presets.Add(newPreset);
        }

        public void LoadMainWindow()
        {
            ReadSavedPresets();
        }

        /// <summary>
        /// Reads the saved presets from the serialized list of Presets.
        /// </summary>
        public void ReadSavedPresets()
        {
            try
            {
                System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(BindingList<Preset>));
                System.IO.StreamReader file = new System.IO.StreamReader("SavedPresets.xml");
                Presets = (BindingList<Preset>)reader.Deserialize(file);
                file.Close();
            }
            catch (System.IO.FileNotFoundException)
            {

            }
        }

        /// <summary>
        /// Updates the selected Preset.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="d"></param>
        /// <param name="f"></param>
        public void UpdatePresetSettings(int i, int d, int f)
        {
            Presets[i].Delay = d;
            Presets[i].Frequency = f;
        }
    }
}
