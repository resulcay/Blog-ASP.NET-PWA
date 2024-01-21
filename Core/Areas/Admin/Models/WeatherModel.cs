namespace Core.Areas.Admin.Models
{
	public class WeatherModel
	{
		public double Latitude { get; set; }
		public double Longitude { get; set; }
		public double Generationtime_ms { get; set; }
		public int Utc_offset_seconds { get; set; }
		public string Timezone { get; set; }
		public string Timezone_abbreviation { get; set; }
		public double Elevation { get; set; }
		public CurrentUnits Current_units { get; set; }
		public Current Current { get; set; }
	}

	public class Current
	{
		public string Time { get; set; }
		public int Interval { get; set; }
		public double Temperature_2m { get; set; }
	}

	public class CurrentUnits
	{
		public string Time { get; set; }
		public string Interval { get; set; }
		public string Temperature_2m { get; set; }
	}
}
