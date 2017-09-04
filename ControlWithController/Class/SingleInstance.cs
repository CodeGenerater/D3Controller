using System;

namespace CodeGenerater.Diablo3.ControlWithController
{
	static class SingleInstance<T>
	{
		#region Field
		static T _Instance;
		#endregion

		#region Property
		public static T Instance
		{
			get
			{
				if (_Instance == null)
					_Instance = (T)typeof(T).GetConstructor(Type.EmptyTypes).Invoke(new object[0]);
				return _Instance;
			}
		}
		#endregion
	}
}