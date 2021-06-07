using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryMailUp.Entities
{
	public class Item
	{
		public int? Count { get; set; }
		public bool Deletable { get; set; }
		public string Name { get; set; }
		public string Notes { get; set; }
		public int? idGroup { get; set; }
		public int? idList { get; set; }
	}

	public class Version
	{
		public int major { get; set; }
		public int minor { get; set; }
		public int build { get; set; }
		public int revision { get; set; }
		public int majorRevision { get; set; }
		public int minorRevision { get; set; }
	}

	public class ResponseContent
	{
		public bool IsPaginated { get; set; }
		public Item[] Items { get; set; }
		public int PageNumber { get; set; }
		public int PageSize { get; set; }
		public int Skipped { get; set; }
		public int TotalElementsCount { get; set; }
		public string Type { get; set; }
		public Version Version { get; set; }
		public int StatusCode { get; set; }
		public string ReasonPhrase { get; set; }
		public string[] Headers { get; set; }
		public string[] TrailingHeaders { get; set; }
		public string Message { get; set; }
		public bool isSuccessStatusCode { get; set; }
	}
}
