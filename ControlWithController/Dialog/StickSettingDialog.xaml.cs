using System.Windows;

namespace CodeGenerater.Diablo3.ControlWithController
{
	public partial class StickSettingDialog : Window
	{
		#region Constructor
		public StickSettingDialog()
		{
			InitializeComponent();
		}
		#endregion

		#region Field
		StickSetting _Setting;
		#endregion

		#region Property
		public StickSetting Setting
		{
			set => DataContext = _Setting = value;
			get => _Setting;
		}
		#endregion
	}
}
