using System.Diagnostics.CodeAnalysis;

namespace MontyHallProblem
{
	public struct DoorNumber
	{
		private ushort _value;
		private DoorNumber(ushort val)
		{
			if (val > 3) throw new ArgumentOutOfRangeException(nameof(val));
			this._value = val;
		}
		public static explicit operator DoorNumber(ushort value)
		{
			return new DoorNumber(value);
		}
		public static implicit operator ushort(DoorNumber me)
		{
			return me._value;
		}
		public static List<DoorNumber> All()
		{
			return new List<DoorNumber> { (DoorNumber)1, (DoorNumber)2, (DoorNumber)3 };
		}
	}
}