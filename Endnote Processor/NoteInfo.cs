using System;

namespace FirstVistaTest
{
    [Serializable]
	public class NoteInfo
	{
		public bool SupraOrId;

		public int Type;

		public bool Processed;

		public NoteInfo()
		{
			this.SupraOrId = false;
			this.Type = 0;
			this.Processed = false;
		}

		public NoteInfo copy()
		{
			return new NoteInfo
			{
				Processed = this.Processed,
				Type = this.Type,
				SupraOrId = this.SupraOrId
			};
		}
	}
}
