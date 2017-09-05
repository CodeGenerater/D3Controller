using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace CodeGenerater.Diablo3.ControlWithController
{
    static class SerializationManager
    {
		#region Field
		const string FileName = "Config";
		#endregion

		#region Property
		static BindingSettingManager SettingManager
		{
			get
			{
				if (!SingleInstance<BindingSettingManager>.IsExist)
					return null;
				else
					return SingleInstance<BindingSettingManager>.Instance;
			}
		}
		#endregion

		#region Method
		public static void Load()
		{
			using (FileStream Stream = new FileStream(FileName, FileMode.OpenOrCreate))
			{
				BinaryFormatter f = new BinaryFormatter();

				try
				{
					var List = f.Deserialize(Stream) as List<object>;
					BindToInstance(List);
				}
				catch (ArgumentNullException)
				{

				}
				catch (SerializationException)
				{

				}
			}
		}

		public static void Save()
		{
			using (FileStream Stream = new FileStream(FileName, FileMode.OpenOrCreate))
			{
				BinaryFormatter f = new BinaryFormatter();

				var List = GetInstances();

				f.Serialize(Stream, List);
			}
		}
		#endregion

		#region Helper
		static void BindToInstance(List<object> Array)
		{
			if (Array == null) throw new ArgumentNullException(nameof(Array));

			foreach (var each in Array)
				if (each == null)
					continue;
				else if (each is BindingSettingManager)
					SingleInstance<BindingSettingManager>.Instance = each as BindingSettingManager;
					
		}

		static List<object> GetInstances()
		{
			List<object> List = new List<object>();

			if (SingleInstance<BindingSettingManager>.IsExist)
				List.Add(SingleInstance<BindingSettingManager>.Instance);

			return List;
		}
		#endregion
	}
}