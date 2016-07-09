using System;
using System.Runtime.Serialization;

namespace EndnoteProcessor
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

    [DataContract]
    struct NoteExportInfo
    {
        public enum Type
        {
            [EnumMember(Value = "B")]
            B,
            [EnumMember(Value = "C")]
            C,
            [EnumMember(Value = "J")]
            J,
            [EnumMember(Value = "L")]
            L,
            [EnumMember(Value = "P")]
            P,
            [EnumMember(Value = "M")]
            M
        }
        
        public Type SourceType;
        [DataMember(Name = "type")]
        public string SourceTypeHack { get { return SourceType.ToString(); } private set { } }

        [DataMember(Name ="citation")]
        public string Citation;
    }
}
