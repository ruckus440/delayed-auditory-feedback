/*
 * Preset.cs
 * Author: Mike Ruckert
 * CIS 598 Senior Project - Kansas State University
 * 
 * Model class for Preset objects of the DAF/AAF app. Implements INotifyPropertyChanged.
 */

using System;
using System.ComponentModel;

namespace CIS_598_Project
{
    [Serializable]
    public class Preset : INotifyPropertyChanged
    {
        /// <summary>
        /// The name of the Preset.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The amount of delay for the Preset.
        /// </summary>
        public int Delay { get; set; }
        /// <summary>
        /// The frequency of the Preset.
        /// </summary>
        public int Frequency { get; set; }

        /// <summary>
        /// Event handler for when a property is changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Invokes the property changed event.
        /// </summary>
        /// <param name="propertyName"></param>
        protected void NotifyPropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Overrides the Preset object's ToString method.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
