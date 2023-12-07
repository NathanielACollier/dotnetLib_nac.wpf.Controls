using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace nac.wpf.Controls;

/// <summary>
/// Interaction logic for DirectoryPicker.xaml
/// </summary>
public partial class DirectoryPicker : System.Windows.Controls.UserControl
{
    /*
     * Remember that to bind to this Property you need to use the TwoWay mode if you want to write the values back to the property
     * Here is an example of this: <local:DirectoryPicker DirectoryPath="{Binding Path, Mode=TwoWay}"></local:DirectoryPicker>
     */
    public static readonly DependencyProperty DirectoryPathProperty = DependencyProperty.Register(
        "DirectoryPath",
        typeof(string),
        typeof(DirectoryPicker),
        new UIPropertyMetadata(string.Empty, new PropertyChangedCallback(DirectoryPathChangedCallBack)));


    public event EventHandler DirectoryPathChanged;


    public DirectoryPicker()
    {
        InitializeComponent();
    }

    /**
     * <summary>
     *  This property is both so that the user can determine if a directory has been picked, and so that they can get the value
     * </summary>
     */
    public string DirectoryPath
    {
        get
        {
            return (string)GetValue(DirectoryPathProperty);
        }
        set
        {
            SetValue(DirectoryPathProperty, value);
        }
    }

    /**
     * <summary>
     *  This fires when the DirectoryPath is changed so that we can set our textbox
     * </summary>
     */
    static void DirectoryPathChangedCallBack(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    {
        // create a reference to our DirectoryPicker object that contains the DependencyProperty
        DirectoryPicker dp = (DirectoryPicker)sender;

        // our directorypath has changed at this point so raise an event
        if (dp.DirectoryPathChanged != null)
        {
            dp.DirectoryPathChanged(dp, new EventArgs());
        }
    }

    /**
     * <summary>
     *  Uses FolderBrowserDialog to let the user pick a Directory
     * </summary>
     */
    private void PickDirectoryButton_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new nac.wpf.Controls.OpenFolderDialog();


        // if there is allready a folder picked, then use it if it exists.  If something isn't right use Desktop
        if (!string.IsNullOrEmpty(this.DirectoryPath) && System.IO.Directory.Exists(this.DirectoryPath))
        {
            dialog.InitialFolder = this.DirectoryPath;
        }
        else
        {
            dialog.InitialFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
        }

        var result = dialog.ShowDialog(this);

        if (result == System.Windows.Forms.DialogResult.OK)
        {
            this.DirectoryPath = dialog.Folder;
        }

    }

}
