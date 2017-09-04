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
			set
			{
				if (_Instance != null)
					throw new ArgumentException();

				_Instance = value;
			}
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