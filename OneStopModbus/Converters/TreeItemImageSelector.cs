using DevExpress.Xpf.Core;
using DevExpress.Xpf;
using System;
using DevExpress.Xpf.Grid;
using System.Windows.Forms;
using System.Windows;
using DevExpress.Utils;
using System.Windows.Media;

namespace OneStopModbus.Converters
{
    public class TreeItemImageSelector : TreeListNodeImageSelector
    {
        static TreeItemImageSelector()
        {
        }

        public override ImageSource Select(DevExpress.Xpf.Grid.TreeList.TreeListRowData rowData)
        {
            string ty = rowData.Row.GetType().ToString();
            if (rowData.Row is OneStopModbus.Connection.Device)
            {
                return GetImage("TreeItemDevice.svg");
            }
            else if (rowData.Row is OneStopModbus.Connection.ParameterGroup)
            {
                return GetImage("TreeItemParameterGroup.svg");
            }
            else
            {
                return GetImage("TreeItemParameter.svg");
            }
        }

        private static ImageSource GetImage(string imageName)
        {
            string path = "pack://application:,,,/OneStopModbus;component/Images/" + imageName;
            var extension = new SvgImageSourceExtension() { Uri = new Uri(path), Size = new Size(16, 16) };
            var image = (ImageSource)extension.ProvideValue(null);
            return image;
        }
    }
}