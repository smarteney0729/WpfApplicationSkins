using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WpfApp3
{
  public class ChangeSkinCommand : ICommand
  {
    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter)
    {
      if (!(parameter is string)) return false;
      if (string.IsNullOrEmpty((string)parameter)) return false;

      //Is the skin already loaded / the current skin?
      var loadedSkin = Application.Current.Resources.MergedDictionaries
        .Where(d => d is Skin)
        .Cast<Skin>()
        .FirstOrDefault();

      if (loadedSkin is null) return true;
      if (loadedSkin.SkinName.Equals((string)parameter, StringComparison.OrdinalIgnoreCase)) return false;

      //TODO: Raise CanExecuteChanged if appropraite.
      return true;
    }

    public void Execute(object parameter)
    {
      if (!(parameter is string)) return;
      var skinName = parameter as string;

      var resources = Application.Current.Resources.MergedDictionaries;

      foreach (var d in resources) {
        if (d is Skin skin) {
          skin.Reload(skinName);
        } else {
          d.Source = d.Source;
        }
      }

    }
  }
}
