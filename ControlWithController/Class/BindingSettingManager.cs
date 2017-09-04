using System.Collections.Generic;
using System.Collections.Concurrent;
using CodeGenerater.Diablo3.Controller;

namespace CodeGenerater.Diablo3.ControlWithController
{
	class BindingSettingManager
	{
		#region Field
		ConcurrentDictionary<PadButtons, KeySetting> KeySettingDict;
		#endregion
	}
}