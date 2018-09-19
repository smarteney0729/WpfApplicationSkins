using System;
using System.IO;
using System.Windows;

namespace WpfApp3
{
  internal class Skin : ResourceDictionary
  {
    private string _skinName = string.Empty;
    public string SkinName {
      get => _skinName;
      set {
        if (_skinName.Equals(value, StringComparison.InvariantCultureIgnoreCase)) return;
        if (string.IsNullOrEmpty(value)) {
          _skinName = string.Empty;
          Clear();
        } else {
          _skinName = value;
          Reload(_skinName);
        }
      }
    }
    private string _name = string.Empty;
    public string Name {
      get => _name;
      set {
        if (_name.Equals(value, StringComparison.InvariantCultureIgnoreCase)) return;
        if (string.IsNullOrEmpty(value)) {
          _name = string.Empty;
          Clear();
        } else {
          _name = value;
          Reload(_skinName);
        }
      }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="skin"></param>
    /// <param name="name">Accepts name either with .xaml extension or not.</param>
    /// <exception cref="ArgumentException"
    /// <returns>A new skin resource dictionary for the given skin name and resource dictionary name.</returns>
    public static ResourceDictionary Load(string skin, string name)
    {
      var skinResources = new Skin();
      if (string.IsNullOrEmpty(Path.GetExtension(name))){
        Path.ChangeExtension(name, "xaml");
      }

      var path = Path.Combine("skins", skin,name);
      var skinSource = new Uri(path, UriKind.Relative);

      skinResources.SkinName = skin;
      skinResources.Name = name;
      skinResources.Load();
      return skinResources;
    }

    internal void Reload(string skinName)
    {
      //TODO: Ensure thread safety, not currently thread safe
      if (SkinName.Equals(skinName, StringComparison.OrdinalIgnoreCase)) return;
      Clear();
      SkinName = skinName;
      Load();
    }

    private void Load()
    {
      if (string.IsNullOrEmpty(SkinName) || string.IsNullOrEmpty(Name)) return;
      var name = Path.ChangeExtension(Name, ".xaml");
      var path = Path.Combine("skins", SkinName, name);
      var skinSource = new Uri(path, UriKind.Relative);

      try {
        Source = skinSource;
      }
      catch (Exception ex) {
        Source = null;
      }

    }
  }
}